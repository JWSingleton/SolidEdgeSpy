using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace SolidEdge.Spy.InteropServices
{
    public delegate void ComTypeLibrarySelectedHandler(object sender, ComTypeLibrary comTypeLibrary);
    public delegate void ComTypeInfoSelectedHandler(object sender, ComTypeInfo comTypeInfo);

    public sealed class ComTypeManager
    {
        private List<ComTypeLibrary> _typeLibraries = new List<ComTypeLibrary>();

        private static volatile ComTypeManager _instance;
        private static object _syncRoot = new Object();

        public event ComTypeLibrarySelectedHandler ComTypeLibrarySelected;
        public event ComTypeInfoSelectedHandler ComTypeInfoSelected;

        private ComTypeManager() { }

        public static ComTypeManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_syncRoot)
                    {
                        if (_instance == null)
                            _instance = new ComTypeManager();
                    }
                }

                return _instance;
            }
        }

        private ComTypeLibrary GetComTypeLibrary(ITypeLib typeLib)
        {
            Guid typeLibGuid = typeLib.GetGuid();
            Version typeLibVersion = typeLib.GetVersion();

            ComTypeLibrary comTypeLibrary = _typeLibraries.Where(
                x => x.Guid.Equals(typeLibGuid)).Where(
                x => x.Version.Equals(typeLibVersion)
                ).FirstOrDefault();

            if (comTypeLibrary == null)
            {
                comTypeLibrary = new ComTypeLibrary(typeLib);
                _typeLibraries.Add(comTypeLibrary);
                _typeLibraries.Sort(delegate(ComTypeLibrary a, ComTypeLibrary b)
                {
                    return a.Name.CompareTo(b.Name);
                });
            }

            return comTypeLibrary;
        }

        public ComTypeLibrary LoadRegTypeLib(Guid guid, Version version)
        {
            ComTypeLibrary comTypeLibrary = null;

            try
            {
                ITypeLib typeLib = NativeMethods.LoadRegTypeLib(guid, (short)version.Major, (short)version.Minor);
                comTypeLibrary = GetComTypeLibrary(typeLib);
            }
            catch
            {
                GlobalExceptionHandler.HandleException();
            }

            return comTypeLibrary;
        }

        public ComTypeLibrary LoadRegTypeLib(Guid guid, short wVerMajor, short wVerMinor)
        {
            return LoadRegTypeLib(guid, new Version(wVerMajor, wVerMinor));
        }

        public ComTypeInfo FromITypeInfo(ITypeInfo typeInfo)
        {
            if (typeInfo == null) return null;

            ITypeLib typeLib = null;
            int index = 0;
            typeInfo.GetContainingTypeLib(out typeLib, out index);

            ComTypeLibrary comTypeLibrary = GetComTypeLibrary(typeLib);

            string typeName = Marshal.GetTypeInfoName(typeInfo);

            if (comTypeLibrary != null)
            {
                return comTypeLibrary.ComTypeInfos.Where(
                    x => x.Name.Equals(typeName)).FirstOrDefault();
            }

            return null;
        }

        public ComTypeInfo FromIDispatch(IDispatch dispatch)
        {
            if (dispatch == null) return null;

            return FromITypeInfo(dispatch.GetTypeInfo());
        }

        public ComTypeInfo LookupUserDefined(System.Runtime.InteropServices.ComTypes.TYPEDESC typeDesc, ComTypeInfo comTypeInfo)
        {
            if (comTypeInfo == null) return null;

            ITypeInfo refTypeInfo = null;
            VarEnum variantType = (VarEnum)typeDesc.vt;

            if (variantType == VarEnum.VT_USERDEFINED)
            {
                IFixedTypeInfo fixedTypeInfo = (IFixedTypeInfo)comTypeInfo.GetITypeInfo();
                fixedTypeInfo.GetRefTypeInfo(typeDesc.lpValue, out refTypeInfo);
                return ComTypeManager.Instance.FromITypeInfo(refTypeInfo);
            }

            return null;
        }

        public ComTypeLibrary[] ComTypeLibraries { get { return _typeLibraries.ToArray(); } }

        public bool HasComType(string fullName)
        {
            string[] tokens = fullName.Split(new char[] { '.' });

            if (tokens.Length == 2)
            {
                ComTypeLibrary comTypeLibrary = _typeLibraries.Where(x => x.Name.Equals(tokens[0])).FirstOrDefault();
                if (comTypeLibrary != null)
                {
                    ComTypeInfo comTypeInfo = comTypeLibrary.ComTypeInfos.Where(x => x.Name.Equals(tokens[1])).FirstOrDefault();
                    if (comTypeInfo != null)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public void LookupAndSelect(string name)
        {
            if (String.IsNullOrWhiteSpace(name)) return;

            string[] tokens = name.Split(new char[] { '.' });

            ComTypeLibrary comTypeLibrary = null;
            ComTypeInfo comTypeInfo = null;

            if (tokens.Length == 1)
            {
                comTypeLibrary = _typeLibraries.Where(x => x.Name.Equals(tokens[0])).FirstOrDefault();
                if (comTypeLibrary != null)
                {
                    if (ComTypeLibrarySelected != null)
                    {
                        ComTypeLibrarySelected(this, comTypeLibrary);
                    }
                }
            }
            else if (tokens.Length == 2)
            {
                comTypeLibrary = _typeLibraries.Where(x => x.Name.Equals(tokens[0])).FirstOrDefault();
                if (comTypeLibrary != null)
                {
                    comTypeInfo = comTypeLibrary.ComTypeInfos.Where(x => x.Name.Equals(tokens[1])).FirstOrDefault();
                    if ((comTypeInfo != null) && (ComTypeInfoSelected != null))
                    {
                        ComTypeInfoSelected(this, comTypeInfo);
                    }
                }
            }
        }
    }
}
