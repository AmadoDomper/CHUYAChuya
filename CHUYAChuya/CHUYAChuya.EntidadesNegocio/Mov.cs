using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CHUYAChuya.EntidadesNegocio
{
    public class Mov
    {
        private int _nMovNro;
        private string _cMovDesc;
        private Constante _oOpeTpo = new Constante();
        private string _cUsuario;
        private int _nMovAge;
        private int _nMovEst;
        private DateTime _dMovFecha;

        [JsonProperty(PropertyName = "nMovNro")]
        public int nMovNro
        {
            get { return _nMovNro; }
            set { _nMovNro = value; }
        }

        [JsonProperty(PropertyName = "cMovDesc")]
        public string cMovDesc
        {
            get { return _cMovDesc; }
            set { _cMovDesc = value; }
        }

        [JsonProperty(PropertyName = "oOpeTpo")]
        public Constante oOpeTpo
        {
            get { return _oOpeTpo; }
            set { _oOpeTpo = value; }
        }

        [JsonProperty(PropertyName = "cUsuario")]
        public string cUsuario
        {
            get { return _cUsuario; }
            set { _cUsuario = value; }
        }

        [JsonProperty(PropertyName = "nMovAge")]
        public int nMovAge
        {
            get { return _nMovAge; }
            set { _nMovAge = value; }
        }

        [JsonProperty(PropertyName = "nMovEst")]
        public int nMovEst
        {
            get { return _nMovEst; }
            set { _nMovEst = value; }
        }

        [JsonProperty(PropertyName = "dMovFecha")]
        public DateTime dMovFecha
        {
            get { return _dMovFecha; }
            set { _dMovFecha = value; }
        }

    }
}
