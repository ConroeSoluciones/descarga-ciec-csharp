using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using descarga_ciec_sdk.src.Models;

namespace descarga_ciec_sdk.src.Interfaces
{
    public interface IWSConsultarService
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="consultaParametrosCS"></param>
        void Consultar(ConsultaParametros consultaParametros, string folio = null);

        /// <summary>
        ///
        /// </summary>
        /// <param name="consultaParametrosCS"></param>
        Task ConsultarAsync(ConsultaParametros consultaParametros, string folio = null);

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        string GetEncontrado();

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        string GetFolio();

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        int GetPaginas();

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        Task<int> GetPaginasAsync();

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
        Task<string> GetStatusAsync();

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        int GetTotalResultados();

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        Task<int> getTotalResultadosAsync();

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        bool IsFallo();

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        Task<bool> IsFalloAsync();

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        bool IsTerminada();

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        Task<bool> IsTerminadaAsync();

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        string GetEmpresa();
    }
}
