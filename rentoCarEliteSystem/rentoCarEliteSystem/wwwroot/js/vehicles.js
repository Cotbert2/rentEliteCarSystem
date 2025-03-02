//global variables
let currentVehicleEdit;
let currentVehicleDelete;
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
getAllVehicles((data) => {
    tableData = data;
    renderTableFun(data);
});

//utils

const validateCreationEditionForm = () => {

    const brand = document.getElementById('brand').value;
    const model = document.getElementById('model').value;
    const year = document.getElementById('year').value;
    const dailyPrice = document.getElementById('dailyPrice').value;
    const photoURL = document.getElementById('photoURL').value;


    if (!brand || !model || !year || !dailyPrice || !photoURL) {
        alert('All fields are required');
        return false;
    }

    if (!validateNumber(year)) {
        alert('Year must be a number');
        return false;
    }

    if (!validatePrice(dailyPrice)) {
        alert('Price must be a number');
        return false;
    }

    if(!validateUrl(photoURL)){
        alert('Photo URL is not valid');
        return false
    }

    return true;
}


const renderTableFun = (data) => {
    
    renderTable({
        headers: ['Id', 'Año', 'Marca', 'Modelo', 'Precio por dia', 'Estado'],
        keys: ['vehicleId', 'vehicleYear', 'brand', 'model', 'price', 'status'],
        data : data,
        isEditable: true,
        isDeletable: true,
        showPhotoField : true,
        editFunction: 'editVehicle',
        deleteFunction: 'deleteVehicle',
    });
}

//Event listeners


document.getElementById('downloadCsv').addEventListener('click', () => {
    downloadCSVFileFromTable('dataTable', 'vehicles');
});


document.getElementById('saveCustomerButton').addEventListener('click', (event) => {
    event.preventDefault();
    saveEditedVehicle();

});

document.getElementById('addNewCustomerButton').addEventListener('click', () => {
    myModal.show();
    document.getElementById('saveCustomerButton').style.display = 'none';
    document.getElementById('addVehicleButton').style.display = 'block';
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


document.getElementById('addVehicleButton').addEventListener('click', () => {


    if (!validateCreationEditionForm()) return;

    let myForm = new FormData(document.getElementById('userForm'));

    const dataToSent = {
        brand: myForm.get('brand'),
        model: myForm.get('model'),
        vehicleYear: myForm.get('year'),
        price: myForm.get('dailyPrice'),
        photo: myForm.get('photoURL'),
    }

    console.log('data to sent', dataToSent);


    createVehicle(dataToSent, (data) => {
        console.log('data from createVehicle', data);
        if (data.code > 1) {
            alert('Vehiculo creado exitosamente');
            myModal.hide();
        } else {
            alert('Ocurrio un error al crear el vehiculo');
        }

        getAllVehicles((data) => {
            tableData = data;
            renderTableFun(data);
        });
    });
    console.log('add customer!!!asdas');

});


document.getElementById('deletCustomerButton').addEventListener('click', () => {
    deleteCurrentVehicle();
});

document.getElementById('closeFormModalButton').addEventListener('click', () => {
    document.getElementById('userForm').reset();
    myModal.hide();
});



//CRUD operations

const saveEditedVehicle = () => {
    console.log('date to edit!!!', currentVehicleEdit);

    if (!validateCreationEditionForm()) return;

    currentVehicleEdit.brand = document.getElementById('brand').value;
    currentVehicleEdit.model = document.getElementById('model').value;
    currentVehicleEdit.vehicleYear = document.getElementById('year').value;
    currentVehicleEdit.price = document.getElementById('dailyPrice').value;
    currentVehicleEdit.photo = document.getElementById('photoURL').value;


    bodyFetch('/Vehicle/updateVehicle', currentVehicleEdit, 'json', (response) => {
        console.log('data froms service', response);
        
        if (response.code == 1) {
            alert('Se ha actualizado el Vehiculo');
            myModal.hide();
        } else {
            alert('Error actualizando el Vehiculo');
        }
        getAllVehicles((data) => {
            tableData = data;
            renderTableFun(data);

        });


    }, 'PUT');
}



const editVehicle = (vehicle) => {
    document.getElementById('saveCustomerButton').style.display = 'block';
    document.getElementById('addVehicleButton').style.display = 'none';

    let dataVehicle = JSON.parse(decodeURIComponent(vehicle));
    currentVehicleEdit = dataVehicle;
    console.log(dataVehicle);

    myModal.show();


    document.getElementById('brand').value = currentVehicleEdit.brand;
    document.getElementById('model').value = currentVehicleEdit.model;
    document.getElementById('year').value = currentVehicleEdit.vehicleYear;
    document.getElementById('dailyPrice').value = currentVehicleEdit.price;
    document.getElementById('photoURL').value = currentVehicleEdit.photo;

}


const deleteVehicle = (currentVehicle) => {
    const dataVehicle = JSON.parse(decodeURIComponent(currentVehicle));
    console.log('data to delete', dataVehicle);

    currentVehicleDelete = dataVehicle;
    document.getElementById('confirmationDeletrUserLabel').textContent = `Estas seguro que deseas eliminar al vehiculo  ${dataVehicle.brand} ${dataVehicle.model} ?`;
    //put the image in the modal
    document.getElementById('confirmationDeletrUserLabel').insertAdjacentHTML('beforeend', `<img src="${dataVehicle.photo}" style="width: 100%"/>`);
    deleteModal.show();
}


const deleteCurrentVehicle = () => {

    console.log('should delete vehicle', currentVehicleDelete);

    deleteModal.hide();

    deleteVehicleService(currentVehicleDelete.vehicleId, (response) => {
         console.log('data froms service', response);

         (response.code == 1) ? alert('Vehiculo eliminado con exito') : alert('Error eliminando el Vehiculo');

         getAllVehicles((data) => {
             tableData = data;
             renderTableFun(data);
         });
     });
}



