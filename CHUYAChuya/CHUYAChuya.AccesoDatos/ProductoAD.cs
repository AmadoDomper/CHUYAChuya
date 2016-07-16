using CHUYAChuya.AccesoDatos.Helper;
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
using CHUYAChuya.EntidadesNegocio;

namespace CHUYAChuya.AccesoDatos
{
    public class ProductoAD
    {
        private Database oDatabase = EnterpriseLibraryContainer.Current.GetInstance<Database>(Conexion.cnsCHUYAChuya);

        public List<Producto> ListaProductos()
        {
            List<Producto> ListaProductos = new List<Producto>();

            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.stp_sel_ListarProductos);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int inProdId = oIDataReader.GetOrdinal("nProdId");
                int icProdDesc = oIDataReader.GetOrdinal("cProdDesc");
                int inProdPrecioUnit = oIDataReader.GetOrdinal("nProdPrecioUnit");
                int icMedida = oIDataReader.GetOrdinal("cMedida");
                int ibProdSerLavado = oIDataReader.GetOrdinal("bProdSerLavado");
                int ibProdSerSecado = oIDataReader.GetOrdinal("bProdSerSecado");
                int ibProdSerPlanchado = oIDataReader.GetOrdinal("bProdSerPlanchado");
                int idProdReg = oIDataReader.GetOrdinal("dProdReg");

