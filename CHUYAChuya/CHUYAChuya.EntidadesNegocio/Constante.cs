using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CHUYAChuya.EntidadesNegocio
{
    public class Constante
    {
        private string _ConstanteID;
        private string _Nombre;
        private List<Constante> _oSubConstante;
        private bool _Estado;
        private int _nMin;

        [JsonProperty(PropertyName = "id")]
        public string ConstanteID
        {
            get { return _ConstanteID; }
            set { _ConstanteID = value; }
        }

        [JsonProperty(PropertyName = "nom")]
        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        [JsonProperty(PropertyName = "sub")]
        public List<Constante> oSubConstante
        {
            get { return _oSubConstante; }
            set { _oSubConstante = value; }
        }

        [JsonIgnore]
        public bool Estado
        {
            get { return _Estado; }
            set { _Estado = value; }
        }

        [JsonProperty(PropertyName = "min")]
        public int nMin
        {
            get { return _nMin; }
            set { _nMin = value; }
        }
    }
}
