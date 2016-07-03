using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CHUYAChuya.EntidadesNegocio
{
    public class MovCaja
    {
        private string _dMovFecha;
        private string _cOpeDesc;
        private string _cMonIngreso;
        private string _cMonEgreso;
        private string _cMonActual;

        [JsonProperty(PropertyName = "dMovFec")]
        public string dMovFecha
        {
            get { return _dMovFecha; }
            set { _dMovFecha = value; }
        }

        [JsonProperty(PropertyName = "cOpeDesc")]
        public string cOpeDesc
        {
            get { return _cOpeDesc; }
            set { _cOpeDesc = value; }
        }

        [JsonProperty(PropertyName = "cMonIng")]
        public string cMonIngreso
        {
            get { return _cMonIngreso; }
            set { _cMonIngreso = value; }
        }

        [JsonProperty(PropertyName = "cMonEgr")]
        public string cMonEgreso
        {
            get { return _cMonEgreso; }
            set { _cMonEgreso = value; }
        }

        [JsonProperty(PropertyName = "cMonActl")]
        public string cMonActual
        {
            get { return _cMonActual; }
            set { _cMonActual = value; }
        }
    }
}
