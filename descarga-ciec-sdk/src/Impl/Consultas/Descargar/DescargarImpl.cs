using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using descarga_ciec_sdk.src.Impl.Https;
using descarga_ciec_sdk.src.Interfaces;
using descarga_ciec_sdk.src.Models;
using descarga_ciec_sdk.src.Utils;
using Newtonsoft.Json;

namespace descarga_ciec_sdk.src.Impl.Consultas.Descargar
{
    public class DescargarImpl : IDescargarImpl
    {
        /// <summary>
        ///
        /// </summary>
        private ResponseProgreso _responseProgreso;

        /// <summary>
        ///
        /// </summary>
        private Response _response;

        /// <summary>
        ///
        /// </summary>
        private string _idConsulta;

        /// <summary>
        ///
        /// </summary>
        private IUserAgent _iCIECUserAgent;

        /// <summary>
        ///
        /// </summary>
        private IRequestFactory _requestCIECFactory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="responseProgreso"></param>
        public DescargarImpl(
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
                || status.Trim().Contains("REPETIR")
                || this.IsFallo()
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
                || this.IsFallo()
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
            string estado = "";

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

                estado = responseProgreso.estado;
            }

            return estado;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetStatusAsync()
        {
            string estado = "";
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

                estado = responseProgreso.estado;
            }

            return estado;
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
        public string GetPathZip()
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
        public int GetPaginas()
        {
            ValidarTerminada();

            var request = _requestCIECFactory.ResumenRequest(_idConsulta);

            var response = _iCIECUserAgent.Enviar(request);

            int totalResultados = 0;

            if (JsonValidator.IsValidJson(response.Json))
            {
                ResponseResumen responseResumen = JsonConvert.DeserializeObject<ResponseResumen>(
                    response.Json
                );

                totalResultados = Int32.Parse(responseResumen.paginas.ToString());
            }

            return totalResultados;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetPaginasAsync()
        {
            await ValidarTerminadaAsync();

            var request = _requestCIECFactory.ResumenRequest(_idConsulta);

            var response = _iCIECUserAgent.Enviar(request);
            int totalResultados = 0;

            if (JsonValidator.IsValidJson(response.Json))
            {
                ResponseResumen responseResumen = JsonConvert.DeserializeObject<ResponseResumen>(
                    response.Json
                );

                totalResultados = Int32.Parse(responseResumen.paginas.ToString());
            }

            return totalResultados;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public List<Metadata> GetTotalMetadata()
        {
            List<Metadata> listaMetadata = new List<Metadata>();

            ValidarTerminada();

            int paginas = GetPaginas();

            for (int pagina = 1; pagina <= paginas; pagina++)
            {
                var resultadosCFDI = GetResultados(pagina);

                foreach (var metadata in resultadosCFDI)
                {
                    listaMetadata.Add(metadata);
                }
            }

            return listaMetadata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public async Task<List<Metadata>> GetTotalMetadataAsync()
        {
            List<Metadata> listaMetadata = new List<Metadata>();

            await ValidarTerminadaAsync();

            int paginas = await GetPaginasAsync();

            var tasks = new List<Task>();
            tasks.Add(
                Task.Run(async () =>
                {
                    for (int pagina = 1; pagina <= paginas; pagina++)
                    {
                        var resultadosCFDI = await GetResultadosAsync(pagina);

                        foreach (var metadata in resultadosCFDI)
                        {
                            string formatoFecha = "dd-MM-yyyy HH:mm:ss";

                            metadata.folio = metadata.folio.ToUpper();

                            metadata.status = metadata.status;

                            metadata.tipo = metadata.tipo;

                            if (!string.IsNullOrWhiteSpace(metadata.fechaCancelacion))
                            {
                                metadata.fechaCancelacion = DateTime
                                    .Parse(metadata.fechaCancelacion)
                                    .ToString(formatoFecha);
                            }

                            if (!string.IsNullOrWhiteSpace(metadata.fechaCertificacion))
                            {
                                metadata.fechaCertificacion = DateTime
                                    .Parse(metadata.fechaCertificacion)
                                    .ToString(formatoFecha);
                            }

                            if (!string.IsNullOrWhiteSpace(metadata.fechaEmision))
                            {
                                metadata.fechaEmision = DateTime
                                    .Parse(metadata.fechaEmision)
                                    .ToString(formatoFecha);
                            }

                            listaMetadata.Add(metadata);
                        }
                    }
                })
            );

            await Task.WhenAll(tasks.ToArray());
            return listaMetadata;
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
            return lst;
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
        /// <param name="folioCFDI"></param>
        /// <returns></returns>
        public string GetCFDIXML()
        {
            //ValidarTerminada();

            var response = _response.Json;

            return response;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public Metadata GetMetadataXML()
        {
            //ValidarTerminada();

            Metadata metadata = JsonConvert.DeserializeObject<Metadata>(_response.Json);
            return metadata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="folioCFDI"></param>
        /// <returns></returns>
        public async Task<string> GetCFDIXMLAsync(string folioCFDI)
        {
            ValidarTerminada();

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
    }
}
