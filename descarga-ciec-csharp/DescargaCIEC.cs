using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using descarga_ciec_sdk.src.Impl.Consultas.Descargar;
using descarga_ciec_sdk.src.Impl.Consultas.Repetir;
using descarga_ciec_sdk.src.Impl.Consultas.Solicitar;
using descarga_ciec_sdk.src.Impl.Consultas.Verificar;
using descarga_ciec_sdk.src.Interfaces;
using descarga_ciec_sdk.src.Models;

namespace descarga_ciec_sdk
{
    public class DescargaCIEC
    {
        /// <summary>
        /// Methodo para hacer la solicitud de la consulta
        /// </summary>
        /// <param name="consultaParametros"></param>
        /// <returns></returns>
        public string SolicitarConsulta(ConsultaParametros consultaParametros)
        {
            try
            {
                if (consultaParametros == null)
                {
                    throw new Exception("Los parametros de consulta son requeridos");
                }
                SolicitarHandle solicitarHandle = new SolicitarHandle();
                return solicitarHandle.Handle(consultaParametros);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Metodo para buscar por folio
        /// </summary>
        /// <param name="consultaParametros"></param>
        /// <returns></returns>
        private string BuscarFolio(string folio, ConsultaParametros consultaParametros)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(folio))
                {
                    throw new Exception("El folio es requerido");
                }

                //consultaParametros.Folios.Add(consultaParametros.getFolio());
                //consultaParametros.listaFolios.Add(folio);

                SolicitarHandle solicitarHandle = new SolicitarHandle();
                return solicitarHandle.HandleFolios(consultaParametros);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Metodo para buscar por folio
        /// </summary>
        /// <param name="consultaParametros"></param>
        /// <returns></returns>
        public string GetXml(string folio, ConsultaParametros consultaParametros)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(folio))
                {
                    throw new Exception("El folio es requerido");
                }
                //consultaParametros.listaFolios.Add(folio);

                SolicitarHandle solicitarHandle = new SolicitarHandle();
                return solicitarHandle.HandleFolios(consultaParametros);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="folioConsulta"></param>
        /// <returns></returns>
        public ResponseResumen GetSummary(string folioConsulta)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(folioConsulta))
                {
                    throw new Exception("El Id de la consulta es requerido");
                }

