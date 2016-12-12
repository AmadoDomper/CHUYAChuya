using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CHUYAChuya.EntidadesNegocio
{
    public class TicketCierre
    {
        private int _nCajaCaId;
        private DateTime _dFechaApertura;
        private DateTime _dFechaCierre;
        private string _cUsuario;
        private decimal _nContado;
        private decimal _nDiferencia;

        private decimal _nCajaInicio;
        private decimal _nCajaEntEfec;
        private decimal _nEntTotal;

        private decimal _nCajaNotaAnticipo;
        private decimal _nCajaNotaPagadas;
        private decimal _nPagoProv;
        private decimal _nSalidaOtro;
        private decimal _nAnula;
        private decimal _nCajaTotal;

        [JsonProperty(PropertyName = "nCajaCaId")]
        public int nCajaCaId
        {
            get { return _nCajaCaId; }
            set { _nCajaCaId = value; }
        }

        [JsonProperty(PropertyName = "dFechaApertura")]
        public DateTime dFechaApertura
        {
            get { return _dFechaApertura; }
            set { _dFechaApertura = value; }
        }

        [JsonProperty(PropertyName = "dFechaCierre")]
        public DateTime dFechaCierre
        {
            get { return _dFechaCierre; }
            set { _dFechaCierre = value; }
        }

        [JsonProperty(PropertyName = "cUsuario")]
        public string cUsuario
        {
            get { return _cUsuario; }
            set { _cUsuario = value; }
        }

        [JsonProperty(PropertyName = "nContado")]
        public decimal nContado
        {
            get { return _nContado; }
            set { _nContado = value; }
        }

        [JsonProperty(PropertyName = "nDiferencia")]
        public decimal nDiferencia
        {
            get { return _nDiferencia; }
            set { _nDiferencia = value; }
        }

        [JsonProperty(PropertyName = "nCajaInicio")]
        public decimal nCajaInicio
        {
            get { return _nCajaInicio; }
            set { _nCajaInicio = value; }
        }

        [JsonProperty(PropertyName = "nCajaEntEfec")]
        public decimal nCajaEntEfec
        {
            get { return _nCajaEntEfec; }
            set { _nCajaEntEfec = value; }
        }

        [JsonProperty(PropertyName = "nEntTotal")]
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

    }
}
