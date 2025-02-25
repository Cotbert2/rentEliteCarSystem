document.getElementById('downloadCsv').addEventListener('click', () => {
    downloadCSVFileFromTable('dataTable', 'customers');
});

let tableData;


const renderTableFun = (data) => {
    
    renderTable({
        headers: ['id', 'first name', 'last name', 'phone', 'email'],
        keys: ['id', 'firstName', 'lastName', 'phone', 'email'],
        data : data,
        isEditable: true,
        isDeletable: true,
        editFunction: 'editCustomer',
        deleteFunction: 'deleteCustomer',
    });
}


getAllCustomers((data) => {
    tableData = data;
    renderTableFun(data);
});


let currentCustomerEdit;
let currentCustomerDelete;


const saveEditedCustomer = () => {
    console.log('date to edit!!!', currentCustomerEdit);

    currentCustomerEdit.firstName = document.getElementById('firstName').value;
    currentCustomerEdit.lastName = document.getElementById('lastName').value;
    currentCustomerEdit.phone = document.getElementById('phone').value;
    currentCustomerEdit.email = document.getElementById('email').value;

    bodyFetch('/Customer/updateCustomer', currentCustomerEdit, 'json', (response) => {
        console.log('data froms service', response);

        getAllCustomers((data) => {
            tableData = data;
            renderTableFun(data);
        });


    }, 'PUT');
}



const editCustomer = (customer) => {

    let dataCustomer = JSON.parse(decodeURIComponent(customer));
    currentCustomerEdit = dataCustomer;
    console.log(dataCustomer);

    let myModal = new bootstrap.Modal(document.getElementById('formModal'));
    myModal.show();


    document.getElementById('firstName').value = currentCustomerEdit.firstName;
    document.getElementById('lastName').value = currentCustomerEdit.lastName;
    document.getElementById('phone').value = currentCustomerEdit.phone;
    document.getElementById('email').value = currentCustomerEdit.email;
}


document.getElementById('saveCustomerButton').addEventListener('click', (event) => {
    event.preventDefault();
    saveEditedCustomer();

})


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
        (data.code > 1) ? alert('Usuario creado exitosamente') : alert("Ocurrio un error al crear el usuario");
        getAllCustomers((data) => {
            tableData = data;
            renderTableFun(data);
        });
    });

});


const deleteCurrentCustomer = () => {

    console.log('should delete customer', currentCustomerDelete);

    deleteCustomerService(currentCustomerDelete.id, (response) => {
        console.log('data froms service', response);

        (response.code == 1) ? alert('Customer deleted successfully') : alert('Error deleting customer');

        getAllCustomers((data) => {
            tableData = data;
            renderTableFun(data);
        });
    });
}



const deleteCustomer = (currentCustomer) => {
    const dataCustomer = JSON.parse(decodeURIComponent(currentCustomer));
    console.log('data to delete', dataCustomer);

    currentCustomerDelete = dataCustomer;
    document.getElementById('confirmationDeletrUserLabel').textContent = `Are you sure you want to delete ${currentCustomerDelete.firstName} ${currentCustomerDelete.lastName}`;
    let deleteModal = new bootstrap.Modal(document.getElementById('deleteUserModal'));
    deleteModal.show();
}

document.getElementById('deletCustomerButton').addEventListener('click', () => {
    deleteCurrentCustomer();
});