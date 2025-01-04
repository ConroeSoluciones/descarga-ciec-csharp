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

namespace descarga_ciec_sdk.src.Impl.Consultas.Verificar
{
    public class VerificarProvider : IVerificarProvider
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
        public VerificarProvider(
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
        /// <param name="IDConsulta"></param>
        /// <param name="parametrosCS"></param>
        public IVerificarImpl Verificar(string IDConsulta)
        {
            ResponseProgreso responseProgreso = null;

            var request = _requestCIECFactory.VerificarRequest(IDConsulta);
            //2.User Agent
            var response = _webReponsePolicy
                .GetPolicies(_configuracionPolly)
                .ExecuteAndCapture(() => _iCIECUserAgent.Enviar(request));

            if (response.Outcome == OutcomeType.Failure && (response.FinalException is Exception))
            {
                throw response.FinalException;
            }

            if (response.Result.Code == 404)
            {
                throw new Exception(
                    $"El recurso especificado no se ha encontrado {IDConsulta} "
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

            if (JsonValidator.IsValidJson(response.Result.Json))
            {
                responseProgreso = JsonConvert.DeserializeObject<ResponseProgreso>(
                    response.Result.Json
                );
            }

            return new VerificarImpl(
                responseProgreso,
                response.Result,
                IDConsulta,
                _iCIECUserAgent,
                _requestCIECFactory
            );
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="IDConsulta"></param>
        /// <param name="parametrosCS"></param>
        public async Task<IVerificarImpl> VerificarAsync(string IDConsulta)
        {
            ResponseProgreso responseProgreso = null;
            PolicyResult<Response> response = null;

            var request = _requestCIECFactory.VerificarRequest(IDConsulta);

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
                    $"El recurso especificado no se ha encontrado {IDConsulta} "
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

            if (JsonValidator.IsValidJson(response.Result.Json))
            {
                responseProgreso = JsonConvert.DeserializeObject<ResponseProgreso>(
                    response.Result.Json
                );
            }

            return new VerificarImpl(
                responseProgreso,
                response.Result,
                IDConsulta,
                _iCIECUserAgent,
                _requestCIECFactory
            );
        }
    }
}
