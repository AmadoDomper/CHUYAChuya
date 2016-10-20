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

        public JsonResult Imprimir()
        {
            Process p;

            using (StreamWriter writer = new StreamWriter("C:\\ticket.txt", false, System.Text.Encoding.GetEncoding(850)))
            {
                writer.WriteLine("       No se aceptan devoluciones    ");
                writer.WriteLine("Los cambios de producto solo a traves de");
                writer.WriteLine("Nota de Credito hasta 7 dias calendario desde la fecha de compra y unicamente");
                writer.WriteLine("      con el comprobante de pago     ");
                writer.WriteLine("       Hola mi nombre es Goku    ");
                writer.WriteLine("Los cambios de producto solo a traves de");
                writer.WriteLine("Nota de Credito hasta 7 dias calendario desde la fecha de compra y unicamente");
                writer.WriteLine("      pagá el comprobante de pago     ");
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

            return Json(1);
        }



        public JsonResult RegistrarRolPermisos(string oJsonRol)
        {
            //ConfiguracionLN oConfigLN = new ConfiguracionLN();
            Rol lstRol = JsonConvert.DeserializeObject<Rol>(oJsonRol);


            //lstRol = oConfigLN.CargaRolPermisos(nRolId);
            //return Json(JsonConvert.SerializeObject(lstRol));
            return Json(1);
        }

    }
}
