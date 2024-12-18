using System;
using System.Collections.Generic;
using System.Text;
using descarga_ciec_sdk.src.Enums;

namespace descarga_ciec_sdk.src.Models
{
    public class ConsultaParametrosBuilder
    {
        /// <summary>
        /// Credenciales para la empresa fiscal.
        /// </summary>
        public Credenciales SATCredenciales { get; private set; }

        /// <summary>
        ///  /// Credenciales de contratación.
        /// </summary>
        public User CSCredenciales { get; private set; }

        /// <summary>
        /// Fecha de emisión inicial de los CFDIs a buscar.
        /// </summary>
        public DateTime FechaInicio { get; private set; }

        /// <summary>
        /// Fecha de emisión final de los CFDIs a buscar.
        /// </summary>
        public DateTime FechaFinal { get; private set; }

        /// <summary>
        /// El estatus de los CFDIs a buscar, por defecto es TODOS.
        /// </summary>
        public EstatusCFDI EstatusCFDI { get; private set; }

        /// <summary>
        /// Tipo de complemento a buscar.
        /// </summary>
        public string Complementos { get; private set; }

        /// <summary>
        /// El tipo de información a buscar (xml/metadatas).
        /// </summary>
        public string tipoSolicitud { get; private set; }

        /// <summary>
        /// Representa el tipo de comprobante.
        /// - Recibidadas.
        /// - Emitidas.
        /// - Todos.
        /// </summary>
        public Movimiento Movimiento { get; private set; }

        /// <summary>
        /// Representa el tipo de documento.
        /// - cfdi.
        /// - retencion.
        /// </summary>
        public TipoDocumento TipoDocumneto { get; private set; }

