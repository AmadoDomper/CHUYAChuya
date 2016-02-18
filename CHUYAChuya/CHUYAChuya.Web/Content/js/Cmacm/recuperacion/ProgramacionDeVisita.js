//variables iniciales
var Modelo=null;
var lsProductoPersona = null;
var lsvisitas = new Array();
var tblvisita = $('#tblVisitas');
var editar = false;
var Nedit = -1;

$(document).ready(function () {
    cargarModelo();
    PrepararInterfaces();
});


function PrepararInterfaces() {
    //creamos el modal "Agregar Programacion"
    $("#dv_agregarProgramacion").dialog({
        autoOpen: false,
        modal: true,
        draggable: false,
        width: '75%',
        open: function (event, ui) {
            $('.ui-dialog').css('z-index', 1002);
            $('.ui-widget-overlay').css('z-index', 1001);
        },
    });
    $(".ui-dialog").css({ 'max-width': '750px' });
    //creamos la tabla de programacion de visitas
    tblvisita.simple_datagrid();
    //formateamos el tb para que solo admita hora
    $('#tb_hora_prog').timeEntry();


    $(window).resize(function() {
        $("#dv_agregarProgramacion").dialog("option", "position", {my: "center", at: "center", of: window});
    });
    
}


function cargarModelo() {
    $.fn.Conexion({
        direccion: '/Recuperacion/CargarModeloProgramacionVisita',
        terminado: function (data) {
            Modelo = JSON.parse(data);
            AlCargarModelo();
        },
        bloqueo: true
    });
}


function cargarDatosCredito(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    if (tecla == 13) {
        var caja = $("#tb_CodCaja").val();
        var age = $("#tb_tCodAge").val();
        var prod = $("#tb_CodProd").val();
        var cuenta = $("#tb_CodCuenta").val();
        var nrocuenta = caja + age + prod + cuenta;
        if (nrocuenta.length == 18) {
            $.fn.Conexion({
                direccion: '/Recuperacion/ListarPersonasPorCreditoVencido',
                datos: { "cCtaCod": nrocuenta },
                terminado: function (data) {
                    lsProductoPersona = JSON.parse(data);
                    if (lsProductoPersona.length > 0)
                        AlCargarPersonas();
                    else {
                        $.fn.Mensaje({ titulo: "Mensaje", mensaje: "No se han devuelto datos", tamano: "sm" });
                        disableFormAgregarProgramacion();
                        $("#tb_nombre_prog").val("");
                        $("#tb_direccion_prog").val("");
                        $("#tb_hora_prog").val("");
                    }
                },
                bloqueo: true
            });
        } else {
            $.fn.Mensaje({ mensaje: 'Numero de Cuenta Incorrecto', tamano: "sm" });
        }
    }
}

//devuelve el nombre de la agencia pasando el idAgencia
/*requiere:
    -Modelo Cargado
*/
function devolverNombrePorIdAgencia(codigoAge) {
    for (var i in Modelo.lsAgencias) {
        if (Modelo.lsAgencias[i].cAgeCod == codigoAge) {
            return Modelo.lsAgencias[i].cAgeDescripcion;
        }
    }
    return "Sin Nombre";
}
//devuelve una persona por el idrelacion
/*requiere:
    -lsProductoPersona Cargado
*/
function DevolverPersonaPorRelacion(idrelacion) {
    for (var i in lsProductoPersona) {
        if (lsProductoPersona[i].relac.id == idrelacion) {
            return lsProductoPersona[i].pers;
        }
    }
    return null;
}

function ExisteCreditoVisita(nrocrecdito) {
    for (var i in lsvisitas) {
        if (lsvisitas[i].cCtaCod == nrocrecdito) {
            return true;
        }
    }
    return false;
}

function DevolverVisita(N, por) {
    
    for (var i in lsvisitas) {
            if (lsvisitas[i].N == N) {
               return lsvisitas[i];
            }
    
    }
    return null;
}


function EstaEnNotificaciones(notificaciones, valor) {
    for (var i in notificaciones) {
        if (notificaciones[i].cod == valor) {
            return true;
        }
    }
    return false;
}

