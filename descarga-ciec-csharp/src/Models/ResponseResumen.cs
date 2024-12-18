using System;
using System.Collections.Generic;
using System.Text;

namespace descarga_ciec_sdk.src.Models
{
    public class ResponseResumen
    {
        /// <summary>
        ///
        /// </summary>
        public int total { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int paginas { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int cancelados { get; set; }

        /// <summary>
        ///
        /// </summary>
        public bool xmlFaltantes { get; set; }

        /// <summary>
        ///
        /// </summary>
        public List<string> fechasMismoHorario { get; set; }
    }
}
