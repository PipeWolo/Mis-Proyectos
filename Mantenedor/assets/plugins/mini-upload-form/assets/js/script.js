var auxAdjuntos = [];
var _URL = window.URL || window.webkitURL;
$(function () {

    var ul = $('.upload ul');

    $('#dropBack a').click(function () {
        // Simulate a click on the file input button
        // to show the file browser dialog
        $(this).parent().find('input').click();
    });

    $('#dropFront a').click(function () {
        // Simulate a click on the file input button
        // to show the file browser dialog
        $(this).parent().find('input').click();
    });

    $('#dropClienteFront a').click(function () {
        // Simulate a click on the file input button
        // to show the file browser dialog
        $(this).parent().find('input').click();
    });

    $('#dropClienteInterno a').click(function () {
        // Simulate a click on the file input button
        // to show the file browser dialog
        $(this).parent().find('input').click();
    });

    $('#dropClienteInternoCliente a').click(function () {
        // Simulate a click on the file input button
        // to show the file browser dialog
        $(this).parent().find('input').click();
    });

    $('#dropCerrar a').click(function () {
        // Simulate a click on the file input button
        // to show the file browser dialog
        $(this).parent().find('input').click();
    });

    $('#dropCerrarDerivar a').click(function () {
        // Simulate a click on the file input button
        // to show the file browser dialog
        $(this).parent().find('input').click();
    });

    // Initialize the jQuery File Upload plugin
    $('#uploadBack').fileupload({

        // This element will accept file drag/drop uploading
        dropZone: $('#dropBack'),

        // This function is called when a file is added to the queue;
        // either via the browse button, or via drag/drop:
        add: function (e, data) {
            //e.preventDefault();
            //console.log("data", data);
            var file = data.files[0];
            if (file.size > 4194304) {
                swal.fire('Error', 'O arquivo n&#227;o deve exceder 4 MB', 'warning');
                return false;
            }
            var tpl = $('<li class="working"><input type="text" value="0" data-width="48" data-height="48"' +
                ' data-fgColor="#0788a5" data-readOnly="1" data-bgColor="#3e4043" /><p></p><span></span></li>');

            // Append the file name and file size
            tpl.find('p').text(data.files[0].name)
                .append('<i>' + formatFileSize(data.files[0].size) + '</i>');

            ul = $(this).children('ul');
            // Add the HTML to the UL element
            data.context = tpl.appendTo(ul);

            // Initialize the knob plugin
            tpl.find('input').knob();

            // Listen for clicks on the cancel icon
            tpl.find('span').click(function () {

                if (tpl.hasClass('working')) {
                    reader.abort();
                }

                tpl.fadeOut(function () {
                    var idx = $(tpl).index();
                    auxAdjuntos.splice(idx, 1);
                    tpl.remove();
                });

            });
            // Automatically upload the file once it is added to the queue
            var reader = new FileReader();

            reader.onload = function (data) {
                return function (e) {
                    var file = data.files[0];
                    var f = e.target.result;
                    var ext = f.split(",")[0];
                    var bin = f.split(",")[1];
                    var kvp = new KVP();
                    kvp.KeyName = file.name;
                    kvp.KeyValue = ext;
                    kvp.KeyValue2 = encodeURIComponent(bin);
                    kvp.KeyValue3 = bin;
                    auxAdjuntos.push(kvp);
                };
            }(data);

            reader.onprogress = function (data) {
                return function (e) {
                    // Calculate the completion percentage of the upload
                    var progress = parseInt(e.loaded / e.total * 100, 10);

                    // Update the hidden input field and trigger a change
                    // so that the jQuery knob plugin knows to update the dial
                    data.context.find('input').val(progress).change();

                    if (progress == 100) {
                        data.context.removeClass('working');
                    }
                };
            }(data);

            reader.onerror = function (data) {
                return function (e) {
                    // Something has gone wrong!
                    data.context.addClass('error');
                };
            }(data);

            reader.readAsDataURL(file);
        },

        progress: function (e, data) {

            // Calculate the completion percentage of the upload
            var progress = parseInt(data.loaded / data.total * 100, 10);

            // Update the hidden input field and trigger a change
            // so that the jQuery knob plugin knows to update the dial
            data.context.find('input').val(progress).change();

            if (progress == 100) {
                data.context.removeClass('working');
            }
        },

        fail: function (e, data) {
            // Something has gone wrong!
            data.context.addClass('error');
        }

    });

    $('#uploadFront').fileupload({

        // This element will accept file drag/drop uploading
        dropZone: $('#dropFront'),

        // This function is called when a file is added to the queue;
        // either via the browse button, or via drag/drop:
        add: function (e, data) {
            //e.preventDefault();
            //console.log("data", data);
            var tpl = $('<li class="working"><input type="text" value="0" data-width="48" data-height="48"' +
                ' data-fgColor="#0788a5" data-readOnly="1" data-bgColor="#3e4043" /><p></p><span></span></li>');

            // Append the file name and file size
            tpl.find('p').text(data.files[0].name)
                .append('<i>' + formatFileSize(data.files[0].size) + '</i>');

            ul = $(this).children('ul');
            // Add the HTML to the UL element
            data.context = tpl.appendTo(ul);

            // Initialize the knob plugin
            tpl.find('input').knob();

            // Listen for clicks on the cancel icon
            tpl.find('span').click(function () {

                if (tpl.hasClass('working')) {
                    reader.abort();
                }

                tpl.fadeOut(function () {
                    var idx = $(tpl).index();
                    auxAdjuntos.splice(idx, 1);
                    tpl.remove();
                });

            });

            // Automatically upload the file once it is added to the queue
            var file = data.files[0];
            var reader = new FileReader();

            reader.onload = function (data) {
                return function (e) {
                    var file = data.files[0];
                    var f = e.target.result;
                    var ext = f.split(",")[0];
                    var bin = f.split(",")[1];
                    var kvp = new KVP();
                    kvp.KeyName = file.name;
                    kvp.KeyValue = ext;
                    kvp.KeyValue2 = encodeURIComponent(bin);
                    kvp.KeyValue3 = bin;
                    auxAdjuntos.push(kvp);
                };
            }(data);

            reader.onprogress = function (data) {
                return function (e) {
                    // Calculate the completion percentage of the upload
                    var progress = parseInt(e.loaded / e.total * 100, 10);

                    // Update the hidden input field and trigger a change
                    // so that the jQuery knob plugin knows to update the dial
                    data.context.find('input').val(progress).change();

                    if (progress == 100) {
                        data.context.removeClass('working');
                    }
                };
            }(data);

            reader.onerror = function (data) {
                return function (e) {
                    // Something has gone wrong!
                    data.context.addClass('error');
                };
            }(data);

            reader.readAsDataURL(file);
        },

        progress: function (e, data) {

            // Calculate the completion percentage of the upload
            var progress = parseInt(data.loaded / data.total * 100, 10);

            // Update the hidden input field and trigger a change
            // so that the jQuery knob plugin knows to update the dial
            data.context.find('input').val(progress).change();

            if (progress == 100) {
                data.context.removeClass('working');
            }
        },

        fail: function (e, data) {
            // Something has gone wrong!
            data.context.addClass('error');
        }

    });

    $('#uploadClienteFront').fileupload({

        // This element will accept file drag/drop uploading
        dropZone: $('#dropClienteFront'),

        // This function is called when a file is added to the queue;
        // either via the browse button, or via drag/drop:
        add: function (e, data) {
            //e.preventDefault();
            //console.log("data", data);
            var tpl = $('<li class="working"><input type="text" value="0" data-width="48" data-height="48"' +
                ' data-fgColor="#0788a5" data-readOnly="1" data-bgColor="#3e4043" /><p></p><span></span></li>');

            // Append the file name and file size
            tpl.find('p').text(data.files[0].name)
                .append('<i>' + formatFileSize(data.files[0].size) + '</i>');

            ul = $(this).children('ul');
            // Add the HTML to the UL element
            data.context = tpl.appendTo(ul);

            // Initialize the knob plugin
            tpl.find('input').knob();

            // Listen for clicks on the cancel icon
            tpl.find('span').click(function () {

                if (tpl.hasClass('working')) {
                    reader.abort();
                }

                tpl.fadeOut(function () {
                    var idx = $(tpl).index();
                    auxAdjuntos.splice(idx, 1);
                    tpl.remove();
                });

            });

            // Automatically upload the file once it is added to the queue
            var file = data.files[0];
            var reader = new FileReader();

            reader.onload = function (data) {
                return function (e) {
                    var file = data.files[0];
                    var f = e.target.result;
                    var ext = f.split(",")[0];
                    var bin = f.split(",")[1];
                    var kvp = new KVP();
                    kvp.KeyName = file.name;
                    kvp.KeyValue = ext;
                    kvp.KeyValue2 = encodeURIComponent(bin);
                    kvp.KeyValue3 = bin;
                    auxAdjuntos.push(kvp);
                };
            }(data);

            reader.onprogress = function (data) {
                return function (e) {
                    // Calculate the completion percentage of the upload
                    var progress = parseInt(e.loaded / e.total * 100, 10);

                    // Update the hidden input field and trigger a change
                    // so that the jQuery knob plugin knows to update the dial
                    data.context.find('input').val(progress).change();

                    if (progress == 100) {
                        data.context.removeClass('working');
                    }
                };
            }(data);

            reader.onerror = function (data) {
                return function (e) {
                    // Something has gone wrong!
                    data.context.addClass('error');
                };
            }(data);

            reader.readAsDataURL(file);
        },

        progress: function (e, data) {

            // Calculate the completion percentage of the upload
            var progress = parseInt(data.loaded / data.total * 100, 10);

            // Update the hidden input field and trigger a change
            // so that the jQuery knob plugin knows to update the dial
            data.context.find('input').val(progress).change();

            if (progress == 100) {
                data.context.removeClass('working');
            }
        },

        fail: function (e, data) {
            // Something has gone wrong!
            data.context.addClass('error');
        }

    });

    $('#uploadClienteInterno').fileupload({

        // This element will accept file drag/drop uploading
        dropZone: $('#dropClienteInterno'),

        // This function is called when a file is added to the queue;
        // either via the browse button, or via drag/drop:
        add: function (e, data) {
            //e.preventDefault();
            //console.log("data", data);
            var tpl = $('<li class="working"><input type="text" value="0" data-width="48" data-height="48"' +
                ' data-fgColor="#0788a5" data-readOnly="1" data-bgColor="#3e4043" /><p></p><span></span></li>');

            // Append the file name and file size
            tpl.find('p').text(data.files[0].name)
                .append('<i>' + formatFileSize(data.files[0].size) + '</i>');

            ul = $(this).children('ul');
            // Add the HTML to the UL element
            data.context = tpl.appendTo(ul);

            // Initialize the knob plugin
            tpl.find('input').knob();

            // Listen for clicks on the cancel icon
            tpl.find('span').click(function () {

                if (tpl.hasClass('working')) {
                    reader.abort();
                }

                tpl.fadeOut(function () {
                    var idx = $(tpl).index();
                    auxAdjuntos.splice(idx, 1);
                    tpl.remove();
                });

            });

            // Automatically upload the file once it is added to the queue
            var file = data.files[0];
            var reader = new FileReader();

            reader.onload = function (data) {
                return function (e) {
                    var file = data.files[0];
                    var f = e.target.result;
                    var ext = f.split(",")[0];
                    var bin = f.split(",")[1];
                    var kvp = new KVP();
                    kvp.KeyName = file.name;
                    kvp.KeyValue = ext;
                    kvp.KeyValue2 = encodeURIComponent(bin);
                    kvp.KeyValue3 = bin;
                    auxAdjuntos.push(kvp);
                };
            }(data);

            reader.onprogress = function (data) {
                return function (e) {
                    // Calculate the completion percentage of the upload
                    var progress = parseInt(e.loaded / e.total * 100, 10);

                    // Update the hidden input field and trigger a change
                    // so that the jQuery knob plugin knows to update the dial
                    data.context.find('input').val(progress).change();

                    if (progress == 100) {
                        data.context.removeClass('working');
                    }
                };
            }(data);

            reader.onerror = function (data) {
                return function (e) {
                    // Something has gone wrong!
                    data.context.addClass('error');
                };
            }(data);

            reader.readAsDataURL(file);
        },

        progress: function (e, data) {

            // Calculate the completion percentage of the upload
            var progress = parseInt(data.loaded / data.total * 100, 10);

            // Update the hidden input field and trigger a change
            // so that the jQuery knob plugin knows to update the dial
            data.context.find('input').val(progress).change();

            if (progress == 100) {
                data.context.removeClass('working');
            }
        },

        fail: function (e, data) {
            // Something has gone wrong!
            data.context.addClass('error');
        }

    });

    $('#uploadClienteInternoCliente').fileupload({

        // This element will accept file drag/drop uploading
        dropZone: $('#dropClienteInternoCliente'),

        // This function is called when a file is added to the queue;
        // either via the browse button, or via drag/drop:
        add: function (e, data) {
            //e.preventDefault();
            //console.log("data", data);
            var tpl = $('<li class="working"><input type="text" value="0" data-width="48" data-height="48"' +
                ' data-fgColor="#0788a5" data-readOnly="1" data-bgColor="#3e4043" /><p></p><span></span></li>');

            // Append the file name and file size
            tpl.find('p').text(data.files[0].name)
                .append('<i>' + formatFileSize(data.files[0].size) + '</i>');

            ul = $(this).children('ul');
            // Add the HTML to the UL element
            data.context = tpl.appendTo(ul);

            // Initialize the knob plugin
            tpl.find('input').knob();

            // Listen for clicks on the cancel icon
            tpl.find('span').click(function () {

                if (tpl.hasClass('working')) {
                    reader.abort();
                }

                tpl.fadeOut(function () {
                    var idx = $(tpl).index();
                    auxAdjuntos.splice(idx, 1);
                    tpl.remove();
                });

            });

            // Automatically upload the file once it is added to the queue
            var file = data.files[0];
            var reader = new FileReader();

            reader.onload = function (data) {
                return function (e) {
                    var file = data.files[0];
                    var f = e.target.result;
                    var ext = f.split(",")[0];
                    var bin = f.split(",")[1];
                    var kvp = new KVP();
                    kvp.KeyName = file.name;
                    kvp.KeyValue = ext;
                    kvp.KeyValue2 = encodeURIComponent(bin);
                    kvp.KeyValue3 = bin;
                    auxAdjuntos.push(kvp);
                };
            }(data);

            reader.onprogress = function (data) {
                return function (e) {
                    // Calculate the completion percentage of the upload
                    var progress = parseInt(e.loaded / e.total * 100, 10);

                    // Update the hidden input field and trigger a change
                    // so that the jQuery knob plugin knows to update the dial
                    data.context.find('input').val(progress).change();

                    if (progress == 100) {
                        data.context.removeClass('working');
                    }
                };
            }(data);

            reader.onerror = function (data) {
                return function (e) {
                    // Something has gone wrong!
                    data.context.addClass('error');
                };
            }(data);

            reader.readAsDataURL(file);
        },

        progress: function (e, data) {

            // Calculate the completion percentage of the upload
            var progress = parseInt(data.loaded / data.total * 100, 10);

            // Update the hidden input field and trigger a change
            // so that the jQuery knob plugin knows to update the dial
            data.context.find('input').val(progress).change();

            if (progress == 100) {
                data.context.removeClass('working');
            }
        },

        fail: function (e, data) {
            // Something has gone wrong!
            data.context.addClass('error');
        }

    });

    $('#uploadCerrar').fileupload({

        // This element will accept file drag/drop uploading
        dropZone: $('#dropCerrar'),

        // This function is called when a file is added to the queue;
        // either via the browse button, or via drag/drop:
        add: function (e, data) {
            //e.preventDefault();
            //console.log("data", data);
            var tpl = $('<li class="working"><input type="text" value="0" data-width="48" data-height="48"' +
                ' data-fgColor="#0788a5" data-readOnly="1" data-bgColor="#3e4043" /><p></p><span></span></li>');

            // Append the file name and file size
            tpl.find('p').text(data.files[0].name)
                .append('<i>' + formatFileSize(data.files[0].size) + '</i>');

            ul = $(this).children('ul');
            // Add the HTML to the UL element
            data.context = tpl.appendTo(ul);

            // Initialize the knob plugin
            tpl.find('input').knob();

            // Listen for clicks on the cancel icon
            tpl.find('span').click(function () {

                if (tpl.hasClass('working')) {
                    reader.abort();
                }

                tpl.fadeOut(function () {
                    var idx = $(tpl).index();
                    auxAdjuntos.splice(idx, 1);
                    tpl.remove();
                });

            });

            // Automatically upload the file once it is added to the queue
            var file = data.files[0];
            var reader = new FileReader();

            reader.onload = function (data) {
                return function (e) {
                    var file = data.files[0];
                    var f = e.target.result;
                    var ext = f.split(",")[0];
                    var bin = f.split(",")[1];
                    var kvp = new KVP();
                    kvp.KeyName = file.name;
                    kvp.KeyValue = ext;
                    kvp.KeyValue2 = encodeURIComponent(bin);
                    kvp.KeyValue3 = bin;
                    auxAdjuntos.push(kvp);
                };
            }(data);

            reader.onprogress = function (data) {
                return function (e) {
                    // Calculate the completion percentage of the upload
                    var progress = parseInt(e.loaded / e.total * 100, 10);

                    // Update the hidden input field and trigger a change
                    // so that the jQuery knob plugin knows to update the dial
                    data.context.find('input').val(progress).change();

                    if (progress == 100) {
                        data.context.removeClass('working');
                    }
                };
            }(data);

            reader.onerror = function (data) {
                return function (e) {
                    // Something has gone wrong!
                    data.context.addClass('error');
                };
            }(data);

            reader.readAsDataURL(file);
        },

        progress: function (e, data) {

            // Calculate the completion percentage of the upload
            var progress = parseInt(data.loaded / data.total * 100, 10);

            // Update the hidden input field and trigger a change
            // so that the jQuery knob plugin knows to update the dial
            data.context.find('input').val(progress).change();

            if (progress == 100) {
                data.context.removeClass('working');
            }
        },

        fail: function (e, data) {
            // Something has gone wrong!
            data.context.addClass('error');
        }

    });

    $('#uploadCerrarDerivar').fileupload({

        // This element will accept file drag/drop uploading
        dropZone: $('#dropCerrarDerivar'),

        // This function is called when a file is added to the queue;
        // either via the browse button, or via drag/drop:
        add: function (e, data) {
            //e.preventDefault();
            //console.log("data", data);
            var tpl = $('<li class="working"><input type="text" value="0" data-width="48" data-height="48"' +
                ' data-fgColor="#0788a5" data-readOnly="1" data-bgColor="#3e4043" /><p></p><span></span></li>');

            // Append the file name and file size
            tpl.find('p').text(data.files[0].name)
                .append('<i>' + formatFileSize(data.files[0].size) + '</i>');

            ul = $(this).children('ul');
            // Add the HTML to the UL element
            data.context = tpl.appendTo(ul);

            // Initialize the knob plugin
            tpl.find('input').knob();

            // Listen for clicks on the cancel icon
            tpl.find('span').click(function () {

                if (tpl.hasClass('working')) {
                    reader.abort();
                }

                tpl.fadeOut(function () {
                    var idx = $(tpl).index();
                    auxAdjuntos.splice(idx, 1);
                    tpl.remove();
                });

            });

            // Automatically upload the file once it is added to the queue
            var file = data.files[0];
            var reader = new FileReader();

            reader.onload = function (data) {
                return function (e) {
                    var file = data.files[0];
                    var f = e.target.result;
                    var ext = f.split(",")[0];
                    var bin = f.split(",")[1];
                    var kvp = new KVP();
                    kvp.KeyName = file.name;
                    kvp.KeyValue = ext;
                    kvp.KeyValue2 = encodeURIComponent(bin);
                    kvp.KeyValue3 = bin;
                    auxAdjuntos.push(kvp);
                };
            }(data);

            reader.onprogress = function (data) {
                return function (e) {
                    // Calculate the completion percentage of the upload
                    var progress = parseInt(e.loaded / e.total * 100, 10);

                    // Update the hidden input field and trigger a change
                    // so that the jQuery knob plugin knows to update the dial
                    data.context.find('input').val(progress).change();

                    if (progress == 100) {
                        data.context.removeClass('working');
                    }
                };
            }(data);

            reader.onerror = function (data) {
                return function (e) {
                    // Something has gone wrong!
                    data.context.addClass('error');
                };
            }(data);

            reader.readAsDataURL(file);
        },

        progress: function (e, data) {

            // Calculate the completion percentage of the upload
            var progress = parseInt(data.loaded / data.total * 100, 10);

            // Update the hidden input field and trigger a change
            // so that the jQuery knob plugin knows to update the dial
            data.context.find('input').val(progress).change();

            if (progress == 100) {
                data.context.removeClass('working');
            }
        },

        fail: function (e, data) {
            // Something has gone wrong!
            data.context.addClass('error');
        }

    });
    // Prevent the default action when a file is dropped on the window
    $(document).on('drop dragover', function (e) {
        e.preventDefault();
    });

    // Helper function that formats the file sizes
    function formatFileSize(bytes) {
        if (typeof bytes !== 'number') {
            return '';
        }

        if (bytes >= 1000000000) {
            return (bytes / 1000000000).toFixed(2) + ' GB';
        }

        if (bytes >= 1000000) {
            return (bytes / 1000000).toFixed(2) + ' MB';
        }

        return (bytes / 1000).toFixed(2) + ' KB';
    }

});