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
    public class PersonaAD
    {
        private Database oDatabase = EnterpriseLibraryContainer.Current.GetInstance<Database>(Conexion.cnsCHUYAChuya);

        public List<Array> getAllUsers()
        {
            List<Array> ListaUsuarios = new List<Array>();
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.stp_sel_RS_getAllUsers);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int icUser = oIDataReader.GetOrdinal("cUser");
                int icPersNombre = oIDataReader.GetOrdinal("cPersNombre");

                string cUser, cPersNombre = "";
                string[] usuario;

                while (oIDataReader.Read())
                {
                    cUser = DataUtil.DbValueToDefault<string>(oIDataReader[icUser]);
                    cPersNombre = DataUtil.DbValueToDefault<string>(oIDataReader[icPersNombre]);

                    usuario = new string[] { cUser, cPersNombre };

                    ListaUsuarios.Add(usuario);
                }
            }
            return ListaUsuarios;
        }


        public ListaPaginada ListaClientesPag(int nPage, int nSize)
        {
            ListaPaginada oLisCliPag = new ListaPaginada();
            //List<Persona> ListaClientes = new List<Persona>();

            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.stp_sel_ListarClientes);
            oDatabase.AddInParameter(oDbCommand, "@nPageN", DbType.Int32, nPage);
            oDatabase.AddInParameter(oDbCommand, "@nPageSize", DbType.Int32, nSize);
            oDatabase.AddOutParameter(oDbCommand, "@nRows", DbType.Int32, 10);
            oDatabase.AddOutParameter(oDbCommand, "@nPageTotal", DbType.Int32, 10);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int inPersId = oIDataReader.GetOrdinal("nPersId");
                int icPersNombre = oIDataReader.GetOrdinal("cNombre");
                int icSexo = oIDataReader.GetOrdinal("cSexo");
                int icDOI = oIDataReader.GetOrdinal("cDOI");
                int inPersTipo = oIDataReader.GetOrdinal("nPersTipo");
                int icPersTelefono1 = oIDataReader.GetOrdinal("cPersTelefono1");
                int icPersDireccion = oIDataReader.GetOrdinal("cPersDireccion");

                while (oIDataReader.Read())
                {
                    Persona oPersona = new Persona();
                    oPersona.nPersId = DataUtil.DbValueToDefault<Int32>(oIDataReader[inPersId]);
                    oPersona.cPersDesc = DataUtil.DbValueToDefault<String>(oIDataReader[icPersNombre]);
                    oPersona.cPersSexo = DataUtil.DbValueToDefault<String>(oIDataReader[icSexo]);
                    oPersona.cPersDOI = DataUtil.DbValueToDefault<String>(oIDataReader[icDOI]);
                    oPersona.cPersTipo = DataUtil.DbValueToDefault<String>(oIDataReader[inPersTipo]);
                    oPersona.cPersTelefono1 = DataUtil.DbValueToDefault<String>(oIDataReader[icPersTelefono1]);
                    oPersona.cPersDireccion = DataUtil.DbValueToDefault<String>(oIDataReader[icPersDireccion]);

                    //ListaClientes.Add(oPersona);
                    oLisCliPag.oLista.Add(oPersona);
                }
            }

            //oLisCliPag.oLista.Add(ListaClientes);
            oLisCliPag.nPage = nPage;
            oLisCliPag.nPageSize = nSize;
            oLisCliPag.nRows = Convert.ToInt32(oDatabase.GetParameterValue(oDbCommand, "@nRows"));
            oLisCliPag.nPageTotal = Convert.ToInt32(oDatabase.GetParameterValue(oDbCommand, "@nPageTotal"));

            return oLisCliPag;
        }


        public List<Persona> BuscarClientes(string cPersDOI, string cNombre)
        {
            List<Persona> ListaClientes = new List<Persona>();

            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.stp_sel_BuscarClientes);
            oDatabase.AddInParameter(oDbCommand, "@cPersDOI", DbType.String, (object)cPersDOI ?? DBNull.Value);
            oDatabase.AddInParameter(oDbCommand, "@cPersDesc", DbType.String, (object)cNombre ?? DBNull.Value);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int inPersId = oIDataReader.GetOrdinal("nPersId");
                int icPersNombre = oIDataReader.GetOrdinal("cNombre");
                int icSexo = oIDataReader.GetOrdinal("cSexo");
                int icDOI = oIDataReader.GetOrdinal("cDOI");
                int inPersTipo = oIDataReader.GetOrdinal("nPersTipo");
                int icPersTelefono1 = oIDataReader.GetOrdinal("cPersTelefono1");
                int icPersDireccion = oIDataReader.GetOrdinal("cPersDireccion");

                while (oIDataReader.Read())
                {
                    Persona oPersona = new Persona();

                    oPersona.nPersId = DataUtil.DbValueToDefault<Int32>(oIDataReader[inPersId]);
                    oPersona.cPersDesc = DataUtil.DbValueToDefault<String>(oIDataReader[icPersNombre]);
                    oPersona.cPersSexo = DataUtil.DbValueToDefault<String>(oIDataReader[icSexo]);
                    oPersona.cPersDOI = DataUtil.DbValueToDefault<String>(oIDataReader[icDOI]);
                    oPersona.cPersTipo = DataUtil.DbValueToDefault<String>(oIDataReader[inPersTipo]);
                    oPersona.cPersTelefono1 = DataUtil.DbValueToDefault<String>(oIDataReader[icPersTelefono1]);
                    oPersona.cPersDireccion = DataUtil.DbValueToDefault<String>(oIDataReader[icPersDireccion]);

                    ListaClientes.Add(oPersona);
                }
            }
            return ListaClientes;
        }

        public List<Persona> BuscarProveedores(string cPersDOI, string cNombre)
        {
            List<Persona> ListaProveedores = new List<Persona>();

            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.stp_sel_BuscarProveedores);
            oDatabase.AddInParameter(oDbCommand, "@cPersRUC", DbType.String, (object)cPersDOI ?? DBNull.Value);
            oDatabase.AddInParameter(oDbCommand, "@cPersDesc", DbType.String, (object)cNombre ?? DBNull.Value);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int inPersId = oIDataReader.GetOrdinal("nPersId");
                int icPersNombre = oIDataReader.GetOrdinal("cNombre");
                int icRUC = oIDataReader.GetOrdinal("cRUC");
                int icPersTelefono1 = oIDataReader.GetOrdinal("cPersTelefono1");
                int icPersDireccion = oIDataReader.GetOrdinal("cPersDireccion");

                while (oIDataReader.Read())
                {
                    Persona oPersona = new Persona();

                    oPersona.nPersId = DataUtil.DbValueToDefault<Int32>(oIDataReader[inPersId]);
                    oPersona.cPersDesc = DataUtil.DbValueToDefault<String>(oIDataReader[icPersNombre]);
                    oPersona.cPersDOI = DataUtil.DbValueToDefault<String>(oIDataReader[icRUC]);
                    oPersona.cPersTelefono1 = DataUtil.DbValueToDefault<String>(oIDataReader[icPersTelefono1]);
                    oPersona.cPersDireccion = DataUtil.DbValueToDefault<String>(oIDataReader[icPersDireccion]);

                    ListaProveedores.Add(oPersona);
                }
            }
            return ListaProveedores;
        }


        



    }
}
