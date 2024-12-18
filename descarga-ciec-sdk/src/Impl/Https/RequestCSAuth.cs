using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using descarga_ciec_sdk.src.Enums;
using descarga_ciec_sdk.src.Models;

namespace descarga_ciec_sdk.src.Impl.Https
{
    public class RequestCSAuth
    {
        public RequestCSAuth(Uri uri, HttpMethod httpMethod, MediaType media = MediaType.JSON)
        {
            this.Uri = uri;
            this.HttpMethod = httpMethod;
            this.MediaType = media;
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
        public Autenticación Autenticación { get; set; }

        /// <summary>
        ///
        /// </summary>
        public Dictionary<string, string> parametros { get; set; }
    }
}
