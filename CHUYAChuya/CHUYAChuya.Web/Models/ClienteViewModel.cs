using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CHUYAChuya.EntidadesNegocio;
using Newtonsoft.Json;

namespace CHUYAChuya.Web.Models
{
    public class ClienteViewModel
    {

        public PersonaJur PersJur {get; set;}
        public PersonaNat PersNat { get; set; }
        


    }
}