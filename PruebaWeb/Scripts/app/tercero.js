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
    refreshTablaTerceros();
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
                    'defaultContent': '<a href="javascript:" class="edit_tercero"><i class="fas fa-edit" title="Editar"></i></a><a href="javascript:" class="delete_tercero"><i class="fas fa-user-minus" title="Eliminar"></i></a>'
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