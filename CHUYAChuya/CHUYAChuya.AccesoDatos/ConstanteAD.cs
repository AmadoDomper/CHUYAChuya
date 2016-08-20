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
    public class ConstanteAD
    {
        private Database oDatabase = EnterpriseLibraryContainer.Current.GetInstance<Database>(Conexion.cnsCHUYAChuya);

        public List<Constante> ListaConstante(int id)
        {
            List<Constante> ListaConstantes = new List<Constante>();
            Constante oConstante = new Constante();

            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.stp_sel_constante);
            oDatabase.AddInParameter(oDbCommand, "@nConsCod", DbType.Int32, id);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int inConsValor = oIDataReader.GetOrdinal("nConsValor");
                int icConsDescripcion = oIDataReader.GetOrdinal("cConsDescripcion");

                while (oIDataReader.Read())
                {
                    oConstante = new Constante();
                    oConstante.cConstanteID = oIDataReader[inConsValor].ToString();
                    oConstante.cNombre = DataUtil.DbValueToDefault<String>(oIDataReader[icConsDescripcion]).ToUpper();
                    ListaConstantes.Add(oConstante);
                }
            }
            return ListaConstantes;
        }

        public List<Constante> ListaDepartamento()
        {
            List<Constante> ListaConstantes = new List<Constante>();
            Constante oConstante = new Constante();

            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.stp_sel_Departamento);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int icDepId = oIDataReader.GetOrdinal("cDepId");
                int icDepDesc = oIDataReader.GetOrdinal("cDepDesc");

                while (oIDataReader.Read())
                {
                    oConstante = new Constante();
                    oConstante.cConstanteID = DataUtil.DbValueToDefault<String>(oIDataReader[icDepId]);
                    oConstante.cNombre = DataUtil.DbValueToDefault<String>(oIDataReader[icDepDesc]);
                    ListaConstantes.Add(oConstante);    
                }
            }
            return ListaConstantes;
        }

        public List<Constante> ListaProvincia(string cDepId)
        {
            List<Constante> ListaConstantes = new List<Constante>();
            Constante oConstante = new Constante();

            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.stp_sel_Provincia);
            oDatabase.AddInParameter(oDbCommand, "@cDepId", DbType.String, cDepId);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int icProvId = oIDataReader.GetOrdinal("cProvId");
                int icProvDesc = oIDataReader.GetOrdinal("cProvDesc");

                while (oIDataReader.Read())
                {
                    oConstante = new Constante();
                    oConstante.cConstanteID = DataUtil.DbValueToDefault<String>(oIDataReader[icProvId]);
                    oConstante.cNombre = DataUtil.DbValueToDefault<String>(oIDataReader[icProvDesc]);
                    ListaConstantes.Add(oConstante);
                }
            }
            return ListaConstantes;
        }

        public List<Constante> ListaDistrito(string cProvId)
        {
            List<Constante> ListaConstantes = new List<Constante>();
            Constante oConstante = new Constante();

            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.stp_sel_Distrito);
            oDatabase.AddInParameter(oDbCommand, "@cProvId", DbType.String, cProvId);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int icDistId = oIDataReader.GetOrdinal("cDistId");
                int icDistDesc = oIDataReader.GetOrdinal("cDistDesc");

                while (oIDataReader.Read())
                {
                    oConstante = new Constante();
                    oConstante.cConstanteID = DataUtil.DbValueToDefault<String>(oIDataReader[icDistId]);
                    oConstante.cNombre = DataUtil.DbValueToDefault<String>(oIDataReader[icDistDesc]);
                    ListaConstantes.Add(oConstante);
                }
            }
            return ListaConstantes;
        }

    }
}
