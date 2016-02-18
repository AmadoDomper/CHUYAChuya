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
        private string _cPersCod;
        private string _cPersNombre;
        private string _cPersSexo;
        private string _cNacionalidad; //Verificar
        private int _nResidente; //Verificar
        private int _nPersPersoneria; //Verificar
        //private CIIU _oCIIU;
        private string _cPersDireccDomicilio;
        private List<Documento> _ListaDocumento;


        private DateTime _dFecNacCreacion;
        private decimal _nIngPromedio;
        private string _cTelefono;
        private string _cCorreo;
        private Constante _EstCivil;
        private Constante _oCIIU;
        private Constante _Ubigeo;


        [JsonProperty(PropertyName = "id", Order = 0)]
        public string cPersCod
        {
            get { return _cPersCod; }
            set { _cPersCod = value; }
        }

        [JsonProperty(PropertyName = "nom", Order = 1)]
        public string cPersNombre
        {
            get { return _cPersNombre; }
            set { _cPersNombre = value; }
        }

        [JsonProperty(PropertyName = "direc", Order = 2)]
        public string cPersDireccDomicilio
        {
            get { return _cPersDireccDomicilio; }
            set { _cPersDireccDomicilio = value; }
        }

        [JsonProperty(PropertyName = "sexo", Order = 3)]
        public string cPersSexo
        {
            get { return _cPersSexo; }
            set { _cPersSexo = value; }
        }

        [JsonProperty(PropertyName = "nacion", Order = 4)]
        public string cNacionalidad
        {
            get { return _cNacionalidad; }
            set { _cNacionalidad = value; }
        }

        [JsonProperty(PropertyName = "resi", Order = 5)]
        public int nResidente
        {
            get { return _nResidente; }
            set { _nResidente = value; }
        }

        //public CIIU oCIIU
        //{
        //    get { return _oCIIU; }
        //    set { _oCIIU = value; }
        //}

        [JsonProperty(PropertyName = "lstdoc", Order = 6)]
        public List<Documento> ListaDocumento
        {
            get { return _ListaDocumento; }
            set { _ListaDocumento = value; }
        }

        [JsonProperty(PropertyName = "personeria", Order = 7)]
        public int nPersPersoneria
        {
            get { return _nPersPersoneria; }
            set { _nPersPersoneria = value; }
        }


        [JsonIgnore]
        public DateTime dFecNacCreacion
        {
            get { return _dFecNacCreacion; }
            set { _dFecNacCreacion = value; }
        }

        [JsonIgnore]
        public decimal nIngPromedio
        {
            get { return _nIngPromedio; }
            set { _nIngPromedio = value; }
        }

        [JsonIgnore]
        public string cTelefono
        {
            get { return _cTelefono; }
            set { _cTelefono = value; }
        }
        [JsonIgnore]
        public string cCorreo
        {
            get { return _cCorreo; }
            set { _cCorreo = value; }
        }
        [JsonIgnore]
        public Constante EstCivil
        {
            get { return _EstCivil; }
            set { _EstCivil = value; }
        }
        [JsonProperty(PropertyName = "oCIIU")]
        public Constante oCIIU
        {
            get { return _oCIIU; }
            set { _oCIIU = value; }
        }
        [JsonIgnore]
        public Constante Ubigeo
        {
            get { return _Ubigeo; }
            set { _Ubigeo = value; }
        }
    }
}
