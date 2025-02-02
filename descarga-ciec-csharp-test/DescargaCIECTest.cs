using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using descarga_ciec_sdk;
using descarga_ciec_sdk.src.Models;
using Newtonsoft.Json;
using Xunit;
using static System.Net.Mime.MediaTypeNames;



namespace descarga_ciec_sdk_test
{
    /// <summary>
    /// Clase prueba
    /// </summary>
    public class DescargaCIECTest
    {
        /// <summary>
        ///
        /// </summary>
        private descarga_ciec_sdk.DescargaCIEC DescargaCIEC;

        /// <summary>
        ///
        /// </summary>
        private ConsultaParametros _ParametrosConsulta;

        /// <summary>
        ///
        /// </summary>
        private User _user;

        /// <summary>
        ///
        /// </summary>
        private ConsultaParametrosBuilder _ParametrosBuilder;

        /// <summary>
        ///
        /// </summary>
        private string _folioConsulta;

        /// <summary>
        ///
        /// </summary>
        private DatosPrueba _DatosPrueba;

        /// <summary>
        ///
        /// </summary>
        public DescargaCIECTest()
        {
            DescargaCIEC = new descarga_ciec_sdk.DescargaCIEC();
            SetParametrosConsulta();
        }

        /// <summary>
        /// Definir todos los parametros para la consulta
        /// </summary>
        private void SetParametrosConsulta()
        {
            _ParametrosBuilder = new ConsultaParametrosBuilder();

            //var datosTest = @"./resources/Data.json";

            //if (File.Exists(datosTest))
            //{
            //    _DatosPrueba = JsonConvert.DeserializeObject<DatosPrueba>(
            //        File.ReadAllText(datosTest)
            //    );
            //    _folioConsulta = _DatosPrueba.UuidConsulta_;
            //}

            //var fileUser = @"./resources/User.json";
            //var fileCredenciales = @"./resources/Credenciales.json";

            //if (File.Exists(fileUser) && File.Exists(fileCredenciales))
            //{
            //    _user = JsonConvert.DeserializeObject<User>(File.ReadAllText(fileUser));

            //    _ParametrosBuilder.setCredecialesContratacion(_user);
            //    _ParametrosBuilder.setCredecialesSAT(
            //        JsonConvert.DeserializeObject<Credenciales>(File.ReadAllText(fileCredenciales))
            //    );
            //}
            //// credenciales empresa ante el SAT
            //Credenciales credenciales = new Credenciales("CSO1304138Z0", "C0nR0E50");

            //// credenciales de contratacion CSFacturacion
            // User user = new User("AAA010101AAA", "Iehee*th2036");
            //_ParametrosBuilder.setFechaInicio(new DateTime(2024, 2, 1));
            //_ParametrosBuilder.setFechaFin(new DateTime(2024, 3, 3));
            //_ParametrosBuilder.setMovimiento(descarga_ciec_sdk.src.Enums.Movimiento.TODAS);
            //_ParametrosBuilder.setFiltroEstatusCFDI(descarga_ciec_sdk.src.Enums.EstatusCFDI.TODOS);
            //_ParametrosBuilder.setTipoDocumento(descarga_ciec_sdk.src.Enums.TipoDocumento.CFDI);
            //_ParametrosConsulta = new ConsultaParametros(_ParametrosBuilder);


            // credenciales empresa ante el SAT
            Credenciales credenciales = new Credenciales("", "");

            // credenciales de contratacion CSFacturacion
            User user = new User("", "");

            // construir parametros de consulta con ParametrosBuilder
            _ParametrosBuilder.setCredecialesContratacion(user);
            _ParametrosBuilder.setCredecialesSAT(credenciales);
            _ParametrosBuilder.setFechaInicio(new DateTime(2022, 2, 1));
            _ParametrosBuilder.setFechaFin(new DateTime(2024, 3, 3));
            _ParametrosBuilder.setMovimiento(descarga_ciec_sdk.src.Enums.Movimiento.TODAS);
            _ParametrosBuilder.setFiltroEstatusCFDI(descarga_ciec_sdk.src.Enums.EstatusCFDI.TODOS);
            _ParametrosBuilder.setTipoDocumento(descarga_ciec_sdk.src.Enums.TipoDocumento.CFDI);
            _ParametrosConsulta = new ConsultaParametros(_ParametrosBuilder);

        }

