using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using descarga_ciec_sdk.src.Impl.Consultas.Descargar;
using descarga_ciec_sdk.src.Impl.Factories;
using descarga_ciec_sdk.src.Interfaces;
using descarga_ciec_sdk.src.Models;

namespace descarga_ciec_sdk.src.Impl.Consultas.Descargar
{
    public class DescargarHandle
    {
        /// <summary>
        ///
        /// </summary>
        private IDescargarHandleFactory _descargarCIECHandleFactory;

        /// <summary>
        ///
        /// </summary>
        private List<Metadata> _listaMetada;

        public DescargarHandle()
        {
            _descargarCIECHandleFactory = new DescargarHandleFactory();
            _listaMetada = new List<Metadata>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idConsulta"></param>
        /// <param name="parametrosCS"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public string Handle(string idConsulta, string pathZIP, ConsultaParametros parametrosCS)
        {
            if (string.IsNullOrWhiteSpace(idConsulta))
            {
                throw new System.Exception("El folio de la consulta es requerido");
            }

            IDescargarProvider descargarCIECProvider = new DescargarProvider();
            var descargar = descargarCIECProvider.Descargar(idConsulta);

            _listaMetada = descargar.GetTotalMetadata();

            var uriZIP = descargar.GetPathZip();

            string pathFull = _descargarCIECHandleFactory.GuardarComprobantes(
                _listaMetada,
                idConsulta,
                uriZIP,
                pathZIP
            );

            Thread.Sleep(1000);

            return pathFull;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idConsulta"></param>
        /// <param name="pathZIP"></param>
        /// <param name="parametrosCS"></param>
        /// <returns></returns>
        public string Descargar(string idConsulta, string pathZIP)
        {
            string pathFull = pathZIP;

            if (string.IsNullOrWhiteSpace(idConsulta))
            {
                throw new System.Exception("El folio de la consulta es requerido");
            }

            IDescargarProvider descargarCIECProvider = new DescargarProvider();
            var descargar = descargarCIECProvider.Descargar(idConsulta);

            // _listaMetada = descargar.getTotalMetadata();

            if (descargar.GetTotalMetadata().Count > 0)
            {
                var uriZIP = descargar.GetPathZip();

                pathFull = _descargarCIECHandleFactory.GuardarComprobantes(
                    _listaMetada,
                    idConsulta,
                    uriZIP,
                    pathZIP
                );
            }
            else
            {
                throw new Exception("No se pudo descargar el ZIP no hay XMLs");
            }

            return pathFull;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idConsulta"></param>
        /// <param name="pathZIP"></param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public string DescargarXml(string folio)
        {
            if (string.IsNullOrWhiteSpace(folio))
            {
                throw new System.Exception("El folio del XML es requerido");
            }

            IDescargarProvider descargarCIECProvider = new DescargarProvider();

            return descargarCIECProvider.DescargarXml(folio).GetCFDIXML();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="folio"></param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public Metadata DescargarMetadataXml(string folio)
        {
            if (string.IsNullOrWhiteSpace(folio))
            {
                throw new System.Exception("El folio de XML es requerido");
            }

            IDescargarProvider descargarCIECProvider = new DescargarProvider();

            return descargarCIECProvider.DescargarMetadataXml(folio).GetMetadataXML();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idConsulta"></param>
        /// <param name="pathZIP"></param>
        /// <param name="parametrosCS"></param>
        /// <returns></returns>
        public string DescargarYdescomprimir(string idConsulta, string pathZIP)
        {
            if (string.IsNullOrWhiteSpace(idConsulta))
            {
                throw new System.Exception("El Id de la consulta es requerido");
            }

            IDescargarProvider descargarCIECProvider = new DescargarProvider();
            var descargar = descargarCIECProvider.Descargar(idConsulta);

            //_listaMetada = descargar.getTotalMetadata();

            var uriZIP = descargar.GetPathZip();

            string pathFull = _descargarCIECHandleFactory.GuardarComprobantes(
                _listaMetada,
                idConsulta,
                uriZIP,
                pathZIP
            );

            //Thread.Sleep(1000);


            return pathFull;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public List<Metadata> GetMetadatas(string idConsulta)
        {
            if (string.IsNullOrWhiteSpace(idConsulta))
            {
                throw new System.Exception("El folio de la consulta es requerido");
            }

            IDescargarProvider descargarCIECProvider = new DescargarProvider();
            var descargar = descargarCIECProvider.Descargar(idConsulta);

            _listaMetada = descargar.GetTotalMetadata();

            return _listaMetada;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idConsulta"></param>
        /// <param name="parametrosCS"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task Handle(
            string idConsulta,
            ConsultaParametros parametrosCS,
            CancellationToken cancellationToken
        )
        {
            string pathZIP = "";

            string rfcEmpresa = parametrosCS.SATCredenciales.RFC;

            if (string.IsNullOrWhiteSpace(idConsulta))
            {
                throw new System.Exception("El folio de la consulta es requerido");
            }

            IDescargarProvider descargarCIECProvider = new DescargarProvider();
            var descargar = await descargarCIECProvider.DescargarAsync(idConsulta);

            var lista = await descargar.GetTotalMetadataAsync();

            pathZIP = descargar.GetPathZip();

            string pathFull = _descargarCIECHandleFactory.GuardarComprobantes(
                lista,
                idConsulta,
                pathZIP,
                pathZIP
            );

            Thread.Sleep(1000);
        }
    }
}
