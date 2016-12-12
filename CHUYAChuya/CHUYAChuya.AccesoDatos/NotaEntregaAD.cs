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
    public class NotaEntregaAD
    {
        private Database oDatabase = EnterpriseLibraryContainer.Current.GetInstance<Database>(Conexion.cnsCHUYAChuya);

        public int RegistrarNotaEntrega(NotaEntrega oNotEnt)
        {
            int nNoTaEntId = -1;

            try
            {
                using (SqlConnection oSqlConnection = new SqlConnection(Conexion.cnsCHUYAChuyaSQL))
                {
                    SqlCommand oSqlCommand = new SqlCommand();
                    oSqlCommand.CommandText = Procedimiento.stp_ins_NotaEntrega;
                    oSqlCommand.CommandType = CommandType.StoredProcedure;
                    oSqlCommand.Connection = oSqlConnection;

                    oSqlCommand.Parameters.Add("@nNotaEntId", SqlDbType.Int).Value = (object)oNotEnt.nNotaEntId ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@nPersId", SqlDbType.Int).Value = (object)oNotEnt.oPers.nPersId ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@cNotaDireccion", SqlDbType.VarChar, 150).Value = (object)oNotEnt.cNotaDireccion ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@cNotaComentario", SqlDbType.VarChar, 250).Value = (object)oNotEnt.cNotaComentario ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@dFechaReg", SqlDbType.DateTime).Value = (object)oNotEnt.dFechaReg ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@dFechaEntrega", SqlDbType.DateTime).Value = (object)oNotEnt.dFechaEntrega ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@nNotaSubTotal", SqlDbType.Money).Value = (object)oNotEnt.nNotaSubTotal ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@nNotaDescuento", SqlDbType.Money).Value = (object)oNotEnt.nNotaDescuento ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@nNotaAnticipo", SqlDbType.Money).Value = (object)oNotEnt.nNotaAnticipo ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@nNotaEfectivo", SqlDbType.Money).Value = (object)oNotEnt.nNotaEfectivo ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@nNotaCambio", SqlDbType.Money).Value = (object)oNotEnt.nNotaCambio ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@nNotaMontoTotal", SqlDbType.Money).Value = (object)oNotEnt.nNotaMontoTotal ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@nNotaEstado", SqlDbType.TinyInt).Value = (object)Convert.ToByte(oNotEnt.oNotaEstado.cConstanteID) ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@cNotaUsuReg", SqlDbType.VarChar, 4).Value = (object)oNotEnt.cNotaUsuReg ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@cNotaUsuAge", SqlDbType.VarChar, 2).Value = (object)oNotEnt.cNotaUsuAge ?? DBNull.Value;

                    oSqlCommand.Parameters.Add("@T_NotaEntregaProducto", SqlDbType.Structured).Value = NotaEntregaProductoCollection.TSqlDataRecord(oNotEnt.ListaNotaEntProd.ToList());
                    oSqlConnection.Open();

                    using (IDataReader oIDataReader = oSqlCommand.ExecuteReader())
                    {
                        int inNotaEntId = oIDataReader.GetOrdinal("nNotaEntId");

                        while (oIDataReader.Read())
                        {
                            nNoTaEntId = DataUtil.DbValueToDefault<int>(oIDataReader[inNotaEntId]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                nNoTaEntId = -1;
            }
            return nNoTaEntId;
        }

        public int RealizarCobroServicio(int nNotaEntId, int nPersId, int nTipoC, decimal nEfecCo, decimal nCambioCo, string cNotaUsuCo, string cNotaUsuAge)
        {
            int nTicketId = 0;

            try
            {
                using (SqlConnection oSqlConnection = new SqlConnection(Conexion.cnsCHUYAChuyaSQL))
                {
                    SqlCommand oSqlCommand = new SqlCommand();
                    oSqlCommand.CommandText = Procedimiento.stp_ins_RealizarCobroServicio;
                    oSqlCommand.CommandType = CommandType.StoredProcedure;
                    oSqlCommand.Connection = oSqlConnection;

                    oSqlCommand.Parameters.Add("@nNotaEntId", SqlDbType.Int).Value = (object)nNotaEntId ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@nPersId", SqlDbType.Int).Value = (object)nPersId ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@nTipoC", SqlDbType.Int).Value = (object)nTipoC ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@nEfectivoCobro", SqlDbType.Money).Value = (object)nEfecCo ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@nCambioCobro", SqlDbType.Money).Value = (object)nCambioCo ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@cNotaUsuCobro", SqlDbType.VarChar, 4).Value = (object)cNotaUsuCo ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@cNotaUsuAge", SqlDbType.VarChar, 2).Value = (object)cNotaUsuAge ?? DBNull.Value;
                    oSqlConnection.Open();

                    using (IDataReader oIDataReader = oSqlCommand.ExecuteReader())
                    {
                        int inTicketId = oIDataReader.GetOrdinal("nTicketId");

                        while (oIDataReader.Read())
                        {
                            nTicketId = DataUtil.DbValueToDefault<int>(oIDataReader[inTicketId]);
                        }
                    }

                    //if (resultado[1].Length != 18 || resultado[1].IndexOf("109", 0, 3) == -1)
                    //{
                    //    /*Error de BD*/
                    //    resultado[0] = "2";
                    //    resultado[1] = "Ha ocurrido un error: " + "TIPO 2-" + resultado[1];
                    //}
                    //else
                    //{
                    //    /*Resultado OK*/
                    //    if (oCred.oColocaciones.oProducto.cCtaCod != null)
                    //    {
                    //        resultado[0] = "1";
                    //        resultado[1] = "Se actualizó correctamente los datos";
                    //    }
                    //    else
                    //    {
                    //        resultado[0] = "0";
                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {
                nTicketId = -1;
                //oError.cErrDescription = ex.Message.ToString();
                //oError.cErrSource = ex.StackTrace.ToString();
                //oError.cProceso = ex.TargetSite.ToString();

                //resultado[0] = "3";
                //resultado[1] = "Ha ocurrido un error: " + "TIPO 3-" + oErrorAD.InsertaErrorAplicacion(oError);
            }
            return nTicketId;
        }

        public int RealizarConfirmacionEntrega(int nNotaEntId, string cUsuario, string cUsuarioAge)
        {
            int nMovNro = 0;

            try
            {
                using (SqlConnection oSqlConnection = new SqlConnection(Conexion.cnsCHUYAChuyaSQL))
                {
                    SqlCommand oSqlCommand = new SqlCommand();
                    oSqlCommand.CommandText = Procedimiento.stp_ins_ConfirmarEntrega;
                    oSqlCommand.CommandType = CommandType.StoredProcedure;
                    oSqlCommand.Connection = oSqlConnection;

                    oSqlCommand.Parameters.Add("@nNotaEntId", SqlDbType.Int).Value = (object)nNotaEntId ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@cUsuario", SqlDbType.VarChar, 4).Value = (object)cUsuario ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@cUsuarioAge", SqlDbType.VarChar, 2).Value = (object)cUsuarioAge ?? DBNull.Value;
                    oSqlConnection.Open();

                    using (IDataReader oIDataReader = oSqlCommand.ExecuteReader())
                    {
                        int inMovNro = oIDataReader.GetOrdinal("nMovNro");

                        while (oIDataReader.Read())
                        {
                            nMovNro = DataUtil.DbValueToDefault<int>(oIDataReader[inMovNro]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                nMovNro = -1;
            }
            return nMovNro;
        }

        public int RealizarAnularComprobante(int nNotaEntId, string cUsuario, string cUsuarioAge)
        {
            int nMovNro = -2;

            try
            {
                using (SqlConnection oSqlConnection = new SqlConnection(Conexion.cnsCHUYAChuyaSQL))
                {
                    SqlCommand oSqlCommand = new SqlCommand();
                    oSqlCommand.CommandText = Procedimiento.stp_ins_RealizarAnularComprobante;
                    oSqlCommand.CommandType = CommandType.StoredProcedure;
                    oSqlCommand.Connection = oSqlConnection;

                    oSqlCommand.Parameters.Add("@nNotaEntId", SqlDbType.Int).Value = (object)nNotaEntId ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@cUsuario", SqlDbType.VarChar, 4).Value = (object)cUsuario ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@cUsuarioAge", SqlDbType.VarChar, 2).Value = (object)cUsuarioAge ?? DBNull.Value;
                    oSqlConnection.Open();

                    using (IDataReader oIDataReader = oSqlCommand.ExecuteReader())
                    {
                        int inMovNro = oIDataReader.GetOrdinal("nMovNro");

                        while (oIDataReader.Read())
                        {
                            nMovNro = DataUtil.DbValueToDefault<int>(oIDataReader[inMovNro]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                nMovNro = -2;
            }
            return nMovNro;
        }

        public int RealizarAnularNota(int nNotaEntId, string cUsuario, string cUsuarioAge)
        {
            int nMovNro = -2;

            try
            {
                using (SqlConnection oSqlConnection = new SqlConnection(Conexion.cnsCHUYAChuyaSQL))
                {
                    SqlCommand oSqlCommand = new SqlCommand();
                    oSqlCommand.CommandText = Procedimiento.stp_ins_RealizarAnularNota;
                    oSqlCommand.CommandType = CommandType.StoredProcedure;
                    oSqlCommand.Connection = oSqlConnection;

                    oSqlCommand.Parameters.Add("@nNotaEntId", SqlDbType.Int).Value = (object)nNotaEntId ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@cUsuario", SqlDbType.VarChar, 4).Value = (object)cUsuario ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@cUsuarioAge", SqlDbType.VarChar, 2).Value = (object)cUsuarioAge ?? DBNull.Value;
                    oSqlConnection.Open();

                    using (IDataReader oIDataReader = oSqlCommand.ExecuteReader())
                    {
                        int inMovNro = oIDataReader.GetOrdinal("nMovNro");

                        while (oIDataReader.Read())
                        {
                            nMovNro = DataUtil.DbValueToDefault<int>(oIDataReader[inMovNro]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                nMovNro = -2;
            }
            return nMovNro;
        }

        public ListaPaginada BuscarNotaEntPag(int nNotaEst, int nPage = 1, int nSize= 10, int nNotaEntId = -1, string cPersDOI = null, string cPersDesc = null, DateTime? dIni = null, DateTime? dFin = null)
        {
            ListaPaginada oLisNotas = new ListaPaginada();

            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.stp_sel_BuscarNotaEntregas);
            oDatabase.AddInParameter(oDbCommand, "@nNotaEstado", DbType.Int16, (object)nNotaEst ?? DBNull.Value);
            oDatabase.AddInParameter(oDbCommand, "@nNotaEntId", DbType.Int32, (object)nNotaEntId ?? DBNull.Value);
            oDatabase.AddInParameter(oDbCommand, "@cPersDOI", DbType.String, (object)cPersDOI ?? DBNull.Value);
            oDatabase.AddInParameter(oDbCommand, "@cPersDesc", DbType.String, (object)cPersDesc ?? DBNull.Value);
            oDatabase.AddInParameter(oDbCommand, "@dFechaEntI", DbType.Date, (object)dIni ?? DBNull.Value);
            oDatabase.AddInParameter(oDbCommand, "@dFechaEntF", DbType.Date, (object)dFin ?? DBNull.Value);
            oDatabase.AddInParameter(oDbCommand, "@nPageN", DbType.Int32, nPage);
            oDatabase.AddInParameter(oDbCommand, "@nPageSize", DbType.Int32, nSize);
            oDatabase.AddOutParameter(oDbCommand, "@nRows", DbType.Int32, 10);
            oDatabase.AddOutParameter(oDbCommand, "@nPageTotal", DbType.Int32, 10);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int inNotaEntId = oIDataReader.GetOrdinal("nNotaEntId");
                int icPersDesc = oIDataReader.GetOrdinal("cPersDesc");
                int icDOI = oIDataReader.GetOrdinal("cDOI");
                int idFechaReg = oIDataReader.GetOrdinal("dFechaReg");
                int idFechaEntrega = oIDataReader.GetOrdinal("dFechaEntrega");
                int inNotaMontoTotal = oIDataReader.GetOrdinal("nNotaMontoTotal");
                int inNotaEstado = oIDataReader.GetOrdinal("nNotaEstado");

                while (oIDataReader.Read())
                {
                    NotaEntrega oNotaEntrega = new NotaEntrega();

                    oNotaEntrega.nNotaEntId = DataUtil.DbValueToDefault<Int32>(oIDataReader[inNotaEntId]);
                    oNotaEntrega.oPers.cPersDesc = DataUtil.DbValueToDefault<String>(oIDataReader[icPersDesc]);
                    oNotaEntrega.oPers.cPersDOI = DataUtil.DbValueToDefault<String>(oIDataReader[icDOI]);
                    oNotaEntrega.dFechaReg = DataUtil.DbValueToDefault<DateTime>(oIDataReader[idFechaReg]);
                    oNotaEntrega.dFechaEntrega = DataUtil.DbValueToDefault<DateTime>(oIDataReader[idFechaEntrega]);
                    oNotaEntrega.nNotaMontoTotal = DataUtil.DbValueToDefault<decimal>(oIDataReader[inNotaMontoTotal]);
                    oNotaEntrega.oNotaEstado.cConstanteID = DataUtil.DbValueToDefault<String>(oIDataReader[inNotaEstado].ToString());

                    oLisNotas.oLista.Add(oNotaEntrega);
                }
            }

            oLisNotas.nPage = nPage;
            oLisNotas.nPageSize = nSize;
            oLisNotas.nRows = Convert.ToInt32(oDatabase.GetParameterValue(oDbCommand, "@nRows"));
            oLisNotas.nPageTotal = Convert.ToInt32(oDatabase.GetParameterValue(oDbCommand, "@nPageTotal"));

            return oLisNotas;
        }

        public NotaEntrega CargoDatosNotaEntrega(int nNotaId)
        {
            try
            {
                NotaEntrega oNotaEntrega = new NotaEntrega();

                DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.stp_sel_CargarNotaEntrega);
                oDatabase.AddInParameter(oDbCommand, "@nNotaId", DbType.Int32, (object)nNotaId ?? DBNull.Value);

                using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
                {
                    int inPersId = oIDataReader.GetOrdinal("nPersId");
                    int icPersDesc = oIDataReader.GetOrdinal("cPersDesc");
                    int icDOI = oIDataReader.GetOrdinal("cDOI");
                    int icPersTelefono1 = oIDataReader.GetOrdinal("cPersTelefono1");
                    int inNotaEntId = oIDataReader.GetOrdinal("nNotaEntId");
                    int icNotaComentario = oIDataReader.GetOrdinal("cNotaComentario");
                    int idFechaReg = oIDataReader.GetOrdinal("dFechaReg");
                    int idFechaEntrega = oIDataReader.GetOrdinal("dFechaEntrega");
                    int inNotaSubTotal = oIDataReader.GetOrdinal("nNotaSubTotal");

                    int inNotaAnticipo = oIDataReader.GetOrdinal("nNotaAnticipo");
                    int inNotaDescuento = oIDataReader.GetOrdinal("nNotaDescuento");
                    int inNotaEfectivo = oIDataReader.GetOrdinal("nNotaEfectivo");
                    int inNotaCambio = oIDataReader.GetOrdinal("nNotaCambio");


                    int inNotaMontoTotal = oIDataReader.GetOrdinal("nNotaMontoTotal");
                    int inNotaEstado = oIDataReader.GetOrdinal("nNotaEstado");

                    while (oIDataReader.Read())
                    {
                        oNotaEntrega.oPers.nPersId = DataUtil.DbValueToDefault<Int32>(oIDataReader[inPersId]);
                        oNotaEntrega.oPers.cPersDesc = DataUtil.DbValueToDefault<String>(oIDataReader[icPersDesc]);
                        oNotaEntrega.oPers.cPersDOI = DataUtil.DbValueToDefault<String>(oIDataReader[icDOI]);
                        oNotaEntrega.oPers.cPersTelefono1 = DataUtil.DbValueToDefault<String>(oIDataReader[icPersTelefono1]);
                        oNotaEntrega.nNotaEntId = DataUtil.DbValueToDefault<Int32>(oIDataReader[inNotaEntId]);
                        oNotaEntrega.cNotaComentario = DataUtil.DbValueToDefault<String>(oIDataReader[icNotaComentario]);
                        oNotaEntrega.dFechaReg = DataUtil.DbValueToDefault<DateTime>(oIDataReader[idFechaReg]);
                        oNotaEntrega.dFechaEntrega = DataUtil.DbValueToDefault<DateTime>(oIDataReader[idFechaEntrega]);
                        oNotaEntrega.nNotaSubTotal = DataUtil.DbValueToDefault<decimal>(oIDataReader[inNotaSubTotal]);

                        oNotaEntrega.nNotaAnticipo = DataUtil.DbValueToDefault<decimal>(oIDataReader[inNotaAnticipo]);
                        oNotaEntrega.nNotaDescuento = DataUtil.DbValueToDefault<decimal>(oIDataReader[inNotaDescuento]);
                        oNotaEntrega.nNotaEfectivo = DataUtil.DbValueToDefault<decimal>(oIDataReader[inNotaEfectivo]);
                        oNotaEntrega.nNotaCambio = DataUtil.DbValueToDefault<decimal>(oIDataReader[inNotaCambio]);

                        oNotaEntrega.nNotaMontoTotal = DataUtil.DbValueToDefault<decimal>(oIDataReader[inNotaMontoTotal]);
                        oNotaEntrega.oNotaEstado.cConstanteID = DataUtil.DbValueToDefault<String>(oIDataReader[inNotaEstado].ToString());

                        oNotaEntrega.ListaNotaEntProd = ListaNotaEntProductos(nNotaId);
                    }
                }

                return oNotaEntrega;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<NotaEntProd> ListaNotaEntProductos(int nNotaId)
        {
            List<NotaEntProd> ListaNotaEntProd = new List<NotaEntProd>();

            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.stp_sel_ListaNotaEntProductos);
            oDatabase.AddInParameter(oDbCommand, "@nNotaId", DbType.String, nNotaId);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int inProdId = oIDataReader.GetOrdinal("nProdId");
                int icProdDesc = oIDataReader.GetOrdinal("cProdDesc");
                int ibProdSerLavado = oIDataReader.GetOrdinal("bProdSerLavado");
                int ibProdSerSecado = oIDataReader.GetOrdinal("bProdSerSecado");
                int ibProdSerPlanchado = oIDataReader.GetOrdinal("bProdSerPlanchado");
                int icConsDescripcion = oIDataReader.GetOrdinal("cConsDescripcion");
                int inDetCantidad = oIDataReader.GetOrdinal("nDetCantidad");
                int inDetProdPrecioUnit = oIDataReader.GetOrdinal("nDetProdPrecioUnit");
                int inDetImporte = oIDataReader.GetOrdinal("nDetImporte");

                while (oIDataReader.Read())
                {
                    NotaEntProd oNotaEntPro = new NotaEntProd();

                    oNotaEntPro.oProd.nProdId = DataUtil.DbValueToDefault<Int32>(oIDataReader[inProdId]);
                    oNotaEntPro.oProd.cProdDesc = DataUtil.DbValueToDefault<String>(oIDataReader[icProdDesc]);
                    oNotaEntPro.oProd.bProdSerLavado = DataUtil.DbValueToDefault<Boolean>(oIDataReader[ibProdSerLavado]);
                    oNotaEntPro.oProd.bProdSerSecado = DataUtil.DbValueToDefault<Boolean>(oIDataReader[ibProdSerSecado]);
                    oNotaEntPro.oProd.bProdSerPlanchado = DataUtil.DbValueToDefault<Boolean>(oIDataReader[ibProdSerPlanchado]);

                    oNotaEntPro.oProd.oProdMedida.cNombre = DataUtil.DbValueToDefault<string>(oIDataReader[icConsDescripcion]);
                    oNotaEntPro.nDetCantidad = DataUtil.DbValueToDefault<decimal>(oIDataReader[inDetCantidad]);
                    oNotaEntPro.nProdPrecioUnit = DataUtil.DbValueToDefault<decimal>(oIDataReader[inDetProdPrecioUnit]);
                    oNotaEntPro.nDetImporte = DataUtil.DbValueToDefault<decimal>(oIDataReader[inDetImporte]);

                    ListaNotaEntProd.Add(oNotaEntPro);
                }
            }
            return ListaNotaEntProd;
        }



        public Ticket ObtenerDatosNotaEntImp(int nNotaId)
        {
            try
            {
                Ticket oTicket = new Ticket();

                DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.stp_sel_ObtenerDatosNotaEntImp);
                oDatabase.AddInParameter(oDbCommand, "@nNotaId", DbType.Int32, (object)nNotaId ?? DBNull.Value);

                using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
                {
                    int inNotaEntId = oIDataReader.GetOrdinal("nNotaEntId");
                    int inMovNro = oIDataReader.GetOrdinal("nMovNro");
                    int idMovFecha = oIDataReader.GetOrdinal("dMovFecha");
                    int icMovUsuario = oIDataReader.GetOrdinal("cMovUsuario");
                    int inPersId = oIDataReader.GetOrdinal("nPersId");
                    int icPersTipo = oIDataReader.GetOrdinal("cPersTipo");
                    int icPersDesc = oIDataReader.GetOrdinal("cPersDesc");
                    int icPersDireccion = oIDataReader.GetOrdinal("cPersDireccion");
                    int icDOI = oIDataReader.GetOrdinal("cDOI");

                    int idFechaEntrega = oIDataReader.GetOrdinal("dFechaEntrega");

                    int inNotaSubTotal = oIDataReader.GetOrdinal("nNotaSubTotal");
                    int inNotaAnticipo = oIDataReader.GetOrdinal("nNotaAnticipo");
                    int inNotaDescuento = oIDataReader.GetOrdinal("nNotaDescuento");
                    int inNotaEfectivo = oIDataReader.GetOrdinal("nNotaEfectivo");
                    int inNotaCambio = oIDataReader.GetOrdinal("nNotaCambio");
                    int inNotaMontoTotal = oIDataReader.GetOrdinal("nNotaMontoTotal");
                    int inNotaEstado = oIDataReader.GetOrdinal("nNotaEstado");

                    while (oIDataReader.Read())
                    {
                        oTicket.oNotaEntrega.nNotaEntId = DataUtil.DbValueToDefault<Int32>(oIDataReader[inNotaEntId]);
                        oTicket.oMov.nMovNro = DataUtil.DbValueToDefault<Int32>(oIDataReader[inMovNro]);
                        oTicket.oMov.dMovFecha = DataUtil.DbValueToDefault<DateTime>(oIDataReader[idMovFecha]);
                        oTicket.oMov.cUsuario = DataUtil.DbValueToDefault<String>(oIDataReader[icMovUsuario]);

                        oTicket.oNotaEntrega.oPers.nPersId = DataUtil.DbValueToDefault<Int32>(oIDataReader[inPersId]);
                        oTicket.oNotaEntrega.oPers.cPersTipo = DataUtil.DbValueToDefault<String>(oIDataReader[icPersTipo]);
                        oTicket.oNotaEntrega.oPers.cPersDesc = DataUtil.DbValueToDefault<String>(oIDataReader[icPersDesc]);
                        oTicket.oNotaEntrega.oPers.cPersDireccion = DataUtil.DbValueToDefault<String>(oIDataReader[icPersDireccion]);
                        oTicket.oNotaEntrega.oPers.cPersDOI = DataUtil.DbValueToDefault<String>(oIDataReader[icDOI]);

                        oTicket.oNotaEntrega.dFechaEntrega = DataUtil.DbValueToDefault<DateTime>(oIDataReader[idFechaEntrega]);

                        oTicket.oNotaEntrega.nNotaSubTotal = DataUtil.DbValueToDefault<decimal>(oIDataReader[inNotaSubTotal]);
                        oTicket.oNotaEntrega.nNotaAnticipo = DataUtil.DbValueToDefault<decimal>(oIDataReader[inNotaAnticipo]);
                        oTicket.oNotaEntrega.nNotaDescuento = DataUtil.DbValueToDefault<decimal>(oIDataReader[inNotaDescuento]);
                        oTicket.oNotaEntrega.nNotaEfectivo = DataUtil.DbValueToDefault<decimal>(oIDataReader[inNotaEfectivo]);
                        oTicket.oNotaEntrega.nNotaCambio = DataUtil.DbValueToDefault<decimal>(oIDataReader[inNotaCambio]);

                        oTicket.oNotaEntrega.nNotaMontoTotal = DataUtil.DbValueToDefault<decimal>(oIDataReader[inNotaMontoTotal]);
                        oTicket.oNotaEntrega.oNotaEstado.cConstanteID = DataUtil.DbValueToDefault<String>(oIDataReader[inNotaEstado].ToString());

                        oTicket.oNotaEntrega.ListaNotaEntProd = ListaNotaEntProductos(oTicket.oNotaEntrega.nNotaEntId);
                    }
                }

                return oTicket;
            }
            catch (Exception)
            {

                throw;
            }
        }



        public Ticket ObtenerDatosTicket(int nTicketId)
        {
            try
            {
                Ticket oTicket = new Ticket();

                DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.stp_sel_ObtenerDatosTicket);
                oDatabase.AddInParameter(oDbCommand, "@nTicketId", DbType.Int32, (object)nTicketId ?? DBNull.Value);

                using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
                {
                    int inTicketSerie = oIDataReader.GetOrdinal("nTicketSerie");
                    int inTicketCorrelativo = oIDataReader.GetOrdinal("nTicketCorrelativo");
                    int inTicketTipo = oIDataReader.GetOrdinal("nTicketTipo");
                    int inMovNro = oIDataReader.GetOrdinal("nMovNro");
                    int idMovFecha = oIDataReader.GetOrdinal("dMovFecha");
                    int icMovUsuario = oIDataReader.GetOrdinal("cMovUsuario");
                    int inPersId = oIDataReader.GetOrdinal("nPersId");
                    int icPersTipo = oIDataReader.GetOrdinal("cPersTipo");
                    int icPersDesc = oIDataReader.GetOrdinal("cPersDesc");
                    int icPersDireccion = oIDataReader.GetOrdinal("cPersDireccion");
                    int icDOI = oIDataReader.GetOrdinal("cDOI");

                    int inNotaEntId = oIDataReader.GetOrdinal("nNotaEntId");
                    int idFechaEntrega = oIDataReader.GetOrdinal("dFechaEntrega");

                    int inNotaSubTotal = oIDataReader.GetOrdinal("nNotaSubTotal");
                    int inNotaAnticipo = oIDataReader.GetOrdinal("nNotaAnticipo");
                    int inNotaDescuento = oIDataReader.GetOrdinal("nNotaDescuento");
                    int inNotaEfectivo = oIDataReader.GetOrdinal("nNotaEfectivo");
                    int inNotaCambio = oIDataReader.GetOrdinal("nNotaCambio");
                    int inNotaMontoTotal = oIDataReader.GetOrdinal("nNotaMontoTotal");
                    int inNotaEstado = oIDataReader.GetOrdinal("nNotaEstado");
                    int icImpSerie = oIDataReader.GetOrdinal("cImpSerie");

                    while (oIDataReader.Read())
                    {
                        oTicket.nTicketSerie = DataUtil.DbValueToDefault<Int32>(oIDataReader[inTicketSerie]);
                        oTicket.nTicketCorrelativo = DataUtil.DbValueToDefault<Int32>(oIDataReader[inTicketCorrelativo]);
                        oTicket.oTicketTipo.cConstanteID = DataUtil.DbValueToDefault<String>(oIDataReader[inTicketTipo].ToString());
                        oTicket.oMov.nMovNro = DataUtil.DbValueToDefault<Int32>(oIDataReader[inMovNro]);
                        oTicket.oMov.dMovFecha = DataUtil.DbValueToDefault<DateTime>(oIDataReader[idMovFecha]);
                        oTicket.oMov.cUsuario = DataUtil.DbValueToDefault<String>(oIDataReader[icMovUsuario]);

                        oTicket.oNotaEntrega.oPers.nPersId = DataUtil.DbValueToDefault<Int32>(oIDataReader[inPersId]);
                        oTicket.oNotaEntrega.oPers.cPersTipo = DataUtil.DbValueToDefault<String>(oIDataReader[icPersTipo]);
                        oTicket.oNotaEntrega.oPers.cPersDesc = DataUtil.DbValueToDefault<String>(oIDataReader[icPersDesc]);
                        oTicket.oNotaEntrega.oPers.cPersDireccion = DataUtil.DbValueToDefault<String>(oIDataReader[icPersDireccion]);
                        oTicket.oNotaEntrega.oPers.cPersDOI = DataUtil.DbValueToDefault<String>(oIDataReader[icDOI]);

                        oTicket.oNotaEntrega.nNotaEntId = DataUtil.DbValueToDefault<Int32>(oIDataReader[inNotaEntId]);
                        oTicket.oNotaEntrega.dFechaEntrega = DataUtil.DbValueToDefault<DateTime>(oIDataReader[idFechaEntrega]);

                        oTicket.oNotaEntrega.nNotaSubTotal = DataUtil.DbValueToDefault<decimal>(oIDataReader[inNotaSubTotal]);
                        oTicket.oNotaEntrega.nNotaAnticipo = DataUtil.DbValueToDefault<decimal>(oIDataReader[inNotaAnticipo]);
                        oTicket.oNotaEntrega.nNotaDescuento = DataUtil.DbValueToDefault<decimal>(oIDataReader[inNotaDescuento]);
                        oTicket.oNotaEntrega.nNotaEfectivo = DataUtil.DbValueToDefault<decimal>(oIDataReader[inNotaEfectivo]);
                        oTicket.oNotaEntrega.nNotaCambio = DataUtil.DbValueToDefault<decimal>(oIDataReader[inNotaCambio]);

                        oTicket.oNotaEntrega.nNotaMontoTotal = DataUtil.DbValueToDefault<decimal>(oIDataReader[inNotaMontoTotal]);
                        oTicket.oNotaEntrega.oNotaEstado.cConstanteID = DataUtil.DbValueToDefault<String>(oIDataReader[inNotaEstado].ToString());
                        oTicket.oImp.nImpSerie = DataUtil.DbValueToDefault<String>(oIDataReader[icImpSerie]);

                        oTicket.oNotaEntrega.ListaNotaEntProd = ListaNotaEntProductos(oTicket.oNotaEntrega.nNotaEntId);
                    }
                }

                return oTicket;
            }
            catch (Exception)
            {

                throw;
            }
        }




    }
}
