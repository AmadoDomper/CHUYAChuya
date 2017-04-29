(function ($)
{
    var lstBuscaProv = [];
    $.fn.BuscarProveedor = function (m)
    {
        m = m || {};
        /*
            var m = {				
                    Direccion: "aa/gQuery.js";
                    FuncionAceptar:function(){mensaje aceptar};
                    FuncionCancelar:function(){mensaje cancelar};
            }		
        */
        m.funcionCancelar = m.FuncionCancelar || function () { };
        m.valor = m.valor || null;
        m.durante = m.durante || function () { };
        //m["terminado"] = m["terminado"] || function () { };
        m.error = m.error || function () { };

        var tablaId = "tblProveedor";
        var btnAceptar = "#vntBuscaProveedor #btnProvAceptar";
        var tabla_Id = "#" + tablaId;
        var buscarId = "txtBuscar";
        var buscar_Id = "#" + buscarId;

        if (!m.valor) {
            CrearVentana();
        } else {
            BuscarClientes(m.valor);
            
            if (lstBuscaProv.length == 1) {
                Aceptar(0);
            } else {
                CrearVentana(m.valor, lstBuscaProv);
            }
        }

        function CrearVentana(valor,lista) {
            $.fn.Ventana({
                id: "vntBuscaProveedor",
                titulo: "Buscar Proveedor",
                tamano: "lg"
            });

            var html = '<div class="row"><div class="col-xs-12 col-sm-9 col-md-12 col-lg-12"><div class="form-inline ng-pristine ng-valid"><input id="txtBuscar" type="text" onclick="this.select();" class="form-control " placeholder="Buscar cliente..." tabindex="1" style="border-bottom-width: 1px;margin-bottom: 10px;width: 70%;"><button id="btnBuscar" type="button" style="border-bottom-width: 1px;margin-left: 5px;margin-bottom: 10px;" class="btn btn-sm btn-default"><span class="glyphicon glyphicon-search"></span></button></div></div><div class="row"><div id="' + 'cntBuscarProveedor' + '" class="col-xs-12 col-sm-12 col-md-12 col-lg-12"></div></div><div class="row"><div class="col-md-3 col-md-offset-9 text-right"><button id="btnProvAceptar" type="submit" class="btn btn-sm btn-primary m-r-5">Aceptar</button><button id="btnProvCancelar" type="submit" data-dismiss="modal" aria-hidden="true" class="btn btn-sm btn-default">Cancelar</button></div></div>';
            $("#vntBuscaProveedor .panel-body").html(html);

            var focus = function () { $(buscar_Id).focus().select(); };
            setTimeout(focus(), 500);
            CrearTabla(lista);
            $("#txtBuscar").val(valor);
            $('#vntBuscaProveedor').modal('show');
        }

        function Aceptar(index) {
            if (index == -1) {
                $.fn.Mensaje({ mensaje: "No se ha seleccionado ninguna proveedor", tamano: "sm" });
                $(buscar_Id).focus();
            } else {
                m["funcionAceptar"](lstBuscaProv[index]);
            }
        }

        $(btnAceptar).bind("click", function ()
        {
            var index = $(tabla_Id).find("tr.seleccionado").index();
            Aceptar(index);
        });

        $("#btnProvCancelar").bind("click", function ()
        {
            m["funcionCancelar"]();
        });

        $("#btnBuscar").bind("click", function () { Buscar(tablaId, btnAceptar); });
        
        $(buscar_Id).bind("keydown", function (e)
        {
            if (e.which == 40 || e.which == 38 || e.which == 13) { e.preventDefault(); }
        });

        $(buscar_Id).bind("keyup", function (e)
        {
            if (e.which == 40)
            {
                e.preventDefault();
                if ($(tabla_Id).find("tbody tr").length == 0) return false;
                var s = 0
                s = $(tabla_Id).find("tr.seleccionado").index();
                if (s == -1)
                {
                    $(tabla_Id + " tbody tr").eq(0).addClass("seleccionado");
                    //$(buscar_Id).attr("value", $(tabla_Id).find("tr").eq(1).find("td").eq(0).html()).focus();
                    $(buscar_Id).val($(tabla_Id + " tbody tr").eq(0).find("td").eq(0).html()).focus();
                } else if (s < ($(tabla_Id + " tbody tr").length - 1))
                {
                    $(tabla_Id + " tbody").find("tr.seleccionado").removeClass("seleccionado");
                    $(tabla_Id + " tbody").find("tr").eq(s + 1).addClass("seleccionado");
                    //$(buscar_Id).attr("value", $(tabla_Id).find("tr").eq(s + 1).find("td").eq(0).html()).focus();
                    $(buscar_Id).val($(tabla_Id).find("tbody tr").eq(s + 1).find("td").eq(0).html()).focus();
                }
                a = $(tabla_Id).find("tr.seleccionado").index();
                if (a > 3)
                {
                    dif = (a - 3) * 30;
                    $(tabla_Id).parent().animate({ scrollTop: dif }, 100);
                }
            }
            else if (e.which == 38)
            {
                e.preventDefault();
                if ($(tabla_Id).find("tbody tr").length == 0) return false;
                var s = 0;
                s = $(tabla_Id).find("tr.seleccionado").index();
                if (s > 0)
                {
                    $(tabla_Id).find("tr.seleccionado").removeClass("seleccionado");
                    $(tabla_Id).find("tbody tr").eq(s - 1).addClass("seleccionado");
                    //$(buscar_Id).attr("value", $(tabla_Id).find("tr").eq(s - 1).find("td").eq(0).html()).focus();
                    $(buscar_Id).val($(tabla_Id).find("tbody tr").eq(s - 1).find("td").eq(0).html()).focus();
                };
                var ftop = $(".seleccionado").offset().top - 12;
                var dtop = $(tabla_Id).parent().offset().top;
                if (ftop < dtop)
                {
                    a = $(tabla_Id).find("tr.seleccionado").index();
                    dif = a * 28;
                    $(tabla_Id).parent().animate({ scrollTop: dif }, 100);
                }

            }
            else if (e.which == 13)
            {
                Buscar(tablaId, btnAceptar);
            }
            else
            {
                $("tr.seleccionado").removeClass("seleccionado");
            }
        });
    }


    function Buscar(tablaId, btnAceptar) {
        if ($("#" + tablaId).find("tr.seleccionado").index() == -1) {
            var valor = $("#txtBuscar").val();
            BuscarClientes(valor);
            CrearTabla(lstBuscaProv);
        }
        else {
            $(btnAceptar).click();
        }
    }

    function BuscarClientes(valor) {
        var cProvRUC, cNombre;
        isNaN(valor) ? cNombre = valor : cProvRUC = valor;
                $.fn.Conexion({
                    direccion: '/Cliente/BuscarProveedores',
                    datos: { "cProvRUC": cProvRUC, "cNombre": cNombre },
                    async: false,
                    terminado: function (data) {
                        lstBuscaProv = JSON.parse(data);
                    },
                    error: function (v, s) { $.fn.Mensaje({ mensaje: "Realizar la busqueda nuevamente" }); }
                });
    }

    function CrearTabla(lista) {
        $("#cntBuscarProveedor").Tabla({
            tblId: "tblProveedor",
            scrollVertical: "Si",
            cantRegVertical: 5,
            cabecera: "Nombre,DOI,Tel&eacute;fono,Direcci&oacute;n",
            campos: "nom,doi,tel1,direc",
            empty: "El proveedor no se encuentra registrado",
            datos: lista
        });
        IniciaTabla();
    }

    function IniciaTabla() {
        if (lstBuscaProv.length == 0) {
            $("#btnProvAceptar").addClass("disabled");
        } else {
            $("#btnProvAceptar").removeClass("disabled");
            $("#tblProveedor").parent().scrollTop(0);

            $("#tblProveedor tbody tr").bind({
                "dblclick": function () {
                    $("#btnProvAceptar").click();
                }
            });

            $(".seleccionado").bind("click", function () {
                $("#btnProvAceptar").click();
            });
        }
    }


})(jQuery);