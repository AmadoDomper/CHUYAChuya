using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CHUYAChuya.EntidadesNegocio;
using CHUYAChuya.AccesoDatos;

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

        public ListaPaginada ListaClientesPag(int nPage, int nSize, int nCliId = -1, string cCliDesc = null, string cCliDOI = null)
        {
            return oPersonaAD.ListaClientesPag(nPage, nSize, nCliId, cCliDesc, cCliDOI);
        }

        public List<Persona> BuscarClientes(string cPersDOI, string cNombre)
        {
            return oPersonaAD.BuscarClientes(cPersDOI, cNombre);
        }

        public List<Persona> BuscarProveedores(string cProvRUC, string cNombre)
        {
            return oPersonaAD.BuscarProveedores(cProvRUC, cNombre);
        }

        public int EliminarCliente(int nPersId)
        {
            return oPersonaAD.EliminarCliente(nPersId);
        }

    }
}
