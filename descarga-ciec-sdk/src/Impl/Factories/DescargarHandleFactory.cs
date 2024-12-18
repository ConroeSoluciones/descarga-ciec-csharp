using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using descarga_ciec_sdk.src.Impl.Consultas.Descargar;
using descarga_ciec_sdk.src.Interfaces;
using descarga_ciec_sdk.src.Models;

namespace descarga_ciec_sdk.src.Impl.Factories
{
    public class DescargarHandleFactory : IDescargarHandleFactory
    {
        /// <summary>
        ///
        /// </summary>
        public DescargarHandleFactory() { }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public string GuardarComprobantes(
            List<Metadata> lista,
            string idConsulta,
            string uriZip,
            string pathRutaZIps
        )
        {
            string pathOutCFDI = pathRutaZIps;
            // string pathOutMetadata = "";
            string nombre;
            //string pathFull = "";

            // pathOutCFDI = Path.Combine(pathRutaZIps, "XML");
            //pathOutMetadata = Path.Combine(pathRutaZIps, "Metadata");

            nombre = idConsulta;

            return GuardarZIPAsync(uriZip, pathOutCFDI, nombre);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="lista"></param>
        /// <param name="idConsulta"></param>
        /// <param name="uriZip"></param>
        /// <param name="RFCSolicitante"></param>
        /// <returns></returns>
        public Task<string> GuardarComprobantesAsync(
            List<Metadata> lista,
            string idConsulta,
            string uriZip,
            string RFCSolicitante
        )
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public string GuardarZIPAsync(string pathZIP, string pathOutCFDI, string nombre)
        {
            IZIPService iZIPService = new ZIPService();
            return iZIPService.DescargarYDescomprimirZIPAsync_(pathZIP, nombre, pathOutCFDI);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public void GuardarZIP(string pathZIP, string pathOutCFDI, string nombre, string rfcEmpresa)
        {
            IZIPService iZIPService = new ZIPService();
            iZIPService.DescargarYDescomprimirZIPAsync_(pathZIP, nombre, pathOutCFDI, rfcEmpresa);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public void GuardarYDescomprimirZIP(
            string pathZIP,
            string pathOutCFDI,
            string nombre,
            string rfcEmpresa
        )
        {
            IZIPService iZIPService = new ZIPService();
            iZIPService.DescargarYDescomprimirZIPAsync_(pathZIP, nombre, pathOutCFDI, rfcEmpresa);
        }
    }
}
