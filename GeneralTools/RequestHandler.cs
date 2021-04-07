using System;
using System.Collections.Generic;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace GeneralTools
{
    public static class RequestHandler
    {

        public static void Execute(UIApplication uiapp, RequestId reqest)
        {
            switch (reqest)
            {
                case RequestId.None:
                    {
                        return;  // no request at this time -> we can leave immediately
                    }
                case RequestId.ClearInst:
                    {
                        //ModifySelectedDoors(uiapp, "Delete doors", e => e.Document.Delete(e.Id));
                        break;
                    }
                case RequestId.LineStylesDelete:
                    {
                       // ModifySelectedDoors(uiapp, "Flip door Hand", e => e.flipHand());
                        break;
                    }
                case RequestId.LineStylesRename:
                    {
                        //ModifySelectedDoors(uiapp, "Flip door Facing", e => e.flipFacing());
                        break;
                    }
                case RequestId.LinePatternsDelete:
                    {
                        //ModifySelectedDoors(uiapp, "Make door Left", MakeLeft);
                        break;
                    }
                case RequestId.LinePatternsRename:
                    {
                        //ModifySelectedDoors(uiapp, "Make door Right", MakeRight);
                        break;
                    }
                default:
                    {
                        // some kind of a warning here should
                        // notify us about an unexpected request 
                        break;
                    }
            }

            return;
        }



    }
}
