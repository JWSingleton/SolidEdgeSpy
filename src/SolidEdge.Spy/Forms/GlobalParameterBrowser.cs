using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SolidEdge.Spy.InteropServices;
using System.Runtime.InteropServices;

namespace SolidEdge.Spy.Forms
{
    public partial class GlobalParameterBrowser : UserControl
    {
        public GlobalParameterBrowser()
        {
            InitializeComponent();
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            RefreshGlobalParameters();

            Cursor.Current = Cursors.Default;
        }

        public void RefreshGlobalParameters()
        {
            ComPtr pApplication = IntPtr.Zero;
            if (MarshalEx.Succeeded(MarshalEx.GetActiveObject("SolidEdge.Application", out pApplication)))
            {
                SelectedObject = new GlobalParameterInfo(pApplication);
            }
        }

        public object SelectedObject
        {
            get { return propertyGrid.SelectedObject; }
            set
            {
                GlobalParameterInfo globalParameterInfo = propertyGrid.SelectedObject as GlobalParameterInfo;
                if (globalParameterInfo != null)
                {
                    globalParameterInfo.Dispose();
                }

                propertyGrid.SelectedObject = value;
            }
        }
    }

    public class GlobalParameterInfo : ICustomTypeDescriptor, IDisposable
    {
        private ComPtr _pApplication = IntPtr.Zero;
        private List<SolidEdgeFramework.ApplicationGlobalConstants> _colorGlobalConstants = new List<SolidEdgeFramework.ApplicationGlobalConstants>();

        public GlobalParameterInfo(ComPtr pApplication)
        {
            _pApplication = pApplication;

            try
            {
                var type = typeof(SolidEdgeFramework.ApplicationGlobalConstants);
                var enumNames = type.GetEnumNames();
                var enumValues = type.GetEnumValues();

                // Build list of global constants that represent color using the constant name.
                for (int i = 0; i < enumNames.Length; i++)
                {
                    if (enumNames[i].Contains("Color"))
                    {
                        _colorGlobalConstants.Add((SolidEdgeFramework.ApplicationGlobalConstants)enumValues.GetValue(i));
                    }
                }

                // These don't have "Color" in their name but are colors. Add manually to list.
                _colorGlobalConstants.Add(SolidEdgeFramework.ApplicationGlobalConstants.seApplicationGlobalInside);
                _colorGlobalConstants.Add(SolidEdgeFramework.ApplicationGlobalConstants.seApplicationGlobalInsideOutside);
            }
            catch
            {
            }
        }

        #region "TypeDescriptor Implementation"

        public String GetClassName() { return TypeDescriptor.GetClassName(this, true); }
        public AttributeCollection GetAttributes() { return TypeDescriptor.GetAttributes(this, true); }
        public String GetComponentName() { return TypeDescriptor.GetComponentName(this, true); }
        public TypeConverter GetConverter() { return TypeDescriptor.GetConverter(this, true); }
        public EventDescriptor GetDefaultEvent() { return TypeDescriptor.GetDefaultEvent(this, true); }
        public PropertyDescriptor GetDefaultProperty() { return TypeDescriptor.GetDefaultProperty(this, true); }
        public object GetEditor(Type editorBaseType) { return TypeDescriptor.GetEditor(this, editorBaseType, true); }
        public EventDescriptorCollection GetEvents(Attribute[] attributes) { return TypeDescriptor.GetEvents(this, attributes, true); }
        public EventDescriptorCollection GetEvents() { return TypeDescriptor.GetEvents(this, true); }

        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            if ((_pApplication == null) || (_pApplication.IsInvalid)) return new PropertyDescriptorCollection(new PropertyDescriptor[] { });

            List<GlobalParameterPropertyDescriptor> list = new List<GlobalParameterPropertyDescriptor>();

