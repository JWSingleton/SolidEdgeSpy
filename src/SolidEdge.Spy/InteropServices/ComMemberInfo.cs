using SolidEdge.Spy.Extensions;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Linq;
using System.Text;

namespace SolidEdge.Spy.InteropServices
{
    public abstract class ComMemberInfo
    {
        internal ComTypeInfo _comTypeInfo;
        internal string _name = String.Empty;
        internal string _description = String.Empty;
        internal int _helpContext = 0;
        internal string _helpFile = String.Empty;

        public ComMemberInfo(ComTypeInfo comTypeInfo)
        {
            _comTypeInfo = comTypeInfo;
        }

        public string Name { get { return _name; } }
        public string Description { get { return _description; } }
        public ComTypeInfo ComTypeInfo { get { return _comTypeInfo; } }

        public override string ToString()
        {
            return _name;
        }
    }

    public class ComFunctionInfo : ComMemberInfo
    {
        private IntPtr _pFuncDesc = IntPtr.Zero;
        private System.Runtime.InteropServices.ComTypes.FUNCDESC _funcDesc;
        private List<ComParameterInfo> _parameters = new List<ComParameterInfo>();
        private ComParameterInfo _returnParameter;

        public ComFunctionInfo(ComTypeInfo parent, IntPtr pFuncDesc)
            : base(parent)
        {
            _pFuncDesc = pFuncDesc;
            _funcDesc = pFuncDesc.ToStructure < System.Runtime.InteropServices.ComTypes.FUNCDESC>();
            _comTypeInfo.GetITypeInfo().GetDocumentation(_funcDesc.memid, out _name, out _description, out _helpContext, out _helpFile);

            if (_description == null) _description = String.Empty;
            if (_helpFile == null) _helpFile = String.Empty;

            LoadParameters();
        }

        public ComParameterInfo ReturnParameter { get { return _returnParameter; } }

        public ComParameterInfo[] Parameters
        {
            get
            {
                if (_parameters == null)
                {
                    LoadParameters();
                }

                return _parameters.ToArray();
            }
        }

        public System.Runtime.InteropServices.ComTypes.FUNCFLAGS FunctionFlags { get { return (System.Runtime.InteropServices.ComTypes.FUNCFLAGS)_funcDesc.wFuncFlags; } }

        public int DispId { get { return _funcDesc.memid; } }
        public System.Runtime.InteropServices.ComTypes.INVOKEKIND InvokeKind { get { return _funcDesc.invkind; } }

        public bool IsBindable { get { return FunctionFlags.IsSet(System.Runtime.InteropServices.ComTypes.FUNCFLAGS.FUNCFLAG_FBINDABLE); } }
        public bool IsDefaultBind { get { return FunctionFlags.IsSet(System.Runtime.InteropServices.ComTypes.FUNCFLAGS.FUNCFLAG_FDEFAULTBIND); } }
        public bool IsDefaultCollectionElemement { get { return FunctionFlags.IsSet(System.Runtime.InteropServices.ComTypes.FUNCFLAGS.FUNCFLAG_FDEFAULTCOLLELEM); } }
        public bool IsDisplayBindable { get { return FunctionFlags.IsSet(System.Runtime.InteropServices.ComTypes.FUNCFLAGS.FUNCFLAG_FDISPLAYBIND); } }
        public bool IsHidden { get { return FunctionFlags.IsSet(System.Runtime.InteropServices.ComTypes.FUNCFLAGS.FUNCFLAG_FHIDDEN); } }
        public bool IsImmediateBindable { get { return FunctionFlags.IsSet(System.Runtime.InteropServices.ComTypes.FUNCFLAGS.FUNCFLAG_FIMMEDIATEBIND); } }
        public bool IsNonBrowsable { get { return FunctionFlags.IsSet(System.Runtime.InteropServices.ComTypes.FUNCFLAGS.FUNCFLAG_FNONBROWSABLE); } }
        public bool IsReplaceable { get { return FunctionFlags.IsSet(System.Runtime.InteropServices.ComTypes.FUNCFLAGS.FUNCFLAG_FREPLACEABLE); } }
        public bool IsRequestEdit { get { return FunctionFlags.IsSet(System.Runtime.InteropServices.ComTypes.FUNCFLAGS.FUNCFLAG_FREQUESTEDIT); } }
        public bool IsRestricted { get { return FunctionFlags.IsSet(System.Runtime.InteropServices.ComTypes.FUNCFLAGS.FUNCFLAG_FRESTRICTED); } }
        public bool IsSource { get { return FunctionFlags.IsSet(System.Runtime.InteropServices.ComTypes.FUNCFLAGS.FUNCFLAG_FSOURCE); } }
        public bool IsUiDefault { get { return FunctionFlags.IsSet(System.Runtime.InteropServices.ComTypes.FUNCFLAGS.FUNCFLAG_FUIDEFAULT); } }
        public bool SupportsGetLastError { get { return FunctionFlags.IsSet(System.Runtime.InteropServices.ComTypes.FUNCFLAGS.FUNCFLAG_FUSESGETLASTERROR); } }

