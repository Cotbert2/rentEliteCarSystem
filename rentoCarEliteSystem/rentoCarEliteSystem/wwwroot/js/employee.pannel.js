
//check if user is root or administrator

const currentUser  = JSON.parse(localStorage.getItem('currentSession'));


console.log('', currentUser.position);

if (!(currentUser.position == 'root' || currentUser.position == 'admin')) {
   document.getElementById('infoPannel').innerText = 'No cuentas con permisos de administrador | Consulta con uno para saber mas';
   document.getElementById('employeePannel').style.display = 'none';
}


















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
getAllEmployees((data) => {
    tableData = data;
    renderTableFun(data);
});

//utils

const validateCreationEditionForm = () => {
    const firstName = document.getElementById('firstName').value;
    const lastName = document.getElementById('lastName').value;
    const phone = document.getElementById('phone').value;
    const email = document.getElementById('email').value;
    const jobTitle = document.getElementById('jobTitle').value;
    const password = document.getElementById('password').value;
    const confirmPassword = document.getElementById('confirmPassword').value;

    if (!firstName || !lastName || !phone || !email || !jobTitle || !password || !confirmPassword) {
        warningToast('Todos los campos son requeridos');
        return false;
    }

    if (password != confirmPassword) {
        warningToast('Las contrasenas no coinciden');
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

    if (!validateName(firstName) || !validateName(lastName)) {
        warningToast('Invalid name');
        return false;
    }

    return true;
}


const renderTableFun = (data) => {

    renderTable({
        headers: ['Id', 'Nombre', 'Apellido', 'Puesto', 'Email', 'Telefono'],
        keys: ['employeeID', 'firstName', 'lastName', 'position', 'email', 'phone'],
        data: data,
        isEditable: true,
        isDeletable: true,
        editFunction: 'editEmployee',
        deleteFunction: 'deleteCustomer',
    });
}

//Event listeners


document.getElementById('downloadCsv').addEventListener('click', () => {
    downloadCSVFileFromTable('dataTable', 'employee');
});


document.getElementById('saveEmployeeButton').addEventListener('click', (event) => {
    event.preventDefault();
    saveEditedEmployee();

});

document.getElementById('addNewCustomerEmployee').addEventListener('click', () => {
    myModal.show();
    document.getElementById('saveEmployeeButton').style.display = 'none';
    document.getElementById('addEmployeeButton').style.display = 'block';
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
        getAllEmployees((data) => {
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


document.getElementById('addEmployeeButton').addEventListener('click', () => {


    if (!validateCreationEditionForm()) return;

    let myForm = new FormData(document.getElementById('userForm'));




    const dataToSent = {
        firstName: myForm.get('firstName'),
        lastName: myForm.get('lastName'),
        phone: myForm.get('phone'),
        position: myForm.get('jobTitle'),
        email: myForm.get('email'),
        password: myForm.get('confirmPassword'),
    }

    console.log('data to sent', dataToSent);


    createEmployeeService(dataToSent, (data) => {
        console.log('data from createCustomer', data);
        if (data.code > 1) {
            successToast('Empleado creado exitosamente');
            myModal.hide();
        } else if (data.code == -1 && data.message.includes('UNIQUE KEY')) {
            errorToast('El email ya existe');
        } else {
            errorToast('Ocurrio un error al crear el Empleado');
        }

        getAllEmployees((data) => {
            tableData = data;
            renderTableFun(data);
        });
    });
});


document.getElementById('deletCustomerButton').addEventListener('click', () => {
    deeleteCurrentEployee();
});

document.getElementById('closeFormModalButton').addEventListener('click', () => {
    document.getElementById('userForm').reset();
    myModal.hide();
});



//CRUD operations

const saveEditedEmployee = () => {
    console.log('date to edit!!!', currentCustomerEdit);

    if (!validateCreationEditionForm()) return;

    currentCustomerEdit.firstName = document.getElementById('firstName').value;
    currentCustomerEdit.lastName = document.getElementById('lastName').value;
    currentCustomerEdit.phone = document.getElementById('phone').value;
    currentCustomerEdit.email = document.getElementById('email').value;
    currentCustomerEdit.position = document.getElementById('jobTitle').value;
    currentCustomerEdit.password = document.getElementById('password').value;

    bodyFetch('/Employee/updateEmployee', currentCustomerEdit, 'json', (response) => {
        console.log('data froms service', response);

        if (response.code == 1) {
            successToast('Se ha actualizado el Empleado');
            myModal.hide();
        } else {
            errorToast('Error actualizando el Empleado');
        }
        getAllEmployees((data) => {
            tableData = data;
            renderTableFun(data);

        });


    }, 'PUT');
}



const editEmployee = (customer) => {
    //add a display block class
    document.getElementById('saveEmployeeButton').style.display = 'block';
    document.getElementById('addEmployeeButton').style.display = 'none';

    let dataCustomer = JSON.parse(decodeURIComponent(customer));
    currentCustomerEdit = dataCustomer;
    console.log(dataCustomer);

    myModal.show();


    document.getElementById('firstName').value = currentCustomerEdit.firstName;
    document.getElementById('lastName').value = currentCustomerEdit.lastName;
    document.getElementById('phone').value = currentCustomerEdit.phone;
    document.getElementById('email').value = currentCustomerEdit.email;
    document.getElementById('jobTitle').value = currentCustomerEdit.position;
}


const deleteCustomer = (currentCustomer) => {
    const dataCustomer = JSON.parse(decodeURIComponent(currentCustomer));
    console.log('data to delete', dataCustomer);
    //position lo lower case


    currentCustomerDelete = dataCustomer;


    if (currentCustomerDelete.position.toLowerCase() == 'root') {
        errorToast('No puedes eliminar al administrador principal');
        return;
    }

    document.getElementById('confirmationDeletrUserLabel').textContent = `Estas seguro que deseas eliminar al Empleado ${currentCustomerDelete.firstName} ${currentCustomerDelete.lastName}`;
    deleteModal.show();
}


const deeleteCurrentEployee = () => {

    console.log('should delete customer', currentCustomerDelete);

    deleteModal.hide();

    deleteEmployeeService(currentCustomerDelete.employeeID, (response) => {
        console.log('data froms service', response);

        (response.code == 1) ? successToast('Empleado eliminado con exito') : errorToast('Error eliminando el Empleado');

        getAllEmployees((data) => {
            tableData = data;
            renderTableFun(data);
        });
    });
}