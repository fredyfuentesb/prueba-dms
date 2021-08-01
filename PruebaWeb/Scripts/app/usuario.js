var usuarios; //Esta variable se usa para guardar referencia de la tabla
$(document).ready(function () {
    /*
     * Se valida los campos obligatorios del formulario de creacion de usuario
     */
    $('#fromUsuario').validate({
        focusInvalid: false,
        ignore: "",
        rules: {
            nombre: { required: true },
            apellidos: { required: true },
            email: { required: true },
            usuario: { required: true },
            clave: { required: true, minlength: 5 },
            repite_clave: { minlength: 5, equalTo: "#clave"}
        },
        submitHandler: function (form) {
            $.post('/Usuario/Save', $('#fromUsuario').serialize(), function (data) {
                //Una vez guardada la informacion se refresca la tabla para mostrar los nuevos datos
                refreshTablaUsuarios();
                //Se envia una notificacion de exito o de error al usuario dependiendo de como salio la transaccion
                if (data.guardo) {
                    toastr.success(`Exito al guardar el usuario: ${$('#usuario').val()}`);                    
                    $('#fromUsuario').trigger("reset");
                }
                else {
                    toastr.error(`Error al guardar el usuario: ${$('#usuario').val()}`);
                }
            });
            
        }
    });

    /*
     * Se valida los campos obligatorios del formulario de actualizacion de usuario
     */
    $('#fromUsuarioUpdate').validate({
        focusInvalid: false,
        ignore: "",
        rules: {            
            usuario: { required: true },
            activo: { required: true },
        },
        submitHandler: function (form) {
            $.post('/Usuario/Update', $('#fromUsuarioUpdate').serialize(), function (data) {
                //Una vez guardada la informacion se refresca la tabla para mostrar los nuevos datos
                refreshTablaUsuarios();
                //Se envia una notificacion de exito o de error al usuario dependiendo de como salio la transaccion
                if (data.guardo) {
                    toastr.success(`Exito al guardar el usuario: ${$('#usuario2').val()}`);
                    $('#updateUsuarioModal').modal("hide");
                    $('#fromUsuarioUpdate').trigger("reset");
                }
                else {
                    toastr.error(`Error al guardar el usuario: ${$('#usuario2').val()}`);
                }
            });
        }
    });

    /*
     * Se valida los campos obligatorios del formulario de cambio de clave de usuario
     */
    $('#fromCambiarClave').validate({
        focusInvalid: false,
        ignore: "",
        rules: {
            clave: { required: true, minlength: 5 },
            repite_clave: { minlength: 5, equalTo: "#clave3" }
        },
        submitHandler: function (form) {
            $.post('/Usuario/CambiarClave', $('#fromCambiarClave').serialize(), function (data) {
                //Una vez guardada la informacion se refresca la tabla para mostrar los nuevos datos
                refreshTablaUsuarios();
                //Se envia una notificacion de exito o de error al usuario dependiendo de como salio la transaccion
                if (data.guardo) {
                    toastr.success(`Exito al cambiar la clave del usuario: ${$('#usuario3').val()}`);
                    $('#cambiarClaveModal').modal("hide");
                    $('#fromCambiarClave').trigger("reset");
                }
                else {
                    toastr.error(`Error al cambiar la clave del usuario: ${$('#usuario3').val()}`);
                }
            });
        }
    });

    refreshTablaUsuarios();
    //Se detecta el click el el icono de editar usuario para mostar el modal con la informacion del usuario
    $('#usuariosRegistrados tbody').on('click', 'a.edit_usuario', function () {
        var tr = $(this).closest('tr');
        var row = usuarios.row(tr);        
        updateUsuario(row.data());        
    });    

    //Se detecta el click el el icono de editar usuario para mostar el modal de cambio de clave con la informacion del usuario
    $('#usuariosRegistrados tbody').on('click', 'a.cambiar_clave', function () {
        var tr = $(this).closest('tr');
        var row = usuarios.row(tr);
        cambiarClave(row.data());
    }); 
});

function refreshTablaUsuarios() {
    showLoaderUsuario(true);
    $.get('/Usuario/List', function (usuariosBd) {
        showLoaderUsuario(false);
        //Se crea el datatable para darle mas dinamismo a la manera de como se visualizan los datos
        usuarios = $('#usuariosRegistrados').DataTable({
            destroy: true,
            responsive: true,
            data: usuariosBd.usuarios,
            columns: [
                { 'data': 'nombre' },
                { 'data': 'apellidos' },
                { 'data': 'email' },
                { 'data': 'usuario' },
                {
                    'data': 'activo',
                    'render': function (data) {
                        return (data) ? `<span class="badge badge-success">Activo</span>` : `<span class="badge badge-danger">Inactivo</span>`
                    }
                },
                {
                    'searchable': false,
                    'orderable': false,
                    'defaultContent': '<a href="javascript:" class="edit_usuario"><i class="fas fa-edit" title="Editar"></i></a><a href="javascript:" class="cambiar_clave"><i class="fas fa-key" title="Cambiar contraseña"></i></a>'
                }
            ]
        });
    })
}

//Muestra un loader por si la operacion de listar los usuarios demora más de lo estimado
function showLoaderUsuario(show) {
    if (show) {
        $('#dvUsuarios').hide();
        $('#dvLoaderUsuario').show();
    }
    else {
        $('#dvUsuarios').show();
        $('#dvLoaderUsuario').hide();
    }
}
//Muestra el modal con los datos que se van a editar
function updateUsuario(data) {
    console.log(data)
    $('#updateUsuarioModal').modal("show");
    $('#titleModalEdit').html(data.usuario);
    $('#nombre2').val(data.nombre);
    $('#apellidos2').val(data.apellidos);
    $('#email2').val(data.email);
    $('#usuario2').val(data.usuario);
    $('#id').val(data.id);
    $('#activo').val(`${data.activo}`);
    $('#id_tercero').val(data.id_tercero);
}

function cambiarClave(data) {
    $('#cambiarClaveModal').modal("show");
    $('#titleModalCambiar').html(data.usuario);        
    $('#id3').val(data.id);
    $('#usuario3').val(data.usuario);
}