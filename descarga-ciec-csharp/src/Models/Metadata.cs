using System;
using System.Collections.Generic;
using System.Text;

namespace descarga_ciec_sdk.src.Models
{
    public class Metadata
    {
        /// <summary>
        ///
        /// </summary>
        public string folio { get; set; }

        /// <summary>
        ///
        /// </summary>
        public EmpresaFiscal emisor { get; set; } = new EmpresaFiscal();

        /// <summary>
        ///
        /// </summary>
        public EmpresaFiscal receptor { get; set; } = new EmpresaFiscal();

        /// <summary>
        ///
        /// </summary>
        public Cancelacion cancelacion { get; set; } = new Cancelacion();

        /// <summary>
        ///
        /// </summary>
        public string fechaEmision { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string fechaCertificacion { get; set; }

        public string fechaCancelacion { get; set; }

        /// <summary>
        ///
        /// </summary>
        public EmpresaFiscal PACCertificador { get; set; }

        /// <summary>
        ///
        /// </summary>
        public double total { get; set; } = 0;

        /// <summary>
        ///
        /// </summary>
        public string tipo { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string status { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string url { get; set; }
    }
}
