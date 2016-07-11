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
    public class CajaController : Controller
    {
        //
        // GET: /Caja/

        public ActionResult Caja()
        {
            return View();
        }

        //MOVIMIENTO DE CAJA
        #region Movimiento Caja

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
        #endregion
        //END MOVIMIENTO DE CAJA


        public ActionResult AperturaCaja()
        {
            return View();
        }

        //CORTE DE CAJA
        #region CorteCaja

        public ActionResult CorteCaja()
        {
            return View();
        }

        public JsonResult CargaDetalleCorte(string cUsuario, DateTime dFecha)
        {
            CajaLN oCajaLN = new CajaLN();
            Corte oCorteDet = new Corte();
            oCorteDet = oCajaLN.CargaDetalleCorte(cUsuario, dFecha);

            return Json(JsonConvert.SerializeObject(oCorteDet));
        }
        #endregion
        //END CORTE DE CAJA


    }
}
