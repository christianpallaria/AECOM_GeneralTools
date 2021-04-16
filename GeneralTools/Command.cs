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
        public ClearInstancesForm clearInstancesForm = null;

        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Autodesk.Revit.ApplicationServices.Application app = uiapp.Application;
            Document doc = uidoc.Document;

            {
                try
                {
                    if (clearInstancesForm == null)
                    {
                        clearInstancesForm = new ClearInstancesForm(uiapp, uidoc, app, doc);
                        clearInstancesForm.Show();
                    }
                    else
                    {
                        clearInstancesForm.Show();
                    }

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
        public LineStylesForm lineStyleForm = null;

        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Autodesk.Revit.ApplicationServices.Application app = uiapp.Application;
            Document doc = uidoc.Document;

            {
                try
                {
                    if (lineStyleForm == null)
                    {
                        lineStyleForm = new LineStylesForm(uiapp, uidoc, app, doc);
                        lineStyleForm.Show();
                    }
                    else
                    {
                        lineStyleForm.Show();
                    }

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
        public LinePatternsForm linePatternForm = null;

        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Autodesk.Revit.ApplicationServices.Application app = uiapp.Application;
            Document doc = uidoc.Document;

            {
                try
                {
                    if (linePatternForm == null)
                    {
                        linePatternForm = new LinePatternsForm(uiapp, uidoc, app, doc);
                        linePatternForm.Show();
                    }
                    else
                    {
                        linePatternForm.Show();
                    }

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
