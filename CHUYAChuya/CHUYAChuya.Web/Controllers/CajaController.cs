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
             CajaLN oCajaLN = new CajaLN();
             bool bEstado = oCajaLN.CajaDiaAbierto();
             ViewBag.bEstado = bEstado;
             return View();
        }

        //MOVIMIENTO DE CAJA
        #region Movimiento Caja

        [RequiresAuthenticationAttribute]
        public JsonResult ListaConstantes()
        {
            UsuarioLN oUsuarioLN = new UsuarioLN();
            CajaViewModel oCajaVM = new CajaViewModel();

            oCajaVM.lstUsuarios = oUsuarioLN.Usuarios();
            return Json(JsonConvert.SerializeObject(oCajaVM));
        }


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

            string cUsuario = "";
            cUsuario = ((Usuario)Session["Datos"]).cUsuNombre;

            CajaLN oCajaLN = new CajaLN();
            List<MovCaja> ListasMovCaja = new List<MovCaja>();
            ListasMovCaja = oCajaLN.BuscarMovCaja(cUsuario, cMovDesc);
            return Json(JsonConvert.SerializeObject(ListasMovCaja, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
        }
        #endregion
        //END MOVIMIENTO DE CAJA

        public ActionResult ConfirmarInicioCaja()
        {
            return View();
        }

        //CORTE DE CAJA
        #region CierreCaja

        public ActionResult CierreCaja()
        {
            return View();
        }

        public JsonResult CargaDetalleCierre(string cUsuario, DateTime dFecha)
        {
            CajaLN oCajaLN = new CajaLN();
            Cierre oCierreDet = new Cierre();
            oCierreDet = oCajaLN.CargaDetalleCierre(cUsuario, dFecha);

            return Json(JsonConvert.SerializeObject(oCierreDet));
        }
        #endregion
        //END CORTE DE CAJA

        //APERTURA DE CAJA
        #region AperturaCaja
        public JsonResult RegistrarAperturaCaja(string cUsuOpe, decimal nMontoIni, DateTime dFechaApe)
        {
            CajaLN oCajaLN = new CajaLN();
            string cUsuario = "", cAgencia = "01";
            int nMovNro;

            cUsuario = ((Usuario)Session["Datos"]).cUsuNombre;
            nMovNro = oCajaLN.RegistrarAperturaCaja(cUsuOpe, nMontoIni, dFechaApe, cUsuario, cAgencia);
            return Json(nMovNro);
        }

        public JsonResult RegistrarAsigMonto(string cUsuOpe, decimal nMontoAsig)
        {
            CajaLN oCajaLN = new CajaLN();
            string cUsuario = "", cAgencia = "01";
            int nMovNro;

            cUsuario = ((Usuario)Session["Datos"]).cUsuNombre;
            nMovNro = oCajaLN.RegistrarAsigMonto(cUsuOpe, nMontoAsig, cUsuario, cAgencia);
            return Json(nMovNro);
        }

        public JsonResult RegistrarConfDineroIni(int nCCId)
        {
            CajaLN oCajaLN = new CajaLN();
            string cUsuario = "", cAgencia = "01";
            int nMovNro;

            cUsuario = ((Usuario)Session["Datos"]).cUsuNombre;
            nMovNro = oCajaLN.RegistrarConfDineroIni(nCCId, cUsuario, cAgencia);
            return Json(nMovNro);
        }


        #endregion

        //END APERTURA DE CAJA


    }
}
