using System;
using System.Collections.Generic;
using System.Text;

namespace descarga_ciec_sdk.src.Impl.Https
{
    public class RawResponse
    {
        /// <summary>
        ///
        /// </summary>
        private string Contenido;

        /// <summary>
        ///
        /// </summary>
        private int Code;

        /// <summary>
        ///
        /// </summary>
        private string Json;

        /// <summary>
        ///
        /// </summary>
        /// <param name="contenido"></param>
        /// <param name="code"></param>
        /// <param name="json"></param>
        public RawResponse(string contenido, int code, string json)
        {
            this.Contenido = contenido;
            this.Code = code;
            this.Json = json;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public string GetContenido()
        {
            return this.Contenido;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public int GetCode()
        {
            return this.Code;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public string GetJson()
        {
            return this.Json;
        }
    }
}