//valida el formulario agregar programación
function formulario_prog_validar() {
    var ret = true;
    var mensaje = "";

    var caja = $("#tb_CodCaja").val();
    var age = $("#tb_tCodAge").val();
    var prod = $("#tb_CodProd").val();
    var cuenta = $("#tb_CodCuenta").val();
    var nrocuenta = caja + age + prod + cuenta;

    if (nrocuenta.length != 18) {
        ret = false;
        mensaje += "<br>*El número de credito es invalido";
    }


    if ($("#tb_nombre_prog").val() == "") {
        ret = false;
        mensaje += "<br>*Asegurese de llenar el nombre de la persona";
    }
    if ($("#tb_direccion_prog").val() == "") {
        ret = false;
        mensaje += "<br>*Asegurese de llenar la dirección de la persona";
    }
    if ($("#tb_hora_prog").val().length!=7)
    {
        ret = false;
        mensaje += "<br>*Asegurese de especificar una hora válida";
    }
    if(!ret)
        $.fn.Mensaje({ titulo: "Alerta", mensaje: mensaje, tamano: "sm" });
    return ret;
}


function formulario_prog_editar(visita) {

    if (visita != null) {
        $.fn.Conexion({
            direccion: '/Recuperacion/ListarPersonasPorCreditoVencido',
            datos: { "cCtaCod": visita.cCtaCod },
            terminado: function (data) {
                lsProductoPersona = JSON.parse(data);
            },
            bloqueo: true
        });


        var tb_CodCaja = $("#tb_CodCaja");
        var tb_tCodAge = $("#tb_tCodAge");
        var tb_CodProd = $("#tb_CodProd");
        var tb_CodCuenta = $("#tb_CodCuenta");

        tb_CodCaja.val(visita.cCtaCod.substring(0, 3));
        tb_tCodAge.val(visita.cCtaCod.substring(3, 5));
        tb_CodProd.val(visita.cCtaCod.substring(5, 8));
        tb_CodCuenta.val(visita.cCtaCod.substring(8, 18));

        tb_tCodAge.attr("disabled", "disabled");
        tb_CodProd.attr("disabled", "disabled");
        tb_CodCuenta.attr("disabled", "disabled");
        $("#bt_buscar_prog").attr("disabled", "disabled");

        $("#tb_nombre_prog").val(visita.cPersNombre);
        $("#tb_direccion_prog").val(visita.cPersdireccion);
        $("#tb_fecha_prog").val(Modelo.fechaSistema);
        $("#tb_hora_prog").val(visita.cHora);



        $('#cmb_motivovisita_prog > option[value="' + visita.cMotivoid + '"]').attr("selected", true);
        $('#cmb_lugar_prog > option[value="' + visita.cLugarid + '"]').attr("selected", true);
        $('#cmb_relacion_prog > option[value="' + visita.cPersRelacionid + '"]').attr("selected", true);


        $('input[name="radioNot_prog[]"]').each(function () {
            if (EstaEnNotificaciones(visita.Notificaciones, $(this).val())) {
                $(this).prop('checked', true);
            }
            else {
                $(this).prop('checked', false);
            }
        });
        EnableFormAgregarProgramacion();

        $("#dv_agregarProgramacion").dialog("open");
    }
}


//evento ocurre cuando termina de cargar el modelo de vista
function AlCargarModelo() {
    //Ponemos el nombre de la agencia 
    var id = $("#tb_agencia").val();
    var AgeNom = devolverNombrePorIdAgencia(id);
    $("#tb_agencia").val(AgeNom);

    //ponemos la fecha del sistema en el tb_fecha principal
    $("#tb_fecha").val(Modelo.fechaSistema);
    $("#tb_fecha_prog").val(Modelo.fechaSistema);

    //bindeamos los combobox con las constantes
    $("#cmb_relacion_prog").dropdowlist({
        dataShow: "cConsDescripcion",
        dataValue: "nConsValor",
        dataselect:'20',
        datalist: Modelo.lsConstanteRelacionesDeCretditos
    });
    $("#cmb_lugar_prog").dropdowlist({
        dataShow: "cConsDescripcion",
        dataValue: "nConsValor",
        datalist: Modelo.lsConstanteLugaresDeVisita
    });
    $("#cmb_motivovisita_prog").dropdowlist({
        dataShow: "cConsDescripcion",
        dataValue: "nConsValor",
        datalist: Modelo.lsConstanteMotivoDeVisita
    });
    bind_ChBoxGroup("#dv_tipoNotificaion_prog", Modelo.lsConstanteNotificacionDeVisita, "nConsValor", "cConsDescripcion", "radioNot_prog", "radioNot_prog[]");
}




