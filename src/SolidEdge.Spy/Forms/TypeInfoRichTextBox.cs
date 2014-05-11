using SolidEdge.Spy.Extensions;
using SolidEdge.Spy.InteropServices;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Windows.Forms;

namespace SolidEdge.Spy.Forms
{
    public class TypeInfoRichTextBox : RichTextBoxEx
    {
        private static Color _colorBlue = Color.FromArgb(0, 102, 204);

        public void DescribeComTypeLibrary(ComTypeLibrary comTypeLibrary)
        {
            Clear();

            AppendText("Library ");
            AppendText(comTypeLibrary.Name, ForeColor, FontStyle.Bold);
            AppendText(Environment.NewLine);
            AppendText(String.Format("    {0}", comTypeLibrary.Filename));
            AppendText(Environment.NewLine);

            WriteSummary(comTypeLibrary.Description);
            WriteGuid(comTypeLibrary.Guid);
        }

        public void DescribeComTypeInfo(ComTypeInfo comTypeInfo)
        {
            Clear();

            if (comTypeInfo is ComAliasInfo)
            {
                AppendText("Alias ");
            }
            else if (comTypeInfo is ComCoClassInfo)
            {
                AppendText("CoClass ");
            }
            else if (comTypeInfo is ComEnumInfo)
            {
                AppendText("Enum ");
            }
            else if (comTypeInfo is ComDispatchInfo)
            {
                AppendText("Dispatch interface ");
            }
            else if (comTypeInfo is ComInterfaceInfo)
            {
                AppendText("Interface ");
            }
            //else if (comTypeInfo is ComMaxInfo)
            //{
            //}
            else if (comTypeInfo is ComModuleInfo)
            {
                AppendText("Module ");
            }
            else if (comTypeInfo is ComRecordInfo)
            {
                AppendText("Struct ");
            }
            else if (comTypeInfo is ComUnionInfo)
            {
                AppendText("Union ");
            }

            //Interface Application
            //    Member of SolidEdgeFramework
            //    Provides the properties and methods necessary for a Visual Basic user to drive Solid Edge programmatically.

            AppendText(comTypeInfo.Name, ForeColor, FontStyle.Bold);
            AppendText(Environment.NewLine);
            AppendText("    Member of ");
            InsertLink(comTypeInfo.ComTypeLibrary.Name);
            AppendText(Environment.NewLine);

            WriteSummary(comTypeInfo.Description);
            WriteGuid(comTypeInfo.Guid);
        }

        public void DescribeComFunctionInfo(ComFunctionInfo comFunctionInfo)
        {
            try
            {
                Clear();

                if (comFunctionInfo == null) return;

                WriteReturnParameter(comFunctionInfo.ReturnParameter);

                AppendText(String.Format(" {0}", comFunctionInfo.Name), ForeColor, FontStyle.Bold);

                if (comFunctionInfo.Parameters.Length > 0)
                {
                    AppendText("(");

                    for (int i = 0; i < comFunctionInfo.Parameters.Length; i++)
                    {
                        ComParameterInfo parameter = comFunctionInfo.Parameters[i];

                        WriteParameter(parameter);

                        if (i < comFunctionInfo.Parameters.Length - 1)
                        {
                            AppendText(", ");
                        }
                    }

                    AppendText(")");
                }
                else
                {
                    AppendText("()");
                }

                AppendText(Environment.NewLine);
                AppendText("   Member of ");
                InsertLink(comFunctionInfo.ComTypeInfo.FullName);
                AppendText(Environment.NewLine);

                WriteSummary(comFunctionInfo.Description);
                WriteDispatchid(comFunctionInfo.DispId);
            }
            catch
            {
            }
        }

        public void DescribeComPropertyInfo(ComPropertyInfo comPropertyInfo)
        {
            try
            {
                Clear();

                if (comPropertyInfo == null) return;

                ComFunctionInfo comFunctionInfo = comPropertyInfo.GetFunction;

                WriteReturnParameter(comFunctionInfo.ReturnParameter);

                AppendText(String.Format(" {0}", comFunctionInfo.Name), ForeColor, FontStyle.Bold);

                if (comFunctionInfo.Parameters.Length > 0)
                {
                    AppendText("(");

                    for (int i = 0; i < comFunctionInfo.Parameters.Length; i++)
                    {
                        ComParameterInfo parameter = comFunctionInfo.Parameters[i];

                        WriteParameter(parameter);

                        if (i < comFunctionInfo.Parameters.Length - 1)
                        {
                            AppendText(", ");
                        }
                    }

                    AppendText(")");
                }

                //string Caption { set; get; }
                AppendText(" { ");
                
                if (comPropertyInfo.GetFunction != null)
                {
                    AppendText("get; ");
                }

                if (comPropertyInfo.SetFunction != null)
                {
                    AppendText("set; ");
                }

                AppendText("}");
                AppendText(Environment.NewLine);

                AppendText("   Member of ");
                InsertLink(comFunctionInfo.ComTypeInfo.FullName);
                AppendText(Environment.NewLine);

                WriteSummary(comFunctionInfo.Description);
                WriteDispatchid(comFunctionInfo.DispId);
            }
            catch
            {
            }
        }

