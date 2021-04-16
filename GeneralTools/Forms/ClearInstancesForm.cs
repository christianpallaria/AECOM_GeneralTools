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
        private UIApplication _uiapp;
        private UIDocument _uidoc;
        private Application _app;
        private Document _doc;
        private ExternalEvent _exEvent;
        private HandlerClearInstances _handler;

        CommonFunctions CommFun = new CommonFunctions();

        public ClearInstancesForm (UIApplication uiapp, UIDocument uidoc, Autodesk.Revit.ApplicationServices.Application app, Document doc)
        {
            InitializeComponent();
            _uiapp = uiapp;
            _uidoc = uidoc;
            Autodesk.Revit.ApplicationServices.Application _app = app;
            _doc = doc;

            _handler = new HandlerClearInstances(this, _app, doc);
            _exEvent = ExternalEvent.Create(_handler);
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            _exEvent.Raise();

        }
    }
}
