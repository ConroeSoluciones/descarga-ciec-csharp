using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using descarga_ciec_sdk.src.Impl.Factories;
using descarga_ciec_sdk.src.Impl.Https;
using descarga_ciec_sdk.src.Interfaces;
using descarga_ciec_sdk.src.Models;
using descarga_ciec_sdk.src.Utils;
using Newtonsoft.Json;
using Polly;

namespace descarga_ciec_sdk.src.Impl.Consultas.Descargar
{
    public class DescargarProvider : IDescargarProvider
    {
        /// <summary>
        ///
        /// </summary>
        private readonly IUserAgent _iCIECUserAgent;

        /// <summary>
        ///
        /// </summary>
        private readonly IRequestFactory _requestCIECFactory;

        /// <summary>
        /// WebRequestPolicy
        /// </summary>
        private IWebResponsePolicy _webReponsePolicy;

        /// <summary>
        /// rest option
        /// </summary>
        private ConfiguracionPolly _configuracionPolly;

        /// <summary>
        ///
        /// </summary>
        /// <param name="iCIECUserAgent"></param>
        /// <param name="requestCIECFactory"></param>
        public DescargarProvider(
            IUserAgent iCIECUserAgent = null,
            IRequestFactory requestCIECFactory = null
        )
        {
            _iCIECUserAgent = iCIECUserAgent ?? new UserAgent();
            _requestCIECFactory = requestCIECFactory ?? new RequestFactory();
            _webReponsePolicy = new WebResponsePolicy();
            _configuracionPolly = new ConfiguracionPolly();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idConsulta"></param>
        /// <param name="parametrosCS"></param>
        public IDescargarImpl Descargar(string idConsulta)
        {
            ResponseProgreso responseConsulta = null;

            var request = _requestCIECFactory.VerificarRequest(idConsulta);

            //2.User Agent
            var response = _webReponsePolicy
                .GetPolicies(_configuracionPolly)
                .ExecuteAndCapture(() => _iCIECUserAgent.Enviar(request));

            if (response.Outcome == OutcomeType.Failure && (response.FinalException is Exception))
            {
                throw response.FinalException;
            }

            if (JsonValidator.IsValidJson(response.Result.Json))
            {
                if (response.Result.Code == 404)
                {
                    throw new Exception(
                        $"El recurso especificado no se ha encontrado {idConsulta} "
                            + response.Result.Code
                    );
                }

                if (response.Result.Code != 200)
                {
                    throw new Exception(
                        "Ocurrió un error al "
                            + "comunicarse con el servidor de descarga masiva."
                            + "Código del servidor: "
                            + response.Result.Code
                    );
                }

                responseConsulta = JsonConvert.DeserializeObject<ResponseProgreso>(
                    response.Result.Json
                );
            }

            return new DescargarImpl(
                responseConsulta,
                response.Result,
                idConsulta,
                _iCIECUserAgent,
                _requestCIECFactory
            );
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idConsulta"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public IDescargarImpl DescargarXml(string folio)
        {
            ResponseProgreso responseConsulta = null;

            var request = _requestCIECFactory.DescargaFolioRequest(folio);

            //2.User Agent
            var response = _webReponsePolicy
                .GetPolicies(_configuracionPolly)
                .ExecuteAndCapture(() => _iCIECUserAgent.EnviarFolio(request));

            if (response.Outcome == OutcomeType.Failure && (response.FinalException is Exception))
            {
                throw response.FinalException;
            }

            if (JsonValidator.IsValidJson(response.Result.Json))
            {
                if (response.Result.Code == 404)
                {
                    throw new Exception(
                        $"El recurso especificado no se ha encontrado {folio} "
                            + response.Result.Code
                    );
                }

                if (response.Result.Code != 200)
                {
                    throw new Exception(
                        "Ocurrió un error al "
                            + "comunicarse con el servidor de descarga masiva."
                            + "Código del servidor: "
                            + response.Result.Code
                    );
                }

                responseConsulta = JsonConvert.DeserializeObject<ResponseProgreso>(
                    response.Result.Json
                );
            }

            //if (responseConsulta.estado.ToString() == "REPETIR")
            //{
            //    throw new Exception($"Por el momento no podemos repetir la consulta {idConsulta}");
            //}

            return new DescargarImpl(
                responseConsulta,
                response.Result,
                folio,
                _iCIECUserAgent,
                _requestCIECFactory
            );
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="folio"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public IDescargarImpl DescargarMetadataXml(string folio)
        {
            ResponseProgreso responseConsulta = null;

            var request = _requestCIECFactory.DescargaFolioRequest(folio);

            //2.User Agent
            var response = _webReponsePolicy
                .GetPolicies(_configuracionPolly)
                .ExecuteAndCapture(() => _iCIECUserAgent.Enviar(request));

            if (response.Outcome == OutcomeType.Failure && (response.FinalException is Exception))
            {
                throw response.FinalException;
            }

            if (JsonValidator.IsValidJson(response.Result.Json))
            {
                if (response.Result.Code == 404)
                {
                    throw new Exception(
                        $"El recurso especificado no se ha encontrado {folio} "
                            + response.Result.Code
                    );
                }

                if (response.Result.Code != 200)
                {
                    throw new Exception(
                        "Ocurrió un error al "
                            + "comunicarse con el servidor de descarga masiva."
                            + "Código del servidor: "
                            + response.Result.Code
                    );
                }

                responseConsulta = JsonConvert.DeserializeObject<ResponseProgreso>(
                    response.Result.Json
                );
            }

            //if (responseConsulta.estado.ToString() == "REPETIR")
            //{
            //    throw new Exception($"Por el momento no podemos repetir la consulta {idConsulta}");
            //}

            return new DescargarImpl(
                responseConsulta,
                response.Result,
                folio,
                _iCIECUserAgent,
                _requestCIECFactory
            );
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idConsulta"></param>
        /// <param name="parametrosCS"></param>
        public async Task<IDescargarImpl> DescargarAsync(string idConsulta)
        {
            ResponseProgreso responseConsulta = null;

            PolicyResult<Response> response = null;

            var request = _requestCIECFactory.VerificarRequest(idConsulta);

            //2.User Agent
            response = await _webReponsePolicy
                .GetPoliciesAsync(_configuracionPolly)
                .ExecuteAndCaptureAsync(() => _iCIECUserAgent.EnviarAsync(request));

            if (response.Outcome == OutcomeType.Failure && (response.FinalException is Exception))
            {
                throw response.FinalException;
            }

            if (response.Result.Code == 404)
            {
                throw new Exception(
                    $"El recurso especificado no se ha encontrado {idConsulta} "
                        + response.Result.Code
                );
            }

            if (response.Result.Code != 200)
            {
                throw new Exception(
                    "Ocurrió un error al "
                        + "comunicarse con el servidor de descarga masiva."
                        + "Código del servidor: "
                        + response.Result.Code
                );
            }

            responseConsulta = JsonConvert.DeserializeObject<ResponseProgreso>(
                response.Result.Json
            );

            //if (responseConsulta.estado.ToString() == "REPETIR")
            //{
            //    throw new Exception($"Por el momento no podemos repetir la consulta {idConsulta}");
            //}

            return new DescargarImpl(
                responseConsulta,
                response.Result,
                idConsulta,
                _iCIECUserAgent,
                _requestCIECFactory
            );
        }
    }
}
