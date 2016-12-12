using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CHUYAChuya.EntidadesNegocio.Reportes
{
    public class RepVentas
    {
        private Int64 _nNumero;
        private string _dFechaE;
        private string _cTipoTabla;
        private string _cSerie;
        private string _cCorrelativo;
        private string _cDOI;
        private string _cPersDesc;
        private decimal _nNotaSub;

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

        [JsonProperty(PropertyName = "cSerT")]
        public string cSerie
        {
            get { return _cSerie; }
            set { _cSerie = value; }
        }

        [JsonProperty(PropertyName = "cCorrT")]
        public string cCorrelativo
        {
            get { return _cCorrelativo; }
            set { _cCorrelativo = value; }
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

        [JsonProperty(PropertyName = "nNotSub")]
        public decimal nNotaSub
        {
            get { return _nNotaSub; }
            set { _nNotaSub = value; }
        }

       
    }
}
