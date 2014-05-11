using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;

namespace SolidEdge.Spy.InteropServices
{
    public class ComPtr : SafeHandle, ICustomTypeDescriptor
    {
        PropertyDescriptorCollection _propertyDescriptorCollection;

        public ComPtr()
            : base(IntPtr.Zero, true)
        {
        }

        public ComPtr(IntPtr pUnk)
            : this()
        {
            if (pUnk.Equals(IntPtr.Zero) == false)
            {
                Marshal.AddRef(pUnk);
            }

            this.SetHandle(pUnk);
        }

        public ComPtr(ComPtr p)
            : this()
        {
            if ((p != null) && (p.IsInvalid == false))
            {
                this.SetHandle(p.handle);
                Marshal.AddRef(p.handle);
            }
        }

        #region Operators

        public static implicit operator IntPtr(ComPtr p)
        {
            return p.handle;
        }

        public static implicit operator ComPtr(IntPtr p)
        {
            return new ComPtr(p);
        }

        public static bool operator true(ComPtr p)
        {
            if (p == null) return false;
            return !p.IsInvalid;
        }

        public static bool operator false(ComPtr p)
        {
            if (p == null) return true;
            return p.IsInvalid;
        }

        #endregion

        #region Overrides

        public override bool IsInvalid
        {
            get { return this.handle == IntPtr.Zero; }
        }

        protected override bool ReleaseHandle()
        {
            if (IsInvalid == false)
            {
                try
                {
                    int count = Marshal.Release(this.handle);
                }
                catch
                {
                    GlobalExceptionHandler.HandleException();
                }
                finally
                {
                    SetHandle(IntPtr.Zero);
                }
            }

            return true;
        }

        public override string ToString()
        {
            return this.handle.ToString();
        }

        #endregion

        #region Properties

        public int RefCount
        {
            get
            {
                if (this.IsInvalid) return 0;

                try
                {
                    Marshal.AddRef(this.handle);
                    return Marshal.Release(this.handle);
                }
                catch
                {
                }

                return 0;
            }
        }

        public bool IsDispatch
        {
            get
            {
                return Is<IDispatch>();
            }
        }

        #endregion

        #region Methods

        public static ComPtr FromRCW(object rcw)
        {
            IntPtr pUnk = IntPtr.Zero;

            try
            {
                pUnk = Marshal.GetIUnknownForObject(rcw);
                int count = Marshal.Release(pUnk);
            }
            catch
            {
            }
            finally
            {
            }

            return new ComPtr(pUnk);
        }

        public bool Is<T>()
        {
            if (this.IsInvalid) return false;

            var riid = typeof(T).GUID;
            IntPtr p = IntPtr.Zero;

            try
            {
                if (MarshalEx.Succeeded(Marshal.QueryInterface(this.handle, ref riid, out p)))
                {
                    int count = Marshal.Release(p);
                    return true;
                }
            }
            catch
            {
            }

            return false;
        }

        public object TryGetFirstAvailableProperty(string[] propertyNames)
        {
            object value = null;
            ComTypeInfo comType = TryGetComTypeInfo();

            if (comType != null)
            {
                foreach (string propertyName in propertyNames)
                {
                    ComPropertyInfo comPropertyInfo = comType.Properties.Where(x => x.Name.Equals(propertyName)).FirstOrDefault();

                    if ((comPropertyInfo != null) && (comPropertyInfo.GetFunction != null))
                    {
                        if (MarshalEx.Succeeded(TryInvokePropertyGet(comPropertyInfo.GetFunction.DispId, out value)))
                        {
                            break;
                        }
                    }
                }
            }

            return value;
        }

        public int TryGetItemCount()
        {
            IDispatch dispatch = null;
            object count = 0;

            try
            {
                dispatch = TryGetUniqueRCW<IDispatch>();
                if (MarshalEx.Succeeded(dispatch.InvokePropertyGet("Count", out count)))
                {
                    return count is int ? (int)count : -1;
                }
            }
            catch
            {
            }
            finally
            {
                CleanupUniqueRCW(dispatch);
            }

            return -1;
        }

