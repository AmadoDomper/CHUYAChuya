﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Server;

namespace CHUYAChuya.EntidadesNegocio
{
    public class ListaMenuCollection
    {
        public static IEnumerable<SqlDataRecord> TSqlDataRecord(List<Menu> ListaMenu)
        {
            List<SqlDataRecord> listaSqlDataRecord = new List<SqlDataRecord>();

            foreach (Menu oMenu in ListaMenu)
            {
                SqlDataRecord oSqlDataRecord = new SqlDataRecord(
                    new SqlMetaData[]{ new SqlMetaData("nMenuId", SqlDbType.Int),
                                        new SqlMetaData("nValor", SqlDbType.Bit)
                                    });

                oSqlDataRecord.SetInt32(0, oMenu.nMenuId);
                oSqlDataRecord.SetBoolean(1, oMenu.bEstado);
                listaSqlDataRecord.Add(oSqlDataRecord);
            }

            return listaSqlDataRecord;
        }

    }
}
