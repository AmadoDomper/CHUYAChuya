using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CHUYAChuya.AccesoDatos;
using CHUYAChuya.EntidadesNegocio;

namespace CHUYAChuya.LogicaNegocio
{
    public class CajaLN
    {
        CajaAD oCajaAD;

        public CajaLN()
        {
            oCajaAD = new CajaAD();
        }

        public int RegistrarSalidaEfePagoProv(int nPersId, decimal nMontoSalida, string cComprobante, byte nTipoComp, DateTime dFechaEmi, string cUsuario, string cAgencia)
        {
            return oCajaAD.RegistrarSalidaEfePagoProv(nPersId, nMontoSalida, cComprobante, nTipoComp, dFechaEmi, cUsuario, cAgencia);
        }

        public int RegistrarSalidaEfe(decimal nMontoSalida, string cMotivo,string cUsuario, string cAgencia)
        {
            return oCajaAD.RegistrarSalidaEfe(nMontoSalida, cMotivo, cUsuario, cAgencia);
        }

        public int RegistrarEntradaEfe(decimal nMontoEntrada, string cUsuario, string cAgencia)
        {
            return oCajaAD.RegistrarEntradaEfe(nMontoEntrada, cUsuario, cAgencia);
        }

        public List<MovCaja> BuscarMovCaja(string cMovDesc = null)
        {
            return oCajaAD.BuscarMovCaja(cMovDesc);
        }


        //CORTE DE CAJA

        public Corte CargaDetalleCorte(string cUsuario, DateTime dFecha)
        {
            return oCajaAD.CargaDetalleCorte(cUsuario, dFecha);
        }

        //END CORTE CAJA
    }
}
