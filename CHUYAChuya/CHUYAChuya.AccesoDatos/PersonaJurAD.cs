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
    public class PersonaJurAD
    {
        private Database oDatabase = EnterpriseLibraryContainer.Current.GetInstance<Database>(Conexion.cnsCHUYAChuya);

        public int RegistrarActualizarPersJuridico(PersonaJur oPersJur)
        {
            int resultado = 0;

            try
            {
                using (SqlConnection oSqlConnection = new SqlConnection(Conexion.cnsCHUYAChuyaSQL))
                {
                    SqlCommand oSqlCommand = new SqlCommand();
                    oSqlCommand.CommandText = Procedimiento.stp_ins_upd_ClienteJuridico;
                    oSqlCommand.CommandType = CommandType.StoredProcedure;
                    oSqlCommand.Connection = oSqlConnection;

                    oSqlCommand.Parameters.Add("@nPersId", SqlDbType.Int).Value = oPersJur.oPers.nPersId;
                    oSqlCommand.Parameters.Add("@cPersTel1", SqlDbType.VarChar, 20).Value = (object)oPersJur.oPers.cPersTelefono1 ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@cPersTel2", SqlDbType.VarChar, 20).Value = (object)oPersJur.oPers.cPersTelefono2 ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@cPersEmail", SqlDbType.VarChar, 100).Value = (object)oPersJur.oPers.cPersEmail ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@cPersDir", SqlDbType.VarChar, 150).Value = (object)oPersJur.oPers.cPersDireccion ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@cPersUbigeo", SqlDbType.VarChar, 20).Value = (object)oPersJur.oPers.oPersUbigeo.cConstanteID ?? DBNull.Value;

                    oSqlCommand.Parameters.Add("@cPersJurEmpresa", SqlDbType.VarChar, 100).Value = (object)oPersJur.cPersJurEmpresa ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@cPersJurRep", SqlDbType.VarChar, 100).Value = (object)oPersJur.cPersJurRep ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@cPersJurRUC", SqlDbType.VarChar, 11).Value = (object)oPersJur.cPersJurRUC ?? "";
                    oSqlCommand.Parameters.Add("@nRes", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.Output;
                    //oSqlCommand.Parameters.Add("@dPersJurFecConst", SqlDbType.DateTime).Value = (oPersJur.dPersJurFecConst).Add(DateTime.Now.TimeOfDay);
                    //oSqlCommand.Parameters.Add("@nPersJurActividad", SqlDbType.SmallInt).Value = oPersJur.oPersJurActividad.cConstanteID;

                    oSqlConnection.Open();
                    oSqlCommand.ExecuteNonQuery();

                    resultado = (int)oSqlCommand.Parameters["@nRes"].Value;

                    oSqlConnection.Close();

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

        public PersonaJur CargarDatosClienteJuridico(int nPersId)
        {
            try
            {
                PersonaJur oPersonaJur = new PersonaJur();

                DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.stp_sel_ClienteJuridico);
                oDatabase.AddInParameter(oDbCommand, "@nPersId", DbType.Int32, nPersId);

                using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
                {
                    int inPersId = oIDataReader.GetOrdinal("nPersId");
                    int icPersTelefono1 = oIDataReader.GetOrdinal("cPersTelefono1");
                    int icPersTelefono2 = oIDataReader.GetOrdinal("cPersTelefono2");
                    int icPersEmail = oIDataReader.GetOrdinal("cPersEmail");
                    int icPersDireccion = oIDataReader.GetOrdinal("cPersDireccion");
                    int icPersUbigeo = oIDataReader.GetOrdinal("cPersUbigeo");

                    int icPersJurEmpresa = oIDataReader.GetOrdinal("cPersJurEmpresa");
                    int icPersJurRep = oIDataReader.GetOrdinal("cPersJurRep");
                    int icPersJurRUC = oIDataReader.GetOrdinal("cPersJurRUC");
                    //int idPersJurFecConst = oIDataReader.GetOrdinal("dPersJurFecConst");
                    //int inPersJurActividad = oIDataReader.GetOrdinal("nPersJurActividad");

                    while (oIDataReader.Read())
                    {

                        oPersonaJur.oPers.nPersId = DataUtil.DbValueToDefault<Int32>(oIDataReader[inPersId]);
                        oPersonaJur.oPers.cPersTelefono1 = DataUtil.DbValueToDefault<String>(oIDataReader[icPersTelefono1]);
                        oPersonaJur.oPers.cPersTelefono2 = DataUtil.DbValueToDefault<String>(oIDataReader[icPersTelefono2]);
                        oPersonaJur.oPers.cPersEmail = DataUtil.DbValueToDefault<String>(oIDataReader[icPersEmail]);
                        oPersonaJur.oPers.cPersDireccion = DataUtil.DbValueToDefault<String>(oIDataReader[icPersDireccion]);
                        oPersonaJur.oPers.oPersUbigeo.cConstanteID = DataUtil.DbValueToDefault<String>(oIDataReader[icPersUbigeo]);

                        oPersonaJur.cPersJurEmpresa = DataUtil.DbValueToDefault<String>(oIDataReader[icPersJurEmpresa]);
                        oPersonaJur.cPersJurRep = DataUtil.DbValueToDefault<String>(oIDataReader[icPersJurRep]);
                        oPersonaJur.cPersJurRUC = DataUtil.DbValueToDefault<String>(oIDataReader[icPersJurRUC]);
                        //oPersonaJur.dPersJurFecConst = DataUtil.DbValueToDefault<DateTime>(oIDataReader[idPersJurFecConst]);
                        //oPersonaJur.oPersJurActividad.cConstanteID = DataUtil.DbValueToDefault<String>(oIDataReader[inPersJurActividad].ToString());
                    }
                }

                return oPersonaJur;

            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
