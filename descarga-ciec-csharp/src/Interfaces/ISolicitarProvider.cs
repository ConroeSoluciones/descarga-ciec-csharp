using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using descarga_ciec_sdk.src.Models;

namespace descarga_ciec_sdk.src.Interfaces
{
    /// <summary>
    ///
    /// </summary>
    public interface ISolicitarProvider
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="consultaParametros"></param>
        /// <returns></returns>
        ISolicitarImpl Solicitar(ConsultaParametros consultaParametros);

        /// <summary>
        ///
        /// </summary>
        /// <param name="consultaParametros"></param>
        /// <returns></returns>
        ISolicitarImpl SolicitarFolios(ConsultaParametros consultaParametros);

        /// <summary>
        ///
        /// </summary>
        /// <param name="IdConsulta"></param>
        /// <returns></returns>
        ResponseResumen SolicitarSummary(string IdConsulta);

        /// <summary>
        ///
        /// </summary>
        /// <param name="consultaParametros"></param>
        /// <returns></returns>
        Task<ISolicitarImpl> SolicitarAsync(ConsultaParametros consultaParametros);
    }
}
