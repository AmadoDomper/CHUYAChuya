//-----jaox-------

//listas 
var ListaGestores = null;
var ListaAgencias = null;
var listaCreditos = null;
var listaCreditosElegidos = new Array();

//variables globales
var idAnlistaElegido = null;
var idGestorElegido = null;



$("#tb_gestores").bind("change", function () {

    if (listaCreditosElegidos.length > 0) {
        $.fn.Mensaje({
            titulo: "Confirmar", mensaje: "¿esta seguro de cambiar de Gestor de Cobranza sin antes guardar los cambios?", tamano: "sm", tipo: "SiNo", funcionSi:
                function () {
                    idGestorElegido = $("#tb_gestores").val();
                    if (idAnlistaElegido != null)
                        CargarCreditosPorAnalista(idAnlistaElegido)
                },
            funcionNo:
                function () {
                    $("#tb_gestores > option[value=" + idGestorElegido + "]").attr("selected", true);
                }
        });
    }
    else {
        idGestorElegido = $("#tb_gestores").val();
        if (idAnlistaElegido != null)
            CargarCreditosPorAnalista(idAnlistaElegido)
    }
});

$("#bt_guardar").bind("click", function () {
    GuardarAsignacion();
});



$(document).ready(function () {
    CargarTodasLasVariablesIniciales();
});



function GuardarAsignacion() {

    $.fn.Mensaje({ titulo: "Confirmar", mensaje: "¿Está seguro que desea guardar la asignación?", tamano: "sm", tipo: "SiNo", funcionSi: 
        function () {
            var idGestor = $("#tb_gestores").val();
            $.fn.Conexion({
                direccion: '/Recuperacion/guardarAsignacion',
                //contentType: "application/json",
                //datos: listaCreditosElegidos,
                datos: { "listaCreditos": JSON.stringify(listaCreditosElegidos), "codigoGestor": idGestor },
                terminado: function (data) {
                    $.fn.Mensaje({ titulo: "Estado", mensaje: data, tamano: "sm" });
                },
                bloqueo: true
            });
            if (idAnlistaElegido != null)
                CargarCreditosPorAnalista(idAnlistaElegido);
        }
    });


  
}


function CargarTodasLasVariablesIniciales() {
    cargarGestoresDeCobranza();
    cargarAgencias();
}

function cargarGestoresDeCobranza() {

    $.fn.Conexion({
        direccion: '/Recuperacion/ListarGestoresDeCobranza',
        terminado: function (data) {
            ListaGestores = JSON.parse(data);
            idGestorElegido = bind_TB("#tb_gestores", ListaGestores, "cPersCod", "cPersNombre");
        },
        bloqueo: true
    });
}

function cargarAgencias() {
    $.fn.Conexion({
        direccion: '/Recuperacion/ListarAgencias',
        terminado: function (data) {
            ListaAgencias = JSON.parse(data);

            $('#arbol').tree({
                data: CrearNodoAgencias(),
                selectable: true,
                onCanSelectNode: function (node) {
                    var Sid = node.id + '';
                    if (Sid.substring(0, 2) == "AN") {

                        var ret = false;

                        if (listaCreditosElegidos.length > 0) {
                            $.fn.Mensaje({
                                titulo: "Confirmar", mensaje: "¿esta seguro de cambiar de creditos de analista sin antes guardar los cambios?", tamano: "sm", tipo: "SiNo", funcionSi:
                                    function () {
                                        CargarCreditosPorAnalista(Sid.substring(3));
                                        idAnlistaElegido = Sid.substring(3);
                                        ret = true;
                                    }
                            });

                        }
                        else {
                            CargarCreditosPorAnalista(Sid.substring(3));
                            idAnlistaElegido = Sid.substring(3);
                            ret = true;
                        }


                        /*CargarCreditosPorAnalista(Sid.substring(3));
                        idAnlistaElegido = Sid.substring(3);*/

                        return ret;
                    }
                    
                }
            });

            CrearNodosAnalistas();
        },
        bloqueo: true
    });
}

function CrearNodoAgencias() {
    var strdatos = '';
    strdatos += '[';
    for (i in ListaAgencias) {
        strdatos += '{ "label":"' + ListaAgencias[i].cAgeDescripcion + '", "id":"AG_' + ListaAgencias[i].cAgeCod;

        strdatos += '"}';
        if (i < ListaAgencias.length-1) {
            strdatos += ",";
        }
    }
    strdatos += ']';
    return JSON.parse(strdatos);
}


function CrearNodosAnalistas() {
   
    for (i in ListaAgencias) {
            $.fn.Conexion({
                direccion: '/Recuperacion/ListarAnalistasPorAgencia',
                datos: { "codAge": ListaAgencias[i].cAgeCod },
                terminado: function (data) {
                    analistas = JSON.parse(data);
                    for (j in analistas) {
                        if (j < analistas.length - 1) {
                            var strdatos = '';
                            strdatos += '{label:"' + analistas[j].nom + '", id:"AN_' + analistas[j].id;
                            strdatos += '"}';
                            eval("var parent_node = $('#arbol').tree('getNodeById','AG_" + analistas[analistas.length - 1].id + "');");
                            eval("$('#arbol').tree('appendNode'," + strdatos + ", parent_node);");
                        }
                    }
                },
                bloqueo: true
            });
    }

}

