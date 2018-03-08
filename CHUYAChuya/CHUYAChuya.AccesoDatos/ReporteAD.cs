using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;
using CHUYAChuya.AccesoDatos.Helper;
using CHUYAChuya.EntidadesNegocio.Reportes;

namespace CHUYAChuya.AccesoDatos
{
    public class ReporteAD
    {
        private Database oDatabase = EnterpriseLibraryContainer.Current.GetInstance<Database>(Conexion.cnsCHUYAChuya);

        //public List<Object> ListarVentasIngresos(DateTime dIni, DateTime dFin)
        //{
        //    List<Object> oListVentas = new List<Object>();

        //    DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.stp_sel_ObtenerVentasIngresos);
        //    oDatabase.AddInParameter(oDbCommand, "@dFechaIni", DbType.Date, (object)dIni ?? DBNull.Value);
        //    oDatabase.AddInParameter(oDbCommand, "@dFechaFin", DbType.Date, (object)dFin ?? DBNull.Value);

        //    using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
        //    {
        //        int inNum = oIDataReader.GetOrdinal("nNum");
        //        int idMovFecha = oIDataReader.GetOrdinal("dMovFecha");
        //        int icTicketTipo = oIDataReader.GetOrdinal("cTicketTipo");
        //        int icTicketSerie = oIDataReader.GetOrdinal("cTicketSerie");
        //        int icTicketCorrelativo = oIDataReader.GetOrdinal("cTicketCorrelativo");
        //        int icDOI = oIDataReader.GetOrdinal("cDOI");
        //        int icPersDesc = oIDataReader.GetOrdinal("cPersDesc");
        //        int inNotaSubTotal = oIDataReader.GetOrdinal("nNotaSubTotal");

        //        while (oIDataReader.Read())
        //        {
        //            dynamic oRep = new System.Dynamic.ExpandoObject();

        //            oRep.nNum = DataUtil.DbValueToDefault<Int64>(oIDataReader[inNum]);
        //            oRep.dFecha = DataUtil.DbValueToDefault<DateTime>(oIDataReader[idMovFecha]).ToString("dd/MM/yyyy");
        //            oRep.cTpoT = DataUtil.DbValueToDefault<String>(oIDataReader[icTicketTipo]);
        //            oRep.cSerT = DataUtil.DbValueToDefault<String>(oIDataReader[icTicketSerie]);
        //            oRep.cCorrT = DataUtil.DbValueToDefault<String>(oIDataReader[icTicketCorrelativo]);
        //            oRep.cDoi = DataUtil.DbValueToDefault<String>(oIDataReader[icDOI]);
        //            oRep.cPersDesc = DataUtil.DbValueToDefault<String>(oIDataReader[icPersDesc]);
        //            oRep.nNotSub = DataUtil.DbValueToDefault<decimal>(oIDataReader[inNotaSubTotal]);

        //            oListVentas.Add(oRep);
        //        }
        //    }

        //    return oListVentas;
        //}


        public List<RepVentas> ListarVentasIngresos(DateTime dIni, DateTime dFin)
        {
            List<RepVentas> oListVentas = new List<RepVentas>();

            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.stp_sel_ObtenerVentasIngresos);
            oDatabase.AddInParameter(oDbCommand, "@dFechaIni", DbType.Date, (object)dIni ?? DBNull.Value);
            oDatabase.AddInParameter(oDbCommand, "@dFechaFin", DbType.Date, (object)dFin ?? DBNull.Value);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int inNum = oIDataReader.GetOrdinal("nNum");
                int idMovFecha = oIDataReader.GetOrdinal("dMovFecha");
                int icTicketTipo = oIDataReader.GetOrdinal("cTicketTipo");
                int icTicketSerie = oIDataReader.GetOrdinal("cTicketSerie");
                int icTicketCorrelativo = oIDataReader.GetOrdinal("cTicketCorrelativo");
                int icDOI = oIDataReader.GetOrdinal("cDOI");
                int icPersDesc = oIDataReader.GetOrdinal("cPersDesc");
                int inNotaSubTotal = oIDataReader.GetOrdinal("nNotaSubTotal");

