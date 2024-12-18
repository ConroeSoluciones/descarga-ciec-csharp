using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using descarga_ciec_sdk.src.Models;

namespace descarga_ciec_sdk.src.Interfaces
{
    public interface IVerificarProvider
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="IDConsulta"></param>
        /// <returns></returns>
        IVerificarImpl Verificar(string IDConsulta);

        /// <summary>
        ///
        /// </summary>
        /// <param name="IDConsulta"></param>
        /// <returns></returns>
        Task<IVerificarImpl> VerificarAsync(string IDConsulta);
    }
}
