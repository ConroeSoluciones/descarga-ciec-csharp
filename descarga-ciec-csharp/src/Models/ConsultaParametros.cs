using System;
using System.Collections.Generic;
using System.Text;
using descarga_ciec_sdk.src.Enums;
using descarga_ciec_sdk.src.Utils;

namespace descarga_ciec_sdk.src.Models
{
    public class ConsultaParametros
    {
        /// <summary>
        ///
        /// </summary>
        public ConsultaParametros() { }

        ///// <summary>
        /////
        ///// </summary>
        //public string TOKEN { get; set; }

        ///// <summary>
        /////
        ///// </summary>
        //public string TipoDatoConsulta { get; set; }

        ///// <summary>
        /////
        ///// </summary>
        //private string DataID { get; set; }

        ///// <summary>
        /////
        ///// </summary>
        //public string DataKey { get; set; }

        ///// <summary>
        /////
        ///// </summary>
        //public bool IsExportacionMasiva { get; set; }

        ///// <summary>
        /////
        ///// </summary>
        //public bool IsMetadata { get; set; }

        ///// <summary>
        /////
        ///// </summary>
       // public Credenciales SATCredenciales { get; set; }

        ///// <summary>
        /////
        //public User CSCredenciales { get; set; }

        ///// <summary>
        /////
        ///// </summary>
        //public UriRequest UriRequest { get; set; }

        /// <summary>
        ///
        /// </summary>
       // public DateTime FechaInicio { get; set; }

        ///// <summary>
        /////
        ///// </summary>
        //public DateTime FechaFinal { get; set; }

        ///// <summary>
        /////
        ///// </summary>
        //public EstatusCFDI EstatusCFDI { get; set; }

        ///// <summary>
        /////
        ///// </summary>
        //public TipoDocumento TipoDocumento { get; set; }

        ///// <summary>
        /////
        ///// </summary>
        //public string Complemento { get; set; }

        ///// <summary>
        /////
        ///// </summary>
        //public string BusquedaRFC { get; set; }

        ///// <summary>
        /////
        ///// </summary>
        //public List<string> BusquedaRFCs { get; set; }

        ///// <summary>
        /////
        ///// </summary>
        //public Movimiento Tipo { get; set; }

        ///// <summary>
        /////
        ///// </summary>
        private string folio { get; set; }

        ///// <summary>
        /////
        ///// </summary>
        //public List<string> Folios { get; set; } = new List<string>();

        ///// <summary>
        /////
        ///// </summary>
        //public string UUIDConsulta { get; set; }

        /// <summary>
        ///
        /// </summary>
        private string rfcBusqueda;

        /// <summary>
        ///
        /// </summary>
        private DateTime fechaInicio;

        /// <summary>
        ///
        /// </summary>
        private DateTime fechaFin;

        /// <summary>
        ///
        /// </summary>
        private EstatusCFDI estatus = EstatusCFDI.TODOS;

        /// <summary>
        ///
        /// </summary>
        private Movimiento tipo = Movimiento.TODAS;

        /// <summary>
        ///
        /// </summary>
        private TipoDocumento tipoDocumento = TipoDocumento.CFDI;

        /// <summary>
        ///
        /// </summary>
        private string complementoCFDI;

        /// <summary>
        ///
        /// </summary>
        private readonly string search="";

        /// <summary>
        ///
        /// </summary>
        private List<string> listaFolios { get; set; } = new List<string>();

        /// <summary>
        ///
        /// </summary>
        private TipoPeticion tipoPeticion = TipoPeticion.FORM_URLENCODED;

        /// <summary>
        ///
        /// </summary>
        private TipoURIRequest tipoURIRequest = TipoURIRequest.XML;

        /// <summary>
        ///
        /// </summary>
        private bool isMetadata;

        /// <summary>
        ///
        /// </summary>
        private User credecialesContratacion;

        /// <summary>
        ///
        /// </summary>
        private Credenciales credecialesSAT;


        /// <summary>
        ///
        /// </summary>
        /// <param name="builder"></param>
        public ConsultaParametros(ConsultaParametrosBuilder builder)
        {
            rfcBusqueda = builder?.getRfcBusqueda();
            fechaFin = builder.getFechaFin();
            fechaInicio = builder.getFechaInicio();
            estatus = builder.getFiltroEstatusCFDI();
            tipo = builder.getMovimiento();
            tipoDocumento = builder.getTipoDocumento();
            complementoCFDI = builder.getComplemento();
            tipoPeticion = builder.getTipoPeticion();
            listaFolios = builder.getFolios();
            tipoURIRequest = builder.getTipoURIRequest();
            isMetadata = builder.getIsMetadata();
            credecialesContratacion = builder.getCredencialesContratacion();
            credecialesSAT = builder.getCredencialesSAT();
            folio = builder.Folio;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public User getCredencialesCS()
        {
            return credecialesContratacion;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public Credenciales getCredencialesSAT()
        {
            return credecialesSAT;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public bool getIsMetadata()
        {
            return isMetadata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public TipoPeticion getTipoPeticion()
        {
            return tipoPeticion;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public List<string> getFolios()
        {
            return listaFolios;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public string getFolio()
        {
            return folio;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public string getRfcBusqueda()
        {
            return rfcBusqueda;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public string getFechaInicio()
        {
            return DatetimeUtil.convertDateTimeToStringFormat_yyyy_MM_ddT00_00_00(fechaInicio);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public string getFechaFin()
        {
            return DatetimeUtil.convertDateTimeToStringFormat_yyyy_MM_ddT23_59_59(fechaFin);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public EstatusCFDI getEstatusCFDI()
        {
            return estatus;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public Movimiento getTipo()
        {
            return tipo;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public TipoDocumento getTipoDoc()
        {
            return tipoDocumento;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public string getSearch()
        {
            return this.search;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public string getComplemento()
        {
            return complementoCFDI;
        }
    }
}
