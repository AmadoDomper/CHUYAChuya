using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CHUYAChuya.EntidadesNegocio;
using CHUYAChuya.AccesoDatos;

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

        public List<MovCaja> BuscarMovCaja(string cUsuario , string cMovDesc = null)
        {
            return oCajaAD.BuscarMovCaja(cUsuario, cMovDesc);
        }


        //CORTE DE CAJA

        public CierreDatos UltimaFechaCajaDia()
        {
            return oCajaAD.UltimaFechaCajaDia();
        }

        public CierreDatos CargaUsuariosCierres(DateTime dFecha)
        {
            CierreDatos oCierreDatos = new CierreDatos();
            oCierreDatos = oCajaAD.CargaUsuariosCierres(dFecha);


            foreach (var oListaUC in oCierreDatos.oListUsuarioCierres)
	        {
                foreach (var oCierre in oListaUC.oListCierres)
                {
                    oCierre.cFecha = oCierre.dFechaIni.ToString("hh:mm tt") +"-"+ oCierre.dFechaFin.ToString("hh:mm tt");
                }
	        }
            
            return oCierreDatos;
        }

        public Cierre CargaDetalleCierre(string cUsuario, DateTime dFecha, int nCierre)
        {
            return oCajaAD.CargaDetalleCierre(cUsuario, dFecha, nCierre);
        }

        //END CORTE CAJA



        public int RegistrarAperturaCaja(string cUsuarioOpe, decimal nMontoIni, DateTime dFechaApe, string cUsuarioSup, string cAgencia)
        {
            return oCajaAD.RegistrarAperturaCaja(cUsuarioOpe,nMontoIni,dFechaApe,cUsuarioSup,cAgencia);
        }

        public int RegistrarAsigMonto(string cUsuarioOpe, decimal nMontoAsig, string cUsuarioSup, string cAgencia)
        {
            return oCajaAD.RegistrarAsigMonto(cUsuarioOpe, nMontoAsig, cUsuarioSup, cAgencia);
        }

        public int RegistrarConfDineroIni(int nCCId, string cUsuario, string cAgencia)
        {
            return oCajaAD.RegistrarConfDineroIni(nCCId, cUsuario, cAgencia);
        }

        public CajeroCaja BuscarConfirmacionDineroPendiente(string cUsuario)
        {
            return oCajaAD.BuscarConfirmacionDineroPendiente(cUsuario);
        }

        public Boolean CajaDiaAbierto()
        {
            return oCajaAD.CajaDiaAbierto();
        }

        public int RealizarCierreCaja(string cUsuario, string cAgencia, decimal nCont, decimal nDif)
        {
            return oCajaAD.RealizarCierreCaja(cUsuario, cAgencia, nCont, nDif);
        }

        public int RealizarCierreCajaDia(string cUsuario, string cAgencia)
        {
            return oCajaAD.RealizarCierreCajaDia(cUsuario, cAgencia);
        }

        public TicketCierre ObtenerDatosCierreImp(int nCajeCaId)
        {
            return oCajaAD.ObtenerDatosCierreImp(nCajeCaId);
        }

        public TicketCierre ObtenerDatosCierreDiaImp(int nCajeId)
        {
            return oCajaAD.ObtenerDatosCierreDiaImp(nCajeId);
        }

        public string ObtenerUsuarioIniciaDia(string cUsuario)
        {
            return oCajaAD.ObtenerUsuarioIniciaDia(cUsuario);
        }

        public decimal ObtenerUltimoSaldoCaja()
        {
            return oCajaAD.ObtenerUltimoSaldoCaja();
        }

    }
}
