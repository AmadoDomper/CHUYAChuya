function VerificaSubProd(idsubprod, idcasa, cntvendedor, tipo) {
    idcasa = idcasa || "";
    cntvendedor = cntvendedor || "";
    tipo = tipo || 0;

    var valor = $(idsubprod + " option:selected").val();
    if (cntvendedor != "") { $(cntvendedor).addClass('deshabilitado'); }

    if (tipo == 1) {
        if (typeof (datos.lstRelac) != "undefined") {
            DefineCondicionCredProd(datos.lstRelac[0].codigo, "#txtCondicion");
            DefineCondicionCredProd(datos.lstRelac[0].codigo, "#txtCondicionProd", true);
        }
        if (valor.substring(0, 1) != "6") { datos.oagro = null; }
    }
    switch (valor) {
        case "510":
        case "511":
        case "706":
            if ($(idcasa + " option:selected").val() > 0) { $(cntvendedor).removeClass('deshabilitado'); }
            break;
        case "512":
        case "704":
        case "805":
            if (tipo == 1) { MostarVentanaConvenio(); }
            break;
        case "601":
        case "602":
            if (tipo == 1) { MostarVentanaAgropecuario(valor); }
            break;
    }
}

function CargaSolicitudCred(e, idcaja, idage, idprod, idcuenta) {
    tecla = (document.all) ? e.keyCode : e.which;
    if (tecla == 13) {
        var caja = $(idcaja).val();
        var age = $(idage).val();
        var prod = $(idprod).val();
        var cuenta = $(idcuenta).val();
        var nrocuenta = caja + age + prod + cuenta;
        if (nrocuenta.length == 18) {
            DatosCredito(nrocuenta);
        } else {
            $.fn.Mensaje({ mensaje: ':( Numero de Cuenta Incorrecto', tamano: "sm" });
        }
    } else {
        return val_09(e);
    }
}

function DatosCredito(nrocuenta) {
    $.fn.Conexion({
        direccion: '/Credito/CargoDatosSolicitud',
        datos: { "codcta": nrocuenta },
        terminado: function (data) {
            var cred = JSON.parse(data);
            if (cred.col == null) {
                $.fn.Mensaje({ mensaje: ':( No Hay Datos', tamano: "sm" });
            } else {
                numcred = nrocuenta;
                var indicecon = -1;
                var fech = new Date(cred.lsthist[0].fecha);

                indicecon = ObtenerIndiceLista(datos.lstCondicion, cred.cond);
                $("#txtCondicion").val(datos.lstCondicion[indicecon].nom);
                $("#txtCondicionId").val(datos.lstCondicion[indicecon].id);

                indicecon = ObtenerIndiceLista(datos.lstCondicion, cred.condprod);
                $("#txtCondicionProd").val(datos.lstCondicion[indicecon].nom);
                $("#txtCondicionProdId").val(datos.lstCondicion[indicecon].id);

                $("#cmbCampana").val(cred.camp);
                CrearSubLista("#cmbCampana", "#cmbCasaComercial", datos.lstCampana);
                $("#cmbCasaComercial").val(cred.casa);
                $("#txtVendedor").val(cred.vend);
                $("#cmbProducto").val(+(cred.col.tpoprod.id.toString().substring(0, 1) + '00'));
                CrearSubLista("#cmbProducto", "#cmbSubProducto", datos.lstProducto);
                $("#cmbSubProducto").val(cred.col.tpoprod.id);
                VerificaSubProd("#cmbSubProducto", "#cmbCasaComercial", "#cntVendedor");
                $("#cmbMoneda").val(+nrocuenta.substring(8, 9));
                $("#txtMontoSol").val(number_format(cred.lsthist[0].monto, 2));
                $("#txtCuota").val(cred.lsthist[0].cuotas);
                $("#txtPlazo").val(cred.lsthist[0].plazo);
                $("#cmbDestino").val(cred.oColocDestino.ConstanteID);
                $("#txtFecha").val(formatDate(fech, "DD/MM/YYYY"));
                $("#cmbAnalistas").val(cred.analista.id);
                $("#cmbPromotores").val(cred.promotor.id);
                $("#txtCodCliente").val(cred.col.prod.lstprodpers[0].pers.id);
                $("#txtNomCliente").val(cred.col.prod.lstprodpers[0].pers.nom);
                $("#txtDNI").val(cred.col.prod.lstprodpers[0].pers.lstdoc[0].num);
                $("#txtRUC").val(cred.col.prod.lstprodpers[0].pers.lstdoc[1].num);

                $("#tblRelacionesGen tbody tr").remove();

                var lstpp = cred.col.prod.lstprodpers;
                var lstRelac = [];
                var oEnvioEstCta = {};
                oEnvioEstCta.tpoenvio = cred.col.prod.tpoenvio;
                oEnvioEstCta.lstPers = [];

                for (i in lstpp) {

                    var idpers = lstpp[i].pers.id;
                    var idrelac = lstpp[i].relac.id;
                    var env = lstpp[i].envio;

                    var obj = { nombre: lstpp[i].pers.nom, codigo: idpers, crelacion: lstpp[i].relac.nom, nrelacion: idrelac, cdoc: lstpp[i].pers.lstdoc[0].num, cruc: lstpp[i].pers.lstdoc[1].num, direc: lstpp[i].pers.direc, sexo: lstpp[i].pers.sexo };

                    if (env) {
                        var objenv = { indice: i, direc: lstpp[i].pers.direc, envio: true };
                        oEnvioEstCta.lstPers = oEnvioEstCta.lstPers.concat([objenv]);
                    }

                    lstRelac = lstRelac.concat([obj]);
                    $("#tblRelacionesGen").append("<tr><td><input type='hidden' data-val='true' name='Cred.oColocaciones.oProducto.ListaProductoPersona[" + i + "].oPersona.cPersCod' value='" + idpers + "' />" + lstpp[i].pers.nom + "</td><td><input type='hidden' data-val='true' name='Cred.oColocaciones.oProducto.ListaProductoPersona[" + i + "].oPrdPersRelac.ConstanteID' value='" + idrelac + "'/>" + lstpp[i].relac.nom + "</td><td>" + idpers + "</td></tr>");
                }
                datos.lstRelac = lstRelac;
                datos.oEnvioEstCta = oEnvioEstCta;

                var oConvenio = {
                    ccodinst: cred.convenio.inst.id,
                    ccodmod: cred.convenio.codmodular,
                    carben: cred.convenio.carben,
                    cargo: cred.convenio.cargo,
                    plan: cred.convenio.plani
                };
                datos.oConvenio = oConvenio;

                if (cred.agrico.tipo.id != 0) {
                    var obj = { tipo: cred.agrico.tipo.id, subtipo: cred.agrico.subtipo.id, totalhect: cred.agrico.totalhect, hectprod: cred.agrico.hectprod, codcop: cred.agrico.coopera.id, nomcop: cred.agrico.coopera.nom, ani: cred.agrico.animal }
                    datos.oagro = obj;
                }

            }

        },
        error: function () { numcred = ""; },
        bloqueo: true
    });
}