function AlCargarPersonas()
{
    EnableFormAgregarProgramacion();
    ClearFormAgregarProgramacion();
    asignarDatosPersona_form_prog();

    $("#tb_tCodAge").attr("disabled", "disabled");
    $("#tb_CodProd").attr("disabled", "disabled");
    $("#tb_CodCuenta").attr("disabled", "disabled");
    $("#bt_buscar_prog").attr("disabled", "disabled");
}

function disableFormAgregarProgramacion()
{
    $("#formulario_prog :input").attr("disabled", "disabled");
}

function EnableFormAgregarProgramacion() {

    $("#formulario_prog :input").removeAttr("disabled");
}
function ClearFormAgregarProgramacion() {
    $("#cmb_relacion_prog > option[value='20']").attr("selected", true);
    $("#tb_nombre_prog").val("");
    $("#tb_direccion_prog").val("");
    var boxesNotif = document.getElementsByName("radioNot_prog[]");
    $(boxesNotif).prop('checked', false);
}

function asignarDatosPersona_form_prog()
{
  

    var relID = $("#cmb_relacion_prog").val();
    var pers = DevolverPersonaPorRelacion(relID);

    if (pers != null) {
        $("#tb_nombre_prog").val(pers.nom);
        $("#tb_direccion_prog").val(pers.direc);
        $("#tb_nombre_prog").attr("disabled","disabled");
        $("#tb_direccion_prog").attr("disabled","disabled")
    }
    else {
        $("#tb_nombre_prog").val("");
        $("#tb_direccion_prog").val("");
        $("#tb_nombre_prog").removeAttr("disabled");
        $("#tb_direccion_prog").removeAttr("disabled")
    }
}


function lsvisitas_remove(index) {
    for (var i in lsvisitas) {
        if (lsvisitas[i].N === index) {
            lsvisitas.splice(i, 1);
            break;
        }
    }
    for (var i in lsvisitas) {
        lsvisitas[i].N = i-(-1);
    }
}

function refresh_tblvisita() {
    var data = JSON.stringify(lsvisitas);
    var data = JSON.parse(data);
    tblvisita.simple_datagrid('loadData', data);
}

