using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CHUYAChuya.EntidadesNegocio
{
    public class Producto
    {
        private int _nProdId;
        private string _cProdDesc;
        private decimal _nProdPrecioUnit;
        private Constante _oProdMedida = new Constante();
        private Boolean _bProdSerLavado;
        private Boolean _bProdSerSecado;
        private Boolean _bProdSerPlanchado;
        private string _cProdUsuReg;
        private DateTime _dProdReg;
        private Boolean _bProdOtros;

        [JsonProperty(PropertyName = "nPrId")]
        public int nProdId
        {
            get { return _nProdId; }
            set { _nProdId = value; }
        }

        [JsonProperty(PropertyName = "cPrDesc")]
        public string cProdDesc
        {
            get { return _cProdDesc; }
            set { _cProdDesc = value; }
        }

        [JsonProperty(PropertyName = "nPrPrecU")]
        public decimal nProdPrecioUnit
        {
            get { return _nProdPrecioUnit; }
            set { _nProdPrecioUnit = value; }
        }

        [JsonProperty(PropertyName = "oPrMed")]
        public Constante oProdMedida
        {
            get { return _oProdMedida; }
            set { _oProdMedida = value; }
        }

        [JsonProperty(PropertyName = "bPrSerLav")]
        public Boolean bProdSerLavado
        {
            get { return _bProdSerLavado; }
            set { _bProdSerLavado = value; }
        }

        [JsonProperty(PropertyName = "bPrSerSec")]
        public Boolean bProdSerSecado
        {
            get { return _bProdSerSecado; }
            set { _bProdSerSecado = value; }
        }

        [JsonProperty(PropertyName = "bPrSerPla")]
        public Boolean bProdSerPlanchado
        {
            get { return _bProdSerPlanchado; }
            set { _bProdSerPlanchado = value; }
        }

        [JsonProperty(PropertyName = "cPrUsuReg")]
        public string cProdUsuReg
        {
            get { return _cProdUsuReg; }
            set { _cProdUsuReg = value; }
        }

        [JsonProperty(PropertyName = "dPrReg")]
        public DateTime dProdReg
        {
            get { return _dProdReg; }
            set { _dProdReg = value; }
        }

        [JsonProperty(PropertyName = "bProdO")]
        public Boolean bProdOtros
        {
            get { return _bProdOtros; }
            set { _bProdOtros = value; }
        }

    }
}
