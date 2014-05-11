using SolidEdge.Spy.Extensions;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Linq;
using System.Text;

namespace SolidEdge.Spy.InteropServices
{
    public class ComTypeInfo
    {
        protected ComTypeLibrary _comTypeLibrary;
        protected ITypeInfo _typeInfo;
        protected IntPtr _pTypeAttr;
        protected System.Runtime.InteropServices.ComTypes.TYPEATTR _typeAttr;
        protected string _name = String.Empty;
        protected string _description = String.Empty;
        protected int _helpContext = 0;
        protected string _helpFile = String.Empty;
        protected List<ComMemberInfo> _members = null;
        protected List<ComImplementedTypeInfo> _implementedTypes = null;

        public ComTypeInfo(ComTypeLibrary comTypeLibrary, ITypeInfo typeInfo, IntPtr pTypeAttr)
        {
            _comTypeLibrary = comTypeLibrary;
            _typeInfo = typeInfo;
            _pTypeAttr = pTypeAttr;
            _typeAttr = _pTypeAttr.ToStructure<System.Runtime.InteropServices.ComTypes.TYPEATTR>();
            _typeInfo.GetDocumentation(-1, out _name, out _description, out _helpContext, out _helpFile);
        }

        public ComImplementedTypeInfo[] ImplementedTypes
        {
            get
            {
                if (_implementedTypes == null)
                {
                    _implementedTypes = new List<ComImplementedTypeInfo>();

                    for (int i = 0; i< _typeAttr.cImplTypes; i++)
                    {
                        System.Runtime.InteropServices.ComTypes.IMPLTYPEFLAGS flags = default(System.Runtime.InteropServices.ComTypes.IMPLTYPEFLAGS);
                        _typeInfo.GetImplTypeFlags(i, out flags);

                        ITypeInfo refTypeInfo = null;
                        int href = 0;
                        _typeInfo.GetRefTypeOfImplType(i, out href);
                        _typeInfo.GetRefTypeInfo(href, out refTypeInfo);
                        
                        ComTypeInfo comTypeInfo = ComTypeManager.Instance.FromITypeInfo(refTypeInfo);

                        _implementedTypes.Add(new ComImplementedTypeInfo(comTypeInfo, flags));
                    }
                }

                return _implementedTypes.ToArray();
            }
        }

        public ComMemberInfo[] Members
        {
            get
            {
                if (_members == null) LoadMembers();

                return _members.ToArray();
            }
        }

        public ComFunctionInfo[] Methods
        {
            get
            {
                return Members.OfType<ComFunctionInfo>().Where(function => function.InvokeKind == System.Runtime.InteropServices.ComTypes.INVOKEKIND.INVOKE_FUNC).ToArray();
            }
        }

        public ComPropertyInfo[] Properties
        {
            get
            {

                List<ComPropertyInfo> list = new List<ComPropertyInfo>();
                Dictionary<string, List<ComFunctionInfo>> dictionary = new Dictionary<string, List<ComFunctionInfo>>();

                ComFunctionInfo[] functions = Members.OfType<ComFunctionInfo>().Where(
                    x => x.InvokeKind != System.Runtime.InteropServices.ComTypes.INVOKEKIND.INVOKE_FUNC).ToArray();

                foreach (ComFunctionInfo function in functions)
                {
                    if (!dictionary.ContainsKey(function.Name))
                    {
                        dictionary.Add(function.Name, new List<ComFunctionInfo>());
                    }

                    dictionary[function.Name].Add(function);
                }

                var enumerator = dictionary.GetEnumerator();

                while (enumerator.MoveNext())
                {
                    list.Add(new ComPropertyInfo(this, enumerator.Current.Value.ToArray()));
                }

                return list.ToArray();
            }
        }

        public ComVariableInfo[] Variables
        {
            get
            {
                return Members.OfType<ComVariableInfo>().ToArray();
            }
        }

