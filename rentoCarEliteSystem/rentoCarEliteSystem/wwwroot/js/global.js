document.getElementById('logoutButton').addEventListener('click', (event) => {
    confirmationModal('Cerrar sesion', 'Desea cerrar sesion?',
        'Cerrar sesion', 'Cancelar', '#d33', '#3085d6',
        () => {
        localStorage.removeItem('currentSession');
        //wait 2 seconds for the session to be removed
        setTimeout(() => {
            window.location.href = '/Employee/login';
        }, 2000);
    });
});



const confirmationModal = (title, text,
    confirmLabel,
    cancelLabel,
    confirmationColor, cancelColor,
    confirmationCallback) => {

    confirmationColor = (confirmationColor == undefined) ? '#3085d6' : confirmationColor;
    cancelColor = (cancelColor == undefined) ? '#d33' : cancelColor;

    Swal.fire({
        title,
        text,
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: confirmationColor,
        cancelButtonColor: cancelColor,
        confirmButtonText: confirmLabel,
        cancelButtonText: cancelLabel,
    }).then((result) => {
        if (result.isConfirmed) {
            confirmationCallback();
        }
    });
} 