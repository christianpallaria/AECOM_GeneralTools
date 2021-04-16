#region Namespaces
using System;
using System.Collections.Generic;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.IO;
using System.Windows.Media.Imaging;
using Autodesk.Revit.UI.Events;
#endregion

namespace GeneralTools
{
    public class Application : IExternalApplication
    {
        // class instance
        internal static Application thisApp = null;
        // ModelessForm instance
        private ClearInstancesForm m_MyFormClearInstance;
        private LineStylesForm m_MyFormLineStyles;
        private LinePatternsForm m_MyFormLinePatterns;

        public Result OnStartup(UIControlledApplication a)
        {
            m_MyFormClearInstance = null;
            m_MyFormLineStyles = null;
            m_MyFormLinePatterns = null; // no dialog needed yet; the command will bring it
            thisApp = this;  // static access to this application instance

            string fullPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string folderPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            string myRibbon = "AECOM";
            a.CreateRibbonTab(myRibbon);

            RibbonPanel panelA = a.CreateRibbonPanel(myRibbon, "AECOM Tools");

            //Pull Down Button
            PulldownButtonData pullDownData = new PulldownButtonData("General Tools", "General Tools");

            pullDownData.Image = new BitmapImage(new Uri(Path.Combine(folderPath, "tools_16.png"), UriKind.Absolute));
            pullDownData.LargeImage = new BitmapImage(new Uri(Path.Combine(folderPath, "tools_32.png"), UriKind.Absolute));

            PulldownButton pullDownButton = panelA.AddItem(pullDownData) as PulldownButton;

            PushButton btnClearInstancesPullDown = pullDownButton.AddPushButton(new PushButtonData("ClearDWG", "Clear imported DWG", fullPath, "GeneralTools.CommandClearInstances"));
            PushButton btnDeleteLineStyles = pullDownButton.AddPushButton(new PushButtonData("Line Styles", "Line Styles", fullPath, "GeneralTools.CommandLineStyles"));
            PushButton btnRenameLineStylesPullDown = pullDownButton.AddPushButton(new PushButtonData("Line Patterns", "Line Patterns", fullPath, "GeneralTools.CommandLinePatterns"));

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication a)
        {
            if (m_MyFormClearInstance != null && !m_MyFormClearInstance.IsDisposed)
            {
                m_MyFormClearInstance.Dispose();
                m_MyFormClearInstance = null;
            }

            if (m_MyFormLineStyles != null && !m_MyFormLineStyles.IsDisposed)
            {
                m_MyFormLineStyles.Dispose();
                m_MyFormLineStyles = null;

            }

            if (m_MyFormLinePatterns != null && !m_MyFormLinePatterns.IsDisposed)
            {
                m_MyFormLinePatterns.Dispose();
                m_MyFormLinePatterns = null;

                // if we've had a dialog, we had subscribed
                //a.Idling -= IdlingHandlerLinePatterns;
            }



            return Result.Succeeded;
        }

        //public void ShowFormLineStyles(UIApplication uiapp)
        //{
        //    // If we do not have a dialog yet, create and show it
        //    if (m_MyFormLineStyles == null || m_MyFormLineStyles.IsDisposed)
        //    {

        //        // A new handler to handle request posting by the dialog
        //        HandlerLineStyles handler = new RequestHandlerLineStyles();

        //        // External Event for the dialog to use (to post requests)
        //        ExternalEvent exEvent = ExternalEvent.Create(handler);


        //        m_MyFormLineStyles = new LineStylesForm(exEvent, handler);
        //        m_MyFormLineStyles.Show();

        //    }
        //}


        //public void IdlingHandlerLinePatterns(object sender, IdlingEventArgs args)
        //{
        //    UIApplication uiapp = sender as UIApplication;

        //    if (m_MyFormLinePatterns.IsDisposed)
        //    {
        //        uiapp.Idling -= IdlingHandlerLinePatterns;
        //        return;
        //    }
        //    else   // dialog still exists
        //    {
        //        // fetch the request from the dialog

        //        RequestId request = m_MyFormLinePatterns.RequestLineStyles.Take();

        //        if (request != RequestId.None)
        //        {
        //            try
        //            {
        //                // we take the request, if any was made,
        //                // and pass it on to the request executor

        //                RequestHandlerLineStyle.Execute(uiapp, request);
        //            }
        //            finally
        //            {
        //                // The dialog may be in its waiting state;
        //                // make sure we wake it up even if we get an exception.

        //                m_MyFormLinePatterns.WakeUp();
        //            }
        //        }
        //    }

        //    return;
        //}

    }
}
