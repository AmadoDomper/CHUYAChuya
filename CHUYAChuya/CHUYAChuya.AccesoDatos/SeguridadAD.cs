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
    public class SeguridadAD
    {

        private Database oDatabase = EnterpriseLibraryContainer.Current.GetInstance<Database>(Conexion.cnsCHUYAChuya);

           
        public Usuario ObtenerUsuarioContrasena(Usuario oUsuario)
        {

            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.stp_sel_ObtenerUsuarioContrasena, oUsuario.cUsuNombre);


            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int inUsuId = oIDataReader.GetOrdinal("nUsuId");
                int inUsuNombre = oIDataReader.GetOrdinal("cUsuNombre");
                int icUsuContrasena = oIDataReader.GetOrdinal("cUsuContrasena");
                int icPersDesc = oIDataReader.GetOrdinal("cPersDesc");
                int inRolId = oIDataReader.GetOrdinal("nRolId");
                int icRolDesc = oIDataReader.GetOrdinal("cRolDesc");

                while (oIDataReader.Read())
                {
                    oUsuario = new Usuario();
                    //oUsuario.oDatoPersona = new Persona();
                    oUsuario.nUsuId = DataUtil.DbValueToDefault<Int32>(oIDataReader[inUsuId]);
                    oUsuario.cUsuNombre = DataUtil.DbValueToDefault<String>(oIDataReader[inUsuNombre]);
                    oUsuario.cUsuContrasena = DataUtil.DbValueToDefault<String>(oIDataReader[icUsuContrasena]);
                    oUsuario.oPersNat.oPers.cPersDesc = DataUtil.DbValueToDefault<String>(oIDataReader[icPersDesc]);
                    oUsuario.nRolId = DataUtil.DbValueToDefault<Int32>(oIDataReader[inRolId]);
                    oUsuario.cRolDesc = DataUtil.DbValueToDefault<String>(oIDataReader[icRolDesc]);
                }
            }

            return oUsuario;
        }
    }
}