        private void LoadParameters()
        {
            _parameters = new List<ComParameterInfo>();

            string[] rgBstrNames = new string[_funcDesc.cParams + 1];
            int pcNames = 0;
            _comTypeInfo.GetITypeInfo().GetNames(_funcDesc.memid, rgBstrNames, rgBstrNames.Length, out pcNames);

            IntPtr pElemDesc = _funcDesc.lprgelemdescParam;

            _returnParameter = new ComParameterInfo(this, rgBstrNames[0], _funcDesc.elemdescFunc);

            if (_funcDesc.cParams > 0)
            {
                for (int cParams = 0; cParams < _funcDesc.cParams; cParams++)
                {
                    System.Runtime.InteropServices.ComTypes.ELEMDESC elemDesc = (System.Runtime.InteropServices.ComTypes.ELEMDESC)Marshal.PtrToStructure(pElemDesc, typeof(System.Runtime.InteropServices.ComTypes.ELEMDESC));
                    _parameters.Add(new ComParameterInfo(this, rgBstrNames[cParams + 1], elemDesc));
                    pElemDesc = new IntPtr(pElemDesc.ToInt64() + Marshal.SizeOf(typeof(System.Runtime.InteropServices.ComTypes.ELEMDESC)));
                }
            }
            else
            {
                //list.Add(new ElemDesc(this, m_funcDesc.elemdescFunc, rgBstrNames[0], -1));
            }

            //m_parameters = list.ToArray();
        }

        public System.Runtime.InteropServices.ComTypes.FUNCDESC FuncDesc { get { return this._funcDesc; } }

        public override string ToString()
        {
            return Name;
        }

        public string ToString(bool includeParameters)
        {
            if (!includeParameters) return ToString();

            StringBuilder sb = new StringBuilder();

            sb.Append(_name);

            if (_parameters.Count > 0)
            {
                sb.Append('(');

                foreach (ComParameterInfo parameter in _parameters)
                {
                    sb.Append(parameter.Name);
                    sb.Append(", ");
                }

                sb.Remove(sb.Length - 2, 2);

                sb.Append(')');
            }
            else
            {
                sb.Append("()");
            }

            return sb.ToString();
        }
    }

    public class ComPropertyInfo : ComMemberInfo
    {
        private List<ComFunctionInfo> _functions = new List<ComFunctionInfo>();

        public ComPropertyInfo(ComTypeInfo parent, ComFunctionInfo[] functions)
            : base(parent)
        {
            _functions.AddRange(functions);

            _name = functions[0].Name;
            _description = functions[0].Description;
        }

        public ComFunctionInfo GetFunction
        {
            get { return _functions.Where(x => x.InvokeKind == System.Runtime.InteropServices.ComTypes.INVOKEKIND.INVOKE_PROPERTYGET).FirstOrDefault(); }
        }

        public ComFunctionInfo SetFunction
        {
            get
            {
                return _functions.Where(
                    x => x.InvokeKind != System.Runtime.InteropServices.ComTypes.INVOKEKIND.INVOKE_FUNC).Where(
                    x => x.InvokeKind != System.Runtime.InteropServices.ComTypes.INVOKEKIND.INVOKE_PROPERTYGET)
                    .FirstOrDefault();
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return ((GetFunction != null) && (SetFunction == null));
            }
        }

        public bool IsWriteOnly
        {
            get
            {
                return ((SetFunction != null) && (GetFunction == null));
            }
        }

        public bool IsReadWrite
        {
            get
            {
                return ((SetFunction != null) && (GetFunction != null));
            }
        }

        public bool GetFunctionHasParameters
        {
            get
            {
                ComFunctionInfo getComFunctionInfo = GetFunction;
                
                if (getComFunctionInfo != null)
                {
                    return getComFunctionInfo.Parameters.Length > 0;
                }

                return false;
            }
        }
    }

    public class ComVariableInfo : ComMemberInfo
    {
        private System.Runtime.InteropServices.ComTypes.VARDESC _varDesc;
        private object _constantValue;

        public ComVariableInfo(ComTypeInfo parent, System.Runtime.InteropServices.ComTypes.VARDESC varDesc, object constantValue)
            : base(parent)
        {
            _varDesc = varDesc;
            _comTypeInfo.GetITypeInfo().GetDocumentation(_varDesc.memid, out _name, out _description, out _helpContext, out _helpFile);
            _constantValue = constantValue;
            if (_description == null) _description = String.Empty;
            if (_helpFile == null) _helpFile = String.Empty;
        }

        public object ConstantValue { get { return _constantValue; } }
        public System.Runtime.InteropServices.ComTypes.VARDESC VariableDescription { get { return _varDesc; } }
        public System.Runtime.InteropServices.ComTypes.VARKIND VariableKind { get { return _varDesc.varkind; } }
        public System.Runtime.InteropServices.ComTypes.VARFLAGS VariableFlags { get { return (System.Runtime.InteropServices.ComTypes.VARFLAGS)_varDesc.wVarFlags; } }

