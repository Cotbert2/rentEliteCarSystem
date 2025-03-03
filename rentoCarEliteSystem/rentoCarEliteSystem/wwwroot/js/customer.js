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
getAllCustomers((data) => {
    tableData = data;
    renderTableFun(data);
});

//utils

const validateCreationEditionForm = () => {
    const firstName = document.getElementById('firstName').value;
    const lastName = document.getElementById('lastName').value;
    const phone = document.getElementById('phone').value;
    const email = document.getElementById('email').value;

    if (!firstName || !lastName || !phone || !email) {
        warningToast('All fields are required');
        return false;
    }

    if (!validateEmail(email)) {
        warningToast('Invalid email');
        return false;
    }

    if (!validatePhone(phone)) {
        warningToast('Invalid phone');
        return false;
    }

    return true;
}


const renderTableFun = (data) => {
    
    renderTable({
        headers: ['Id', 'Nombre', 'Apellido', 'Telefono', 'email'],
        keys: ['id', 'firstName', 'lastName', 'phone', 'email'],
        data : data,
        isEditable: true,
        isDeletable: false,
        editFunction: 'editCustomer',
        deleteFunction: 'deleteCustomer',
    });
}

//Event listeners


document.getElementById('downloadCsv').addEventListener('click', () => {
    downloadCSVFileFromTable('dataTable', 'customers');
});


document.getElementById('saveCustomerButton').addEventListener('click', (event) => {
    event.preventDefault();
    saveEditedCustomer();

});

document.getElementById('addNewCustomerButton').addEventListener('click', () => {
    myModal.show();
    document.getElementById('saveCustomerButton').style.display = 'none';
    document.getElementById('addCustomerButton').style.display = 'block';
});


document.getElementById('userForm').addEventListener('submit', (event) => {
    event.preventDefault();
    const formData = {
        firstName: document.getElementById('firstName').value,
        lastName: document.getElementById('lastName').value,
        phone: document.getElementById('phone').value,
        email: document.getElementById('email').value,
    }
    createCustomer(formData, (data) => {
        console.log('data from createCustomer', data);
        getAllCustomers((data) => {
            tableData = data;
            renderTableFun(data);
        });

    });
});


document.getElementById('searchInput').addEventListener('keyup', (event) => {
    const searchValue = event.target.value.trim().toLowerCase();
    if (!tableData || !Array.isArray(tableData)) return;

    console.log('value to search', searchValue);

    const filteredData = tableData.filter((currentData) =>
        Object.values(currentData).some(value =>
            value.toString().toLowerCase().includes(searchValue))
    );

    console.log('filtered value', filteredData);
    renderTableFun(filteredData);
});


document.getElementById('addCustomerButton').addEventListener('click', () => {


    if (!validateCreationEditionForm()) return;

    let myForm = new FormData(document.getElementById('userForm'));

    const dataToSent = {
        firstName: myForm.get('firstName'),
        lastName: myForm.get('lastName'),
        phone: myForm.get('phone'),
        email: myForm.get('email')
    }

    console.log('data to sent', dataToSent);


    createCustomer(dataToSent, (data) => {
        console.log('data from createCustomer', data);
        if (data.code > 1) {
            successToast('Usuario creado exitosamente');
            myModal.hide();
        } else if (data.code == -1 && data.message.includes('UNIQUE KEY')) {
            errorToast('El email ya existe');
        } else {
            errorToast('Ocurrio un error al crear el usuario');
        }

        getAllCustomers((data) => {
            tableData = data;
            renderTableFun(data);
        });
    });
    console.log('add customer!!!asdas');

});


document.getElementById('deletCustomerButton').addEventListener('click', () => {
    deleteCurrentCustomer();
});

document.getElementById('closeFormModalButton').addEventListener('click', () => {
    document.getElementById('userForm').reset();
    myModal.hide();
});



//CRUD operations

const saveEditedCustomer = () => {
    console.log('date to edit!!!', currentCustomerEdit);

    if (!validateCreationEditionForm()) return;

    currentCustomerEdit.firstName = document.getElementById('firstName').value;
    currentCustomerEdit.lastName = document.getElementById('lastName').value;
    currentCustomerEdit.phone = document.getElementById('phone').value;
    currentCustomerEdit.email = document.getElementById('email').value;

    bodyFetch('/Customer/updateCustomer', currentCustomerEdit, 'json', (response) => {
        console.log('data froms service', response);
        
        if (response.code == 1) {
            successToast('Se ha actualizado el usuario');
            myModal.hide();
        } else {
            successToast('Error actualizando el usuario');
        }
        getAllCustomers((data) => {
            tableData = data;
            renderTableFun(data);

        });


    }, 'PUT');
}



const editCustomer = (customer) => {
    //add a display block class
    document.getElementById('saveCustomerButton').style.display = 'block';
    document.getElementById('addCustomerButton').style.display = 'none';

    let dataCustomer = JSON.parse(decodeURIComponent(customer));
    currentCustomerEdit = dataCustomer;
    console.log(dataCustomer);

    myModal.show();


    document.getElementById('firstName').value = currentCustomerEdit.firstName;
    document.getElementById('lastName').value = currentCustomerEdit.lastName;
    document.getElementById('phone').value = currentCustomerEdit.phone;
    document.getElementById('email').value = currentCustomerEdit.email;
}


const deleteCustomer = (currentCustomer) => {
    const dataCustomer = JSON.parse(decodeURIComponent(currentCustomer));
    console.log('data to delete', dataCustomer);

    currentCustomerDelete = dataCustomer;
    document.getElementById('confirmationDeletrUserLabel').textContent = `Estas seguro que deseas eliminar al usuario ${currentCustomerDelete.firstName} ${currentCustomerDelete.lastName}`;
    deleteModal.show();
}


const deleteCurrentCustomer = () => {

    console.log('should delete customer', currentCustomerDelete);

    deleteModal.hide();

    deleteCustomerService(currentCustomerDelete.id, (response) => {
        console.log('data froms service', response);

        (response.code == 1) ? successToast('Usuario eliminado con exito') : errorToast('Error eliminando el usuario');

        getAllCustomers((data) => {
            tableData = data;
            renderTableFun(data);
        });
    });
}



