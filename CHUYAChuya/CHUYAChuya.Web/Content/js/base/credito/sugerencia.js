function CargaSugerenciaCred(e, idcaja, idage, idprod, idcuenta)
{
    tecla = (document.all) ? e.keyCode : e.which;
    if (tecla == 13)
    {
        var caja = $(idcaja).val();
        var age = $(idage).val();
        var prod = $(idprod).val();
        var cuenta = $(idcuenta).val();
        var nrocuenta = caja + age + prod + cuenta;
        if (nrocuenta.length == 18)
        {
            DatosCredito(nrocuenta);
        } else
        {
            $.fn.Mensaje({ mensaje: ':( Numero de Cuenta Incorrecto', tamano: "sm" });
        }
    } else
    {
        return val_09(e);
    }
}

$("body").on("change", "#cmbTpoCred", function ()
{
    var tpoProd = document.getElementById("cmbTpoCred").value;
    if (tpoProd == "150")
    {
        $("#lblInstCorp,#cmbInstCorp").show();
        $("#cmbVivienda").hide();
        $("lblInstCorp").html("Institución Corporativa");
    } else if (tpoProd == "850")
    {
        $("#lblInstCorp,#cmbVivienda").show();
        $("#cmbInstCorp").hide();
        $("lblInstCorp").html("Datos de la Vivienda");
    } else
    {
        $("#lblInstCorp,#cmbInstCorp,#cmbVivienda").hide();
    }

});

function DatosSugerencia(nrocuenta)
{
    $.fn.Conexion({
        direccion: '/Credito/CargaDatosSugerencia',
        datos: { "codcta": nrocuenta },
        terminado: function (data)
        {
            var sug = JSON.parse(data);
            if (sug.Cred == null)
            {
                $.fn.Mensaje({ mensaje: ':( No Hay Datos', tamano: "sm" });
            } else
            {
                $("#txtCodCliente").val(sug.Cred.oTitu.id);
                $("#txtNomCliente").val(sug.Cred.oTitu.nom);
                $("#txtDNI").val(sug.Cred.oTitu.lstdoc[0].num);
                $("#txtRUC").val(sug.Cred.oTitu.lstdoc[1].num);
                $("#txtProducto").val(sug.Cred.col.tpoprod.nom);
                $("#txtSubProducto").val(sug.Cred.col.stpoprod.nom);
                $("#txtMonto").val(number_format(sug.Cred.lsthist[1].monto,2));
                $("#txtNroCuotas").val(sug.Cred.lsthist[1].cuotas);
                $("#txtPlazo").val(sug.Cred.lsthist[1].plazo);
                $("#txtDestCredito").val(sug.Cred.dest.nom);
                $("#txtAnalista").val(sug.Cred.analista.nom);
                $("#txtMontoMiVivienda").val(number_format(sug.Cred.col.montovnda, 2));
                $("#txtComentario").val(sug.Cred.lsthist[0].est.nom);
                $("#txtMontoSug").val(number_format(sug.Cred.lsthist[0].monto,2));
                $("#txtTC").val(sug.Cred.lstProdTasaInt[0].tasaint);
                $("#txtTM").val(sug.Cred.lstProdTasaInt[1].tasaint);
                $("#txtTasaGracia").val(number_format(sug.Cred.lstProdTasaInt[2].tasaint,2));
                $("#txtCuotas").val(sug.Cred.lsthist[0].cuotas);
                /*$("#txtMontoCuota").val();*/
                $("#txtPlazoSug").val(sug.Cred.lsthist[0].plazo);
                
                $("#txtCerticom").val(sug.Cred.NumConcer);
                $("#txtDia1").val(sug.Cred.lsthist[0].PerFecFija);
                $("#txtDia2").val(sug.Cred.lsthist[0].PerFecFij2);
                $("#txtExpMax").val(number_format(sug.Cred.ExpoAntmax,2));
                $("#txtPeriodoGracia").val(sug.Cred.lsthist[0].PerGracia);

                var liniaTemp = sug.Cred.col.linea;
                $("#txtLineaCred").val(liniaTemp.substr(5, 1) == "1" ? "CP1-" + liniaTemp : "LP2-" + liniaTemp);
                
                var tipoGast = sug.Cred.lsthist[0].TipGast;

                if (tipoGast == "V")
                {
                    $("#rbtSegDesVariable").prop("checked", true);
                } else
                {
                    $("#rbtSegDesFijo").prop("checked", true);
                }

                var tipoProCod = sug.Cred.col.stpoprod.id;

                if (tipoProCod == "503" || tipoProCod == "504")
                {
                    $("#txtScore").val(sug.Cred.NumConMic).show();
                    $("#lblScore").show();
                } else
                {
                    $("#txtScore,#lblScore").hide();
                }

                CrearLista("#cmbInstCorp", sug.ListaInstFinan);
                CrearLista("#cmbVivienda", sug.ListaVivienda);
                $("#cmbInstCorp").val(sug.Cred.col.tpoinst.id == 0 ? 1 : sug.Cred.col.tpoinst.id);
                $("#cmbInstCorp").val(sug.Cred.DatoVivi == 0 ? 1 : sug.Cred.DatoVivi);
                $("#cmbCIIU").val(sug.Cred.oTitu.oCIIU.id);
                $("#chkCuotaCom").prop("checked", sug.Cred.CuotaCom);
                $("#chkCalendMiVi").prop("checked", sug.Cred.MiVivienda);
                $("#chkProxMes").prop("checked", sug.Cred.lsthist[0].ProxMes);

                /*$("#").val(sug.Cred.col.stpocred.id)*/

                comboTipoCred = new hCombos();
                comboTipoCred.ids = ['cmbTpoCred', 'cmbSubTpoCred'];
                comboTipoCred.pre = [''];
                comboTipoCred.info = sug.ListaTipCred;
                comboTipoCred.init();
                var tpoCred = document.getElementById("cmbTpoCred");
                var subTpoProd = sug.Cred.col.stpocred.id;
                tpoCred.value = subTpoProd.substring(0, 2) + "0";
                tpoCred.onchange();
                $("#cmbTpoCred").change();
                document.getElementById("cmbSubTpoCred").value = subTpoProd;
            }

        },
        error: function () { numcred = ""; },
        bloqueo: true
    });
}