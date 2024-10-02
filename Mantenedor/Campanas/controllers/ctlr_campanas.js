var ctlr_campanas = new ctlr_campanas();

function ctlr_campanas() {


    this.Init = function () {
        moment.locale('es');

    }

    this.SetListeners = function () {
        $('#nuevoCampana').on('shown.bs.modal', function () {
            $('.modal-backdrop').remove();
            if (!$("body").hasClass('sidebar-mini')) {
                $("a[data-toggle='sidebar']").trigger('click');
            }
        });

        $('#nuevoReciclado').on('shown.bs.modal', function () {
            $('.modal-backdrop').remove();
            if (!$("body").hasClass('sidebar-mini')) {
                $("a[data-toggle='sidebar']").trigger('click');
            }
        });

        
        $("#btnReestablecer").click(function () {
            $("#txtCampanaBuscar").val('');
            var filtro = new FiltroCampana();
            ctlr_campanas.Cargar(filtro);
        });

        $("#btnFiltrar").click(function () {
            var filtro = new FiltroCampana();
            filtro.CAMPANA = $("#txtCampanaBuscar").val();
            
            ctlr_campanas.Cargar(filtro);
        });

        $("#btnNuevo").click(function () {
            ctlr_campanas.Editar('0');
        });

        $("#btnGrabar").click(function (e) {
            var btn = $(e.currentTarget);
            ctlr_campanas.Grabar(btn);
        });

        $("#btnReciclar").click(function (e) {
            var btn = $(e.currentTarget);
            var segmento = $("#cboSegmento").val();
            var CS = $("#hiCodigoServicio").val();
            var Campana = $("#hiCampana").val();

            if (segmento == "0") {
                swal.fire('Error', "Debe seleccionar un segmento", 'error');
                return;
            }

            ctlr_campanas.Eliminar(CS, segmento, Campana);
        });

        $('#tblcampanas tbody').on('click', '.row-edit-button', function (e) {
            e.preventDefault();
            if ($.fn.dataTable.isDataTable('#tblcampanas')) {
                var data = table.row($(this).parents('tr')).data();
                var idCampana = data.CODIGO_SERVICIO;
                ctlr_campanas.Editar(idCampana);
            }
        });

        $('#tblcampanas tbody').on('click', '.row-delete-button', function (e) {
            e.preventDefault();
            if ($.fn.dataTable.isDataTable('#tblcampanas')) {
                var table = $('#tblcampanas').DataTable();
                var data = table.row($(this).parents('tr')).data();
                var idCampana = data.CODIGO_SERVICIO;
                var Campana = data.NOMBRE_CAMPANA;
                ctlr_campanas.Reciclado(idCampana, Campana);
            }
        });
    }

    this.Editar = function (id) {
        ctlr_campanas.LimpiarCampos();
        ctlr_campanas.QuitaErrorValidacion();
        if (id == '0') {
            $("#hidId").val(id);
            $("#nuevoCampana").modal('show');
        } else {
            ctlr_campanas.CargarCampanaId(id)
        }
    }

    this.Reciclado = function (id, Campana) {
        
        $("#hiCodigoServicio").val(id);
        $("#hiCampana").val(Campana);
        $("#cboSegmento").val('0');
        $("#nuevoReciclado").modal('show');
    }

    this.Cargar = function Cargar(filtro) {
        
        try {
            $('#tblcampanas').DataTable().destroy();
        } catch (ex) {

        }
        try {

            $("#tblcampanas tbody").html("");
            table = $('#tblcampanas').DataTable({
                "pageLength": 10,
                responsive: true,
                scrollX: false,
                scrollCollapse: false,
                "destroy": true,
                "searching": false,
                language: {
                    url: '../assets/plugins/datatables/Spanish.json'
                },
                "processing": true,
                "serverSide": true,
                order: [[0, "asc"]],
                "ajax": {
                    "url": "../TableService.asmx/CargarCampanas",
                    "method": "POST",
                    "data": {
                        "Campana": ""
                    }
                },
                'columnDefs': [
                    {
                        "targets": 0,
                        "data": null,
                        "width": "50px",
                        "render": function (data, type, row, meta) {

                            var click = "<a href='#' class='row-edit-button'><i class='fas fa-pencil-alt'></i></a>";

                            var html = click;
                            return html;
                        }
                    },
                    { "targets": 1, "data": 'CODIGO_SERVICIO' },
                    { "targets": 2, "data": 'NOMBRE_CAMPANA' },
                    { "targets": 3, "data": 'MODO_DISCADO' },
                    { "targets": 4, "data": 'PREFIJO' },
                    {
                        "targets": 5,
                        "data": null,
                        "width": "50px",
                        "render": function (data, type, row, meta) {

                            var click = "<a href='#' class='row-delete-button'><i class='fa fa-trash'></i></a>";
                            var html = "";
                            var modo = data['MODO_DISCADO'];

                            if (modo == "Asistido") {
                                html = click;
                            } else {
                                html = "No permitido";
                            }

                            return html;
                        }
                    },
                ],
                "drawCallback": function () {
                }
            });
        } catch (ex) {
            console.log(ex.message);
        }
    }

    this.CargarCampanaId = function CargarCampanaId(CS) {

        if (CS != '0') {
            var values = JSON.stringify({ CS: CS });

            $.ajax({
                url: "Main.aspx/CargarCampanaId",
                data: values,
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                beforeSend: function () {
                },
                success: function (response) {
                    var r = response.d;

                    if (r.ret == 'OK') {
                        var data = r.values[0];
                        if (data.ID_CAMPANA != '' && data.ID_CAMPANA != '0') {
                            $("#hidId").val(data.ID_CAMPANA);
                            $("#txtCodigoServicio").val(data.CODIGO_SERVICIO);
                            $("#txtNombreCampana").val(data.NOMBRE_CAMPANA);
                            $("#txtSkill1").val(data.SKILL_1);
                            $("#txtSkill2").val(data.SKILL_2);
                            $("#cboModoDiscado").val(data.MODO_DISCADO);
                            $("#txtPrefijo").val(data.PREFIJO);
                            //$("#rbdActivo").prop('checked', data.ACTIVO == '1' ? true : false);
                        } else {
                            $("#hidId").val('0');
                            $("#txtCodigoServicio").val('');
                            $("#txtNombreCampana").val('');
                            $("#txtSkill1").val('');
                            $("#txtSkill2").val('');
                            $("#cboModoDiscado").val('');
                            $("#txtPrefijo").val('');
                            //$("#rbdActivo").prop('checked', false);
                        }

                        var perfil = sessionStorage.getItem("perfilid");

                        if (perfil == "3") {
                            $("#txtNombreCampana").attr('disabled', 'disabled');
                            $("#txtSkill1").attr('disabled', 'disabled');
                            $("#txtSkill2").attr('disabled', 'disabled');
                            $("#txtPrefijo").attr('disabled', 'disabled');
                        }

                    } else {
                    }
                },
                error: function (response) {
                }
            });
        } else {
            $("#hidId").val('0');
            $("#txtCodigoServicio").val('');
            $("#txtNombreCampana").val('');
            $("#txtSkill1").val('');
            $("#txtSkill2").val('');
            $("#cboModoDiscado").val('');
            $("#txtPrefijo").val('');
            //$("#rbdActivo").prop('checked', false);
        }
        $("#nuevoCampana").modal('show');
    }

    this.Grabar = function (btn) {

        ctlr_campanas.QuitaErrorValidacion();

        var isValid = true;
        var campana = new Campana();
        //campana.ID_CAMPANA = $("#hidId").val() || '0';
        campana.CODIGO_SERVICIO = $("#txtCodigoServicio").val();
        campana.NOMBRE_CAMPANA = $("#txtNombreCampana").val();
        campana.SKILL_1 = $("#txtSkill1").val();
        campana.SKILL_2 = $("#txtSkill2").val();
        campana.MODO_DISCADO = $("#cboModoDiscado").val();
        campana.PREFIJO = $("#txtPrefijo").val();
        //campana.ACTIVO = $("#rbdActivo").prop('checked') ? '1' : '0';
        if (campana.CODIGO_SERVICIO.length == 0) {
            ctlr_campanas.MuestraErrorValidacion($("#txtCodigoServicio"));
            isValid = false;
        }

        if (campana.NOMBRE_CAMPANA.length == 0) {
            ctlr_campanas.MuestraErrorValidacion($("#txtNombreCampana"));
            isValid = false;
        }

        if (campana.SKILL_1.length == 0) {
            ctlr_campanas.MuestraErrorValidacion($("#txtSkill1"));
            isValid = false;
        }

        if (campana.SKILL_2.length == 0) {
            ctlr_campanas.MuestraErrorValidacion($("#txtSkill2"));
            isValid = false;
        }

        if (campana.MODO_DISCADO == 0) {
            ctlr_campanas.MuestraErrorValidacion($("#cboModoDiscado"));
            isValid = false;
        }

        if (campana.PREFIJO.length == 0) {
            ctlr_campanas.MuestraErrorValidacion($("#txtPrefijo"));
            isValid = false;
        }

        if (isValid) {


            swal.fire({
                title: "Confirmación",
                text: "¿Desea actualizar la campaña?",
                icon: "warning",
                showCancelButton: true,
                confirmButtonText: 'Guardar',
                cancelButtonText: 'Cancelar'
            }).then(function (isConfirm) {
                if (isConfirm && isConfirm.value) {
                    var values = JSON.stringify({ campana: campana });
                    $.ajax({
                        url: "Main.aspx/Grabar",
                        data: values,
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        beforeSend: function () {
                            $(btn).buttonLoader('start');
                        },
                        success: function (response) {
                            $(btn).buttonLoader('stop');
                            var r = response.d;
                            if (r.ret == 'OK') {
                                try {
                                    swal.fire({
                                        title: "Operación exitosa",
                                        text: r.msg,
                                        icon: "success"
                                    }).then(function (result) {
                                        var filtro = new FiltroCampana();
                                        ctlr_campanas.Cargar(filtro);
                                        $("#nuevoCampana").modal('hide');
                                    });
                                }
                                catch (err) {
                                    alert('Operación exitosa: ' + r.msg);
                                    var filtro = new FiltroCampana();
                                    ctlr_campanas.Cargar(filtro);
                                    $("#nuevoCampana").modal('hide');
                                }
                            } else {
                                try {
                                    swal.fire("Error", r.msg, "error");
                                }
                                catch (err) {
                                    alert("Error" + ': ' + r.msg);
                                }
                            }
                        },
                        error: function (response) {
                            $(btn).buttonLoader('stop');
                            try {
                                swal.fire("Error inesperado", 'Ocurrió un error inesperado, inténtelo más tarde.', "error");
                            }
                            catch (err) {
                                alert("Error inesperado" + ': ' + 'Ocurrió un error inesperado, inténtelo más tarde.');
                            }
                        }
                    });
                }
            });
        }

    }

    this.Eliminar = function (CS, Segmento, Campana) {

        swal.fire({
            title: "Confirmación",
            text: "¿Esta seguro de que desea reciclar la campaña " + Campana + "/" + Segmento + "?",
            icon: "warning",
            showCancelButton: true,
            confirmButtonText: 'Reciclar',
            cancelButtonText: 'Cancelar'
        }).then(function (isConfirm) {
            if (isConfirm && isConfirm.value) {
                var values = JSON.stringify({ CS: CS, Segmento: Segmento });
                $.ajax({
                    url: "Main.aspx/ReciclarCampana",
                    data: values,
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    beforeSend: function () {
                    },
                    success: function (response) {
                        var r = response.d;
                        if (r.ret == 'OK') {
                            var data = r.values[0].KeyValue;

                            try {
                                swal.fire({
                                    title: "Operación exitosa",
                                    text: "El campana/segmento ha sido correctamente reciclada. Registros reciclados: " + data,
                                    icon: "success"
                                }).then(function (result) {
                                    var filtro = new FiltroCampana();
                                    ctlr_campanas.Cargar(filtro);
                                    $("#nuevoReciclado").modal('hide');
                                });
                            }
                            catch (err) {
                                alert('Operación exitosa: ' + "El campana/segmento ha sido correctamente reciclada. Registros reciclados: " + data);
                                var filtro = new FiltroCampana();
                                ctlr_campanas.Cargar(filtro);
                                $("#nuevoReciclado").modal('hide');
                            }
                        } else {
                            try {
                                swal.fire("Error", r.msg, "error");
                            }
                            catch (err) {
                                alert("Error" + ': ' + r.msg);
                            }
                        }
                    },
                    error: function (response) {
                        $(btn).buttonLoader('stop');
                        try {
                            swal.fire("Error inesperado", 'Ocurrió un error inesperado, inténtelo más tarde.', "error");
                        }
                        catch (err) {
                            alert("Error inesperado" + ': ' + 'Ocurrió un error inesperado, inténtelo más tarde.');
                        }
                    }
                });
            }
        });

    }

    this.QuitaErrorValidacion = function () {
        $(".error-validacion").remove();
    }

    this.MuestraErrorValidacion = function (input) {
        var mensaje = $(input).data("validacion");
        $(input).parent().append('<span class="text-danger error-validacion" style="display:none;">' + mensaje + '</span>');
        $(".error-validacion").fadeIn();
    }

    this.LimpiarCampos = function () {

        $("#hidId").val('0');
        $("#txtCodigoServicio").val('');
        $("#txtNombreCampana").val('');
        $("#txtSkill1").val('');
        $("#txtSkill2").val('');
        $("#cboModoDiscado").val('');
        $("#txtPrefijo").val('');
        //$("#rbdActivo").prop('checked', true);
    }

}