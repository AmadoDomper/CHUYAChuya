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
    public class UsuarioController : Controller
    {
        //
        // GET: /Usuario/

        public ActionResult Usuarios()
        {
            return View();
        }

        public JsonResult ListaUsuariosPag(int nPage = 1, int nSize = 10, int nUsuId = -1, string cUsuDesc = null, string cUsuName = null, string cUsuDOI = null)
        {
            UsuarioLN oUsuarioLN = new UsuarioLN();
            ListaPaginada ListaUsuariosPag = new ListaPaginada();
            ListaUsuariosPag = oUsuarioLN.ListaUsuariosPag(nPage, nSize, nUsuId, cUsuDesc, cUsuName, cUsuDOI);
            return Json(JsonConvert.SerializeObject(ListaUsuariosPag, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
        }

        [RequiresAuthenticationAttribute]
        public JsonResult RegistrarUsuario(UsuarioViewModel oUsuarioViewModel)
        {
            UsuarioLN oUsuarioLN = new UsuarioLN();
            int resultado;
            resultado = oUsuarioLN.RegistrarActualizarUsuario(oUsuarioViewModel.oUsu);
            return Json(resultado);
        }

        [RequiresAuthenticationAttribute]
        public JsonResult EliminarUsuario(int nPersId)
        {
            UsuarioLN oPers = new UsuarioLN();
            int resultado;
            resultado = oPers.EliminarUsuario(nPersId);
            return Json(resultado);
        }

        public JsonResult CargarDatosUsuario(int nPersId, string cDNI)
        {
            UsuarioLN oUsuarioLN = new UsuarioLN();
            Usuario oPersNat = new Usuario();
            oPersNat = oUsuarioLN.CargarDatosUsuario(nPersId, cDNI);

            return Json(JsonConvert.SerializeObject(oPersNat));
        }

    }
}