function MostarVentanaRelaciones() {
    var lstRelac = !datos.lstRelac ? [] : datos.lstRelac.slice(0);

    var cerrar = function () {
        if (!datos.lstRelac) {
            $("#cmbCampana,#txtCondicion,#cmbCasaComercial,#txtVendedor,#txtCodCliente,#txtNomCliente,#txtDNI,#txtRUC,#cmbProducto,#cmbSubProducto,#cmbMoneda,#cmbDestino,#txtFecha,#cmbAnalistas,#cmbPromotores,#txtCondicionProd").attr("disabled", "disabled").val(""); /*Se repirte*/
            $("#txtMontoSol,#txtCuota,#txtPlazo").val(0);
            $("#tblRelacionesGen tbody").remove();
            $("#txtCodProd,#txtCodCuenta").val("");

            $("#cntNuevoGen,#btnNuevoGen").show();
            $("#cntVendedor,#cntGrabarGen,#btnGrabarGen").hide();
        } else {
            $("#txtCondicion,#txtVendedor,#cmbCampana,#cmbCasaComercial,#txtCodCliente,#txtNomCliente,#txtDNI,#txtRUC,#btnRelaciones,#btnEnvioEstCta,#cmbProducto,#cmbSubProducto,#cmbMoneda,#txtMontoSol,#txtCuota,#txtPlazo,#cmbDestino,#cmbAnalistas,#txtFecha,#cmbPromotores,#txtCondicionProd,#btnGrabarGen,#btnCancelarGen").removeAttr("disabled", "disabled"); /*Se repite*/

            $("#btnEditarGen,#btnNuevoGen,#cntNuevoGen,#cntEditarGen").hide(); /*Se repirte*/
            $("#btnGrabarGen,#cntGrabarGen,#btnCancelarGen,#cntCancelaGen").show(); /*Se repirte*/
            $("#btnExaminar").attr("disabled", "disabled");
            $("#txtCodAge,#txtCodProd,#txtCodCuenta").attr("disabled", "disabled");

        }
    }

    $.fn.Ventana({
        id: "vntRelaciones",
        titulo: "Personas del Credito",
        tamano: "lg",
        funcionCerrar: cerrar
    });

    $("#vntRelaciones .panel-body").html('<div class="row"><div class="panel panel-cmacm"><div class="panel-heading">Datos del Cliente</div><div class="panel-body"><div class="col-xs-12 col-sm-12 col-md-12 col-lg-12"><div class="row"><div class="col-xs-12 col-sm-5 col-md-5 col-lg-5"><div class="row"><div class="col-xs-5 col-sm-5 col-md-5 col-lg-5">Cliente:</div><div class="col-xs-7 col-sm-7 col-md-7 col-lg-7"><input id="txtCliente" type="text" class="form-control" readonly="true" /></div></div></div><div class="hidden-xs col-sm-7 col-md-7 col-lg-7"><div class="row"><div class="col-xs-12 col-sm-12 col-md-12 col-lg-12"><input id="txtNombre" type="text" class="form-control" readonly="true" /></div></div></div></div><div class="row"><div class="col-xs-12 col-sm-5 col-md-5 col-lg-5"><div class="row"><div class="col-xs-5 col-sm-5 col-md-5 col-lg-5">' + "Doc. de Identidad:" + '</div><div class="col-xs-7 col-sm-7 col-md-7 col-lg-7"><input id="txtDoc" type="text" class="form-control" readonly="true" /></div></div></div><div class="col-xs-12 col-sm-7 col-md-7 col-lg-7"><div class="row"><div class="col-xs-5 col-sm-5 col-md-4 col-lg-4">RUC:</div><div class="col-xs-7 col-sm-7 col-md-8 col-lg-8"><input id="txtRuc" type="text" class="form-control" readonly="true" /></div></div></div></div><div class="row"><div class="col-xs-12 col-sm-5 col-md-5 col-lg-5"><div class="row"><div class="col-xs-5 col-sm-5 col-md-5 col-lg-5">Relacion:</div><div class="col-xs-7 col-sm-7 col-md-7 col-lg-7"><select id="cmbRelacion" class="form-control manito"></select></div></div></div></div><div class="row"><div class="col-xs-12 col-sm-12 col-md-12 col-lg-12"><strong>Relaciones</strong></div></div><div class="row"><div id="cntRelaciones" class="col-xs-12 col-sm-10 col-md-10 col-lg-10"></div><div class="col-xs-12 col-sm-2 col-md-2 col-lg-2"><div class="row"><fieldset><legend></legend><div class="col-xs-6 col-sm-12 col-md-12 col-lg-12"><input id="btnAceptar" type="button" class="btn btn-cmacm btn-block" value="Aceptar" /><input id="btnAceptarEdi" type="button" class="btn btn-cmacm btn-block" value="Aceptar" style="display:none;" /></div><div class="col-xs-6 col-sm-12 col-md-12 col-lg-12"><input id="btnCancelar" type="button" class="btn btn-cmacm btn-block" value="Cancelar"/></div><div class="col-xs-6 col-sm-12 col-md-12 col-lg-12"><button id="btnAgregar" type="button" class="btn btn-cmacm btn-block" data-toggle="modal" data-target="#vntBuscaPersona">Agregar</button></div><div class="col-xs-6 col-sm-12 col-md-12 col-lg-12"><input id="btnEditar" type="button" class="btn btn-cmacm btn-block" value="Editar" /></div><div class="col-xs-6 col-sm-12 col-md-12 col-lg-12"><input id="btnEliminar" type="button" class="btn btn-cmacm btn-block" value="Eliminar" /></div></fieldset></div></div></div></div></div></div></div><div class="row"><div class="col-xs-12 col-sm-12 col-md-12 col-lg-12"><div class="row"><div class="col-xs-6 col-sm-2 col-md-2 col-lg-2"><input id="btnGrabar" type="button" class="btn btn-cmacm btn-block" value="Grabar"/></div><div class="col-xs-6 col-sm-2 col-md-2 col-lg-2"><button id="btnSalir" type="button" class="btn btn-cmacm btn-block" data-dismiss="modal" aria-hidden="true">Salir</button></div></div></div></div>');

    $("#cntRelaciones").Tabla({
        tblId: "tblRelaciones",
        cabecera: "Nombre Cliente, Relaci&oacute;n, C&oacute;digo",
        scrollVertical: "Si",
        campos: "nombre,crelacion,codigo",
        datos: datos.lstRelac
    });

    CrearLista("#cmbRelacion", datos.lstRelaciones);




    vntRelaciones - btnCancelar
    var verificaDatos = function () {
        $("#btnAceptar,#btnCancelar,#btnAceptarEdi").hide();
        if ($("#tblRelaciones tbody tr").length == 0) {
            $("#btnGrabar, #btnEditar, #btnEliminar").addClass("disabled");
            $("#btnAceptar,#btnCancelar,#btnAceptarEdi").hide();
        } else {
            $("#btnGrabar, #btnEditar, #btnEliminar").removeClass("disabled");
        }
    }

    verificaDatos();
    $("#btnCancelar").bind("click", function () {
        $("#btnAgregar,#btnEditar,#btnEliminar").show();
        $("#btnAceptar,#btnCancelar,#btnAceptarEdi").hide();
        $("#txtCliente,#txtDoc,#txtNombre,#txtRuc").val("");

        $("#btnGrabar,#btnSalir").removeClass("disabled");
        verificaDatos();

        if ($("#tblRelaciones tbody tr").length == 0) {
            $("#btnGrabar, #btnEditar, #btnEliminar").addClass("disabled");
        } else {
            $("#btnGrabar, #btnEditar, #btnEliminar").removeClass("disabled");
        }
        $("#tblRelaciones").attr("data-edit", "false");
    });

    var persSelec = {}
    $("#btnAgregar").bind("click", function () {
        var aceptar = function (d) {
            persSelec = d;
            $("#txtNombre").val(d.nom);
            $("#txtCliente").val(d.id);
            $("#txtDoc").val(d.lstdoc[0].num);
            $("#txtRuc").val(d.lstdoc[1].num);

            $("#btnEditar,#btnAgregar,#btnEliminar").hide();
            $("#btnCancelar,#btnAceptar").show();
            $('#vntBuscaPersona').modal('hide');
        };

        var cancelar = function () {
            $("#btnCancelar").click();
        }
        $.fn.BuscarPersona({
            funcionAceptar: aceptar,
            funcionCancelar: cancelar
        });
    });

    $("#btnAceptar").bind("click", function () {
        var cPersCod = $('#txtCliente').val();
        var cPersNombre = $('#txtNombre').val();
        var sel = $("#cmbRelacion option:selected");
        var nPrdPersRelac = sel.val();

        if (ValidaRelacion(nPrdPersRelac, cPersCod, persSelec.sexo, cPersNombre))
        {
            ValidaRelacionBD(cPersCod, cPersNombre, nPrdPersRelac);
        }
    });

    function AgregarRelacion()
    {
        var cPersCod = $('#txtCliente').val();
        var cPersNombre = $('#txtNombre').val();
        var cdoc = $("#txtDoc").val();
        var cruc = $("#txtRuc").val();
        var sel = $("#cmbRelacion option:selected");
        var nPrdPersRelac = sel.val();
        var cPrdPersRelac = sel.text();

            var obj = { nombre: cPersNombre, codigo: cPersCod, crelacion: cPrdPersRelac, nrelacion: nPrdPersRelac, cdoc: cdoc, cruc: cruc, direc: persSelec.direc, sexo: persSelec.sexo };

            lstRelac = lstRelac.concat([obj]);

            $("#cntRelaciones").Tabla({
                tblId: "tblRelaciones",
                cabecera: "Nombre Cliente, Relaci&oacute;n, C&oacute;digo",
                campos: "nombre,crelacion,codigo",
                scrollVertical: "Si",
                datos: lstRelac
            });

            $("#btnAgregar,#btnEditar,#btnEliminar").show().removeClass("disabled");
            $("#btnAceptar,#btnCancelar,#btnAceptarEdi").hide();
            $("#txtCliente,#txtDoc,#txtNombre,#txtRuc").val("");
            verificaDatos();      
    }



    $("#btnEditar").bind("click", function () {
        var index = $("#tblRelaciones tr.seleccionado").index();

        if (index == -1)
        {
            $.fn.Mensaje({ mensaje: "Debe seleccionar al Cliente para editarlo" });
            return false;
        } else if (lstRelac[index].nrelacion == "20")
        {
            $.fn.Mensaje({ mensaje: "No se puede editar la relación del titular" });
            return false;
        }
        else {
            $("#txtCliente").val(lstRelac[index].codigo);
            $("#txtNombre").val(lstRelac[index].nombre);
            $("#txtDoc").val(lstRelac[index].cdoc);
            $("#txtRuc").val(lstRelac[index].cruc);
            $("#cmbRelacion").val(lstRelac[index].nrelacion);

            $("#btnEditar,#btnAgregar,#btnEliminar").hide();
            $("#btnCancelar,#btnAceptarEdi").show();
            $("#btnGrabar, #btnSalir").addClass("disabled");
            $("#tblRelaciones").attr("data-edit", "true");
        }
    });

    function ValidaRelacion(nrelacion, codigo, sexo, cPersNombre)
    {
        if ($("#cmbRelacion").val() == "")
        {
            $.fn.Mensaje({ mensaje: "Seleccione la relación", tamano: "sm" });
            $("#cmbRelacion").focus();
            return false;
        }

        if (lstRelac.length == 0 && nrelacion != 20)
        {
            $.fn.Mensaje({ mensaje: "Por favor ingrese primero el titular de la Cuenta"});
            $("#cmbRelacion").focus();
            return false;
        }

        for (i in lstRelac) {
            if (lstRelac[i].nrelacion == nrelacion && nrelacion == "20")
            {
                $.fn.Mensaje({ mensaje: "El Titular de la cuenta ya ha sido ingresado" });
                $("#cmbRelacion").focus();
                return false;
            } else if (lstRelac[i].nrelacion == 20 && nrelacion == 21 && lstRelac[i].sexo == sexo)
            {
                $.fn.Mensaje({ mensaje: "No se puede agregar al conyugue porque posee el mismo sexo que el titular"});
                $("#cmbRelacion").focus();
                return false;
            } else if (lstRelac[i].codigo == codigo)
            {
                $.fn.Mensaje({ mensaje: "La persona ya posee una relación con el crédito", tamano: "sm" });
                $("#cmbRelacion").focus();
                return false;
            }
        }
        return true;
    }

    function ValidaRelacionBD(codigo, cPersNombre, nrelacion)
    {
        var men = {};

        $.fn.Conexion({
            direccion: '/Persona/ValidaAgregarPersona',
            datos: { "cPersCod": codigo, "cPersNom": cPersNombre, "nRelacion": nrelacion },
            terminado: function (data, textStatus, jqXHR)
            {
                men = JSON.parse(data);
                if (men.men == null)
                {
                    AgregarRelacion();
                } else
                {
                    $.fn.Mensaje({
                        mensaje: men.men, tipo: men.tipo, funcionAceptar: function (){if (men.valido){AgregarRelacion();}}
                    });
                }
            }
        });

    }



    $("#btnAceptarEdi").bind("click", function () {
        var index = $("#tblRelaciones tr.seleccionado").index();
        var sel = $("#cmbRelacion option:selected");

        if (ValidaRelacion(sel.val(), null, lstRelac[index].sexo)) {
            lstRelac[index].crelacion = sel.text();
            lstRelac[index].nrelacion = sel.val();

            $("#cntRelaciones").Tabla({
                tblId: "tblRelaciones",
                cabecera: "Nombre Cliente, Relaci&oacute;n, C&oacute;digo",
                campos: "nombre,crelacion,codigo",
                scrollVertical: "Si",
                datos: lstRelac
            });

            /*Codigo repetido verificar*/
            $("#btnAgregar,#btnEditar,#btnEliminar").show();
            $("#btnAceptar,#btnCancelar,#btnAceptarEdi").hide();
            $("#txtCliente,#txtDoc,#txtNombre,#txtRuc").val("");
            $("#btnGrabar,#btnSalir").removeClass("disabled");
            $("#tblRelaciones").attr("data-edit", "false");
        }
    });

    $("#btnEliminar").bind("click", function () {
        var index = $("#tblRelaciones tr.seleccionado").index();

        if (index == -1)
        {
            $.fn.Mensaje({ mensaje: "Debe seleccionar al Cliente para eliminarlo", tamano: "sm" });
            return false;
        }
        else if (lstRelac[index].nrelacion == "20" && lstRelac.length > 1)
        {
            $.fn.Mensaje({ mensaje: "No se puede eliminar el titular mientras existan otras personas relacionadas al crédito" });
            $("#cmbRelacion").focus();
            return false;
        }
        else {
            lstRelac.splice(index, 1);

            $("#cntRelaciones").Tabla({
                tblId: "tblRelaciones",
                cabecera: "Nombre Cliente, Relaci&oacute;n, C&oacute;digo",
                campos: "nombre,crelacion,codigo",
                scrollVertical: "Si",
                datos: lstRelac
            });
        }
    });


    $("#btnGrabar").bind("click", function () {
        if (lstRelac.length > 0) {
            $("#txtCodCliente").val(lstRelac[0].codigo);
            $("#txtNomCliente").val(lstRelac[0].nombre);
            $("#txtDNI").val(lstRelac[0].cdoc);
            $("#txtRUC").val(lstRelac[0].cruc);

            $("#tblRelacionesGen tbody tr").remove();
            for (i in lstRelac) {
                var idpers = lstRelac[i].codigo;
                var idrelac = lstRelac[i].nrelacion;
                $("#tblRelacionesGen").append("<tr><td><input type='hidden' data-val='true' name='Cred.oColocaciones.oProducto.ListaProductoPersona[" + i + "].oPersona.cPersCod' value='" + idpers + "' />" + lstRelac[i].nombre + "</td><td><input type='hidden' data-val='true' name='Cred.oColocaciones.oProducto.ListaProductoPersona[" + i + "].oPrdPersRelac.ConstanteID' value='" + idrelac + "'/>" + lstRelac[i].crelacion + "</td><td>" + idpers + "</td></tr>");
            }
            datos.lstRelac = lstRelac.slice(0);

            DefineCondicionCredProd(datos.lstRelac[0].codigo, "#txtCondicion");
            $('#vntRelaciones').modal('hide');
        } else
        {
            $.fn.Mensaje({ mensaje: "No hay relaciones para grabar",tamano: "sm" });
        }
    });
}