            try
            {
                ComTypeLibrary comTypeLibrary = ComTypeManager.Instance.ComTypeLibraries.Where(x => x.Name.Equals("SolidEdgeFramework")).FirstOrDefault();

                if (comTypeLibrary != null)
                {
                    ComEnumInfo enumInfo = comTypeLibrary.Enums.Where(x => x.Name.Equals("ApplicationGlobalConstants")).FirstOrDefault();

                    foreach (ComVariableInfo variableInfo in enumInfo.Variables)
                    {
                        SolidEdgeFramework.ApplicationGlobalConstants globalConst = (SolidEdgeFramework.ApplicationGlobalConstants)variableInfo.ConstantValue;

                        try
                        {
                            object[] args = new object[] { globalConst, new VariantWrapper(null) };
                            object returnValue = null;

                            if (MarshalEx.Succeeded(_pApplication.TryInvokeMethod("GetGlobalParameter", args, out returnValue)))
                            {
                                if (args[1] != null)
                                {
                                    Type propertyType = args[1].GetType();

                                    string name = variableInfo.Name.Replace("seApplicationGlobal", string.Empty);
                                    StringBuilder description = new StringBuilder();
                                    description.AppendLine(variableInfo.Description);
                                    description.AppendLine(String.Format("Application.GetGlobalParameter({0}.{1}, out value)", enumInfo.FullName, variableInfo.Name));

                                    GlobalParameterProperty property = new GlobalParameterProperty(name, description.ToString(), args[1], propertyType, true);

                                    list.Add(new GlobalParameterPropertyDescriptor(ref property, attributes));

                                    try
                                    {
                                        if (_colorGlobalConstants.Contains(globalConst))
                                        {
                                            var color = Color.Empty;

                                            if (args[1] is int)
                                            {
                                                byte[] rgb = BitConverter.GetBytes((int)args[1]);
                                                color = Color.FromArgb(255, rgb[0], rgb[1], rgb[2]);
                                            }
                                            else if (args[1] is uint)
                                            {
                                                byte[] rgb = BitConverter.GetBytes((uint)args[1]);
                                                color = Color.FromArgb(255, rgb[0], rgb[1], rgb[2]);
                                            }
                                            else
                                            {
#if DEBUG
                                                //System.Diagnostics.Debugger.Break();
#endif
                                            }

                                            if (color.IsEmpty == false)
                                            {
                                                description = new StringBuilder();
                                                description.AppendLine(property.Description);
                                                description.AppendLine("byte[] rgb = BitConverter.GetBytes((int)value)");
                                                description.AppendLine("Color color = Color.FromArgb(255, rgb[0], rgb[1], rgb[2]");

                                                property = new GlobalParameterProperty(String.Format("{0} (converted to color)", property.Name), description.ToString(), color, color.GetType(), true);

                                                list.Add(new GlobalParameterPropertyDescriptor(ref property, attributes));
                                            }
                                        }

                                        //switch (globalConst)
                                        //{
                                        //    case SolidEdgeFramework.ApplicationGlobalConstants.seApplicationGlobalColorLiveSectionEdge:
                                        //    case SolidEdgeFramework.ApplicationGlobalConstants.seApplicationGlobalColorLiveSectionCenterline:
                                        //    case SolidEdgeFramework.ApplicationGlobalConstants.seApplicationGlobalColorLiveSectionRegion:
                                        //    case SolidEdgeFramework.ApplicationGlobalConstants.seApplicationGlobalColorLiveSectionOpacity:
                                        //    case SolidEdgeFramework.ApplicationGlobalConstants.seApplicationGlobalColorSheetTab1:
                                        //    case SolidEdgeFramework.ApplicationGlobalConstants.seApplicationGlobalColorSheetTab2:
                                        //    case SolidEdgeFramework.ApplicationGlobalConstants.seApplicationGlobalColorActive:
                                        //    case SolidEdgeFramework.ApplicationGlobalConstants.seApplicationGlobalColorBackground:
                                        //    case SolidEdgeFramework.ApplicationGlobalConstants.seApplicationGlobalColorConstruction:
                                        //    case SolidEdgeFramework.ApplicationGlobalConstants.seApplicationGlobalColorDisabled:
                                        //    case SolidEdgeFramework.ApplicationGlobalConstants.seApplicationGlobalColorFailed:
                                        //    case SolidEdgeFramework.ApplicationGlobalConstants.seApplicationGlobalColorHandle:
                                        //    case SolidEdgeFramework.ApplicationGlobalConstants.seApplicationGlobalColorHighlight:
                                        //    case SolidEdgeFramework.ApplicationGlobalConstants.seApplicationGlobalColorProfile:
                                        //    case SolidEdgeFramework.ApplicationGlobalConstants.seApplicationGlobalColorSelected:
                                        //    case SolidEdgeFramework.ApplicationGlobalConstants.seApplicationGlobalColorSheet:
                                        //    case SolidEdgeFramework.ApplicationGlobalConstants.seApplicationGlobalOverlayColor:
                                        //    case SolidEdgeFramework.ApplicationGlobalConstants.seApplicationGlobalColorRefPlane:
                                        //        if (args[1] is int)
                                        //        {
                                        //            byte[] rgb = BitConverter.GetBytes((int)args[1]);
                                        //            Color color = Color.FromArgb(255, rgb[0], rgb[1], rgb[2]);

                                        //            description = new StringBuilder();
                                        //            description.AppendLine(property.Description);
                                        //            description.AppendLine("byte[] rgb = BitConverter.GetBytes((int)value)");
                                        //            description.AppendLine("Color color = Color.FromArgb(255, rgb[0], rgb[1], rgb[2]");

                                        //            property = new GlobalParameterProperty(String.Format("{0} (converted to color)", property.Name), description.ToString(), color, color.GetType(), true);

                                        //            list.Add(new GlobalParameterPropertyDescriptor(ref property, attributes));
                                        //        }
                                        //        break;
                                        //}
                                    }
                                    catch
                                    {
                                    }
                                }
                            }
                        }
                        catch
                        {
                            GlobalExceptionHandler.HandleException();
                        }
                    }
                }
            }
            catch
            {
                GlobalExceptionHandler.HandleException();
            }

