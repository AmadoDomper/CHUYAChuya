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
        /// <returns>Lista en de Clientes en formato JSON</returns>
        public JsonResult ListaClientesPag(int nPage = 1, int nSize = 10)
        {
            PersonaLN oPersonaLN = new PersonaLN();
            ListaPaginada ListaClientesPag = new ListaPaginada();
            ListaClientesPag = oPersonaLN.ListaClientesPag(nPage, nSize);
            return Json(JsonConvert.SerializeObject(ListaClientesPag, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
        }

        /// <summary>
        /// Metodo para buscar clientes por DOI o Nombre
        /// </summary>
        /// <param name="cPersDOI">Documento del cliente</param>
        /// <param name="cNombre">Nombre del cliente</param>
        /// <returns></returns>
        public JsonResult BuscarClientes(string cPersDOI = null, string cNombre = null)
        {
            PersonaLN oPersonaLN = new PersonaLN();
            List<Persona> ListaClientes = new List<Persona>();
            ListaClientes = oPersonaLN.BuscarClientes(cPersDOI, cNombre);
            return Json(JsonConvert.SerializeObject(ListaClientes, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
        }

        public JsonResult BuscarProveedores(string cPersRUC = null, string cNombre = null)
        {
            PersonaLN oPersonaLN = new PersonaLN();
            List<Persona> ListaProveedores = new List<Persona>();
            ListaProveedores = oPersonaLN.BuscarProveedores(cPersRUC, cNombre);
            return Json(JsonConvert.SerializeObject(ListaProveedores, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
        }


        /// <summary>
        /// Metodo para el mantenimiento de persona Natural
        /// </summary>
        /// <param name="oClienteViewModel"></param>
        /// <returns></returns>
        [RequiresAuthenticationAttribute]
        public JsonResult RegistrarPersNatural(ClienteViewModel oClienteViewModel)
        {
            PersonaNatLN oPersNatLN = new PersonaNatLN();
            int resultado;
            resultado = oPersNatLN.RegistrarActualizarPersNatural(oClienteViewModel.PersNat);
            return Json(resultado);
        }

        /// <summary>
        /// Metodo para el matenimiento de Persona Juridica
        /// </summary>
        /// <param name="oClienteViewModel"></param>
        /// <returns></returns>
        [RequiresAuthenticationAttribute]
        public JsonResult RegistrarPersJuridica(ClienteViewModel oClienteViewModel)
        {
            PersonaJurLN oPersJurLN = new PersonaJurLN();
            int resultado;
            resultado = oPersJurLN.RegistrarActualizarPersJuridico(oClienteViewModel.PersJur);
            return Json(resultado);
        }

        /// <summary>
        /// Metodo para obtener los datos del cliente por tipo
        /// </summary>
        /// <param name="nPersId">Codigo del cliente</param>
        /// <param name="cTipo">Tipo de cliente</param>
        /// <returns></returns>
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
