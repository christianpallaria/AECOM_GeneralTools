using System;
using System.Collections.Generic;

using System.Linq;
using System.Windows.Forms;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace GeneralTools
{
    public class HandlerLineStyles : IExternalEventHandler
    {
        private Document mDoc = null;
        private LineStylesForm lStFrm;
        private Autodesk.Revit.ApplicationServices.Application app;
        private string _tbFilter = "";

        public HandlerLineStyles(LineStylesForm _lineStyleForm, Autodesk.Revit.ApplicationServices.Application _app, Autodesk.Revit.DB.Document _doc)
        {
            this.lStFrm = _lineStyleForm;
            app = _app;
            mDoc = _doc;

        }


        public void Execute(UIApplication app)
        {
            _tbFilter = lStFrm.tbFilter.Text;

            DelLineStyles(mDoc, _tbFilter, true);

            //switch (Request.Take())
            //{
            //    case RequestId.None:
            //        {
            //            return;  // no request at this time -> we can leave immediately
            //        }
            //    case RequestId.ClearInst:
            //        {
            //            //ModifySelectedDoors(uiapp, "Delete doors", e => e.Document.Delete(e.Id));
            //            break;
            //        }
            //    case RequestId.LineStylesDelete:
            //        {
            //           // ModifySelectedDoors(uiapp, "Flip door Hand", e => e.flipHand());
            //            break;
            //        }
            //    case RequestId.LineStylesRename:
            //        {
            //            break;
            //        }
            //    case RequestId.LinePatternsDelete:
            //        {
            //            DelLineStyles(mUiapp.ActiveUIDocument.Document,"testLineStyle",true);
            //            break;
            //        }
            //    case RequestId.LinePatternsRename:
            //        {
            //            //ModifySelectedDoors(uiapp, "Make door Right", MakeRight);
            //            break;
            //        }
            //    default:
            //        {
            //            // some kind of a warning here should
            //            // notify us about an unexpected request 
            //            break;
            //        }
            //}

            return;
        }


        public string GetName()
        {
            return "LineStyles";
        }

        private void DelLineStyles(Document pDoc, string pstrFind, bool pblnMatchCase)
        {
            Category linesCat = pDoc.Settings.Categories.get_Item(BuiltInCategory.OST_Lines);
            IList<Category> catList;
            List<ElementId> IdsToDelete = new List<ElementId>();

            using (Transaction tr = new Transaction(pDoc, "Delete Linestyle"))
            {
                tr.Start();

                if (pblnMatchCase)
                {
                    catList = linesCat.SubCategories.Cast<Category>().Where(c => c.Name.ToUpper().Contains(pstrFind.ToUpper())).ToList();
                }
                else
                {
                    catList = linesCat.SubCategories.Cast<Category>().Where(c => c.Name.Contains(pstrFind)).ToList();
                }



                foreach (Category cat in catList)
                {

                    ElementId eid = cat.Id as ElementId;
                    IdsToDelete.Add(eid);

                }

                try
                {
                    pDoc.Delete(IdsToDelete);
                }
                catch
                {
                    MessageBox.Show("Cannot delete system Line Styles");
                }


                tr.Commit();

                if (catList.Count > 0)
                {
                    MessageBox.Show(catList.Count + " Line Styles containing the word" + pstrFind + " have been succesfully deleted");
                }
                else { MessageBox.Show("Nome of the Line Styles match with you find rules"); }

            }
        }

    }
}
