using SolidEdge.Spy.Extensions;
using SolidEdge.Spy.InteropServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolidEdge.Spy
{
    static class CommandHelper
    {
        static ComTypeLibrary _constantsTypeLib = null;
        static Dictionary<Guid, ComEnumInfo> _environmentConstantsMap = new Dictionary<Guid, ComEnumInfo>();

        static string[,] _categoryConstantsMap = new string[,]
        {
            { CategoryIDs.CATID_SEAssembly, "AssemblyCommandConstants" },
            { CategoryIDs.CATID_SEDMAssembly, "AssemblyCommandConstants" },
            { CategoryIDs.CATID_SECuttingPlaneLine, "CuttingPlaneLineCommandConstants" },
            { CategoryIDs.CATID_SEDraft, "DetailCommandConstants" },
            { CategoryIDs.CATID_SEDrawingViewEdit, "DrawingViewEditCommandConstants" },
            { CategoryIDs.CATID_SEExplode, "ExplodeCommandConstants" },
            { CategoryIDs.CATID_SELayout, "LayoutCommandConstants" },
            { CategoryIDs.CATID_SESketch, "LayoutInPartCommandConstants" },
            { CategoryIDs.CATID_SEMotion, "MotionCommandConstants" },
            { CategoryIDs.CATID_SEPart, "PartCommandConstants" },
            { CategoryIDs.CATID_SEDMPart, "PartCommandConstants" },
            { CategoryIDs.CATID_SEProfile, "ProfileCommandConstants" },
            { CategoryIDs.CATID_SEProfileHole, "ProfileHoleCommandConstants" },
            { CategoryIDs.CATID_SEProfilePattern, "ProfilePatternCommandConstants" },
            { CategoryIDs.CATID_SEProfileRevolved, "ProfileRevolvedCommandConstants" },
            { CategoryIDs.CATID_SESheetMetal, "SheetMetalCommandConstants" },
            { CategoryIDs.CATID_SEDMSheetMetal, "SheetMetalCommandConstants" },
            { CategoryIDs.CATID_SESimplify, "SimplifyCommandConstants" },
            { CategoryIDs.CATID_SEStudio, "StudioCommandConstants" },
            { CategoryIDs.CATID_SEXpresRoute, "TubingCommandConstants" },
            { CategoryIDs.CATID_SEWeldment, "WeldmentCommandConstants" }
        };

        private static void Initialize()
        {
            _environmentConstantsMap.Clear();

            try
            {
                //Solid Edge Constants Type Library
                Guid typeLibGuid = new Guid("{C467A6F5-27ED-11D2-BE30-080036B4D502}");

                _constantsTypeLib = ComTypeManager.Instance.ComTypeLibraries.Where(x => x.Guid.Equals(typeLibGuid)).FirstOrDefault();

                if (_constantsTypeLib != null)
                {
                    for (int i = 0; i < _categoryConstantsMap.GetLength(0); i++)
                    {
                        Guid guid = new Guid(_categoryConstantsMap[i, 0]);
                        _environmentConstantsMap.Add(guid, _constantsTypeLib.Enums.Where(x => x.Name.Equals(_categoryConstantsMap[i, 1])).FirstOrDefault());
                    }
                }
            }
            catch
            {
                GlobalExceptionHandler.HandleException();
            }
        }

        public static string ResolveCommandId(SolidEdgeFramework.Application application, int theCommandID)
        {
            ComVariableInfo variableInfo = null;

            if (_environmentConstantsMap.Count == 0)
            {
                Initialize();
            }

            try
            {
                ComEnumInfo enumInfo = null;
                SolidEdgeFramework.Environment environment = application.GetActiveEnvironment();
                Guid environmentGuid = environment.GetGuid();

                if (_environmentConstantsMap.TryGetValue(environmentGuid, out enumInfo))
                {
                    variableInfo = enumInfo.Variables.Where(x => x.ConstantValue != null).Where(x => x.ConstantValue.Equals(theCommandID)).FirstOrDefault();

                    if (variableInfo != null)
                    {
                        return String.Format("{0} [{1}.{2}]", theCommandID, variableInfo.ComTypeInfo.Name, variableInfo.Name);
                    }
                }
            }
            catch
            {
                GlobalExceptionHandler.HandleException();
            }

            return String.Format("{0} [Undefined]", theCommandID);
        }
    }
}
