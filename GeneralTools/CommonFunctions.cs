using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Revit.DB;
using Microsoft.Office.Interop.Excel;
using _Excel = Microsoft.Office.Interop.Excel;

namespace GeneralTools
{
    class CommonFunctions
    {
        public List<ElementId> Ids(FilteredElementCollector _coll, bool _rbKeep, string _filter)
        {
            if (_rbKeep)
            {
                List<ElementId> _lstIds = _coll.Cast<LinePatternElement>().Where(x => !x.GetLinePattern().Name.ToUpper().Contains(_filter.ToUpper())).Select(x => x.Id).ToList();
                return _lstIds;
            }
            else
            {
                List<ElementId> _lstIds = _coll.Cast<LinePatternElement>().Where(x => x.GetLinePattern().Name.ToUpper().Contains(_filter.ToUpper())).Select(x => x.Id).ToList();
                return _lstIds;
            }

        }

        public string Rename(string pstrOldName, string pstrFind, string pstrReplace)
        {
            StringBuilder sbNewName = new StringBuilder(pstrOldName);
            sbNewName.Replace(pstrFind, pstrReplace);

            return sbNewName.ToString();
        }

        public void OpenFile(System.Windows.Forms.TextBox _txtInput)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Excel Worksheets|*.xls;*.xlsx";
                if (openFileDialog.ShowDialog() != DialogResult.OK)
                    return;
                _txtInput.Text = openFileDialog.FileName;
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

    }

    class Excel
    {
        string path = "";
        _Application excel = new _Excel.Application();
        Workbook wb;
        Worksheet ws;

        public Excel(string path, int Sheet)
        {
            this.path = path;
            wb = excel.Workbooks.Open(path);
            ws = wb.Worksheets[Sheet];
        }

        public string ReadCell(int i, int j)
        {
            i++;
            j++;

            if (ws.Cells[i,j].Value2 != null)
            {
                return ws.Cells[i, j].Value2;
            }
            else
            {
                return "";
            }

        }
    }

}
