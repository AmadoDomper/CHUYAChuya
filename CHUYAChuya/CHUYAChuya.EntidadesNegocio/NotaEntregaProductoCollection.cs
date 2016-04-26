using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Server;

namespace CHUYAChuya.EntidadesNegocio
{
    public class NotaEntregaProductoCollection
    {
        public static IEnumerable<SqlDataRecord> TSqlDataRecord(List<NotaEntProd> listaNotaEntProd)
        {
            List<SqlDataRecord> listaSqlDataRecord = new List<SqlDataRecord>();

            foreach (NotaEntProd oNotaEntProd in listaNotaEntProd)
            {
                SqlDataRecord oSqlDataRecord = new SqlDataRecord(
                    new SqlMetaData[]{ new SqlMetaData("nProdId", SqlDbType.Int),
                                        new SqlMetaData("nDetCantidad", SqlDbType.Decimal),
                                        new SqlMetaData("nDetProdPrecioUnit", SqlDbType.Money),
                                        new SqlMetaData("nDetImporte", SqlDbType.Money)
                                    });

                oSqlDataRecord.SetInt32(0, oNotaEntProd.oProd.nProdId);
                oSqlDataRecord.SetDecimal(1, oNotaEntProd.nDetCantidad);
                oSqlDataRecord.SetSqlMoney(2, oNotaEntProd.nProdPrecioUnit);
                oSqlDataRecord.SetSqlMoney(3, oNotaEntProd.nDetImporte);
                listaSqlDataRecord.Add(oSqlDataRecord);
            }

            return listaSqlDataRecord;
        }
    }
}
