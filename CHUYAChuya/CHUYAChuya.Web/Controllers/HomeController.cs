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
using CHUYAChuya.Web.Controllers.Base;

namespace CHUYAChuya.Web.Controllers
{
    [RequiresAuthenticationAttribute]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.titulo = "Inicio";
            return View();
        }
    }


}
