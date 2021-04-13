using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace GeneralTools
{
    public partial class LineStylesForm : System.Windows.Forms.Form
    {
        //UIApplication mUiapp = null;
        private Document _mDoc = null;
        private CommonFunctions _CommFun = new CommonFunctions();
        private ExternalEvent _exEvent;
        private RequestHandlerLineStyles _handler;

        public LineStylesForm()
        {
            InitializeComponent();
        }

        public LineStylesForm(ExternalEvent exEvent, RequestHandlerLineStyles handler)
        {
            this._exEvent = exEvent;
            this._handler = handler;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string find = tbFilter.Text;
            MakeRequest(RequestId.LineStylesDelete); //add this if you want to add multiple parameters: , tbFilter.Text, tbFind.Text, tbReplace.Text);
        }

        private void MakeRequest(RequestId request) //add this if you want to add multiple parameters: , params string[] parameters)
        {
            _handler.Request.Make(request); //add this if you want to add multiple parameters: , parameters);
            _exEvent.Raise();

        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            // we own both the event and the handler
            // we should dispose it before we are closed
            _exEvent.Dispose();
            _exEvent = null;
            _handler = null;

            // do not forget to call the base class
            base.OnFormClosed(e);
        }

        private void btnCancel1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK1_Click(object sender, EventArgs e)
        {
            try
            {
                RenLineStyles(_mDoc, tbFind.Text, tbReplace.Text, false);
            }
            catch
            {
                MessageBox.Show("Cannot rename Line Styles", "Warning");
            }
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

        private void RenLineStyles(Document pDoc, string pstrFind, string pstrReplace, bool pblnMatchCase)
        {
            Category linesCat = pDoc.Settings.Categories.get_Item(BuiltInCategory.OST_Lines);
            IList<Category> catList;

            using (Transaction tr = new Transaction(pDoc, "Rename Linestyle"))
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
                    Element type = pDoc.GetElement(eid);

                    if (null != type)
                    {
                        type.Name = _CommFun.Rename(cat.Name, pstrFind, pstrReplace);
                    }
                }

                tr.Commit();

                if (catList.Count > 0)
                {
                    MessageBox.Show(catList.Count + " Line Styles containing the word " + pstrFind + " have been succesfully renamed!");
                }
                else { MessageBox.Show("Nome of the Line Styles match with you find rules"); }

            }
        }
    }
}
