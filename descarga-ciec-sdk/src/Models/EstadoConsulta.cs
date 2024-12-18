using System;
using System.Collections.Generic;
using System.Text;

namespace descarga_ciec_sdk.src.Models
{
    public class EstadoConsulta
    {
        /// <summary>
        /// EN_ESPERA
        /// </summary>
        public const string EN_ESPERA = "EN_ESPERA";

        /// <summary>
        /// EN_PROCESO
        /// </summary>
        public const string EN_PROCESO = "EN_PROCESO";

        /// <summary>
        /// DESCARGANDO
        /// </summary>
        public const string DESCARGANDO = "DESCARGANDO";

        /// <summary>
        /// FALLO_AUTENTICACION
        /// </summary>
        public const string FALLO_AUTENTICACION = "FALLO_AUTENTICACION";

        /// <summary>
        /// FALLO_500_MISMO_HORARIO
        /// </summary>
        public const string FALLO_500_MISMO_HORARIO = "FALLO_500_MISMO_HORARIO";

        /// <summary>
        /// FALLO
        /// </summary>
        public const string FALLO = "FALLO";

        /// <summary>
        /// COMPLETADO
        /// </summary>
        public const string COMPLETADO = "COMPLETADO";

        /// <summary>
        /// COMPLETADO_CON_FALTANTES
        /// </summary>
        public const string COMPLETADO_CON_FALTANTES = "COMPLETADO_CON_FALTANTES";

        /// <summary>
        /// COMPLETADO_XML_FALTANTES
        /// </summary>
        public const string COMPLETADO_XML_FALTANTES = "COMPLETADO_XML_FALTANTES";

        /// <summary>
        /// COMPLETADO_CON_FALTANTES_XMLS_NO_DISPONIBLES
        /// </summary>
        public const string COMPLETADO_CON_FALTANTES_XMLS_NO_DISPONIBLES =
            "COMPLETADO_CON_FALTANTES_XMLS_NO_DISPONIBLES";

        /// <summary>
        /// REPETIR
        /// </summary>
        public const string REPETIR = "REPETIR";
    }
}
