using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CHUYAChuya.EntidadesNegocio
{
    public class Persona
    {
        private int _nPersId;
        private string _cPersDesc;
        private string _cPersTelefono1;
        private string _cPersTelefono2;
        private string _cPersEmail;
        private string _cPersDireccion;
        private Constante _oPersUbigeo = new Constante();


        private string _cPersSexo;
        private string _cPersDOI;
        private string _cPersTipo;


        [JsonProperty(PropertyName = "id")]
        public int nPersId
        {
            get { return _nPersId; }
            set { _nPersId = value; }
        }

        [JsonProperty(PropertyName = "nom")]
        public string cPersDesc
        {
            get { return _cPersDesc; }
            set { _cPersDesc = value; }
        }

        [JsonProperty(PropertyName = "tel1")]
        public string cPersTelefono1
        {
            get { return _cPersTelefono1; }
            set { _cPersTelefono1 = value; }
        }

        [JsonProperty(PropertyName = "tel2")]
        public string cPersTelefono2
        {
            get { return _cPersTelefono2; }
            set { _cPersTelefono2 = value; }
        }

        [JsonProperty(PropertyName = "mail")]
        public string cPersEmail
        {
            get { return _cPersEmail; }
            set { _cPersEmail = value; }
        }

        [JsonProperty(PropertyName = "direc")]
        public string cPersDireccion
        {
            get { return _cPersDireccion; }
            set { _cPersDireccion = value; }
        }

        [JsonProperty(PropertyName = "oUbigeo")]
        public Constante oPersUbigeo
        {
            get { return _oPersUbigeo; }
            set { _oPersUbigeo = value; }
        }

        [JsonProperty(PropertyName = "sexo")]
        public string cPersSexo
        {
            get { return _cPersSexo; }
            set { _cPersSexo = value; }
        }

        [JsonProperty(PropertyName = "doi")]
        public string cPersDOI
        {
            get { return _cPersDOI; }
            set { _cPersDOI = value; }
        }

        [JsonProperty(PropertyName = "tipo")]
        public string cPersTipo
        {
            get { return _cPersTipo; }
            set { _cPersTipo = value; }
        }


    }
}
