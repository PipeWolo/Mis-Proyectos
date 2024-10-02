var ctrl_tipif = new Contoller_Tipificacion();

function Contoller_Tipificacion() {
    this.CargaMotivosLlamada = function CargaMotivosLlamada() {
        var tip = JSON.parse(sessionStorage.getItem("tip"));
        var resultadoLlamada = $("#cboResultadoLlamada").find("option:selected").text();
        ctlr_util.ResetCombo("#cboResultadoCampana"); $("#cboResultadoCampana").attr('disabled', 'disabled');
        ctlr_util.ResetCombo("#cboMotivoCampana"); $("#cboMotivoCampana").attr('disabled', 'disabled');

        if (tip != null) {
            var opt = ctlr_util.ResetCombo("#cboMotivoLlamada");

            $.each(tip, function (i, c) {
                if (c.ResultadoLlamada == resultadoLlamada)
                    opt[opt.length] = new Option(c.MotivoLlamada, c.MotivoLlamada);
            });

            $("#cboMotivoLlamada").attr('disabled', false);
        }
    }
    this.CargaResultadoCampana = function CargaResultadoCampana() {
        var tip = JSON.parse(sessionStorage.getItem("tip"));
        ctlr_util.ResetCombo("#cboMotivoLlamada"); $("#cboMotivoLlamada").attr('disabled', 'disabled');
        var resultadoCampanaAux = "";

        if (tip != null) {
            var opt = ctlr_util.ResetCombo("#cboResultadoCampana");

            $.each(tip, function (i, c) {
                if (c.ResultadoCampana != null) {
                    if (c.ResultadoCampana != resultadoCampanaAux)
                        opt[opt.length] = new Option(c.ResultadoCampana, c.ResultadoCampana);

                    resultadoCampanaAux = c.ResultadoCampana;
                }
            });

            $("#cboResultadoCampana").attr('disabled', false);
        }
    }
    this.CargaMotivosCampana = function CargaMotivosCampana() {
        var tip = JSON.parse(sessionStorage.getItem("tip"));
        var resultadoCampana = $("#cboResultadoCampana").find("option:selected").text();

        if (tip != null) {
            var opt = ctlr_util.ResetCombo("#cboMotivoCampana");

            $.each(tip, function (i, c) {
                if (c.ResultadoCampana == resultadoCampana)
                    opt[opt.length] = new Option(c.MotivoCampana, c.MotivoCampana);
            });

            $("#cboMotivoCampana").attr('disabled', false);
        }
    }
    this.VerificaReprogramacion = function VerificaReprogramacion() {
        var tip = JSON.parse(sessionStorage.getItem("tip"));
        var resultadoLlamada = $("#cboResultadoLlamada").find("option:selected").text();
        var motivoLlamada = $("#cboMotivoLlamada").find("option:selected").text();
        var pnlReprogramacion = document.getElementById("pnlReprogramacion");
        pnlReprogramacion.style.display = "none";
        $("#txtFechaRepro").val('')

        $.each(tip, function (i, c) {
            if (c.ResultadoLlamada == resultadoLlamada) {
                if (c.MotivoLlamada == motivoLlamada) {
                    var repro = c.Reprogramacion;

                    if (repro == "Usuario") {
                        var pnlReprogramacion = document.getElementById("pnlReprogramacion");
                        pnlReprogramacion.style.display = "block";
                    }
                }
            }
        });
    }
}