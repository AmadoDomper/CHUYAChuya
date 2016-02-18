//variables iniciales
var listaPeriodos = null;
var listaTramos = null;
var listaTramoCarteraVencida=null;
var terminodecargar = 0;


$("#bt_guardar").bind("click", function () {
    GuardarTCV();
});

$("#bt_limpiar").bind("click", function () {

    $.fn.Mensaje({ titulo: "Confirmar", mensaje: "¿Está seguro que desea limpiar?", tamano: "sm", tipo: "SiNo", funcionSi:limpiar });
});

/*$("#bt_salir").bind("click", function () {
    $.fn.Mensaje({
        titulo: "Confirmar", mensaje: "¿Está seguro que desea salir?", tamano: "sm", tipo: "SiNo",
        funcionSi: function () {
            //aqui poner codigo de redireccion al principal
        }
    });
});*/


$(document).ready(function () {
    listarTramosCarteraVencida();
    ListarPeriodos();
    ListarTramos();
});


function limpiar() {


    listarTramosCarteraVencida();
    ListarPeriodos();
    ListarTramos();
}



function GuardarTCV() {


    if (Validar())
    {
        for (i in listaTramoCarteraVencida)
        {

            var inicioCon = "TCV_D_" + listaTramoCarteraVencida[i].nTramoCarteraVencidaId;
            var finCon = "TCV_H_" + listaTramoCarteraVencida[i].nTramoCarteraVencidaId;
            var BonCon = "TCV_B_" + listaTramoCarteraVencida[i].nTramoCarteraVencidaId;

            var desde = eval("$('#" + inicioCon + "').val()");
            var hasta = eval("$('#" + finCon + "').val()");
            var bono = eval("$('#" + BonCon + "').val()");

            var cambio =
                listaTramoCarteraVencida[i].nPorcentajeInicio != desde ||
                listaTramoCarteraVencida[i].nPorcentajeTermino != hasta ||
                listaTramoCarteraVencida[i].nBono != bono;

            listaTramoCarteraVencida[i].nPorcentajeInicio = desde;
            listaTramoCarteraVencida[i].nPorcentajeTermino = hasta;
            listaTramoCarteraVencida[i].nBono = bono;
            listaTramoCarteraVencida[i].cambio = cambio;

        }

        var form = $("#form-TCV");



        $.fn.Conexion({
            direccion: form.attr("action"),
            contentType:"application/json",
            datos: JSON.stringify(listaTramoCarteraVencida),
            terminado: function (data) {
                $.fn.Mensaje({ titulo: "Estado", mensaje: data, tamano: "sm" });
            },
            bloqueo: true
        });

    }
}


function ListarPeriodos() {
    $.fn.Conexion({
        direccion: '/Recuperacion/ListarPeriodo',
        terminado: function (data) {
            listaPeriodos = JSON.parse(data);
            terminodecargar++;
            bindData();
        },
        bloqueo: true
    });
}

function ListarTramos() {
    $.fn.Conexion({
        direccion: '/Recuperacion/ListarTramos',
        terminado: function (data) {
            listaTramos = JSON.parse(data);
            terminodecargar++;
            bindData();
        }, bloqueo: true
    });
}

function listarTramosCarteraVencida() {
    $.fn.Conexion({

        direccion: '/Recuperacion/ListarTramoCarteraVencida',
        terminado: function (data) {
            listaTramoCarteraVencida = JSON.parse(data);
            terminodecargar++;
            bindData()
        }, bloqueo: true
    });
}

function bindData() {
    if (terminodecargar >= 3) {
        crearTabla("#tcv-table");
    }
}


function crearTabla(div)
{
    BloquearCarga();
    var vhtml = "";
    vhtml += '<div class="table-responsive">';//panel
    vhtml += '<table id="tbTCV" class="table table-bordered table-hover manito">';//panelbody
            vhtml += '<thead><tr>';//cabeza
                vhtml += '<th>--</th>';

                //para las cabeceras del periodo
                for (i in listaPeriodos) {
                    vhtml += '<th colspan="3">' + listaPeriodos[i].cConsDescripcion + ' dias </th>';
                }
            vhtml += '</tr></thead>';//cabeza
            vhtml += '<tbody>';
            vhtml += '<tr>';
            vhtml += '<td><b>Tramo</b></td>';
            for (i in listaPeriodos) {
                vhtml += '<td><b> Desde(%)</b></td>';
                vhtml += '<td><b> Hasta(%)</b></td>';
                vhtml += '<td><b> Bono(S/.)</b></td>';
            }
            vhtml += '</tr>';

            for (i in listaTramos) {
                vhtml += '<tr>'
                vhtml += '<td><b>' + listaTramos[i].cConsDescripcion + '</b></td>';
                for (j in listaPeriodos) {
                    var row = CursordevolverfilaTCV(listaPeriodos[j].nConsValor, listaTramos[i].nConsValor);
                    vhtml += '<td><input type="text" class="form-control tb-porcentaje tb-numero" id="TCV_D_' + row.nTramoCarteraVencidaId + '" value="' + row.nPorcentajeInicio + '"  onkeypress="return val_09DC(event, this);" onpaste="return false"></td>';
                    vhtml += '<td><input type="text" class="form-control tb-porcentaje tb-numero" id="TCV_H_' + row.nTramoCarteraVencidaId + '" value="' + row.nPorcentajeTermino + '"  onkeypress="return val_09DC(event, this);" onpaste="return false"></td>';
                    vhtml += '<td><input type="text" class="form-control tb-numero" id="TCV_B_' + row.nTramoCarteraVencidaId + '" value="' + row.nBono + '" onkeypress="return val_09DC(event, this);" onpaste="return false"></td>';
                }
                vhtml += '</tr>'
            }
            vhtml += '</tbody>';

        vhtml += '</table>';//panelbody
    vhtml += '</div>';//panel
    $(div).html(vhtml);


    $('.tb-porcentaje').keyup(function () {
        if (this.value > 100) this.value = 100;
        if (this.value < 0) this.value = 0;
    });

    DesbloquearCarga();
}

function CursordevolverfilaTCV(nPeriodo, ntramo) {
    for (var i in listaTramoCarteraVencida) {
        if (listaTramoCarteraVencida[i].nConstantePeriodo == nPeriodo && listaTramoCarteraVencida[i].nConstanteTramo == ntramo) {
            return listaTramoCarteraVencida[i];
        }
    }
}

