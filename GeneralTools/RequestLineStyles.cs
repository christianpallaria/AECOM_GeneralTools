﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace GeneralTools
{

    public enum RequestId : int
    {
        None = 0,
        ClearInst = 1,
        LineStylesDelete = 2,
        LineStylesRename = 3,
        LinePatternsDelete = 4,
        LinePatternsRename = 5,
    }


    public class RequestLineStyles
    {
        // Storing the value as a plain Int makes using the interlocking mechanism simpler
        private int m_request = (int)RequestId.None;

        /// <summary>
        ///   Take - The Idling handler calls this to obtain the latest request. 
        /// </summary>
        /// <remarks>
        ///   This is not a getter! It takes the request and replaces it
        ///   with 'None' to indicate that the request has been "passed on".
        /// </remarks>
        /// 
        public RequestId Take()
        {
            return (RequestId)Interlocked.Exchange(ref m_request, (int)RequestId.None);
        }

        /// <summary>
        ///   Make - The Dialog calls this when the user presses a command button there. 
        /// </summary>
        /// <remarks>
        ///   It replaces any older request previously made.
        /// </remarks>
        /// 
        public void Make(RequestId request)
        {
            Interlocked.Exchange(ref m_request, (int)request);
        }
    }
}
