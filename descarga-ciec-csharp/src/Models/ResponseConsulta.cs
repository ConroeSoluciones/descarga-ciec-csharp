using System;
using System.Collections.Generic;
using System.Text;

namespace descarga_ciec_sdk.src.Models
{
    public class ResponseConsulta
    {
        /// <summary>
        ///
        /// </summary>
        public Data data { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string idConsulta { get; set; }

        /// <summary>
        ///
        /// </summary>
        public bool autenticado { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string error { get; set; }
    }
}
