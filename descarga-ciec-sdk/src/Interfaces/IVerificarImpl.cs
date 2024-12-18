using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using descarga_ciec_sdk.src.Enums;
using descarga_ciec_sdk.src.Models;

namespace descarga_ciec_sdk.src.Interfaces
{
    /// <summary>
    ///
    /// </summary>
    public interface IVerificarImpl
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="folioCFDI"></param>
        /// <returns></returns>
        //string GetCFDIXML(string folioCFDI);

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        List<string> GetFechaMismoHorario();

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        string GetIdConsulta();

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        bool GetIsXMLFaltantes();

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        int GetPaginas();

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        ResponseResumen GetResponseResumen();

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
        int GetTotalResultados();

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
        bool IsRepetir();

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        bool IsTerminada();

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        string GetEstado();

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        int GetEncontrado();

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
        Task<string> GetStatusAsync();

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        Task<ResponseResumen> GetResponseResumenAsync();
    }
}
