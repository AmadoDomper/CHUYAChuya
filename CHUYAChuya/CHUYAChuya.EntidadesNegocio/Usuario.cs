using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHUYAChuya.EntidadesNegocio
{
    public class Usuario
    {
        private Persona _oDatoPersona;
        private string _cNomUsuario;
        private string _cCargo;
        private string _cCodAge; 
        private string _NombrePC;
        private string _IpPC;

        public Persona oDatoPersona
        {
            get { return _oDatoPersona; }
            set { _oDatoPersona = value; }
        }

        public string cNomUsuario
        {
            get { return _cNomUsuario; }
            set { _cNomUsuario = value; }
        }

        public string cCargo
        {
            get { return _cCargo; }
            set { _cCargo = value; }
        }

        public string cCodAge
        {
            get { return _cCodAge; }
            set { _cCodAge = value; }
        }

        public string NombrePC
        {
            get { return _NombrePC; }
            set { _NombrePC = value; }
        }

        public string IpPC
        {
            get { return _IpPC; }
            set { _IpPC = value; }
        }
    }
}
