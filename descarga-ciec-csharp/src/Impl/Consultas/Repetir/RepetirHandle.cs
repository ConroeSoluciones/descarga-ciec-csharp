using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using descarga_ciec_sdk.src.Models;

namespace descarga_ciec_sdk.src.Impl.Consultas.Repetir
{
    /// <summary>
    ///
    /// </summary>
    public class RepetirHandle
    {
        public RepetirHandle() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UUID"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public string Handle(string UUID, User user)
        {
            if (UUID == null)
            {
                throw new System.Exception("El folio de la consulta es requerido");
            }

            //Si no obtenemos ningún error es que si se repitio la consulta.
            RepetirProvider repetirCIECProvider = new RepetirProvider();
            var result = repetirCIECProvider.Repetir(UUID, user);

            return result;
        }
    }
}