function MostarVentanaAgropecuario(tipo) {
    var oagro = datos.oagro == null ? {} : datos.oagro;

    $.fn.Ventana({
        id: "vntAgropecuario",
        titulo: "Actividad Agropecuaria"
    });

    '<div class="panel panel-cmacm"><div class="panel-heading">Detalle de Actividad</div><div id="fldAgroActividad" class="panel-body">'

    '</div></div>'


    $("#vntAgropecuario .panel-body").html('<div class="row"> <div class="col-xs-5 col-sm-4 col-md-3 col-lg-3">Tipo de Actividad: </div> <div class="col-xs-7 col-sm-8 col-md-9 col-lg-9"> <select id="cmbAgroActividad" class="form-control manito"></select> </div> </div> <div class="row deshabilitado" id="cntCoperativaAgro"> <div class="panel panel-cmacm"><div class="panel-heading">Cooperativa</div><div class="panel-body"><div class="hidden-xs col-sm-4 col-md-3 col-lg-3"> <input id="txtAgroCodCoopera" type="text" class="form-control" readonly="true" /> </div> <div class="col-xs-9 col-sm-6 col-md-7 col-lg-7"> <input id="txtAgroNomCoopera" type="text" class="form-control" readonly="true" /> </div> <div class="col-xs-3 col-sm-2 col-md-2 col-lg-2"> <button id="btnAgroBuscarPers" type="button" class="btn btn-cmacm btn-block" data-toggle="modal" data-target="#vntBuscaPersona">...</button></div></div></div></div> <div class="row"><div class="panel panel-cmacm"><div class="panel-heading">Detalle de Actividad</div><div id="fldAgroActividad" class="panel-body"><div class="col-xs-12 col-sm-12 col-md-12 col-lg-12"> <div class="row"> <div class="col-xs-5 col-sm-5 col-md-5 col-lg-5" id="cntAgroSubTipoActividad">Cultivo: </div> <div class="col-xs-7 col-sm-7 col-md-7 col-lg-7"> <select id="cmbAgroSubActividad" class="form-control manito" ></select></div></div> <div class="row"> <div class="col-xs-5 col-sm-5 col-md-5 col-lg-5">N° Total de Hectareas: </div> <div class="col-xs-7 col-sm-7 col-md-7 col-lg-7"> <input id="txtAgroTotalHect" type="text" class="form-control" onkeypress="return val_09(event)"/> </div> </div> <div class="row"> <div class="col-xs-5 col-sm-5 col-md-5 col-lg-5">N° de Hectareas en Producción: </div> <div class="col-xs-7 col-sm-7 col-md-7 col-lg-7"> <input id="txtAgroHectProd" type="text" class="form-control" onkeypress="return val_09(event)"/> </div> </div> <div class="row deshabilitado" id="cntCabezaAni"> <hr /> <div class="col-xs-5 col-sm-5 col-md-5 col-lg-5">N° de Animales(CGV): </div> <div class="col-xs-7 col-sm-7 col-md-7 col-lg-7"> <input id="txtAgroAnimales" type="text" class="form-control" onkeypress="return val_09(event)"/></div></div></div></div></div></div><div class="row"> <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12"> <div class="row"> <div class="col-xs-6 col-sm-2 col-md-2 col-lg-2"> <input id="btnAgroAceptar" type="button" class="btn btn-cmacm btn-block" value="Aceptar" /> </div> <div class="col-xs-6 col-sm-2 col-md-2 col-lg-2"> <button id="btnAgroSalir" type="button" class="btn btn-cmacm btn-block" data-dismiss="modal" aria-hidden="true">Salir</button> </div> </div> </div> </div>');

    if (tipo == "601") { $("#cntCoperativaAgro").addClass('deshabilitado'); } else { $("#cntCoperativaAgro").removeClass('deshabilitado'); }
    CrearLista("#cmbAgroActividad", datos.lstProductosAgri);
    CrearSubLista("#cmbAgroActividad", "#cmbAgroSubActividad", datos.lstProductosAgri, true);

    var CargaDatosAgro = function () {
        if (oagro != "") {
            $("#cmbAgroActividad").val(oagro.tipo);
            $("#cmbAgroActividad").change();
            CrearSubLista("#cmbAgroActividad", "#cmbAgroSubActividad", datos.lstProductosAgri, true);
            $("#cmbAgroSubActividad").val(oagro.subtipo);
            $("#txtAgroTotalHect").val(oagro.totalhect);
            $("#txtAgroHectProd").val(oagro.hectprod);
            $("#txtAgroAnimales").val(oagro.ani);
            $("#txtAgroCodCoopera").val(oagro.codcop);
            $("#txtAgroNomCoopera").val(oagro.nomcop);

        }
    }

    CargaDatosAgro();

    $('#vntAgropecuario').modal('show');

    $("body").on("change", "#cmbAgroActividad", function () {
        if ($(this).val() > 0) { $("#fldAgroActividad legend").text("Detalle de Actividad - " + $("#" + $(this).attr("id") + " option:selected").text()); }
        if ($(this).val() == 2) { $("#cntCabezaAni").removeClass('deshabilitado'); $("#cntAgroSubTipoActividad").html("SubTipo"); } else { $("#cntCabezaAni").addClass('deshabilitado'); $("#cntAgroSubTipoActividad").html("Cultivo"); }
    });

    $("#btnAgroSalir").bind("click", function () {
        $("#cmbSubProducto").val("");
        datos.oagro = null;
    });

    $("#btnAgroBuscarPers").bind("click", function () {
        var aceptar = function (d) {
            $("#txtAgroCodCoopera").val(d.id);
            $("#txtAgroNomCoopera").val(d.nom);
            $('#vntBuscaPersona').modal('hide');
        };

        var cancelar = function () {
            $("#btnCancelar").click();
        }
        $.fn.BuscarPersona({
            funcionAceptar: aceptar,
            funcionCancelar: cancelar
        });
    });

    $("#btnAgroAceptar").bind("click", function () {
        if (ValidaDatos(4)) {
            oagro.tipo = $("#cmbAgroActividad").val();
            oagro.subtipo = $("#cmbAgroSubActividad").val();
            oagro.totalhect = $("#txtAgroTotalHect").val();
            oagro.hectprod = $("#txtAgroHectProd").val();

            if (tipo == "602") {
                oagro.codcop = $("#txtAgroCodCoopera").val();
                oagro.nomcop = $("#txtAgroNomCoopera").val();
            } else {
                oagro.codcop = "";
                oagro.nomcop = "";
            }

            if (oagro.tipo == 1) {
                oagro.ani = 0;
            } else {
                oagro.ani = $("#txtAgroAnimales").val();
            }


            datos.oagro = oagro;
            $('#vntAgropecuario').modal('hide');
        }
    });
}

