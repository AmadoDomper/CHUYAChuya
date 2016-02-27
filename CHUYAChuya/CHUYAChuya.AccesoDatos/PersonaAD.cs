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

    }
}
