using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using descarga_ciec_sdk.src.Enums;
using descarga_ciec_sdk.src.Models;

namespace descarga_ciec_sdk.src.Utils
{
    public class ConsultaUtil
    {
        /// <summary>
        ///
        /// </summary>
        public static string SERVICIO { get; } = "CRAPI";

        /// <summary>
        ///
        /// </summary>
        /// <param name="credenciales"></param>
        public static void validarCredencialesCS(User credenciales)
        {
            if (credenciales == null)
            {
                throw new Exception();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="credenciales"></param>
        public static void validarCredencialesSAT(Credenciales credenciales)
        {
            if (credenciales == null)
            {
                throw new Exception();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="rfc"></param>
        public static void validarRFC(string rfc)
        {
            if (rfc != null)
            {
                if (!(Regex.IsMatch(rfc, ExpresionRegular.EXPRESION_RFC)))
                {
                    throw new Exception();
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="tipo"></param>
        /// <returns></returns>
        public static string getBusquedaConsultaEnum(Movimiento tipo)
        {
            string value = "";

            switch (tipo)
            {
                case Movimiento.EMITIDAS:
                    value = "emitidas";
                    break;

                case Movimiento.RECIBIDAS:
                    value = "recibidas";
                    break;

                default:
                    value = "todos";
                    break;
            }
            return value;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="estatus"></param>
        /// <returns></returns>
        public static string getBusquedaStatusEnum(EstatusCFDI estatus)
        {
            string value = "";

            switch (estatus)
            {
                case EstatusCFDI.VIGENTE:
                    value = "vigentes";
                    break;

                case EstatusCFDI.CANCELADO:
                    value = "canceladas";
                    break;

                default:
                    value = "";
                    break;
            }
            return value;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="tipoDoc"></param>
        /// <returns></returns>
        public static string getBusquedaTipoDocEnum(TipoDocumento tipoDoc)
        {
            string value = "";

            switch (tipoDoc)
            {
                case TipoDocumento.CFDI:
                    value = "cfdi";
                    break;

                case TipoDocumento.RETENCION:
                    value = "retencion";
                    break;
                default:
                    value = "";
                    break;
            }
            return value;
        }
    }
}
