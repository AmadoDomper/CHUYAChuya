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
    public class MenuAD
    {
        private Database oDatabase = EnterpriseLibraryContainer.Current.GetInstance<Database>(Conexion.cnsCHUYAChuya);

        public List<Menu> ObtenerMenuFull(string NombreUsuario, string Grupos)
        {
            List<Menu> ListaMenus = new List<Menu>();
            Menu oMenu;

            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.stp_sel_RS_SeleccionarMenuFull_Web);
            oDatabase.AddInParameter(oDbCommand, "@nomusu", DbType.String, NombreUsuario);
            oDatabase.AddInParameter(oDbCommand, "@grupos", DbType.String, Grupos);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int iid_menu = oIDataReader.GetOrdinal("id_menu");
                int iid_padre = oIDataReader.GetOrdinal("id_padre");
                int idescripcion = oIDataReader.GetOrdinal("descripcion");
                int iicono = oIDataReader.GetOrdinal("icono");
                int iposicion = oIDataReader.GetOrdinal("posicion");
                int iurl = oIDataReader.GetOrdinal("url");
                int iestado = oIDataReader.GetOrdinal("estado");

                while (oIDataReader.Read())
                {
                    oMenu = new Menu();
                    oMenu.id_menu = DataUtil.DbValueToDefault<int>(oIDataReader[iid_menu]);
                    oMenu.id_padre = DataUtil.DbValueToDefault<int>(oIDataReader[iid_padre]);
                    oMenu.descripcion = DataUtil.DbValueToDefault<String>(oIDataReader[idescripcion]);
                    oMenu.icono = DataUtil.DbValueToDefault<String>(oIDataReader[iicono]);
                    oMenu.posicion = DataUtil.DbValueToDefault<int>(oIDataReader[iposicion]);
                    oMenu.url = DataUtil.DbValueToDefault<String>(oIDataReader[iurl]);
                    oMenu.estado = DataUtil.DbValueToDefault<String>(oIDataReader[iestado]);

                    ListaMenus.Add(oMenu);
                }
            }
            return ListaMenus;
        }


        public List<Menu> ObtenerMenuOperacion(string NombreUsuario, string Grupos, string MenuPadreId)
        {
            List<Menu> ListaMenus = new List<Menu>();
            Menu oMenu;

            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.stp_sel_RS_SeleccionarOperacionMenu_Web);
            oDatabase.AddInParameter(oDbCommand, "@nomusu", DbType.String, NombreUsuario);
            oDatabase.AddInParameter(oDbCommand, "@grupos", DbType.String, Grupos);
            oDatabase.AddInParameter(oDbCommand, "@id_pmenu", DbType.String, MenuPadreId);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int iid_menu = oIDataReader.GetOrdinal("id_menu");
                int iid_padre = oIDataReader.GetOrdinal("id_padre");
                int idescripcion = oIDataReader.GetOrdinal("descripcion");
                int iicono = oIDataReader.GetOrdinal("icono");
                int iposicion = oIDataReader.GetOrdinal("posicion");
                int iurl = oIDataReader.GetOrdinal("url");
                int iestado = oIDataReader.GetOrdinal("estado");

                while (oIDataReader.Read())
                {
                    oMenu = new Menu();
                    oMenu.id_menu = DataUtil.DbValueToDefault<Int32>(oIDataReader[iid_menu]);
                    oMenu.id_padre = DataUtil.DbValueToDefault<Int32>(oIDataReader[iid_padre]);
                    oMenu.descripcion = DataUtil.DbValueToDefault<String>(oIDataReader[idescripcion]);
                    oMenu.icono = "~/imagenes/" + DataUtil.DbValueToDefault<String>(oIDataReader[iicono]);
                    oMenu.posicion = DataUtil.DbValueToDefault<int>(oIDataReader[iposicion]);
                    oMenu.url = DataUtil.DbValueToDefault<String>(oIDataReader[iurl]);
                    oMenu.estado = DataUtil.DbValueToDefault<String>(oIDataReader[iestado]);

                    ListaMenus.Add(oMenu);
                }
            }
            return ListaMenus;
        }


        public List<Menu> ObtenerOperaciones(string Tipo)
        {
            List<Menu> ListaMenus = new List<Menu>();
            Menu oMenu;

            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.stp_sel_RS_ListarOperaciones_Web);
            oDatabase.AddInParameter(oDbCommand, "@tipo", DbType.String, Tipo);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int iid_menu = oIDataReader.GetOrdinal("id_menu");
                int iid_padre = oIDataReader.GetOrdinal("id_padre");
                int idescripcion = oIDataReader.GetOrdinal("descripcion");
                int iicono = oIDataReader.GetOrdinal("icono");
                int iposicion = oIDataReader.GetOrdinal("posicion");
                int iurl = oIDataReader.GetOrdinal("url");
                int iestado = oIDataReader.GetOrdinal("estado");

                while (oIDataReader.Read())
                {
                    oMenu = new Menu();
                    oMenu.id_menu = DataUtil.DbValueToDefault<Int32>(oIDataReader[iid_menu]);
                    oMenu.id_padre = DataUtil.DbValueToDefault<Int32>(oIDataReader[iid_padre]);
                    oMenu.descripcion = DataUtil.DbValueToDefault<String>(oIDataReader[idescripcion]);
                    oMenu.icono = "~/imagenes/" + DataUtil.DbValueToDefault<String>(oIDataReader[iicono]);
                    oMenu.posicion = DataUtil.DbValueToDefault<int>(oIDataReader[iposicion]);
                    oMenu.url = DataUtil.DbValueToDefault<String>(oIDataReader[iurl]);
                    oMenu.estado = DataUtil.DbValueToDefault<String>(oIDataReader[iestado]);

                    ListaMenus.Add(oMenu);
                }
            }
            return ListaMenus;
        }


    }
}