function MostrarVentanaEnvioEstadoCta() {
    var lstRelac = datos.lstRelac == null ? [] : datos.lstRelac;
    var oEnvioEstCta = datos.oEnvioEstCta == null ? {} : datos.oEnvioEstCta;

    if (lstRelac.length != 0) {

        $.fn.Ventana({
            id: "vntEnvioEstado",
            titulo: "Envío de Estado de Cuenta"
        });

        $("#vntEnvioEstado .panel-body").html('<div class="row"> <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12"> Por  favor  elija  el  medio  por  el  cual  el  cliente  desee  recibir  la  información  relacionada con sus movimientos mensuales (Físico tendrá un costo de S/. 10.00) </div> </div> <br /> <div class="row"> <div class="col-xs-5 col-sm-5 col-md-5 col-lg-5"> Modo de Recepción: </div> <div class="col-xs-7 col-sm-5 col-md-5 col-lg-5"> <select id="cmbEECModoRecep" class="form-control manito"> </select> </div> </div> <br /> <div class="row"> <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" id="ctnEECPersonas"> </div> </div> <div class="row"> <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12"> <div class="row"> <div class="col-xs-6 col-sm-3 col-md-3 col-lg-3"> <input id="btnEECGrabar" type="button" class="btn btn-cmacm btn-block" value="Grabar" /> </div> <div class="col-xs-6 col-sm-3 col-md-3 col-lg-3"> <button id="btnEECCancelar" type="button" class="btn btn-cmacm btn-block" data-dismiss="modal" aria-hidden="true">Cancelar</button> </div> </div> </div> </div>');

        $("#ctnEECPersonas").Tabla({
            tblId: "tblEECPersonas",
            cabecera: "Nombre Cliente,Domicilio, Env&iacute;o",
            scrollVertical: "Si"
        });

        CrearLista("#cmbEECModoRecep", datos.lstModoRecep);
        if (!jQuery.isEmptyObject(oEnvioEstCta)) { $("#cmbEECModoRecep").val(oEnvioEstCta.tpoenvio == 0 ? "" : (+oEnvioEstCta.tpoenvio)); }

        for (i in lstRelac) {
            var checked = "";
            var lectura = 'readonly="readonly"';
            var direcc = lstRelac[i].direc;
            if (!jQuery.isEmptyObject(oEnvioEstCta)) {
                if (typeof (oEnvioEstCta.lstPers[i]) != "undefined")
                { if (oEnvioEstCta.lstPers[i].envio) { checked = 'checked="checked"'; lectura = ''; direcc = oEnvioEstCta.lstPers[i].direc; } }
            }

            $("#tblEECPersonas").append('<tr><td>' + lstRelac[i].nombre + '</td><td><input type="text" class="form-control EECTxt" ' + lectura + ' value="' + direcc + '" /></td><td class="text-center"><input type="checkbox" data-index="' + i + '" class="manito EECChk" ' + checked + ' /></td></tr>');
        }

        $("body").on("change", "#cmbEECModoRecep", function () {

            if ($("#cmbEECModoRecep").val() != 2) {
                $("#tblEECPersonas tbody input.EECChk").prop("checked", "");
                $("#tblEECPersonas tbody input.EECTxt").attr("readonly", "true");
                $("#tblEECPersonas tbody input.EECChk").attr("disabled", "true");
            } else {
                $("#tblEECPersonas tbody input.EECChk").removeAttr("disabled");
            }
        })

        $(".EECChk").bind("click", function () {
            if ($(this).parent().find("input.EECChk:checked").val() == "on") {
                $(this).parent().parent().find("td input.EECTxt").removeAttr("readonly");
            } else {
                $(this).parent().parent().find("td input.EECTxt").attr("readonly", "true");
            }
        })

        $("#btnEECGrabar").bind("click", function () {
            if (ValidaDatos(2)) {
                oEnvioEstCta.tpoenvio = $("#cmbEECModoRecep").val();
                oEnvioEstCta.lstPers = [];
                if (oEnvioEstCta.tpoenvio == 2) {
                    $("#tblEECPersonas tbody tr").each(function (index) {
                        var env = $(this).find("td input.EECChk:checked").val() == "on" ? true : false;
                        if (env) {
                            var dir = $(this).find("td input.EECTxt").val()
                            var ind = $(this).find("td input.EECChk").attr("data-index");
                            var obj = { indice: ind, direc: dir, envio: env }
                            oEnvioEstCta.lstPers = oEnvioEstCta.lstPers.concat([obj]);
                        }
                    })
                }
                datos.oEnvioEstCta = oEnvioEstCta;
                $('#vntEnvioEstado').modal('hide');
            }
        });

        $("#btnEECCancelar").bind("click", function () {
            oEnvioEstCta = {};
        })

    } else {
        $.fn.Ventana({
            id: "vntAlertas",
            titulo: "Aviso",
            tamano: "sm",
            cuerpo: '<h4>No hay personas relacionadas al Crédito.</h4>'
        });
        $('#vntAlertas').modal('show');
    }
}