        public ComFunctionInfo[] GetMethods(bool includeInherited)
        {
            List<ComFunctionInfo> list = new List<ComFunctionInfo>();

            list.AddRange(Methods);

            if (includeInherited)
            {
                list.AddRange(GetInheritedMethods(this));
            }

            list.Sort(delegate(ComFunctionInfo a, ComFunctionInfo b)
            {
                return a.Name.CompareTo(b.Name);
            });

            return list.GroupBy(x => x.Name).Select(s => s.First()).ToArray();
        }

        public ComPropertyInfo[] GetProperties(bool includeInherited)
        {
            List<ComPropertyInfo> list = new List<ComPropertyInfo>();

            list.AddRange(Properties);

            if (includeInherited)
            {
                list.AddRange(GetInheriteProperties(this));
            }

            list.Sort(delegate(ComPropertyInfo a, ComPropertyInfo b)
            {
                return a.Name.CompareTo(b.Name);
            });

            return list.GroupBy(x => x.Name).Select(s => s.First()).ToArray();
        }

        private ComFunctionInfo[] GetInheritedMethods(ComTypeInfo comTypeInfo)
        {
            List<ComFunctionInfo> list = new List<ComFunctionInfo>();

            for (int i = 0; i < comTypeInfo.ImplementedTypes.Length; i++)
            {
                ComImplementedTypeInfo comImplementedTypeInfo = comTypeInfo.ImplementedTypes[i];

                if (comImplementedTypeInfo.IsSource == false)
                {
                    foreach (ComFunctionInfo comFunctionInfo in comImplementedTypeInfo.ComTypeInfo.Methods)
                    {
                        if (list.FirstOrDefault(x => x.Name.Equals(comFunctionInfo.Name)) == null)
                        {
                            list.Add(comFunctionInfo);
                        }
                    }

                    list.AddRange(GetInheritedMethods(comImplementedTypeInfo.ComTypeInfo));
                }
            }

            list.Sort(delegate(ComFunctionInfo a, ComFunctionInfo b)
            {
                return a.Name.CompareTo(b.Name);
            });

            return list.ToArray();
        }

        private ComPropertyInfo[] GetInheriteProperties(ComTypeInfo comTypeInfo)
        {
            List<ComPropertyInfo> list = new List<ComPropertyInfo>();

            for (int i = 0; i < comTypeInfo.ImplementedTypes.Length; i++)
            {
                ComImplementedTypeInfo comImplementedTypeInfo = comTypeInfo.ImplementedTypes[i];

                if (comImplementedTypeInfo.IsSource == false)
                {
                    foreach (ComPropertyInfo comPropertyInfo in comImplementedTypeInfo.ComTypeInfo.Properties)
                    {
                        if (list.FirstOrDefault(x => x.Name.Equals(comPropertyInfo.Name)) == null)
                        {
                            list.Add(comPropertyInfo);
                        }
                    }

                    list.AddRange(GetInheriteProperties(comImplementedTypeInfo.ComTypeInfo));
                }
            }

            list.Sort(delegate(ComPropertyInfo a, ComPropertyInfo b)
            {
                return a.Name.CompareTo(b.Name);
            });

            return list.ToArray();
        }

