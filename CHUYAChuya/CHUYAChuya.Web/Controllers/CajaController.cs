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
            ConstanteLN oConstLN = new ConstanteLN();
            CajaViewModel oCajaVM = new CajaViewModel();

            oCajaVM.lstUsuarios = oUsuarioLN.Usuarios();
            oCajaVM.lstTpoCom = oConstLN.ListaConstante(Constantes.TpoComPro);
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
            CajaLN oCajaLN = new CajaLN();
            CierreDatos oCierreDet = new CierreDatos();
            oCierreDet = oCajaLN.UltimaFechaCajaDia();
            return View(oCierreDet);
        }

        public JsonResult CargaUsuariosCierres(DateTime dFecha)
        {
            CajaLN oCajaLN = new CajaLN();
            CierreDatos oCierreDet = new CierreDatos();
            oCierreDet = oCajaLN.CargaUsuariosCierres(dFecha);

            return Json(JsonConvert.SerializeObject(oCierreDet));
        }

        public JsonResult CargaDetalleCierre(string cUsuario, DateTime dFecha, int nCierre)
        {
            CajaLN oCajaLN = new CajaLN();
            Cierre oCierreDet = new Cierre();
            oCierreDet = oCajaLN.CargaDetalleCierre(cUsuario, dFecha, nCierre);

            return Json(JsonConvert.SerializeObject(oCierreDet));
        }

        public JsonResult ObtenerUsuarioIniciaDia()
        {
            string cUsuIni = "", cUsuario ="";
            CajaLN oCajaLN = new CajaLN();
            cUsuario = ((Usuario)Session["Datos"]).cUsuNombre;
            cUsuIni = oCajaLN.ObtenerUsuarioIniciaDia(cUsuario);

            return Json(cUsuIni);
        }

        public JsonResult ObtenerUltimoSaldoCaja()
        {
            CajaLN oCajaLN = new CajaLN();
            return Json(oCajaLN.ObtenerUltimoSaldoCaja());
        }


        public bool ValidarUsuario(string userName, string password)
        {
            try
            {
                SeguridadLN oSeguridadLN = new SeguridadLN();
                Usuario oUsuario = new Usuario();
                oUsuario.cUsuNombre = userName;

                oUsuario = oSeguridadLN.ObtenerUsuarioContrasena(oUsuario);

                bool validar = false;
                if (oUsuario != null)
                {
                    if (oUsuario.cUsuContrasena == password)
                    {
                        validar = true;
                    }
                }
                return validar;
            }
            catch
            {
                return false;
            }
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

        //CIERRE DE CAJA

        public JsonResult RealizarCierreCaja(decimal nCont, decimal nDif, string cUsu, string cPass)
        {
            int nCajaCaId = -4;

            if (cUsu != "")
            {
                if (ValidarUsuario(cUsu, cPass) )
                {
                    nCajaCaId = RealizaCierreCa(nCont, nDif);
                }
            }
            else
            {
                nCajaCaId = RealizaCierreCa(nCont, nDif);
            }
            
            return Json(nCajaCaId);
        }

        public int RealizaCierreCa(decimal nCont, decimal nDif)
        {
            CajaLN oCajaLN = new CajaLN();
            string cUsuario = "", cAgencia = "01";
            int nCajaCaId;

            cUsuario = ((Usuario)Session["Datos"]).cUsuNombre;
            nCajaCaId = oCajaLN.RealizarCierreCaja(cUsuario, cAgencia, nCont, nDif);

            if (nCajaCaId > 0)
            {
                ImprimirCierre(nCajaCaId);
            }
            return nCajaCaId;
        }


        public JsonResult RealizarCierreCajaDia()
        {
            CajaLN oCajaLN = new CajaLN();
            string cUsuario = "", cAgencia = "01";
            int nCajaId;

            cUsuario = ((Usuario)Session["Datos"]).cUsuNombre;
            nCajaId = oCajaLN.RealizarCierreCajaDia(cUsuario, cAgencia);

            if (nCajaId > 0)
            {
                ImprimirCierreDia(nCajaId);
            }

            return Json(nCajaId);
        }

        //END CIERRE DE CAJA


        public void ImprimirCierre(int nCajeCaId)
        {
            Process p;

            CajaLN oCajaLN = new CajaLN();
            TicketCierre oTicketCierre = new TicketCierre();
            oTicketCierre = oCajaLN.ObtenerDatosCierreImp(nCajeCaId);


            using (StreamWriter writer = new StreamWriter("C:\\ticket.txt", false, System.Text.Encoding.GetEncoding(850)))
            {

                //writer.WriteLine("          LAVANDERIA CHUYACHUYA         ");
                //writer.WriteLine("           RUC: 2056727288057           ");
                //writer.WriteLine("  JR. MORONA Nº 441 - IQUITOS - MAYNAS  ");
                //writer.WriteLine("          Telefono: (065)242847         ");
                //writer.WriteLine("");
                //CabeceraTicket(writer);

                writer.WriteLine("Fecha: " + ((DateTime)oTicketCierre.dFechaCierre).ToString("dd/MM/yyyy").PadRight(19) + "Hora: " + ((DateTime)oTicketCierre.dFechaCierre).ToString("HH:mm:ss"));
                writer.WriteLine("Equipo: PC-CAJA-01     Serie: " + "FFGF252758");
                writer.WriteLine("Usuario: " + oTicketCierre.cUsuario.PadRight(15) + "Caja Id: " + oTicketCierre.nCajaCaId.ToString().PadLeft(7));
                writer.WriteLine("");
                writer.WriteLine("************ CIERRE DE CAJA ************");
                writer.WriteLine("");
                writer.WriteLine("PERIODO: " + oTicketCierre.dFechaApertura.ToString("hh:mm tt") + "-" + oTicketCierre.dFechaCierre.ToString("hh:mm tt"));
                writer.WriteLine("");
                writer.WriteLine(" CALCULADO: S/.".PadLeft(15) + oTicketCierre.nCajaTotal.ToString("#,##0.00").PadLeft(9));
                writer.WriteLine("   CONTADO: S/.".PadLeft(15) + oTicketCierre.nContado.ToString("#,##0.00").PadLeft(9));
                writer.WriteLine("DIFERENCIA: S/.".PadLeft(15) + oTicketCierre.nDiferencia.ToString("#,##0.00").PadLeft(9));
                writer.WriteLine("");
                writer.WriteLine("********** Entrada de Efectivo *********");
                writer.WriteLine("");
                writer.WriteLine("Dinero Inicial Caja     : + S/.".PadLeft(31) + oTicketCierre.nCajaInicio.ToString("#,##0.00").PadLeft(8));
                writer.WriteLine("Entrada de Efectivo     : + S/.".PadLeft(31) + oTicketCierre.nCajaEntEfec.ToString("#,##0.00").PadLeft(8));
                writer.WriteLine("".PadRight(40, '-'));
                writer.WriteLine("TOTAL                   :   S/.".PadLeft(31) + oTicketCierre.nEntTotal.ToString("#,##0.00").PadLeft(8));
                writer.WriteLine("");
                writer.WriteLine("************* Total en Caja ************");
                writer.WriteLine("");
                writer.WriteLine("Entrada Efectivo        : + S/.".PadLeft(31) + oTicketCierre.nEntTotal.ToString("#,##0.00").PadLeft(8));
                writer.WriteLine("Notas con Anticipo      : + S/.".PadLeft(31) + oTicketCierre.nCajaNotaAnticipo.ToString("#,##0.00").PadLeft(8));
                writer.WriteLine("Notas de Entrega Pagadas: + S/.".PadLeft(31) + oTicketCierre.nCajaNotaPagadas.ToString("#,##0.00").PadLeft(8));
                writer.WriteLine("Pago a proveedores      : - S/.".PadLeft(31) + oTicketCierre.nPagoProv.ToString("#,##0.00").PadLeft(8));
                writer.WriteLine("Salida de efectivo      : - S/.".PadLeft(31) + oTicketCierre.nSalidaOtro.ToString("#,##0.00").PadLeft(8));
                writer.WriteLine("Anulaciones             : - S/.".PadLeft(31) + oTicketCierre.nAnula.ToString("#,##0.00").PadLeft(8));
                writer.WriteLine("".PadRight(40, '-'));
                writer.WriteLine("TOTAL                   :   S/.".PadLeft(31) + oTicketCierre.nCajaTotal.ToString("#,##0.00").PadLeft(8));                
                writer.WriteLine("");
                writer.WriteLine("");
                writer.WriteLine("");
                writer.WriteLine("");
                writer.WriteLine("");
                writer.WriteLine("");
                writer.WriteLine("");
                writer.WriteLine("");

                writer.WriteLine(char.ConvertFromUtf32(27) + "i");
                writer.Close();

                using (StreamWriter writerBat = new StreamWriter(@"C:\TicketBatch\impresion.bat", false))
                {
                    writerBat.WriteLine(@"type C:\ticket.txt > " + Constantes.ImpRed);

                    writerBat.Close();
                }

                p = new Process();
                p.StartInfo.FileName = @"C:\TicketBatch\impresion.bat";

                p.Start();
                p.Close();
                p.Dispose();


            }
        }

        public void ImprimirCierreDia(int nCajaId)
        {
            Process p;

            CajaLN oCajaLN = new CajaLN();
            TicketCierre oTicketCierre = new TicketCierre();
            oTicketCierre = oCajaLN.ObtenerDatosCierreDiaImp(nCajaId);

            using (StreamWriter writer = new StreamWriter("C:\\ticket.txt", false, System.Text.Encoding.GetEncoding(850)))
            {

                writer.WriteLine("Fecha: " + ((DateTime)oTicketCierre.dFechaCierre).ToString("dd/MM/yyyy").PadRight(19) + "Hora: " + ((DateTime)oTicketCierre.dFechaCierre).ToString("HH:mm:ss"));
                writer.WriteLine("Equipo: PC-CAJA-01     Serie: " + "FFGF252758");
                writer.WriteLine("Usuario: " + oTicketCierre.cUsuario.PadRight(11) + "Caja Día Id: " + oTicketCierre.nCajaCaId.ToString().PadLeft(7));
                writer.WriteLine("");
                writer.WriteLine("******** CIERRE DE CAJA DEL DIA ********");
                writer.WriteLine("");
                writer.WriteLine("PERIODO: " + oTicketCierre.dFechaApertura.ToString("hh:mm tt") + "-" + oTicketCierre.dFechaCierre.ToString("hh:mm tt"));
                //writer.WriteLine("");
                //writer.WriteLine(" CALCULADO: S/.".PadLeft(15) + oTicketCierre.nCajaTotal.ToString("#,##0.00").PadLeft(9));
                //writer.WriteLine("   CONTADO: S/.".PadLeft(15) + oTicketCierre.nContado.ToString("#,##0.00").PadLeft(9));
                //writer.WriteLine("DIFERENCIA: S/.".PadLeft(15) + oTicketCierre.nDiferencia.ToString("#,##0.00").PadLeft(9));
                writer.WriteLine("");
                writer.WriteLine("********** Entrada de Efectivo *********");
                writer.WriteLine("");
                writer.WriteLine("Dinero Inicial Caja     : + S/.".PadLeft(31) + oTicketCierre.nCajaInicio.ToString("#,##0.00").PadLeft(8));
                writer.WriteLine("Entrada de Efectivo     : + S/.".PadLeft(31) + oTicketCierre.nCajaEntEfec.ToString("#,##0.00").PadLeft(8));
                writer.WriteLine("".PadRight(40, '-'));
                writer.WriteLine("TOTAL                   :   S/.".PadLeft(31) + oTicketCierre.nEntTotal.ToString("#,##0.00").PadLeft(8));
                writer.WriteLine("");
                writer.WriteLine("************* Total en Caja ************");
                writer.WriteLine("");
                writer.WriteLine("Entrada Efectivo        : + S/.".PadLeft(31) + oTicketCierre.nEntTotal.ToString("#,##0.00").PadLeft(8));
                writer.WriteLine("Notas con Anticipo      : + S/.".PadLeft(31) + oTicketCierre.nCajaNotaAnticipo.ToString("#,##0.00").PadLeft(8));
                writer.WriteLine("Notas de Entrega Pagadas: + S/.".PadLeft(31) + oTicketCierre.nCajaNotaPagadas.ToString("#,##0.00").PadLeft(8));
                writer.WriteLine("Pago a proveedores      : - S/.".PadLeft(31) + oTicketCierre.nPagoProv.ToString("#,##0.00").PadLeft(8));
                writer.WriteLine("Salida de efectivo      : - S/.".PadLeft(31) + oTicketCierre.nSalidaOtro.ToString("#,##0.00").PadLeft(8));
                writer.WriteLine("Anulaciones             : - S/.".PadLeft(31) + oTicketCierre.nAnula.ToString("#,##0.00").PadLeft(8));
                writer.WriteLine("".PadRight(40, '-'));
                writer.WriteLine("TOTAL                   :   S/.".PadLeft(31) + oTicketCierre.nCajaTotal.ToString("#,##0.00").PadLeft(8));
                writer.WriteLine("");
                writer.WriteLine("");
                writer.WriteLine("");
                writer.WriteLine("");
                writer.WriteLine("");
                writer.WriteLine("");
                writer.WriteLine("");
                writer.WriteLine("");

                writer.WriteLine(char.ConvertFromUtf32(27) + "i");
                writer.Close();

                using (StreamWriter writerBat = new StreamWriter(@"C:\TicketBatch\impresion.bat", false))
                {
                    writerBat.WriteLine(@"type C:\ticket.txt > " + Constantes.ImpRed);

                    writerBat.Close();
                }

                p = new Process();
                p.StartInfo.FileName = @"C:\TicketBatch\impresion.bat";

                p.Start();
                p.Close();
                p.Dispose();
            }
        }

    }
}
