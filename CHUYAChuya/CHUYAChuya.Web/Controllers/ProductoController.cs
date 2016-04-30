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
    public class ProductoController : Controller
    {
        //
        // GET: /Producto/

        public ActionResult Productos()
        {
            return View();
        }

        public JsonResult ListaProductos()
        {
            ProductoLN oProductoLN = new ProductoLN();
            List<Producto> ListaProductos = new List<Producto>();
            ListaProductos = oProductoLN.ListaProductos();
            return Json(JsonConvert.SerializeObject(ListaProductos, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
        }

        public JsonResult BuscarProductos(int nProdId = -1, string cProdDesc = null)
        {
            ProductoLN oProductoLN = new ProductoLN();
            List<Producto> ListaProductos = new List<Producto>();
            ListaProductos = oProductoLN.BuscarProductos(nProdId, cProdDesc);
            return Json(JsonConvert.SerializeObject(ListaProductos, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
        }

        [RequiresAuthenticationAttribute]
        public JsonResult RegistrarProducto(ProductoViewModel oProductoViewModel)
        {
            ProductoLN oProductoLN = new ProductoLN();
            int resultado;
            oProductoViewModel.Producto.cProdUsuReg = ((Usuario)Session["Datos"]).cUsuNombre;
            resultado = oProductoLN.RegistrarActualizarProducto(oProductoViewModel.Producto);
            return Json(resultado);
        }

        public JsonResult CargoDatosProducto(int nProdId)
        {
            ProductoLN oProductoLN = new ProductoLN();
            Producto oProdNat = new Producto();
            oProdNat = oProductoLN.CargoDatosProducto(nProdId);

            return Json(JsonConvert.SerializeObject(oProdNat));
        }







    }
}