        public ComTypeInfo TryGetComTypeInfo()
        {
            ComTypeInfo comTypeInfo = null;
            IDispatch dispatch = null;

            try
            {
                dispatch = TryGetUniqueRCW<IDispatch>();

                if (dispatch != null)
                {
                    comTypeInfo = ComTypeManager.Instance.FromIDispatch(dispatch);
                }
            }
            catch
            {
            }
            finally
            {
                ComPtr.CleanupUniqueRCW(dispatch);
            }


            return comTypeInfo;
        }

        public System.Runtime.InteropServices.ComTypes.ITypeInfo TryGetTypeInfo()
        {
            System.Runtime.InteropServices.ComTypes.ITypeInfo typeInfo = null;

            IDispatch dispatch = null;

            try
            {
                dispatch = TryGetUniqueRCW<IDispatch>();

                if (dispatch != null)
                {
                    typeInfo = dispatch.GetTypeInfo();
                }
            }
            catch
            {
            }
            finally
            {
                ComPtr.CleanupUniqueRCW(dispatch);
            }

            return typeInfo;
        }

        public object TryGetRCW()
        {
            if (IsInvalid == false)
            {
                return Marshal.GetObjectForIUnknown(this.handle);
            }

            return null;
        }

        public object TryGetUniqueRCW()
        {
            if (IsInvalid == false)
            {
                return Marshal.GetUniqueObjectForIUnknown(this.handle);
            }

            return null;
        }

        public T TryGetUniqueRCW<T>()
        {
            object rcw = null;

            try
            {
                if (Is<T>())
                {
                    rcw = TryGetUniqueRCW();
                }
            }
            catch
            {
            }

            return (T)rcw;
        }

        public int TryInvokeMethod(int dispId, object[] args, out object returnValue)
        {
            int hr = NativeMethods.S_OK;
            IDispatch dispatch = null;
            returnValue = null;

            try
            {
                if (IsDispatch)
                {
                    dispatch = TryGetUniqueRCW<IDispatch>();

                    if (dispatch != null)
                    {
                        if (MarshalEx.Succeeded(hr = dispatch.InvokeMethod(dispId, args, out returnValue)))
                        {
                        }
                    }
                }
            }
            catch
            {
            }
            finally
            {
                ComPtr.CleanupUniqueRCW(dispatch);
            }

            return hr;
        }

        public int TryInvokeMethod(string name, object[] args, out object returnValue)
        {
            int hr = NativeMethods.S_OK;
            IDispatch dispatch = null;
            returnValue = null;

            try
            {
                if (IsDispatch)
                {
                    dispatch = TryGetUniqueRCW<IDispatch>();

                    if (dispatch != null)
                    {
                        if (MarshalEx.Succeeded(hr = dispatch.InvokeMethod(name, args, out returnValue)))
                        {
                        }
                    }
                }
            }
            catch
            {
            }
            finally
            {
                ComPtr.CleanupUniqueRCW(dispatch);
            }

            return hr;
        }

        public int TryInvokePropertyGet(int dispId, out object value)
        {
            int hr = NativeMethods.S_OK;
            IDispatch dispatch = null;
            value = null;

            try
            {
                if (IsDispatch)
                {
                    dispatch = TryGetUniqueRCW<IDispatch>();

                    if (dispatch != null)
                    {
                        if (MarshalEx.Succeeded(hr = dispatch.InvokePropertyGet(dispId, out value)))
                        {
                        }
                    }
                }
            }
            catch
            {
            }
            finally
            {
                ComPtr.CleanupUniqueRCW(dispatch);
            }

            return hr;
        }

        public int TryInvokePropertyGet(string propertyName, out object value)
        {
            int hr = NativeMethods.S_OK;
            IDispatch dispatch = null;
            value = null;

            try
            {
                if (IsDispatch)
                {
                    dispatch = TryGetUniqueRCW<IDispatch>();

                    if (dispatch != null)
                    {
                        if (MarshalEx.Succeeded(hr = dispatch.InvokePropertyGet(propertyName, out value)))
                        {
                        }
                    }
                }
            }
            catch
            {
            }
            finally
            {
                ComPtr.CleanupUniqueRCW(dispatch);
            }

            return hr;
        }

