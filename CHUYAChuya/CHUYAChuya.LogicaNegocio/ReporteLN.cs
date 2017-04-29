using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CHUYAChuya.EntidadesNegocio.Reportes;
using CHUYAChuya.AccesoDatos;

namespace CHUYAChuya.LogicaNegocio
{
    public class ReporteLN
    {
        ReporteAD oReporteAD;

        public ReporteLN()
        {
            oReporteAD = new ReporteAD();
        }

        public List<RepVentas> ListarVentasIngresos(DateTime dIni, DateTime dFin)
        {
            return oReporteAD.ListarVentasIngresos(dIni, dFin);
        }

        public List<RepCompras> ListarCompras(DateTime dIni, DateTime dFin)
        {
            return oReporteAD.ListarCompras(dIni, dFin);
        }

        public List<RepNotaEntregas> ListarNotaEntrega(DateTime dIni, DateTime dFin)
        {
            return oReporteAD.ListarNotaEntrega(dIni, dFin);
        }
    }
}
