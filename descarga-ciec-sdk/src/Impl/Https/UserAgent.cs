using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using descarga_ciec_sdk.src.Enums;
using descarga_ciec_sdk.src.Interfaces;
using descarga_ciec_sdk.src.Models;
using Newtonsoft.Json;

namespace descarga_ciec_sdk.src.Impl.Https
{
    public class UserAgent : IUserAgent
    {
        /// <summary>
        ///
        /// </summary>
        private HttpWebRequest httpWebRequest;

        /// <summary>
        ///
        /// </summary>
        private HttpWebResponse httpWebResponse;

        /// <summary>
        ///
        /// </summary>
        private StreamReader streamReader;

        /// <summary>
        ///
        /// </summary>
        private string mediaType;

        /// <summary>
        ///
        /// </summary>
        private JsonConverterCollection Json;

        /// <summary>
        ///
        /// </summary>
        public UserAgent()
        {
            httpWebRequest = null;
            httpWebResponse = null;
            streamReader = null;
        }

        /// <summary>
        /// Realiza una petición a un servidor web.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Response Open(Request request)
        {
            throw new Exception();
        }

        /// <summary>
        /// Realiza una petición a un servidor web.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Response> OpenAsync(Request request)
        {
            throw new Exception();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="httpWebRequest"></param>
        /// <param name="entity"></param>
        private void HandleFormEncoded(
            HttpWebRequest httpWebRequest,
            Dictionary<string, string> entity
        )
        {
            StringBuilder builder = new StringBuilder();

            foreach (KeyValuePair<string, string> kvp in entity)
            {
                builder.Append(kvp.Key + "=" + kvp.Value + "&");
            }

            byte[] byteArray = Encoding.UTF8.GetBytes(builder.ToString());

            httpWebRequest.ContentLength = byteArray.Length;

            Stream dataStream = httpWebRequest.GetRequestStream();

            dataStream.Write(byteArray, 0, byteArray.Length);

            dataStream.Close();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="httpWebRequest"></param>
        /// <param name="entity"></param>
        private async Task HandleFormEncodedAsync(
            HttpWebRequest httpWebRequest,
            Dictionary<string, string> entity
        )
        {
            StringBuilder builder = new StringBuilder();

            foreach (KeyValuePair<string, string> kvp in entity)
            {
                builder.Append(kvp.Key + "=" + kvp.Value + "&");
            }

            byte[] byteArray = Encoding.UTF8.GetBytes(builder.ToString());

            httpWebRequest.ContentLength = byteArray.Length;

            Stream dataStream = await httpWebRequest.GetRequestStreamAsync();

            await dataStream.WriteAsync(byteArray, 0, byteArray.Length);

            dataStream.Close();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="httpWebRequest"></param>
        /// <param name="entity"></param>
        private void HanbleJSON(HttpWebRequest httpWebRequest, Dictionary<string, string> entity)
        {
            string json = JsonConvert.SerializeObject(entity);

            httpWebRequest.ContentLength = json.Length;

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="httpWebRequest"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        private async Task HanbleJSONAsync(
            HttpWebRequest httpWebRequest,
            Dictionary<string, string> entity
        )
        {
            string json = JsonConvert.SerializeObject(entity);

            httpWebRequest.ContentLength = json.Length;

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                await streamWriter.WriteAsync(json);
                streamWriter.Flush();
                streamWriter.Close();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="httpWebRequest"></param>
        /// <param name="consulta"></param>
        private void HanbleJSONConsulta(HttpWebRequest httpWebRequest, Consulta consulta)
        {
            string json = JsonConvert.SerializeObject(consulta);

            byte[] data = Encoding.UTF8.GetBytes(json);
            httpWebRequest.ContentLength = data.Length;

            Stream dataStream = httpWebRequest.GetRequestStream();
            dataStream.Write(data, 0, data.Length);
            dataStream.Close();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="httpWebRequest"></param>
        /// <param name="consulta"></param>
        /// <returns></returns>
        private async Task HanbleJSONConsultaAsync(HttpWebRequest httpWebRequest, Consulta consulta)
        {
            string json = JsonConvert.SerializeObject(consulta);

            byte[] data = Encoding.UTF8.GetBytes(json);
            httpWebRequest.ContentLength = data.Length;

            Stream dataStream = httpWebRequest.GetRequestStream();
            await dataStream.WriteAsync(data, 0, data.Length);
            dataStream.Close();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private RawResponse OpenRaw(HttpWebRequest request)
        {
            RawResponse rawResponse = null;

            try
            {
                httpWebResponse = (HttpWebResponse)request.GetResponse();

                int code = (int)httpWebResponse.StatusCode;
                string mensajeCode = httpWebResponse.StatusCode.ToString();

                streamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.UTF8);

                string responseFromServer = streamReader.ReadToEnd();

                rawResponse = new RawResponse(mensajeCode, code, responseFromServer);
            }
            catch (WebException ex)
            {
                throw new WebException(ex.Message);
            }
            finally
            {
                Close();
            }

            return rawResponse;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<RawResponse> OpenRawAsync(HttpWebRequest request)
        {
            RawResponse rawResponse = null;

            try
            {
                httpWebResponse = (HttpWebResponse)await request.GetResponseAsync();

                int code = (int)httpWebResponse.StatusCode;
                string mensajeCode = httpWebResponse.StatusCode.ToString();

                streamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.UTF8);

                string responseFromServer = await streamReader.ReadToEndAsync();

                rawResponse = new RawResponse(mensajeCode, code, responseFromServer);
            }
            catch (WebException ex)
            {
                throw new WebException(ex.Message);
            }
            finally
            {
                Close();
            }

            return rawResponse;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public JsonConverterCollection GetJSONCOnvert()
        {
            return this.Json;
        }

        /// <summary>
        /// Realiza las operaciones necesarias para liberar los recursos utilizados.
        /// </summary>
        public void Close()
        {
            try
            {
                if (httpWebResponse != null)
                {
                    httpWebResponse.Close();
                }
            }
            catch (IOException e)
            {
                throw new IOException(e.Message);
                ;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Response Enviar(Request request)
        {
            if (request.HttpMethod == HttpMethod.Get)
            {
                httpWebRequest = (HttpWebRequest)
                    WebRequest.Create(request.Uri.AbsoluteUri.ToString());
                httpWebRequest.Method = request.HttpMethod.ToString();
                httpWebRequest.UserAgent = "Descarga CIEC SDK C#";

                if (request.MediaType == MediaType.TEXT_XML)
                {
                    httpWebRequest.ContentType = "application/xml";
                }

                if (!string.IsNullOrWhiteSpace(request?.Consulta?.rfcContribuyente))
                {
                    httpWebRequest.Headers.Add("rfc", request?.Consulta?.rfcContribuyente);
                    httpWebRequest.Headers.Add("password", request.Consulta.password);
                }
            }
            else
            {
                httpWebRequest = (HttpWebRequest)
                    WebRequest.Create(request.Uri.AbsoluteUri.ToString());
                httpWebRequest.Method = HttpMethod.Post.ToString();

                if (request.MediaType == MediaType.TEXT_XML)
                {
                    httpWebRequest.ContentType = "application/xml";
                }
                else
                {
                    httpWebRequest.ContentType = "application/json";
                }

                httpWebRequest.UserAgent = "Descarga CIEC SDK C#";

                if (!string.IsNullOrWhiteSpace(request?.Consulta?.rfcContribuyente))
                {
                    httpWebRequest.Headers.Add("rfc", request?.Consulta?.rfcContribuyente);
                    httpWebRequest.Headers.Add("password", request.Consulta.password);
                }

                switch (request.MediaType)
                {
                    case MediaType.JSON:
                        HanbleJSONConsulta(httpWebRequest, request.Consulta);
                        break;
                    case MediaType.X_WWW_FORM_URLENCODED:
                        HandleFormEncoded(httpWebRequest, request.Entity);
                        break;
                    default:
                        throw new InvalidOperationException("method not handled");
                }

                httpWebRequest.AllowAutoRedirect = true;
                httpWebResponse = default(HttpWebResponse);
            }

            RawResponse rawResponse = OpenRaw(httpWebRequest);

            return new Response(
                rawResponse.GetJson(),
                rawResponse.GetContenido(),
                rawResponse.GetCode()
            );
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Response EnviarFolio(Request request)
        {
            if (request.HttpMethod == HttpMethod.Get)
            {
                httpWebRequest = (HttpWebRequest)
                    WebRequest.Create(request.Uri.AbsoluteUri.ToString());
                httpWebRequest.Method = request.HttpMethod.ToString();
                httpWebRequest.UserAgent = "Descarga CIEC SDK C#";

                if (request.MediaType == MediaType.TEXT_XML)
                {
                    httpWebRequest.ContentType = "application/xml";
                    httpWebRequest.Accept = "application/xml";
                }
            }

            RawResponse rawResponse = OpenRaw(httpWebRequest);

            return new Response(
                rawResponse.GetJson(),
                rawResponse.GetContenido(),
                rawResponse.GetCode()
            );
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Response> EnviarAsync(Request request)
        {
            if (request.HttpMethod == HttpMethod.Get)
            {
                httpWebRequest = (HttpWebRequest)
                    WebRequest.Create(request.Uri.AbsoluteUri.ToString());
                httpWebRequest.Method = request.HttpMethod.ToString();
                httpWebRequest.UserAgent = "Descarga CIEC SDK C#";

                if (request.MediaType == MediaType.TEXT_XML)
                {
                    httpWebRequest.ContentType = "application/xml";
                }

                if (!string.IsNullOrWhiteSpace(request?.Consulta?.rfcContribuyente))
                {
                    httpWebRequest.Headers.Add("rfc", request?.Consulta?.rfcContribuyente);
                    httpWebRequest.Headers.Add("password", request.Consulta.password);
                }
            }
            else
            {
                httpWebRequest = (HttpWebRequest)
                    WebRequest.Create(request.Uri.AbsoluteUri.ToString());
                httpWebRequest.Method = HttpMethod.Post.ToString();

                if (request.MediaType == MediaType.TEXT_XML)
                {
                    httpWebRequest.ContentType = "application/xml";
                }
                else
                {
                    httpWebRequest.ContentType = "application/json";
                }

                httpWebRequest.UserAgent = "Descarga CIEC SDK C#";

                if (!string.IsNullOrWhiteSpace(request?.Consulta?.rfcContribuyente))
                {
                    httpWebRequest.Headers.Add("rfc", request?.Consulta?.rfcContribuyente);
                    httpWebRequest.Headers.Add("password", request?.Consulta?.password);
                }

                switch (request.MediaType)
                {
                    case MediaType.JSON:
                        await HanbleJSONConsultaAsync(httpWebRequest, request.Consulta);
                        break;
                    case MediaType.X_WWW_FORM_URLENCODED:
                        await HandleFormEncodedAsync(httpWebRequest, request.Entity);
                        break;
                    default:
                        throw new InvalidOperationException("method not handled");
                }

                httpWebRequest.AllowAutoRedirect = false;
                httpWebResponse = default(HttpWebResponse);
            }

            RawResponse rawResponse = await OpenRawAsync(httpWebRequest);

            return new Response(
                rawResponse.GetJson(),
                rawResponse.GetContenido(),
                rawResponse.GetCode()
            );
        }
    }
}
