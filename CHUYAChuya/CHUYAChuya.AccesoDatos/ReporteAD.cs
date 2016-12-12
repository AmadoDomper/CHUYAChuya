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

    }
}