        public void DescribeComVariableInfo(ComVariableInfo comVariableInfo)
        {
            try
            {
                Clear();

                if (comVariableInfo == null) return;

                AppendText("Constant ");
                AppendText(comVariableInfo.Name, ForeColor, FontStyle.Bold);
                AppendText(String.Format(" = {0}", comVariableInfo.ConstantValue));
                AppendText(Environment.NewLine);
                AppendText("   Member of ");
                InsertLink(comVariableInfo.ComTypeInfo.FullName);
                AppendText(Environment.NewLine);

                WriteSummary(comVariableInfo.Description);
            }
            catch
            {
            }
        }

        public void AppendText(string text, Color color)
        {
            SelectionStart = TextLength;
            SelectionLength = 0;

            SelectionColor = color;
            AppendText(text);
            SelectionColor = ForeColor;
        }

        public void AppendText(string text, Color color, FontStyle fontStyle)
        {
            SelectionStart = TextLength;
            SelectionLength = 0;

            SelectionColor = color;
            SelectionFont = new Font(Font, fontStyle);
            AppendText(text);
            SelectionColor = ForeColor;
            SelectionFont = Font;
        }

        private void WriteParameter(ComParameterInfo parameter)
        {
            if (parameter.IsOptional)
            {
                AppendText("[");
            }

            if (parameter.IsOut)
            {
                if (parameter.IsIn)
                {
                    AppendText("ref ");
                }
                else
                {
                    AppendText("out ");
                }
            }
            else if (parameter.VariantType == System.Runtime.InteropServices.VarEnum.VT_PTR)
            {
                AppendText("ref ");
            }

            char[] tokens = { '[', ']' };
            ITypeInfo typeInfo = parameter.ComFunctionInfo.ComTypeInfo.GetITypeInfo();
            string parameterTypeName = ComHelper.TypeDescToString(parameter.ELEMDESC.tdesc, typeInfo);
            string parameterTypeNameLink = parameterTypeName.Trim(tokens);

            if (ComTypeManager.Instance.HasComType(parameterTypeNameLink))
            {
                InsertLink(parameterTypeName, parameterTypeNameLink);
            }
            else
            {
                AppendText(parameterTypeName, _colorBlue, FontStyle.Bold);
            }

            AppendText(" ");
            AppendText(parameter.Name, ForeColor, FontStyle.Italic);

            if (parameter.IsOptional)
            {
                AppendText("]");
            }
        }

        private void WriteReturnParameter(ComParameterInfo parameter)
        {
            char[] tokens = { '[', ']' };
            ITypeInfo typeInfo = parameter.ComFunctionInfo.ComTypeInfo.GetITypeInfo();
            string returnParameter = ComHelper.TypeDescToString(parameter.ELEMDESC.tdesc, typeInfo);
            string returnParameterLink = returnParameter.Trim(tokens);

            switch (parameter.VariantType)
            {
                case System.Runtime.InteropServices.VarEnum.VT_VOID:
                    AppendText(returnParameter, ForeColor, FontStyle.Bold);
                    break;
                default:
                    if (ComTypeManager.Instance.HasComType(returnParameterLink))
                    {
                        InsertLink(returnParameter, returnParameterLink);
                    }
                    else
                    {
                        AppendText(returnParameter, _colorBlue, FontStyle.Bold);
                    }
                    break;
            }
        }

        private void WriteSummary(string summary)
        {
            if (String.IsNullOrWhiteSpace(summary) == false)
            {
                AppendText(Environment.NewLine);
                AppendText("Summary:", ForeColor, FontStyle.Bold);
                AppendText(Environment.NewLine);
                AppendText(String.Format("    {0}", summary));
                AppendText(Environment.NewLine);
            }
        }

        private void WriteGuid(Guid guid)
        {
            if (guid.Equals(Guid.Empty) == false)
            {
                AppendText(Environment.NewLine);
                AppendText("GUID:", ForeColor, FontStyle.Bold);
                AppendText(Environment.NewLine);
                AppendText(String.Format("    {0}", guid.ToString("B").ToUpper()));
                AppendText(Environment.NewLine);
            }
        }

        private void WriteDispatchid(int id)
        {
            AppendText(Environment.NewLine);
            AppendText("ID:", ForeColor, FontStyle.Bold);
            AppendText(Environment.NewLine);
            AppendText(String.Format("    {0} (0x{1})", id, id.ToString("X8")));
            AppendText(Environment.NewLine);
        }
    }
}
