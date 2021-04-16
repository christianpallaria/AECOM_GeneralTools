using System;
using System.Collections.Generic;

using System.Linq;
using System.Windows.Forms;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace GeneralTools
{
    public class HandlerLinePatterns : IExternalEventHandler
    {
        private Document mDoc = null;
        private LinePatternsForm lPtFrm;
        private Autodesk.Revit.ApplicationServices.Application app;
        private string filter = "";
        private bool rbKeep = false;


        CommonFunctions CommFun = new CommonFunctions();

        

        public HandlerLinePatterns(LinePatternsForm _linePatternsForm, Autodesk.Revit.ApplicationServices.Application _app, Autodesk.Revit.DB.Document _doc)
        {
            this.lPtFrm = _linePatternsForm;
            app = _app;
            mDoc = _doc;

        }


        public void Execute(UIApplication app)
        {

            filter = lPtFrm.tbFilter.Text;
            rbKeep = lPtFrm.rbKeep.Checked;

            DeletePatterns(mDoc, rbKeep, filter);

            return;
        }

        public void DeletePatterns(Document _Doc, bool _rbKeep, string _filter)
        {

            FilteredElementCollector coll = new FilteredElementCollector(_Doc).OfClass(typeof(LinePatternElement));

            List<ElementId> lstIds = CommFun.Ids(coll, _rbKeep, _filter);

            try
            {
                using (Transaction tx = new Transaction(_Doc))
                {
                    tx.Start("Delete Patterns");
                    _Doc.Delete(lstIds);
                    tx.Commit();
                    if (lstIds.Count > 0)
                    {
                        MessageBox.Show(lstIds.Count + " Line Patterns containing/not containing the word " + _filter + " have been succesfully deleted/kept!");
                    }
                    else { MessageBox.Show("There are no patterns contain the word " + _filter); }
                }
            }
            catch
            {
                MessageBox.Show("Cannot delete patterns!");
            }

            return;
        }


        public string GetName()
        {
            return "LinePatterns";
        }

    }
}
