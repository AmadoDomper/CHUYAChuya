using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CHUYAChuya.AccesoDatos;
using CHUYAChuya.EntidadesNegocio;

namespace CHUYAChuya.LogicaNegocio
{
    public class NotaEntregaLN
    {
        NotaEntregaAD oNotaEntregaAD;

        public NotaEntregaLN()
        {
            oNotaEntregaAD = new NotaEntregaAD();
        }

        public int RegistrarNotaEntrega(NotaEntrega oNotEnt)
        {
            return oNotaEntregaAD.RegistrarNotaEntrega(oNotEnt);
        }

        public List<NotaEntrega> BuscarNotaEntregas(int nNotaEst, int nNotaEntId = -1, string cPersDOI = null, string cPersDesc = null, DateTime? dIni = null, DateTime? dFin = null)
        {
            return oNotaEntregaAD.BuscarNotaEntregas(nNotaEst, nNotaEntId, cPersDOI, cPersDesc, dIni, dFin);
        }

        public NotaEntrega CargoDatosNotaEntrega(int nNotadId)
        {
            return oNotaEntregaAD.CargoDatosNotaEntrega(nNotadId);
        }

        


    }
}
