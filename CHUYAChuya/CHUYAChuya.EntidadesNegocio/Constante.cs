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
        private string _cConstanteID;
        private string _cNombre;
        private List<Constante> _oSubConstante = new List<Constante>();
        private bool _bEstado;
        private int _nMin;

        [JsonProperty(PropertyName = "id")]
        public string cConstanteID
        {
            get { return _cConstanteID; }
            set { _cConstanteID = value; }
        }

        [JsonProperty(PropertyName = "nom")]
        public string cNombre
        {
            get { return _cNombre; }
            set { _cNombre = value; }
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
            get { return _bEstado; }
            set { _bEstado = value; }
        }

        //[JsonProperty(PropertyName = "min")]
        //public int nMin
        //{
        //    get { return _nMin; }
        //    set { _nMin = value; }
        //}
    }
}
