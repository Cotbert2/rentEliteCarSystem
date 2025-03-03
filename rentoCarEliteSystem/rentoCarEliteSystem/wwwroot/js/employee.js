window.onload = () => {
    //check for the current session
    const currentSession = localStorage.getItem('currentSession');
    if (currentSession) {
        window.location.href = '/Home/Index';
    }

}

document.getElementById('loginButton').addEventListener('click', (event) => {
    event.preventDefault();
    const myForm = new FormData(document.getElementById('formLogin'));
    let dataToSend = {};
    dataToSend.email = myForm.get('username');
    dataToSend.password = myForm.get('password');
    console.log('data to send', dataToSend);

    if (!dataToSend.email || !dataToSend.password) {
        warningToast('Todos los campos son requeridos');
        return;
    }

    if (!validateEmail(dataToSend.email)) {
        warningToast('Ingrese un correo valido');
        return;
    }


    login(dataToSend, (data) => {
        console.log('response form data service', data);
        errorToast( (data.employeeID == 0)? 'Usuario o contrasena invalidos' : 'Inicio de sesion exitoso')

        //load data into the local storage
        if (data.employeeID != 0) {
            localStorage.setItem('currentSession', JSON.stringify(data));
            window.location.href = '/Home/Index';
        }
    })



});