                SolicitarHandle solicitarHandle = new SolicitarHandle();
                return solicitarHandle.HandleSummary(folioConsulta);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="folioConsulta"></param>
        /// <returns></returns>
        public bool HasResultado(string folioConsulta)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(folioConsulta))
                {
                    throw new Exception("El Id de la consulta es requerido");
                }

                SolicitarHandle solicitarHandle = new SolicitarHandle();
                return solicitarHandle.HandleSummary(folioConsulta).total > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="folioConsulta"></param>
        /// <param name="pagina"></param>
        /// <returns></returns>
        public List<Metadata> GetResultado(string folioConsulta, int pagina)
        {
            try
            {
                if (pagina <= 0)
                {
                    throw new Exception("El número de página debe ser mayor or igual a uno (1)");
                }

                VerificarHandle verificarHandle = new VerificarHandle();
                var resultados = verificarHandle.Handle(folioConsulta).GetResultados(pagina);

                return resultados;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obtener el estatus de una consulta
        /// </summary>
        public string GetEstatusConsulta(string folioConsulta)
        {

            try
            {
                if (string.IsNullOrWhiteSpace(folioConsulta))
                {
                    throw new Exception("El folio de la consulta es requerido");
                }

                VerificarHandle verificarHandle = new VerificarHandle();
                return verificarHandle.Handle(folioConsulta).GetStatus();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Metodo para buscar por folio
        /// </summary>
        /// <param name="consultaParametros"></param>
        /// <returns></returns>
        public string GetMetadataCFDI(string folio, ConsultaParametros consultaParametros)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(folio))
                {
                    throw new Exception("El folio es requerido");
                }
                // consultaParametros.listaFolios.Add(folio);

                SolicitarHandle solicitarHandle = new SolicitarHandle();
                return solicitarHandle.HandleFolios(consultaParametros);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Metodo para buscar por una lista de folios
        /// </summary>
        /// <param name="consultaParametros"></param>
        /// <returns></returns>
        public string BuscarListaFolios(ConsultaParametros consultaParametros)
        {
            try
            {
                if (consultaParametros.getFolios().Count < 0)
                {
                    throw new Exception("La lista de folios esta vacia o nula");
                }
                SolicitarHandle solicitarHandle = new SolicitarHandle();
                return solicitarHandle.HandleFolios(consultaParametros);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Metodo para consultar el estado de la consulta
        /// </summary>
        /// <param name="folioConsulta"></param>
        /// <returns></returns>
        public IVerificarImpl GetProgreso(string folioConsulta)
        {

            try
            {
                if (string.IsNullOrWhiteSpace(folioConsulta))
                {
                    throw new Exception("El folio de la consulta es requerido");
                }

                VerificarHandle verificarHandle = new VerificarHandle();
                return verificarHandle.Handle(folioConsulta);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="folioConsulta"></param>
        /// <returns></returns>
        public int GetTotalEncontrados(string folioConsulta)
        {
          
            try
            {
                if (string.IsNullOrWhiteSpace(folioConsulta))
                {
                    throw new Exception("El folio de la consulta es requerido");
                }

                VerificarHandle verificarHandle = new VerificarHandle();
                return verificarHandle.Handle(folioConsulta).GetEncontrado();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="folioConsulta"></param>
        /// <returns></returns>
        public string GetEstadoConsulta(string folioConsulta)
        {
      
            try
            {

                if (string.IsNullOrWhiteSpace(folioConsulta))
                {
                    throw new Exception("El folio de la consulta es requerido");
                }
                VerificarHandle verificarHandle = new VerificarHandle();
                return verificarHandle.Handle(folioConsulta).GetStatus();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="folio"></param>
        /// <returns></returns>
        public string DescargaXml(string folio)
        {
          
            try
            {
                if (string.IsNullOrWhiteSpace(folio))
                {
                    throw new Exception("El folio es requerido");
                }
                DescargarHandle descargarHandle = new DescargarHandle();
                return descargarHandle.DescargarXml(folio);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="folio"></param>
        /// <returns></returns>
        public Metadata DescargaMetadataXml(string folio)
        {
         
            try
            {
                if (string.IsNullOrWhiteSpace(folio))
                {
                    throw new Exception("El folio es requerido");
                }
                DescargarHandle descargarHandle = new DescargarHandle();
                return descargarHandle.DescargarMetadataXml(folio);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Metodo para descarga el ZIP de los XMls
        /// </summary>
        /// <param name="folioConsulta"></param>
        /// <param name="pathZIP"></param>
        /// <returns></returns>
        public string DescargaZIP(string folioConsulta, string pathZIP)
        {
       
            try
            {
                if (string.IsNullOrWhiteSpace(folioConsulta))
                {
                    throw new Exception("El folio de la consulta es requerido");
                }
                if (string.IsNullOrEmpty(pathZIP))
                {
                    throw new Exception("La ruta del ZIP es requerida");
                }

                DescargarHandle descargarHandle = new DescargarHandle();
                return descargarHandle.Descargar(folioConsulta, pathZIP);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Descarga ZIP
        /// Descomprima ZIP
        /// <param name="folioConsulta"></param>
        /// <param name="pathZIP"></param>
        /// <returns></returns>
        public string DescargarAndDescomprimirZIP(string folioConsulta, string pathZIP)
        {
          
            try
            {
                if (string.IsNullOrWhiteSpace(folioConsulta))
                {
                    throw new Exception("El folio de la consulta es requerido");
                }
                DescargarHandle descargarHandle = new DescargarHandle();
                return descargarHandle.DescargarYdescomprimir(folioConsulta, pathZIP);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Metodo para obtener la lista de las metadatas de la consulta
        /// </summary>
        /// <param name="folioConsulta"></param>
        /// <returns></returns>
        public List<Metadata> GetListMetadata(string folioConsulta)
        {
            
            try
            {
                if (string.IsNullOrWhiteSpace(folioConsulta))
                {
                    throw new Exception("El folio de la consulta es requerido");
                }
                DescargarHandle descargarHandle = new DescargarHandle();
                return descargarHandle.GetMetadatas(folioConsulta);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Metodo para repetir una consulta
        /// </summary>
        /// <param name="folioConsulta"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public string RepetirConsulta(string folioConsulta, User user)
        {
            
            try
            {
                if (string.IsNullOrWhiteSpace(folioConsulta))
                {
                    throw new Exception("El folio de la consulta es requerido");
                }
                if (user == null)
                {
                    throw new Exception("Las credenciales de contratacion son requeridas (User)");
                }
                RepetirHandle repetirHandle = new RepetirHandle();
                return repetirHandle.Handle(folioConsulta, user);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
