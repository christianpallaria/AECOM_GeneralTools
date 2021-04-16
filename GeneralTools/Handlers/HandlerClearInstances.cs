using System;
using System.Collections.Generic;

using System.Linq;
using System.Windows.Forms;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace GeneralTools
{
    public class HandlerClearInstances : IExternalEventHandler
    {
        private Document mDoc = null;
        private ClearInstancesForm clrFrm;
        private Autodesk.Revit.ApplicationServices.Application app;
        private string filter = "";


        CommonFunctions CommFun = new CommonFunctions();



        public HandlerClearInstances(ClearInstancesForm _clearInstancesForm, Autodesk.Revit.ApplicationServices.Application _app, Autodesk.Revit.DB.Document _doc)
        {
            this.clrFrm = _clearInstancesForm;
            app = _app;
            mDoc = _doc;

        }

        public void Execute(UIApplication app)
        {

            filter = clrFrm.tbFilter.Text;

            ClearInstances(mDoc, filter);

            return;
        }

        public void ClearInstances(Document _doc, string _filter)
        {
            try
            {

                FilteredElementCollector ImportedInstancesCollector = new FilteredElementCollector(_doc).OfClass(typeof(ImportInstance)).WhereElementIsNotElementType();

                int count = ImportedInstancesCollector.GetElementCount();
                List<string> str = new List<string>();

                using (Transaction tx = new Transaction(_doc, "Clear All Imported DWG Instances"))
                {
                    tx.Start();

                    if (count == 0)
                    {
                        MessageBox.Show("There are no imported instances in this project file!", "Warning");
                    }
                    else
                    {

                        List<ElementId> Ids = new List<ElementId>();

                        foreach (Element c in ImportedInstancesCollector)
                        {

                            Parameter n = c.get_Parameter(BuiltInParameter.IMPORT_SYMBOL_NAME);


                            if (n.AsString().Contains(_filter))
                            {
                                Ids.Add(c.Id);
                            }
                        }
                        _doc.Delete(Ids);
                        if (Ids.Count > 0) { MessageBox.Show(Ids.Count + " instances containing the word " + _filter + " have been succesfully deleted!"); }
                        else { MessageBox.Show("None of the imported instances in the project match with the filter!"); }

                    }

                    tx.Commit();
                }

            }

            catch
            {
                MessageBox.Show("Cannot collect imported instances!", "Warning");
            }


        }

        public string GetName()
        {
            return "Clear Instances";
        }
    }


}
