var terceroArchivo; //Esta variable se usa para guardar referencia de la tabla

$(document).ready(function () {
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
                toastr.success(`Exito al guardar el archivo`);
                refreshTablaArchivos()
            }
            else {
                toastr.error(`Error al guardar el archivo`);
            }
        }
    };
    $('.prospects-file').setProgressedUploader(settings0);

    refreshTablaArchivos();

    //Se detecta el click el el icono de descargar archivo
    $('#archivosRegistrados tbody').on('click', 'a.download_archivo', function () {
        var tr = $(this).closest('tr');
        var row = terceroArchivo.row(tr);
        window.location.href = `/Tercero/DownloadFile/${row.data().id}`;
    }); 

    //Se detecta el click el el icono de eliminar tercero para mostar el modal de cambio de clave con la informacion del usuario
    $('#archivosRegistrados tbody').on('click', 'a.delete_tercero', function () {
        var tr = $(this).closest('tr');
        var row = terceroArchivo.row(tr);
        eliminarArchivo(row.data());
    }); 
});

function refreshTablaArchivos() {
    showLoaderTerceroArchivo(true);
    var id = $('#id_tercero').val();
    $.get(`/Tercero/ListFiles/${id}`, function (tercerosBd) {
        showLoaderTerceroArchivo(false);
        //Se crea el datatable para darle mas dinamismo a la manera de como se visualizan los datos
        terceroArchivo = $('#archivosRegistrados').DataTable({
            destroy: true,
            responsive: true,
            data: tercerosBd,
            columns: [
                { 'data': 'nombre_archivo' },                
                {
                    'data': 'es_foto',
                    'render': function (data) {
                        return (data) ? `<span class="badge badge-success">SI</span>` : `<span class="badge badge-danger">NO</span>`
                    }
                },
                {
                    'searchable': false,
                    'orderable': false,
                    'defaultContent': '<a href="javascript:" class="download_archivo"><i class="fas fa-download" title="Descargar"></i></a>   <a href="javascript:" class="delete_tercero error"><i class="fas fa-trash-alt" title="Eliminar"></i></a>'
                }
            ]
        });

    });
}

//Muestra un loader por si la operacion de listar los terceros demora más de lo estimado
function showLoaderTerceroArchivo(show) {
    if (show) {
        $('#dvArchivos').hide();
        $('#dvLoaderArchivos').show();
    }
    else {
        $('#dvArchivos').show();
        $('#dvLoaderArchivos').hide();
    }
}

function eliminarArchivo(data) {
    $('#eliminarArchivoModal').modal("show");
    $('#titleModalEliminar').html(`${data.nombre_archivo}`);
    $('#nombreEliminar').html(`${data.nombre_archivo}`);
    $('#id3').val(data.id);    
}

function confirmarEliminacion() {
    var id = $('#id3').val();
    $.get(`/Tercero/DeleteFile?id=${id}`, function (data) {
        $('#eliminarArchivoModal').modal("hide");
        if (data.elimino) {
            refreshTablaArchivos();
            toastr.success(`Exito al eliminar el archivo`);
        }
        else {
            toastr.error(`Error al eliminar el archivo`);
        }
    });
}