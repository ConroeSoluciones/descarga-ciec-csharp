using System;
using System.Collections.Generic;
using System.Text;
using descarga_ciec_sdk.src.Models;

namespace descarga_ciec_sdk.src.Interfaces
{
    /// <summary>
    /// Interface Solicitar
    /// </summary>
    public interface ISolicitarImpl
    {
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        int GetCodigo();

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        string GetIDConsulta();

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        string GetMensaje();

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        bool IsAutenticado();
    }
}
