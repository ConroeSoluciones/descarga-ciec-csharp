using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace descarga_ciec_sdk.src.Interfaces
{
    public interface IZIPService
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="nombreZIP"></param>
        /// <param name="path"></param>
        void DescargarZIP(string uri, string nombreZIP, string path, string token);

        /// <summary>
        ///
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="nombreZIP"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        Task DescargarZIPAsync(string uri, string nombreZIP, string path, string token = null);

        /// <summary>
        ///
        /// </summary>
        /// <param name="paqueteFromBase64String"></param>
        /// <param name="nombreZIP"></param>
        /// <param name="path"></param>
        void GuardarZIPFileBase64(byte[] paqueteFromBase64String, string nombreZIP, string path);

        /// <summary>
        ///
        /// </summary>
        /// <param name="paqueteFromBase64String"></param>
        /// <param name="nombreZIP"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        Task GuardarZIPFileBase64Async(
            byte[] paqueteFromBase64String,
            string nombreZIP,
            string path
        );

        /// <summary>
        ///
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="nombreZIP"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        string DescargarYDescomprimirZIPAsync_(
            string uri,
            string nombreZIP,
            string path,
            string token = null
        );

        /// <summary>
        ///
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="nombreZIP"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        string DescargarZIP(string uri, string nombreZIP, string path);

        /// <summary>
        ///
        /// </summary>
        /// <param name="paqueteFromBase64String"></param>
        /// <param name="nombreZIP"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        Task GuardarZIPFileBase64YDescomprimirZIPAsync(
            byte[] paqueteFromBase64String,
            string nombreZIP,
            string path
        );

        /// <summary>
        ///
        /// </summary>
        /// <param name="paqueteFromBase64String"></param>
        /// <param name="nombrePDF"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        Task DescargarPDFBase64Async(
            string paqueteFromBase64String,
            string nombrePDF,
            string path,
            string token = null
        );
    }
}
