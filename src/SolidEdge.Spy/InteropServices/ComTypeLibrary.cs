using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace SolidEdge.Spy.InteropServices
{
    public class ComTypeLibrary
    {
        private string _fileName;
        private ITypeLib _typeLib;
        private string _name = String.Empty;
        private string _description = String.Empty;
        private int _helpContext = 0;
        private string _helpFile = String.Empty;
        private System.Runtime.InteropServices.ComTypes.TYPELIBATTR _typeLibAttr;
        private List<ComTypeInfo> _typeInfos = null;

        public ComTypeLibrary(ITypeLib typeLib)
        {
            _typeLib = typeLib;
            _typeLib.GetDocumentation(-1, out _name, out _description, out _helpContext, out _helpFile);
            _typeLibAttr = _typeLib.GetLibAttr();

            _fileName = NativeMethods.QueryPathOfRegTypeLib(_typeLibAttr.guid, _typeLibAttr.wMajorVerNum, _typeLibAttr.wMinorVerNum, _typeLibAttr.lcid);
            _fileName = _fileName.Trim(new [] { '\0' });

            _fileName = Path.GetFullPath(_fileName);
        }

        public string Name { get { return _name; } }
        public string Description { get { return _description; } }
        public int HelpContext { get { return _helpContext; } }
        public string HelpFile { get { return _helpFile; } }
        public string Filename { get { return _fileName; } }

        public ITypeLib GetITypeLib() { return _typeLib; }

        public Guid Guid { get { return _typeLibAttr.guid; } }
        public int Lcid { get { return _typeLibAttr.lcid; } }
        public System.Runtime.InteropServices.ComTypes.SYSKIND Syskind { get { return _typeLibAttr.syskind; } }
        public System.Runtime.InteropServices.ComTypes.LIBFLAGS Libflags { get { return _typeLibAttr.wLibFlags; } }
        public Version Version { get { return new Version(_typeLibAttr.wMajorVerNum, _typeLibAttr.wMinorVerNum); } }

        public static ComTypeLibrary FromRegistry(Guid guid, short wVerMajor, short wVerMinor)
        {
            return new ComTypeLibrary(NativeMethods.LoadRegTypeLib(guid, wVerMajor, wVerMinor));
        }

        public ComTypeInfo[] ComTypeInfos
        {
            get
            {
                if (_typeInfos == null)
                {
                    LoadTypes();
                }

                return _typeInfos.ToArray();
            }
        }

        public ComAliasInfo[] Typedefs
        {
            get
            {
                return ComTypeInfos.Where(entity => entity is ComAliasInfo).Cast<ComAliasInfo>().ToArray();
            }
        }

        public ComCoClassInfo[] CoClasses
        {
            get
            {
                return ComTypeInfos.Where(entity => entity is ComCoClassInfo).Cast<ComCoClassInfo>().ToArray();
            }
        }

        public ComDispatchInfo[] Dispinterfaces
        {
            get
            {
                return ComTypeInfos.Where(entity => entity is ComDispatchInfo).Cast<ComDispatchInfo>().ToArray();
            }
        }

        public ComEnumInfo[] Enums
        {
            get
            {
                return ComTypeInfos.Where(entity => entity is ComEnumInfo).Cast<ComEnumInfo>().ToArray();
            }
        }

        public ComInterfaceInfo[] Interfaces
        {
            get
            {
                return ComTypeInfos.Where(entity => entity is ComInterfaceInfo).Cast<ComInterfaceInfo>().ToArray();
            }
        }

        public ComModuleInfo[] Modules
        {
            get
            {
                return ComTypeInfos.Where(entity => entity is ComModuleInfo).Cast<ComModuleInfo>().ToArray();
            }
        }

        public ComRecordInfo[] Structs
        {
            get
            {
                return ComTypeInfos.Where(entity => entity is ComRecordInfo).Cast<ComRecordInfo>().ToArray();
            }
        }

        public ComUnionInfo[] Unions
        {
            get
            {
                return ComTypeInfos.Where(entity => entity is ComUnionInfo).Cast<ComUnionInfo>().ToArray();
            }
        }

        public override string ToString()
        {
            return _name;
        }

        private void LoadTypes()
        {
            _typeInfos = new List<ComTypeInfo>();

            int count = _typeLib.GetTypeInfoCount();

            for (int i = 0; i < count; i++)
            {
                ITypeInfo typeInfo = null;
                _typeLib.GetTypeInfo(i, out typeInfo);

                IntPtr pTypeAttr = IntPtr.Zero;
                typeInfo.GetTypeAttr(out pTypeAttr);
                System.Runtime.InteropServices.ComTypes.TYPEATTR typeAttr = pTypeAttr.ToStructure<System.Runtime.InteropServices.ComTypes.TYPEATTR>();

                switch (typeAttr.typekind)
                {
                    case System.Runtime.InteropServices.ComTypes.TYPEKIND.TKIND_ALIAS:
                        _typeInfos.Add(new ComAliasInfo(this, typeInfo, pTypeAttr));
                        break;
                    case System.Runtime.InteropServices.ComTypes.TYPEKIND.TKIND_COCLASS:
                        _typeInfos.Add(new ComCoClassInfo(this, typeInfo, pTypeAttr));
                        break;
                    case System.Runtime.InteropServices.ComTypes.TYPEKIND.TKIND_DISPATCH:
                        _typeInfos.Add(new ComDispatchInfo(this, typeInfo, pTypeAttr));
                        break;
                    case System.Runtime.InteropServices.ComTypes.TYPEKIND.TKIND_ENUM:
                        _typeInfos.Add(new ComEnumInfo(this, typeInfo, pTypeAttr));
                        break;
                    case System.Runtime.InteropServices.ComTypes.TYPEKIND.TKIND_INTERFACE:
                        _typeInfos.Add(new ComInterfaceInfo(this, typeInfo, pTypeAttr));
                        break;
                    //case System.Runtime.InteropServices.ComTypes.TYPEKIND.TKIND_MAX:
                    //    _typeInfos.Add(new ComMaxInfo(this, typeInfo, pTypeAttr));
                    //    break;
                    case System.Runtime.InteropServices.ComTypes.TYPEKIND.TKIND_MODULE:
                        _typeInfos.Add(new ComModuleInfo(this, typeInfo, pTypeAttr));
                        break;
                    case System.Runtime.InteropServices.ComTypes.TYPEKIND.TKIND_RECORD:
                        _typeInfos.Add(new ComRecordInfo(this, typeInfo, pTypeAttr));
                        break;
                    case System.Runtime.InteropServices.ComTypes.TYPEKIND.TKIND_UNION:
                        _typeInfos.Add(new ComUnionInfo(this, typeInfo, pTypeAttr));
                        break;
                }
            }

            _typeInfos.Sort(delegate(ComTypeInfo a, ComTypeInfo b)
            {
                return a.Name.CompareTo(b.Name);
            });
        }

        public static ComTypeLibrary FromObject(object o)
        {
            return FromIDispatch((IDispatch)o);
        }

        public static ComTypeLibrary FromIDispatch(IDispatch dispatch)
        {
            ITypeLib typeLib = null;
            ITypeInfo typeInfo = dispatch.GetTypeInfo();
            int index = 0;

            typeInfo.GetContainingTypeLib(out typeLib, out index);

            return new ComTypeLibrary(typeLib);
        }
    }
}
