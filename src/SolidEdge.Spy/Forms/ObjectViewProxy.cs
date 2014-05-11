//using SolidEdge.Spy.Extensions;
//using SolidEdge.Spy.InteropServices;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Runtime.InteropServices;
//using System.Text;

//namespace SolidEdge.Spy.Forms
//{
//    public class ObjectViewProxy : MarshalByRefObject
//    {
//        private SolidEdgeFramework.Application _application = null;
//        private SolidEdgeFramework.DISEApplicationEvents_Event _applicationEvents = null;

//        public void Initialize()
//        {
//            try
//            {
//                // Register with OLE to handle concurrency issues on the current thread.
//                OleMessageFilter.Register();

//                _application = (SolidEdgeFramework.Application)Marshal.GetActiveObject("SolidEdge.Application");
//                _applicationEvents = (SolidEdgeFramework.DISEApplicationEvents_Event)_application.ApplicationEvents;
//                _applicationEvents.AfterActiveDocumentChange += _applicationEvents_AfterActiveDocumentChange;
//                _applicationEvents.AfterNewDocumentOpen += _applicationEvents_AfterNewDocumentOpen;
//                _applicationEvents.BeforeDocumentClose += _applicationEvents_BeforeDocumentClose;
//                _applicationEvents.BeforeQuit += _applicationEvents_BeforeQuit;
//            }
//            catch
//            {
//            }
//        }

//        public ComNodeMetadata[] GetRootNodes()
//        {
//            List<ComNodeMetadata> list = new List<ComNodeMetadata>();

//            if (_application != null)
//            {
//                IntPtr pUnknown = Marshal.GetIUnknownForObject(_application);
//                int count = Marshal.Release(pUnknown);

//                ComTypeInfo comTypeInfo = ComTypeInfo.FromObject(_application);

//                ComNodeMetadata metadata = new ComNodeMetadata();

//                metadata.IUnknownPointer = pUnknown;
//                metadata.Caption = "Application";
//                metadata.TypeFullName = comTypeInfo.FullName;
//                metadata.TypeGuid = comTypeInfo.Guid;

//                list.Add(metadata);
//            }

//            return list.ToArray();
//        }

//        public ComNodeMetadataBase[] GetChildren(ComNodeMetadataBase nodeMetadata)
//        {
//            List<ComNodeMetadataBase> list = new List<ComNodeMetadataBase>();

//            return list.ToArray();
//        }

//        #region SolidEdgeFramework.DISEApplicationEvents

//        void _applicationEvents_AfterNewDocumentOpen(object theDocument)
//        {
//            AppDomain.Unload(AppDomain.CurrentDomain);
//        }

//        void _applicationEvents_AfterActiveDocumentChange(object theDocument)
//        {
//            AppDomain.Unload(AppDomain.CurrentDomain);
//        }

//        void _applicationEvents_BeforeDocumentClose(object theDocument)
//        {
//            SolidEdgeFramework.SolidEdgeDocument document = theDocument as SolidEdgeFramework.SolidEdgeDocument;

//            if ((document != null) && (document.IsTemporary() == false))
//            {
//                AppDomain.Unload(AppDomain.CurrentDomain);
//            }
//        }

//        void _applicationEvents_BeforeQuit()
//        {
//            AppDomain.Unload(AppDomain.CurrentDomain);
//        }

//        #endregion

//        [Serializable]
//        public class ComNodeMetadataBase
//        {
//            public string Caption = String.Empty;
//            public string TypeFullName = String.Empty;
//        }

//        [Serializable]
//        public class ComNodeMetadata : ComNodeMetadataBase
//        {
//            public IntPtr IUnknownPointer = IntPtr.Zero;
//            public Guid TypeGuid = Guid.Empty;
//        }

//        [Serializable]
//        public class ComPropertyNodeMetadata : ComNodeMetadata
//        {
//            public int MemberId = -1;
//            public string PropertyName = String.Empty;
//        }

//        [Serializable]
//        public class ComFunctionNodeMetadata : ComNodeMetadata
//        {
//            public int MemberId = -1;
//            public string FunctionName = String.Empty;
//        }
//    }
//}
