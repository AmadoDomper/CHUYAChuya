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
        public const string stp_sel_ListarClientes = "stp_sel_ListarClientes";
        #endregion


        #region PersonaNat
        public const string stp_ins_upd_ClienteNatural = "stp_ins_upd_ClienteNatural";
        public const string stp_sel_ClienteNatural = "stp_sel_ClienteNatural";
        #endregion

        #region PersonaJur
        public const string stp_ins_upd_ClienteJuridico = "stp_ins_upd_ClienteJuridico";
        public const string stp_sel_ClienteJuridico = "stp_sel_ClienteJuridico";
        #endregion

    }
}
