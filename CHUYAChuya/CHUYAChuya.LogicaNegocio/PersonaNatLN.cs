using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CHUYAChuya.AccesoDatos;
using CHUYAChuya.EntidadesNegocio;

namespace CHUYAChuya.LogicaNegocio
{
    public class PersonaNatLN
    {
        PersonaNatAD oPersonaNatAD;

        public PersonaNatLN()
        {
            oPersonaNatAD = new PersonaNatAD();
        }

        public int RegistrarActualizarPersNatural(PersonaNat oPersNat)
        {
            return oPersonaNatAD.RegistrarActualizarPersNatural(oPersNat);
        }


        public PersonaNat CargarDatosClienteNatural(int nPersId)
        {
            return oPersonaNatAD.CargarDatosClienteNatural(nPersId);
        }


    }
}