                while (oIDataReader.Read())
                {
                    Producto oProducto = new Producto();

                    oProducto.nProdId = DataUtil.DbValueToDefault<Int32>(oIDataReader[inProdId]);
                    oProducto.cProdDesc = DataUtil.DbValueToDefault<String>(oIDataReader[icProdDesc]);
                    oProducto.nProdPrecioUnit = DataUtil.DbValueToDefault<decimal>(oIDataReader[inProdPrecioUnit]);
                    oProducto.oProdMedida.cNombre = DataUtil.DbValueToDefault<String>(oIDataReader[icMedida]);
                    oProducto.bProdSerLavado = DataUtil.DbValueToDefault<Boolean>(oIDataReader[ibProdSerLavado]);
                    oProducto.bProdSerSecado = DataUtil.DbValueToDefault<Boolean>(oIDataReader[ibProdSerSecado]);
                    oProducto.bProdSerPlanchado = DataUtil.DbValueToDefault<Boolean>(oIDataReader[ibProdSerPlanchado]);
                    oProducto.dProdReg = DataUtil.DbValueToDefault<DateTime>(oIDataReader[idProdReg]);

                    ListaProductos.Add(oProducto);
                }
            }
            return ListaProductos;
        }

        public Producto CargoDatosProducto(int nProdId)
        {
            try
            {
                Producto oProducto = new Producto();

                DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.stp_sel_Producto);
                oDatabase.AddInParameter(oDbCommand, "@nProdId", DbType.Int32, (object)nProdId ?? DBNull.Value);

                using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
                {
                    int inProdId = oIDataReader.GetOrdinal("nProdId");
                    int icProdDesc = oIDataReader.GetOrdinal("cProdDesc");
                    int inProdPrecioUnit = oIDataReader.GetOrdinal("nProdPrecioUnit");
                    int inProdMedida = oIDataReader.GetOrdinal("nProdMedida");
                    int ibProdSerLavado = oIDataReader.GetOrdinal("bProdSerLavado");
                    int ibProdSerSecado = oIDataReader.GetOrdinal("bProdSerSecado");
                    int ibProdSerPlanchado = oIDataReader.GetOrdinal("bProdSerPlanchado");

                    while (oIDataReader.Read())
                    {
                        oProducto.nProdId = DataUtil.DbValueToDefault<Int32>(oIDataReader[inProdId]);
                        oProducto.cProdDesc = DataUtil.DbValueToDefault<String>(oIDataReader[icProdDesc]);
                        oProducto.nProdPrecioUnit = DataUtil.DbValueToDefault<decimal>(oIDataReader[inProdPrecioUnit]);
                        oProducto.oProdMedida.cConstanteID = DataUtil.DbValueToDefault<String>(oIDataReader[inProdMedida].ToString());
                        oProducto.bProdSerLavado = DataUtil.DbValueToDefault<Boolean>(oIDataReader[ibProdSerLavado]);
                        oProducto.bProdSerSecado = DataUtil.DbValueToDefault<Boolean>(oIDataReader[ibProdSerSecado]);
                        oProducto.bProdSerPlanchado = DataUtil.DbValueToDefault<Boolean>(oIDataReader[ibProdSerPlanchado]);

                    }
                }

                return oProducto;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public int RegistrarActualizarProducto(Producto oProducto)
        {
            int resultado = 0;

            try
            {
                using (SqlConnection oSqlConnection = new SqlConnection(Conexion.cnsCHUYAChuyaSQL))
                {
                    SqlCommand oSqlCommand = new SqlCommand();
                    oSqlCommand.CommandText = Procedimiento.stp_ins_upd_Producto;
                    oSqlCommand.CommandType = CommandType.StoredProcedure;
                    oSqlCommand.Connection = oSqlConnection;

                    oSqlCommand.Parameters.Add("@nProdId", SqlDbType.Int).Value = (object)oProducto.nProdId ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@cProdDesc", SqlDbType.VarChar, 200).Value = (object)oProducto.cProdDesc ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@nProdPrecioUnit", SqlDbType.Money, 20).Value = (object)oProducto.nProdPrecioUnit ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@nProdMedida", SqlDbType.TinyInt, 100).Value = (object)oProducto.oProdMedida.cConstanteID ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@bProdSerLavado", SqlDbType.Bit).Value = (object)oProducto.bProdSerLavado ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@bProdSerSecado", SqlDbType.Bit).Value = (object)oProducto.bProdSerSecado ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@bProdSerPlanchado", SqlDbType.Bit).Value = (object)oProducto.bProdSerPlanchado ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@cProdUsuReg", SqlDbType.VarChar, 4).Value = (object)oProducto.cProdUsuReg ?? DBNull.Value;

                    oSqlConnection.Open();

                    using (IDataReader oIDataReader = oSqlCommand.ExecuteReader())
                    {
                        int iResultado = oIDataReader.GetOrdinal("Resultado");

                        while (oIDataReader.Read())
                        {
                            resultado = DataUtil.DbValueToDefault<int>(oIDataReader[iResultado]);
                        }
                    }

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


        public List<Producto> BuscarProductos(int nProdId, string cProdDesc)
        {
            List<Producto> ListaProductos = new List<Producto>();

            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.stp_sel_BuscarProductos);
            oDatabase.AddInParameter(oDbCommand, "@nProdId", DbType.String, (object)nProdId ?? DBNull.Value);
            oDatabase.AddInParameter(oDbCommand, "@cProdDesc", DbType.String, (object)cProdDesc ?? DBNull.Value);


            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int inProdId = oIDataReader.GetOrdinal("nProdId");
                int icProdDesc = oIDataReader.GetOrdinal("cProdDesc");
                int inProdPrecioUnit = oIDataReader.GetOrdinal("nProdPrecioUnit");
                int icMedida = oIDataReader.GetOrdinal("cMedida");
                int ibProdSerLavado = oIDataReader.GetOrdinal("bProdSerLavado");
                int ibProdSerSecado = oIDataReader.GetOrdinal("bProdSerSecado");
                int ibProdSerPlanchado = oIDataReader.GetOrdinal("bProdSerPlanchado");

                while (oIDataReader.Read())
                {
                    Producto oProducto = new Producto();

                    oProducto.nProdId = DataUtil.DbValueToDefault<Int32>(oIDataReader[inProdId]);
                    oProducto.cProdDesc = DataUtil.DbValueToDefault<String>(oIDataReader[icProdDesc]);
                    oProducto.nProdPrecioUnit = DataUtil.DbValueToDefault<decimal>(oIDataReader[inProdPrecioUnit]);
                    oProducto.oProdMedida.cNombre = DataUtil.DbValueToDefault<String>(oIDataReader[icMedida]);
                    oProducto.bProdSerLavado = DataUtil.DbValueToDefault<Boolean>(oIDataReader[ibProdSerLavado]);
                    oProducto.bProdSerSecado = DataUtil.DbValueToDefault<Boolean>(oIDataReader[ibProdSerSecado]);
                    oProducto.bProdSerPlanchado = DataUtil.DbValueToDefault<Boolean>(oIDataReader[ibProdSerPlanchado]);

                    ListaProductos.Add(oProducto);
                }
            }
            return ListaProductos;
        }
    }
}
