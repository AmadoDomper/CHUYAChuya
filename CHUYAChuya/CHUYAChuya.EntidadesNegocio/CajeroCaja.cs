using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CHUYAChuya.EntidadesNegocio
{
    public class CajeroCaja
    {
        private int _nCajeroCajaId;
        private decimal _nCajeCaMontoInicial;

        [JsonProperty(PropertyName = "nCC_Id")]
        public int nCajeroCajaId
        {
            get { return _nCajeroCajaId; }
            set { _nCajeroCajaId = value; }
        }

        [JsonProperty(PropertyName = "nCC_MonIni")]
        public decimal nCajeCaMontoInicial
        {
            get { return _nCajeCaMontoInicial; }
            set { _nCajeCaMontoInicial = value; }
        }


    }
}