function CargarCreditosPorAnalista(codigoAnalista) {
    $.fn.Conexion({
        direccion: '/Recuperacion/ListarCreditosPorAnalista',
        datos: { "cPersCodAnalista": codigoAnalista },
        terminado: function (data) {
            listaCreditos = JSON.parse(data);
            construirVistaCreditos();
        },
        bloqueo: true
    });
    //reseteamos la lista de creditos elegidos
    listaCreditosElegidos = new Array();
}


function construirVistaCreditos()
{
    if (listaCreditos.length > 0) {

        //identificamos las agencias que contiene la lista y creamos los divs
        var lsIDAgencias = identificarAgenciasDeCreditos(listaCreditos);
        $("#dv_creditos").html("");
        for (var i in lsIDAgencias) {
            crearPanelCredito(lsIDAgencias[i], devolverNombrePorIdAgencia(lsIDAgencias[i]));


            var lsCredAgencias = new Array();
            for (var j in listaCreditos) {
                if (listaCreditos[j].cCodAge == lsIDAgencias[i]) {
                    lsCredAgencias.push(listaCreditos[j]);
                }

                //para mostrar la asignacion de gestores de acuerdo al tbgestor
                
                if ($("#tb_gestores").val() == listaCreditos[j].gestorCredito.pers.id) {
                    listaCreditos[j].gestorCredito.pers.nom = "--Asignado--";
                }

            }

            $("#dv_agencia_" + lsIDAgencias[i]).MultiSelecTabla({
                tblId: "tblcredito_agencia" + lsIDAgencias[i],
                cabecera: "Codigo,Titular,Gestor",
                campos: "id,lstprodpers[0].pers.nom,gestorCredito.pers.nom",
                datos: lsCredAgencias,
                funcion_click: function (datos, seleccionado) {
                    var oCredito = JSON.parse(datos);
                    if (seleccionado) {
                        //le metemos el id del gestor que esta siendo elegido
                        //var idGestor = $("#tb_gestores").val();
                       // oCredito.gestorCredito.pers.id = idGestor;
                        listaCreditosElegidos.push(oCredito.id);
                    }
                    else {
                        QuitarElemento(oCredito);
                    }
                   // alert(listaCreditosElegidos.length);
                }
            });
        }

    }
    else {
        $("#dv_creditos").html("No hay créditos para el analista");
    }
}

//Quita un elemento credito de la lista de creditos elegido
/*
Requisitos: tener la listaCreditosElegidos inicializada
*/
function QuitarElemento(_oCredito) {
    for (var i in listaCreditosElegidos) {
        if (listaCreditosElegidos[i] == _oCredito.id) {
            listaCreditosElegidos.splice(i, 1);
        }
    }
}

//añade el panel de agencias a #div_creditos
function crearPanelCredito(codigoAgencia, nombreAgencia) {
    var html = '';
    html += ' <div class="row"> ';
    html += '<div class="panel panel-cmacm">';
    html += '<div class="panel-heading">' + nombreAgencia + '</div>';
    html += '<div class="panel-body">';
   // html += '<div class="col-xs-1"></div><div class="col-xs-11">';
    html += '<div id="dv_agencia_' + codigoAgencia + '"></div>';
    //html += '</div>';//cols
    html+='</div>';//body
    html += '</div>';//panel;
    html += '</div>';//row;
    $("#dv_creditos").append(html);

}


//devuelve un array con los ids de las agencias identificadas en los creditos obtenidos
function identificarAgenciasDeCreditos(_listaCreditos) {
    if (listaCreditos.length > 0) {
        var lscodAge=new Array();
        lscodAge.push(_listaCreditos[0].cCodAge);
        for (var i in _listaCreditos) {
            if (!existeCodigoAG(lscodAge, _listaCreditos[i].cCodAge))
            {
                lscodAge.push(_listaCreditos[i].cCodAge);
            }
        }
        return lscodAge;
    }
    return null;
}
//apoyo para "identificarAgenciasDeCreditos"
function existeCodigoAG(ls, codigo) {
    for (var i in ls) {
        if (ls[i] == codigo)
        {
            return true;
        }
    }
    return false;
}
//devuelve el nombre de la agencia pasando el idAgencia
/*requiere:
    -ListaAgencias cargadas
*/
function devolverNombrePorIdAgencia(codigoAge) {
    for (var i in ListaAgencias) {
        if (ListaAgencias[i].cAgeCod == codigoAge) {
            return ListaAgencias[i].cAgeDescripcion;
        }
    }
    return "Sin Nombre";
}


