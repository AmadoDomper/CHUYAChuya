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

        public JsonResult BuscarNotaEntregas(int nNotaEst, int nNotaEntId = -1, string cPersDOI = null, string cPersDesc = null, DateTime? dIni = null, DateTime? dFin = null)
        {
            NotaEntregaLN oNotaEntregaLN = new NotaEntregaLN();
            List<NotaEntrega> ListaNotaEntregas = new List<NotaEntrega>();
            ListaNotaEntregas = oNotaEntregaLN.BuscarNotaEntregas(nNotaEst, nNotaEntId, cPersDOI, cPersDesc, dIni, dFin);
            return Json(JsonConvert.SerializeObject(ListaNotaEntregas, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
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

        public JsonResult CargoDatosNotaEntrega(int nNotaId)
        {
            NotaEntregaLN oNotaEntregaLN = new NotaEntregaLN();
            NotaEntrega oNotaEntrega = new NotaEntrega();
            oNotaEntrega = oNotaEntregaLN.CargoDatosNotaEntrega(nNotaId);

            return Json(JsonConvert.SerializeObject(oNotaEntrega));
        }





    }
}
