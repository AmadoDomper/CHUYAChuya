using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHUYAChuya.EntidadesNegocio
{
    public class Documento
    {
        private string _cPersIDnro;
        private Constante _oPersIDTpo;

        [JsonProperty(PropertyName = "tpo ", Order = 0)]
        public Constante oPersIDTpo
        {
            get { return _oPersIDTpo; }
            set { _oPersIDTpo = value; }
        }

        [JsonProperty(PropertyName = "num", Order = 1)]
        public string cPersIDnro
        {
            get { return _cPersIDnro; }
            set { _cPersIDnro = value; }
        }
    }
}
