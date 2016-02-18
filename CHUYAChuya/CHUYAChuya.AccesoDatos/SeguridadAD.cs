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

        public Usuario ObtenerDatosUsuario(Usuario oUsuario)
        {

            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.stp_sel_GetDataUser, oUsuario.cNomUsuario);


            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int icPersNombre = oIDataReader.GetOrdinal("cPersNombre");
                string cUser = oUsuario.cNomUsuario.ToUpper();
                int icPersCod = oIDataReader.GetOrdinal("cPersCod");
                int icDNI = oIDataReader.GetOrdinal("DNI");
                int icRUC = oIDataReader.GetOrdinal("RUC");
                int icRHCargoDescripcion = oIDataReader.GetOrdinal("cRHCargoDescripcion");
                int icAgenciaActual = oIDataReader.GetOrdinal("cAgenciaActual");

                while (oIDataReader.Read())
                {
                    oUsuario = new Usuario();
                    oUsuario.oDatoPersona = new Persona();
                    oUsuario.cNomUsuario = cUser;
                    oUsuario.cCargo = DataUtil.DbValueToDefault<String>(oIDataReader[icRHCargoDescripcion]);
                    oUsuario.oDatoPersona.cPersNombre = DataUtil.DbValueToDefault<String>(oIDataReader[icPersNombre]);
                    oUsuario.oDatoPersona.cPersCod = DataUtil.DbValueToDefault<String>(oIDataReader[icPersCod]);
                    //oUsuario.oDatoPersona.cPersDNI = DataUtil.DbValueToDefault<String>(oIDataReader[icDNI]);ErrorCambio
                    //oUsuario.oDatoPersona.cPersRUC = DataUtil.DbValueToDefault<String>(oIDataReader[icRUC]);
                    oUsuario.cCodAge = DataUtil.DbValueToDefault<String>(oIDataReader[icAgenciaActual]);
                }
            }

            return oUsuario;
        }
    }
}
