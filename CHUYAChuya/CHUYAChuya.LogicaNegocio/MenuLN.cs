using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CHUYAChuya.AccesoDatos;
using CHUYAChuya.EntidadesNegocio;

namespace CHUYAChuya.LogicaNegocio
{
    public class MenuLN
    {
        MenuAD oMenuAD;

        public MenuLN()
        {
            oMenuAD = new MenuAD();
        }

        public List<Menu> ObtenerMenusFull(int nRolId)
        {
            try
            {
                return oMenuAD.ObtenerMenuFull(nRolId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Menu> ObtenerMenusOperacion(string NombreUsuario, string Grupos, string MenuPadreId)
        {
            try
            {
                return oMenuAD.ObtenerMenuOperacion(NombreUsuario, Grupos, MenuPadreId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Menu> ObtenerOperaciones(string Tipo)
        {
            try
            {
                return oMenuAD.ObtenerOperaciones(Tipo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //public List<Menu> ObtenerOperacionesUsuarioGrupo(string Grupo, string Tipo)
        //{
        //    try
        //    {
        //        string Usuario = "";
        //        string GruposUsuario = "";

        //        int iUsuario = -1;
        //        iUsuario = Grupo.IndexOf("UserCMACM");

        //        if (iUsuario == 0)
        //        {
        //            iUsuario = 9;
        //            GruposUsuario = Grupo.Substring(iUsuario + 5, Grupo.Length - (iUsuario + 5));
        //            Usuario = Grupo.Substring(iUsuario, 4);
        //        }
        //        else
        //        {
        //            GruposUsuario = Grupo;
        //        }

        //        return oMenuAD.obtenerOperacionesUsuarioGrupo(Usuario, GruposUsuario, Tipo);
        //    }
        //    catch (Exception ex) { throw new Exception(ex.Message); }
        //}

        //public List<Menu> ObtenerOperacionesFull(string Tipo)
        //{
        //    try
        //    {
        //        return oMenuAD.obtenerOperacionesFull(Tipo);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        //public void InsertarOperaciones(string Grupo, List<int> id_MenuArray, List<int> id_OpcionMenu)
        //{
        //    try
        //    {
        //        string MenuId = "";

        //        foreach (int id_menu in id_OpcionMenu)
        //        {
        //            MenuId = MenuId + id_menu + ",";
        //        }
        //        if (MenuId.Length > 0)
        //        {
        //            MenuId = MenuId.Substring(0, MenuId.Length - 1);
        //        }
        //        oMenuAD.eliminarOperacionesMenu(Grupo, MenuId);
        //        foreach (int id_menu in id_MenuArray)
        //        {
        //            oMenuAD.insertarOperaciones(Grupo, id_menu);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        //public void HabilitarOperaciones(string Tipo, List<string> ArrHabilitar)
        //{
        //    try
        //    {
        //        string MenuId = "";

        //        foreach (string id_menu in ArrHabilitar)
        //        {
        //            MenuId = MenuId + id_menu + ",";
        //        }
        //        if (MenuId.Length > 0)
        //        {
        //            MenuId = MenuId.Substring(0, MenuId.Length - 1);
        //        }
        //        oMenuAD.habilitarOperaciones(Tipo, MenuId);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}


    }
}
