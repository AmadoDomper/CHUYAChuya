using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CHUYAChuya.EntidadesNegocio
{
    public class Cierre
    {
    /*ENTRADA DE EFECTIVO*/
        private decimal	_nCajaInicio;
		private decimal	_nCajaEntradaEfectivo;
		private decimal	_nEntTotal;
	/*TOTAL EN CAJA*/
		private decimal	_nCajaNotaAnticipo;
		private decimal	_nCajaNotaPagadas;
		private decimal	_nPagoProv;
		private decimal	_nSalidaOtro;
        private decimal _nAnula;
        private decimal _nCajaTotal;
        List<PagoProveedores> _oListaPagoProv = new List<PagoProveedores>();
        List<NotaEntregaL> _oListaNotaPag = new List<NotaEntregaL>();
        List<NotaEntregaL> _oListaNotAnt = new List<NotaEntregaL>();


        [JsonProperty(PropertyName = "nCajaIni")]
        public decimal nCajaInicio
        {
            get { return _nCajaInicio; }
            set { _nCajaInicio = value; }
        }

        [JsonProperty(PropertyName = "nCajaEntEfec")]
        public decimal nCajaEntradaEfectivo
        {
            get { return _nCajaEntradaEfectivo; }
            set { _nCajaEntradaEfectivo = value; }
        }

        [JsonProperty(PropertyName = "nEntTot")]
        public decimal nEntTotal
        {
            get { return _nEntTotal; }
            set { _nEntTotal = value; }
        }

        [JsonProperty(PropertyName = "nCajaNotaAnt")]
        public decimal nCajaNotaAnticipo
        {
            get { return _nCajaNotaAnticipo; }
            set { _nCajaNotaAnticipo = value; }
        }

        [JsonProperty(PropertyName = "nCajaNotaPag")]
        public decimal nCajaNotaPagadas
        {
            get { return _nCajaNotaPagadas; }
            set { _nCajaNotaPagadas = value; }
        }

        [JsonProperty(PropertyName = "nPagoProv")]
        public decimal nPagoProv
        {
            get { return _nPagoProv; }
            set { _nPagoProv = value; }
        }

        [JsonProperty(PropertyName = "nSalOtro")]
        public decimal nSalidaOtro
        {
            get { return _nSalidaOtro; }
            set { _nSalidaOtro = value; }
        }

        [JsonProperty(PropertyName = "nAnula")]
        public decimal nAnula
        {
            get { return _nAnula; }
            set { _nAnula = value; }
        }

        [JsonProperty(PropertyName = "nCajaTot")]
        public decimal nCajaTotal
        {
            get { return _nCajaTotal; }
            set { _nCajaTotal = value; }
        }

        [JsonProperty(PropertyName = "oLisPagoProv")]
        public List<PagoProveedores> oListaPagoProv
        {
            get { return _oListaPagoProv; }
            set { _oListaPagoProv = value; }
        }

        [JsonProperty(PropertyName = "oLisNotaPag")]
        public List<NotaEntregaL> oListaNotaPag
        {
            get { return _oListaNotaPag; }
            set { _oListaNotaPag = value; }
        }

        [JsonProperty(PropertyName = "oLisNotAnt")]
        public List<NotaEntregaL> oListaNotAnt
        {
            get { return _oListaNotAnt; }
            set { _oListaNotAnt = value; }
        }
    }
}
