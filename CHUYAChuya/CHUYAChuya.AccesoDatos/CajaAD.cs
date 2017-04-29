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
    public class CajaAD
    {
        private Database oDatabase = EnterpriseLibraryContainer.Current.GetInstance<Database>(Conexion.cnsCHUYAChuya);

        public int RegistrarSalidaEfePagoProv(int nPersId, decimal nMontoSalida, string cComprobante, byte nTipoComp, DateTime dFechaEmi, string cUsuario, string cAgencia)
        {
            int resultado = -2;

            try
            {
                using (SqlConnection oSqlConnection = new SqlConnection(Conexion.cnsCHUYAChuyaSQL))
                {
                    SqlCommand oSqlCommand = new SqlCommand();
                    oSqlCommand.CommandText = Procedimiento.stp_ins_SalidaEfectivoPagoProveedor;
                    oSqlCommand.CommandType = CommandType.StoredProcedure;
                    oSqlCommand.Connection = oSqlConnection;

                    oSqlCommand.Parameters.Add("@nPersId", SqlDbType.Int).Value = (object)nPersId ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@nMontoSalida", SqlDbType.Money).Value = (object)nMontoSalida ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@cComprobante", SqlDbType.VarChar, 11).Value = (object)cComprobante ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@nTipoComprobante", SqlDbType.TinyInt).Value = (object)nTipoComp ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@dFechaEmision", SqlDbType.DateTime).Value = (object)dFechaEmi ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@cUsuario", SqlDbType.VarChar,4).Value = (object)cUsuario ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@cAgencia", SqlDbType.VarChar,2).Value = (object)cAgencia ?? DBNull.Value;

                    oSqlConnection.Open();

                    using (IDataReader oIDataReader = oSqlCommand.ExecuteReader())
                    {
                        int iResultado = oIDataReader.GetOrdinal("Resultado");

                        while (oIDataReader.Read())
                        {
                            resultado = DataUtil.DbValueToDefault<int>(oIDataReader[iResultado]);
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
                resultado = -2;
                //oError.cErrDescription = ex.Message.ToString();
                //oError.cErrSource = ex.StackTrace.ToString();
                //oError.cProceso = ex.TargetSite.ToString();

                //resultado[0] = "3";
                //resultado[1] = "Ha ocurrido un error: " + "TIPO 3-" + oErrorAD.InsertaErrorAplicacion(oError);
            }
            return resultado;
        }


        public int RegistrarSalidaEfe(decimal nMontoSalida, string cMotivo, string cUsuario, string cAgencia)
        {
            int resultado = -2;

            try
            {
                using (SqlConnection oSqlConnection = new SqlConnection(Conexion.cnsCHUYAChuyaSQL))
                {
                    SqlCommand oSqlCommand = new SqlCommand();
                    oSqlCommand.CommandText = Procedimiento.stp_ins_SalidaEfectivoOtros;
                    oSqlCommand.CommandType = CommandType.StoredProcedure;
                    oSqlCommand.Connection = oSqlConnection;

                    oSqlCommand.Parameters.Add("@nMontoSalida", SqlDbType.Money).Value = (object)nMontoSalida ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@cMotOtro", SqlDbType.VarChar,150).Value = (object)cMotivo ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@cUsuario", SqlDbType.VarChar, 4).Value = (object)cUsuario ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@cAgencia", SqlDbType.VarChar,4).Value = (object)cAgencia ?? DBNull.Value;

                    oSqlConnection.Open();

                    using (IDataReader oIDataReader = oSqlCommand.ExecuteReader())
                    {
                        int iResultado = oIDataReader.GetOrdinal("Resultado");

                        while (oIDataReader.Read())
                        {
                            resultado = DataUtil.DbValueToDefault<int>(oIDataReader[iResultado]);
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
                resultado = -2;
                //oError.cErrDescription = ex.Message.ToString();
                //oError.cErrSource = ex.StackTrace.ToString();
                //oError.cProceso = ex.TargetSite.ToString();

                //resultado[0] = "3";
                //resultado[1] = "Ha ocurrido un error: " + "TIPO 3-" + oErrorAD.InsertaErrorAplicacion(oError);
            }
            return resultado;
        }



        public int RegistrarEntradaEfe(decimal nMontoEntrada, string cUsuario, string cAgencia)
        {
            int resultado = -2;

            try
            {
                using (SqlConnection oSqlConnection = new SqlConnection(Conexion.cnsCHUYAChuyaSQL))
                {
                    SqlCommand oSqlCommand = new SqlCommand();
                    oSqlCommand.CommandText = Procedimiento.stp_ins_IngresoEfectivo;
                    oSqlCommand.CommandType = CommandType.StoredProcedure;
                    oSqlCommand.Connection = oSqlConnection;

                    oSqlCommand.Parameters.Add("@nMontoIngreso", SqlDbType.Money).Value = (object)nMontoEntrada ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@cUsuario", SqlDbType.VarChar, 4).Value = (object)cUsuario ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@cAgencia", SqlDbType.VarChar,4).Value = (object)cAgencia ?? DBNull.Value;

                    oSqlConnection.Open();

                    using (IDataReader oIDataReader = oSqlCommand.ExecuteReader())
                    {
                        int iResultado = oIDataReader.GetOrdinal("Resultado");

                        while (oIDataReader.Read())
                        {
                            resultado = DataUtil.DbValueToDefault<int>(oIDataReader[iResultado]);
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
                resultado = -2;
                //oError.cErrDescription = ex.Message.ToString();
                //oError.cErrSource = ex.StackTrace.ToString();
                //oError.cProceso = ex.TargetSite.ToString();

                //resultado[0] = "3";
                //resultado[1] = "Ha ocurrido un error: " + "TIPO 3-" + oErrorAD.InsertaErrorAplicacion(oError);
            }
            return resultado;
        }

        public List<MovCaja> BuscarMovCaja(string cUsuario, string cMovDesc = null)
        {
            List<MovCaja> ListaMovCaja = new List<MovCaja>();

            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.stp_sel_BuscarMovCaja);
            oDatabase.AddInParameter(oDbCommand, "@cUsuario", DbType.String, (object)cUsuario ?? DBNull.Value);
            oDatabase.AddInParameter(oDbCommand, "@cMovDesc", DbType.String, (object)cMovDesc ?? DBNull.Value);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int idMovFecha = oIDataReader.GetOrdinal("dMovFecha");
                int icOpeDesc = oIDataReader.GetOrdinal("cOpeDesc");
                int icMonIngreso = oIDataReader.GetOrdinal("cMonIngreso");
                int icMonEgreso = oIDataReader.GetOrdinal("cMonEgreso");
                int icMonActual = oIDataReader.GetOrdinal("cMonActual");

                while (oIDataReader.Read())
                {
                    MovCaja MovCaja = new MovCaja();

                    MovCaja.dMovFecha = DataUtil.DbValueToDefault<String>(oIDataReader[idMovFecha]);
                    MovCaja.cOpeDesc = DataUtil.DbValueToDefault<String>(oIDataReader[icOpeDesc]);
                    MovCaja.cMonIngreso = DataUtil.DbValueToDefault<String>(oIDataReader[icMonIngreso]);
                    MovCaja.cMonEgreso = DataUtil.DbValueToDefault<String>(oIDataReader[icMonEgreso]);
                    MovCaja.cMonActual = DataUtil.DbValueToDefault<String>(oIDataReader[icMonActual]);

                    ListaMovCaja.Add(MovCaja);
                }
            }
            return ListaMovCaja;
        }

        //CORTE DE CAJA
        public Cierre CargaDetalleCierre(string cUsuario, DateTime dFecha, int nCierre)
        {
            Cierre oDetalleCierre = new Cierre();

            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.stp_sel_ListaDetalleCierre);
            oDatabase.AddInParameter(oDbCommand, "@cUsuario", DbType.String, cUsuario);
            oDatabase.AddInParameter(oDbCommand, "@dFecha", DbType.String, dFecha.ToString("yyyyMMdd"));
            oDatabase.AddInParameter(oDbCommand, "@nCierre", DbType.Int32, nCierre);


            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int inCajaInicio = oIDataReader.GetOrdinal("nCajaInicio");
                int inCajaEntradaEfectivo = oIDataReader.GetOrdinal("nCajaEntradaEfectivo");
                int inEntTotal = oIDataReader.GetOrdinal("nEntTotal");
                int inCajaNotaAnticipo = oIDataReader.GetOrdinal("nCajaNotaAnticipo");
                int inCajaNotaPagadas = oIDataReader.GetOrdinal("nCajaNotaPagadas");
                int inPagoProvl = oIDataReader.GetOrdinal("nPagoProv");
                int inSalidaOtro = oIDataReader.GetOrdinal("nSalidaOtro");
                int inAnula = oIDataReader.GetOrdinal("nAnula");
                int inCajaTotal = oIDataReader.GetOrdinal("nCajaTotal");

                while (oIDataReader.Read())
                {
                    oDetalleCierre.nCajaInicio = DataUtil.DbValueToDefault<decimal>(oIDataReader[inCajaInicio]);
                    oDetalleCierre.nCajaEntradaEfectivo = DataUtil.DbValueToDefault<decimal>(oIDataReader[inCajaEntradaEfectivo]);
                    oDetalleCierre.nEntTotal = DataUtil.DbValueToDefault<decimal>(oIDataReader[inEntTotal]);
                    oDetalleCierre.nCajaNotaAnticipo = DataUtil.DbValueToDefault<decimal>(oIDataReader[inCajaNotaAnticipo]);
                    oDetalleCierre.nCajaNotaPagadas = DataUtil.DbValueToDefault<decimal>(oIDataReader[inCajaNotaPagadas]);
                    oDetalleCierre.nPagoProv = DataUtil.DbValueToDefault<decimal>(oIDataReader[inPagoProvl]);
                    oDetalleCierre.nSalidaOtro = DataUtil.DbValueToDefault<decimal>(oIDataReader[inSalidaOtro]);
                    oDetalleCierre.nAnula = DataUtil.DbValueToDefault<decimal>(oIDataReader[inAnula]);
                    oDetalleCierre.nCajaTotal = DataUtil.DbValueToDefault<decimal>(oIDataReader[inCajaTotal]);
                }
                oDetalleCierre.oListaNotaPag = ListaNotasPagada(cUsuario, dFecha, nCierre);
                oDetalleCierre.oListaNotAnt = ListaNotasAnticipo(cUsuario, dFecha, nCierre);
                oDetalleCierre.oListaPagoProv = ListaPagoProveedores(cUsuario, dFecha, nCierre);
            }
            return oDetalleCierre;
        }


        public List<NotaEntregaL> ListaNotasPagada(string cUsuario, DateTime dFecha, int nCierre)
        {
            List<NotaEntregaL> ListaNotasPagada = new List<NotaEntregaL>();

            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.stp_sel_ListaNotasPagadas);
            oDatabase.AddInParameter(oDbCommand, "@cUsuario", DbType.String, cUsuario);
            oDatabase.AddInParameter(oDbCommand, "@dFecha", DbType.Date, dFecha);
            oDatabase.AddInParameter(oDbCommand, "@nCierre", DbType.Int32, nCierre);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int idFechaReg = oIDataReader.GetOrdinal("dFechaReg");
                int inNotaEntId = oIDataReader.GetOrdinal("nNotaEntId");
                int icPersDesc = oIDataReader.GetOrdinal("cPersDesc");
                int inNotaSubTotal = oIDataReader.GetOrdinal("nNotaSubTotal");
                int inNotaDescuento = oIDataReader.GetOrdinal("nNotaDescuento");
                int inNotaAnticipo = oIDataReader.GetOrdinal("nNotaAnticipo");
                int inNotaMontoTotal = oIDataReader.GetOrdinal("nNotaMontoTotal");

                while (oIDataReader.Read())
                {
                    NotaEntregaL NotaAnticipo = new NotaEntregaL();

                    NotaAnticipo.cFechaReg = DataUtil.DbValueToDefault<DateTime>(oIDataReader[idFechaReg]).ToString("hh:mm tt");
                    NotaAnticipo.nNotaEntId = DataUtil.DbValueToDefault<int>(oIDataReader[inNotaEntId]);
                    NotaAnticipo.cPersDesc = DataUtil.DbValueToDefault<string>(oIDataReader[icPersDesc]);
                    NotaAnticipo.nNotaSubTotal = DataUtil.DbValueToDefault<decimal>(oIDataReader[inNotaSubTotal]);
                    NotaAnticipo.nNotaDescuento = DataUtil.DbValueToDefault<decimal>(oIDataReader[inNotaDescuento]);
                    NotaAnticipo.nNotaAnticipo = DataUtil.DbValueToDefault<decimal>(oIDataReader[inNotaAnticipo]);
                    NotaAnticipo.nNotaMontoTotal = DataUtil.DbValueToDefault<decimal>(oIDataReader[inNotaMontoTotal]);

                    ListaNotasPagada.Add(NotaAnticipo);
                }
            }
            return ListaNotasPagada;
        }

        public List<NotaEntregaL> ListaNotasAnticipo(string cUsuario, DateTime dFecha, int nCierre)
        {
            List<NotaEntregaL> ListaNotasAnticipo = new List<NotaEntregaL>();

            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.stp_sel_ListaNotasAnticipo);
            oDatabase.AddInParameter(oDbCommand, "@cUsuario", DbType.String, cUsuario);
            oDatabase.AddInParameter(oDbCommand, "@dFecha", DbType.Date, dFecha);
            oDatabase.AddInParameter(oDbCommand, "@nCierre", DbType.Int32, nCierre);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int idFechaReg = oIDataReader.GetOrdinal("dFechaReg");
                int inNotaEntId = oIDataReader.GetOrdinal("nNotaEntId");
                int icPersDesc = oIDataReader.GetOrdinal("cPersDesc");
                int inNotaSubTotal = oIDataReader.GetOrdinal("nNotaSubTotal");
                int inNotaDescuento = oIDataReader.GetOrdinal("nNotaDescuento");
                int inNotaAnticipo = oIDataReader.GetOrdinal("nNotaAnticipo");
                int inNotaMontoTotal = oIDataReader.GetOrdinal("nNotaMontoTotal");

                while (oIDataReader.Read())
                {
                    NotaEntregaL NotaAnticipo = new NotaEntregaL();

                    NotaAnticipo.cFechaReg = DataUtil.DbValueToDefault<DateTime>(oIDataReader[idFechaReg]).ToString("hh:mm tt");
                    NotaAnticipo.nNotaEntId = DataUtil.DbValueToDefault<int>(oIDataReader[inNotaEntId]);
                    NotaAnticipo.cPersDesc = DataUtil.DbValueToDefault<string>(oIDataReader[icPersDesc]);
                    NotaAnticipo.nNotaSubTotal = DataUtil.DbValueToDefault<decimal>(oIDataReader[inNotaSubTotal]);
                    NotaAnticipo.nNotaDescuento = DataUtil.DbValueToDefault<decimal>(oIDataReader[inNotaDescuento]);
                    NotaAnticipo.nNotaAnticipo = DataUtil.DbValueToDefault<decimal>(oIDataReader[inNotaAnticipo]);
                    NotaAnticipo.nNotaMontoTotal = DataUtil.DbValueToDefault<decimal>(oIDataReader[inNotaMontoTotal]);

                    ListaNotasAnticipo.Add(NotaAnticipo);
                }
            }
            return ListaNotasAnticipo;
        }

        public List<PagoProveedores> ListaPagoProveedores(string cUsuario, DateTime dFecha, int nCierre)
        {
            List<PagoProveedores> ListaProveedores = new List<PagoProveedores>();

            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.stp_sel_ListaPagoProveedores);
            oDatabase.AddInParameter(oDbCommand, "@cUsuario", DbType.String, cUsuario);
            oDatabase.AddInParameter(oDbCommand, "@dFecha", DbType.Date, dFecha);
            oDatabase.AddInParameter(oDbCommand, "@nCierre", DbType.Int32, nCierre);    

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int idMovFecha = oIDataReader.GetOrdinal("dMovFecha");
                int icPersDesc = oIDataReader.GetOrdinal("cPersDesc");
                int inMonto = oIDataReader.GetOrdinal("nMonto");

                while (oIDataReader.Read())
                {
                    PagoProveedores Proveedores = new PagoProveedores();

                    Proveedores.cMovFecha = DataUtil.DbValueToDefault<DateTime>(oIDataReader[idMovFecha]).ToString("hh:mm tt");
                    Proveedores.cPersDesc = DataUtil.DbValueToDefault<string>(oIDataReader[icPersDesc]);
                    Proveedores.nMonto = DataUtil.DbValueToDefault<decimal>(oIDataReader[inMonto]);

                    ListaProveedores.Add(Proveedores);
                }
            }
            return ListaProveedores;
        }
        //END - CORTE DE CAJA

        //APERTURA DE CAJA
        public int RegistrarAperturaCaja(string cUsuarioOpe,decimal nMontoIni,DateTime dFechaApe, string cUsuarioSup, string cAgencia)
        {
            int nMovNro = -2;

            try
            {
                using (SqlConnection oSqlConnection = new SqlConnection(Conexion.cnsCHUYAChuyaSQL))
                {
                    SqlCommand oSqlCommand = new SqlCommand();
                    oSqlCommand.CommandText = Procedimiento.stp_ins_AperturarCaja;
                    oSqlCommand.CommandType = CommandType.StoredProcedure;
                    oSqlCommand.Connection = oSqlConnection;
                    
                    oSqlCommand.Parameters.Add("@cUsuarioOpe", SqlDbType.VarChar, 4).Value = (object)cUsuarioOpe ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@nMontoIni", SqlDbType.Money).Value = (object)nMontoIni ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@dFechaApertura", SqlDbType.DateTime).Value = (object)dFechaApe ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@cUsuarioSup", SqlDbType.VarChar, 4).Value = (object)cUsuarioSup ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@cAgencia", SqlDbType.VarChar, 4).Value = (object)cAgencia ?? DBNull.Value;

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

        public int RegistrarAsigMonto(string cUsuarioOpe, decimal nMontoIni, string cUsuarioSup, string cAgencia)
        {
            int nMovNro = -3;

            try
            {
                using (SqlConnection oSqlConnection = new SqlConnection(Conexion.cnsCHUYAChuyaSQL))
                {
                    SqlCommand oSqlCommand = new SqlCommand();
                    oSqlCommand.CommandText = Procedimiento.stp_ins_RegistrarAsigMonto;
                    oSqlCommand.CommandType = CommandType.StoredProcedure;
                    oSqlCommand.Connection = oSqlConnection;

                    oSqlCommand.Parameters.Add("@cUsuarioOpe", SqlDbType.VarChar, 4).Value = (object)cUsuarioOpe ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@nMontoIni", SqlDbType.Money).Value = (object)nMontoIni ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@cUsuarioSup", SqlDbType.VarChar, 4).Value = (object)cUsuarioSup ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@cAgencia", SqlDbType.VarChar, 4).Value = (object)cAgencia ?? DBNull.Value;

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
                nMovNro = -3;
            }
            return nMovNro;
        }

        public int RegistrarConfDineroIni(int nCCId, string cUsuario, string cAgencia)
        {
            int nMovNro = -2;

            try
            {
                using (SqlConnection oSqlConnection = new SqlConnection(Conexion.cnsCHUYAChuyaSQL))
                {
                    SqlCommand oSqlCommand = new SqlCommand();
                    oSqlCommand.CommandText = Procedimiento.stp_ins_ConfirmarDineroEntrega;
                    oSqlCommand.CommandType = CommandType.StoredProcedure;
                    oSqlCommand.Connection = oSqlConnection;

                    oSqlCommand.Parameters.Add("@nCCId", SqlDbType.Int).Value = (object)nCCId ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@cUsuario", SqlDbType.VarChar,4).Value = (object)cUsuario ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@cAgencia", SqlDbType.VarChar,2).Value = (object)cAgencia ?? DBNull.Value;

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
        

        //END APERTURA DE CAJA

        public CajeroCaja BuscarConfirmacionDineroPendiente(string cUsuario)
        {
            try
            {
                CajeroCaja oCajeroCaja = null;

                DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.stp_sel_ConfirmacionDineroPendiente);
                oDatabase.AddInParameter(oDbCommand, "@cUsuario", DbType.String, (object)cUsuario ?? DBNull.Value);

                using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
                {
                    int inCajeroCajaId = oIDataReader.GetOrdinal("nCajeroCajaId");
                    int inCajeCaMontoInicial = oIDataReader.GetOrdinal("nCajeCaMontoInicial");

                    while (oIDataReader.Read())
                    {
                        oCajeroCaja = new CajeroCaja();

                        oCajeroCaja.nCajeroCajaId = DataUtil.DbValueToDefault<Int32>(oIDataReader[inCajeroCajaId]);
                        oCajeroCaja.nCajeCaMontoInicial = DataUtil.DbValueToDefault<decimal>(oIDataReader[inCajeCaMontoInicial]);
                    }
                }

                return oCajeroCaja;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool CajaDiaAbierto()
        {
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.stp_sel_CajaDiaAbierto);
            oDatabase.AddOutParameter(oDbCommand, "@nRes", DbType.Boolean, 10);

            oDatabase.ExecuteScalar(oDbCommand);
            return Convert.ToBoolean(oDatabase.GetParameterValue(oDbCommand, "@nRes"));
        }

        public CierreDatos UltimaFechaCajaDia()
        {
            CierreDatos oCierreDatos = new CierreDatos();
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.stp_sel_UltimaFechaCajaDia);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int idCajaFecha = oIDataReader.GetOrdinal("dCajaFecha");
                int inCajeCaTotal = oIDataReader.GetOrdinal("nCajeCaTotal");
                int inEstado = oIDataReader.GetOrdinal("nEstado");

                while (oIDataReader.Read())
                {
                    oCierreDatos.nEstado = DataUtil.DbValueToDefault<Byte>(oIDataReader[inEstado]);
                    oCierreDatos.dFecha = DataUtil.DbValueToDefault<DateTime>(oIDataReader[idCajaFecha]);
                    oCierreDatos.nCalculado = DataUtil.DbValueToDefault<decimal>(oIDataReader[inCajeCaTotal]);
                }
            }
            return oCierreDatos;
        }

        public CierreDatos CargaUsuariosCierres(DateTime dFecha)
        {
            CierreDatos oCierreDatos = new CierreDatos();
            oCierreDatos.dFecha = dFecha == default(DateTime) ? UltimaFechaCajaDia().dFecha : dFecha;

            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.stp_sel_ListaCajeros);
            oDatabase.AddInParameter(oDbCommand, "@dFecha", DbType.Date, oCierreDatos.dFecha);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int icUsuario = oIDataReader.GetOrdinal("cUsuario");

                while (oIDataReader.Read())
                {
                    UsuarioCierres oUsuCierres = new UsuarioCierres();
                    oUsuCierres.cUsuario = DataUtil.DbValueToDefault<string>(oIDataReader[icUsuario]);
                    oUsuCierres.oListCierres = ListaCierres( oUsuCierres.cUsuario,oCierreDatos.dFecha);

                    oCierreDatos.oListUsuarioCierres.Add(oUsuCierres);
                }
            }
            return oCierreDatos;
        }

        public List<Cierres> ListaCierres(string cUsuario, DateTime dFecha)
        {
            List<Cierres> ListaCierres = new List<Cierres>();

            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.stp_sel_ListaCierre);
            oDatabase.AddInParameter(oDbCommand, "@cUsuario", DbType.String, cUsuario);
            oDatabase.AddInParameter(oDbCommand, "@dFecha", DbType.Date, dFecha);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int inCajeroCajaId = oIDataReader.GetOrdinal("nCajeroCajaId");
                int idFechaInicio = oIDataReader.GetOrdinal("dFechaInicio");
                int idFechaCierre = oIDataReader.GetOrdinal("dFechaCierre");

                while (oIDataReader.Read())
                {
                    Cierres oCierres = new Cierres();

                    oCierres.nCajeCaId = DataUtil.DbValueToDefault<int>(oIDataReader[inCajeroCajaId]);
                    oCierres.dFechaIni = DataUtil.DbValueToDefault<DateTime>(oIDataReader[idFechaInicio]);
                    oCierres.dFechaFin = DataUtil.DbValueToDefault<DateTime>(oIDataReader[idFechaCierre]);

                    ListaCierres.Add(oCierres);
                }
            }
            return ListaCierres;
        }


        public int RealizarCierreCaja(string cUsuario, string cAgencia, decimal nCont, decimal nDif)
        {
            int nCajaCaId = -3;

            try
            {
                using (SqlConnection oSqlConnection = new SqlConnection(Conexion.cnsCHUYAChuyaSQL))
                {
                    SqlCommand oSqlCommand = new SqlCommand();
                    oSqlCommand.CommandText = Procedimiento.stp_ins_RealizarCierreCaja;
                    oSqlCommand.CommandType = CommandType.StoredProcedure;
                    oSqlCommand.Connection = oSqlConnection;

                    oSqlCommand.Parameters.Add("@cUsuario", SqlDbType.VarChar, 4).Value = (object)cUsuario ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@cAgencia", SqlDbType.VarChar, 2).Value = (object)cAgencia ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@nCont", SqlDbType.Money, 4).Value = (object)nCont ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@nDif", SqlDbType.Money, 4).Value = (object)nDif ?? DBNull.Value;

                    oSqlConnection.Open();

                    using (IDataReader oIDataReader = oSqlCommand.ExecuteReader())
                    {
                        int inCajaCaId = oIDataReader.GetOrdinal("nCajaCaId");

                        while (oIDataReader.Read())
                        {
                            nCajaCaId = DataUtil.DbValueToDefault<int>(oIDataReader[inCajaCaId]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                nCajaCaId = -3;
            }
            return nCajaCaId;
        }

        public int RealizarCierreCajaDia(string cUsuario, string cAgencia)
        {
            int nCajaId = -4;

            try
            {
                using (SqlConnection oSqlConnection = new SqlConnection(Conexion.cnsCHUYAChuyaSQL))
                {
                    SqlCommand oSqlCommand = new SqlCommand();
                    oSqlCommand.CommandText = Procedimiento.stp_ins_RealizarCierreCajaDia;
                    oSqlCommand.CommandType = CommandType.StoredProcedure;
                    oSqlCommand.Connection = oSqlConnection;

                    oSqlCommand.Parameters.Add("@cUsuario", SqlDbType.VarChar, 4).Value = (object)cUsuario ?? DBNull.Value;
                    oSqlCommand.Parameters.Add("@cAgencia", SqlDbType.VarChar, 4).Value = (object)cAgencia ?? DBNull.Value;

                    oSqlConnection.Open();

                    using (IDataReader oIDataReader = oSqlCommand.ExecuteReader())
                    {
                        int inCajaId = oIDataReader.GetOrdinal("nCajaId");

                        while (oIDataReader.Read())
                        {
                            nCajaId = DataUtil.DbValueToDefault<int>(oIDataReader[inCajaId]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                nCajaId = -4;
            }
            return nCajaId;
        }

        public TicketCierre ObtenerDatosCierreImp(int nCajeCaId)
        {
            try
            {
                TicketCierre oTicketC = new TicketCierre();

                DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.stp_sel_ObtenerDatosCierre);
                oDatabase.AddInParameter(oDbCommand, "@nCajaCaId", DbType.Int32, (object)nCajeCaId ?? DBNull.Value);

                using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
                {
                    int inCajaCaId = oIDataReader.GetOrdinal("nCajaCaId");
                    int idFechaApertura = oIDataReader.GetOrdinal("dFechaApertura");
                    int idFechaCierre = oIDataReader.GetOrdinal("dFechaCierre");
                    int icUsuario = oIDataReader.GetOrdinal("cUsuario");
                    int inContado = oIDataReader.GetOrdinal("nContado");
                    int inDiferencia = oIDataReader.GetOrdinal("nDiferencia");

                    int inCajaInicio = oIDataReader.GetOrdinal("nCajaInicio");
                    int inCajaEntradaEfectivo = oIDataReader.GetOrdinal("nCajaEntradaEfectivo");
                    int inEntTotal = oIDataReader.GetOrdinal("nEntTotal");

                    int inCajaNotaAnticipo = oIDataReader.GetOrdinal("nCajaNotaAnticipo");
                    int inCajaNotaPagadas = oIDataReader.GetOrdinal("nCajaNotaPagadas");
                    int inPagoProv = oIDataReader.GetOrdinal("nPagoProv");
                    int inSalidaOtro = oIDataReader.GetOrdinal("nSalidaOtro");
                    int inAnula = oIDataReader.GetOrdinal("nAnula");
                    int inCajaTotal = oIDataReader.GetOrdinal("nCajaTotal");

                    while (oIDataReader.Read())
                    {
                        oTicketC.nCajaCaId = DataUtil.DbValueToDefault<Int32>(oIDataReader[inCajaCaId]);
                        oTicketC.dFechaApertura = DataUtil.DbValueToDefault<DateTime>(oIDataReader[idFechaApertura]);
                        oTicketC.dFechaCierre = DataUtil.DbValueToDefault<DateTime>(oIDataReader[idFechaCierre]);
                        oTicketC.cUsuario = DataUtil.DbValueToDefault<String>(oIDataReader[icUsuario]);
                        oTicketC.nContado = DataUtil.DbValueToDefault<decimal>(oIDataReader[inContado]);
                        oTicketC.nDiferencia = DataUtil.DbValueToDefault<decimal>(oIDataReader[inDiferencia]);

                        oTicketC.nCajaInicio = DataUtil.DbValueToDefault<decimal>(oIDataReader[inCajaInicio]);
                        oTicketC.nCajaEntEfec = DataUtil.DbValueToDefault<decimal>(oIDataReader[inCajaEntradaEfectivo]);
                        oTicketC.nEntTotal = DataUtil.DbValueToDefault<decimal>(oIDataReader[inEntTotal]);

                        oTicketC.nCajaNotaAnticipo = DataUtil.DbValueToDefault<decimal>(oIDataReader[inCajaNotaAnticipo]);
                        oTicketC.nCajaNotaPagadas = DataUtil.DbValueToDefault<decimal>(oIDataReader[inCajaNotaPagadas]);
                        oTicketC.nPagoProv = DataUtil.DbValueToDefault<decimal>(oIDataReader[inPagoProv]);
                        oTicketC.nSalidaOtro = DataUtil.DbValueToDefault<decimal>(oIDataReader[inSalidaOtro]);
                        oTicketC.nAnula = DataUtil.DbValueToDefault<decimal>(oIDataReader[inAnula]);
                        oTicketC.nCajaTotal = DataUtil.DbValueToDefault<decimal>(oIDataReader[inCajaTotal]);
                    }
                }

                return oTicketC;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public TicketCierre ObtenerDatosCierreDiaImp(int nCajeId)
        {
            try
            {
                TicketCierre oTicketC = new TicketCierre();

                DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.stp_sel_ObtenerDatosCierreDia);
                oDatabase.AddInParameter(oDbCommand, "@nCaja", DbType.Int32, (object)nCajeId ?? DBNull.Value);

                using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
                {
                    int inCajaCaId = oIDataReader.GetOrdinal("nCajaCaId");
                    int idFechaApertura = oIDataReader.GetOrdinal("dFechaApertura");
                    int idFechaCierre = oIDataReader.GetOrdinal("dFechaCierre");
                    int icUsuario = oIDataReader.GetOrdinal("cUsuario");
                    //int inContado = oIDataReader.GetOrdinal("nContado");
                    //int inDiferencia = oIDataReader.GetOrdinal("nDiferencia");

                    int inCajaInicio = oIDataReader.GetOrdinal("nCajaInicio");
                    int inCajaEntradaEfectivo = oIDataReader.GetOrdinal("nCajaEntradaEfectivo");
                    int inEntTotal = oIDataReader.GetOrdinal("nEntTotal");

                    int inCajaNotaAnticipo = oIDataReader.GetOrdinal("nCajaNotaAnticipo");
                    int inCajaNotaPagadas = oIDataReader.GetOrdinal("nCajaNotaPagadas");
                    int inPagoProv = oIDataReader.GetOrdinal("nPagoProv");
                    int inSalidaOtro = oIDataReader.GetOrdinal("nSalidaOtro");
                    int inAnula = oIDataReader.GetOrdinal("nAnula");
                    int inCajaTotal = oIDataReader.GetOrdinal("nCajaTotal");

                    while (oIDataReader.Read())
                    {
                        oTicketC.nCajaCaId = DataUtil.DbValueToDefault<Int32>(oIDataReader[inCajaCaId]);
                        oTicketC.dFechaApertura = DataUtil.DbValueToDefault<DateTime>(oIDataReader[idFechaApertura]);
                        oTicketC.dFechaCierre = DataUtil.DbValueToDefault<DateTime>(oIDataReader[idFechaCierre]);
                        oTicketC.cUsuario = DataUtil.DbValueToDefault<String>(oIDataReader[icUsuario]);
                        //oTicketC.nContado = DataUtil.DbValueToDefault<decimal>(oIDataReader[inContado]);
                        //oTicketC.nDiferencia = DataUtil.DbValueToDefault<decimal>(oIDataReader[inDiferencia]);

                        oTicketC.nCajaInicio = DataUtil.DbValueToDefault<decimal>(oIDataReader[inCajaInicio]);
                        oTicketC.nCajaEntEfec = DataUtil.DbValueToDefault<decimal>(oIDataReader[inCajaEntradaEfectivo]);
                        oTicketC.nEntTotal = DataUtil.DbValueToDefault<decimal>(oIDataReader[inEntTotal]);

                        oTicketC.nCajaNotaAnticipo = DataUtil.DbValueToDefault<decimal>(oIDataReader[inCajaNotaAnticipo]);
                        oTicketC.nCajaNotaPagadas = DataUtil.DbValueToDefault<decimal>(oIDataReader[inCajaNotaPagadas]);
                        oTicketC.nPagoProv = DataUtil.DbValueToDefault<decimal>(oIDataReader[inPagoProv]);
                        oTicketC.nSalidaOtro = DataUtil.DbValueToDefault<decimal>(oIDataReader[inSalidaOtro]);
                        oTicketC.nAnula = DataUtil.DbValueToDefault<decimal>(oIDataReader[inAnula]);
                        oTicketC.nCajaTotal = DataUtil.DbValueToDefault<decimal>(oIDataReader[inCajaTotal]);
                    }
                }

                return oTicketC;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public string ObtenerUsuarioIniciaDia(string cUsuario)
        {
            try
            {
                string cUsuIniDia = "";
                DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.stp_sel_UsuarioIniciaDia);
                oDatabase.AddInParameter(oDbCommand, "@cUsuario", DbType.String, (object)cUsuario ?? DBNull.Value);

                using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
                {
                    int icUsuarioSup = oIDataReader.GetOrdinal("cUsuarioSup");

                    while (oIDataReader.Read())
                    {
                        cUsuIniDia = DataUtil.DbValueToDefault<String>(oIDataReader[icUsuarioSup]);
                    }
                }

                return cUsuIniDia;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public decimal ObtenerUltimoSaldoCaja()
        {
            try
            {
                decimal nSaldo = 0;
                DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.stp_sel_UltimoSaldoCaja);

                using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
                {
                    int inCajaTotal = oIDataReader.GetOrdinal("nCajaTotal");

                    while (oIDataReader.Read())
                    {
                        nSaldo = DataUtil.DbValueToDefault<decimal>(oIDataReader[inCajaTotal]);
                    }
                }

                return nSaldo;
            }
            catch (Exception)
            {

                throw;
            }
        }

        

    }
}