            return new PropertyDescriptorCollection(list.ToArray());
        }

        public PropertyDescriptorCollection GetProperties() { return TypeDescriptor.GetProperties(this, true); }
        public object GetPropertyOwner(PropertyDescriptor pd) { return this; }

        #endregion

        public void Dispose()
        {
            if (_pApplication != null)
            {
                _pApplication.Dispose();
            }
        }
    }

    public class GlobalParameterProperty
    {
        private string _name = string.Empty;
        private bool _readonly = false;
        private object _value = null;
        private string _description = String.Empty;
        private Type _type;

        public GlobalParameterProperty(string sName, string description, object value, Type type, bool bReadOnly)
        {
            _name = sName;
            _description = description;
            _value = value;
            _type = type;
            _readonly = bReadOnly;
        }

        public Type Type { get { return _type; } }

        public bool ReadOnly { get { return _readonly; } }

        public string Name { get { return _name; } }
        public string Description { get { return _description; } }

        public object Value
        {
            get { return _value; }
            set { _value = value; }
        }
    }

    public class GlobalParameterPropertyDescriptor : PropertyDescriptor
    {
        GlobalParameterProperty _property;

        public GlobalParameterPropertyDescriptor(ref GlobalParameterProperty property, Attribute[] attrs)
            : base(property.Name, attrs)
        {
            _property = property;
        }

        #region PropertyDescriptor specific

        public override bool CanResetValue(object component)
        {
            return false;
        }

        public override Type ComponentType
        {
            get { return null; }
        }

        public override object GetValue(object component)
        {
            return _property.Value;
        }

        public override string Description
        {
            get { return _property.Description; }
        }

        public override string Category
        {
            get { return string.Empty; }
        }

        public override string DisplayName
        {
            get { return _property.Name; }
        }

        public override bool IsReadOnly
        {
            get { return _property.ReadOnly; }
        }

        public override void ResetValue(object component)
        {
        }

        public override bool ShouldSerializeValue(object component)
        {
            return false;
        }

        public override void SetValue(object component, object value)
        {
            _property.Value = value;
        }

        public override Type PropertyType
        {
            get { return _property.Type; }
        }

        #endregion
    }
}
