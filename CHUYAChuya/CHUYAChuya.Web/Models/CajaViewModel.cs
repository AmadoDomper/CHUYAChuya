using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CHUYAChuya.EntidadesNegocio;
using Newtonsoft.Json;

namespace CHUYAChuya.Web.Models
{
    public class CajaViewModel
    {
        public List<Constante> lstUsuarios { get; set; }
        public List<Constante> lstTpoCom { get; set; }
    }
}