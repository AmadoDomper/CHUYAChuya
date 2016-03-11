using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CHUYAChuya.EntidadesNegocio
{
    public class PersonaNat
    {
        private Persona _oPers = new Persona();
        private string _cPersNatNombre;
        private string _cPersNatApellido;
        private string _cPersNatDOI;
        private DateTime _dPersNatNac;
        private Constante _oPersNatSexo = new Constante();

        [JsonProperty(PropertyName = "oPers")]
        public Persona oPers
        {
            get { return _oPers; }
            set { _oPers = value; }
        }

        [JsonProperty(PropertyName = "cNom")]
        public string cPersNatNombre
        {
            get { return _cPersNatNombre; }
            set { _cPersNatNombre = value; }
        }

        [JsonProperty(PropertyName = "cApe")]
        public string cPersNatApellido
        {
            get { return _cPersNatApellido; }
            set { _cPersNatApellido = value; }
        }

        [JsonProperty(PropertyName = "cDOI")]
        public string cPersNatDOI
        {
            get { return _cPersNatDOI; }
            set { _cPersNatDOI = value; }
        }

        [JsonProperty(PropertyName = "dNac")]
        public DateTime dPersNatNac
        {
            get { return _dPersNatNac; }
            set { _dPersNatNac = value; }
        }

        [JsonProperty(PropertyName = "oSexo")]
        public Constante oPersNatSexo
        {
            get { return _oPersNatSexo; }
            set { _oPersNatSexo = value; }
        }
    }
}