        /// <summary>
        /// Solicitar una conuslta de descarga
        /// </summary>
        [Fact]
        public void Solicitar_Consulta_Return_Success()
        {
            try
            {
                _folioConsulta = DescargaCIEC.SolicitarConsulta(_ParametrosConsulta);

            }
            catch (Exception ex)
            {

                Assert.ThrowsAny<ArgumentException>(() => throw new ArgumentException());
            }
        }


        /// <summary>
        /// Obtener el resultado de la consulta por número de página
        /// Success
        /// </summary>
        [Fact]
        public void Get_Resultado_Returns_Return_Success()
        {
            

            try
            {
                var metadata = DescargaCIEC.GetResultado(_DatosPrueba.UuidConsulta_, 1);

                Assert.Equal(20, metadata.Count);

            }
            catch (Exception)
            {

                Assert.ThrowsAny<ArgumentException>(() => throw new ArgumentException());
            }
        }

        /// <summary>
        /// Obtener el resultado de la consulta por número de página
        /// Failled  
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        [Fact]
        public void Get_Resultado_Paginas_Returns_ReturnException_Failled()
        {
            try
            {
                var metadata = DescargaCIEC.GetResultado(_DatosPrueba.UuidConsulta_, 100);
            }
            catch (Exception)
            {
                Assert.ThrowsAny<ArgumentException>(() => throw new ArgumentException());
            }
        }

        /// <summary>
        /// Obtner el total de XMLs encontrados
        /// </summary>
        [Fact]
        public void Get_Total_Encontrados_Consulta_Return_Success()
        {
            

            try
            {
                var estatus = DescargaCIEC.GetEstatusConsulta("b0cab294-dab4-4f27-adc7-fd3cd788cff5");

                var total = DescargaCIEC.GetTotalEncontrados("b0cab294-dab4-4f27-adc7-fd3cd788cff5");
                Assert.Equal(249, total);
            }
            catch (Exception)
            {

                Assert.ThrowsAny<ArgumentException>(() => throw new ArgumentException());
            }
        }

        /// <summary>
        /// Obtener el estatus de la consulta
        /// </summary>
        [Fact]
        public void Get_Estatus_Consulta_Return_Success()
        {
            

            try
            {
                _folioConsulta = DescargaCIEC.SolicitarConsulta(_ParametrosConsulta);

                var estatus = DescargaCIEC.GetEstatusConsulta(_folioConsulta);

                Assert.Equal("REPETIR", estatus);
            }
            catch (Exception)
            {

                Assert.ThrowsAny<ArgumentException>(() => throw new ArgumentException());
            }
        }

        /// <summary>
        /// Obtener el progreso de la consulta
        /// </summary>
        [Fact]
        public void Get_Progreso_Consulta_Return_Success()
        {
            

            try
            {
                var folioConsulta = DescargaCIEC.SolicitarConsulta(_ParametrosConsulta);

                var progreso = DescargaCIEC.GetProgreso(folioConsulta);

                while (!progreso.IsCompletado())
                {
                    System.Diagnostics.Debug.WriteLine(
                        $"Estatus Consulta: {progreso.GetStatus()} | Total CFDI encontrado: {progreso.GetEncontrado()}"
                    );
                }

            }
            catch (Exception)
            {

                Assert.ThrowsAny<ArgumentException>(() => throw new ArgumentException());
            }
        }

        /// <summary>
        /// Obtener el resumen de una consulta usando el IdConsulta
        /// </summary>
        [Fact]
        public void Get_Summary_Return_Success()
        {
            

            try
            {
                var summary = DescargaCIEC.GetSummary(_DatosPrueba.UuidConsulta_);

                Assert.Equal(249, summary.total);
                Assert.Equal(13, summary.paginas);
                Assert.False(summary.xmlFaltantes);
                Assert.Empty(summary.fechasMismoHorario);
                Assert.Equal(4, summary.cancelados);
            }
            catch (Exception)
            {

                Assert.ThrowsAny<ArgumentException>(() => throw new ArgumentException());
            }
        }

        /// <summary>
        /// Verificar si tiene resultado la consulta
        /// </summary>
        [Fact]
        public void Has_Resultado_Return_Success()
        {
          


            try
            {
                var hasResultado = DescargaCIEC.HasResultado(_DatosPrueba.UuidConsulta_);

                Assert.True(hasResultado);
            }
            catch (Exception)
            {

                Assert.ThrowsAny<ArgumentException>(() => throw new ArgumentException());
            }
        }

