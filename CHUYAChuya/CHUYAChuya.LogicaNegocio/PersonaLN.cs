using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CHUYAChuya.AccesoDatos;
using CHUYAChuya.EntidadesNegocio;

namespace CHUYAChuya.LogicaNegocio
{
    public class PersonaLN
    {
        PersonaAD oPersonaAD;

        public PersonaLN()
        {
            oPersonaAD = new PersonaAD();
        }

        public List<Array> getAllUsers()
        {
            try
            {
                return oPersonaAD.getAllUsers();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Persona> ListaClientes()
        {
            return oPersonaAD.ListaClientes();
        }


        public List<Persona> BuscarClientes(string cPersDOI, string cNombre)
        {
            return oPersonaAD.BuscarClientes(cPersDOI, cNombre);
        }



    }
}
