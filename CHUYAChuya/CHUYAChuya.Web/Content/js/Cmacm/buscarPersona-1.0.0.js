(function ($)
{
    $.fn.BuscarPersona = function (m)
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
            id: "vntBuscaPersona",
            titulo: "Buscar Persona",
            tamano: "lg"
        });


        //Contenido Estatico

  var html = '<div class="row"><div class="col-xs-12 col-sm-3 col-md-3 col-lg-3"><div class="row"><div class="col-xs-7 col-sm-12 col-md-12 col-lg-12"><div class="row"><div class="panel panel-cmacm"><div class="panel-heading">Buscar Por</div><div class="panel-body"><div class="col-xs-12 col-sm-12 col-md-12 col-lg-12"><label class="manito"><input id="rbtApellido" name="rad" type="radio" value="1" checked="checked" />Apellido</label></div><div class="col-xs-12 col-sm-12 col-md-12 col-lg-12"><label class="manito"><input id="rbtCodigo" name="rad" value="2" type="radio" />C\u00F3digo</label></div><div class="col-xs-12 col-sm-12 col-md-12 col-lg-12"><label class="manito"><input id="rbtDoc" name="rad" value="3" type="radio" />Documento</label></div></div></div></div></div><div class="col-xs-5 col-sm-12 col-md-12 col-lg-12"><div class="row"><div class="col-xs-12 col-sm-8 col-md-8 col-lg-8"><input id="btnPersAceptar" type="button" class="btn btn-cmacm btn-block" value="Aceptar" /></div><div class="col-xs-12 col-sm-8 col-md-8 col-lg-8"><button id="btnPersCancelar" type="button" class="btn btn-cmacm btn-block" data-dismiss="modal" aria-hidden="true">Cancelar</button></div></div></div></div></div><div class="col-xs-12 col-sm-9 col-md-9 col-lg-9"><div class="row"><div class="col-xs-12 col-sm-9 col-md-9 col-lg-9">Ingrese Dato a Buscar:</div></div><div class="row"><div class="col-xs-12 col-sm-9 col-md-9 col-lg-9"><input id="txtBuscar" type="text" class="form-control" tabindex="1" /></div></div><div class="row"><div id="' + 'cntBuscarPersona' + '" class="col-xs-12 col-sm-12 col-md-12 col-lg-12"></div></div></div></div>';

        var tablaId = "tblPersonas";
        var btnAceptar = "#vntBuscaPersona #btnPersAceptar";
        var tabla_Id = "#" + tablaId;
        var buscarId = "txtBuscar";
        var buscar_Id = "#" + buscarId;
        var lstBuscaPers = [];

        $("#vntBuscaPersona .panel-body").html(html);
        var focus = function () { $(buscar_Id).focus().select(); };
        setTimeout(focus(), 500);

        $("#cntBuscarPersona").Tabla({
            tblId: tablaId,
            cabecera: "Nombre Cliente, Direccion, C\u00F3digo,DNI,RUC",
            campos: "nom,direc,id,lstdoc[0].num,lstdoc[1].num",
            scrollVertical: "Si",
            cantRegVertical: 5
        });
        
        $("#vntBuscaPersona input:radio").bind("click", function ()
        {
            focus();
        });

        $(btnAceptar).bind("click", function ()
        {
            var index = $(tabla_Id).find("tr.seleccionado").index();


            if (index == -1)
            {
                $.fn.Mensaje({ mensaje: "No se ha seleccionado ninguna persona", tamano: "sm" });
                $(buscar_Id).focus();
            } else
            {
                var men = {};
                $.fn.Conexion({
                    direccion: '/Persona/ValidaAceptarBuscarPersona',
                    datos: {"oPersona" : JSON.stringify(lstBuscaPers[index]) },
                    terminado: function (data)
                    {
                        men = JSON.parse(data);
                        if (men.men == null)
                        {
                            m["funcionAceptar"](lstBuscaPers[index]);
                        } else
                        {
                            $.fn.Mensaje({ mensaje: men.men, tipo: men.tipo });
                        }
                    }
                });
            }
        });

        $("#btnPersCancelar").bind("click", function ()
        {
            m["funcionCancelar"]();
        });
        
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
                if ($("#"+tablaId).find("tr.seleccionado").index() == -1)
                {
                    var nTipoBusqueda = $('#vntBuscaPersona input:radio:checked').val();
                    var cValor = $(this).val();

                    $.fn.Conexion({
                        direccion: '/Persona/BuscarPersonas',
                        datos: { "nTipoBusqueda": nTipoBusqueda, "cValor": cValor },
                        terminado: function (data)
                        {
                            lstBuscaPers = JSON.parse(data).lstPersonas;
                            $(".seleccionado").remove();

                            $("#cntBuscarPersona").Tabla({
                                tblId: tablaId,
                                scrollVertical: "Si",
                                cantRegVertical: 5,
                                cabecera: "Nombre Cliente, Direccion, C\u00F3digo,DNI,RUC",
                                campos: "nom,direc,id,lstdoc[0].num,lstdoc[1].num",
                                datos: lstBuscaPers
                            });

                            $(tabla_Id).parent().scrollTop(0);

                            $(tabla_Id + " tbody tr").bind({
                                "dblclick": function ()
                                {
                                    $(btnAceptar).click();
                                }
                                //,
                                //"mouseenter": function ()
                                //{
                                //    $("tr.seleccionado").removeClass("seleccionado");
                                //    $(this).addClass("seleccionado");
                                //}
                            });

                            $(".seleccionado").bind("click", function ()
                            {
                                $(btnAceptar).click();
                            });
                        },
                        error: function (v, s) { $.fn.Mensaje({ mensaje: "Realizar la busqueda nuevamente o informar al departamento de TI"}); }
                    });
                }
                else
                {
                    $(btnAceptar).click();
                }
            }
            else
            {
                $("tr.seleccionado").removeClass("seleccionado");
            }
        });
    }
})(jQuery);