function MostarVentanaConvenio() {
    var oConvenio = datos.oConvenio == null ? {} : datos.oConvenio;

    $.fn.Ventana({
        id: "vntConvenio",
        titulo: "Ingreso de Datos de Solicitud de Convenio",
        tamano: "lg"
    });


    $("#vntConvenio .panel-body").html('<div class="row"> <div class="col-xs-5 col-sm-3 col-md-2 col-lg-2"> Institucion: </div> <div class="col-xs-7 col-sm-9 col-md-10 col-lg-10"> <select id="cmbInstitucion" class="form-control manito"> </select> </div> </div> <div class="row"> <div class="col-xs-12 col-sm-6 col-md-4 col-lg-4"> <div class="row"> <div class="col-xs-5 col-sm-6 col-md-6 col-lg-6"> Cod. Modular o DNI: </div> <div class="col-xs-7 col-sm-6 col-md-6 col-lg-6"> <input type="text" id="txtDNIConv" class="form-control" /> </div> </div> </div> <div class="col-xs-12 col-sm-6 col-md-4 col-lg-4"> <div class="row"> <div class="col-xs-5 col-sm-5 col-md-5 col-lg-5"> Cargo: </div> <div class="col-xs-7 col-sm-7 col-md-7 col-lg-7"> <input id="txtCargo" type="text" class="form-control" value="000000" maxlength="6" /> </div> </div> </div> <div class="col-xs-12 col-sm-12 col-md-4 col-lg-4"> <div class="row"> <div class="col-xs-5 col-sm-3 col-md-5 col-lg-5"> Correlativo Sobreviviente: </div> <div class="col-xs-7 col-sm-3 col-md-7 col-lg-7"> <input id="txtCorrelativo" type="text" class="form-control" value="0000" maxlength="4" /> </div> </div> </div> </div><div class="row"><div class="panel panel-cmacm"><div class="panel-heading">Tipo Planilla</div><div class="panel-body"><div class="col-xs-12 col-sm-12 col-md-12 col-lg-12"> <div class="row"> <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6"> <label class="manito"> <input id="rbtDescA" name="tpoplanilla" value="A" type="radio" />Descuento Para Activos</label> </div> <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6"> <label class="manito"> <input id="rbtDescC" name="tpoplanilla" value="C" type="radio" />Descuento para Cesantes</label> </div> </div> </div> </div></div></div> <div class="row"> <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12"> <div class="row"> <div class="col-xs-6 col-sm-2 col-md-2 col-lg-2"> <input id="btnGrabarConv" type="button" class="btn btn-cmacm btn-block" value="Aceptar" /> </div> <div class="col-xs-6 col-sm-2 col-md-2 col-lg-2"> <button type="button" id="btnSalirConv" class="btn btn-cmacm btn-block" data-dismiss="modal" aria-hidden="true">Salir</button> </div> </div> </div> </div>');
    CrearLista("#cmbInstitucion", datos.lstTipoPersonas);

    if (datos.oConvenio != null) {
        $("#cmbInstitucion").val(oConvenio.ccodinst);
        $("#txtDNIConv").val(oConvenio.ccodmod);
        $("#txtCargo").val(oConvenio.cargo);
        $("#txtCorrelativo").val(oConvenio.carben);
        $(":radio[name='tpoplanilla'][value='" + oConvenio.plan + "']").attr('checked', true);
    }

    $("#btnGrabarConv").bind("click", function () {
        if (ValidaDatos(3)) {
            datos.oConvenio = {
                ccodinst: $("#cmbInstitucion").val(),
                ccodmod: $("#txtDNIConv").val(),
                carben: $("#txtCorrelativo").val(),
                cargo: $("#txtCargo").val(),
                plan: $("input:radio[name=tpoplanilla]:checked").val()
            };
            $('#vntConvenio').modal('hide');
        }
    });

    $("#btnSalirConv").bind("click", function () {
        $("#cmbSubProducto").val("");
    });

    $('#vntConvenio').modal('show');
    $("#txtCargo").val('000000');
    $("#txtCorrelativo").val('0000');

}

