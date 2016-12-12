using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CHUYAChuya.EntidadesNegocio
{
    public class Cierres
    {
        private int _nCajeCaId;
        private DateTime _dFechaIni;
        private DateTime _dFechaFin;
        private string _cFecha;

        [JsonProperty(PropertyName = "nCajeCaId")]
        public int nCajeCaId
        {
            get { return _nCajeCaId; }
            set { _nCajeCaId = value; }
        }

        [JsonIgnore]
        public DateTime dFechaIni
        {
            get { return _dFechaIni; }
            set { _dFechaIni = value; }
        }

        [JsonIgnore]
        public DateTime dFechaFin
        {
            get { return _dFechaFin; }
            set { _dFechaFin = value; }
        }

        [JsonProperty(PropertyName = "cFecha")]
        public string cFecha
        {
            get { return _cFecha; }
            set { _cFecha = value; }
        }


    }
}
