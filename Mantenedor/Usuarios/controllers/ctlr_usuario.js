var ctlr_usuario = new ctlr_usuario();
var ultimoEmail = null;
var table = null;
var valorValidacion = '';
var pestanaActual = "datos";
var jsonArchivoTipif = [];
var camposActual = [];
var registrosTipifOK = [];

function ctlr_usuario() {


    this.Init = function () {
        moment.locale('es');
        //$('.modal ').insertAfter($('body'));
    }

    this.SetListeners = function () {
        $('#nuevo').on('shown.bs.modal', function () {
            $('.modal-backdrop').remove();
            if (!$("body").hasClass('sidebar-mini')) {
                $("a[data-toggle='sidebar']").trigger('click');
            }
        });

        $('#mdlCargaMasiva').on('shown.bs.modal', function () {
            $('.modal-backdrop').remove();
            if (!$("body").hasClass('sidebar-mini')) {
                $("a[data-toggle='sidebar']").trigger('click');
            }
        });

        $('#mdlCargaMasiva').on('hide.bs.modal', function () {
            $("body").removeClass('modal-open');
        });

        $('#mdlVerLog').on('shown.bs.modal', function () {
            $('.modal-backdrop').remove();
            if (!$("body").hasClass('sidebar-mini')) {
                $("a[data-toggle='sidebar']").trigger('click');
            }
        });
        $("#btnReestablecer").click(function () {
            $("#txtUsuarioBuscar").val('');
            $("#cboPerfilBuscar").val('0').trigger('change');
            var filtro = new FiltroUsuario();
            ctlr_usuario.Cargar(filtro);
        });

        $("#btnFiltrar").click(function () {
            var filtro = new FiltroUsuario();

            filtro.Usuario = $("#txtUsuarioBuscar").val();
            filtro.IdPerfil = $("#cboPerfilBuscar").val();

            ctlr_usuario.Cargar(filtro);
        });

        $("#btnNuevo").click(function () {
            ctlr_usuario.Editar('0');
            //$("#tblservicios > tbody").html("");
        });

        $("#txtCorreo").focusout(function () {
            var valor = $("#txtCorreo").val();
            var regex = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
            if (!regex.test(valor) && ultimoEmail != valor) {
                swal.fire('Error de validación', "Debe ingresar un correo válido", 'warning');
                $("#txtCorreo").focus();
                ultimoEmail = valor;
            } else {
                ultimoEmail = null;
            }
        });

        $("#btnGrabar").click(function (e) {
            var btn = $(e.currentTarget);
            ctlr_usuario.GrabarUsuario(btn);
        });

        $('#tblusuarios tbody').on('click', '.row-edit-button', function (e) {
            e.preventDefault();
            if ($.fn.dataTable.isDataTable('#tblusuarios')) {
                var data = table.row($(this).parents('tr')).data();
                var idUsuario = data.ID_USUARIO;
                ctlr_usuario.Editar(idUsuario);
            }
        });

        $('#tblusuarios tbody').on('click', '.row-ver-button', function (e) {
            e.preventDefault();
            if ($.fn.dataTable.isDataTable('#tblusuarios')) {
                var data = table.row($(this).parents('tr')).data();
                var idUsuario = data.ID_USUARIO;
                ctlr_usuario.CargarLog(idUsuario);
            }
        });

        $('#tblusuarios tbody').on('click', '.row-delete-button', function (e) {
            e.preventDefault();
            if ($.fn.dataTable.isDataTable('#tblusuarios')) {
                var table = $('#tblusuarios').DataTable();
                var data = table.row($(this).parents('tr')).data();
                var idUsuario = data.ID_USUARIO;
                ctlr_usuario.Eliminar(idUsuario);
            }
        });

        $("#cboPerfil").change(function () {
            var perfil = $("#cboPerfil").find("option:selected").text();

            if (perfil == "Agente") {
                $("#lblCorreo").hide();
                $("#txtCorreo").hide();
            } else {
                $("#lblCorreo").show();
                $("#txtCorreo").show();
            }
        });

        $("#btnCargaMasiva").click(function () {
            $(".fileinput").fileinput('clear');
            if ($.fn.DataTable.isDataTable("#tablaResumenTipif")) {
                $("#tablaResumenTipif").DataTable().destroy();
            }

            $("#tablaResumenTipif tbody").html('<tr><td colspan="4" style="text-align:center;">Suba archivo para validar datos</td></tr>');
            $("#mdlCargaMasiva").modal('show');
        });

        $("#btnDescargaPlantillaTipif").click(function () {
            ctlr_usuario.DescargarPlantilla();
        });

        $("#fileUploadTipif").fileupload({
            add: function (e, data) {
                data.submit();
            },
            beforeSend: function (e, data) {
                $.busyLoadFull("show", { text: 'Procesando el Archivo' });
                var regexp = /\.(xls)|(xlsx)$/i;

                if (!regexp.test(data.files[0].name)) {
                    try {
                        $.busyLoadFull("hide");
                        swal("Error", "¡Formato de archivo no permitido!", "error");
                    }
                    catch (err) {
                        $.busyLoadFull("hide");
                        alert("Formato de archivo no permitido");
                    }
                    $(".fileinput").fileinput('clear');
                    return false;
                }
            },
            done: function (e, data) {
                var excelRows;
                var files = data.files;
                var i, f;
                for (i = 0, f = files[i]; i != files.length; ++i) {
                    var reader = new FileReader();
                    var name = f.name;
                    reader.onload = function (e) {
                        var data = e.target.result;
                        data = new Uint8Array(data);
                        var workbook = XLSX.read(data, { type: 'array', cellDates: true, dateNF: 'yyyy/mm/dd;@' });
                        var firstSheet = workbook.SheetNames[0];
                        excelRows = XLSX.utils.sheet_to_json(workbook.Sheets[firstSheet], { raw: true, defval: '' });
                        jsonArchivoTipif = excelRows;
                        $.busyLoadFull("hide");
                        ctlr_usuario.ValidarExcelTipif();
                    };
                    reader.readAsArrayBuffer(f);
                }
            }
        });

        $("#btnConfirmarArchTipif").click(function () {
            ctlr_usuario.SubirCargaUsuarios();
        });
    }

    this.CargarCombosUsuario = function () {
        var values = null;

        $.ajax({
            url: "Main.aspx/CargarCombosUsuario",
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
                    var usu = r.values[1];
                    sessionStorage.setItem("InfoUsuarios", JSON.stringify(usu));

                    var populate = [];
                    var populate2 = [];
                    populate.push({
                        'id': '0',
                        'text': 'Seleccione'
                    });
                    populate2.push({
                        'id': '0',
                        'text': 'Todos'
                    });

                    $.each(data, function (index, item) {
                        populate.push({
                            'id': item.KeyName,
                            'text': item.KeyValue
                        });
                        populate2.push({
                            'id': item.KeyName,
                            'text': item.KeyValue
                        });
                    });
                    $("#cboPerfil").select2({ placeholder: 'Seleccione...', data: populate });
                    $("#cboPerfil").removeAttr('disabled');
                    $("#cboPerfil").val('0').trigger('change');

                    $("#cboPerfilBuscar").select2({ placeholder: 'Seleccione...', data: populate2 });
                    $("#cboPerfilBuscar").removeAttr('disabled');
                    $("#cboPerfilBuscar").val('0').trigger('change');

                }
            },
            error: function (response) {

            }
        });
    }

    this.Editar = function (id) {
        ctlr_usuario.LimpiarCampos();
        ctlr_usuario.QuitaErrorValidacion();
        if (id == '0') {
            $("#hidId").val(id);
            $("#nuevo").modal('show');
        } else {
            ctlr_usuario.CargarUsuarioId(id)
        }
    }

    this.Cargar = function Cargar(filtro) {
        try {
            $('#tblusuarios').DataTable().destroy();
        } catch (ex) {

        }
        try {
            $("#tblusuarios tbody").html("");
            table = $('#tblusuarios').DataTable({
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
                    "url": "../TableService.asmx/CargarUsuarios",
                    "method": "POST",
                    "data": {
                        "Usuario": $('#txtUsuarioBuscar').val().trim(),
                        "IdPerfil": $('#cboPerfilBuscar').val() || '0'
                    }
                },
                'columnDefs': [
                    {
                        "targets": 0,
                        "data": null,
                        "width": "50px",
                        "render": function (data, type, row, meta) {

                            var click = "<a href='#' class='row-edit-button'><i class='fas fa-pencil-alt'></i></a>";
                            var ver = ""; //"<a href='#' class='row-conf-button'><i class='fa fa-cog'></i></a>";
                            var eliminar = "<a href='#' class='row-delete-button'><i class='fa fa-trash'></i></a>";

                            var html = click + "&nbsp;&nbsp;" + ver + "&nbsp;&nbsp;" + eliminar;
                            return html;
                        }
                    },
                    { "targets": 1, "data": 'USUARIO' },
                    { "targets": 2, "data": 'NOMBRE' },
                    { "targets": 3, "data": 'PERFIL' },
                    { "targets": 4, "data": 'FECHA_CREACION' },
                ],
                "drawCallback": function () {
                }
            });
        } catch (ex) {
            console.log(ex.message);
        }
    }

    this.CargarUsuarioId = function CargarUsuarioId(idUsuario) {

        var ID_USUARIO = idUsuario;

        try {
            if (ID_USUARIO != '0') {
                var values = JSON.stringify({ ID_USUARIO: ID_USUARIO });

                $.ajax({
                    url: "Main.aspx/CargarUsuarioId",
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

                            if (data.ID_USUARIO != '' && data.ID_USUARIO != '0') {
                                $("#hidId").val(data.ID_USUARIO);
                                $("#txtUsuario").val(data.USUARIO);
                                $("#txtPassword").val('');
                                $("#txtNombre").val(data.NOMBRE);
                                $("#txtCorreo").val(data.CORREO);
                                $("#cboPerfil").val(data.ID_PERFIL).trigger('change');
                            } else {
                                $("#hidId").val('0');
                                $("#txtUsuario").val('');
                                $("#txtPassword").val('');
                                $("#txtNombre").val('');
                                $("#txtCorreo").val('');
                                $("#cboPerfil").val(0).trigger('change');
                            }
                        } else {
                        }
                    },
                    error: function (response) {
                    }
                });
            } else {
                $("#hidId").val('0');
                $("#txtUsuario").val('');
                $("#txtPassword").val('');
                $("#txtNombre").val('');
                $("#txtCorreo").val('');
                $("#cboPerfil").val(0).trigger('change');
            }
            $("#nuevo").modal('show');
        } catch (ex) {
            console.log(ex.message);
        }
    }

    this.GrabarUsuario = function (btn) {

        ctlr_usuario.QuitaErrorValidacion();

        var isValid = true;
        var usuario = new Usuario();

        usuario.ID_USUARIO = $("#hidId").val();
        usuario.USUARIO = $("#txtUsuario").val();
        usuario.PASSWORD = $("#txtPassword").val();
        usuario.NOMBRE = $("#txtNombre").val();
        usuario.CORREO = $("#txtCorreo").val();
        usuario.ID_PERFIL = $("#cboPerfil").val();

        var perfil = $("#cboPerfil").find("option:selected").text();

        if (usuario.USUARIO.length == 0) {
            ctlr_usuario.MuestraErrorValidacion($("#txtUsuario"));
            isValid = false;
        }

        if (usuario.NOMBRE.length == 0) {
            ctlr_usuario.MuestraErrorValidacion($("#txtNombre"));
            isValid = false;
        }

        if (usuario.ID_PERFIL == 0) {
            ctlr_usuario.MuestraErrorValidacion($("#cboPerfil"));
            isValid = false;
        }

        if (perfil == "Agente") {
            if (usuario.ID_USUARIO == "0" || usuario.PASSWORD.length > 0) {
                var regMay = /[A-Z]/g;
                var regNum = /[0-9]/g;
                var regSimbolo = /[\.\,\-\_\@\#\$]/g;

                if (usuario.USUARIO.length > 8) {
                    $("#txtUsuario").data('validacion', 'El largo maximo del Rut es 8.');
                    ctlr_usuario.MuestraErrorValidacion($("#txtUsuario"));
                    isValid = false;
                } else if (usuario.ID_USUARIO == '0' && usuario.PASSWORD.length == 0) {
                    $("#txtPassword").data('validacion', 'Debe ingresar una contraseña');
                    ctlr_usuario.MuestraErrorValidacion($("#txtPassword"));
                    isValid = false;
                }
                else if (usuario.PASSWORD.length == 0) {
                    ctlr_usuario.MuestraErrorValidacion($("#txtPassword"));
                    isValid = false;
                }
            }
        } else {
            if (usuario.ID_USUARIO == "0" || usuario.PASSWORD.length > 0) {
                var regMay = /[A-Z]/g;
                var regNum = /[0-9]/g;
                var regSimbolo = /[\.\,\-\_\@\#\$]/g;

                if (usuario.ID_USUARIO == '0' && usuario.PASSWORD.length == 0) {
                    $("#txtPassword").data('validacion', 'Debe ingresar una contraseña');
                    ctlr_usuario.MuestraErrorValidacion($("#txtPassword"));
                    isValid = false;
                }
                else if (usuario.PASSWORD.length == 0) {
                    ctlr_usuario.MuestraErrorValidacion($("#txtPassword"));
                    isValid = false;
                }
                else if (usuario.PASSWORD.length < 14 || usuario.PASSWORD.length > 25) {
                    $("#txtPassword").data('validacion', 'La contraseña debe tener entre 14 y 25 caracteres');
                    ctlr_usuario.MuestraErrorValidacion($("#txtPassword"));
                    isValid = false;
                }
                else if (!regMay.test(usuario.PASSWORD)) {
                    $("#txtPassword").data('validacion', 'La contraseña debe tener al menos una mayúscula');
                    ctlr_usuario.MuestraErrorValidacion($("#txtPassword"));
                    isValid = false;
                }
                else if (!regNum.test(usuario.PASSWORD)) {
                    $("#txtPassword").data('validacion', 'La contraseña debe tener al menos un número');
                    ctlr_usuario.MuestraErrorValidacion($("#txtPassword"));
                    isValid = false;
                }
                else if (!regSimbolo.test(usuario.PASSWORD)) {
                    $("#txtPassword").data('validacion', 'La contraseña debe tener al menos un carácter especial(. , - _ @ # $).');
                    ctlr_usuario.MuestraErrorValidacion($("#txtPassword"));
                    isValid = false;
                }

            }

            if (usuario.CORREO.length == 0) {
                ctlr_usuario.MuestraErrorValidacion($("#txtCorreo"));
                isValid = false;
            }

            var regex = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
            if (usuario.CORREO.length > 0 && !regex.test(usuario.CORREO)) {
                ctlr_usuario.MuestraErrorValidacion($("#txtCorreo"));
                isValid = false;
            }
        }

        if (isValid) {

            swal.fire({
                title: "Confirmación",
                text: "¿Desea grabar el usuario?",
                icon: "warning",
                showCancelButton: true,
                confirmButtonText: 'Guardar',
                cancelButtonText: 'Cancelar'
            }).then(function (isConfirm) {
                if (isConfirm && isConfirm.value) {
                    var values = JSON.stringify({ usuario: usuario });
                    $.ajax({
                        url: "Main.aspx/GrabarUsuario",
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
                                        var filtro = new FiltroUsuario();
                                        ctlr_usuario.Cargar(filtro);
                                        $("#nuevo").modal('hide');
                                    });
                                }
                                catch (err) {
                                    alert('Operación exitosa: ' + r.msg);
                                    var filtro = new FiltroUsuario();
                                    ctlr_usuario.Cargar(filtro);
                                    $("#nuevo").modal('hide');
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

    this.Eliminar = function (idUsuario) {

        swal.fire({
            title: "Confirmación",
            text: "¿Esta seguro de que desea eliminar el usuario?",
            icon: "warning",
            showCancelButton: true,
            confirmButtonText: 'Eliminar',
            cancelButtonText: 'Cancelar'
        }).then(function (isConfirm) {
            if (isConfirm && isConfirm.value) {
                var values = JSON.stringify({ ID_USUARIO: idUsuario });
                $.ajax({
                    url: "Main.aspx/EliminarUsuario",
                    data: values,
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    beforeSend: function () {
                    },
                    success: function (response) {
                        var r = response.d;
                        if (r.ret == 'OK') {
                            try {
                                swal.fire({
                                    title: "Operación exitosa",
                                    text: "Se ha eliminado correctamente el usuario",
                                    icon: "success"
                                }).then(function (result) {
                                    var filtro = new FiltroUsuario();
                                    ctlr_usuario.Cargar(filtro);
                                });
                            }
                            catch (err) {
                                alert('Operación exitosa: ' + "Se ha eliminado correctamente el usuario");
                                var filtro = new FiltroUsuario();
                                ctlr_usuario.Cargar(filtro);
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
        $("#cboPerfil").val('0').trigger('change');
        $("#txtUsuario").val('');
        $("#txtPassword").val('');
        $("#txtNombre").val('');
        $("#txtCorreo").val('');
    }

    this.CargarLog = function CargarLog(idUsuario) {
        $("#mdlVerLog").modal('show');
        try {
            $('#tblLog').DataTable().destroy();
        } catch (ex) {

        }
        try {
            $("#tblLog tbody").html("");
            $('#tblLog').DataTable({
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
                    "url": "../TableService.asmx/CargarLogUsuarios",
                    "method": "POST",
                    "data": {
                        "ID_USUARIO": idUsuario
                    }
                },
                'columnDefs': [
                    { "targets": 0, "data": 'FECHA' },
                    { "targets": 1, "data": 'USUARIO' },
                    { "targets": 2, "data": 'TIPO' },
                ],
                "drawCallback": function () {
                }
            });
        } catch (ex) {
            console.log(ex.message);
        }
    }
    this.ValidarExcelTipif = function () {
        var resumen = [];
        var msg = "";
        registrosTipifOK = [];
        var ID_SERVICIO = $("#hidId").val();
        var tip = JSON.parse(sessionStorage.getItem("InfoUsuarios"));
        var largo = 0;

        if (jsonArchivoTipif != null) {
            if (jsonArchivoTipif.length > 0) {
                var columnaValida = true;
                try {

                    var primeraFila = jsonArchivoTipif[0];
                    if (primeraFila.USUARIO == null) {
                        msg += "No existe columna USUARIO en el archivo. ";
                        columnaValida = false;
                    }
                    if (primeraFila.NOMBRE == null) {
                        msg += "No existe columna NOMBRE en el archivo. ";
                        columnaValida = false;
                    }
                    if (primeraFila.CORREO == null) {
                        msg += "No existe columna CORREO en el archivo. ";
                        columnaValida = false;
                    }
                    if (primeraFila.PERFIL == null) {
                        msg += "No existe columna PERFIL en el archivo. ";
                        columnaValida = false;
                    }
                }
                catch (e) {
                    console.error(e);
                }

                try {
                    if (columnaValida) {
                        $.each(jsonArchivoTipif, function (indexJson, fila) {
                            var error = false;
                            var tipif = new Usuario();

                            var tmpResumen = new ValidacionCarga();
                            tmpResumen.N_LINEA = fila.__rowNum__ + 1;
                            largo = fila.USUARIO.toString().length;
                            if (fila.USUARIO != null && fila.USUARIO != "") {
                                if (largo > 8) {
                                    tmpResumen.VALIDO = "<i class='fa fa-times' style='color:red;'></i>";
                                    tmpResumen.MENSAJE = "El largo del la columna USUARIO supera el máximo de 8 caracteres.";
                                    resumen.push(tmpResumen);
                                    error = true;
                                } else if (!ctlr_util.ValidaNumeros(fila.USUARIO)) {
                                    tmpResumen.VALIDO = "<i class='fa fa-times' style='color:red;'></i>";
                                    tmpResumen.MENSAJE = "La columna USUARIO solo debe tener números.";
                                    resumen.push(tmpResumen);
                                    error = true;
                                } else {
                                    tipif.USUARIO = fila.USUARIO;
                                }
                            } else {
                                tmpResumen.VALIDO = "<i class='fa fa-times' style='color:red;'></i>";
                                tmpResumen.MENSAJE = "No se ha ingresado valor para columna USUARIO.";
                                resumen.push(tmpResumen);
                                error = true;
                            }

                            tmpResumen = new ValidacionCarga();
                            tmpResumen.N_LINEA = fila.__rowNum__ + 1;
                            largo = fila.NOMBRE.toString().length;
                            if (fila.NOMBRE != null && fila.NOMBRE != "") {
                                if (largo > 100) {
                                    tmpResumen.VALIDO = "<i class='fa fa-times' style='color:red;'></i>";
                                    tmpResumen.MENSAJE = "El largo del la columna NOMBRE supera el máximo de 100 caracteres.";
                                    resumen.push(tmpResumen);
                                    error = true;
                                } else {
                                    tipif.NOMBRE = fila.NOMBRE;
                                }
                            } else {
                                tmpResumen.VALIDO = "<i class='fa fa-times' style='color:red;'></i>";
                                tmpResumen.MENSAJE = "No se ha ingresado valor para columna NOMBRE.";
                                resumen.push(tmpResumen);
                                error = true;
                            }

                            tmpResumen = new ValidacionCarga();
                            tmpResumen.N_LINEA = fila.__rowNum__ + 1;
                            largo = fila.CORREO.toString().length;
                            var correoValido = ctlr_util.ValidaEmail(fila.CORREO);
                            if (fila.CORREO != null && fila.CORREO != "") {
                                if (largo > 100) {
                                    tmpResumen.VALIDO = "<i class='fa fa-times' style='color:red;'></i>";
                                    tmpResumen.MENSAJE = "El largo del la columna CORREO supera el máximo de 100 caracteres.";
                                    resumen.push(tmpResumen);
                                    error = true;
                                } else if (correoValido == false) {
                                    tmpResumen.VALIDO = "<i class='fa fa-times' style='color:red;'></i>";
                                    tmpResumen.MENSAJE = "El correo no es válido.";
                                    resumen.push(tmpResumen);
                                    error = true;
                                } else {
                                    tipif.CORREO = fila.CORREO;
                                }
                            } else {
                                tmpResumen.VALIDO = "<i class='fa fa-times' style='color:red;'></i>";
                                tmpResumen.MENSAJE = "No se ha ingresado valor para columna CORREO.";
                                resumen.push(tmpResumen);
                                error = true;
                            }

                            tmpResumen = new ValidacionCarga();
                            tmpResumen.N_LINEA = fila.__rowNum__ + 1;
                            largo = fila.PERFIL.toString().length;
                            if (fila.PERFIL != null && fila.PERFIL != "") {
                                if (largo > 100) {
                                    tmpResumen.VALIDO = "<i class='fa fa-times' style='color:red;'></i>";
                                    tmpResumen.MENSAJE = "El largo del la columna PERFIL supera el máximo de 100 caracteres.";
                                    resumen.push(tmpResumen);
                                    error = true;
                                } else {
                                    tipif.PERFIL = fila.PERFIL;
                                }
                            } else {
                                tmpResumen.VALIDO = "<i class='fa fa-times' style='color:red;'></i>";
                                tmpResumen.MENSAJE = "No se ha ingresado valor para columna PERFIL.";
                                resumen.push(tmpResumen);
                                error = true;
                            }


                            var ExisteAgente = 0;
                            if (!error) {
                                var tmpResumen = new ValidacionCarga();
                                tmpResumen.N_LINEA = fila.__rowNum__ + 1;
                                if (tip != null) {
                                    $.each(tip, function (i, c) {
                                        if (c.KeyName == tipif.USUARIO) {
                                            ExisteAgente = 1;
                                        }
                                    });
                                }

                                if (ExisteAgente == 1) {
                                    tmpResumen.VALIDO = "<i class='fa fa-times' style='color:red;'></i>";
                                    tmpResumen.MENSAJE = "El Usuario " + tipif.USUARIO + " ya existe en el sistema.";
                                    resumen.push(tmpResumen);
                                    error = true;
                                }
                            }

                            if (!error) {
                                var tmpResumen = new ValidacionCarga();
                                tmpResumen.N_LINEA = fila.__rowNum__ + 1;
                                var cont = 0;
                                var id_usu = 0;
                                var sel = document.getElementById("cboPerfil");
                                for (var i = 0; i < sel.length; i++) {
                                    //  Aca haces referencia al "option" actual
                                    var opt = sel[i];

                                    if (tipif.PERFIL == opt.label) {
                                        cont = cont + 1;
                                        id_usu = opt.value;
                                        break;
                                    }
                                }

                                if (cont == 0) {
                                    tmpResumen.VALIDO = "<i class='fa fa-times' style='color:red;'></i>";
                                    tmpResumen.MENSAJE = "El perfil " + tipif.PERFIL + " no existe.";
                                    resumen.push(tmpResumen);
                                    error = true;
                                } else {
                                    tipif.ID_PERFIL = id_usu;
                                }
                            }

                            var repetido = registrosTipifOK.filter(function (tipifRev) {
                                return tipifRev.USUARIO == tipif.USUARIO;
                            });

                            var tmpResumen = new ValidacionCarga();
                            tmpResumen.N_LINEA = fila.__rowNum__ + 1;
                            if (repetido.length > 0) {
                                tmpResumen.VALIDO = "<i class='fa fa-times' style='color:red;'></i>";
                                tmpResumen.MENSAJE = "Ya existe un USUARIO con los mismos datos en la carga masiva.";
                                resumen.push(tmpResumen);
                                error = true;
                            }

                            if (!error) {
                                registrosTipifOK.push(tipif);
                            }
                        });
                        ctlr_usuario.CargaResumenTipif(resumen);

                        if (registrosTipifOK.length > 0) {
                            //$("#btnConfirmarArchTipif").removeAttr('disabled');
                            swal.fire({
                                title: "Informacion Validación",
                                text: "Registros Válidos: " + registrosTipifOK.length + ". Presione el botón Procesar, para subir los datos",
                                type: "info"
                            }).then(function (result) {
                                document.getElementById('btnConfirmarArchTipif').disabled = false;
                            });

                        } else {
                            swal.fire({
                                title: "Informacion Validación",
                                text: "No se encontraron registros válidos.",
                                type: "info"
                            }).then(function (result) {
                                document.getElementById('btnConfirmarArchTipif').disabled = true;
                            });

                            //$("#btnConfirmarArchTipif").attr('disabled', 'disabled');
                        }
                    } else {
                        swal({
                            title: "Validación",
                            text: msg,
                            type: "info"
                        }).then(function (result) {
                            $(".fileinput").fileinput('clear');
                        });
                    }
                }
                catch (e) {
                    console.error(e);
                }
            } else {
                swal.fire({
                    title: "Validación",
                    text: "El archivo está vacío.",
                    icon: "info"
                }).then(function (result) {
                    $(".fileinput").fileinput('clear');
                });
            }
        } else {
            swal.fire({
                title: "Error de validación",
                text: "El archivo no se pudo leer correctamente, favor inténtelo mas tarde.",
                icon: "error"
            }).then(function (result) {
                $(".fileinput").fileinput('clear');
            });
        }
    }
    this.CargaResumenTipif = function (resumen) {

        if (resumen && resumen.length > 0) {
            var html = "";
            $.each(resumen, function (idx, tmp) {
                html += "<tr>";
                html += "<td>" + tmp.N_LINEA + "</td>";
                html += "<td style='text-align:center;'>" + tmp.VALIDO + "</td>";
                html += "<td>" + tmp.MENSAJE + "</td>";
                html += "</tr>";
            });

            if ($.fn.DataTable.isDataTable("#tablaResumenTipif")) {
                $("#tablaResumenTipif").DataTable().destroy();
            }

            $("#tablaResumenTipif tbody").html(html);
            $("#tablaResumenTipif").DataTable({
                url: '../assets/plugins/datatables/Spanish.json',
                "searching": false,
            });

        }
        else {
            if ($.fn.DataTable.isDataTable("#tablaResumenTipif")) {
                $("#tablaResumenTipif").DataTable().destroy();
            }

            $("#tablaResumenTipif tbody").html('<tr><td colspan="4" style="text-align:center;">Todos los datos son válidos</td></tr>');
            $("#filtrarResumen").hide();
        }
    }
    this.SubirCargaUsuarios = function (btn) {

        ctlr_usuario.QuitaErrorValidacion();

        if (registrosTipifOK.length > 0) {
            swal.fire({
                title: "Confirmación",
                text: "¿Esta seguro que desea cargar los registros válidos? Estos se agregarán a los ya existentes.",
                icon: "warning",
                showCancelButton: true,
                confirmButtonText: 'Guardar',
                cancelButtonText: 'Cancelar'
            }).then(function (isConfirm) {
                if (isConfirm && isConfirm.value) {
                    var values = JSON.stringify({ usuarios: registrosTipifOK });
                    $.ajax({
                        url: "Main.aspx/SubirCargaUsuarios",
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
                                        $(".fileinput").fileinput('clear');
                                        if ($.fn.DataTable.isDataTable("#tablaResumenTipif")) {
                                            $("#tablaResumenTipif").DataTable().destroy();
                                        }

                                        $("#tablaResumenTipif tbody").html('<tr><td colspan="4" style="text-align:center;">Suba archivo para validar datos</td></tr>');
                                        $("#mdlCargaMasiva").modal("hide");
                                        var filtro = new FiltroUsuario();
                                        ctlr_usuario.Cargar(filtro);
                                    });
                                }
                                catch (err) {
                                    alert('Operación exitosa: ' + r.msg);
                                    $(".fileinput").fileinput('clear');
                                    if ($.fn.DataTable.isDataTable("#tablaResumenTipif")) {
                                        $("#tablaResumenTipif").DataTable().destroy();
                                    }

                                    $("#tablaResumenTipif tbody").html('<tr><td colspan="4" style="text-align:center;">Suba archivo para validar datos</td></tr>');
                                    $("#mdlCargaMasiva").modal("hide");
                                    var filtro = new FiltroUsuario();
                                    ctlr_usuario.Cargar(filtro);
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
    this.DescargarPlantilla = function () {

        var url = "../ExportService.asmx/DescargarPlantillaUsuarios";

        var win = window.open(url, '_blank');
    }
}