using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace CHUYAChuya.EntidadesNegocio.Reportes
{
    public class RepNotaEntregas
    {

        private string _dFechaReg;
        private int _nNotaEntId;
        private string _cPersDesc ;
        private string _cProdDesc;
        private Boolean _bProdSerLavado;
        private Boolean _bProdSerSecado;
        private Boolean _bProdSerPlanchado;
        private string _cServicios;
        private string _cPrenda;
        private string _cPeso;
        private string _cDetPrecioUnit;
        private string _cDetImporte;
        private string _cPendiente;
        private string _cPagado;
        private string _cEntregado;
        private string _cAnulado;
        private string _cBoleta;
        private string _cFactura;
        private string _dFechaPago;

        [JsonProperty(PropertyName = "dFecReg")]
        public string dFechaReg
        {
            get { return _dFechaReg; }
            set { _dFechaReg = value; }
        }

        [JsonProperty(PropertyName = "nNotaId")]
        public int nNotaEntId
        {
            get { return _nNotaEntId; }
            set { _nNotaEntId = value; }
        }

        [JsonProperty(PropertyName = "cPersDesc")]
        public string cPersDesc
        {
            get { return _cPersDesc; }
            set { _cPersDesc = value; }
        }

        [JsonProperty(PropertyName = "cProdDesc")]
        public string cProdDesc
        {
            get { return _cProdDesc; }
            set { _cProdDesc = value; }
        }

        [JsonProperty(PropertyName = "bProdLav")]
        public Boolean bProdSerLavado
        {
            get { return _bProdSerLavado; }
            set { _bProdSerLavado = value; }
        }

        [JsonProperty(PropertyName = "bProdSec")]
        public Boolean bProdSerSecado
        {
            get { return _bProdSerSecado; }
            set { _bProdSerSecado = value; }
        }

        [JsonProperty(PropertyName = "bProdPla")]
        public Boolean bProdSerPlanchado
        {
            get { return _bProdSerPlanchado; }
            set { _bProdSerPlanchado = value; }
        }

        [JsonProperty(PropertyName = "cServ")]
        public string cServicios
        {
            get { return _cServicios; }
            set { _cServicios = value; }
        }

        [JsonProperty(PropertyName = "cPrenda")]
        public string cPrenda
        {
            get { return _cPrenda; }
            set { _cPrenda = value; }
        }

        [JsonProperty(PropertyName = "cPeso")]
        public string cPeso
        {
            get { return _cPeso; }
            set { _cPeso = value; }
        }

        [JsonProperty(PropertyName = "cDetP")]
        public string cDetPrecioUnit
        {
            get { return _cDetPrecioUnit; }
            set { _cDetPrecioUnit = value; }
        }

        [JsonProperty(PropertyName = "cDetI")]
        public string cDetImporte
        {
            get { return _cDetImporte; }
            set { _cDetImporte = value; }
        }

        [JsonProperty(PropertyName = "cPend")]
        public string cPendiente
        {
            get { return _cPendiente; }
            set { _cPendiente = value; }
        }

        [JsonProperty(PropertyName = "cPag")]
        public string cPagado
        {
            get { return _cPagado; }
            set { _cPagado = value; }
        }

        [JsonProperty(PropertyName = "cEnt")]
        public string cEntregado
        {
            get { return _cEntregado; }
            set { _cEntregado = value; }
        }

        [JsonProperty(PropertyName = "cAnul")]
        public string cAnulado
        {
            get { return _cAnulado; }
            set { _cAnulado = value; }
        }

        [JsonProperty(PropertyName = "cBole")]
        public string cBoleta
        {
            get { return _cBoleta; }
            set { _cBoleta = value; }
        }

        [JsonProperty(PropertyName = "cFact")]
        public string cFactura
        {
            get { return _cFactura; }
            set { _cFactura = value; }
        }

        [JsonProperty(PropertyName = "dFecPag")]
        public string dFechaPago
        {
            get { return _dFechaPago; }
            set { _dFechaPago = value; }
        }
    }
}
