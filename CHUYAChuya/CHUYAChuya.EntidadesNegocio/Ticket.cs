using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CHUYAChuya.EntidadesNegocio
{
    public class Ticket
    {
        private int _nTicketId;
        private int _nTicketSerie;
        private int _nTicketCorrelativo;
        private NotaEntrega _oNotaEntrega = new NotaEntrega();
        private Mov _oMov = new Mov();
        private Constante _oTicketTipo = new Constante();
        private Impresora _oImp = new Impresora();
        private int _nTicketEst;

        [JsonProperty(PropertyName = "nTicketId")]
        public int nTicketId
        {
            get { return _nTicketId; }
            set { _nTicketId = value; }
        }

        [JsonProperty(PropertyName = "nTicketSerie")]
        public int nTicketSerie
        {
            get { return _nTicketSerie; }
            set { _nTicketSerie = value; }
        }

        [JsonProperty(PropertyName = "_nTicketCorr")]
        public int nTicketCorrelativo
        {
            get { return _nTicketCorrelativo; }
            set { _nTicketCorrelativo = value; }
        }

        [JsonProperty(PropertyName = "_oNotaEnt")]
        public NotaEntrega oNotaEntrega
        {
            get { return _oNotaEntrega; }
            set { _oNotaEntrega = value; }
        }

        [JsonProperty(PropertyName = "oMov")]
        public Mov oMov
        {
            get { return _oMov; }
            set { _oMov = value; }
        }

        [JsonProperty(PropertyName = "oTicketTpo")]
        public Constante oTicketTipo
        {
            get { return oTicketTipo; }
            set { oTicketTipo = value; }
        }

        [JsonProperty(PropertyName = "oImp")]
        public Impresora oImp
        {
            get { return _oImp; }
            set { _oImp = value; }
        }

        [JsonProperty(PropertyName = "nTicketEst")]
        public int nTicketEst
        {
            get { return _nTicketEst; }
            set { _nTicketEst = value; }
        }
    }
}
