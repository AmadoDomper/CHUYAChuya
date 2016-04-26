(function ($)
{
    var lstBuscaProd = [];
    $.fn.BuscarProducto = function (m)
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
        m.durante = m.durante || function () { };
        //m["terminado"] = m["terminado"] || function () { };
        m.error = m.error || function () { };

        //Crea una nueva venta
        $.fn.Ventana({
            id: "vntBuscaProducto",
            titulo: "Buscar Producto",
            tamano: "lg"
        });

        //Contenido Estatico

        //var html = '<div class="row"><div class="col-xs-12 col-sm-3 col-md-3 col-lg-3"><div class="row"><div class="col-xs-7 col-sm-12 col-md-12 col-lg-12"><div class="row"><div class="panel panel-cmacm"><div class="panel-heading">Buscar Por</div><div class="panel-body"><div class="col-xs-12 col-sm-12 col-md-12 col-lg-12"><label class="manito"><input id="rbtApellido" name="rad" type="radio" value="1" checked="checked" />Apellido</label></div><div class="col-xs-12 col-sm-12 col-md-12 col-lg-12"><label class="manito"><input id="rbtCodigo" name="rad" value="2" type="radio" />C\u00F3digo</label></div><div class="col-xs-12 col-sm-12 col-md-12 col-lg-12"><label class="manito"><input id="rbtDoc" name="rad" value="3" type="radio" />Documento</label></div></div></div></div></div><div class="col-xs-5 col-sm-12 col-md-12 col-lg-12"><div class="row"><div class="col-xs-12 col-sm-8 col-md-8 col-lg-8"><input id="btnProdAceptar" type="button" class="btn btn-cmacm btn-block" value="Aceptar" /></div><div class="col-xs-12 col-sm-8 col-md-8 col-lg-8"><button id="btnProdCancelar" type="button" class="btn btn-cmacm btn-block" data-dismiss="modal" aria-hidden="true">Cancelar</button></div></div></div></div></div><div class="col-xs-12 col-sm-9 col-md-9 col-lg-9"><div class="row"><div class="col-xs-12 col-sm-9 col-md-9 col-lg-9">Ingrese Dato a Buscar:</div></div><div class="row"><div class="col-xs-12 col-sm-9 col-md-9 col-lg-9"><input id="txtBuscar" type="text" class="form-control" tabindex="1" /></div></div><div class="row"><div id="' + 'cntBuscarProducto' + '" class="col-xs-12 col-sm-12 col-md-12 col-lg-12"></div></div></div></div>';
        var html = '<div class="row"><div class="col-xs-12 col-sm-9 col-md-12 col-lg-12"><div class="form-inline ng-pristine ng-valid"><input id="txtBuscar" type="text" onclick="this.select();" class="form-control " placeholder="Buscar producto..." tabindex="1" style="border-bottom-width: 1px;margin-bottom: 10px;width: 70%;"><button id="btnBuscar" type="button" style="border-bottom-width: 1px;margin-left: 5px;margin-bottom: 10px;" class="btn btn-sm btn-default"><span class="glyphicon glyphicon-search"></span></button></div></div><div class="row"><div id="' + 'cntBuscarProducto' + '" class="col-xs-12 col-sm-12 col-md-12 col-lg-12"></div></div><div class="row"><div class="col-md-3 col-md-offset-9 text-right"><button id="btnProdAceptar" type="submit" class="btn btn-sm btn-primary m-r-5">Aceptar</button><button id="btnProdCancelar" type="submit" data-dismiss="modal" aria-hidden="true" class="btn btn-sm btn-default">Cancelar</button></div></div>'

        var tablaId = "tblProductos";
        var btnAceptar = "#vntBuscaProducto #btnProdAceptar";
        var tabla_Id = "#" + tablaId;
        var buscarId = "txtBuscar";
        var buscar_Id = "#" + buscarId;

        $("#vntBuscaProducto .panel-body").html(html);
        var focus = function () { $(buscar_Id).focus().select(); };
        setTimeout(focus(), 500);

        $("#cntBuscarProducto").Tabla({
            tblId: tablaId,
            cabecera: "Cod&iacute;go,Descripci&oacute;n,Precio Unit,Medida,Lavado,Secado,Planchado",
            campos: "nPrId,cPrDesc,nPrPrecU,oPrMed.nom,bPrSerLav,bPrSerSec,bPrSerPla",
            scrollVertical: "Si",
            cantRegVertical: 5
        });
        
        $("#vntBuscaProducto input:radio").bind("click", function ()
        {
            focus();
        });

        $(btnAceptar).bind("click", function ()
        {
            var index = $(tabla_Id).find("tr.seleccionado").index();


            if (index == -1)
            {
                $.fn.Mensaje({ mensaje: "No se ha seleccionado ningún Producto", tamano: "sm" });
                $(buscar_Id).focus();
            } else
            {
                m["funcionAceptar"](lstBuscaProd[index]);
                $('#txtCant').focus();
            }
        });

        $("#btnProdCancelar").bind("click", function ()
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
            var cProdId,cNombre;
            var valor = $("#txtBuscar").val();

            isNaN(valor) ? cNombre = valor : cProdId = valor;

            $.fn.Conexion({
                direccion: '/Producto/BuscarProductos',
                datos: { "nProdId": cProdId, "cProdDesc": cNombre },
                terminado: function (data) {
                    lstBuscaProd = JSON.parse(data);
                    $(".seleccionado").remove();

                    $("#cntBuscarProducto").Tabla({
                        tblId: tablaId,
                        scrollVertical: "Si",
                        cantRegVertical: 5,
                        cabecera: "Cod&iacute;go,Descripci&oacute;n,Precio Unit,Medida,Lavado,Secado,Planchado",
                        campos: "nPrId,cPrDesc,nPrPrecU,oPrMed.nom,bPrSerLav,bPrSerSec,bPrSerPla",
                        datos: lstBuscaProd
                    });

                    if (lstBuscaProd.length == 0) {
                        $("#btnProdAceptar").addClass("disabled");
                    } else {
                        $("#btnProdAceptar").removeClass("disabled");
                        $("#" + tablaId).parent().scrollTop(0);

                        $("#" + tablaId + " tbody tr").bind({
                            "dblclick": function () {
                                $(btnAceptar).click();
                            }
                        });

                        $(".seleccionado").bind("click", function () {
                            $(btnAceptar).click();
                        });
                    }
                },
                error: function (v, s) { $.fn.Mensaje({ mensaje: "Realizar la busqueda nuevamente o informar al departamento de TI" }); }
            });
        }
        else {
            $(btnAceptar).click();
        }
    }

})(jQuery);