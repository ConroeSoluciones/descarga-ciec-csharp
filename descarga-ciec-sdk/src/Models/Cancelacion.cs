using System;
using System.Collections.Generic;
using System.Text;

namespace descarga_ciec_sdk.src.Models
{
    public class Cancelacion
    {
        /// <summary>
        ///
        /// </summary>
        public string motivo { get; set; } = "";

        /// <summary>
        ///
        /// </summary>
        public string cancelable { get; set; } = "";

        /// <summary>
        ///
        /// </summary>
        public string status { get; set; } = "";

        /// <summary>
        ///
        /// </summary>
        public string fecha { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string uuidSustitucion { get; set; } = "";
    }
}