        /// <summary>
        /// Representa la solicitud de tipo de información que se va descargar puede ser.
        /// - XML
        /// - Metadata
        /// </summary>
        public bool IsMetadata { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string TipoDatoConsulta { get; set; }

        /// <summary>
        /// Representa el Foio de cada comprobante que queremos buscar en el Ws.
        /// </summary>
        public string Folio { get; private set; }

        /// <summary>
        /// Representa la lista de Folios de cada comprobante que queremos buscar en el WS.
        /// </summary>
        public List<string> Folios { get; private set; } = new List<string>();

        /// <summary>
        /// El RFC del emisor/receptor, según el status definido.
        /// </summary>
        public string BusquedaRFC { get; private set; }

        // <summary>
        /// Lista de  RFCs del emisor/receptor, según el status definido.
        /// </summary>
        public List<string> BusquedaRFCs { get; private set; }

        /// <summary>
        /// Representa al UUID de consulta que quermos buscar en el WS
        /// </summary>
        public string UUIDConsulta { get; private set; }

        /// <summary>
        /// Representa la lista de UUID de consultas que queremos buscasr en el WS.
        /// </summary>
        public List<string> UUIDConsultas { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public string TOKEN { get; set; }

        /// <summary>
        ///
        /// </summary>
        public UriRequest UriRequest { get; private set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public ConsultaParametrosBuilder addToken(string token)
        {
            this.TOKEN = token;

            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="SATCredenciales"></param>
        /// <returns></returns>
        public ConsultaParametrosBuilder addSATCredenciales(Credenciales SATCredenciales)
        {
            this.SATCredenciales = SATCredenciales;
            return this;
        }

        ///// <summary>
        /////
        ///// </summary>
        ///// <param name="CSCredenciales"></param>
        ///// <returns></returns>
        //public ConsultaParametrosBuilder addCSCredenciales(User CSCredenciales)
        //{
        //    this.CSCredenciales = CSCredenciales;
        //    return this;
        //}


        /// <summary>
        ///
        /// </summary>
        /// <param name="isXML"></param>
        /// <returns></returns>
        public ConsultaParametrosBuilder addIsXMLMetadata(bool IsMetadata)
        {
            this.IsMetadata = IsMetadata;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="tipoDato"></param>
        /// <returns></returns>
        public ConsultaParametrosBuilder addTipoDatoConsulta(string tipoDato)
        {
            this.TipoDatoConsulta = tipoDato;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="estatus"></param>
        /// <returns></returns>
        public ConsultaParametrosBuilder setFiltroEstatusCFDI(EstatusCFDI estatus)
        {
            this.EstatusCFDI = estatus;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="Complementos"></param>
        /// <returns></returns>
        public ConsultaParametrosBuilder addComplemento(string Complementos)
        {
            this.Complementos = Complementos;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="BusquedaRFC"></param>
        /// <returns></returns>
        public ConsultaParametrosBuilder addBusquedaRFC(string BusquedaRFC)
        {
            this.BusquedaRFC = BusquedaRFC;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="BusquedaRFCs"></param>
        /// <returns></returns>
        public ConsultaParametrosBuilder addBusquedaRFCs(List<string> BusquedaRFCs)
        {
            this.BusquedaRFCs = BusquedaRFCs;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="Movimiento"></param>
        /// <returns></returns>
        public ConsultaParametrosBuilder addMovimiento(Movimiento Movimiento)
        {
            this.Movimiento = Movimiento;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="tipoDocumento"></param>
        /// <returns></returns>
        public ConsultaParametrosBuilder addTipoDocumento(TipoDocumento tipoDocumento)
        {
            this.TipoDocumneto = tipoDocumento;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="Folio"></param>
        /// <returns></returns>
        public ConsultaParametrosBuilder setFolio(string Folio)
        {
            this.Folio = Folio;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="Folios"></param>
        /// <returns></returns>
        public ConsultaParametrosBuilder setListaFolios(List<string> Folios)
        {
            this.Folios = Folios;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="UUIDConsulta"></param>
        /// <returns></returns>
        public ConsultaParametrosBuilder addUUIDConsulta(string UUIDConsulta)
        {
            this.UUIDConsulta = UUIDConsulta;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public ConsultaParametrosBuilder addUriRequest(UriRequest uri)
        {
            this.UriRequest = uri;

            return this;
        }

        /// <summary>
        /// RFC de búsqueda de la consulta.
        /// </summary>
        private string rfcBusqueda;

        /// <summary>
        /// Fecha de inicio.
        /// </summary>
        private DateTime fechaInicio;

        /// <summary>
        /// Fecha final
        /// </summary>
        private DateTime fechaFin;

        /// <summary>
        /// Tipo de comprobante.
        /// </summary>
        private Movimiento movimiento;

        /// <summary>
        /// TipoDOc: cfdi o retencion.
        /// </summary>
        private TipoDocumento tipoDocumento;

        /// <summary>
        /// Tipo de complemento que vamos a búscar
        /// </summary>
        private string complemento;

        /// <summary>
        ///
        /// </summary>
        private string search;

        /// <summary>
        /// Lista de folios de comprobantes.
        /// </summary>
       // private List<string> listFolios;

        /// <summary>
        /// Tipo de petición
        /// </summary>
        private TipoPeticion tipoPeticion;

        /// <summary>
        /// Tipo de xml que vamos a salicitar
        /// </summary>
        private TipoURIRequest tipoURIRequest = TipoURIRequest.XML;

        /// <summary>
        /// Representa la búsqueda de la información del CFDi.
        /// </summary>
        private bool isMetadata=false;

        /// <summary>
        ///
        /// </summary>
        private User credencialesContractacion;

        /// <summary>
        ///
        /// </summary>
        private Credenciales credencialesSAT;


        /// <summary>
        ///
        /// </summary>
        /// <param name="credencialesContractacion"></param>
        /// <returns></returns>
        public ConsultaParametrosBuilder setCredecialesContratacion(User credencialesContractacion)
        {
            this.credencialesContractacion = credencialesContractacion;

            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="credencialesSAT"></param>
        /// <returns></returns>
        public ConsultaParametrosBuilder setCredecialesSAT(Credenciales credencialesSAT)
        {
            this.credencialesSAT = credencialesSAT;

            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="rfc"></param>
        /// <returns></returns>
        public ConsultaParametrosBuilder RFCBusqueda(string rfc)
        {
            this.rfcBusqueda = rfc;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public ConsultaParametrosBuilder FechaFin(DateTime fecha)
        {
            this.fechaFin = fecha;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="movimiento"></param>
        /// <returns></returns>
        public ConsultaParametrosBuilder Tipo(Movimiento movimiento)
        {
            this.movimiento = movimiento;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="tipoDoc"></param>
        /// <returns></returns>
        public ConsultaParametrosBuilder TipoDocumento(TipoDocumento tipoDoc)
        {
            this.tipoDocumento = tipoDoc;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="estatus"></param>
        /// <returns></returns>
        public ConsultaParametrosBuilder FiltroEstatusCFDI(EstatusCFDI estatus)
        {
            this.EstatusCFDI = estatus;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public ConsultaParametrosBuilder Search(string search)
        {
            this.search = search;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="complemento"></param>
        /// <returns></returns>
        public ConsultaParametrosBuilder Complemento(string complemento)
        {
            this.complemento = complemento;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="peticion"></param>
        /// <returns></returns>
        public ConsultaParametrosBuilder TipoPeticion(TipoPeticion peticion)
        {
            this.tipoPeticion = peticion;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public string getRfcBusqueda()
        {
            return this.rfcBusqueda;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public User getCredencialesContratacion()
        {
            return credencialesContractacion;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public Credenciales getCredencialesSAT()
        {
            return credencialesSAT;
        }

        public DateTime getFechaInicio()
        {
            return this.fechaInicio;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public bool getIsMetadata()
        {
            return this.isMetadata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public DateTime getFechaFin()
        {
            return this.fechaFin;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public EstatusCFDI getStatus()
        {
            return this.EstatusCFDI;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public Movimiento getMovimiento()
        {
            return this.movimiento;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public Movimiento setMovimiento(Movimiento movimiento)
        {
            return this.movimiento = movimiento;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public TipoDocumento getTipoDocumento()
        {
            return this.tipoDocumento;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public EstatusCFDI getFiltroEstatusCFDI()
        {
            return this.EstatusCFDI;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public TipoDocumento setTipoDocumento(TipoDocumento tipoDocumento)
        {
            return this.tipoDocumento = tipoDocumento;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public string getComplemento()
        {
            return this.complemento;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public TipoPeticion getTipoPeticion()
        {
            return this.tipoPeticion;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public ConsultaParametrosBuilder setFechaInicio(DateTime fecha)
        {
            this.fechaInicio = fecha;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public ConsultaParametrosBuilder setFechaFin(DateTime fecha)
        {
            this.fechaFin = fecha;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public List<string> getFolios()
        {
            return this.Folios;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public TipoURIRequest getTipoURIRequest()
        {
            return this.tipoURIRequest;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public ConsultaParametros getParametros()
        {
            var parametros = new ConsultaParametros(this);

            return parametros;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public ConsultaParametros Build()
        {
            return new ConsultaParametros(this);
        }
    }
}
