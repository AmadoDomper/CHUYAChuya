﻿using System;
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

        //APERTURA DE CAJA

        public int RegistrarAperturaCaja(string cUsuarioOpe, decimal nMontoIni, DateTime dFechaApe, string cUsuarioSup, string cAgencia)
        {
            return oCajaAD.RegistrarAperturaCaja(cUsuarioOpe,nMontoIni,dFechaApe,cUsuarioSup,cAgencia);
        }
        //END APERTURA DE CAJA
    }
}