        public bool IsAlias { get { return _typeAttr.typekind == System.Runtime.InteropServices.ComTypes.TYPEKIND.TKIND_ALIAS; } }
        public bool IsCoClass { get { return _typeAttr.typekind == System.Runtime.InteropServices.ComTypes.TYPEKIND.TKIND_COCLASS; } }
        public bool IsDispatch { get { return _typeAttr.typekind == System.Runtime.InteropServices.ComTypes.TYPEKIND.TKIND_DISPATCH; } }
        public bool IsEnum { get { return _typeAttr.typekind == System.Runtime.InteropServices.ComTypes.TYPEKIND.TKIND_ENUM; } }
        public bool IsInterface { get { return _typeAttr.typekind == System.Runtime.InteropServices.ComTypes.TYPEKIND.TKIND_INTERFACE; } }
        public bool IsMax { get { return _typeAttr.typekind == System.Runtime.InteropServices.ComTypes.TYPEKIND.TKIND_MAX; } }
        public bool IsModule { get { return _typeAttr.typekind == System.Runtime.InteropServices.ComTypes.TYPEKIND.TKIND_MODULE; } }
        public bool IsRecord { get { return _typeAttr.typekind == System.Runtime.InteropServices.ComTypes.TYPEKIND.TKIND_RECORD; } }
        public bool IsUnion { get { return _typeAttr.typekind == System.Runtime.InteropServices.ComTypes.TYPEKIND.TKIND_UNION; } }
        public bool IsAppObject { get { return _typeAttr.wTypeFlags.IsSet(System.Runtime.InteropServices.ComTypes.TYPEFLAGS.TYPEFLAG_FAPPOBJECT); } }
        public bool IsCanCreate { get { return _typeAttr.wTypeFlags.IsSet(System.Runtime.InteropServices.ComTypes.TYPEFLAGS.TYPEFLAG_FCANCREATE); } }
        public bool IsLicensed { get { return _typeAttr.wTypeFlags.IsSet(System.Runtime.InteropServices.ComTypes.TYPEFLAGS.TYPEFLAG_FLICENSED); } }
        public bool IsPredeclid { get { return _typeAttr.wTypeFlags.IsSet(System.Runtime.InteropServices.ComTypes.TYPEFLAGS.TYPEFLAG_FPREDECLID); } }
        public bool IsHidden { get { return _typeAttr.wTypeFlags.IsSet(System.Runtime.InteropServices.ComTypes.TYPEFLAGS.TYPEFLAG_FHIDDEN); } }
        public bool IsControl { get { return _typeAttr.wTypeFlags.IsSet(System.Runtime.InteropServices.ComTypes.TYPEFLAGS.TYPEFLAG_FCONTROL); } }
        public bool IsDual { get { return _typeAttr.wTypeFlags.IsSet(System.Runtime.InteropServices.ComTypes.TYPEFLAGS.TYPEFLAG_FDUAL); } }
        public bool IsNonExtensible { get { return _typeAttr.wTypeFlags.IsSet(System.Runtime.InteropServices.ComTypes.TYPEFLAGS.TYPEFLAG_FNONEXTENSIBLE); } }
        public bool IsOleAutomation { get { return _typeAttr.wTypeFlags.IsSet(System.Runtime.InteropServices.ComTypes.TYPEFLAGS.TYPEFLAG_FOLEAUTOMATION); } }
        public bool IsRestricted { get { return _typeAttr.wTypeFlags.IsSet(System.Runtime.InteropServices.ComTypes.TYPEFLAGS.TYPEFLAG_FRESTRICTED); } }
        public bool IsAggregatable { get { return _typeAttr.wTypeFlags.IsSet(System.Runtime.InteropServices.ComTypes.TYPEFLAGS.TYPEFLAG_FAGGREGATABLE); } }
        public bool IsReplaceable { get { return _typeAttr.wTypeFlags.IsSet(System.Runtime.InteropServices.ComTypes.TYPEFLAGS.TYPEFLAG_FREPLACEABLE); } }
        public bool IsDispatchable { get { return _typeAttr.wTypeFlags.IsSet(System.Runtime.InteropServices.ComTypes.TYPEFLAGS.TYPEFLAG_FDISPATCHABLE); } }
        public bool IsReverseBind { get { return _typeAttr.wTypeFlags.IsSet(System.Runtime.InteropServices.ComTypes.TYPEFLAGS.TYPEFLAG_FREVERSEBIND); } }
        public bool IsProxy { get { return _typeAttr.wTypeFlags.IsSet(System.Runtime.InteropServices.ComTypes.TYPEFLAGS.TYPEFLAG_FPROXY); } }

