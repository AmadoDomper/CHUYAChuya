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


        public List<Persona> ListaClientes()
        {
            List<Persona> ListaClientes = new List<Persona>();

            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.stp_sel_ListarClientes);

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

    }
}
