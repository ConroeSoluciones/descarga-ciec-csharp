using System;
using System.Collections.Specialized;
using System.Net.Http;
using System.Text;
using System.Web;
using descarga_ciec_sdk.src.Enums;
using descarga_ciec_sdk.src.Impl.Https;
using descarga_ciec_sdk.src.Interfaces;
using descarga_ciec_sdk.src.Models;
using descarga_ciec_sdk.src.Utils;
using Mono.Web;

namespace descarga_ciec_sdk.src.Impl.Factories
{
    public class RequestFactory : IRequestFactory
    {
        /// <summary>
        ///
        /// </summary>
        public static string HOST_DEFAULT = "www.csfacturacion.com";

        /// <summary>
        ///
        /// </summary>
        public static string PATH_DEFAULT = "/webservices/csdescargasat/";

        /// <summary>
        ///
        /// </summary>
        public static string SCHEMA_DEFAULT = "https";

        /// <summary>
        ///
        /// </summary>
        public static int PORT_DEFAULT;

        /// <summary>
        ///
        /// </summary>
        private string wsSchema;

        /// <summary>
        ///
        /// </summary>
        private string wsHost;

        /// <summary>
        ///
        /// </summary>
        private string wsPath;

        /// <summary>
        ///
        /// </summary>
        private int wsPort;

        /// <summary>
        ///
        /// </summary>
        private string wsMethod = "";

        /// <summary>
        /// 
        /// </summary>
        //private MediaType mediatType=MediaType.JSON;