        public bool IsBindable { get { return VariableFlags.IsSet(System.Runtime.InteropServices.ComTypes.VARFLAGS.VARFLAG_FBINDABLE); } }
        public bool IsDefaultBind { get { return VariableFlags.IsSet(System.Runtime.InteropServices.ComTypes.VARFLAGS.VARFLAG_FDEFAULTBIND); } }
        public bool IsDefaultCollectionElemement { get { return VariableFlags.IsSet(System.Runtime.InteropServices.ComTypes.VARFLAGS.VARFLAG_FDEFAULTCOLLELEM); } }
        public bool IsDisplayBindable { get { return VariableFlags.IsSet(System.Runtime.InteropServices.ComTypes.VARFLAGS.VARFLAG_FDISPLAYBIND); } }
        public bool IsHidden { get { return VariableFlags.IsSet(System.Runtime.InteropServices.ComTypes.VARFLAGS.VARFLAG_FHIDDEN); } }
        public bool IsImmediateBindable { get { return VariableFlags.IsSet(System.Runtime.InteropServices.ComTypes.VARFLAGS.VARFLAG_FIMMEDIATEBIND); } }
        public bool IsNonBrowsable { get { return VariableFlags.IsSet(System.Runtime.InteropServices.ComTypes.VARFLAGS.VARFLAG_FNONBROWSABLE); } }
        public bool IsReadOnly { get { return VariableFlags.IsSet(System.Runtime.InteropServices.ComTypes.VARFLAGS.VARFLAG_FREADONLY); } }
        public bool IsReplaceable { get { return VariableFlags.IsSet(System.Runtime.InteropServices.ComTypes.VARFLAGS.VARFLAG_FREPLACEABLE); } }
        public bool IsRequestEdit { get { return VariableFlags.IsSet(System.Runtime.InteropServices.ComTypes.VARFLAGS.VARFLAG_FREQUESTEDIT); } }
        public bool IsRestricted { get { return VariableFlags.IsSet(System.Runtime.InteropServices.ComTypes.VARFLAGS.VARFLAG_FRESTRICTED); } }
        public bool IsSource { get { return VariableFlags.IsSet(System.Runtime.InteropServices.ComTypes.VARFLAGS.VARFLAG_FSOURCE); } }
        public bool IsUiDefault { get { return VariableFlags.IsSet(System.Runtime.InteropServices.ComTypes.VARFLAGS.VARFLAG_FUIDEFAULT); } }
    }

    public class ComParameterInfo
    {
        private ComFunctionInfo _comFunctionInfo;
        private string _name;
        private System.Runtime.InteropServices.ComTypes.ELEMDESC _elemDesc;

        public ComParameterInfo(ComFunctionInfo comFunctionInfo, string name, System.Runtime.InteropServices.ComTypes.ELEMDESC elemDesc)
        {
            _comFunctionInfo = comFunctionInfo;
            _name = name;
            _elemDesc = elemDesc;
        }

        public ComFunctionInfo ComFunctionInfo { get { return _comFunctionInfo; } }
        public string Name { get { return _name; } }
        public System.Runtime.InteropServices.ComTypes.ELEMDESC ELEMDESC { get { return _elemDesc; } }
        public System.Runtime.InteropServices.VarEnum VariantType { get { return (System.Runtime.InteropServices.VarEnum)_elemDesc.tdesc.vt; } }

        public bool IsIn { get { return _elemDesc.desc.paramdesc.wParamFlags.IsSet(System.Runtime.InteropServices.ComTypes.PARAMFLAG.PARAMFLAG_FIN); } }
        public bool IsOut { get { return _elemDesc.desc.paramdesc.wParamFlags.IsSet(System.Runtime.InteropServices.ComTypes.PARAMFLAG.PARAMFLAG_FOUT); } }
        public bool IsLcid { get { return _elemDesc.desc.paramdesc.wParamFlags.IsSet(System.Runtime.InteropServices.ComTypes.PARAMFLAG.PARAMFLAG_FLCID); } }
        public bool IsRetval { get { return _elemDesc.desc.paramdesc.wParamFlags.IsSet(System.Runtime.InteropServices.ComTypes.PARAMFLAG.PARAMFLAG_FRETVAL); } }
        public bool IsOptional { get { return _elemDesc.desc.paramdesc.wParamFlags.IsSet(System.Runtime.InteropServices.ComTypes.PARAMFLAG.PARAMFLAG_FOPT); } }
        public bool HasDefault { get { return _elemDesc.desc.paramdesc.wParamFlags.IsSet(System.Runtime.InteropServices.ComTypes.PARAMFLAG.PARAMFLAG_FHASDEFAULT); } }
        public bool HasCustomData { get { return _elemDesc.desc.paramdesc.wParamFlags.IsSet(System.Runtime.InteropServices.ComTypes.PARAMFLAG.PARAMFLAG_FHASCUSTDATA); } }

        public override string ToString()
        {
            return _name;
        }
    }
}
