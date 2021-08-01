$(document).ready(function () {
    cambiarTipo();

    var uploadButton = $('#uploadButton0');

    var settings0 = {
        onInit: function (elements) {
            uploadButton.hide();
        },

        onGetFile: function (elements) {
            uploadButton.show();
            uploadButton.val('Cargar Archivo');
        },

        onStartSubmitting: function (elements) {
            uploadButton.val('Subiendo...');
            uploadButton.prop('disabled', true);
        },

        onProcessing: function (elements) {
            uploadButton.val('Procesando...');
        },

        onFinish: function (elements, data) {
            uploadButton.val('Cargar Archivo');
            uploadButton.prop('disabled', false);
            $('#file-prospects').val('');
            console.log(data);
            if (data.guardo) {
                toastr.success(`Exito al guardar la notificación de: ${$('#id_tipo_notificacion option:selected').text()}`);
            }
            else {
                toastr.error(`Error al guardar la notificación de: ${$('#id_tipo_notificacion option:selected').text()}`);
            }
        }        
    };
    $('.prospects-file').setProgressedUploader(settings0);
});

function cambiarTipo() {
    var tipo = $('#id_tipo_notificacion').val();
    $.get(`/Notificacion/Variables/?tipo=${tipo}`, function (data) {
        var cuerpo = `<div class="callout callout-info"><h5>Estas son las variables usables</h5><p>Estas variables son las que el sistema reemplazara en la notificación</p>`;
        $.each(data, function (key, variable) {
            if (variable.origen == '{{variantes}}') {
                cuerpo += `<p><b>${variable.origen}:</b> Será reemplazado por una lista que tendra el <b>nombre del campo, el valor que tenia y el nuevo valor</b> ej:</p><ul><li><b>Cambio:</b> nombre</li><li><b>Valor Anterior:</b> Juan</li><li><b>Nuevo valor:</b> Pedro</li></ul>`;
            }
            else {
                cuerpo += `<p><b>${variable.origen}:</b> Será reemplazado por la variable del tercero <b>${variable.destino}</b></p>`;
            }            
        });
        cuerpo += `</div>`
        if (tipo == 1) {
            cuerpo += `<div class="callout callout-info"><h5>Ejemplo</h5><div class="alert alert-info alert-dismissible"><button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>Este es un ejemplo de como seria un html</div><code><h6>Hola, {{nombre}}</h6><p>Se ha creado tu contacto como un cliente en prueba dms.</p></code></div>`;
        }
        else {
            cuerpo += `<div class="callout callout-info"><h5>Ejemplo</h5><div class="alert alert-info alert-dismissible"><button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>Este es un ejemplo de como seria un html</div><code><h6>Hola, {{nombre_completo}}</h6><p>Se han actualizado los siguientes datos en nuestro sistema:</p>{{variantes}}</code></div>`;
        }
        $('#variablesUsables').html(cuerpo);
    });
}