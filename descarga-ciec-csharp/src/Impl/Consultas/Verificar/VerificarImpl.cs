using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using descarga_ciec_sdk.src.Enums;
using descarga_ciec_sdk.src.Impl.Https;
using descarga_ciec_sdk.src.Interfaces;
using descarga_ciec_sdk.src.Models;
using descarga_ciec_sdk.src.Utils;
using Newtonsoft.Json;
using Polly;

namespace descarga_ciec_sdk.src.Impl.Consultas.Verificar
{
    public class VerificarImpl : IVerificarImpl
    {
        /// <summary>
        ///
        /// </summary>
        private readonly ResponseProgreso _responseProgreso;

        /// <summary>
        ///
        /// </summary>
        private readonly Response _response;

        /// <summary>
        ///
        /// </summary>
        private readonly string _idConsulta;

        /// <summary>
        ///
        /// </summary>
        private readonly IUserAgent _iCIECUserAgent;

        /// <summary>
        ///
        /// </summary>
        private readonly IRequestFactory _requestCIECFactory;

        /// <summary>
        ///
        /// </summary>
        private string _estado;

        /// <summary>
        ///
        /// </summary>
        private int _encontrado;

        /// <summary>
        ///
        /// </summary>
        private int _totalResultados;

        /// <summary>
        ///
        /// </summary>
        private IWebResponsePolicy _webReponsePolicy;

        /// <summary>
        ///
        /// </summary>
        private ConfiguracionPolly _configuracionPolly;

