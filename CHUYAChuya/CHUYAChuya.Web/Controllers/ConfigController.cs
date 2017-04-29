using System;
using System.Collections.Generic;
using System.Linq;

using System.Diagnostics;
using System.Text;
using System.IO;

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
    public class ConfigController : Controller
    {
        //
        // GET: /Config/

        public ActionResult Config()
        {
            return View();
        }

        public JsonResult ListaRoles()
        {
            RolLN oRolLN = new RolLN();
            List<Rol> ListaRoles = new List<Rol>();
            ListaRoles = oRolLN.ListarRoles();
            return Json(JsonConvert.SerializeObject(ListaRoles, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
        }


        public JsonResult CargaRolPermisos(int nRolId)
        {
            ConfiguracionLN oConfigLN = new ConfiguracionLN();
            Rol lstRol = new Rol();

            lstRol = oConfigLN.CargaRolPermisos(nRolId);
            return Json(JsonConvert.SerializeObject(lstRol));
        }

        public JsonResult RegistrarRolPermisos(string oJsonRol)
        {
            int nReg = 0;
            ConfiguracionLN ConfLN = new ConfiguracionLN();
            //ConfiguracionLN oConfigLN = new ConfiguracionLN();
            Rol lstRol = JsonConvert.DeserializeObject<Rol>(oJsonRol);
            nReg = ConfLN.RegistrarActualizarRolPermisos(lstRol);

            return Json(nReg);
        }

        public JsonResult EliminarRol(int nRolId)
        {
            int nReg = 0;
            ConfiguracionLN ConfLN = new ConfiguracionLN();
            nReg = ConfLN.EliminarRol(nRolId);

            return Json(nReg);
        }

    }
}
