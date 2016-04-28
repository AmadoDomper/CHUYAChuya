using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CHUYAChuya.EntidadesNegocio
{
    public class NotaEntrega
    {
        private int _nNotaEntId;
        private Persona _oPers = new Persona();
        private string _cNotaDireccion;
        private string _cNotaComentario;
        private DateTime _dFechaReg;
        private DateTime _dFechaEntrega;
        private decimal _nNotaSubTotal;
        private decimal _nNotaMontoTotal;
        private Constante _oNotaEstado = new Constante();
        private string _cNotaUsuReg;
        private List<NotaEntProd> _ListaNotaEntProd;

        [JsonProperty(PropertyName = "nNotaEntId")]
        public int nNotaEntId
        {
            get { return _nNotaEntId; }
            set { _nNotaEntId = value; }
        }

        [JsonProperty(PropertyName = "oPers")]
        public Persona oPers
        {
            get { return _oPers; }
            set { _oPers = value; }
        }

        [JsonProperty(PropertyName = "cNotaDir")]
        public string cNotaDireccion
        {
            get { return _cNotaDireccion; }
            set { _cNotaDireccion = value; }
        }

        [JsonProperty(PropertyName = "cNotaCom")]
        public string cNotaComentario
        {
            get { return _cNotaComentario; }
            set { _cNotaComentario = value; }
        }

        [JsonProperty(PropertyName = "dFecReg")]
        public DateTime dFechaReg
        {
            get { return _dFechaReg; }
            set { _dFechaReg = value; }
        }

        [JsonProperty(PropertyName = "dFecEnt")]
        public DateTime dFechaEntrega
        {
            get { return _dFechaEntrega; }
            set { _dFechaEntrega = value; }
        }

        [JsonProperty(PropertyName = "nNotaSubT")]
        public decimal nNotaSubTotal
        {
            get { return _nNotaSubTotal; }
            set { _nNotaSubTotal = value; }
        }

        [JsonProperty(PropertyName = "nNotaMonTot")]
        public decimal nNotaMontoTotal
        {
            get { return _nNotaMontoTotal; }
            set { _nNotaMontoTotal = value; }
        }

        [JsonProperty(PropertyName = "oNotaEst")]
        public Constante oNotaEstado
        {
            get { return _oNotaEstado; }
            set { _oNotaEstado = value; }
        }

        [JsonProperty(PropertyName = "cNotaUsuReg")]
        public string cNotaUsuReg
        {
            get { return _cNotaUsuReg; }
            set { _cNotaUsuReg = value; }
        }

        [JsonProperty(PropertyName = "lstNotEntProd")]
        public List<NotaEntProd> ListaNotaEntProd
        {
            get { return _ListaNotaEntProd; }
            set { _ListaNotaEntProd = value; }
        }

    }
}
