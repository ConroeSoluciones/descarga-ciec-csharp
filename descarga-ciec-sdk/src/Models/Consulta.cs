using System;
using System.Collections.Generic;
using System.Text;

namespace descarga_ciec_sdk.src.Models
{
    public class Consulta
    {
        /// <summary>
        ///
        /// </summary>
        public string servicio { get; set; }

        /// <summary>
        ///
        /// </summary>
        public Descarga descarga { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string rfcContribuyente { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string rfc { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string password { get; set; }

        /// <summary>
        ///
        /// </summary>
        public List<string> folios { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string uuid { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string idContratacion { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string fechaInicio { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string fechaFin { get; set; }

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
        public string rfcBusqueda { get; set; }

        /// <summary>
        ///
        /// </summary>
        public bool metadata { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string complementos { get; set; }
    }
}
