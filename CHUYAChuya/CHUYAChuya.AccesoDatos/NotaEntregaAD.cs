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
                    //oSqlCommand.Parameters.Add("@dFechaReg", SqlDbType.DateTime).Value = (object)oNotEnt.dFechaReg ?? DBNull.Value;
                    //oSqlCommand.Parameters.Add("@dFechaEntrega", SqlDbType.DateTime).Value = (object)oNotEnt.dFechaEntrega ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@nNotaSubTotal", SqlDbType.Money).Value = (object)oNotEnt.nNotaSubTotal ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@nNotaMontoTotal", SqlDbType.Money).Value = (object)oNotEnt.nNotaMontoTotal ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@nNotaEstado", SqlDbType.TinyInt).Value = (object)Convert.ToByte(oNotEnt.oNotaEstado.cConstanteID) ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@cNotaUsuReg", SqlDbType.VarChar,4).Value = (object)oNotEnt.cNotaUsuReg ?? DBNull.Value;

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

    }
}
