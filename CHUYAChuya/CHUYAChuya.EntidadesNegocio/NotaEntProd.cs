using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CHUYAChuya.EntidadesNegocio
{
    public class NotaEntProd
    {
        private Producto _oProd = new Producto();
        private decimal _nDetCantidad;
        private decimal _nDetProdPrecioUnit;
        private decimal _nDetImporte;

        [JsonProperty(PropertyName = "oProd")]
        public Producto oProd
        {
            get { return _oProd; }
            set { _oProd = value; }
        }

        [JsonProperty(PropertyName = "nDetCant")]
        public decimal nDetCantidad
        {
            get { return _nDetCantidad; }
            set { _nDetCantidad = value; }
        }

        [JsonProperty(PropertyName = "nPrPrecU")]
        public decimal nProdPrecioUnit
        {
            get { return _nDetProdPrecioUnit; }
            set { _nDetProdPrecioUnit = value; }
        }

        [JsonProperty(PropertyName = "nDetImp")]
        public decimal nDetImporte
        {
            get { return _nDetImporte; }
            set { _nDetImporte = value; }
        }



    }
}
