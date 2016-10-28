using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Threading.Tasks;
using CHUYAChuya.EntidadesNegocio;

namespace CHUYAChuya.AccesoDatos.Commons
{
    public class FrontUser
    {
        public static bool TienePermiso(RolesPermisos valor)
        {
            return new RolAD().ValidaSugAprobacion(((Usuario)HttpContext.Current.Session["Datos"]).cUsuNombre, (int)valor);
        }
    }
}