        private void LoadMembers()
        {
            _members = new List<ComMemberInfo>();

            for (short i = 0; i < _typeAttr.cFuncs; i++)
            {
                IntPtr pFuncDesc = IntPtr.Zero;

                _typeInfo.GetFuncDesc(i, out pFuncDesc);
                ComFunctionInfo comFunctionInfo = new ComFunctionInfo(this, pFuncDesc);

                _members.Add(comFunctionInfo);
            }

            /* Note that these are not always enum constants.  Some properties show up as VARDESC's. */
            for (short i = 0; i < _typeAttr.cVars; i++)
            {
                System.Runtime.InteropServices.ComTypes.VARDESC varDesc;
                IntPtr p = IntPtr.Zero;

                _typeInfo.GetVarDesc(i, out p);
                object constantValue = null;

                try
                {
                    varDesc = p.ToStructure<System.Runtime.InteropServices.ComTypes.VARDESC>();

                    if (varDesc.varkind == VARKIND.VAR_CONST)
                    {
                        constantValue = Marshal.GetObjectForNativeVariant(varDesc.desc.lpvarValue);
                    }
                }
                finally
                {
                    _typeInfo.ReleaseVarDesc(p);
                }

                ComVariableInfo comVariableInfo = new ComVariableInfo(this, varDesc, constantValue);
                _members.Add(comVariableInfo);
            }

            _members.Sort(delegate(ComMemberInfo a, ComMemberInfo b)
            {
                return a.Name.CompareTo(b.Name);
            });
        }

        //private void LoadFunctions()
        //{
        //    _functions = new List<ComFunctionInfo>();

        //    for (short i = 0; i < _typeAttr.cFuncs; i++)
        //    {
        //        System.Runtime.InteropServices.ComTypes.FUNCDESC funcDesc = _typeInfo.GetFuncDesc(i);

        //        ComFunctionInfo comMethodInfo = new ComFunctionInfo(this, funcDesc);
        //        _functions.Add(comMethodInfo);
        //    }

        //    _functions.Sort(delegate(ComFunctionInfo a, ComFunctionInfo b)
        //    {
        //        return a.Name.CompareTo(b.Name);
        //    });
        //}

        //private void LoadVariables()
        //{
        //    _variables = new List<ComVariableInfo>();

        //    /* Note that these are not always enum constants.  Some properties show up as VARDESC's. */
        //    for (short i = 0; i < _typeAttr.cVars; i++)
        //    {
        //        System.Runtime.InteropServices.ComTypes.VARDESC varDesc;
        //        IntPtr p = IntPtr.Zero;

        //        _typeInfo.GetVarDesc(i, out p);
        //        object constantValue = null;

        //        try
        //        {
        //            varDesc = p.ToVARDESC();

        //            if (varDesc.varkind == VARKIND.VAR_CONST)
        //            {
        //                constantValue = Marshal.GetObjectForNativeVariant(varDesc.desc.lpvarValue);
        //            }
        //        }
        //        finally
        //        {
        //            _typeInfo.ReleaseTypeAttr(p);
        //        }

        //        ComVariableInfo comVariableInfo = new ComVariableInfo(this, varDesc, constantValue);
        //        _variables.Add(comVariableInfo);
        //    }

        //    _variables.Sort(delegate(ComVariableInfo a, ComVariableInfo b)
        //    {
        //        return a.Name.CompareTo(b.Name);
        //    });
        //}

        public ComTypeLibrary ComTypeLibrary { get { return _comTypeLibrary; } }
        public string Name { get { return _name; } }
        public string FullName { get { return String.Format("{0}.{1}", _comTypeLibrary.Name, _name); } }
        public string Description { get { return _description; } }
        public ITypeInfo GetITypeInfo() { return _typeInfo; }
        public Guid Guid { get { return _typeAttr.guid; } }
        public Version Version { get { return new Version(_typeAttr.wMajorVerNum, _typeAttr.wMinorVerNum); } }

