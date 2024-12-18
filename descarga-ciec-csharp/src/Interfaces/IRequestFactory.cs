using System;
using System.Collections.Generic;
using System.Text;
using descarga_ciec_sdk.src.Impl.Https;
using descarga_ciec_sdk.src.Models;

namespace descarga_ciec_sdk.src.Interfaces
{
    public interface IRequestFactory
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="user"></param>
        /// <param name="satCredenciales"></param>
        /// <param name="parametros"></param>
        /// <returns></returns>
        Request NewConsultaRequest(
            User user,
            Credenciales satCredenciales,
            ConsultaParametros parametros
        );

        /// <summary>
        ///
        /// </summary>
        /// <param name="folio"></param>
        /// <param name="folioCFDI"></param>
        /// <param name="parametros"></param>
        /// <returns></returns>
        Request NewDescargaRequest(string folio, string folioCFDI);

        /// <summary>
        ///
        /// </summary>
        /// <param name="folio"></param>
        /// <param name="parametros"></param>
        /// <returns></returns>
        Request NewDescargaZipRequest(string folio);

        /// <summary>
        ///
        /// </summary>
        /// <param name="folio"></param>
        /// <returns></returns>
        Request NewRepetirConsultaRequest(string folio);

        /// <summary>
        ///
        /// </summary>
        /// <param name="folio"></param>
        /// <param name="pagina"></param>
        /// <param name="parametros"></param>
        /// <returns></returns>
        Request NewResultadosRequest(string folio, int pagina);

        /// <summary>
        /// /
        /// </summary>
        /// <param name="folio"></param>
        /// <param name="parametros"></param>
        /// <returns></returns>
        Request NewResumenRequest(string folio);

        /// <summary>
        ///
        /// </summary>
        /// <param name="folio"></param>
        /// <returns></returns>
        Request NewStatusRequest(string folio);

        /// <summary>
        ///
        /// </summary>
        /// <param name="satCredenciales"></param>
        /// <returns></returns>
        Request NewAutenticacion(Credenciales satCredenciales);

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        Request SolicitudRequest(
            User user,
            Credenciales satCredenciales,
            ConsultaParametros parametros
        );

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        Request SolicitudFoliosRequest(
            User user,
            Credenciales satCredenciales,
            ConsultaParametros parametros
        );

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        Request VerificarRequest(string uuid);

        /// <summary>
        /// s
        /// </summary>
        /// <param name="rfc"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Request NewAutenticacion(string rfc, string password);

        /// <summary>
        ///
        /// </summary>
        /// <param name="folio"></param>
        /// <returns></returns>
        Request ResumenRequest(string folio);

        /// <summary>
        ///
        /// </summary>
        /// <param name="folio"></param>
        /// <param name="pagina"></param>
        /// <returns></returns>
        Request ResultadosRequest(string folio, int pagina);

        /// <summary>
        ///
        /// </summary>
        /// <param name="folio"></param>
        /// <param name="folioCFDI"></param>
        /// <returns></returns>
        Request DescargaRequest(string folio, string folioCFDI);

        /// <summary>
        ///
        /// </summary>
        /// <param name="folioCFDI"></param>
        /// <returns></returns>
        Request DescargaFolioRequest(string folioCFDI);

        /// <summary>
        ///
        /// </summary>
        /// <param name="folio"></param>
        /// <returns></returns>
        Request DescargaZipRequest(string folio);

        /// <summary>
        ///
        /// </summary>
        /// <param name="folio"></param>
        /// <param name="parametros"></param>
        /// <returns></returns>
        Request RepetirRequest(string folio, User user);
    }
}
