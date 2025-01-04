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

namespace descarga_ciec_sdk.src.Impl.Consultas.Solicitar
{
    public class SolicitarProvider : ISolicitarProvider
    {
        /// <summary>
        ///
        /// </summary>
        private readonly IUserAgent _UserAgent;

        /// <summary>
        ///
        /// </summary>
        private readonly IRequestFactory _requestCIECFactory;

        /// <summary>
        /// WebRequestPolicy
        /// </summary>
        private IWebResponsePolicy _webReponsePolicy;

        /// <summary>
        /// ConfiguracionPolly
        /// </summary>
        private ConfiguracionPolly _configuracionPolly;

        /// <summary>
        ///
        /// Initializes a new instance of the <see cref="SolicitarProvider"/> class.
        /// </summary>
        /// <param name="userAgent"></param>
        /// <param name="requestCIECFactory"></param>
        public SolicitarProvider(
            UserAgent userAgent = null,
            IRequestFactory requestCIECFactory = null
        )
        {
            _UserAgent = userAgent ?? new UserAgent();
            _requestCIECFactory = requestCIECFactory ?? new RequestFactory();
            this._webReponsePolicy = new WebResponsePolicy();
            this._configuracionPolly = new ConfiguracionPolly();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="parametrosCS"></param>
        /// <returns></returns>
        /// <inheritdoc/>
        public ISolicitarImpl Solicitar(ConsultaParametros parametrosCS)
        {
            string folio = "";
            ResponseConsulta responseConsulta = null;

            try
            {
                // 1. Validaciones
                if (parametrosCS == null)
                {
                    throw new System.ArgumentNullException(
                        "Los parametros para hacer la consulta " + "al WS de la CIEC son necesarios"
                    );
                }

                // Son necesarias las credenciales de CS
                if (parametrosCS?.getCredencialesCS() == null)
                {
                    throw new System.ArgumentNullException("Las credenciales de CS son requeridas");
                }

                // Son necesarias las credenciales del SAT
                if (parametrosCS?.getCredencialesSAT() == null)
                {
                    throw new System.ArgumentNullException(
                        "Las credenciales de la empresa(SAT) son requeridas"
                    );
                }

                // 2. Generamos los datos necesario para hacer la solicitud
                var request = this._requestCIECFactory.SolicitudRequest(
                    parametrosCS?.getCredencialesCS(),
                    parametrosCS?.getCredencialesSAT(),
                    parametrosCS
                );

                // 3. Enviamos los datos y obtenemos el response
                //  response = _UserAgent.enviar(request);

                // 2.User Agent
                var response = this
                    ._webReponsePolicy.GetPolicies(this._configuracionPolly)
                    .ExecuteAndCapture(() => this._UserAgent.Enviar(request));

                if (
                    response.Outcome == OutcomeType.Failure
                    && (response.FinalException is Exception)
                )
                {
                    throw response.FinalException;
                }

                if (JsonValidator.IsValidJson(response.Result.Json))
                {
                    // 4. Deserializamos el Json.
                    responseConsulta = JsonConvert.DeserializeObject<ResponseConsulta>(
                        response.Result.Json
                    );

                    if (responseConsulta.error != null)
                    {
                        throw new Exception(responseConsulta.error);
                    }

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
                        throw new Exception(
                            $"Mensaje: {response.Result.Json}" + response.Result.Code
                        );
                    }
                }

                if (responseConsulta?.data != null)
                {
                    if (!string.IsNullOrWhiteSpace(responseConsulta?.data?.error))
                    {
                        throw new Exception(responseConsulta.data.error);
                    }

                    if (responseConsulta?.data?.uuid == null)
                    {
                        folio = responseConsulta?.data?.idConsulta;
                    }
                    else
                    {
                        folio = responseConsulta?.data?.uuid;

                        if (!responseConsulta.data.contratacion)
                        {
                            string msg =
                                responseConsulta.data.mensaje.Length < 0
                                    ? responseConsulta.data.mensaje
                                    : "No cuentas con una contratación disponible";

                            throw new Exception(msg);
                        }
                    }

                    if (string.IsNullOrEmpty(folio))
                    {
                        string msg =
                            responseConsulta.data.mensaje.Length < 0
                                ? responseConsulta.data.mensaje
                                : "Error en el Web Service, no se devolvió el ID de consulta.";

                        throw new Exception(msg);
                    }
                }

                return new SolicitarImpl(
                    responseConsulta,
                    response.Result,
                    _UserAgent,
                    _requestCIECFactory
                );
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name=""></param>
        public ISolicitarImpl SolicitarFolios(ConsultaParametros parametrosCS)
        {
            string folio = "";
            ResponseConsulta responseConsulta = null;
            Response response = null;

            try
            {
                //1. Validaciones
                if (parametrosCS == null)
                {
                    throw new System.ArgumentNullException(
                        "Los parametros para hacer la consulta " + "al WS de la CIEC son necesarios"
                    );
                }

                //Son necesarias las credenciales de CS
                if (parametrosCS?.getCredencialesCS() == null)
                {
                    throw new System.ArgumentNullException("Las credenciales de CS son requeridas");
                }

                //Son necesarias las credenciales del SAT
                if (parametrosCS?.getCredencialesSAT() == null)
                {
                    throw new System.ArgumentNullException(
                        "Las credenciales de la empresa(SAT) son requeridas"
                    );
                }

                //2. Generamos los datos necesario para hacer la solicitud
                var request = _requestCIECFactory.SolicitudFoliosRequest(
                    parametrosCS?.getCredencialesCS(),
                    parametrosCS?.getCredencialesSAT(),
                    parametrosCS
                );

                //3. Enviamos los datos y obtenemos el response
                response = _UserAgent.Enviar(request);

                //4. Deserializamos el Json.
                responseConsulta = JsonConvert.DeserializeObject<ResponseConsulta>(response.Json);

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

                if (!string.IsNullOrWhiteSpace(responseConsulta?.data?.error))
                {
                    throw new Exception(responseConsulta.data.error);
                }

                if (responseConsulta.data.uuid == null)
                {
                    folio = responseConsulta.data.idConsulta;
                }
                else
                {
                    folio = responseConsulta.data.uuid;
                    if (!responseConsulta.data.contratacion)
                    {
                        string msg =
                            responseConsulta.data.mensaje.Length < 0
                                ? responseConsulta.data.mensaje
                                : "No cuentas con una contratación disponible";

                        throw new Exception(msg);
                    }
                }

                if (string.IsNullOrEmpty(folio))
                {
                    string msg =
                        responseConsulta.data.mensaje.Length < 0
                            ? responseConsulta.data.mensaje
                            : "Error en el Web Service, no se devolvió el ID de consulta.";

                    throw new Exception(msg);
                }
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }

            return new SolicitarImpl(responseConsulta, response, _UserAgent, _requestCIECFactory);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name=""></param>
        public ResponseResumen SolicitarSummary(string IdConsulta)
        {
            ResponseResumen responseResumen = null;
            Response response = null;

            try
            {
                //1. Validaciones
                if (string.IsNullOrWhiteSpace(IdConsulta))
                {
                    throw new System.Exception("El Id de la consulta es requerido");
                }

                //2. Generamos los datos necesario para hacer la solicitud
                var request = _requestCIECFactory.NewResumenRequest(IdConsulta);

                //3. Enviamos los datos y obtenemos el response
                response = _UserAgent.Enviar(request);

                //4. Deserializamos el Json.
                responseResumen = JsonConvert.DeserializeObject<ResponseResumen>(response.Json);

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
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }

            return responseResumen;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name=""></param>
        public async Task<ISolicitarImpl> SolicitarAsync(ConsultaParametros parametrosCS)
        {
            string folio = "";
            ResponseConsulta responseConsulta = null;
            PolicyResult<Response> response = null;

            try
            {
                //1. Validaciones
                if (parametrosCS == null)
                {
                    throw new System.ArgumentNullException(
                        "Los parametros para hacer la consulta " + "al WS de la CIEC son necesarios"
                    );
                }

                //Son necesarias las credenciales de CS
                if (parametrosCS?.getCredencialesCS() == null)
                {
                    throw new System.ArgumentNullException("Las credenciales de CS son requeridas");
                }

                //Son necesarias las credenciales del SAT
                if (parametrosCS?.getCredencialesSAT() == null)
                {
                    throw new System.ArgumentNullException(
                        "Las credenciales de la empresa(SAT) son requeridas"
                    );
                }

                //2. Generamos los datos necesario para hacer la solicitud
                var request = _requestCIECFactory.SolicitudRequest(
                    parametrosCS?.getCredencialesCS(),
                    parametrosCS?.getCredencialesSAT(),
                    parametrosCS
                );

                //3. Enviamos los datos y obtenemos el response

                response = await _webReponsePolicy
                    .GetPoliciesAsync(_configuracionPolly)
                    .ExecuteAndCaptureAsync(() => _UserAgent.EnviarAsync(request));

                if (
                    response.Outcome == OutcomeType.Failure
                    && (response.FinalException is Exception)
                )
                {
                    throw response.FinalException;
                }

                //4. Deserializamos el Json.
                responseConsulta = JsonConvert.DeserializeObject<ResponseConsulta>(
                    response.Result.Json
                );

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

                if (!string.IsNullOrWhiteSpace(responseConsulta?.data?.error))
                {
                    throw new Exception(responseConsulta.data.error);
                }

                if (responseConsulta.data.uuid == null)
                {
                    folio = responseConsulta.data.idConsulta;
                }
                else
                {
                    folio = responseConsulta.data.uuid;

                    if (!responseConsulta.data.contratacion)
                    {
                        string msg =
                            responseConsulta.data.mensaje.Length < 0
                                ? responseConsulta.data.mensaje
                                : "No cuentas con una contratación disponible";

                        throw new Exception(msg);
                    }
                }

                if (string.IsNullOrEmpty(folio))
                {
                    string msg =
                        responseConsulta.data.mensaje.Length < 0
                            ? responseConsulta.data.mensaje
                            : "Error en el Web Service, no se devolvió el ID de consulta.";

                    throw new Exception(msg);
                }
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }

            return new SolicitarImpl(
                responseConsulta,
                response.Result,
                _UserAgent,
                _requestCIECFactory
            );
        }
    }
}
