

function bind_TB(textBoxID, listaBind, dataValue, dataShow) {

    for (var i in listaBind) {
        $(textBoxID).append('<option data-index="' + i + '" value="' + eval("listaBind[i]." + dataValue) + '">' + eval("listaBind[i]." + dataShow) + '</option>');
    }
    if (listaBind.length > 0) {
        return eval("listaBind[0]." + dataValue);
    }
    return -1;
}

function bind_TB(textBoxID, listaBind, dataValue, dataShow, valueselected) {

    for (var i in listaBind) {
        if (valueselected == eval("listaBind[i]." + dataValue)) {
            $(textBoxID).append('<option data-index="' + i + '" value="' + eval("listaBind[i]." + dataValue) + '" selected="true">' + eval("listaBind[i]." + dataShow) + '</option>');
        }
        else {
            $(textBoxID).append('<option data-index="' + i + '" value="' + eval("listaBind[i]." + dataValue) + '">' + eval("listaBind[i]." + dataShow) + '</option>');
        }
    }
    if (listaBind.length > 0) {
        return eval("listaBind[0]." + dataValue);
    }
    return -1;
}


function bind_ChBoxGroup(divPadre, listaBind, dataValue, dataShow, idhtml, idgrupo) {
    for (var i in listaBind) {

        $(divPadre).append('<div class="col-xs-12"><input type="checkbox" id="' + idhtml + eval("listaBind[i]." + dataValue) + '" name="' + idgrupo + '" value="'+ eval("listaBind[i]." + dataValue) +'" ><label for="' + idhtml + eval("listaBind[i]." + dataValue) + '">' + eval("listaBind[i]." + dataShow) + '</label></div>');
    }
    //$(divPadre).buttonset();
    if (listaBind.length > 0) {
        return eval("listaBind[0]." + dataValue);
    }


    return -1;
}


function Validar() {
    //validamos numericos
        if (isNaN($('.tb-numero').val())) {
            $.fn.Mensaje({ titulo: "Alerta", mensaje: "Solo debe ingresar datos numericos", tamano: "sm" });
            return false;
        }
        else {
            return true;
        }
}


(function ($) {
    $.fn.dropdowlist = function (m) {

        m.dataShow = m.dataShow || "";
        m.dataValue = m.dataValue || "";
        m.dataselect = m.dataselect || "";
        m.datalist = m.datalist || null;
        m.contenedor = m.contenedor || this;

        for (var i in m.datalist) {

            if (m.dataselect != "") {
                if (m.dataselect == eval("m.datalist[i]." + m.dataValue)) {
                    m.contenedor.append('<option data-index="' + i + '" value="' + eval("m.datalist[i]." + m.dataValue) + '" selected="true" datashow="' + eval("m.datalist[i]." + m.dataShow) + '">' + eval("m.datalist[i]." + m.dataShow) + '</option>');
                }
                else {
                    m.contenedor.append('<option data-index="' + i + '" value="' + eval("m.datalist[i]." + m.dataValue) + '"  datashow="' + eval("m.datalist[i]." + m.dataShow) + '">' + eval("m.datalist[i]." + m.dataShow) + '</option>');
                }
            }
            else {
               
                m.contenedor.append('<option data-index="' + i + '" value="' + eval("m.datalist[i]." + m.dataValue) + '"  datashow="' + eval("m.datalist[i]." + m.dataShow) + '">' + eval("m.datalist[i]." + m.dataShow) + '</option>');
            }
        }
    }
})(jQuery);








(function ($) {
    $.fn.MultiSelecTabla = function (m) {
       
        var w = m.datos;
        m.tblId = m.tblId || "";
        m.numerado = m.numerado || "No";
        m.contenedor = m.contenedor || this;
        m.cabecera = m.cabecera || "";
        m.funcion_click = m.funcion_click || function () { };
        m.paginacion = m.paginacion || "No";
        m.claseSobre = m.claseSobre || "Si";
        m.scrollVertical = m.scrollVertical || "No";
        m.cantRegVertical = m.cantRegVertical || 4;
        m.campos = m.campos || "";
        m.visible = m.visible || "";

        var col = m.cabecera.split(",");
        var camp;
        if (m.campos != "") {
            camp = m.campos.split(",");
        } else { camp = [] }

        var cssTbl = "table-responsive";

        if (m.scrollVertical == "Si") {
            cssTbl += " scrollvertical-" + m.cantRegVertical
           
        }


        var html = '';
        html = '<div class="' + cssTbl + '"><table data-edit="false" id="' + m.tblId + '" class="table table-bordered table-hover manito">';

        //Cabecera
        html += '<thead><tr>';
        if (m.numerado === "Si") { html += '<th>N°</th>'; }
        for (i in col) { html += '<th>' + col[i] + '</th>'; }
        html += '</tr></thead>';

        //Cuerpo
        html += '<tbody>';

     
        for (i in m.datos) {
            var nomgestor = m.datos[i].gestorCredito.pers.nom;
            if (nomgestor == "--Asignado--") {
                html += "<tr class='seleccionado' objeto='" + JSON.stringify(m.datos[i]) + "'>";
            }
            else {
                html += "<tr objeto='" + JSON.stringify(m.datos[i]) + "'>";
            }
            if (m.numerado == "Si")
                html += '<td>' + i + 1 + '</td>';

            if (camp.length > 0) {
                var dat;
                for (k in camp) {
                    dat = eval("m.datos[i]." + camp[k]);
                    html += '<td>' + (dat == null ? "" : dat) + '</td>';
                }
            }
            else {
                for (j in m.datos[i]) {
                    html += '<td>' + m.datos[i][j] + '</td>';
                }
            }

            html += '</tr>';
        }
        html += '</tbody>';
        $(m.contenedor).html(html);

        $("#" + m.tblId + " tbody tr").bind("click", function () {
            
            if ($("#" + m.tblId).attr("data-edit") == "false") {
                var estado = ($(this).attr('class'));
                if (estado == "seleccionado") {
                    $(this).removeClass("seleccionado");
                }
                else {
                    $(this).addClass("seleccionado");
                }

                m.funcion_click($(this).attr('objeto'), $(this).attr('class') == "seleccionado");
               // $("#" + m.tblId + " tbody tr").removeClass("seleccionado");
                
            }
        })

    }
})(jQuery);




//---clases validadoras Jaox

$(".tb_numero").keypress(function (e) {
    return val_09(e);
});

$(".nopaste").attr("onpaste", "return false");
$(".nocut").attr("oncut", "return false");
$(".nocopy").attr("oncopy", "return false");
$(".readonly").attr("readonly", "true");