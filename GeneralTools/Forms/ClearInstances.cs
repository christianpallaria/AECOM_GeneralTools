using Autodesk.Revit.UI;
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
using System.Security.Cryptography.X509Certificates;

namespace GeneralTools
{
    public partial class ClearInstancesForm : System.Windows.Forms.Form
    {
        //UIApplication mUiapp = null;
        Document mDoc = null;
        CommonFunctions CommFun = new CommonFunctions();

        public ClearInstancesForm ()//(UIApplication pUiapp)
        {
            InitializeComponent();
            //mUiapp = pUiapp;
            //mDoc = mUiapp.ActiveUIDocument.Document;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // Modify document within a transaction
            try
            {

                FilteredElementCollector ImportedInstancesCollector = new FilteredElementCollector(mDoc).OfClass(typeof(ImportInstance)).WhereElementIsNotElementType();

                int count = ImportedInstancesCollector.GetElementCount();
                List<string> str = new List<string>();

                using (Transaction tx = new Transaction(mDoc, "Clear All Imported DWG Instances"))
                {
                    tx.Start();

                    if (count == 0)
                    {
                        MessageBox.Show("There are no imported instances in this project file!", "Warning");
                    }
                    else
                    {

                        string filter = tbFilter.Text;
                        List<ElementId> Ids = new List<ElementId>();

                        foreach (Element c in ImportedInstancesCollector)
                        {

                            Parameter n = c.get_Parameter(BuiltInParameter.IMPORT_SYMBOL_NAME);

                         
                            if (n.AsString().Contains(filter))
                            {
                                Ids.Add(c.Id);
                            }
                        }
                        mDoc.Delete(Ids);
                        if (Ids.Count > 0) { MessageBox.Show(Ids.Count + " instances containing the word " + filter + " have been succesfully deleted!"); }
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
    }
}
