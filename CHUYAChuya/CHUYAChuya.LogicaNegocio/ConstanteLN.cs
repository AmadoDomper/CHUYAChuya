using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CHUYAChuya.EntidadesNegocio;
using CHUYAChuya.AccesoDatos;

namespace CHUYAChuya.LogicaNegocio
{
    public class ConstanteLN
    {
        ConstanteAD oConstanteAD;

        public ConstanteLN()
        {
            oConstanteAD = new ConstanteAD();
        }

        public List<Constante> ListaDepartamento()
        {
            return oConstanteAD.ListaDepartamento();
        }

        public List<Constante> ListaProvincia(string cDepId)
        {
            return oConstanteAD.ListaProvincia(cDepId);
        }

        public List<Constante> ListaDistrito(string cProvId)
        {
            return oConstanteAD.ListaDistrito(cProvId);
        }

    }
}
