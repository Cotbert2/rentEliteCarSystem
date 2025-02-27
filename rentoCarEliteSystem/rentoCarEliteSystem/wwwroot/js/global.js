document.getElementById('logoutnButton').addEventListener('click', (event) => {
    confirmationModal('Cerrar sesion', 'Desea cerrar sesion?',
        'Cerrar sesion', 'Cancelar', '#3085d6', '#d33',
        () => {
        localStorage.removeItem('currentSession');
        //wait 2 seconds for the session to be removed
        setTimeout(() => {
            window.location.href = '/Employee/login';
        }, 2000);
    });
});