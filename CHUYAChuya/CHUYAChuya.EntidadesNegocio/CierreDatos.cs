using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CHUYAChuya.EntidadesNegocio
{
    public class CierreDatos
    {
        private DateTime _dFecha;
        private decimal _nCalculado;
        private Byte _nEstado;
        private List<UsuarioCierres> _oListUsuarioCierres = new List<UsuarioCierres>();

        [JsonProperty(PropertyName = "dFecha")]
        public DateTime dFecha
        {
            get { return _dFecha; }
            set { _dFecha = value; }
        }

        [JsonProperty(PropertyName = "nCalculado")]
        public decimal nCalculado
        {
            get { return _nCalculado; }
            set { _nCalculado = value; }
        }

        [JsonProperty(PropertyName = "nEstado")]
        public Byte nEstado
        {
            get { return _nEstado; }
            set { _nEstado = value; }
        }

        [JsonProperty(PropertyName = "oListUC")]
        public List<UsuarioCierres> oListUsuarioCierres
        {
            get { return _oListUsuarioCierres; }
            set { _oListUsuarioCierres = value; }
        }



    }
}
