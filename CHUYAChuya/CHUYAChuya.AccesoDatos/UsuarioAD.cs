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
    public class UsuarioAD
    {
        private Database oDatabase = EnterpriseLibraryContainer.Current.GetInstance<Database>(Conexion.cnsCHUYAChuya);

        public ListaPaginada ListaUsuariosPag(int nPage = 1, int nSize = 10, int nUsuId = -1, string cUsuDesc = null, string cUsuName = null, string cUsuDOI = null)
        {
            ListaPaginada ListaUsuPag= new ListaPaginada();

            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.stp_sel_ListarUsuarios);
            oDatabase.AddInParameter(oDbCommand, "@nUsuId", DbType.Int32, (object)nUsuId ?? DBNull.Value);
            oDatabase.AddInParameter(oDbCommand, "@cUsuDesc", DbType.String, (object)cUsuDesc ?? DBNull.Value);
            oDatabase.AddInParameter(oDbCommand, "@cUsuName", DbType.String, (object)cUsuName ?? DBNull.Value);
            oDatabase.AddInParameter(oDbCommand, "@cUsuDOI", DbType.String, (object)cUsuDOI ?? DBNull.Value);
            oDatabase.AddInParameter(oDbCommand, "@nPageN", DbType.Int32, nPage);
            oDatabase.AddInParameter(oDbCommand, "@nPageSize", DbType.Int32, nSize);
            oDatabase.AddOutParameter(oDbCommand, "@nRows", DbType.Int32, 10);
            oDatabase.AddOutParameter(oDbCommand, "@nPageTotal", DbType.Int32, 10);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int inPersId = oIDataReader.GetOrdinal("nPersId");
                int inUsuId = oIDataReader.GetOrdinal("nUsuId");
                int icUsuNombre = oIDataReader.GetOrdinal("cUsuNombre");
                int icNombre = oIDataReader.GetOrdinal("cNombre");
                int icSexo = oIDataReader.GetOrdinal("cSexo");
                int icDOI = oIDataReader.GetOrdinal("cDOI");
                int icPersTelefono1 = oIDataReader.GetOrdinal("cPersTelefono1");
                int icPersDireccion = oIDataReader.GetOrdinal("cPersDireccion");

                while (oIDataReader.Read())
                {
                    Usuario oUsuario = new Usuario();

                    oUsuario.oPersNat.oPers.nPersId = DataUtil.DbValueToDefault<Int32>(oIDataReader[inPersId]);
                    oUsuario.nUsuId = DataUtil.DbValueToDefault<Int32>(oIDataReader[inUsuId]);
                    oUsuario.cUsuNombre = DataUtil.DbValueToDefault<String>(oIDataReader[icUsuNombre]);
                    oUsuario.oPersNat.oPers.cPersDesc = DataUtil.DbValueToDefault<String>(oIDataReader[icNombre]);
                    oUsuario.oPersNat.oPers.cPersSexo = DataUtil.DbValueToDefault<String>(oIDataReader[icSexo]);
                    oUsuario.oPersNat.oPers.cPersDOI = DataUtil.DbValueToDefault<String>(oIDataReader[icDOI]);
                    oUsuario.oPersNat.oPers.cPersTelefono1 = DataUtil.DbValueToDefault<String>(oIDataReader[icPersTelefono1]);
                    oUsuario.oPersNat.oPers.cPersDireccion = DataUtil.DbValueToDefault<String>(oIDataReader[icPersDireccion]);

                    ListaUsuPag.oLista.Add(oUsuario);
                }
            }

            ListaUsuPag.nPage = nPage;
            ListaUsuPag.nPageSize = nSize;
            ListaUsuPag.nRows = Convert.ToInt32(oDatabase.GetParameterValue(oDbCommand, "@nRows"));
            ListaUsuPag.nPageTotal = Convert.ToInt32(oDatabase.GetParameterValue(oDbCommand, "@nPageTotal"));

            return ListaUsuPag;
        }

        public List<Constante> Usuarios()
        {
            List<Constante> ListaUsuarios = new List<Constante>();

            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.stp_sel_Usuarios);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int icValor = oIDataReader.GetOrdinal("cValor");
                int icDesc = oIDataReader.GetOrdinal("cDesc");

                while (oIDataReader.Read())
                {
                    Constante oConstante = new Constante();

                    oConstante.cConstanteID = DataUtil.DbValueToDefault<String>(oIDataReader[icValor]);
                    oConstante.cNombre = DataUtil.DbValueToDefault<String>(oIDataReader[icDesc]);

                    ListaUsuarios.Add(oConstante);
                }
            }
            return ListaUsuarios;
        }

        public Usuario CargarDatosUsuario(int nPersId, string cDNI)
        {
            try
            {
                Usuario oUsuario = new Usuario();

                DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.stp_sel_Usuario);
                oDatabase.AddInParameter(oDbCommand, "@nPersId", DbType.Int32, (object) nPersId ?? DBNull.Value);
                oDatabase.AddInParameter(oDbCommand, "@cPersNatDOI", DbType.String, (object)cDNI ?? DBNull.Value);

                using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
                {
                    int inPersId = oIDataReader.GetOrdinal("nPersId");
                    int icPersTelefono1 = oIDataReader.GetOrdinal("cPersTelefono1");
                    int icPersTelefono2 = oIDataReader.GetOrdinal("cPersTelefono2");
                    int icPersEmail = oIDataReader.GetOrdinal("cPersEmail");
                    int icPersDireccion = oIDataReader.GetOrdinal("cPersDireccion");
                    int icPersUbigeo = oIDataReader.GetOrdinal("cPersUbigeo");

                    int icPersNatNombre = oIDataReader.GetOrdinal("cPersNatNombre");
                    int icPersNatApellido = oIDataReader.GetOrdinal("cPersNatApellido");
                    int icPersNatDOI = oIDataReader.GetOrdinal("cPersNatDOI");
                    int idPersNatNac = oIDataReader.GetOrdinal("dPersNatNac");
                    int icPersNatSexo = oIDataReader.GetOrdinal("cPersNatSexo");
                    int icUsuNombre = oIDataReader.GetOrdinal("cUsuNombre");
                    int icUsuContrasena = oIDataReader.GetOrdinal("cUsuContrasena");
                    int inUsuRolId = oIDataReader.GetOrdinal("nUsuRolId");
                    int icUsuRolDesc = oIDataReader.GetOrdinal("cUsuRolDesc");

                    while (oIDataReader.Read())
                    {
                        oUsuario.oPersNat.oPers.nPersId = DataUtil.DbValueToDefault<Int32>(oIDataReader[inPersId]);
                        oUsuario.oPersNat.oPers.cPersTelefono1 = DataUtil.DbValueToDefault<String>(oIDataReader[icPersTelefono1]);
                        oUsuario.oPersNat.oPers.cPersTelefono2 = DataUtil.DbValueToDefault<String>(oIDataReader[icPersTelefono2]);
                        oUsuario.oPersNat.oPers.cPersEmail = DataUtil.DbValueToDefault<String>(oIDataReader[icPersEmail]);
                        oUsuario.oPersNat.oPers.cPersDireccion = DataUtil.DbValueToDefault<String>(oIDataReader[icPersDireccion]);
                        oUsuario.oPersNat.oPers.oPersUbigeo.cConstanteID = DataUtil.DbValueToDefault<String>(oIDataReader[icPersUbigeo]);

                        oUsuario.oPersNat.cPersNatNombre = DataUtil.DbValueToDefault<String>(oIDataReader[icPersNatNombre]);
                        oUsuario.oPersNat.cPersNatApellido = DataUtil.DbValueToDefault<String>(oIDataReader[icPersNatApellido]);
                        oUsuario.oPersNat.cPersNatDOI = DataUtil.DbValueToDefault<String>(oIDataReader[icPersNatDOI]);
                        oUsuario.oPersNat.dPersNatNac = DataUtil.DbValueToDefault<DateTime>(oIDataReader[idPersNatNac]);
                        oUsuario.oPersNat.oPersNatSexo.cConstanteID = DataUtil.DbValueToDefault<String>(oIDataReader[icPersNatSexo]);
                        oUsuario.cUsuNombre = DataUtil.DbValueToDefault<String>(oIDataReader[icUsuNombre]);
                        oUsuario.cUsuContrasena = DataUtil.DbValueToDefault<String>(oIDataReader[icUsuContrasena]);
                        oUsuario.nRolId = DataUtil.DbValueToDefault<Int32>(oIDataReader[inUsuRolId]);
                        oUsuario.cRolDesc = DataUtil.DbValueToDefault<String>(oIDataReader[icUsuRolDesc]);
                    }
                }

                return oUsuario;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public int RegistrarUsuario(Usuario oUsuario)
        {
            int resultado = 0;

            try
            {
                using (SqlConnection oSqlConnection = new SqlConnection(Conexion.cnsCHUYAChuyaSQL))
                {
                    SqlCommand oSqlCommand = new SqlCommand();
                    oSqlCommand.CommandText = Procedimiento.stp_ins_upd_Usuario;
                    oSqlCommand.CommandType = CommandType.StoredProcedure;
                    oSqlCommand.Connection = oSqlConnection;

                    oSqlCommand.Parameters.Add("@nPersId", SqlDbType.Int).Value = (object)oUsuario.oPersNat.oPers.nPersId ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@nUsuId", SqlDbType.Int).Value = (object)oUsuario.nUsuId ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@cUsuNombre", SqlDbType.VarChar, 4).Value = (object)oUsuario.cUsuNombre ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@cUsuContrasena", SqlDbType.VarChar, 15).Value = (object)oUsuario.cUsuContrasena ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@nRolId", SqlDbType.Int).Value = (object)oUsuario.nRolId ?? DBNull.Value;

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

        public int EliminarUsuario(int nPersId)
        {
            int resultado = 0;

            try
            {
                using (SqlConnection oSqlConnection = new SqlConnection(Conexion.cnsCHUYAChuyaSQL))
                {
                    SqlCommand oSqlCommand = new SqlCommand();
                    oSqlCommand.CommandText = Procedimiento.stp_del_Usuario;
                    oSqlCommand.CommandType = CommandType.StoredProcedure;
                    oSqlCommand.Connection = oSqlConnection;

                    oSqlCommand.Parameters.Add("@nPersId", SqlDbType.Int).Value = nPersId;

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
            }
            return resultado;
        }
    }
}
