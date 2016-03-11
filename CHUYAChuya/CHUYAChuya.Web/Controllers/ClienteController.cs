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
    public class ClienteController : Controller
    {
        //
        // GET: /Cliente/

        public ActionResult Clientes()
        {
            return View();
        }

        /// <summary>
        /// Metodo para listas todos los clientes que se muestran en la pantalla inicial de Clientes
        /// </summary>
        /// <param name="codage"></param>
        /// <param name="estados"></param>
        /// <returns>Lista en de Clientes en formato JSON</returns>
        public JsonResult ListaClientes()
        {
            PersonaLN oPersonaLN = new PersonaLN();
            List<Persona> ListaClientes = new List<Persona>();
            ListaClientes = oPersonaLN.ListaClientes();
            return Json(JsonConvert.SerializeObject(ListaClientes, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
        }


        [RequiresAuthenticationAttribute]
        public JsonResult RegistrarPersNatural(ClienteViewModel oClienteViewModel)
        {
            PersonaNatLN oPersNatLN = new PersonaNatLN();
            int resultado;
            resultado = oPersNatLN.RegistrarActualizarPersNatural(oClienteViewModel.PersNat);
            return Json(resultado);
        }


        [RequiresAuthenticationAttribute]
        public JsonResult RegistrarPersJuridica(ClienteViewModel oClienteViewModel)
        {
            PersonaJurLN oPersJurLN = new PersonaJurLN();
            int resultado;
            resultado = oPersJurLN.RegistrarActualizarPersJuridico(oClienteViewModel.PersJur);
            return Json(resultado);
        }

        public JsonResult CargoDatosCliente(int nPersId, string cTipo)
        {
            object cliente = null;

            if (cTipo == "N")
            {
                PersonaNatLN oPersNatLN = new PersonaNatLN();
                PersonaNat oPersNat = new PersonaNat();
                oPersNat = oPersNatLN.CargarDatosClienteNatural(nPersId);
                cliente = oPersNat;
            }
            else if (cTipo == "J")
            {
                PersonaJurLN oPersJurLN = new PersonaJurLN();
                PersonaJur oPersJur = new PersonaJur();
                oPersJur = oPersJurLN.CargarDatosClienteJuridico(nPersId);
                cliente = oPersJur;
            }

            return Json(JsonConvert.SerializeObject(cliente));
        }


    }
}
