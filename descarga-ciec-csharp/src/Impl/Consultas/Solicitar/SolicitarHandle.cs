using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using descarga_ciec_sdk.src.Impl.Consultas.Descargar;
using descarga_ciec_sdk.src.Interfaces;
using descarga_ciec_sdk.src.Models;

namespace descarga_ciec_sdk.src.Impl.Consultas.Solicitar
{
    /// <summary>
    /// 
    /// </summary>
    public class SolicitarHandle
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="parametros"></param>
        /// <returns></returns>
        public string Handle(ConsultaParametros parametros)
        {
            string idConsulta = "";

            string rfcEmpresa = parametros.getCredencialesCS().RFC;

            ISolicitarProvider solicitarCIECProvider = new SolicitarProvider();
            var solicitar = solicitarCIECProvider.Solicitar(parametros);

            idConsulta = solicitar.GetIDConsulta();

            if (string.IsNullOrWhiteSpace(idConsulta))
            {
                throw new System.Exception(
                    "El folio de la consulta no se devolvió del WS de la CIEC"
                );
            }

            return idConsulta;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="parametros"></param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public string HandleFolios(ConsultaParametros parametros)
        {
            string idConsulta = "";

            ISolicitarProvider solicitarCIECProvider = new SolicitarProvider();
            var solicitar = solicitarCIECProvider.SolicitarFolios(parametros).GetIDConsulta(); ;


            if (string.IsNullOrWhiteSpace(idConsulta))
            {
                throw new System.Exception(
                    "El folio de de consulta no se devolvió del WS de la CIEC"
                );
            }

            return idConsulta;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parametros"></param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public string HandleGetMetadataCFDI(ConsultaParametros parametros)
        {
            string idConsulta = "";
            ISolicitarProvider solicitarCIECProvider = new SolicitarProvider();
            var solicitar = solicitarCIECProvider.SolicitarFolios(parametros);

            idConsulta = solicitar.GetIDConsulta();

            if (string.IsNullOrWhiteSpace(idConsulta))
            {
                throw new System.Exception(
                    "El folio de la consulta no se devolvió del WS de la CIEC"
                );
            }

            return idConsulta;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdConsulta"></param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public ResponseResumen HandleSummary(string IdConsulta)
        {
            if (string.IsNullOrWhiteSpace(IdConsulta))
            {
                throw new System.Exception("El folio de la consulta es requerido");
            }
            ISolicitarProvider solicitarCIECProvider = new SolicitarProvider();

            return solicitarCIECProvider.SolicitarSummary(IdConsulta);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="parametros"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<string> HandleAsync(
            ConsultaParametros parametros,
            CancellationToken cancellationToken
        )
        {
            string idConsulta = "";

            string rfcEmpresa = parametros.getCredencialesSAT().RFC;

            ISolicitarProvider solicitarCIECProvider = new SolicitarProvider();
            var solicitar = await solicitarCIECProvider.SolicitarAsync(parametros);

            idConsulta = solicitar.GetIDConsulta();

            if (string.IsNullOrWhiteSpace(idConsulta))
            {
                throw new System.Exception(
                    "El folio de la consulta no se devolvió del WS de la CIEC"
                );
            }

            return idConsulta;
        }
    }
}