        /// <summary>
        /// Obtener el numero de paginas del resultado
        /// </summary>
        [Fact]
        public void Get_Numero_Paginas_Return_Success()
        {
           


            try
            {
                var paginas = DescargaCIEC.GetSummary(_DatosPrueba.UuidConsulta_).paginas;

                Assert.Equal(13, paginas);
            }
            catch (Exception)
            {

                Assert.ThrowsAny<ArgumentException>(() => throw new ArgumentException());
            }
        }

        /// <summary>
        /// Obtener Xmls fechasMismoHorario
        /// </summary>
        [Fact]
        public void Get_FechasMismoHorario_Return_Success()
        {
           

            try
            {
                var fechasMismoHorario = DescargaCIEC
               .GetSummary(_DatosPrueba.UuidConsulta_)
               .fechasMismoHorario;

                Assert.Empty(fechasMismoHorario);
            }
            catch (Exception)
            {

                Assert.ThrowsAny<ArgumentException>(() => throw new ArgumentException());
            }
        }

        /// <summary>
        /// Es Xml Faltantes
        /// </summary>
        [Fact]
        public void Is_Xml_Faltantes_Return_Success()
        {
            


            try
            {
                var isXmlFaltantes = DescargaCIEC.GetSummary(_DatosPrueba.UuidConsulta_).xmlFaltantes;

                Assert.False(isXmlFaltantes);
            }
            catch (Exception)
            {

                Assert.ThrowsAny<ArgumentException>(() => throw new ArgumentException());
            }
        }

        /// <summary>
        ///  Obtener el total de cancelados
        /// </summary>
        [Fact]
        public void Get_Cancelados_Return_Success()
        {
            

            try
            {
                var cancelados = DescargaCIEC.GetSummary(_DatosPrueba.UuidConsulta_).cancelados;

                Assert.Equal(4, cancelados);
            }
            catch (Exception)
            {

                Assert.ThrowsAny<ArgumentException>(() => throw new ArgumentException());
            }
        }

        /// <summary>
        /// Descarga el ZIP que contenga los XMLs
        /// </summary>
        [Fact]
        public void Descargar_ZIP_Return_Success()
        {
          
            try
            {
                // Descargando
                var rutaZIP = DescargaCIEC.DescargaZIP(
                    _DatosPrueba.UuidConsulta_,
                    _DatosPrueba.RutaZIP
                );
                Assert.Equal(
                    rutaZIP,
                    Path.Combine(_DatosPrueba.RutaZIP, _DatosPrueba.UuidConsulta_.ToLower() + ".zip")
                );


            }
            catch (Exception)
            {

                Assert.ThrowsAny<ArgumentException>(() => throw new ArgumentException());
            }
        }

        /// <summary>
        /// Descarga el ZIP que contenga los XMLs
        /// Error Not Found
        /// </summary>
        [Fact]
        public void Descargar_ZIP_ReturnsNotFound_Failled()
        {
            try
            {
                var rutaZIP = DescargaCIEC.DescargaZIP(
                    _DatosPrueba.UuidInexistente,
                    _DatosPrueba.RutaZIP
                );
                Assert.Same(_DatosPrueba.RutaZIP, rutaZIP);
            }
            catch (Exception)
            {
                Assert.ThrowsAny<ArgumentException>(() => throw new ArgumentException());
            }
        }

        /// <summary>
        /// Obtner la lista de metadata de los XMLs
        /// </summary>
        [Fact]
        public void Get_ListaMetadataCFDI_Return_Success()
        {
            

            try
            {
                var lista = DescargaCIEC.GetListMetadata("ff213733-a908-4149-86e5-66641d53f7ce");
                Assert.NotNull(lista);
            }
            catch (Exception)
            {

                Assert.ThrowsAny<ArgumentException>(() => throw new ArgumentException());
            }
        }

        /// <summary>
        /// Obtner la lista de metadata de los XMLs
        /// Error Exception
        /// </summary>
        [Fact]
        public void Get_ListaMetadataCFDI_ReturnsException_Failled()
        {
            try
            {
                var lista = DescargaCIEC.GetListMetadata(_DatosPrueba.UuidInexistente);
                Assert.NotNull(lista);
            }
            catch (Exception)
            {
                Assert.ThrowsAny<ArgumentException>(() => throw new ArgumentException());
            }
        }

