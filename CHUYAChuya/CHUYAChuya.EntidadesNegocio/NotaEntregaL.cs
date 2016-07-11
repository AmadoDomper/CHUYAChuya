using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CHUYAChuya.EntidadesNegocio
{
    public class NotaEntregaL
    {
      private DateTime _dFechaReg;
	  private int _nNotaEntId;
	  private string _cPersDesc;
	  private decimal _nNotaSubTotal;
	  private decimal _nNotaDescuento;
	  private decimal _nNotaAnticipo;
      private decimal _nNotaMontoTotal;

      [JsonProperty(PropertyName = "dFecReg")]
      public DateTime dFechaReg
      {
          get { return _dFechaReg; }
          set { _dFechaReg = value; }
      }

      [JsonProperty(PropertyName = "nNotaEntId")]
      public int nNotaEntId
      {
          get { return _nNotaEntId; }
          set { _nNotaEntId = value; }
      }

      [JsonProperty(PropertyName = "cPersDesc")]
      public string cPersDesc
      {
          get { return _cPersDesc; }
          set { _cPersDesc = value; }
      }

      [JsonProperty(PropertyName = "nNotSubTot")]
      public decimal nNotaSubTotal
      {
          get { return _nNotaSubTotal; }
          set { _nNotaSubTotal = value; }
      }

      [JsonProperty(PropertyName = "nNotaDes")]
      public decimal nNotaDescuento
      {
          get { return _nNotaDescuento; }
          set { _nNotaDescuento = value; }
      }

      [JsonProperty(PropertyName = "nNotaAnt")]
      public decimal nNotaAnticipo
      {
          get { return _nNotaAnticipo; }
          set { _nNotaAnticipo = value; }
      }

      [JsonProperty(PropertyName = "nNotaMonTot")]
      public decimal nNotaMontoTotal
      {
          get { return _nNotaMontoTotal; }
          set { _nNotaMontoTotal = value; }
      }


    }
}
