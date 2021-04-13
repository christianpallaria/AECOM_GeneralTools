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
    public partial class ___LinePatterns : System.Windows.Forms.Form
    {
        //UIApplication mUiapp = null;
        Document mDoc = null;
        CommonFunctions CommFun = new CommonFunctions();

        public ___LinePatterns()
        {
            InitializeComponent();
            RequestLineStyles Request = new RequestLineStyles();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string Find = tbFind.Text;
            string Replace = tbReplace.Text;
            RenLinePatterns(mDoc,Find,Replace,true);
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

    }
}
