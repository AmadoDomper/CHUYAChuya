﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CHUYAChuya.AccesoDatos.Helper
{
    public class Conexion
    {
        public static string cnsCHUYAChuya = "cnsCHUYAChuya";
        public static string cnsCHUYAChuyaSQL = ConfigurationManager.ConnectionStrings["cnsCHUYAChuya"].ToString();
    }
}