function DefineCondicionCredProd(perscod, control, prod) {
    var subprod = $("#cmbSubProducto").val();
    var cuotas = $("#txtCuota").val();
    prod = prod || false;

    if (cuotas == "" || isNaN(cuotas)) { cuotas = 0; }
    if (subprod == "" || isNaN(subprod)) { subprod = 0; }

    if (!prod) { subprod = 0; }

    var indice = -1;

    $.fn.Conexion({
        direccion: '/Credito/DefineCondicionCredProd',
        datos: { "psPersCod": perscod, "pnSubProducto": subprod, "pnNroCuotas": cuotas, "pbRefinaciado": false },
        terminado: function (data) {
            indice = ObtenerIndiceLista(datos.lstCondicion, data);
            $(control + "Id").val(datos.lstCondicion[indice].id);
            $(control).val(datos.lstCondicion[indice].nom);
        }
    });

}

function ValidaDatos(tipo) {
    var mensaje = "";
    var valida = true;

    switch (tipo) {
        case 1:/*Validacion General*/
            {
                if ($("#cmbCampana").val() == "") {
                    mensaje = "Seleccione la Campaña";
                    valida = false;
                    break;
                }

                if ($("#cmbCasaComercial").val() == "") {
                    mensaje = "Seleccione la Casa Comercial";
                    valida = false;
                    break;
                }

                if (typeof ($("#cntVendedor.deshabilitado").attr("id")) == "undefined") {
                    if ($("#txtVendedor").val() == "") {
                        mensaje = "Ingrese al Vendedor";
                        valida = false;
                        break;
                    }
                }

                if (typeof (datos.lstRelac) == "undefined") {
                    mensaje = "Ingrese a los Intervinientes del Crédito";
                    valida = false;
                    break;
                }

                if ($("#cmbProducto").val() == "") {
                    mensaje = "Seleccione el Producto";
                    valida = false;
                    break;
                }

                if ($("#cmbSubProducto").val() == "") {
                    mensaje = "Seleccione el SubProducto";
                    valida = false;
                    break;
                }

                if ($("#cmbMoneda").val() == "") {
                    mensaje = "Seleccione la Moneda";
                    valida = false;
                    break;
                }

                if ($("#txtMontoSol").val() == "" || $("#txtMontoSol").val() == 0) {
                    mensaje = "Ingrese el Monto a Solicitar";
                    valida = false;
                    break;
                }

                if (isNaN($("#txtMontoSol").val().replace(',', ''))) {
                    mensaje = "Ingrese correctamente el Monto";
                    valida = false;
                    break;
                }

                if ($("#txtCuota").val() == "" || $("#txtCuota").val() == 0) {
                    mensaje = "Ingrese el Nro. de Cuotas";
                    valida = false;
                    break;
                }

                if (isNaN($("#txtCuota").val().replace(',', ''))) {
                    mensaje = "Ingrese correctamente el Nro. de Cuotas";
                    valida = false;
                    break;
                }

                if ($("#txtPlazo").val() == "" || $("#txtPlazo").val() == 0) {
                    mensaje = "Ingrese el Plazo";
                    valida = false;
                    break;
                }

                if (isNaN($("#txtPlazo").val().replace(',', ''))) {
                    mensaje = "Ingrese correctamente el Plazo";
                    valida = false;
                    break;
                }

                if ($("#cmbDestino").val() == "") {
                    mensaje = "Seleccione el Destino";
                    valida = false;
                    break;
                }

                if ($("#txtFecha").val() == "") {
                    mensaje = "Ingrese la Fecha";
                    valida = false;
                    break;
                }

                if ($("#cmbAnalistas").val() == "") {
                    mensaje = "Seleccione al Analista";
                    valida = false;
                    break;
                }

                if (typeof ($("#cntPromotores.deshabilitado").attr("id")) == "undefined") {
                    if ($("#cmbPromotores").val() == "") {
                        mensaje = "Seleccione al Promotor";
                        valida = false;
                        break;
                    }
                }
                break;
            }
        case 2:/*Validacion Envio Estado Cuenta*/
            {
                if ($("#cmbEECModoRecep").val() == "") {
                    mensaje = "Seleccione el Modo de Recepción";
                    valida = false;
                    break;
                }
                break;
            }
        case 3:/*Validacion Convenio*/
            {
                if ($("#cmbInstitucion").val() == "") {
                    mensaje = "Debe seleccionar una institucion";
                    valida = false;
                    break;
                }

                if ($("#txtDNIConv").val() == "") {
                    mensaje = "Debe indicar un Codigo Modular";
                    valida = false;
                    break;
                }

                if ($("#txtCargo").val() == "") {
                    mensaje = "Debe indicar un Cargo";
                    valida = false;
                    break;
                }

                if ($("#txtCorrelativo").val() == "") {
                    mensaje = "Debe indicar un Correlativo de Sobreviviente";
                    valida = false;
                    break;
                }

                if (typeof ($("input:radio[name=tpoplanilla]:checked").val()) == "undefined") {
                    mensaje = "Debe indicar tipo de Planilla";
                    valida = false;
                    break;
                }

                if ($("#txtCargo").val().length != 6) {
                    mensaje = "El Cargo debe tener 6 caracteres";
                    valida = false;
                    break;
                }

                if ($("#txtCorrelativo").val().length != 4) {
                    mensaje = "El Correlativo de Sobreviviente debe tener 4 caracteres";
                    valida = false;
                    break;
                }
                break;
            }
        case 4:/*Validacion Agropecuario*/
            {
                if ($("#cmbAgroActividad").val() == "") {
                    mensaje = "Seleccione el Tipo de Actividad";
                    valida = false;
                    break;
                }

                if (typeof ($("#cntCoperativaAgro.deshabilitado").attr("id")) == "undefined") {
                    if ($("#txtAgroCodCoopera").val() == "" || $("#txtAgroNomCoopera").val() == "") {
                        mensaje = "Ingrese la cooperativa a la que Pertenece";
                        valida = false;
                        break;
                    }
                }

                if ($("#cmbAgroSubActividad").val() == "") {
                    mensaje = "Seleccione el SubTipo o Cultivo de Actividad";
                    valida = false;
                    break;
                }

                if ($("#txtAgroTotalHect").val() == "") {
                    mensaje = "Ingrese el N° Total de Hectareas";
                    valida = false;
                    break;
                }

                if ($("#txtAgroTotalHect").val() <= 0) {
                    mensaje = "Total de Hectáreas debe ser Mayor a 0";
                    valida = false;
                    break;
                }

                if ($("#txtAgroHectProd").val() == "") {
                    mensaje = "Ingrese el N° de Hectareas en Producción";
                    valida = false;
                    break;
                }

                if ($("#txtAgroHectProd").val() <= 0) {
                    mensaje = "Hectáreas en Producción debe ser Mayor a 0";
                    valida = false;
                    break;
                }

                if ($("#txtAgroTotalHect").val() < $("#txtAgroHectProd").val()) {
                    mensaje = "Hectáreas en Producción debe ser menor o igual al Total de Hectáreas";
                    valida = false;
                    break;
                }

                if ($("#cmbAgroActividad").val() == 2) {

                    if ($("#txtAgroAnimales").val() == "") {
                        mensaje = "Ingrese la Cantidad de Animales";
                        valida = false;
                        break;
                    }

                    if ($("#txtAgroAnimales").val() <= 0) {
                        mensaje = "Ingrese la Cantidad de Animales";
                        valida = false;
                        break;
                    }

                    min = $("#cmbAgroSubActividad option:selected").attr("data-min");
                    if ($("#txtAgroAnimales").val() < min) {
                        mensaje = "El número minímo de animales para " + $("#cmbAgroSubActividad option:selected").text() + " es " + min + ".";
                        valida = false;
                        break;
                    }
                }
                break;
            }
    }


    if (mensaje != "") {
        $.fn.Mensaje({ mensaje: mensaje, tamano: "sm" });
    } else {
        var mensajes = {};
        valida = false;
        switch (tipo) {
            case 1:/*Validacion General*/
                $.fn.Conexion({
                    direccion: '/Credito/ValidaSolicitud',
                    datos: $("#form-solicitud").serializeArray(),
                    terminado: function (data, textStatus, jqXHR) {
                        mensajes = JSON.parse(data);
                        if (mensajes.length > 0) {
                            var accion = function (i) {
                                if (i < mensajes.length) {
                                    valida = mensajes[i].valido;
                                    $.fn.Mensaje({ mensaje: mensajes[i].men, tipo: mensajes[i].tipo, funcionAceptar: accion, indice: i + 1, funcionSi: accion, funcionNo: function () { valida = false; } });
                                } else {
                                    $.fn.Mensaje({ mensaje: "¿Se va a Grabar los Datos, Desea Continuar?", tipo: "SiNo", funcionSi: GrabaDatos });
                                }
                            };

                            valida = mensajes[0].valido;
                            $.fn.Mensaje({ mensaje: mensajes[0].men, tipo: mensajes[0].tipo, funcionAceptar: accion, indice: 1, funcionSi: accion, funcionNo: function () { valida = false; } });
                        }
                    },
                    bloqueo: true
                });
                break;
        }
    }
    return valida;
}

