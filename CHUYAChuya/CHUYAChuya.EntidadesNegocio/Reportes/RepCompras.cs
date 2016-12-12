using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CHUYAChuya.EntidadesNegocio.Reportes
{
    public class RepCompras
    {

        private Int64 _nNumero;
        private string _dFechaE;
        private string _cTipoTabla;
        private string _cComprobante;
        private string _cDOI;
        private string _cPersDesc;
        private decimal _nMonto;

        [JsonProperty(PropertyName = "nNum")]
        public Int64 nNumero
        {
            get { return _nNumero; }
            set { _nNumero = value; }
        }

        [JsonProperty(PropertyName = "dFecha")]
        public string dFechaE
        {
            get { return _dFechaE; }
            set { _dFechaE = value; }
        }

        [JsonProperty(PropertyName = "cTpoT")]
        public string cTipoTabla
        {
            get { return _cTipoTabla; }
            set { _cTipoTabla = value; }
        }

        [JsonProperty(PropertyName = "cComp")]
        public string cComprobante
        {
            get { return _cComprobante; }
            set { _cComprobante = value; }
        }

        [JsonProperty(PropertyName = "cDoi")]
        public string cDOI
        {
            get { return _cDOI; }
            set { _cDOI = value; }
        }

        [JsonProperty(PropertyName = "cPersDesc")]
        public string cPersDesc
        {
            get { return _cPersDesc; }
            set { _cPersDesc = value; }
        }

        [JsonProperty(PropertyName = "nMonto")]
        public decimal nMonto
        {
            get { return _nMonto; }
            set { _nMonto = value; }
        }
    }
}
