using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CHUYAChuya.AccesoDatos;
using CHUYAChuya.EntidadesNegocio;

namespace CHUYAChuya.LogicaNegocio
{
    public class UsuarioLN
    {
        UsuarioAD oUsuarioAD;

        public UsuarioLN()
        {
            oUsuarioAD = new UsuarioAD();
        }

        public ListaPaginada ListaUsuariosPag(int nPage = 1, int nSize = 10, int nUsuId = -1, string cUsuDesc = null, string cUsuName = null, string cUsuDOI = null)
        {
            return oUsuarioAD.ListaUsuariosPag(nPage, nSize, nUsuId, cUsuDesc, cUsuName, cUsuDOI);
        }

        public List<Constante> Usuarios()
        {
            return oUsuarioAD.Usuarios();
        }

        public Usuario CargarDatosUsuario(int nPersId, string cDNI)
        {
            return oUsuarioAD.CargarDatosUsuario(nPersId,cDNI);
        }

        public int RegistrarActualizarUsuario(Usuario oUsuario)
        {
            return oUsuarioAD.RegistrarUsuario(oUsuario);
        }


    }
}
