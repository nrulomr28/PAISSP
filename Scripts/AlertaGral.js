
function error() {
    Swal.fire({
        icon: 'error',
        title: 'Error',
        text: 'El usuario y la contraseña no son correctos',
    })
};
function error_gral() {
    Swal.fire({
        icon: 'error',
        title: 'Error al guardar los datos',

    })
};
function success() {
    Swal.fire({
        icon: 'success',
        title: 'Datos Guardados con exito',
    })
};

function AlertaContraseña() {
    Swal.fire({
        icon: 'warning',
        title: 'Datos obligatorios',
        text: 'Es necesaria la contraseña',
    })
};

function usuarioRegistradoconexito() {
    Swal.fire({
        icon: 'success',
        title: 'Datos guardados',
        text: 'El usuarios se registro con extio',
    })
};

function caracterescontraseña() {
    Swal.fire({
        icon: 'info',
        title: 'La contraseña como minimo debe de tener 8 caracteres',
    })
};

//alertas de roles 

function Seagrego_usuario_al_rol() {
    Swal.fire({
        icon: 'success',
        title: 'EL usuario se agrego al rol.',
    })
};


function Sequitoel_usuario_del_rol() {
    Swal.fire({
        icon: 'warning',
        title: 'EL usuario se quito del rol.',
    })
};

function registro_user(textoValidacion) {

    console.log(textoValidacion);
    Swal.fire({
        icon: 'warning',
        title: 'Campos obligatorios',
        html: textoValidacion,
    })
};
