using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHUYAChuya.AccesoDatos.Commons
{
    public enum RolesPermisos
    {
        #region Nota de Entrega
            NotaE_Crear_Nueva_Nota_de_Entrega = 1,
            NotaE_Editar_Nota_de_Entrega = 2,
            NotaE_Cobrar_Nota_de_Entrega = 3,
            NotaE_Confirmar_Entrega = 4,
            NotaE_Anular_Nota_de_Entrega = 5,
            NotaE_Anular_Comprobante = 6,
        #endregion

        #region Clientes
            Clie_Crear_Nuevo_Cliente = 7,
            Clie_Editar_Cliente = 8,
            Clie_Eliminar_Cliente = 9,
        #endregion

        #region Usuarios
            Usu_Nuevo_Usuario = 10,
            Usu_Editar_Usuario = 11,
            Usu_Eliminar_Usuario = 12,
        #endregion

        #region Producto
            Pro_Nuevo_Producto = 13,
            Pro_Editar_Producto = 14,
            Pro_Eliminar_Producto = 15,
        #endregion

        #region Configuración
            Con_Agregar_Rol = 16,
            Con_Editar_Rol = 17,
            Con_Eliminar_Rol = 18,
        #endregion

        #region Caja
            Caja_Apertura_de_Caja = 19,
            Caja_Salida_Efectivo = 20,
            Caja_Entrada_Efectivo = 21,
        #endregion

        #region Corte de Caja
            CorCaja_Realizar_Cierre = 22,
            CorCaja_Realizar_Cierre_Final = 23,
        #endregion

        #region Reporte
            Rep_Exportar = 24,
        #endregion

    }
}