        /// <summary>
        ///
        /// </summary>
        /// <param name="wsSchema"></param>
        /// <param name="wsHost"></param>
        /// <param name="wsPath"></param>
        public RequestFactory(
            string wsSchema = null,
            string wsHost = null,
            string wsPath = null,
            int wsPort = 0,
            string wsMethod = null
        )
        {
            this.wsSchema = wsSchema == null ? SCHEMA_DEFAULT : wsSchema;
            this.wsHost = wsHost == null ? HOST_DEFAULT : wsHost;
            this.wsPath = wsPath == null ? PATH_DEFAULT : wsPath;
            this.wsPort = wsPort == 0 ? PORT_DEFAULT : wsPort;
            this.wsMethod = wsMethod;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        protected UriBuilder NewBaseURIBuilder()
        {
            return NewBaseURIBuilder("");
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        protected UriBuilder NewBaseURIBuilder(
            string path,
            string wsSchema = null,
            string wsHost = null
        )
        {
            UriBuilder uriBuilder = new UriBuilder();
            uriBuilder.Scheme = wsSchema == null ? this.wsSchema : wsSchema;
            uriBuilder.Host = wsHost == null ? this.wsHost : wsHost;

            if ((this.wsHost != "www.csfacturacion.com"))
            {
                uriBuilder.Port = wsPort;
                uriBuilder.Path = path;
            }
            else
            {
                uriBuilder.Path = wsPath + path;
            }

            return uriBuilder;
        }

        /// <summary>
        /// TODO:: Tenemos que simplificar el método.
        /// </summary>
        /// <param name="csCredenciales"></param>
        /// <param name="satCredenciales"></param>
        /// <param name="parametros"></param>
        /// <returns></returns>
        public Request NewConsultaRequest(
            User csCredenciales,
            Credenciales satCredenciales,
            ConsultaParametros parametros
        )
        {
            NameValueCollection parametrosEncriptar = null;

            HttpMethod methodRequest = HttpMethod.Post;
            //mediatType = MediaType.JSON;

            NameValueCollection parameters = new NameValueCollection();

            Consulta consulta = null;

            UriBuilder uriBuilder = NewBaseURIBuilder();
            uriBuilder.Scheme = this.wsSchema;
            uriBuilder.Host = this.wsHost;
            uriBuilder.Path = this.wsPath + "/" + wsMethod;

            UriBuilder uriBuilder2 = NewBaseURIBuilder();
            uriBuilder2.Scheme = this.wsSchema;
            uriBuilder2.Host = this.wsHost;
            uriBuilder2.Path = this.wsPath + "/" + wsMethod;

            parameters = HttpUtility.ParseQueryString(uriBuilder.Query);

            if (parametros.getFolios().Count == 0)
            {
                bool isMetada = false;

                if (parametros.getIsMetadata())
                {
                    isMetada = true;
                }

                consulta = new Consulta()
                {
                    servicio = ConsultaUtil.SERVICIO,
                    descarga = new Descarga()
                    {
                        rfcContribuyente = satCredenciales.getRFC().ToString(),
                        password = satCredenciales.getPassword(),
                        fechaInicio = parametros.getFechaInicio(),
                        fechaFin = parametros.getFechaFin(),
                        tipo = ConsultaUtil.getBusquedaConsultaEnum(parametros.getTipo()),
                        tipoDoc = ConsultaUtil.getBusquedaTipoDocEnum(parametros.getTipoDoc()),
                        status = ConsultaUtil.getBusquedaStatusEnum(parametros.getEstatusCFDI()),
                        complementos = parametros.getComplemento(),
                        rfcBusqueda = (
                            parametros.getSearch() != null && parametros.getSearch().Length > 0
                                ? parametros.getSearch()
                                : ""
                        ),
                        metadata = isMetada,
                    },
                    rfcContribuyente = csCredenciales.RFC.ToString(),
                    password = csCredenciales.getPassword(),
                };

                uriBuilder.Scheme = this.wsSchema;
                uriBuilder.Host = this.wsHost;
                uriBuilder.Path = this.wsPath + "v3" + "/" + "cfdis";
            }
            else
            {
                if (!(parametros.getTipoPeticion().ToString() == "FORM_URLENCODED"))
                {
                    uriBuilder.Port = wsPort;
                }

                consulta = new Consulta();
                consulta.rfcContribuyente = csCredenciales.RFC.ToString();
                consulta.password = csCredenciales.getPassword();
                consulta.folios = parametros.getFolios();

                if (parametros.getTipoPeticion().ToString() == "FORM_URLENCODED")
                {
                    methodRequest = HttpMethod.Post;
                    //= MediaType.JSON;

                    uriBuilder.Scheme = this.wsSchema;
                    uriBuilder.Host = this.wsHost;
                    uriBuilder.Path = this.wsPath + "v3" + "/" + "cfdiFolios";

                    consulta.rfcContribuyente = csCredenciales.RFC.ToString();
                    consulta.password = csCredenciales.getPassword();
                    consulta.servicio = ConsultaUtil.SERVICIO;
                    consulta.descarga = new Descarga()
                    {
                        rfcContribuyente = satCredenciales.getRFC().ToString(),
                        password = satCredenciales.getPassword(),
                        folios = parametros.getFolios(),
                    };
                }
            }

            parametrosEncriptar = new NameValueCollection();
            parametrosEncriptar = HttpUtility.ParseQueryString(uriBuilder2.Query);
            parametrosEncriptar.Add(parameters);
            parametrosEncriptar.Set("cpassContrato", csCredenciales.getPassword());
            parametrosEncriptar.Set("cPassword", csCredenciales.getRFC());

            uriBuilder.Query = parameters.ToString();
            uriBuilder2.Query = parametrosEncriptar.ToString();

            Request request = new Request(new Uri(uriBuilder.ToString()), methodRequest);

            return request;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="folio"></param>
        /// <param name="rfcContratacion"></param>
        /// <param name="idContratato"></param>
        /// <returns></returns>
        public Request NewContabilizar(
            string folio,
            string rfcContratacion,
            string idContratato,
            User csCredenciales
        )
        {
            var uriBuilder = NewBaseURIBuilder();

            NameValueCollection parameters = new NameValueCollection();
            parameters = HttpUtility.ParseQueryString(uriBuilder.Query);

            uriBuilder.Scheme = this.wsSchema;
            uriBuilder.Host = this.wsHost;
            uriBuilder.Path = this.wsPath + "v3" + "/" + "contar";
            var consulta = new Consulta()
            {
                servicio = "CSRn",
                rfcContribuyente = csCredenciales.getRFC().ToString(),
                password = csCredenciales.getPassword(),
                uuid = folio,
                idContratacion = idContratato,
            };

            Request request = new Request(new Uri(uriBuilder.ToString()), HttpMethod.Post);
            return request;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="folio"></param>
        /// <param name="rfcContratacion"></param>
        /// <param name="idContratato"></param>
        /// <returns></returns>
        public Request NewAutenticacion(Credenciales satCredenciales)
        {
            var uriBuilder = NewBaseURIBuilder();

            NameValueCollection parameters = new NameValueCollection();
            parameters = HttpUtility.ParseQueryString(uriBuilder.Query);

            uriBuilder.Scheme = this.wsSchema;
            uriBuilder.Host = this.wsHost;
            uriBuilder.Path = this.wsPath + "v2" + "/" + "auth";

            var consulta = new Consulta()
            {
                rfc = satCredenciales.getRFC(),
                password = satCredenciales.getPassword(),
            };

            Request request = new Request(new Uri(uriBuilder.ToString()), HttpMethod.Post);

            return request;
        }

        /// <summary>
        /// TODO: Actualmente le estamos pasando al PATH a tener una URL
        /// estatica, se tiene que cambiar por una absoluta.
        /// </summary>
        /// <param name="folio"></param>
        /// <returns></returns>
        public Request NewRepetirConsultaRequest(string folio)
        {
            UriBuilder uriBuilder = NewBaseURIBuilder();
            ConsultaParametros parameters2 = new ConsultaParametros(
                new ConsultaParametrosBuilder()
            );
            return new Request(
                NewRepetirUUIDURIBuilder(folio, parameters2.ToString()).Uri,
                HttpMethod.Post
            );
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="folio"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        private UriBuilder NewRepetirUUIDURIBuilder(string folio, string path)
        {
            return NewBaseURIBuilder("?" + path + folio);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="folio"></param>
        /// <returns></returns>
        public Request NewStatusRequest(string folio)
        {
            var request = new Request(
                this.NewResultadosURIBuilder(folio, "/progreso").Uri,
                HttpMethod.Get
            );

            return request;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="folio"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        protected UriBuilder NewResultadosURIBuilder(string folio, string path)
        {
            return NewBaseURIBuilder("resultados/" + folio + path);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="folio"></param>
        /// <returns></returns>
        public Request NewResumenRequest(string folio)
        {
            return NewResultadosRequest(folio, "/resumen");
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="folio"></param>
        /// <param name="pagina"></param>
        /// <returns></returns>
        public Request NewResultadosRequest(string folio, int pagina)
        {
            return NewResultadosRequest(folio + "/", +pagina);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="folio"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        private Request NewResultadosRequest(string folio, string path)
        {
            var request = new Request(NewResultadosURIBuilder(folio, path).Uri, HttpMethod.Get);
            request.HttpMethod = HttpMethod.Get;

            return request;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="folio"></param>
        /// <param name="folioCFDI"></param>
        /// <returns></returns>
        public Request NewDescargaRequest(string folio, string folioCFDI)
        {
            Request request = new Request(
                NewDescargaURIBuilder(folio, folioCFDI).Uri,
                HttpMethod.Get
            );

            //request.setAcceptMediaType(Request.MediaType.TEXT_XML);


            return request;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="folio"></param>
        /// <param name="folioCFDI"></param>
        /// <returns></returns>
        protected UriBuilder NewDescargaURIBuilder(string folio, string folioCFDI)
        {
            return NewBaseURIBuilder("/descargas/" + folio + "/" + folioCFDI);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="folio"></param>
        /// <param name="folioCFDI"></param>
        /// <returns></returns>
        protected UriBuilder NewDescargaFolioURIBuilder(string folioCFDI)
        {
            return NewBaseURIBuilder("v3/cfdi" + "/" + folioCFDI);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="folio"></param>
        /// <returns></returns>
        public Request NewDescargaZipRequest(string folio)
        {
            var request = new Request(this.NewResultadosURIBuilder(folio, "").Uri, HttpMethod.Get);
            return request;
        }

        /// <summary>
        /// TODO:: Tenemos que simplificar el método.
        /// </summary>
        /// <param name="csCredenciales"></param>
        /// <param name="satCredenciales"></param>
        /// <param name="parametros"></param>
        /// <returns></returns>
        public Request SolicitudFoliosRequest(
            User csCredenciales,
            Credenciales satCredenciales,
            ConsultaParametros parametros
        )
        {
            NameValueCollection parametrosEncriptar = null;

            HttpMethod methodRequest = HttpMethod.Post;
            MediaType mediatType = MediaType.JSON;

            NameValueCollection parameters = new NameValueCollection();

            Consulta consulta = null;

            UriBuilder uriBuilder = NewBaseURIBuilder();
            uriBuilder.Scheme = this.wsSchema;
            uriBuilder.Host = this.wsHost;

            UriBuilder uriBuilder2 = NewBaseURIBuilder();
            uriBuilder2.Scheme = this.wsSchema;
            uriBuilder2.Host = this.wsHost;

            parameters = HttpUtility.ParseQueryString(uriBuilder.Query);

            consulta = new Consulta();
            consulta.rfcContribuyente = satCredenciales.getRFC();
            consulta.password = satCredenciales.getPassword();
            consulta.folios = parametros.getFolios();

            uriBuilder.Scheme = this.wsSchema;
            uriBuilder.Host = this.wsHost;
            uriBuilder.Path = this.wsPath + "v3" + "/" + "cfdiFolios";

            consulta.rfcContribuyente = csCredenciales.getRFC().ToString();
            consulta.password = csCredenciales.getPassword();
            consulta.servicio = ConsultaUtil.SERVICIO;
            consulta.descarga = new Descarga()
            {
                rfcContribuyente = satCredenciales.getRFC(),
                password = satCredenciales.getPassword(),
                folios = parametros.getFolios(),
            };

            parametrosEncriptar = new NameValueCollection();
            parametrosEncriptar = HttpUtility.ParseQueryString(uriBuilder2.Query);
            parametrosEncriptar.Add(parameters);
            parametrosEncriptar.Set("cpassContrato", csCredenciales.getPassword());
            parametrosEncriptar.Set("cPassword", csCredenciales.getPassword());

            uriBuilder.Query = parameters.ToString();
            uriBuilder2.Query = parametrosEncriptar.ToString();

            Request request = new Request(new Uri(uriBuilder.ToString()), methodRequest);
            request.SetParametro(parametros);
            request.SetConsulta(consulta);
            request.SetMediaType(mediatType);

            return request;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="user"></param>
        /// <param name="satCredenciales"></param>
        /// <param name="parametros"></param>
        /// <returns></returns>
        public Request SolicitudRequest(
            User user,
            Credenciales satCredenciales,
            ConsultaParametros parametros
        )
        {
            Consulta consulta = null;
            Request request = null;

            UriBuilder uriBuilder = NewBaseURIBuilder();
            uriBuilder.Scheme = this.wsSchema;
            uriBuilder.Host = this.wsHost;
            uriBuilder.Path = this.wsPath + "v3" + "/" + "cfdis";

            consulta = new Consulta()
            {
                servicio = ConsultaUtil.SERVICIO,
                descarga = new Descarga()
                {
                    rfcContribuyente = satCredenciales.getRFC().ToString(),
                    password = satCredenciales.getPassword(),
                    fechaInicio = parametros.getFechaInicio(),
                    fechaFin = parametros.getFechaFin(),
                    tipo = ConsultaUtil.getBusquedaConsultaEnum(parametros.getTipo()),
                    tipoDoc = ConsultaUtil.getBusquedaTipoDocEnum(parametros.getTipoDoc()),
                    status = ConsultaUtil.getBusquedaStatusEnum(parametros.getEstatusCFDI()),
                    complementos = parametros.getComplemento(),
                    rfcBusqueda = (
                        parametros.getSearch() != null && parametros.getSearch().Length > 0
                            ? parametros.getSearch()
                            : ""
                    ),
                    metadata = parametros.getIsMetadata(),
                },
                rfcContribuyente = user.RFC.ToString(),
                password = user.getPassword(),
            };

            HttpMethod methodRequest = HttpMethod.Post;

            request = new Request(new Uri(uriBuilder.ToString()), methodRequest)
            {
                Consulta = consulta,
                MediaType = MediaType.JSON,
                HttpMethod = methodRequest,
            };

            return request;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="uuid"></param>
        /// <param name="parametrosCS"></param>
        /// <returns></returns>
        public Request VerificarRequest(string uuid)
        {
            var request = new Request(
                this.NewResultadosURIBuilder(uuid, "/progreso").Uri,
                HttpMethod.Get
            );
            request.HttpMethod = HttpMethod.Get;

            return request;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="rfc"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Request NewAutenticacion(string rfc, string password)
        {
            var uriBuilder = NewBaseURIBuilder();
            NameValueCollection parameters = new NameValueCollection();
            parameters = HttpUtility.ParseQueryString(uriBuilder.Query);
            uriBuilder.Scheme = this.wsSchema;
            uriBuilder.Host = this.wsHost;
            uriBuilder.Path = this.wsPath + "v2" + "/" + "auth";
            var consulta = new Consulta() { rfc = rfc, password = password };

            HttpMethod methodRequest = HttpMethod.Post;
            var request = new Request(new Uri(uriBuilder.ToString()), HttpMethod.Post)
            {
                Consulta = consulta,
                MediaType = MediaType.JSON,
                HttpMethod = methodRequest,
            };

            return request;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="folio"></param>
        /// <param name="parametros"></param>
        /// <returns></returns>
        public Request ResumenRequest(string folio)
        {
            return NewResultadosRequest(folio, "/resumen");
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="folio"></param>
        /// <param name="pagina"></param>
        /// <param name="parametros"></param>
        /// <returns></returns>
        public Request ResultadosRequest(string folio, int pagina)
        {
            return NewResultadosRequest(folio, "/" + pagina);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="folio"></param>
        /// <param name="folioCFDI"></param>
        /// <param name="parametros"></param>
        /// <returns></returns>
        public Request DescargaRequest(string folio, string folioCFDI)
        {
            Request request = new Request(
                NewDescargaURIBuilder(folio, folioCFDI).Uri,
                HttpMethod.Get
            );
            request.MediaType = MediaType.TEXT_XML;
            request.HttpMethod = HttpMethod.Get;

            return request;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="folioCFDI"></param>
        /// <returns></returns>
        public Request DescargaFolioRequest(string folioCFDI)
        {
            Request request = new Request(
                NewDescargaFolioURIBuilder(folioCFDI).Uri,
                HttpMethod.Get
            );

            request.MediaType = MediaType.TEXT_XML;
            request.HttpMethod = HttpMethod.Get;

            return request;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="folio"></param>
        /// <param name="parametros"></param>
        /// <returns></returns>
        public Request DescargaZipRequest(string folio)
        {
            var request = new Request(this.NewResultadosURIBuilder(folio, "").Uri, HttpMethod.Get);
            request.MediaType = MediaType.TEXT_XML;
            return request;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="folio"></param>
        /// <param name="rfc"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Request RepetirRequest(string folio, User user)
        {
            var uriBuilder = NewBaseURIBuilder();
            NameValueCollection parameters = new NameValueCollection();
            parameters = HttpUtility.ParseQueryString(uriBuilder.Query);
            parameters.Add(parameters);
            parameters.Set("uuid", folio);
            uriBuilder.Scheme = this.wsSchema;
            uriBuilder.Host = this.wsHost;
            uriBuilder.Path = this.wsPath + "v3" + "/" + "repetir";

            var consulta = new Consulta()
            {
                rfcContribuyente = user?.RFC,
                password = user?.Password,
            };

            uriBuilder.Query = parameters.ToString();
            HttpMethod methodRequest = HttpMethod.Get;
            var request = new Request(new Uri(uriBuilder.ToString()), HttpMethod.Get)
            {
                Consulta = consulta,
                MediaType = MediaType.JSON,
                HttpMethod = methodRequest,
            };

            return request;
        }
    }
}
