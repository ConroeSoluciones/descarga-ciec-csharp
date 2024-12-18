using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using descarga_ciec_sdk.src.Models;

namespace descarga_ciec_sdk.src.Interfaces
{
    public interface IDescargarProvider
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="idConsulta"></param>
        /// <param name="parametros"></param>
        /// <returns></returns>
        IDescargarImpl Descargar(string idConsulta);

        /// <summary>
        ///
        /// </summary>
        /// <param name="idConsulta"></param>
        /// <returns></returns>
        IDescargarImpl DescargarXml(string folio);

        /// <summary>
        ///
        /// </summary>
        /// <param name="idConsulta"></param>
        /// <returns></returns>
        IDescargarImpl DescargarMetadataXml(string folio);

        /// <summary>
        ///
        /// </summary>
        /// <param name="idConsulta"></param>
        /// <param name="parametros"></param>
        /// <returns></returns>
        Task<IDescargarImpl> DescargarAsync(string idConsulta);
    }
}
