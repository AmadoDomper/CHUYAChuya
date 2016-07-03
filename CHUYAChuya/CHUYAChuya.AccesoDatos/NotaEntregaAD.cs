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
using CHUYAChuya.EntidadesNegocio;

namespace CHUYAChuya.AccesoDatos
{
    public class NotaEntregaAD
    {
        private Database oDatabase = EnterpriseLibraryContainer.Current.GetInstance<Database>(Conexion.cnsCHUYAChuya);

        public int RegistrarNotaEntrega(NotaEntrega oNotEnt)
        {
            int resultado = 0;

            try
            {
                using (SqlConnection oSqlConnection = new SqlConnection(Conexion.cnsCHUYAChuyaSQL))
                {
                    SqlCommand oSqlCommand = new SqlCommand();
                    oSqlCommand.CommandText = Procedimiento.stp_ins_NotaEntrega;
                    oSqlCommand.CommandType = CommandType.StoredProcedure;
                    oSqlCommand.Connection = oSqlConnection;

                    oSqlCommand.Parameters.Add("@nNotaEntId", SqlDbType.Int).Value = (object)oNotEnt.nNotaEntId ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@nPersId", SqlDbType.Int).Value = (object)oNotEnt.oPers.nPersId ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@cNotaDireccion", SqlDbType.VarChar, 150).Value = (object)oNotEnt.cNotaDireccion ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@cNotaComentario", SqlDbType.VarChar, 250).Value = (object)oNotEnt.cNotaComentario ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@dFechaReg", SqlDbType.DateTime).Value = (object)oNotEnt.dFechaReg ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@dFechaEntrega", SqlDbType.DateTime).Value = (object)oNotEnt.dFechaEntrega ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@nNotaSubTotal", SqlDbType.Money).Value = (object)oNotEnt.nNotaSubTotal ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@nNotaDescuento", SqlDbType.Money).Value = (object)oNotEnt.nNotaDescuento ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@nNotaAnticipo", SqlDbType.Money).Value = (object)oNotEnt.nNotaAnticipo ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@nNotaEfectivo", SqlDbType.Money).Value = (object)oNotEnt.nNotaEfectivo ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@nNotaCambio", SqlDbType.Money).Value = (object)oNotEnt.nNotaCambio ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@nNotaMontoTotal", SqlDbType.Money).Value = (object)oNotEnt.nNotaMontoTotal ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@nNotaEstado", SqlDbType.TinyInt).Value = (object)Convert.ToByte(oNotEnt.oNotaEstado.cConstanteID) ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@cNotaUsuReg", SqlDbType.VarChar, 4).Value = (object)oNotEnt.cNotaUsuReg ?? DBNull.Value;

                    oSqlCommand.Parameters.Add("@T_NotaEntregaProducto", SqlDbType.Structured).Value = NotaEntregaProductoCollection.TSqlDataRecord(oNotEnt.ListaNotaEntProd.ToList());
                    oSqlConnection.Open();

                    using (IDataReader oIDataReader = oSqlCommand.ExecuteReader())
                    {
                        int iResultado = oIDataReader.GetOrdinal("Resultado");

                        while (oIDataReader.Read())
                        {
                            resultado = DataUtil.DbValueToDefault<int>(oIDataReader[iResultado]);
                        }
                    }

                    //if (resultado[1].Length != 18 || resultado[1].IndexOf("109", 0, 3) == -1)
                    //{
                    //    /*Error de BD*/
                    //    resultado[0] = "2";
                    //    resultado[1] = "Ha ocurrido un error: " + "TIPO 2-" + resultado[1];
                    //}
                    //else
                    //{
                    //    /*Resultado OK*/
                    //    if (oCred.oColocaciones.oProducto.cCtaCod != null)
                    //    {
                    //        resultado[0] = "1";
                    //        resultado[1] = "Se actualizó correctamente los datos";
                    //    }
                    //    else
                    //    {
                    //        resultado[0] = "0";
                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {
                resultado = -1;
                //oError.cErrDescription = ex.Message.ToString();
                //oError.cErrSource = ex.StackTrace.ToString();
                //oError.cProceso = ex.TargetSite.ToString();

                //resultado[0] = "3";
                //resultado[1] = "Ha ocurrido un error: " + "TIPO 3-" + oErrorAD.InsertaErrorAplicacion(oError);
            }
            return resultado;
        }

        public List<NotaEntrega> BuscarNotaEntregas(int nNotaEst, int nNotaEntId = -1, string cPersDOI = null, string cPersDesc = null, DateTime? dIni = null, DateTime? dFin = null)
        {
            List<NotaEntrega> ListaNotasEntrega = new List<NotaEntrega>();

            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.stp_sel_BuscarNotaEntregas);
            oDatabase.AddInParameter(oDbCommand, "@nNotaEstado", DbType.Int16, (object)nNotaEst ?? DBNull.Value);
            oDatabase.AddInParameter(oDbCommand, "@nNotaEntId", DbType.Int32, (object)nNotaEntId ?? DBNull.Value);
            oDatabase.AddInParameter(oDbCommand, "@cPersDOI", DbType.String, (object)cPersDOI ?? DBNull.Value);
            oDatabase.AddInParameter(oDbCommand, "@cPersDesc", DbType.String, (object)cPersDesc ?? DBNull.Value);
            oDatabase.AddInParameter(oDbCommand, "@dFechaEntI", DbType.Date, (object)dIni ?? DBNull.Value);
            oDatabase.AddInParameter(oDbCommand, "@dFechaEntF", DbType.Date, (object)dFin ?? DBNull.Value);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int inNotaEntId = oIDataReader.GetOrdinal("nNotaEntId");
                int icPersDesc = oIDataReader.GetOrdinal("cPersDesc");
                int icDOI = oIDataReader.GetOrdinal("cDOI");
                int idFechaReg = oIDataReader.GetOrdinal("dFechaReg");
                int idFechaEntrega = oIDataReader.GetOrdinal("dFechaEntrega");
                int inNotaMontoTotal = oIDataReader.GetOrdinal("nNotaMontoTotal");
                int inNotaEstado = oIDataReader.GetOrdinal("nNotaEstado");

                while (oIDataReader.Read())
                {
                    NotaEntrega oNotaEntrega = new NotaEntrega();

                    oNotaEntrega.nNotaEntId = DataUtil.DbValueToDefault<Int32>(oIDataReader[inNotaEntId]);
                    oNotaEntrega.oPers.cPersDesc = DataUtil.DbValueToDefault<String>(oIDataReader[icPersDesc]);
                    oNotaEntrega.oPers.cPersDOI = DataUtil.DbValueToDefault<String>(oIDataReader[icDOI]);
                    oNotaEntrega.dFechaReg = DataUtil.DbValueToDefault<DateTime>(oIDataReader[idFechaReg]);
                    oNotaEntrega.dFechaEntrega = DataUtil.DbValueToDefault<DateTime>(oIDataReader[idFechaEntrega]);
                    oNotaEntrega.nNotaMontoTotal = DataUtil.DbValueToDefault<decimal>(oIDataReader[inNotaMontoTotal]);
                    oNotaEntrega.oNotaEstado.cConstanteID = DataUtil.DbValueToDefault<String>(oIDataReader[inNotaEstado].ToString());

                    ListaNotasEntrega.Add(oNotaEntrega);
                }
            }
            return ListaNotasEntrega;
        }


        public NotaEntrega CargoDatosNotaEntrega(int nNotaId)
        {
            try
            {
                NotaEntrega oNotaEntrega = new NotaEntrega();

                DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.stp_sel_CargarNotaEntrega);
                oDatabase.AddInParameter(oDbCommand, "@nNotaId", DbType.Int32, (object)nNotaId ?? DBNull.Value);

                using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
                {
                    int inPersId = oIDataReader.GetOrdinal("nPersId");
                    int icPersDesc = oIDataReader.GetOrdinal("cPersDesc");
                    int icDOI = oIDataReader.GetOrdinal("cDOI");
                    int icPersTelefono1 = oIDataReader.GetOrdinal("cPersTelefono1");
                    int inNotaEntId = oIDataReader.GetOrdinal("nNotaEntId");
                    int idFechaReg = oIDataReader.GetOrdinal("dFechaReg");
                    int idFechaEntrega = oIDataReader.GetOrdinal("dFechaEntrega");
                    int inNotaSubTotal = oIDataReader.GetOrdinal("nNotaSubTotal");

                    int inNotaAnticipo = oIDataReader.GetOrdinal("nNotaAnticipo");
                    int inNotaDescuento = oIDataReader.GetOrdinal("nNotaDescuento");
                    int inNotaEfectivo = oIDataReader.GetOrdinal("nNotaEfectivo");
                    int inNotaCambio = oIDataReader.GetOrdinal("nNotaCambio");


                    int inNotaMontoTotal = oIDataReader.GetOrdinal("nNotaMontoTotal");
                    int inNotaEstado = oIDataReader.GetOrdinal("nNotaEstado");

                    while (oIDataReader.Read())
                    {
                        oNotaEntrega.oPers.nPersId = DataUtil.DbValueToDefault<Int32>(oIDataReader[inPersId]);
                        oNotaEntrega.oPers.cPersDesc = DataUtil.DbValueToDefault<String>(oIDataReader[icPersDesc]);
                        oNotaEntrega.oPers.cPersDOI = DataUtil.DbValueToDefault<String>(oIDataReader[icDOI]);
                        oNotaEntrega.oPers.cPersTelefono1 = DataUtil.DbValueToDefault<String>(oIDataReader[icPersTelefono1]);
                        oNotaEntrega.nNotaEntId = DataUtil.DbValueToDefault<Int32>(oIDataReader[inNotaEntId]);
                        oNotaEntrega.dFechaReg = DataUtil.DbValueToDefault<DateTime>(oIDataReader[idFechaReg]);
                        oNotaEntrega.dFechaEntrega = DataUtil.DbValueToDefault<DateTime>(oIDataReader[idFechaEntrega]);
                        oNotaEntrega.nNotaSubTotal = DataUtil.DbValueToDefault<decimal>(oIDataReader[inNotaSubTotal]);

                        oNotaEntrega.nNotaSubTotal = DataUtil.DbValueToDefault<decimal>(oIDataReader[inNotaAnticipo]);
                        oNotaEntrega.nNotaDescuento = DataUtil.DbValueToDefault<decimal>(oIDataReader[inNotaDescuento]);
                        oNotaEntrega.nNotaSubTotal = DataUtil.DbValueToDefault<decimal>(oIDataReader[inNotaEfectivo]);
                        oNotaEntrega.nNotaSubTotal = DataUtil.DbValueToDefault<decimal>(oIDataReader[inNotaCambio]);


                        oNotaEntrega.nNotaMontoTotal = DataUtil.DbValueToDefault<decimal>(oIDataReader[inNotaMontoTotal]);
                        oNotaEntrega.oNotaEstado.cConstanteID = DataUtil.DbValueToDefault<String>(oIDataReader[inNotaEstado].ToString());

                        oNotaEntrega.ListaNotaEntProd = ListaNotaEntProductos(nNotaId);
                    }
                }

                return oNotaEntrega;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<NotaEntProd> ListaNotaEntProductos(int nNotaId)
        {
            List<NotaEntProd> ListaNotaEntProd = new List<NotaEntProd>();

            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.stp_sel_ListaNotaEntProductos);
            oDatabase.AddInParameter(oDbCommand, "@nNotaId", DbType.String, nNotaId);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int inProdId = oIDataReader.GetOrdinal("nProdId");
                int icProdDesc = oIDataReader.GetOrdinal("cProdDesc");
                int ibProdSerLavado = oIDataReader.GetOrdinal("bProdSerLavado");
                int ibProdSerSecado = oIDataReader.GetOrdinal("bProdSerSecado");
                int ibProdSerPlanchado = oIDataReader.GetOrdinal("bProdSerPlanchado");
                int inDetCantidad = oIDataReader.GetOrdinal("nDetCantidad");
                int inDetProdPrecioUnit = oIDataReader.GetOrdinal("nDetProdPrecioUnit");
                int inDetImporte = oIDataReader.GetOrdinal("nDetImporte");

                while (oIDataReader.Read())
                {
                    NotaEntProd oNotaEntPro = new NotaEntProd();

                    oNotaEntPro.oProd.nProdId = DataUtil.DbValueToDefault<Int32>(oIDataReader[inProdId]);
                    oNotaEntPro.oProd.cProdDesc = DataUtil.DbValueToDefault<String>(oIDataReader[icProdDesc]);
                    oNotaEntPro.oProd.bProdSerLavado = DataUtil.DbValueToDefault<Boolean>(oIDataReader[ibProdSerLavado]);
                    oNotaEntPro.oProd.bProdSerSecado = DataUtil.DbValueToDefault<Boolean>(oIDataReader[ibProdSerSecado]);
                    oNotaEntPro.oProd.bProdSerPlanchado = DataUtil.DbValueToDefault<Boolean>(oIDataReader[ibProdSerPlanchado]);

                    oNotaEntPro.nDetCantidad = DataUtil.DbValueToDefault<decimal>(oIDataReader[inDetCantidad]);
                    oNotaEntPro.nProdPrecioUnit = DataUtil.DbValueToDefault<decimal>(oIDataReader[inDetProdPrecioUnit]);
                    oNotaEntPro.nDetImporte = DataUtil.DbValueToDefault<decimal>(oIDataReader[inDetImporte]);

                    ListaNotaEntProd.Add(oNotaEntPro);
                }
            }
            return ListaNotaEntProd;
        }



    }
}
