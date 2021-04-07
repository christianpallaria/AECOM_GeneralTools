#region Namespaces
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;

using System.Windows.Forms;
using System.Collections;
using Autodesk.Revit;

#endregion

namespace GeneralTools
{
    
    [Transaction(TransactionMode.Manual)]
    public class CommandClearInstances : IExternalCommand
    {
        App app = new App();

        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            //    UIApplication uiapp = commandData.Application;
            //    UIDocument uidoc = uiapp.ActiveUIDocument;
            //    Application app = uiapp.Application;
            //    Document doc = uidoc.Document;

            //    // Access current selection

            //    Selection sel = uidoc.Selection;

            //    ClearInstances myform = new ClearInstances(uiapp);

            //    myform.ShowDialog();

            //    return Result.Succeeded;

            //COMMENT ADDED TO TEST GITHUB

            {
                try
                {
                    app.ShowFormClearInst(commandData.Application);

                    return Result.Succeeded;
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                    return Result.Failed;
                }
            }

        }
    }

    [Transaction(TransactionMode.Manual)]
    public class CommandLineStyles : IExternalCommand
    {
        App app = new App();

        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            {
                try
                {
                    app.ShowFormLineStyles(commandData.Application);

                    return Result.Succeeded;
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                    return Result.Failed;
                }
            }
        }
    }

    [Transaction(TransactionMode.Manual)]
    public class CommandLinePatterns : IExternalCommand
    {
        App app = new App();

        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            {
                try
                {
                    app.ShowFormLinePatterns(commandData.Application);

                    return Result.Succeeded;
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                    return Result.Failed;
                }
            }
        }
    }

}