        /// <summary>
        ///
        /// </summary>
        /// <param name="responseProgreso"></param>
        public VerificarImpl(
            ResponseProgreso responseProgreso,
            Response response,
            string IdConsulta,
            IUserAgent iCIECUserAgent,
            IRequestFactory requestCIECFactory
        )
        {
            _responseProgreso = responseProgreso;
            _response = response;
            _idConsulta = IdConsulta;
            _iCIECUserAgent = iCIECUserAgent;
            _requestCIECFactory = requestCIECFactory;
            _webReponsePolicy = new WebResponsePolicy();
            _configuracionPolly = new ConfiguracionPolly();
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public string GetIdConsulta()
        {
            return _idConsulta;
        }

        /// <summary>
        ///
        /// </summary>
        protected void ValidarTerminada()
        {
            if (!IsTerminada())
            {
                throw new Exception("La consulta no ha terminado.");
            }
        }

        /// <summary>
        ///
        /// </summary>
        protected async Task ValidarTerminadaAsync()
        {
            if (!await IsTerminadaAsync())
            {
                throw new Exception("La consulta no ha terminado.");
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public bool IsTerminada()
        {
            return this.IsCompletado();
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public async Task<bool> IsTerminadaAsync()
        {
            return await this.IsCompletadoAsync();
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public bool IsCompletado()
        {
            string status = this.GetStatus();

            if (
                status.ToString() == EstadoConsulta.COMPLETADO
                || status.ToString() == EstadoConsulta.COMPLETADO_CON_FALTANTES
                || status.ToString() == EstadoConsulta.COMPLETADO_XML_FALTANTES
                || status.ToString() == EstadoConsulta.COMPLETADO_CON_FALTANTES_XMLS_NO_DISPONIBLES
                || status.Trim().Contains("COMPLETADO")
                || this.IsFallo()
                || status.ToString() == EstadoConsulta.REPETIR
            )
            {
                return true;
            }

            return false;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public async Task<bool> IsCompletadoAsync()
        {
            string status = await this.GetStatusAsync();

            if (
                status.ToString() == EstadoConsulta.COMPLETADO
                || status.ToString() == EstadoConsulta.COMPLETADO_CON_FALTANTES
                || status.ToString() == EstadoConsulta.COMPLETADO_XML_FALTANTES
                || status.ToString() == EstadoConsulta.COMPLETADO_CON_FALTANTES_XMLS_NO_DISPONIBLES
                || status.Trim().Contains("COMPLETADO")
                || await this.IsFalloAsync()
            )
            {
                return true;
            }

            return false;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public string GetStatus()
        {
            var request = _requestCIECFactory.VerificarRequest(_idConsulta);

            var response = _iCIECUserAgent.Enviar(request);

            if (response.Code == 404)
            {
                throw new Exception(
                    $"El recurso especificado no se ha encontrado {_idConsulta} " + response.Code
                );
            }

            if (response.Code != 200)
            {
                throw new Exception(
                    "Ocurrió un error al "
                        + "comunicarse con el servidor de descarga masiva."
                        + "Código del servidor: "
                        + response.Code
                );
            }

            if (JsonValidator.IsValidJson(response.Json))
            {
                ResponseProgreso responseProgreso = JsonConvert.DeserializeObject<ResponseProgreso>(
                    response.Json
                );


                _estado = responseProgreso.estado.ToString();
                _encontrado = responseProgreso.encontrados;
            }
            return _estado;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetStatusAsync()
        {
            PolicyResult<Response> response = null;

            var request = _requestCIECFactory.VerificarRequest(_idConsulta);

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
                    $"El recurso especificado no se ha encontrado {_idConsulta} "
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

            ResponseProgreso responseProgreso = JsonConvert.DeserializeObject<ResponseProgreso>(
                response.Result.Json
            );



            _estado = responseProgreso.estado.ToString();
            _encontrado = responseProgreso.encontrados;
            return _estado;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public bool IsFallo()
        {
            string falloStatus = this.GetStatus();

            switch (falloStatus)
            {
                case EstadoConsulta.FALLO:
                    return true;
                case EstadoConsulta.FALLO_AUTENTICACION:
                    return true;
                case EstadoConsulta.FALLO_500_MISMO_HORARIO:
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public async Task<bool> IsFalloAsync()
        {
            string falloStatus = await this.GetStatusAsync();

            switch (falloStatus)
            {
                case EstadoConsulta.FALLO:
                    return true;
                case EstadoConsulta.FALLO_AUTENTICACION:
                    return true;
                case EstadoConsulta.FALLO_500_MISMO_HORARIO:
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public bool IsRepetir()
        {
            if (this.GetStatus() == EstadoConsulta.REPETIR)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        protected IUserAgent GetUserAgent()
        {
            return _iCIECUserAgent;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        protected IRequestFactory GetRequestFactory()
        {
            return _requestCIECFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="pagina"></param>
        protected void ValidarResultadosSuficientes(int pagina)
        {
            if (!HasResultados() || pagina > GetPaginas())
            {
                throw new Exception(
                    "No existen suficientes "
                        + "resultados para mostrar, total páginas: "
                        + GetPaginas()
                );
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="pagina"></param>
        /// <returns></returns>
        protected async Task ValidarResultadosSuficientesAsync(int pagina)
        {
            if (!await HasResultadosAsync() || pagina > await GetPaginasAsync())
            {
                throw new Exception(
                    "No existen suficientes "
                        + "resultados para mostrar, total páginas: "
                        + GetPaginas()
                );
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public string getPathZip()
        {
            string pathZip = "";

            ValidarTerminada();

            Request request = _requestCIECFactory.DescargaZipRequest(_idConsulta);

            pathZip = request.Uri.AbsoluteUri;

            return pathZip;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public bool HasResultados()
        {
            return GetPaginas() > 0;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public async Task<bool> HasResultadosAsync()
        {
            return await GetPaginasAsync() > 0;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public ResponseResumen GetResponseResumen()
        {
            ValidarTerminada();

            var request = _requestCIECFactory.ResumenRequest(_idConsulta);

            var response = _iCIECUserAgent.Enviar(request);

            ResponseResumen responseResumen = JsonConvert.DeserializeObject<ResponseResumen>(
                response.Json
            );

            return responseResumen;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseResumen> GetResponseResumenAsync()
        {
            await ValidarTerminadaAsync();

            PolicyResult<Response> response = null;

            var request = _requestCIECFactory.ResumenRequest(_idConsulta);

            response = await _webReponsePolicy
                .GetPoliciesAsync(_configuracionPolly)
                .ExecuteAndCaptureAsync(() => _iCIECUserAgent.EnviarAsync(request));

            if (response.Outcome == OutcomeType.Failure && (response.FinalException is Exception))
            {
                throw response.FinalException;
            }

            ResponseResumen responseResumen = JsonConvert.DeserializeObject<ResponseResumen>(
                response.Result.Json
            );

            return responseResumen;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public List<string> GetFechaMismoHorario()
        {
            ValidarTerminada();

            var request = _requestCIECFactory.ResumenRequest(_idConsulta);

            var response = _iCIECUserAgent.Enviar(request);

            ResponseResumen responseResumen = JsonConvert.DeserializeObject<ResponseResumen>(
                response.Json
            );

            var fechasMismoHorario = responseResumen.fechasMismoHorario;

            return fechasMismoHorario;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> GetFechaMismoHorarioAsync()
        {
            ValidarTerminada();

            PolicyResult<Response> response = null;

            var request = _requestCIECFactory.ResumenRequest(_idConsulta);

            response = await _webReponsePolicy
                .GetPoliciesAsync(_configuracionPolly)
                .ExecuteAndCaptureAsync(() => _iCIECUserAgent.EnviarAsync(request));

            if (response.Outcome == OutcomeType.Failure && (response.FinalException is Exception))
            {
                throw response.FinalException;
            }

            ResponseResumen responseResumen = JsonConvert.DeserializeObject<ResponseResumen>(
                response.Result.Json
            );

            var fechasMismoHorario = responseResumen.fechasMismoHorario;

            return fechasMismoHorario;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public bool GetIsXMLFaltantes()
        {
            ValidarTerminada();
            bool xmlFaltantes = false;

            var request = _requestCIECFactory.ResumenRequest(_idConsulta);

            var response = _iCIECUserAgent.Enviar(request);

            if (JsonValidator.IsValidJson(response.Json))
            {
                ResponseResumen responseResumen = JsonConvert.DeserializeObject<ResponseResumen>(
                    response.Json
                );

                xmlFaltantes = responseResumen.xmlFaltantes;
            }

            return xmlFaltantes;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public async Task<bool> GetIsXMLFaltantesAsync()
        {
            await ValidarTerminadaAsync();

            PolicyResult<Response> response = null;

            bool xmlFaltantes = false;

            var request = _requestCIECFactory.ResumenRequest(_idConsulta);

            response = await _webReponsePolicy
                .GetPoliciesAsync(_configuracionPolly)
                .ExecuteAndCaptureAsync(() => _iCIECUserAgent.EnviarAsync(request));

            if (response.Outcome == OutcomeType.Failure && (response.FinalException is Exception))
            {
                throw response.FinalException;
            }

            if (JsonValidator.IsValidJson(response.Result.Json))
            {
                ResponseResumen responseResumen = JsonConvert.DeserializeObject<ResponseResumen>(
                    response.Result.Json
                );

                xmlFaltantes = responseResumen.xmlFaltantes;
            }

            return xmlFaltantes;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public int GetPaginas()
        {
            ValidarTerminada();
            var request = _requestCIECFactory.ResumenRequest(_idConsulta);

            var response = _iCIECUserAgent.Enviar(request);

            if (JsonValidator.IsValidJson(response.Json))
            {
                ResponseResumen responseResumen = JsonConvert.DeserializeObject<ResponseResumen>(
                    response.Json
                );

                _totalResultados = Int32.Parse(responseResumen.paginas.ToString());
            }

            return _totalResultados;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetPaginasAsync()
        {
            await ValidarTerminadaAsync();

            PolicyResult<Response> response = null;

            var request = _requestCIECFactory.ResumenRequest(_idConsulta);

            response = await _webReponsePolicy
                .GetPoliciesAsync(_configuracionPolly)
                .ExecuteAndCaptureAsync(() => _iCIECUserAgent.EnviarAsync(request));

            if (response.Outcome == OutcomeType.Failure && (response.FinalException is Exception))
            {
                throw response.FinalException;
            }

            if (JsonValidator.IsValidJson(response.Result.Json))
            {
                ResponseResumen responseResumen = JsonConvert.DeserializeObject<ResponseResumen>(
                    response.Result.Json
                );

                _totalResultados = Int32.Parse(responseResumen.paginas.ToString());
            }

            return _totalResultados;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public int GetTotalResultados()
        {
            ValidarTerminada();

            var request = _requestCIECFactory.ResumenRequest(_idConsulta);

            var response = _iCIECUserAgent.Enviar(request);

            if (JsonValidator.IsValidJson(response.Json))
            {
                ResponseResumen responseResumen = JsonConvert.DeserializeObject<ResponseResumen>(
                    response.Json
                );

                _totalResultados = Int32.Parse(responseResumen.total.ToString());
            }

            return _totalResultados;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetTotalResultadosAsync()
        {
            await ValidarTerminadaAsync();

            PolicyResult<Response> response = null;

            var request = _requestCIECFactory.ResumenRequest(_idConsulta);

            response = await _webReponsePolicy
                .GetPoliciesAsync(_configuracionPolly)
                .ExecuteAndCaptureAsync(() => _iCIECUserAgent.EnviarAsync(request));

            if (response.Outcome == OutcomeType.Failure && (response.FinalException is Exception))
            {
                throw response.FinalException;
            }

            if (JsonValidator.IsValidJson(response.Result.Json))
            {
                ResponseResumen responseResumen = JsonConvert.DeserializeObject<ResponseResumen>(
                    response.Result.Json
                );

                _totalResultados = Int32.Parse(responseResumen.total.ToString());
            }

            return _totalResultados;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        protected List<Metadata> NewResultadosList(Response response)
        {
            List<Metadata> lst = new List<Metadata>();
            try
            {
                if (response?.Json == null)
                {
                    throw new Exception("El response del json es nulo");
                }

                //LoggerService.LogDebug($"{response?.getAsJsonConsulta()}");

                List<Metadata> results = JsonConvert.DeserializeObject<List<Metadata>>(
                    response.Json
                );

                lst = results;
            }
            catch (Exception)
            {
                // LoggerService.LogError($"Error en mapear el json | {ex.Message}");
                throw;
            }
            return lst;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        protected Task<List<Metadata>> NewResultadosListAsync(Response response)
        {
            List<Metadata> lst = new List<Metadata>();
            try
            {
                if (response?.Json == null)
                {
                    throw new Exception("El response del json es nulo");
                }

                if (JsonValidator.IsValidJson(response.Json))
                {
                    List<Metadata> results = JsonConvert.DeserializeObject<List<Metadata>>(
                        response.Json
                    );
                    lst = results;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return Task.FromResult(lst);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="pagina"></param>
        /// <returns></returns>
        public List<Metadata> GetResultados(int pagina)
        {
            ValidarTerminada();
            ValidarResultadosSuficientes(pagina);

            List<Metadata> resultados = NewResultadosList(
                _iCIECUserAgent.Enviar(_requestCIECFactory.ResultadosRequest(_idConsulta, pagina))
            );

            return resultados;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="pagina"></param>
        /// <returns></returns>
        public async Task<List<Metadata>> GetResultadosAsync(int pagina)
        {
            await ValidarTerminadaAsync();
            await ValidarResultadosSuficientesAsync(pagina);

            List<Metadata> resultados = NewResultadosList(
                await _iCIECUserAgent.EnviarAsync(
                    _requestCIECFactory.ResultadosRequest(_idConsulta, pagina)
                )
            );

            return resultados;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="folioCFDI"></param>
        /// <returns></returns>
        public string GetCFDIXML(string folioCFDI)
        {
            ValidarTerminada();

            var response = GetUserAgent()
                .Enviar(GetRequestFactory().DescargaFolioRequest(folioCFDI));

            if (response.Code != 200)
            {
                throw new Exception("No se encontró el XML para el " + "folio: " + folioCFDI);
            }

            return response.RawResponse;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="folioCFDI"></param>
        /// <returns></returns>
        public async Task<string> GetCFDIXMLAsync(string folioCFDI)
        {
            await ValidarTerminadaAsync();

            var response = await GetUserAgent()
                .EnviarAsync(GetRequestFactory().DescargaRequest(_idConsulta, folioCFDI));

            if (response.Code != 200)
            {
                throw new Exception("No se encontró el XML para el " + "folio: " + folioCFDI);
            }

            return response.RawResponse;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public string GetEstado()
        {
            return this._estado;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public int GetEncontrado()
        {
          
             GetStatus();
            

            return _encontrado; 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ResponseProgreso GetReposnseProgreso()
        {

            return this._responseProgreso;
        }
    }
}
