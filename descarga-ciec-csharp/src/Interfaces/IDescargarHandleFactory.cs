using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using descarga_ciec_sdk.src.Models;

namespace descarga_ciec_sdk.src.Interfaces
{
    public interface IDescargarHandleFactory
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="lista"></param>
        /// <param name="idConsulta"></param>
        /// <param name="uriZip"></param>
        /// <param name="RFCSolicitante"></param>
        /// <returns></returns>
        Task<string> GuardarComprobantesAsync(
            List<Metadata> lista,
            string idConsulta,
            string uriZip,
            string RFCSolicitante
        );

        /// <summary>
        ///
        /// </summary>
        /// <param name="lista"></param>
        /// <param name="idConsulta"></param>
        /// <param name="uriZip"></param>
        /// <param name="RFCSolicitante"></param>
        /// <param name="pathZIP"></param>
        /// <returns></returns>
        string GuardarComprobantes(
            List<Metadata> lista,
            string idConsulta,
            string uriZip,
            string pathZIP
        );

        /// <summary>
        ///
        /// </summary>
        /// <param name="pathZIP"></param>
        /// <param name="pathOutCFDI"></param>
        /// <param name="nombre"></param>
        /// <param name="rfcEmpresa"></param>
        void GuardarZIP(string pathZIP, string pathOutCFDI, string nombre, string rfcEmpresa);
    }
}
