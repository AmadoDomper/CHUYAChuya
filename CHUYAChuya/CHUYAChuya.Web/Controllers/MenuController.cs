using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CHUYAChuya.Web.Models;
using CHUYAChuya.EntidadesNegocio;
using CHUYAChuya.LogicaNegocio;
using Newtonsoft.Json;
using System.Web.Security;
using CHUYAChuya.Seguridad.Filters;
using System.Net;
using CHUYAChuya.Web.Helper;
using CHUYAChuya.Web.Controllers.Base;

namespace CHUYAChuya.Web.Controllers
{
    //[RequiresAuthenticationAttribute]
    public class MenuController : BaseController
    {
        // GET: /Menu/

        MenuLN oMenuLN = new MenuLN();

        [HttpGet]
        //[RequiresAuthorizationAttribute(CodigoOpcion = "7")]
        [RequiresAuthenticationAttribute]
        public ActionResult MantenimientoPermisos()
        {
            MenuViewModel MenuViewModel = new MenuViewModel();
            List<Menu> l_oo, l_om, l_po, l_pm;

            l_oo = oMenuLN.ObtenerOperaciones(Constantes.Operacion).ToList<Menu>();

            l_po = (from Menu m in l_oo
                    where m.id_menu == m.id_padre
                    orderby m.posicion
                    select m).ToList<Menu>();

            AgregarItem(ref l_po, l_oo);

            l_om = oMenuLN.ObtenerOperaciones(Constantes.Menu).ToList<Menu>();

            l_pm = (from Menu m in l_om
                    where m.id_menu == m.id_padre
                    orderby m.posicion
                    select m).ToList<Menu>();

            AgregarItem(ref l_pm, l_om);

            MenuViewModel.GrupoOperacion = "Grupos";
            MenuViewModel.AllGrups = Roles.GetAllRoles();
            MenuViewModel.oTree.listaMenu = l_pm.ToArray();
            MenuViewModel.oTree.listaOperaciones = l_po.ToArray();

            return View(MenuViewModel);
        }

        [HttpPost]
        public JsonResult GrupoOperaciones(int nTipoPer)
        {
            MenuViewModel oMenuVM = new MenuViewModel();
            PersonaLN oPersonaLN = new PersonaLN();
            object data = null;

            if (nTipoPer == 2)
            {
                data = oPersonaLN.getAllUsers();
            }
            else if (nTipoPer == 1)
            {
                data = Roles.GetAllRoles();
            }

            return Json(JsonConvert.SerializeObject(data));
        }

        //public PartialViewResult MenuOperacion(string cGrupoSel)
        //{
        //    MenuViewModel MenuViewModel = new MenuViewModel();
        //    List<Menu> l_oo, l_om, l_po, l_pm;

        //    if (cGrupoSel.Length == 4)
        //    {
        //        string[] g = Roles.GetRolesForUser(cGrupoSel);
        //        string Grupos = String.Join(",", g);
        //        cGrupoSel = "UserCMACM" + cGrupoSel + "," + Grupos;
        //    }

        //    l_oo = oMenuLN.ObtenerOperacionesUsuarioGrupo(cGrupoSel, Constantes.Operacion).ToList<Menu>();

        //    l_po = (from Menu m in l_oo
        //            where m.id_menu == m.id_padre
        //            orderby m.posicion
        //            select m).ToList<Menu>();

        //    AgregarItem(ref l_po, l_oo);

        //    l_om = oMenuLN.ObtenerOperacionesUsuarioGrupo(cGrupoSel, Constantes.Menu).ToList<Menu>();

        //    l_pm = (from Menu m in l_om
        //            where m.id_menu == m.id_padre
        //            orderby m.posicion
        //            select m).ToList<Menu>();

        //    AgregarItem(ref l_pm, l_om);

        //    Partial_TreeVM M = new Partial_TreeVM();
        //    M.listaMenu = l_pm.ToArray();
        //    M.listaOperaciones = l_po.ToArray();
        //    MenuViewModel.oTree = M;

        //    return PartialView("_Partial_Tree", MenuViewModel);
        //}

        //[HttpPost]
        //public ActionResult Guardar(MenuViewModel M)
        //{
        //    List<int> l_pm_opsel, l_pm_op, l_po_opsel, l_po_op;
        //    List<Menu> l_pm, l_po;
        //    string cGrupoSel = M.AllGrups[0];

        //    //Opciones Menu
        //    l_pm = M.oTree.listaMenu.ToList();
        //    l_pm_opsel = Lista_Id(l_pm, true);
        //    l_pm_op = Lista_Id(l_pm);
        //    oMenuLN.InsertarOperaciones(cGrupoSel, l_pm_opsel, l_pm_op);

        //    //Opciones Operaciones
        //    if (M.oTree.listaOperaciones != null)
        //    {
        //        l_po = M.oTree.listaOperaciones.ToList();
        //        l_po_opsel = Lista_Id(l_po, true);
        //        l_po_op = Lista_Id(l_po);
        //        oMenuLN.InsertarOperaciones(cGrupoSel, l_po_opsel, l_po_op);
        //    }
        //    return Content("1");
        //}

        private List<int> Lista_Id(List<Menu> listaMenu, bool? Opcion = null)
        {
            List<int> lista = new List<int>();

            bool sel = false; bool opc = false;

            if (Opcion == null) opc = true;
            else sel = (bool)Opcion;

            foreach (Menu m1 in listaMenu)
            {
                if (m1.seleccionado == sel || opc) lista.Add(m1.id_menu);
                if (m1.listaMenu == null) continue;
                foreach (Menu m2 in m1.listaMenu)
                {
                    if (m2.seleccionado == sel || opc) lista.Add(m2.id_menu);
                    if (m2.listaMenu == null) continue;
                    foreach (Menu m3 in m2.listaMenu)
                    {
                        if (m3.seleccionado == sel || opc) lista.Add(m3.id_menu);
                        if (m3.listaMenu == null) continue;
                        foreach (Menu m4 in m3.listaMenu)
                        {
                            if (m4.seleccionado == sel || opc) lista.Add(m4.id_menu);
                            if (m4.listaMenu == null) continue;
                            foreach (Menu m5 in m4.listaMenu)
                            {
                                if (m5.seleccionado == sel || opc) lista.Add(m5.id_menu);
                            }
                        }
                    }
                }
            }
            return lista;
        }

        //Generación de los menus
        [HttpGet]
        public ActionResult ObtenerMenu()
        {
            string NombreUsuario = User.Identity.Name;
            string[] g = {""}; //= Roles.GetRolesForUser(NombreUsuario);
            string Grupos = String.Join(",", g);

            List<Menu> l_o = oMenuLN.ObtenerMenusFull(NombreUsuario, Grupos).ToList<Menu>();
            List<Menu> l_p = (from Menu m in l_o where m.id_menu == m.id_padre select m).ToList<Menu>();

            AgregarItem(ref l_p, l_o);

            //SessionRemove("CO_*");
            //SessionCreate(l_p, "CO_");

            return PartialView("_Menu", l_p);
        }
        //Fin Generación de los menus

        public void AgregarItem(ref List<Menu> Lista, List<Menu> l_o)
        {
            foreach (Menu menu in Lista)
            {
                List<Menu> sl = (from Menu sm in l_o where menu.id_menu == sm.id_padre && menu.id_menu != sm.id_menu select sm).ToList<Menu>();

                if (sl != null && sl.Count() > 0)
                {
                    menu.listaMenu = sl;

                    AgregarItem(ref sl, l_o);
                }
                else menu.listaMenu = new List<Menu>();
            }
        }
    }
}
