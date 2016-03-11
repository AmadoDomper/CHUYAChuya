using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CHUYAChuya.EntidadesNegocio
{
    public class PersonaJur
    {
        private Persona _oPers = new Persona();
        private string _cPersJurEmpresa;
        private string _cPersJurRep;
        private string _cPersJurRUC;
        private DateTime _dPersJurFecConst;
        private Constante _oPersJurActividad = new Constante();

        [JsonProperty(PropertyName = "oPers")]
        public Persona oPers
        {
            get { return _oPers; }
            set { _oPers = value; }
        }

        [JsonProperty(PropertyName = "cEmpresa")]
        public string cPersJurEmpresa
        {
            get { return _cPersJurEmpresa; }
            set { _cPersJurEmpresa = value; }
        }

        [JsonProperty(PropertyName = "cRep")]
        public string cPersJurRep
        {
            get { return _cPersJurRep; }
            set { _cPersJurRep = value; }
        }

        [JsonProperty(PropertyName = "cRUC")]
        public string cPersJurRUC
        {
            get { return _cPersJurRUC; }
            set { _cPersJurRUC = value; }
        }

        [JsonProperty(PropertyName = "dConst")]
        public DateTime dPersJurFecConst
        {
            get { return _dPersJurFecConst; }
            set { _dPersJurFecConst = value; }
        }

        [JsonProperty(PropertyName = "oAct")]
        public Constante oPersJurActividad
        {
            get { return _oPersJurActividad; }
            set { _oPersJurActividad = value; }
        }




    }
}
