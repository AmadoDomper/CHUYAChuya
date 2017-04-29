using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CHUYAChuya.EntidadesNegocio
{
    public class PagoProveedores
    {
        private DateTime _dMovFecha;
        private string _cMovFecha;
        private string _cPersDesc;
        private decimal _nMonto;

        [JsonProperty(PropertyName = "dMovFecha")]
        public DateTime dMovFecha
        {
            get { return _dMovFecha; }
            set { _dMovFecha = value; }
        }

        [JsonProperty(PropertyName = "cMovFecha")]
        public string cMovFecha
        {
            get { return _cMovFecha; }
            set { _cMovFecha = value; }
        }

        [JsonProperty(PropertyName = "cPersDesc")]
        public string cPersDesc
        {
            get { return _cPersDesc; }
            set { _cPersDesc = value; }
        }

        [JsonProperty(PropertyName = "nMonto")]
        public decimal nMonto
        {
            get { return _nMonto; }
            set { _nMonto = value; }
        }
    }
}
