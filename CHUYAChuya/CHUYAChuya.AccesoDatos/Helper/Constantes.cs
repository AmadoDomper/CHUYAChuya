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

        #region Constante
        public const string stp_sel_constante = "stp_sel_constante";
        #endregion

        #region Menu
        public const string stp_sel_RS_SeleccionarMenuFull_Web = "stp_sel_RS_SeleccionarMenuFull_Web";
        public const string stp_sel_RS_SeleccionarOperacionMenu_Web = "stp_sel_RS_SeleccionarOperacionMenu_Web";
        public const string stp_sel_RS_ListarOperaciones_Web = "stp_sel_RS_ListarOperaciones_Web";
        #endregion

        #region Persona
        public const string stp_sel_RS_getAllUsers = "stp_sel_RS_getAllUsers";
        public const string stp_sel_ListarClientes = "stp_sel_ListarClientes";
        public const string stp_sel_BuscarClientes = "stp_sel_BuscarClientes";
        public const string stp_sel_BuscarProveedores = "stp_sel_BuscarProveedores";
        #endregion

        #region PersonaNat
        public const string stp_ins_upd_ClienteNatural = "stp_ins_upd_ClienteNatural";
        public const string stp_sel_ClienteNatural = "stp_sel_ClienteNatural";
        #endregion

        #region PersonaJur
        public const string stp_ins_upd_ClienteJuridico = "stp_ins_upd_ClienteJuridico";
        public const string stp_sel_ClienteJuridico = "stp_sel_ClienteJuridico";
        #endregion

        #region Usuarios
        public const string stp_sel_ListarUsuarios = "stp_sel_ListarUsuarios";
        public const string stp_sel_Usuario = "stp_sel_Usuario";
        public const string stp_ins_upd_Usuario = "stp_ins_upd_Usuario";
        public const string stp_sel_Usuarios = "stp_sel_Usuarios";
        #endregion

        #region Producto
        public const string stp_sel_ListarProductos = "stp_sel_ListarProductos";
        public const string stp_sel_Producto = "stp_sel_Producto";
        public const string stp_ins_upd_Producto = "stp_ins_upd_Producto";
        public const string stp_sel_BuscarProductos = "stp_sel_BuscarProductos";
        #endregion

        #region NotaEntrega
        public const string stp_ins_NotaEntrega = "stp_ins_NotaEntrega";
        public const string stp_sel_BuscarNotaEntregas = "stp_sel_BuscarNotaEntregas";
        public const string stp_sel_CargarNotaEntrega = "stp_sel_CargarNotaEntrega";
        public const string stp_sel_ListaNotaEntProductos = "stp_sel_ListaNotaEntProductos";
        #endregion

        #region Caja
        public const string stp_ins_SalidaEfectivoPagoProveedor = "stp_ins_SalidaEfectivoPagoProveedor";
        public const string stp_ins_SalidaEfectivoOtros = "stp_ins_SalidaEfectivoOtros";
        public const string stp_ins_IngresoEfectivo = "stp_ins_IngresoEfectivo";
        public const string stp_sel_BuscarMovCaja = "stp_sel_BuscarMovCaja";
        #endregion


        #region CorteDeCaja
        public const string stp_sel_ListaNotasAnticipo = "stp_sel_ListaNotasAnticipo";
        public const string stp_sel_ListaNotasPagadas = "stp_sel_ListaNotasPagadas";
        public const string stp_sel_ListaPagoProveedores = "stp_sel_ListaPagoProveedores";
        public const string stp_sel_ListaDetalleCorte = "stp_sel_ListaDetalleCorte";
        #endregion

        #region AperturaCaja
        public const string stp_ins_AperturarCaja = "stp_ins_AperturarCaja";
        #endregion

    }
}
