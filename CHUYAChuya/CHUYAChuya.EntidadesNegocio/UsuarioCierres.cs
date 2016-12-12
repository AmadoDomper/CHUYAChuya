using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace CHUYAChuya.EntidadesNegocio
{
    public class UsuarioCierres
    {
        private string _cUsuario;
        private List<Cierres> _oListCierres = new List<Cierres>();

        [JsonProperty(PropertyName = "cUsuario")]
        public string cUsuario
        {
            get { return _cUsuario; }
            set { _cUsuario = value; }
        }

        [JsonProperty(PropertyName = "oListCie")]
        public List<Cierres> oListCierres
        {
            get { return _oListCierres; }
            set { _oListCierres = value; }
        }

    }
}
