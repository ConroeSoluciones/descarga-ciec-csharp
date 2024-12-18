using System;
using System.Collections.Generic;
using System.Text;

namespace descarga_ciec_sdk.src.Models
{
    public class UriRequest
    {
        /// <summary>
        ///
        /// </summary>
        public string wsSchema { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string wsHost { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string wsPath { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int wsPort { get; set; } = 0;

        /// <summary>
        ///
        /// </summary>
        public string wsMethod { get; set; }
    }
}