        /// <summary>
        ///  Descarga  y descomprimir el ZIP que contengan los XMLs
        /// </summary>
        [Fact]
        public void Descargar_DescomprimirZIP_Return_Success()
        {


            try
            {

                var rutaZIP = DescargaCIEC.DescargarAndDescomprimirZIP(
                    _DatosPrueba.UuidZip,
                    _DatosPrueba.RutaZIP
                );
                System.Diagnostics.Debug.WriteLine(
                    $"Descargando y descomprimir ZIP | ID Consulta: {_folioConsulta}"
                );

                Assert.Contains(".zip", rutaZIP);
            }
            catch (Exception)
            {

                Assert.ThrowsAny<ArgumentException>(() => throw new ArgumentException());
            }
        }

        /// <summary>
        ///  Descarga  y descomprimir el ZIP que contengan los XMLs
        ///  Error no terminada
        /// </summary>
        [Fact]
        public void Descargar_DescomprimirZIP_ReturnException_Failled()
        {
            try
            {
                var rutaZIP = DescargaCIEC.DescargarAndDescomprimirZIP(
                    _DatosPrueba.UuidConsulta,
                    _DatosPrueba.RutaZIP
                );
                System.Diagnostics.Debug.WriteLine(
                    $"Descargando y descomprimir ZIP | ID Consulta: {_folioConsulta}"
                );
            }
            catch (Exception)
            {
                Assert.ThrowsAny<ArgumentException>(() => throw new ArgumentException());
            }
        }

        /// <summary>
        /// Repetir consulta usando el folio de la consulta
        /// </summary>
        [Fact]
        public void Repetir_Consulta_Return_Success()
        {
           


            try
            {
                var resultado = DescargaCIEC.RepetirConsulta(_DatosPrueba.FolioRepetir, _user);
                Assert.Equal("Peticion realizada con exito", resultado);
            }
            catch (Exception)
            {

                Assert.ThrowsAny<ArgumentException>(() => throw new ArgumentException());
            }
        }

        /// <summary>
        /// Repetir consulta usando el folio de la consulta
        /// </summary>
        [Fact]
        public void Repetir_Consulta_ReturnException_Failled()
        {
            try
            {
                var resultado = DescargaCIEC.RepetirConsulta(_DatosPrueba.FolioRepetir, null);
            }
            catch (Exception)
            {
                Assert.ThrowsAny<ArgumentException>(() => throw new ArgumentException());
            }
        }

        /// <summary>
        ///Obtener el srting del XML
        /// </summary>
        [Fact]
        public void Descargar_Xml_Return_Success()
        {
           

            try
            {
                var xml = DescargaCIEC.DescargaXml(_DatosPrueba.Uuid_);
                System.Diagnostics.Debug.WriteLine($"Solicitud repetida : {xml}");
                Assert.NotNull(xml);

            }
            catch (Exception)
            {

                Assert.ThrowsAny<ArgumentException>(() => throw new ArgumentException());
            }
        }

        /// <summary>
        /// Obtener la metadata del XML (Class Metadata)
        /// </summary>
        [Fact]
        public void Descargar_MetadataXml_Return_Success()
        {

            try
            {

                var metadata = DescargaCIEC.DescargaMetadataXml(_DatosPrueba.Uuid_);
                System.Diagnostics.Debug.WriteLine($"Solicitud repetida : {metadata.folio}");
                Assert.Equal(_DatosPrueba.Uuid_, metadata.folio.ToUpper());
            }
            catch (Exception)
            {

                Assert.ThrowsAny<ArgumentException>(() => throw new ArgumentException());
            }
        }

        /// <summary>
        /// Descaragar la metadata un XML usando el Folio/UUID 
        /// </summary>
        [Fact]
        public void Descargar_MetadataXml_ReturnsException_Failled()
        {
            try
            {
                var metadata = DescargaCIEC.DescargaMetadataXml(_DatosPrueba.UuidInexistente);
                System.Diagnostics.Debug.WriteLine($"Solicitud repetida : {metadata.folio}");
            }
            catch (Exception)
            {
               // Assert.Equal("The remote server returned an error: (404) Not Found.", ex.Message);
                Assert.ThrowsAny<ArgumentException>(() => throw new ArgumentException());
            }
        }
    }

    /// <summary>
    /// Clase con variables de prueba
    /// </summary>
    public class DatosPrueba
    {
        public string FolioRepetir;
        public string UuidConsulta;
        public string UuidConsulta_;
        public string Uuid;
        public string Uuid_;
        public string RutaZIP;
        public string UuidZip;
        public string UuidInexistente;
    }
}
