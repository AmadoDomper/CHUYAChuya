using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace CHUYAChuya.EntidadesNegocio
{
    public class Impresora
    {
        private int _nImpId;
        private string _nImpSerie;

        [JsonProperty(PropertyName = "nImpId")]
        public int nImpId
        {
            get { return _nImpId; }
            set { _nImpId = value; }
        }

        [JsonProperty(PropertyName = "nImpSerie")]
        public string nImpSerie
        {
            get { return _nImpSerie; }
            set { _nImpSerie = value; }
        }

    }
}
