using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using descarga_ciec_sdk.src.Models;

namespace descarga_ciec_sdk.src.Interfaces
{
    public interface IDescargarImpl
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="folioCFDI"></param>
        /// <returns></returns>
        string GetCFDIXML();

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        Metadata GetMetadataXML();

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        int GetPaginas();

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        string GetPathZip();

        /// <summary>
        ///
        /// </summary>
        /// <param name="pagina"></param>
        /// <returns></returns>
        List<Metadata> GetResultados(int pagina);

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        string GetStatus();

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        List<Metadata> GetTotalMetadata();

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        bool HasResultados();

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        bool IsCompletado();

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        bool IsFallo();

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        bool IsTerminada();

        Task<string> GetStatusAsync();

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        Task<bool> IsCompletadoAsync();

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        Task<bool> IsFalloAsync();

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        Task<bool> IsTerminadaAsync();

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        Task<List<Metadata>> GetTotalMetadataAsync();

        /// <summary>
        ///
        /// </summary>
        /// <param name="pagina"></param>
        /// <returns></returns>
        Task<List<Metadata>> GetResultadosAsync(int pagina);

        /// <summary>
        ///
        /// </summary>
        /// <param name="folioCFDI"></param>
        /// <returns></returns>
        Task<string> GetCFDIXMLAsync(string folioCFDI);
    }
}