        public int TryInvokePropertySet(string propertyName, object value)
        {
            int hr = NativeMethods.S_OK;
            IDispatch dispatch = null;
            
            try
            {
                if (IsDispatch)
                {
                    dispatch = TryGetUniqueRCW<IDispatch>();

                    if (dispatch != null)
                    {
                        if (MarshalEx.Succeeded(hr = dispatch.InvokePropertySet(propertyName, value)))
                        {
                        }
                    }
                }
            }
            catch
            {
            }
            finally
            {
                ComPtr.CleanupUniqueRCW(dispatch);
            }

            return hr;
        }

        public bool TryIsCollection()
        {
            ComTypeInfo comTypeInfo = TryGetComTypeInfo();

            if (comTypeInfo != null)
            {
                return comTypeInfo.IsCollection;
            }

            return false;
        }

        public static void CleanupUniqueRCW(object rcw)
        {
            try
            {
                if (rcw != null)
                {
                    int count = Marshal.FinalReleaseComObject(rcw);
                }
            }
            catch
            {
            }
        }

        #endregion

        #region "TypeDescriptor Implementation"
        /// <summary>
        /// Get Class Name
        /// </summary>
        /// <returns>String</returns>
        public String GetClassName()
        {
            return TypeDescriptor.GetClassName(this, true);
        }

        /// <summary>
        /// GetAttributes
        /// </summary>
        /// <returns>AttributeCollection</returns>
        public AttributeCollection GetAttributes()
        {
            return TypeDescriptor.GetAttributes(this, true);
        }

        /// <summary>
        /// GetComponentName
        /// </summary>
        /// <returns>String</returns>
        public String GetComponentName()
        {
            return TypeDescriptor.GetComponentName(this, true);
        }

        /// <summary>
        /// GetConverter
        /// </summary>
        /// <returns>TypeConverter</returns>
        public TypeConverter GetConverter()
        {
            return TypeDescriptor.GetConverter(this, true);
        }

        /// <summary>
        /// GetDefaultEvent
        /// </summary>
        /// <returns>EventDescriptor</returns>
        public EventDescriptor GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(this, true);
        }

