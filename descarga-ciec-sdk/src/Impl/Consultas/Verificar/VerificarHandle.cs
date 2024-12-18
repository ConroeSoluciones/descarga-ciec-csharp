using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using descarga_ciec_sdk.src.Enums;
using descarga_ciec_sdk.src.Interfaces;
using descarga_ciec_sdk.src.Models;

namespace descarga_ciec_sdk.src.Impl.Consultas.Verificar
{
    /// <summary>
    ///
    /// </summary>
    public class VerificarHandle
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public IVerificarImpl Handle(string idConsulta)
        {
            IVerificarImpl verificar = null;

            try
            {
                IVerificarProvider verificarCIECProvider = new VerificarProvider();
                verificar = verificarCIECProvider.Verificar(idConsulta);
                Thread.Sleep(2000);
            }
            catch (System.Exception)
            {
                throw;
            }

            return verificar;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Tuple<bool, TipoEstatusConsulta>> HandleAsync(
            string idConsulta,
            CancellationToken cancellationToken
        )
        {
            string status = "";
            int encontrados = 0;
            bool isOk = true;

            TipoEstatusConsulta consultaEstatus = TipoEstatusConsulta.EN_ESPERA;
            IVerificarImpl verificar = null;

            try
            {
                IVerificarProvider verificarCIECProvider = new VerificarProvider();
                verificar = await verificarCIECProvider.VerificarAsync(idConsulta);

                while (!await verificar.IsCompletadoAsync())
                {
                    Thread.Sleep(5000);

                    status = verificar.GetEstado();
                    encontrados = verificar.GetEncontrado();
                }

                if (await verificar.IsFalloAsync())
                {
                    status = verificar.GetEstado();
                    encontrados = verificar.GetEncontrado();

                    return new Tuple<bool, TipoEstatusConsulta>(isOk, consultaEstatus);
                }

                await verificar.IsCompletadoAsync();

                status = verificar.GetEstado();
                encontrados = verificar.GetEncontrado();

                Thread.Sleep(2000);

                isOk = true;
            }
            catch (System.Exception)
            {
                throw;
            }

            return new Tuple<bool, TipoEstatusConsulta>(isOk, consultaEstatus);
        }
    }
}