//---seccion eventos de controles
$("#bt_agregar").click(function () {
        editar = false;
        $("#dv_agregarProgramacion").dialog("open");
        disableFormAgregarProgramacion();
        ClearFormAgregarProgramacion();
        $('#tb_tCodAge').removeAttr("disabled");
        $('#tb_CodProd').removeAttr("disabled");
        $('#tb_CodCuenta').removeAttr("disabled");
        $('#bt_buscar_prog').removeAttr("disabled");
    });

    $("#bt_quitar").click(function () {

        var row = tblvisita.simple_datagrid('getSelectedRow');
        if (row != null) {
            $.fn.Mensaje({
                titulo: "Confirmar",
                mensaje: "¿Está seguro que desea quitar la visita?",
                tamano: "sm", tipo: "SiNo",
                funcionSi: function () {

                    lsvisitas_remove(row.N);
                    refresh_tblvisita();
                }
            });
        }
       
    });


    $("#bt_editar").click(function () {
        editar = true;
        var row = tblvisita.simple_datagrid('getSelectedRow');
        if (row != null) {
            Nedit = row.N;
            formulario_prog_editar(DevolverVisita(row.N));
        }

    });


    $("#bt_anterior_prog").click(function () {
        editar = true;
        var row = tblvisita.simple_datagrid('getSelectedRow');
        if (row != null) {
            Nedit = row.N;
            if (row.N - 1 > 0) {
                formulario_prog_editar(DevolverVisita(row.N - 1));
                tblvisita.simple_datagrid('selectRow', (row.N - 1) - 1);
            }
        }

    });

    $("#bt_siguiente_prog").click(function () {
        editar = true;
        var row = tblvisita.simple_datagrid('getSelectedRow');
        if (row != null) {
            Nedit = row.N;
            if (row.N < lsvisitas.length) {
                formulario_prog_editar(DevolverVisita(row.N + 1));
                tblvisita.simple_datagrid('selectRow', (row.N - 1) + 1);
            }
        }

    });

    $("#bt_cancelar_prog").click(function () {
        ClearFormAgregarProgramacion();
        disableFormAgregarProgramacion();
        $('#tb_tCodAge').removeAttr("disabled");
        $('#tb_CodProd').removeAttr("disabled");
        $('#tb_CodCuenta').removeAttr("disabled");

        if (editar) {
            $("#dv_agregarProgramacion").dialog("close");
        }
    });


    $("#bt_salir_prog").click(function () {
        $("#dv_agregarProgramacion").dialog("close");
    });

    $(".nrocredito").keypress(function (e) {
        cargarDatosCredito(e);
    });


    $("#cmb_relacion_prog").bind("change", function () {
        asignarDatosPersona_form_prog();
    });




    $("#bt_aceptar_prog").click(function () {
        
        if (formulario_prog_validar()) {



            var caja = $("#tb_CodCaja").val();
            var age = $("#tb_tCodAge").val();
            var prod = $("#tb_CodProd").val();
            var cuenta = $("#tb_CodCuenta").val();
            var nrocuenta = caja + age + prod + cuenta;


            var notificaciones = new Array();
            var cnotificaciones = "";
            
            $('input[name="radioNot_prog[]"]:checked').each(function () {
                var notificacion = new Object();
                notificacion.cod = $(this).val();
                notificacion.text = $(this).next("label").html();
                notificaciones.push(notificacion);
                cnotificaciones += notificacion.text.substring(0, 2) + ".";
            });



            var visita = new Object();
            visita.cCtaCod = nrocuenta;
            visita.cPersNombre = $("#tb_nombre_prog").val();
            visita.cPersRelacion = $("#cmb_relacion_prog option:selected").text();
            visita.cPersRelacionid = $("#cmb_relacion_prog").val();
            visita.cPersdireccion = $("#tb_direccion_prog").val();
            visita.cLugar = $("#cmb_lugar_prog option:selected").text();
            visita.cLugarid = $("#cmb_lugar_prog").val();
            visita.cHora = $("#tb_hora_prog").val();
            visita.cMotivo = $("#cmb_motivovisita_prog option:selected").text();
            visita.cMotivoid = $("#cmb_motivovisita_prog").val();
            visita.cNotificaciones = cnotificaciones;
            visita.Notificaciones = notificaciones;

            if (!editar) {
                visita.N = lsvisitas.length + 1;

                if (!ExisteCreditoVisita(visita.cCtaCod)) {
                    lsvisitas.push(visita);
                    refresh_tblvisita();
                    ClearFormAgregarProgramacion();
                    disableFormAgregarProgramacion();
                    $('#tb_tCodAge').removeAttr("disabled");
                    $('#tb_CodProd').removeAttr("disabled");
                    $('#tb_CodCuenta').removeAttr("disabled");

                    tblvisita.simple_datagrid('selectRow', (visita.N - 1));
                   
                }
                else {
                    $.fn.Mensaje({ titulo: "Mensaje", mensaje: "Ya existe una visita programada con el mismo nro de crédito", tamano: "sm" });
                }
            }
            else {//kiere decir que si va a editar
                
                lsvisitas_remove(Nedit);
                visita.N = Nedit;
                lsvisitas.push(visita);
                refresh_tblvisita();
                ClearFormAgregarProgramacion();
                disableFormAgregarProgramacion();
                $('#tb_tCodAge').removeAttr("disabled");
                $('#tb_CodProd').removeAttr("disabled");
                $('#tb_CodCuenta').removeAttr("disabled");

                tblvisita.simple_datagrid('selectRow', (visita.N - 1));
            }

           // $('#dv_agregarProgramacion').dialog('close');
        }
    });




