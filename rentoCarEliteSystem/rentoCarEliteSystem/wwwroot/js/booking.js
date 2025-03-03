//global variables
let currentCustomerEdit;
let currentCustomerDelete;
let tableData;
let myModal;

let deleteModal;


window.onload = () => {
    //singleton modal, beacuse we only need one modal
    //bootstrap only allows one modal at a time
    myModal = new bootstrap.Modal(document.getElementById('formModal'));
    deleteModal = new bootstrap.Modal(document.getElementById('deleteUserModal'));
}


//Oninit
getAllBookings((data) => {
    tableData = data;
    renderTableFun(data);
});

//utils



const renderTableFun = (data) => {
    
    renderTable({
        headers: ['Fecha inicio', 'Fecha devolucion', 'Cliente', 'Correo', 'vehiculo', 'Tipo de seguro', 'Tipo de pago', 'Total'],

        keys: ['startDate', 'endDate', 'customer', 'email', 'vehicle', 'insurance', 'payment', 'total'],
        data : data,
        isEditable: false,
        isDeletable: true,
        deleteFunction: 'deleteBooking',
    });
}

//Event listeners


document.getElementById('downloadCsv').addEventListener('click', () => {
    downloadCSVFileFromTable('dataTable', 'bookings');
});



document.getElementById('addNewBooking').addEventListener('click', () => {
    //redirect to booking page
    window.location.href = '/Home/newSell';
});




document.getElementById('deletCustomerButton').addEventListener('click', () => {
    deleteCurrentBooking();
});

document.getElementById('closeFormModalButton').addEventListener('click', () => {
    document.getElementById('userForm').reset();
    myModal.hide();
});



//CRUD operations


const deleteBooking = (currentCustomer) => {
    const dataCustomer = JSON.parse(decodeURIComponent(currentCustomer));
    console.log('data to delete', dataCustomer);

    currentCustomerDelete = dataCustomer;
    document.getElementById('confirmationDeletrUserLabel').textContent = `Estas seguro que deseas cancelar esta reserva?`;
    deleteModal.show();
}


const deleteCurrentBooking = () => {

    console.log('should delete customer', currentCustomerDelete);

    deleteModal.hide();

    deleteBookingService(currentCustomerDelete.id, (response) => {
        console.log('data froms service', response);

        (response.code == 1) ? alert('Reserva eliminada con exito') : alert('Error eliminando la reserva');

        getAllBookings((data) => {
            tableData = data;
            renderTableFun(data);
        });
    });
}