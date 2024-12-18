using System;
using System.Collections.Generic;
using System.Text;
using descarga_ciec_sdk.src.Impl.Https;
using descarga_ciec_sdk.src.Interfaces;
using descarga_ciec_sdk.src.Models;

namespace descarga_ciec_sdk.src.Impl.Consultas.Solicitar
{
    public class SolicitarImpl : ISolicitarImpl
    {
        /// <summary>
        ///
        /// </summary>
        private readonly ResponseConsulta _responseConsultaCIEC;

        /// <summary>
        ///
        /// </summary>
        private readonly Response _responseCIEC;

        /// <summary>
        ///
        /// </summary>
        /// <param name="responseConsultaCIEC"></param>
        /// <param name="responseCIEC"></param>
        /// <param name="userAgent"></param>
        /// <param name="requestCIECFactory"></param>
        public SolicitarImpl(
            ResponseConsulta responseConsultaCIEC,
            Response responseCIEC,
            IUserAgent userAgent = null,
            IRequestFactory requestCIECFactory = null
        )
        {
            _responseConsultaCIEC = responseConsultaCIEC;
            _responseCIEC = responseCIEC;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public string GetIDConsulta()
        {
            string idConsulta = "";

            if (_responseConsultaCIEC != null)
            {
                idConsulta = _responseConsultaCIEC.data.uuid;
            }

            return idConsulta;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public bool IsAutenticado()
        {
            bool isOK = false;

            if (_responseConsultaCIEC != null)
            {
                isOK = _responseConsultaCIEC.autenticado;
            }

            return isOK;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public ResponseConsulta GetResponseConsultaCIEC()
        {
            return _responseConsultaCIEC;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public int GetCodigo()
        {
            return _responseCIEC.Code;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public string GetMensaje()
        {
            return _responseCIEC.RawResponse;
        }
    }
}
