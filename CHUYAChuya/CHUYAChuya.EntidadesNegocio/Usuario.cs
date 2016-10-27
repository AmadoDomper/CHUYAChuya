using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CHUYAChuya.EntidadesNegocio
{
    public class Usuario
    {
        private PersonaNat _oPersNat = new PersonaNat();
        private int _nUsuId;
        private string _cUsuNombre;
        private string _cUsuContrasena;
        private int _nRolId;
        //private string _cCargo;
        //private string _cCodAge;

        [JsonProperty(PropertyName = "oPersNat")]
        public PersonaNat oPersNat
        {
            get { return _oPersNat; }
            set { _oPersNat = value; }
        }

        [JsonProperty(PropertyName = "nUsuId")]
        public int nUsuId
        {
            get { return _nUsuId; }
            set { _nUsuId = value; }
        }

        [JsonProperty(PropertyName = "cUsuNom")]
        public string cUsuNombre
        {
            get { return _cUsuNombre; }
            set { _cUsuNombre = value; }
        }

        [JsonProperty(PropertyName = "cUsuCon")]
        public string cUsuContrasena
        {
            get { return _cUsuContrasena; }
            set { _cUsuContrasena = value; }
        }

        [JsonProperty(PropertyName = "nRolId")]
        public int nRolId
        {
            get { return _nRolId; }
            set { _nRolId = value; }
        }

        //public string cCargo
        //{
        //    get { return _cCargo; }
        //    set { _cCargo = value; }
        //}

        //public string cCodAge
        //{
        //    get { return _cCodAge; }
        //    set { _cCodAge = value; }
        //}

        //public string NombrePC
        //{
        //    get { return _NombrePC; }
        //    set { _NombrePC = value; }
        //}

        //public string IpPC
        //{
        //    get { return _IpPC; }
        //    set { _IpPC = value; }
        //}
    }
}
