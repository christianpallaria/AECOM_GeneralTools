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
        private UIApplication _uiapp;
        private UIDocument _uidoc;
        private Application _app;
        private Document _doc;
        private ExternalEvent _exEventDeleteLinePatterns;
        private HandlerLinePatterns _handlerDeleteLinePatterns;
        private ExternalEvent _exEventRenameLinePatterns;
        private HandlerRenameLinePatterns _handlerRenameLinePatterns;

        CommonFunctions CommFun = new CommonFunctions();

        public LinePatternsForm(UIApplication uiapp, UIDocument uidoc, Autodesk.Revit.ApplicationServices.Application app, Document doc)
        {
            InitializeComponent();
            _uiapp = uiapp;
            _uidoc = uidoc;
            Autodesk.Revit.ApplicationServices.Application _app = app;
            _doc = doc;

            _handlerDeleteLinePatterns = new HandlerLinePatterns(this, _app, doc);
            _exEventDeleteLinePatterns = ExternalEvent.Create(_handlerDeleteLinePatterns);

            _handlerRenameLinePatterns = new HandlerRenameLinePatterns(this, _app, doc);
            _exEventRenameLinePatterns = ExternalEvent.Create(_handlerRenameLinePatterns);



        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            // we own both the event and the handler
            // we should dispose it before we are closed
            _exEventDeleteLinePatterns.Dispose();
            _exEventDeleteLinePatterns = null;
            _handlerDeleteLinePatterns = null;

            // do not forget to call the base class
            base.OnFormClosed(e);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            _exEventDeleteLinePatterns.Raise();           

        }



        private void button2_Click(object sender, EventArgs e)
        {
            _exEventRenameLinePatterns.Raise();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
