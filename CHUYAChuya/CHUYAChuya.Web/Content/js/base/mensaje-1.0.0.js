(function ($) {
    $.fn.Mensaje = function (m) {
        m = m || {};
        m.titulo = m.titulo || "Aviso";
        m.tamano = m.tamano || "";
        m.mensaje = m.mensaje || "";
        m.tipo = m.tipo || "Aceptar";
        m.funcionCerrar = m.funcionCerrar || function () { };
        m.funcionAceptar = m.funcionAceptar || function () { };
        m.funcionSi = m.funcionSi || function () { };
        m.funcionNo = m.funcionNo || function () { };
        m.indice = m.indice || 0;
        m.focusElement = m.focusElement || "";

        var html = '<div class="row"><div class="col-xs-12 col-sm-12 col-md-12 col-lg-12"><h4 class="text-center">' + m.mensaje + '</h4></div></div>';
        var btnId;
        switch (m.tipo) {
            case "Aceptar":
                btnId = "btnAceptarMen";
                html = html + '<div class="row"> <div class="col-xs-3 col-sm-4 col-md-4 col-lg-4"></div> <div class="col-xs-6 col-sm-4 col-md-4 col-lg-4"> <button type="button" id="' + btnId + '" class="btn btn-sm btn-primary btn-block">Aceptar</button> </div> <div class="col-xs-3  col-sm-4 col-md-4 col-lg-4"></div> </div>';
                break;
            case "SiNo":
                btnId = "btnSiMen"
                html = html + '<div class="row"> <div class="hidden-xs col-sm-3 col-md-3 col-lg-3"></div> <div class="col-xs-6 col-sm-3 col-md-3 col-lg-3"> <button type="button" id="' + btnId + '" class="btn btn-sm btn-primary btn-block">Si</button> </div> <div class="col-xs-6 col-sm-3 col-md-3 col-lg-3"> <button type="button" id="btnNoMen" class="btn btn-sm btn-default btn-block">No</button> </div> <div class="hidden-xs col-sm-3 col-md-3 col-lg-3"></div> </div>';
                break;
        }

        $.fn.Ventana({
            id: "vntMensaje",
            titulo: m.titulo,
            tamano: m.tamano,
            cuerpo: html,
            focusElement: m.focusElement || btnId,
            funcionCerrar: m.funcionCerrar
        });

        $("#btnAceptarMen").bind("click", function () {
            $("#vntMensaje").modal('hide');
            $('#vntMensaje').on('hidden.bs.modal', function (e) {
                m.funcionAceptar(m.indice);
            });
        });

        $("#btnSiMen").bind("click", function () {
            $("#vntMensaje").modal('hide');
            $('#vntMensaje').on('hidden.bs.modal', function (e) {
                m.funcionSi(m.indice);
            });
        });

        $("#btnNoMen").bind("click", function () {
            m.funcionNo();
            $("#vntMensaje").modal('hide');
        });

        //$('#vntMensaje').on('shown.bs.modal', function () {
        //    $('#btnSiMen').focus();
        //});

        $('#vntMensaje').modal('show')


        
    }
})(jQuery);