﻿using CHUYAChuya.AccesoDatos;
using CHUYAChuya.EntidadesNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHUYAChuya.LogicaNegocio
{
    public class SeguridadLN
    {
        public Usuario ObtenerUsuarioContrasena(Usuario oUsuario)
        {
            return new SeguridadAD().ObtenerUsuarioContrasena(oUsuario);
        }
    }
}