        public bool IsCollection
        {
            get
            {
                var newEnum = Members.OfType<ComFunctionInfo>().Where(x => x.DispId == NativeMethods.DISPID_NEWENUM).FirstOrDefault();
                return newEnum == null ? false : true;
            }
        }

        public override string ToString()
        {
            return FullName;
        }
    }

    public class ComAliasInfo : ComTypeInfo
    {
        public ComAliasInfo(ComTypeLibrary parent, ITypeInfo typeInfo, IntPtr pTypeAttr)
            : base(parent, typeInfo, pTypeAttr)
        {
        }
    }

    public class ComCoClassInfo : ComTypeInfo
    {
        List<ComFunctionInfo> _events = null;

        public ComCoClassInfo(ComTypeLibrary parent, ITypeInfo typeInfo, IntPtr pTypeAttr)
            : base(parent, typeInfo, pTypeAttr)
        {
        }

        public ComFunctionInfo[] Events
        {
            get
            {
                if (_events == null)
                {
                    _events = new List<ComFunctionInfo>();

                    for (int i = 0; i < ImplementedTypes.Length; i++)
                    {
                        ComImplementedTypeInfo comImplementedTypeInfo = ImplementedTypes[i];

                        if (comImplementedTypeInfo.IsSource)
                        {
                            foreach (ComFunctionInfo comFunctionInfo in comImplementedTypeInfo.ComTypeInfo.Methods)
                            {
                                if (comFunctionInfo.IsRestricted == false)
                                {
                                    if (_events.FirstOrDefault(x => x.Name.Equals(comFunctionInfo.Name)) == null)
                                    {
                                        _events.Add(comFunctionInfo);
                                    }
                                }
                            }
                        }
                    }

                    _events.Sort(delegate(ComFunctionInfo a, ComFunctionInfo b)
                    {
                        return a.Name.CompareTo(b.Name);
                    });
                }

                return _events.ToArray();
            }
        }
    }

    public class ComDispatchInfo : ComTypeInfo
    {
        public ComDispatchInfo(ComTypeLibrary parent, ITypeInfo typeInfo, IntPtr pTypeAttr)
            : base(parent, typeInfo, pTypeAttr)
        {
        }
    }

    public class ComEnumInfo : ComTypeInfo
    {
        public ComEnumInfo(ComTypeLibrary parent, ITypeInfo typeInfo, IntPtr pTypeAttr)
            : base(parent, typeInfo, pTypeAttr)
        {
        }
    }

    public class ComInterfaceInfo : ComTypeInfo
    {
        public ComInterfaceInfo(ComTypeLibrary parent, ITypeInfo typeInfo, IntPtr pTypeAttr)
            : base(parent, typeInfo, pTypeAttr)
        {
        }
    }

    public class ComMaxInfo : ComTypeInfo
    {
        public ComMaxInfo(ComTypeLibrary parent, ITypeInfo typeInfo, IntPtr pTypeAttr)
            : base(parent, typeInfo, pTypeAttr)
        {
        }
    }

    public class ComModuleInfo : ComTypeInfo
    {
        public ComModuleInfo(ComTypeLibrary parent, ITypeInfo typeInfo, IntPtr pTypeAttr)
            : base(parent, typeInfo, pTypeAttr)
        {
        }
    }

    public class ComRecordInfo : ComTypeInfo
    {
        public ComRecordInfo(ComTypeLibrary parent, ITypeInfo typeInfo, IntPtr pTypeAttr)
            : base(parent, typeInfo, pTypeAttr)
        {
        }
    }

    public class ComUnionInfo : ComTypeInfo
    {
        public ComUnionInfo(ComTypeLibrary parent, ITypeInfo typeInfo, IntPtr pTypeAttr)
            : base(parent, typeInfo, pTypeAttr)
        {
        }
    }
}
