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
    public class NotaEntregaController : Controller
    {
        //
        // GET: /NotaEntrega/

        public ActionResult NotaEntregas()
        {
            return View();
        }

        [RequiresAuthenticationAttribute]
        public JsonResult RegistrarNotaEntrega(NotaEntregaViewModel oNotaEntregaViewModel)
        {
            NotaEntregaLN oNotaEntLN = new NotaEntregaLN();

            int resultado;
            oNotaEntregaViewModel.oNotEnt.cNotaUsuReg = ((Usuario)Session["Datos"]).cUsuNombre;
            resultado = oNotaEntLN.RegistrarNotaEntrega(oNotaEntregaViewModel.oNotEnt);
            return Json(resultado);
        }





    }
}
