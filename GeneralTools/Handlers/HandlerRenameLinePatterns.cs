using System;
using System.Collections.Generic;

using System.Linq;
using System.Windows.Forms;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace GeneralTools
{
    class HandlerRenameLinePatterns : IExternalEventHandler
    {
        private Document mDoc = null;
        private LinePatternsForm lPtFrm;
        private Autodesk.Revit.ApplicationServices.Application app;
        private string strFind = "";
        private string strReplace = "";


        CommonFunctions CommFun = new CommonFunctions();



        public HandlerRenameLinePatterns(LinePatternsForm _linePatternsForm, Autodesk.Revit.ApplicationServices.Application _app, Autodesk.Revit.DB.Document _doc)
        {
            this.lPtFrm = _linePatternsForm;
            app = _app;
            mDoc = _doc;

        }


        public void Execute(UIApplication app)
        {

            strFind = lPtFrm.tbFind.Text;
            strReplace = lPtFrm.tbReplace.Text;

            RenLinePatterns(mDoc, strFind, strReplace, true);

            return;
        }

        private void RenLinePatterns(Document pDoc, string _strFind, string _strReplace, bool pblnMatchCase)
        {

            FilteredElementCollector coll = new FilteredElementCollector(pDoc).OfClass(typeof(LinePatternElement));
            IList<LinePatternElement> patternElement;

            using (Transaction tr = new Transaction(pDoc, "Rename Line Patterns"))
            {
                tr.Start();

                if (pblnMatchCase)
                {
                    patternElement = coll.Cast<LinePatternElement>().Where(c => c.Name.ToUpper().Contains(_strFind.ToUpper())).ToList();
                }
                else
                {
                    patternElement = coll.Cast<LinePatternElement>().Where(c => c.Name.Contains(_strFind)).ToList();
                }

                foreach (LinePatternElement cat in patternElement)
                {
                    ElementId eid = cat.Id as ElementId;
                    Element type = pDoc.GetElement(eid);

                    if (null != type)
                    {
                        type.Name = CommFun.Rename(cat.Name, _strFind, _strReplace);
                    }
                }

                tr.Commit();

                if (patternElement.Count > 0)
                {
                    MessageBox.Show(patternElement.Count + " Line Patterns containing the word " + _strFind + " have been succesfully renamed!");
                }
                else { MessageBox.Show("Nome of the Line Patterns match with you find rules"); }

            }
        }

        public string GetName()
        {
            return "LinePatterns";
        }
    }
}
