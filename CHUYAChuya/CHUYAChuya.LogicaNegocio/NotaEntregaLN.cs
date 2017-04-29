using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CHUYAChuya.EntidadesNegocio;
using CHUYAChuya.AccesoDatos;

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

        public int ModificarComentario(int nNotaId, string cCom)
        {
            return oNotaEntregaAD.ModificarComentario(nNotaId, cCom);
        }

        public int RealizarCobroServicio(string cNotaEntId, int nPersId, string cPersNombre, string cPersApe, string cPersDOI, string cPersDirec, int nTipoC, decimal nEfecCo, decimal nCambioCo, string cNotaUsuCo, string cNotaUsuAge, bool bGuaCli)
        {
            return oNotaEntregaAD.RealizarCobroServicio(cNotaEntId, nPersId, cPersNombre, cPersApe, cPersDOI, cPersDirec, nTipoC, nEfecCo, nCambioCo, cNotaUsuCo, cNotaUsuAge, bGuaCli);
        }

        public int RealizarConfirmacionEntrega(int nNotaEntId, string cUsuario, string cUsuarioAge)
        {
            return oNotaEntregaAD.RealizarConfirmacionEntrega(nNotaEntId, cUsuario, cUsuarioAge);
        }

        public ListaPaginada BuscarNotaEntPag( int nNotaEst,int nPage=1, int nSize=10, int nNotaEntId = -1, string cPersDOI = null, string cPersDesc = null, DateTime? dIni = null, DateTime? dFin = null)
        {
            return oNotaEntregaAD.BuscarNotaEntPag(nNotaEst, nPage, nSize, nNotaEntId, cPersDOI, cPersDesc, dIni, dFin);
        }

        public List<NotaEntrega> BuscarNotaEntPend(int nNotaId, bool bMult)
        {
            return oNotaEntregaAD.BuscarNotaEntPend(nNotaId, bMult);
        }

        public NotaEntrega CargoDatosNotaEntrega(int nNotadId)
        {
            return oNotaEntregaAD.CargoDatosNotaEntrega(nNotadId);
        }

        public Ticket ObtenerDatosNotaEntImp(int nNotaId)
        {
            return oNotaEntregaAD.ObtenerDatosNotaEntImp(nNotaId);
        }

        public Ticket ObtenerDatosTicket(int nTicketId)
        {
            return oNotaEntregaAD.ObtenerDatosTicket(nTicketId);
        }

        public int RealizarAnularComprobante(int nNotaEntId, string cUsuario, string cUsuarioAge)
        {
            return oNotaEntregaAD.RealizarAnularComprobante(nNotaEntId, cUsuario, cUsuarioAge);
        }

        public int RealizarAnularNota(int nNotaEntId, string cUsuario, string cUsuarioAge)
        {
            return oNotaEntregaAD.RealizarAnularNota(nNotaEntId, cUsuario, cUsuarioAge);
        }

        

    }
}
