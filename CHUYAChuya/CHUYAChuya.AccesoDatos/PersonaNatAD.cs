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
    public class PersonaNatAD
    {
        private Database oDatabase = EnterpriseLibraryContainer.Current.GetInstance<Database>(Conexion.cnsCHUYAChuya);

        public int RegistrarActualizarPersNatural(PersonaNat oPersNat)
        {
            int resultado = 0;

            try
            {
                using (SqlConnection oSqlConnection = new SqlConnection(Conexion.cnsCHUYAChuyaSQL))
                {
                    SqlCommand oSqlCommand = new SqlCommand();
                    oSqlCommand.CommandText = Procedimiento.stp_ins_upd_ClienteNatural;
                    oSqlCommand.CommandType = CommandType.StoredProcedure;
                    oSqlCommand.Connection = oSqlConnection;

                    oSqlCommand.Parameters.Add("@nPersId", SqlDbType.Int).Value = oPersNat.oPers.nPersId;
                    oSqlCommand.Parameters.Add("@cPersTel1", SqlDbType.VarChar, 20).Value = (object)oPersNat.oPers.cPersTelefono1 ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@cPersTel2", SqlDbType.VarChar, 20).Value = (object)oPersNat.oPers.cPersTelefono2 ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@cPersEmail", SqlDbType.VarChar, 100).Value = (object)oPersNat.oPers.cPersEmail ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@cPersDir", SqlDbType.VarChar, 150).Value = (object)oPersNat.oPers.cPersDireccion ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@cPersUbigeo", SqlDbType.VarChar, 20).Value = (object)oPersNat.oPers.oPersUbigeo.cConstanteID ?? DBNull.Value;

                    oSqlCommand.Parameters.Add("@cPersNatNombres", SqlDbType.VarChar, 100).Value = (object)oPersNat.cPersNatNombres ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@cPersNatApellido", SqlDbType.VarChar, 100).Value = (object)oPersNat.cPersNatApellido ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@cPersNatDOI", SqlDbType.VarChar, 11).Value = (object)oPersNat.cPersNatDOI ?? "";
                    oSqlCommand.Parameters.Add("@dPersNatNac", SqlDbType.DateTime).Value = (oPersNat.dPersNatNac == default(DateTime) ? (object)DBNull.Value : oPersNat.dPersNatNac);
                    oSqlCommand.Parameters.Add("@cPersNatSexo", SqlDbType.VarChar, 1).Value = (object)oPersNat.oPersNatSexo.cConstanteID ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@nRes", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.Output;

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




        public PersonaNat CargarDatosClienteNatural(int nPersId)
        {
            try
            {
                PersonaNat oPersonaNat = new PersonaNat();

                DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.stp_sel_ClienteNatural);
                oDatabase.AddInParameter(oDbCommand, "@nPersId", DbType.Int32, nPersId);

                using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
                {
                    int inPersId = oIDataReader.GetOrdinal("nPersId");
                    int icPersTelefono1 = oIDataReader.GetOrdinal("cPersTelefono1");
                    int icPersTelefono2 = oIDataReader.GetOrdinal("cPersTelefono2");
                    int icPersEmail = oIDataReader.GetOrdinal("cPersEmail");
                    int icPersDireccion = oIDataReader.GetOrdinal("cPersDireccion");
                    int icPersUbigeo = oIDataReader.GetOrdinal("cPersUbigeo");

                    int icPersNatNombres = oIDataReader.GetOrdinal("cPersNatNombres");
                    int icPersNatApellido = oIDataReader.GetOrdinal("cPersNatApellido");
                    int icPersNatDOI = oIDataReader.GetOrdinal("cPersNatDOI");
                    int idPersNatNac = oIDataReader.GetOrdinal("dPersNatNac");
                    int icPersNatSexo = oIDataReader.GetOrdinal("cPersNatSexo");

                    while (oIDataReader.Read())
                    {

                        oPersonaNat.oPers.nPersId = DataUtil.DbValueToDefault<Int32>(oIDataReader[inPersId]);
                        oPersonaNat.oPers.cPersTelefono1 = DataUtil.DbValueToDefault<String>(oIDataReader[icPersTelefono1]);
                        oPersonaNat.oPers.cPersTelefono2 = DataUtil.DbValueToDefault<String>(oIDataReader[icPersTelefono2]);
                        oPersonaNat.oPers.cPersEmail = DataUtil.DbValueToDefault<String>(oIDataReader[icPersEmail]);
                        oPersonaNat.oPers.cPersDireccion = DataUtil.DbValueToDefault<String>(oIDataReader[icPersDireccion]);
                        oPersonaNat.oPers.oPersUbigeo.cConstanteID = DataUtil.DbValueToDefault<String>(oIDataReader[icPersUbigeo]);

                        oPersonaNat.cPersNatNombres = DataUtil.DbValueToDefault<String>(oIDataReader[icPersNatNombres]);
                        oPersonaNat.cPersNatApellido = DataUtil.DbValueToDefault<String>(oIDataReader[icPersNatApellido]);
                        oPersonaNat.cPersNatDOI = DataUtil.DbValueToDefault<String>(oIDataReader[icPersNatDOI]);
                        oPersonaNat.dPersNatNac = DataUtil.DbValueToDefault<DateTime>(oIDataReader[idPersNatNac]);
                        oPersonaNat.oPersNatSexo.cConstanteID = DataUtil.DbValueToDefault<String>(oIDataReader[icPersNatSexo]);
                    }
                }

                return oPersonaNat;

            }
            catch (Exception)
            {
                
                throw;
            }
        }

    }
}
