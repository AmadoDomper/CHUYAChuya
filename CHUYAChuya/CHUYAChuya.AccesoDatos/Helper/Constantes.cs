using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHUYAChuya.AccesoDatos.Helper
{
    public static class Procedimiento
    {

        #region Usuario
        public const string stp_sel_ObtenerUsuarioContrasena = "stp_sel_ObtenerUsuarioContrasena";
        #endregion

        #region Menu
        public const string stp_sel_RS_SeleccionarMenuFull_Web = "stp_sel_RS_SeleccionarMenuFull_Web";
        public const string stp_sel_RS_SeleccionarOperacionMenu_Web = "stp_sel_RS_SeleccionarOperacionMenu_Web";
        public const string stp_sel_RS_ListarOperaciones_Web = "stp_sel_RS_ListarOperaciones_Web";
        #endregion

        #region Persona
        public const string stp_sel_RS_getAllUsers = "stp_sel_RS_getAllUsers";
        #endregion
    }
}
