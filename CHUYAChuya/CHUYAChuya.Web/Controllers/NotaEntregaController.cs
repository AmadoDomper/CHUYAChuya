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
    public class NotaEntregaController : Controller
    {
        //
        // GET: /NotaEntrega/

        public ActionResult NotaEntregas()
        {
            return View();
        }

        public JsonResult BuscarNotaEntPag(int nNotaEst, int nPage=1, int nSize=10, int nNotaEntId = -1, string cPersDOI = null, string cPersDesc = null, DateTime? dIni = null, DateTime? dFin = null)
        {
            NotaEntregaLN oNotaEntregaLN = new NotaEntregaLN();
            ListaPaginada ListaNotaEntPag = new ListaPaginada();
            ListaNotaEntPag = oNotaEntregaLN.BuscarNotaEntPag(nNotaEst, nPage, nSize, nNotaEntId, cPersDOI, cPersDesc, dIni, dFin);
            return Json(JsonConvert.SerializeObject(ListaNotaEntPag, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
        }

        [RequiresAuthenticationAttribute]
        public JsonResult RegistrarNotaEntrega(NotaEntregaViewModel oNotaEntregaViewModel)
        {
            NotaEntregaLN oNotaEntLN = new NotaEntregaLN();
            Ticket oTicket = new Ticket();

            oNotaEntregaViewModel.oNotEnt.cNotaUsuReg = ((Usuario)Session["Datos"]).cUsuNombre;
            oNotaEntregaViewModel.oNotEnt.cNotaUsuAge = "01";

            oTicket = oNotaEntLN.RegistrarNotaEntrega(oNotaEntregaViewModel.oNotEnt);

            Imprimir(oTicket.nTicketSerie, oTicket.nTicketCorrelativo);
            return Json(oTicket.oNotaEntrega.nNotaEntId);
        }
        
        [RequiresAuthenticationAttribute]
        public JsonResult RealizarCobroServicio(int nNotaEntId, decimal nNotaEfecCo, decimal nNotaCambioCo)
        {
            NotaEntregaLN oNotaEntLN = new NotaEntregaLN();

            int resultado;
            string cNotaUsuCo = ((Usuario)Session["Datos"]).cUsuNombre;
            string cNotaUsuAge = "01";

            resultado = oNotaEntLN.RealizarCobroServicio(nNotaEntId, nNotaEfecCo, nNotaCambioCo, cNotaUsuCo, cNotaUsuAge);
            return Json(resultado);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult CargoDatosNotaEntrega(int nNotaId)
        {
            NotaEntregaLN oNotaEntregaLN = new NotaEntregaLN();
            NotaEntrega oNotaEntrega = new NotaEntrega();
            oNotaEntrega = oNotaEntregaLN.CargoDatosNotaEntrega(nNotaId);

            return Json(JsonConvert.SerializeObject(oNotaEntrega));
        }

        public string Dia(DateTime d)
        {
            string dia = "";
            switch ((int)d.DayOfWeek)
            {
                case 1:
                    dia = "Lun";
                    break;
                case 2:
                    dia = "Mar";
                    break;
                case 3:
                    dia = "Mié";
                    break;
                case 4:
                    dia = "Jue";
                    break;
                case 5:
                    dia = "Vie";
                    break;
                case 6:
                    dia = "Sáb";
                    break;
                case 7:
                    dia = "Dom  ";
                    break;
            }
            return dia;
        }

        public string Servicios(Boolean bLav, Boolean bSec, Boolean bPlan, int n)
        {
            string serv = "";
            if (bLav) { serv = "L-"; }
            if (bSec) { serv += "S-"; }
            if (bPlan) { serv += "P-"; }

            serv = serv.Remove(serv.Length - 1, 1);
            serv = "".PadLeft(n).Insert(((n - serv.Length) / 2), serv).Remove(n);
            return serv;
        }


        public void Imprimir(int nSerie, int nCorr)
        {
            Process p;

            NotaEntregaLN oNotaEntregaLN = new NotaEntregaLN();
            Ticket oTicket = new Ticket();
            oTicket = oNotaEntregaLN.ObtenerDatosTicket(nSerie, nCorr);


            using (StreamWriter writer = new StreamWriter("C:\\ticket.txt", false, System.Text.Encoding.GetEncoding(850)))
            {

                writer.WriteLine("          LAVANDERIA CHUYACHUYA         ");
                writer.WriteLine("           RUC: 2056727288057           ");
                writer.WriteLine("  JR. MORONA Nº 441 - IQUITOS - MAYNAS  ");
                writer.WriteLine("          Telefono: (065)242847         ");
                writer.WriteLine("");
                writer.WriteLine("Fecha: " + ((DateTime)oTicket.oNotaEntrega.dFechaEntrega).ToString("dd/MM/yyyy").PadRight(19) + "Hora: " + ((DateTime)oTicket.oNotaEntrega.dFechaEntrega).ToString("HH:mm:ss"));
                writer.WriteLine("Equipo: PC-CAJA-01     Serie: " + oTicket.oImp.nImpSerie);
                writer.WriteLine("Usuario: " + ((Usuario)Session["Datos"]).cUsuNombre.PadRight(12) + "Ticket: " + oTicket.nTicketSerie.ToString().PadLeft(3,'0') + "-" + oTicket.nTicketCorrelativo.ToString().PadLeft(7,'0'));
                writer.WriteLine("");
                writer.WriteLine("Cantidad    Servicio    Precio   Importe");
                writer.WriteLine("".PadRight(40, '-'));

                foreach (var prod in oTicket.oNotaEntrega.ListaNotaEntProd)
                {
                    writer.WriteLine("- " + prod.oProd.cProdDesc);
                    writer.WriteLine(prod.nDetCantidad.ToString("##0.000").PadLeft(7) + Servicios(prod.oProd.bProdSerLavado, prod.oProd.bProdSerSecado, prod.oProd.bProdSerPlanchado, 17) +
                                     prod.nProdPrecioUnit.ToString("#,##0.00").PadLeft(5) + prod.nDetImporte.ToString("#,##0.00").PadLeft(11));

                }

                writer.WriteLine("".PadRight(40, '-'));
                writer.WriteLine("SUB-TOTAL S/.".PadLeft(24) + oTicket.oNotaEntrega.nNotaSubTotal.ToString("#,##0.00").PadLeft(16));

                if (oTicket.oNotaEntrega.nNotaDescuento > 0)
                {
                    writer.WriteLine("DESCUENTO S/.".PadLeft(24) + oTicket.oNotaEntrega.nNotaDescuento.ToString("#,##0.00").PadLeft(16));
                }

                if (oTicket.oNotaEntrega.nNotaAnticipo > 0)
                {
                    writer.WriteLine("ANTICIPO S/.".PadLeft(24) + oTicket.oNotaEntrega.nNotaAnticipo.ToString("#,##0.00").PadLeft(16));
                }

                writer.WriteLine("TOTAL VENTA S/.".PadLeft(24) + oTicket.oNotaEntrega.nNotaMontoTotal.ToString("#,##0.00").PadLeft(16));
                writer.WriteLine("".PadRight(40, '-'));

                if (oTicket.oNotaEntrega.nNotaEfectivo > 0)
                {
                    writer.WriteLine("EFECTIVO S/.".PadLeft(24) + oTicket.oNotaEntrega.nNotaEfectivo.ToString("#,##0.00").PadLeft(16));
                    writer.WriteLine("VUELTO S/.".PadLeft(24) + oTicket.oNotaEntrega.nNotaCambio.ToString("#,##0.00").PadLeft(16));
                    writer.WriteLine("".PadRight(40, '-'));
                }

                writer.WriteLine("Nº Mov.   : " + oTicket.oMov.nMovNro.ToString().PadLeft(7,'0'));
                writer.WriteLine("");
                writer.WriteLine("Cliente   : " + oTicket.oNotaEntrega.oPers.cPersDesc.PadRight(18));

                if (oTicket.oNotaEntrega.oPers.cPersTipo == "N")
                {
                    writer.WriteLine("DNI       : " + oTicket.oNotaEntrega.oPers.cPersDOI.PadRight(18));
                }
                else
                {
                    writer.WriteLine("RUC       : " + oTicket.oNotaEntrega.oPers.cPersDOI.PadRight(18));
                }

                writer.WriteLine("Direccion : " + oTicket.oNotaEntrega.oPers.cPersDireccion.PadRight(18));
                writer.WriteLine("");
                writer.WriteLine("Fecha Entrega: " + Dia((DateTime)oTicket.oNotaEntrega.dFechaEntrega) + " " + ((DateTime)oTicket.oNotaEntrega.dFechaEntrega).ToString("dd/MM/yyyy hh:mm"));
                writer.WriteLine("".PadRight(40, '-'));
                writer.WriteLine("    Muchas gracias por su preferencia   ");
                writer.WriteLine("         Lo esperamos nuevamente        ");
                writer.WriteLine("----------------------------------------");
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

                using (StreamWriter writerBat = new StreamWriter("C:\\TicketBatch\\impresion.bat", false))
                {
                    writerBat.WriteLine("type C:\\ticket.txt > " + "LPT1");

                    writerBat.Close();
                }

                p = new Process();
                p.StartInfo.FileName = "C:\\TicketBatch\\impresion.bat";

                p.Start();
                p.Close();
                p.Dispose();


            }


        }


    }
}
