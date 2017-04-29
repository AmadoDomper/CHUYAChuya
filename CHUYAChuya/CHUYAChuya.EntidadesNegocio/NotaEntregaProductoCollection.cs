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
                                        new SqlMetaData("nDetCantidad", SqlDbType.Decimal,12,3),
                                        new SqlMetaData("nDetProdPrecioUnit", SqlDbType.Money),
                                        new SqlMetaData("nDetImporte", SqlDbType.Money),
                                        new SqlMetaData("cDescrip", SqlDbType.VarChar,200),
                                        new SqlMetaData("nProdMedida",SqlDbType.TinyInt),
                                        new SqlMetaData("bProdSerLavado",SqlDbType.Bit),
                                        new SqlMetaData("bProdSerSecado",SqlDbType.Bit),
                                        new SqlMetaData("bProdSerPlanchado",SqlDbType.Bit),
                                        new SqlMetaData("bProdOtros",SqlDbType.Bit)
                                    });

                oSqlDataRecord.SetInt32(0, oNotaEntProd.oProd.nProdId);
                oSqlDataRecord.SetSqlDecimal(1, oNotaEntProd.nDetCantidad);
                oSqlDataRecord.SetSqlMoney(2, oNotaEntProd.nProdPrecioUnit);
                oSqlDataRecord.SetSqlMoney(3, oNotaEntProd.nDetImporte);
                oSqlDataRecord.SetSqlString(4, oNotaEntProd.oProd.cProdDesc);
                oSqlDataRecord.SetSqlByte(5, Convert.ToByte(oNotaEntProd.oProd.oProdMedida.cConstanteID));
                oSqlDataRecord.SetSqlBoolean(6, oNotaEntProd.oProd.bProdSerLavado);
                oSqlDataRecord.SetSqlBoolean(7, oNotaEntProd.oProd.bProdSerSecado);
                oSqlDataRecord.SetSqlBoolean(8, oNotaEntProd.oProd.bProdSerPlanchado);
                oSqlDataRecord.SetSqlBoolean(9, oNotaEntProd.oProd.bProdOtros);
                listaSqlDataRecord.Add(oSqlDataRecord);
            }

            return listaSqlDataRecord;
        }
    }
}