function GrabaDatos() {
    var form = $("#form-solicitud");

    if (numcred != null) {
        if (numcred.length = 18) {
            CrearHidden("#cntHidden", "Cred.oColocaciones.oProducto.cCtaCod", numcred);
        }
    }

    if (!(typeof (datos.oagro) == "undefined" || datos.oagro == null)) {
        if (!(jQuery.isEmptyObject(datos.oagro))) {
            CrearHidden("#cntHidden", "Cred.oCredAgricola.oTipo.ConstanteID", datos.oagro.tipo);
            CrearHidden("#cntHidden", "Cred.oCredAgricola.oSubTipo.ConstanteID", datos.oagro.subtipo);
            CrearHidden("#cntHidden", "Cred.oCredAgricola.nTotalHect", datos.oagro.totalhect);
            CrearHidden("#cntHidden", "Cred.oCredAgricola.nHectProd", datos.oagro.hectprod);
            CrearHidden("#cntHidden", "Cred.oCredAgricola.nAnimales", datos.oagro.ani);
            CrearHidden("#cntHidden", "Cred.oCredAgricola.oCodCoopera.cPersCod", datos.oagro.codcop);
        }
    }

    if (!(typeof (datos.oEnvioEstCta) == "undefined" || datos.oEnvioEstCta == null)) {
        if (!(jQuery.isEmptyObject(datos.oEnvioEstCta))) {
            CrearHidden("#cntHidden", "Cred.oColocaciones.oProducto.nTpoEnvioEstCta", datos.oEnvioEstCta.tpoenvio);
            for (i in datos.oEnvioEstCta.lstPers) {
                CrearHidden("#cntHidden", "Cred.oColocaciones.oProducto.ListaProductoPersona[" + datos.oEnvioEstCta.lstPers[i].indice + "].oPersona.cPersDireccDomicilio", datos.oEnvioEstCta.lstPers[i].direc);
                CrearHidden("#cntHidden", "Cred.oColocaciones.oProducto.ListaProductoPersona[" + datos.oEnvioEstCta.lstPers[i].indice + "].bEnvioEstCta", datos.oEnvioEstCta.lstPers[i].envio);
            }
        }
    }

    if (!(typeof (datos.oConvenio) == "undefined" || datos.oConvenio == null)) {
        CrearHidden("#cntHidden", "Cred.oCredConvenio.oPersona.cPersCod", datos.oConvenio.ccodinst);
        CrearHidden("#cntHidden", "Cred.oCredConvenio.cCodModular", datos.oConvenio.ccodmod);
        CrearHidden("#cntHidden", "Cred.oCredConvenio.cCarben", datos.oConvenio.carben);
        CrearHidden("#cntHidden", "Cred.oCredConvenio.cCargo", datos.oConvenio.cargo);
        CrearHidden("#cntHidden", "Cred.oCredConvenio.cT_Plani", datos.oConvenio.plan);
    }

    valores = form.serializeArray();
    $("#cntHidden input").remove();

    var cerrar = function () {
        $("#btnEditarGen,#cntEditarGen,#cntCancelaGen,#btnCancelarGen,#btnExaminar").removeAttr("disabled").show();
        $("#txtCodAge,#txtCodProd,#txtCodCuenta").attr("disabled", "disabled");
        $("#cntVendedor,#cntGrabarGen,#btnGrabarGen").hide();
        $("#cmbCampana,#txtCondicion,#cmbCasaComercial,#txtVendedor,#btnRelaciones,#btnEnvioEstCta,#txtCodCliente,#txtNomCliente,#txtDNI,#txtRUC,#cmbProducto,#cmbSubProducto,#cmbMoneda,#cmbDestino,#txtFecha,#cmbAnalistas,#cmbPromotores,#txtCondicionProd").attr("disabled", "disabled");
        $("#txtMontoSol,#txtCuota,#txtPlazo").attr("disabled", "disabled");
    }

    $.fn.Conexion({
        direccion: form.attr("action"),
        datos: valores,
        terminado: function (data, textStatus, jqXHR) {
            var resul = data;
            var titulo = "";
            var tam = "";

            if (resul[0] == "0" || resul[0] == "1") {
                if (resul[0] == "0") {
                    titulo = "Número de Crédito";
                    tam = "sm";
                    numcred = resul[1];
                    $("#txtCodCaja").val(numcred.substring(0, 3));
                    $("#txtCodAge").val(numcred.substring(3, 5));
                    $("#txtCodProd").val(numcred.substring(5, 8));
                    $("#txtCodCuenta").val(numcred.substring(8, 18));
                } else { titulo = "Aviso"; }
            } else { titulo = "Aviso"; cerrar = function () { } }

            $.fn.Mensaje({ titulo: titulo, mensaje: resul[1], funcionCerrar: cerrar, tamano: tam });
        },
        bloqueo: true
    });
}