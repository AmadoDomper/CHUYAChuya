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
    public class MovCajaController : Controller
    {
        //
        // GET: /MovCaja/

        public ActionResult MovCaja()
        {
            return View();
        }

        //MOVIMIENTO DE CAJA

        [RequiresAuthenticationAttribute]
        public JsonResult RegistrarSalidaEfePagoProv(int nPersId, decimal nMontoSalida, string cComprobante, byte nTipoComp, DateTime dFechaEmi)
        {
            CajaLN oCajaLN = new CajaLN();
            string cUsuario = "", cAgencia = "01";
            int resultado;

            cUsuario = ((Usuario)Session["Datos"]).cUsuNombre;
            resultado = oCajaLN.RegistrarSalidaEfePagoProv(nPersId, nMontoSalida, cComprobante, nTipoComp, dFechaEmi, cUsuario, cAgencia);
            return Json(resultado);
        }

        [RequiresAuthenticationAttribute]
        public JsonResult RegistrarSalidaEfe(decimal nMontoSalida, string cMotOtro)
        {
            CajaLN oCajaLN = new CajaLN();
            string cUsuario = "", cAgencia = "01";
            int resultado;

            cUsuario = ((Usuario)Session["Datos"]).cUsuNombre;
            resultado = oCajaLN.RegistrarSalidaEfe(nMontoSalida, cMotOtro, cUsuario, cAgencia);
            return Json(resultado);
        }

        [RequiresAuthenticationAttribute]
        public JsonResult RegistrarEntradaEfe(decimal nMontoEntrada)
        {
            CajaLN oCajaLN = new CajaLN();
            string cUsuario = "", cAgencia = "01";
            int resultado;

            cUsuario = ((Usuario)Session["Datos"]).cUsuNombre;
            resultado = oCajaLN.RegistrarEntradaEfe(nMontoEntrada, cUsuario, cAgencia);
            return Json(resultado);
        }


        public JsonResult BuscarMovCaja(string cMovDesc = null)
        {
            CajaLN oCajaLN = new CajaLN();
            List<MovCaja> ListasMovCaja = new List<MovCaja>();
            ListasMovCaja = oCajaLN.BuscarMovCaja(cMovDesc);
            return Json(JsonConvert.SerializeObject(ListasMovCaja, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
        }

        //END MOVIMIENTO DE CAJA

    }
}
