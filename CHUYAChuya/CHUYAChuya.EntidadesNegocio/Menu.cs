using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CHUYAChuya.EntidadesNegocio
{
    public class Menu
    {
        private int _id_menu;
        private int _id_padre;
        private string _descripcion;
        private string _icono;
        private int _posicion;
        private string _url;
        private string _estado;
        private int _id_pmenu;

        //**********************
        private int _tipoPermiso;
        private List<Menu> _listaMenu;
        //**********************
        private bool _seleccionado;

        public int id_menu
        {
            get { return _id_menu; }
            set { _id_menu = value; }
        }

        public int id_padre
        {
            get { return _id_padre; }
            set { _id_padre = value; }
        }

        public string descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        public string icono
        {
            get { return _icono; }
            set { _icono = value; }
        }

        public int posicion
        {
            get { return _posicion; }
            set { _posicion = value; }
        }

        public string url
        {
            get { return _url; }
            set { _url = value; }
        }

        public string estado
        {
            get { return _estado; }
            set { _estado = value; }
        }

        public int id_pmenu
        {
            get { return _id_pmenu; }
            set { _id_pmenu = value; }
        }

        public int tipoPermiso
        {
            get { return _tipoPermiso; }
            set { _tipoPermiso = value; }
        }

        public List<Menu> listaMenu
        {
            get { return _listaMenu; }
            set { _listaMenu = value; }
        }

        public bool seleccionado
        {
            get { return _seleccionado; }
            set { _seleccionado = value; }
        }
    }
}
