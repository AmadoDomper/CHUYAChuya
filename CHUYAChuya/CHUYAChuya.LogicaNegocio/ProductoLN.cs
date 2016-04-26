using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CHUYAChuya.AccesoDatos;
using CHUYAChuya.EntidadesNegocio;

namespace CHUYAChuya.LogicaNegocio
{
    public class ProductoLN
    {
        ProductoAD oProductoAD;

        public ProductoLN()
        {
            oProductoAD = new ProductoAD();
        }

        public List<Producto> ListaProductos()
        {
            return oProductoAD.ListaProductos();
        }

        public Producto CargoDatosProducto(int nProdId)
        {
            return oProductoAD.CargoDatosProducto(nProdId);
        }

        public int RegistrarActualizarProducto(Producto oProducto)
        {
            return oProductoAD.RegistrarActualizarProducto(oProducto);
        }

        public List<Producto> BuscarProductos(int nProdId, string cProdDesc)
        {
            return oProductoAD.BuscarProductos(nProdId, cProdDesc);
        }
    }
}