        /// <summary>
        /// GetDefaultProperty
        /// </summary>
        /// <returns>PropertyDescriptor</returns>
        public PropertyDescriptor GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(this, true);
        }

        /// <summary>
        /// GetEditor
        /// </summary>
        /// <param name="editorBaseType">editorBaseType</param>
        /// <returns>object</returns>
        public object GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(this, editorBaseType, true);
        }

        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(this, attributes, true);
        }

        public EventDescriptorCollection GetEvents()
        {
            return TypeDescriptor.GetEvents(this, true);
        }

        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            if (_propertyDescriptorCollection == null)
            {
                _propertyDescriptorCollection = new PropertyDescriptorCollection(new PropertyDescriptor[] { }, true);

                ComTypeInfo comTypeInfo = TryGetComTypeInfo();

                if (comTypeInfo != null)
                {
                    List<ComPtrPropertyDescriptor> list = new List<ComPtrPropertyDescriptor>();

                    foreach (ComPropertyInfo comPropertyInfo in comTypeInfo.GetProperties(true))
                    {
                        ComFunctionInfo getComFunctionInfo = comPropertyInfo.GetFunction;
                        bool bReadOnly = comPropertyInfo.SetFunction == null ? true : false;

                        if (getComFunctionInfo != null)
                        {
                            VarEnum variantType = getComFunctionInfo.ReturnParameter.VariantType;

                            switch (variantType)
                            {
                                case VarEnum.VT_PTR:
                                case VarEnum.VT_DISPATCH:
                                case VarEnum.VT_UNKNOWN:
                                    continue;
                                case VarEnum.VT_SAFEARRAY:
                                    continue;
                            }

                            // Special case. MailSession is a PITA property that causes modal dialog.
                            if (comPropertyInfo.Name.Equals("MailSession"))
                            {
                                ComPtrProperty comPtrProperty = new ComPtrProperty(comPropertyInfo.Name, comPropertyInfo.Description, 0, typeof(int), variantType, true);
                                list.Add(new ComPtrPropertyDescriptor(ref comPtrProperty, comPropertyInfo, attributes));
                                continue;
                            }

                            object value = null;
                            if (MarshalEx.Succeeded(TryInvokePropertyGet(getComFunctionInfo.DispId, out value)))
                            {
                                Type propertyType = typeof(object);

                                if (value != null)
                                {
                                    propertyType = value.GetType();
                                }
                                else
                                {
                                    bReadOnly = true;
                                }

                                ComPtrProperty comPtrProperty = new ComPtrProperty(comPropertyInfo.Name, comPropertyInfo.Description, value, propertyType, variantType, bReadOnly);
                                list.Add(new ComPtrPropertyDescriptor(ref comPtrProperty, comPropertyInfo, attributes));
                            }
                        }
                    }

#if DEBUG
                    ComPtrProperty refCountProperty = new ComPtrProperty("[RefCount]", "", this.RefCount, typeof(int), VarEnum.VT_I4, true);
                    list.Add(new ComPtrPropertyDescriptor(ref refCountProperty, null, attributes));
#endif
                    _propertyDescriptorCollection = new PropertyDescriptorCollection(list.ToArray());
                }
            }

            return _propertyDescriptorCollection;
        }

        public PropertyDescriptorCollection GetProperties()
        {
            return TypeDescriptor.GetProperties(this, true);
        }

        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            return this;
        }

        #endregion
    }

    public class ComPtrProperty
    {
        private string _name = string.Empty;
        private object _value = null;
        private string _description = String.Empty;
        private Type _type;
        VarEnum _variantType = default(VarEnum);
        private bool _readonly = false;

        public ComPtrProperty(string sName, string description, object value, Type type, VarEnum variantType, bool bReadOnly)
        {
            _name = sName;
            _description = description;
            _value = value;
            _type = type;
            _variantType = variantType;
            _readonly = bReadOnly;
        }

        public string Name { get { return _name; } }
        public string Description { get { return _description; } }
        public Type Type { get { return _type; } }
        public VarEnum VariantType { get { return _variantType; } }
        public bool ReadOnly { get { return _readonly; } }

        public object Value
        {
            get { return _value; }
            set { _value = value; }
        }
    }

    public class ComPtrPropertyDescriptor : PropertyDescriptor
    {
        ComPtrProperty _comPtrProperty;
        ComPropertyInfo _comPropertyInfo;

        public ComPtrPropertyDescriptor(ref ComPtrProperty comPtrProperty, ComPropertyInfo comPropertyInfo, Attribute[] attrs)
            : base(comPtrProperty.Name, attrs)
        {
            _comPtrProperty = comPtrProperty;
            _comPropertyInfo = comPropertyInfo;
        }

        #region PropertyDescriptor specific

        public override bool CanResetValue(object component) { return false; }

        public override Type ComponentType { get { return null; } }

        public override object GetValue(object component)
        {
#if DEBUG
            if (_comPropertyInfo == null)
            {
                if (_comPtrProperty.Name.Equals("[RefCount]"))
                {
                    return ((ComPtr)component).RefCount;
                }
            }
#endif

            try
            {
                if (_comPtrProperty.Name.Equals("MailSession")) return _comPtrProperty.Value;
            }
            catch
            {
            }

            ComPtr p = component as ComPtr;

            if (p != null)
            {
                object value = null;
                if (MarshalEx.Succeeded(p.TryInvokePropertyGet(_comPtrProperty.Name, out value)))
                {
                    return value;
                }
            }

            return _comPtrProperty.Value;
        }

        public override string Description { get { return _comPtrProperty.Description; } }

        public override string Category { get { return string.Empty; } }

        public override string DisplayName { get { return _comPtrProperty.Name; } }

        public override bool IsReadOnly { get { return _comPtrProperty.ReadOnly; } }

        public override void ResetValue(object component) { }

        public override bool ShouldSerializeValue(object component) { return false; }

        public override void SetValue(object component, object value)
        {
            ComPtr p = component as ComPtr;

            if (p != null)
            {
                if (MarshalEx.Succeeded(p.TryInvokePropertySet(_comPtrProperty.Name, value)))
                {
                    _comPtrProperty.Value = value;
                }
            }
        }

        public override Type PropertyType { get { return _comPtrProperty.Type; } }

        #endregion

        public ComPropertyInfo ComPropertyInfo { get { return _comPropertyInfo; } }
        public VarEnum VariantType { get { return _comPtrProperty.VariantType; } }

        public override string ToString()
        {
            return String.Format("{0} [{1}]", _comPtrProperty.Name, _comPtrProperty.VariantType);
        }
    }
}