                while (oIDataReader.Read())
                {
                    RepVentas oRep = new RepVentas();

                    oRep.nNumero = DataUtil.DbValueToDefault<Int64>(oIDataReader[inNum]);
                    oRep.dFechaE = DataUtil.DbValueToDefault<DateTime>(oIDataReader[idMovFecha]).ToString("dd/MM/yyyy");
                    oRep.cTipoTabla = DataUtil.DbValueToDefault<String>(oIDataReader[icTicketTipo]);
                    oRep.cSerie = DataUtil.DbValueToDefault<String>(oIDataReader[icTicketSerie]);
                    oRep.cCorrelativo = DataUtil.DbValueToDefault<String>(oIDataReader[icTicketCorrelativo]);
                    oRep.cDOI = DataUtil.DbValueToDefault<String>(oIDataReader[icDOI]);
                    oRep.cPersDesc = DataUtil.DbValueToDefault<String>(oIDataReader[icPersDesc]);
                    oRep.nNotaSub = DataUtil.DbValueToDefault<decimal>(oIDataReader[inNotaSubTotal]);

                    oListVentas.Add(oRep);
                }
            }
            return oListVentas;
        }

        public List<RepCompras> ListarCompras(DateTime dIni, DateTime dFin)
        {
            List<RepCompras> oListCompras = new List<RepCompras>();

            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.stp_sel_ObtenerComprasProveedores);
            oDatabase.AddInParameter(oDbCommand, "@dFechaIni", DbType.Date, (object)dIni ?? DBNull.Value);
            oDatabase.AddInParameter(oDbCommand, "@dFechaFin", DbType.Date, (object)dFin ?? DBNull.Value);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int inNum = oIDataReader.GetOrdinal("nNum");
                int idFechaEmision = oIDataReader.GetOrdinal("dFechaEmision");
                int icTipoTabla = oIDataReader.GetOrdinal("cTipoTabla");
                int inComprobante = oIDataReader.GetOrdinal("nComprobante");
                int icPersJurRUC = oIDataReader.GetOrdinal("cPersJurRUC");
                int icPersJurEmpresa = oIDataReader.GetOrdinal("cPersJurEmpresa");
                int inMonto = oIDataReader.GetOrdinal("nMonto");

                while (oIDataReader.Read())
                {
                    RepCompras oRep = new RepCompras();

                    oRep.nNumero = DataUtil.DbValueToDefault<Int64>(oIDataReader[inNum]);
                    oRep.dFechaE = DataUtil.DbValueToDefault<DateTime>(oIDataReader[idFechaEmision]).ToString("dd/MM/yyyy");
                    oRep.cTipoTabla = DataUtil.DbValueToDefault<String>(oIDataReader[icTipoTabla]);
                    oRep.cComprobante = DataUtil.DbValueToDefault<String>(oIDataReader[inComprobante]);
                    oRep.cDOI = DataUtil.DbValueToDefault<String>(oIDataReader[icPersJurRUC]);
                    oRep.cPersDesc = DataUtil.DbValueToDefault<String>(oIDataReader[icPersJurEmpresa]);
                    oRep.nMonto = DataUtil.DbValueToDefault<decimal>(oIDataReader[inMonto]);

                    oListCompras.Add(oRep);
                }
            }
            return oListCompras;
        }

        public List<RepNotaEntregas> ListarNotaEntrega(DateTime dIni, DateTime dFin)
        {
            List<RepNotaEntregas> oListNotaEntrega = new List<RepNotaEntregas>();

            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.stp_sel_ObtenerNotaEntregas);
            oDatabase.AddInParameter(oDbCommand, "@dFechaIni", DbType.Date, (object)dIni ?? DBNull.Value);
            oDatabase.AddInParameter(oDbCommand, "@dFechaFin", DbType.Date, (object)dFin ?? DBNull.Value);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int idFechaReg = oIDataReader.GetOrdinal("dFechaReg");
                int inNotaEntId = oIDataReader.GetOrdinal("nNotaEntId");
                int icPersDesc = oIDataReader.GetOrdinal("cPersDesc");
                int icProdDesc = oIDataReader.GetOrdinal("cProdDesc");
                int ibProdSerLavado = oIDataReader.GetOrdinal("bProdSerLavado");
                int ibProdSerSecado = oIDataReader.GetOrdinal("bProdSerSecado");
                int ibProdSerPlanchado = oIDataReader.GetOrdinal("bProdSerPlanchado");
                int icPrenda = oIDataReader.GetOrdinal("cPrenda");
                int icPeso = oIDataReader.GetOrdinal("cPeso");
                int icDetProdPrecioUnit = oIDataReader.GetOrdinal("cDetProdPrecioUnit");
                int icDetImporte = oIDataReader.GetOrdinal("cDetImporte");
                int icPendiente = oIDataReader.GetOrdinal("cPendiente");
                int icPagado = oIDataReader.GetOrdinal("cPagado");
                int icEntregado = oIDataReader.GetOrdinal("cEntregado");
                int icAnulado = oIDataReader.GetOrdinal("cAnulado");
                int icBoleta = oIDataReader.GetOrdinal("cBoleta");
                int icFactura = oIDataReader.GetOrdinal("cFactura");
                int icFechaPago = oIDataReader.GetOrdinal("cFechaPago");

                while (oIDataReader.Read())
                {
                    RepNotaEntregas oRep = new RepNotaEntregas();

                    oRep.dFechaReg = DataUtil.DbValueToDefault<String>(oIDataReader[idFechaReg]);
                    oRep.nNotaEntId = DataUtil.DbValueToDefault<int>(oIDataReader[inNotaEntId]);
                    oRep.cPersDesc = DataUtil.DbValueToDefault<String>(oIDataReader[icPersDesc]);
                    oRep.cProdDesc = DataUtil.DbValueToDefault<String>(oIDataReader[icProdDesc]);
                    oRep.bProdSerLavado = DataUtil.DbValueToDefault<Boolean>(oIDataReader[ibProdSerLavado]);
                    oRep.bProdSerSecado = DataUtil.DbValueToDefault<Boolean>(oIDataReader[ibProdSerSecado]);
                    oRep.bProdSerPlanchado = DataUtil.DbValueToDefault<Boolean>(oIDataReader[ibProdSerPlanchado]);
                    oRep.cPrenda = DataUtil.DbValueToDefault<String>(oIDataReader[icPrenda]);
                    oRep.cPeso = DataUtil.DbValueToDefault<String>(oIDataReader[icPeso]);
                    oRep.cDetPrecioUnit = DataUtil.DbValueToDefault<String>(oIDataReader[icDetProdPrecioUnit]);
                    oRep.cDetImporte = DataUtil.DbValueToDefault<String>(oIDataReader[icDetImporte]);
                    oRep.cPendiente = DataUtil.DbValueToDefault<String>(oIDataReader[icPendiente]);
                    oRep.cPagado = DataUtil.DbValueToDefault<String>(oIDataReader[icPagado]);
                    oRep.cEntregado = DataUtil.DbValueToDefault<String>(oIDataReader[icEntregado]);
                    oRep.cAnulado = DataUtil.DbValueToDefault<String>(oIDataReader[icAnulado]);
                    oRep.cBoleta = DataUtil.DbValueToDefault<String>(oIDataReader[icBoleta]);
                    oRep.cFactura = DataUtil.DbValueToDefault<String>(oIDataReader[icFactura]);
                    oRep.dFechaPago = DataUtil.DbValueToDefault<String>(oIDataReader[icFechaPago]);

                    oListNotaEntrega.Add(oRep);
                }
            }

            return oListNotaEntrega;
        }

    }
}
