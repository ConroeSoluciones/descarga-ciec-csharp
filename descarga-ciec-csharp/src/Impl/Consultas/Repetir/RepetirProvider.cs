using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using descarga_ciec_sdk.src.Impl.Consultas.Solicitar;
using descarga_ciec_sdk.src.Impl.Factories;
using descarga_ciec_sdk.src.Impl.Https;
using descarga_ciec_sdk.src.Interfaces;
using descarga_ciec_sdk.src.Models;
using descarga_ciec_sdk.src.Utils;
using Newtonsoft.Json;
using Polly;

namespace descarga_ciec_sdk.src.Impl.Consultas.Repetir
{
    public class RepetirProvider
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
        public RepetirProvider(
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
        public string Repetir(string folio, User user)
        {
            ResponseConsulta responseConsulta = null;
            //Response response = null;


            if (folio == null)
            {
                throw new System.Exception("El folio de la consulta no debe ser nulo");
            }

            var request = _requestCIECFactory.RepetirRequest(folio, user);

            // response = _iCIECUserAgent.enviar(request);
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
                //4. Deserializamos el Json.
                responseConsulta = JsonConvert.DeserializeObject<ResponseConsulta>(
                    response.Result.Json
                );

                //3. Validación
                if (response.Result.Code != 200)
                {
                    throw new Exception(
                        "Ocurrió un error al "
                            + "comunicarse con el servidor de descarga masiva."
                            + "Código del servidor: "
                            + response.Result.Code
                            + response.Result.Json
                    );
                }

                if (response.Result.Code == 401)
                {
                    throw new Exception($"Mensaje: {response.Result.Json}" + response.Result.Code);
                }
            }

            return responseConsulta.data.mensaje;
        }

        /// <summary>
        ///
        /// </summary>
        public async Task RepetirAsync(string folio, User user)
        {
            if (folio == null)
            {
                throw new System.Exception("El folio de la consulta no debe ser nulo");
            }

            var request = _requestCIECFactory.RepetirRequest(folio, user);

            var response = await _iCIECUserAgent.EnviarAsync(request);

            //3. Validación
            if (response.Code != 200)
            {
                throw new Exception(
                    "Ocurrió un error al "
                        + "comunicarse con el servidor de descarga masiva."
                        + "Código del servidor: "
                        + response.Code
                        + response.Json
                );
            }
        }
    }
}
