var tercero; //Esta variable se usa para guardar referencia de la tabla

$(document).ready(function () {
    /*
     * Se valida los campos obligatorios del formulario de creacion de tercero
     */
    $('#fromTercero').validate({
        focusInvalid: false,
        ignore: "",
        rules: {
            nombre: { required: true },
            apellidos: { required: true },
            direccion: { required: true },
            email: { required: true },
            telefono: { required: true, minlength: 7, number: true }            
        },
        submitHandler: function (form) {
            $.post('/Tercero/Save', $('#fromTercero').serialize(), function (data) {
                //Una vez guardada la informacion se refresca la tabla para mostrar los nuevos datos
                refreshTablaTerceros();
                //Se envia una notificacion de exito o de error al usuario dependiendo de como salio la transaccion
                if (data.guardo) {
                    toastr.success(`Exito al guardar el tercero: ${$('#nombre').val()} ${$('#apellidos').val()}`);
                    $('#fromTercero').trigger("reset");
                }
                else {
                    toastr.error(`Error al guardar el tercero: ${$('#nombre').val()} ${$('#apellidos').val()}`);
                }
            });

        }
    });

    /*
    * Se valida los campos obligatorios del formulario de actualizacion de tercero
    */
    $('#fromTerceroUpdate').validate({
        focusInvalid: false,
        ignore: "",
        rules: {
            nombre: { required: true },
            apellidos: { required: true },
            direccion: { required: true },
            email: { required: true },
            telefono: { required: true, minlength: 7, number: true }
        },
        submitHandler: function (form) {
            $.post('/Tercero/Update', $('#fromTerceroUpdate').serialize(), function (data) {
                //Una vez guardada la informacion se refresca la tabla para mostrar los nuevos datos
                refreshTablaTerceros();
                //Se envia una notificacion de exito o de error al usuario dependiendo de como salio la transaccion
                if (data.guardo) {
                    toastr.success(`Exito al guardar el tercero: ${$('#nombre').val()} ${$('#apellidos').val()}`);
                    $('#updateTerceroModal').modal("hide");
                    $('#fromTerceroUpdate').trigger("reset");                    
                }
                else {
                    toastr.error(`Error al guardar el tercero: ${$('#nombre').val()} ${$('#apellidos').val()}`);
                }
            });

        }
    });
    refreshTablaTerceros();

    //Se detecta el click el el icono de editar tercero para mostar el modal de cambio de clave con la informacion del usuario
    $('#tercerosRegistrados tbody').on('click', 'a.edit_tercero', function () {
        var tr = $(this).closest('tr');
        var row = tercero.row(tr);
        updateTercero(row.data());
    }); 

    //Se detecta el click el el icono de eliminar tercero para mostar el modal de cambio de clave con la informacion del usuario
    $('#tercerosRegistrados tbody').on('click', 'a.delete_tercero', function () {
        var tr = $(this).closest('tr');
        var row = tercero.row(tr);
        eliminarTercero(row.data());
    }); 
});

function refreshTablaTerceros() {
    showLoaderTercero(true);
    $.get('/Tercero/List', function (tercerosBd) {
        showLoaderTercero(false);
        //Se crea el datatable para darle mas dinamismo a la manera de como se visualizan los datos
        tercero = $('#tercerosRegistrados').DataTable({
            destroy: true,
            responsive: true,
            data: tercerosBd,
            columns: [
                { 'data': 'nombre' },
                { 'data': 'apellidos' },
                { 'data': 'direccion' },
                { 'data': 'email' },
                { 'data': 'telefono' },
                {
                    'searchable': false,
                    'orderable': false,
                    'defaultContent': '<a href="javascript:" class="edit_tercero"><i class="fas fa-edit" title="Editar"></i></a>   <a href="javascript:" class="delete_tercero error"><i class="fas fa-user-minus" title="Eliminar"></i></a>    <a href="javascript:" class="files_tercero"><i class="fas fa-folder" title="Archivos"></i></a>'
                }
            ]
        });

    });
}

//Muestra un loader por si la operacion de listar los terceros demora más de lo estimado
function showLoaderTercero(show) {
    if (show) {
        $('#dvTerceros').hide();
        $('#dvLoaderTercero').show();
    }
    else {
        $('#dvTerceros').show();
        $('#dvLoaderTercero').hide();
    }
}

function updateTercero(data) {
    $('#updateTerceroModal').modal("show");
    $('#titleModalEdit').html(`${data.nombre} ${data.apellidos}`);    
    $('#id2').val(data.id);
    $('#nombre2').val(data.nombre);
    $('#apellidos2').val(data.apellidos);
    $('#direccion2').val(data.direccion);
    $('#email2').val(data.email);
    $('#telefono2').val(data.telefono);
}

function eliminarTercero(data) {
    $('#eliminarTerceroModal').modal("show");
    $('#titleModalEliminar').html(`${data.nombre} ${data.apellidos}`);
    $('#nombreEliminar').html(`${data.nombre} ${data.apellidos}`);
    $('#id3').val(data.id);
    $('#nombre3').val(data.nombre);
    $('#apellidos3').val(data.apellidos);
}

function confirmarEliminacion() {
    var id = $('#id3').val();
    $.get(`/Tercero/Delete?id=${id}`, function (data) {
        $('#eliminarTerceroModal').modal("hide");
        if (data.elimino) {
            refreshTablaTerceros();
            toastr.success(`Exito al eliminar el tercero: ${$('#nombre3').val()} ${$('#apellidos3').val()}`);
        }
        else {
            toastr.error(`Error al eliminar el tercero: ${$('#nombre3').val()} ${$('#apellidos3').val()}`);
        }
    });
}