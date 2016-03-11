using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CHUYAChuya.AccesoDatos;
using CHUYAChuya.EntidadesNegocio;

namespace CHUYAChuya.LogicaNegocio
{
    public class PersonaJurLN
    {
        PersonaJurAD oPersonaJurAD;

        public PersonaJurLN()
        {
            oPersonaJurAD = new PersonaJurAD();
        }

        public int RegistrarActualizarPersJuridico(PersonaJur oPersJur)
        {
            return oPersonaJurAD.RegistrarActualizarPersJuridico(oPersJur);
        }

        public PersonaJur CargarDatosClienteJuridico(int nPersId)
        {
            return oPersonaJurAD.CargarDatosClienteJuridico(nPersId);
        }
    }
}
