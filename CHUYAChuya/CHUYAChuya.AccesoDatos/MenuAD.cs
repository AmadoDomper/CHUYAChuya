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

        public List<Menu> ObtenerMenuFull(int nRolId)
        {
            List<Menu> ListaMenus = new List<Menu>();
            Menu oMenu = null;

            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.stp_sel_SeleccionarMenuFull);
            oDatabase.AddInParameter(oDbCommand, "@nRolId", DbType.Int32, nRolId);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int inMenuId = oIDataReader.GetOrdinal("nMenuId");
                int inMenuPadre = oIDataReader.GetOrdinal("nMenuPadre");
                int icMenuDesc = oIDataReader.GetOrdinal("cMenuDesc");
                int icMenuIcono = oIDataReader.GetOrdinal("cMenuIcono");
                int inMenuPosicion = oIDataReader.GetOrdinal("nMenuPosicion");
                int icMenuUrl = oIDataReader.GetOrdinal("cMenuUrl");
                int icMenuEstado = oIDataReader.GetOrdinal("cMenuEstado");

                while (oIDataReader.Read())
                {
                    oMenu = new Menu();
                    oMenu.nMenuId = DataUtil.DbValueToDefault<int>(oIDataReader[inMenuId]);
                    oMenu.nMenuPadre = DataUtil.DbValueToDefault<int>(oIDataReader[inMenuPadre]);
                    oMenu.cMenuDesc = DataUtil.DbValueToDefault<String>(oIDataReader[icMenuDesc]);
                    oMenu.cMenuIcono = DataUtil.DbValueToDefault<String>(oIDataReader[icMenuIcono]);
                    oMenu.nMenuposicion = DataUtil.DbValueToDefault<int>(oIDataReader[inMenuPosicion]);
                    oMenu.cMenuUrl = DataUtil.DbValueToDefault<String>(oIDataReader[icMenuUrl]);
                    oMenu.cMenuEstado = DataUtil.DbValueToDefault<String>(oIDataReader[icMenuEstado]);

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
                    oMenu.nMenuId = DataUtil.DbValueToDefault<Int32>(oIDataReader[iid_menu]);
                    oMenu.nMenuPadre = DataUtil.DbValueToDefault<Int32>(oIDataReader[iid_padre]);
                    oMenu.cMenuDesc = DataUtil.DbValueToDefault<String>(oIDataReader[idescripcion]);
                    oMenu.cMenuIcono = "~/imagenes/" + DataUtil.DbValueToDefault<String>(oIDataReader[iicono]);
                    oMenu.nMenuposicion = DataUtil.DbValueToDefault<int>(oIDataReader[iposicion]);
                    oMenu.cMenuUrl = DataUtil.DbValueToDefault<String>(oIDataReader[iurl]);
                    oMenu.cMenuEstado = DataUtil.DbValueToDefault<String>(oIDataReader[iestado]);

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
                    oMenu.nMenuId = DataUtil.DbValueToDefault<Int32>(oIDataReader[iid_menu]);
                    oMenu.nMenuPadre = DataUtil.DbValueToDefault<Int32>(oIDataReader[iid_padre]);
                    oMenu.cMenuDesc = DataUtil.DbValueToDefault<String>(oIDataReader[idescripcion]);
                    oMenu.cMenuIcono = "~/imagenes/" + DataUtil.DbValueToDefault<String>(oIDataReader[iicono]);
                    oMenu.nMenuposicion = DataUtil.DbValueToDefault<int>(oIDataReader[iposicion]);
                    oMenu.cMenuUrl = DataUtil.DbValueToDefault<String>(oIDataReader[iurl]);
                    oMenu.cMenuEstado = DataUtil.DbValueToDefault<String>(oIDataReader[iestado]);

                    ListaMenus.Add(oMenu);
                }
            }
            return ListaMenus;
        }


    }
}
