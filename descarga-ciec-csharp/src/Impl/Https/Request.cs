using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using descarga_ciec_sdk.src.Enums;
using descarga_ciec_sdk.src.Models;

namespace descarga_ciec_sdk.src.Impl.Https
{
    public class Request
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="httpMethod"></param>
        public Request(Uri uri, HttpMethod httpMethod)
        {
            this.Uri = uri;
            this.HttpMethod = HttpMethod;
        }

        /// <summary>
        ///
        /// </summary>
        public Request() { }

        /// <summary>
        ///
        /// </summary>
        /// <param name="mediaType"></param>
        /// <returns></returns>
        public MediaType SetMediaType(MediaType mediaType)
        {
            return this.MediaType = mediaType;
        }

        public Consulta SetConsulta(Consulta consulta)
        {
            this.Consulta = consulta;

            return consulta;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        public ConsultaParametros SetParametro(ConsultaParametros parametro)
        {
            return Parametros;
        }

        /// <summary>
        ///
        /// </summary>
        public HttpMethod HttpMethod { get; set; }

        /// <summary>
        ///
        /// </summary>
        public Uri Uri { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        ///
        /// </summary>
        public MediaType MediaType { get; set; }

        /// <summary>
        ///
        /// </summary>
        public Consulta Consulta { get; set; }

        /// <summary>
        ///
        /// </summary>
        public ConsultaParametros Parametros { get; set; }

        /// <summary>
        ///
        /// </summary>
        public Dictionary<string, string> Entity { get; set; }
    }
}
