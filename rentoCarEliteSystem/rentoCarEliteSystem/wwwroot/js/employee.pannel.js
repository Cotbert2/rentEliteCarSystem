
//check if user is root or administrator

const currentUser  = JSON.parse(localStorage.getItem('currentSession'));


console.log('', currentUser.position);

if (!(currentUser.position == 'root' || currentUser.position == 'admin')) {
   document.getElementById('infoPannel').innerText = 'No cuentas con permisos de administrador | Consulta con uno para saber mas';
}

