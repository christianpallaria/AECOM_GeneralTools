using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace GeneralTools
{
    public partial class _____RenameLineStyles : System.Windows.Forms.Form
    {
        UIApplication mUiapp = null;
        Document mDoc = null;
        CommonFunctions CommFun = new CommonFunctions();

        public _____RenameLineStyles(UIApplication pUiapp)
        {
            InitializeComponent();
            mUiapp = pUiapp;
            mDoc = mUiapp.ActiveUIDocument.Document;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                RenLineStyles(mDoc, tbFind.Text, tbReplace.Text, false);
            }
            catch
            {
                MessageBox.Show("Cannot rename Line Styles", "Warning");
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
                        type.Name = CommFun.Rename(cat.Name, pstrFind, pstrReplace);
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
