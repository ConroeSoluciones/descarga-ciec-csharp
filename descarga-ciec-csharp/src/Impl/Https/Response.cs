using System;
using System.Collections.Generic;
using System.Text;

namespace descarga_ciec_sdk.src.Impl.Https
{
    public class Response
    {
        /// <summary>
        ///
        /// </summary>
        public string RawResponse;

        /// <summary>
        ///
        /// </summary>
        public int Code;

        /// <summary>
        ///
        /// </summary>
        public string Json;

        /// <summary>
        ///
        /// </summary>
        /// <param name="json"></param>
        /// <param name="rawResponse"></param>
        /// <param name="code"></param>
        public Response(string json, string rawResponse, int code)
        {
            this.Json = json;
            this.RawResponse = rawResponse;
            this.Code = code;
        }

        /// <summary>
        /// Convierte la respuesta a una estructura JSON.
        /// </summary>
        /// <returns>retorna la estructura en formato JSON</returns>
        public string GetAsJsonConsulta()
        {
            return Json;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public string GetRawResponse()
        {
            return this.RawResponse;
        }

        /// <summary>
        /// Estatus code
        /// </summary>
        /// <returns></returns>
        public int GetCode()
        {
            return this.Code;
        }
    }
}
