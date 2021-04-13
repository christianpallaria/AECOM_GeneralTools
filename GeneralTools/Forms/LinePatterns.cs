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
    public partial class LinePatternsForm : System.Windows.Forms.Form
    {
        //UIApplication mUiapp = null;
        Document mDoc = null;

        CommonFunctions CommFun = new CommonFunctions();

        public LinePatternsForm()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string Filter = tbFilter.Text;

            FilteredElementCollector coll = new FilteredElementCollector(mDoc).OfClass(typeof(LinePatternElement));

            List<ElementId> lstIds = CommFun.Ids(coll, rbKeep, Filter);

            try
            {
                using (Transaction tx = new Transaction(mDoc))
                {
                    tx.Start("Delete Patterns");
                    mDoc.Delete(lstIds);
                    tx.Commit();
                    if (lstIds.Count > 0)
                    {
                        MessageBox.Show(lstIds.Count + " Line Patterns containing the word " + Filter + " have been succesfully deleted/kept!" );
                    }
                    else {MessageBox.Show("There are no patterns contain the word " + Filter); }
                }
            }
            catch
            {
                MessageBox.Show("Cannot delete patterns!");
            }
            

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbKeep_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void tbFilter_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string Find = tbFind.Text;
            string Replace = tbReplace.Text;
            RenLinePatterns(mDoc, Find, Replace, true);
        }

        private void RenLinePatterns(Document pDoc, string pstrFind, string pstrReplace, bool pblnMatchCase)
        {
            //Category linesCat = pDoc.Settings.Categories.get_Item(BuiltInCategory.OST_Lines);


            FilteredElementCollector coll = new FilteredElementCollector(mDoc).OfClass(typeof(LinePatternElement));
            IList<LinePatternElement> patternElement;

            using (Transaction tr = new Transaction(pDoc, "Rename Line Patterns"))
            {
                tr.Start();

                if (pblnMatchCase)
                {
                    patternElement = coll.Cast<LinePatternElement>().Where(c => c.Name.ToUpper().Contains(pstrFind.ToUpper())).ToList();
                }
                else
                {
                    patternElement = coll.Cast<LinePatternElement>().Where(c => c.Name.Contains(pstrFind)).ToList();
                }

                foreach (LinePatternElement cat in patternElement)
                {
                    ElementId eid = cat.Id as ElementId;
                    Element type = pDoc.GetElement(eid);

                    if (null != type)
                    {
                        type.Name = CommFun.Rename(cat.Name, pstrFind, pstrReplace);
                    }
                }

                tr.Commit();

                if (patternElement.Count > 0)
                {
                    MessageBox.Show(patternElement.Count + " Line Patterns containing the word " + pstrFind + " have been succesfully renamed!");
                }
                else { MessageBox.Show("Nome of the Line Patterns match with you find rules"); }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
