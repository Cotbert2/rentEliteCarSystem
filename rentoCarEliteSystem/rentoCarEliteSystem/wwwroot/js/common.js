// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code

const completedUrl = window.location.protocol + '//' + window.location.host;





const bodyFetch = async (endpoint, dataToSend, responseType, customCallback, method) => {

    try {

        const res = await fetch(completedUrl + endpoint, {
            headers: {
                'Content-Type': 'application/json'
            },
            method: method,
            body: JSON.stringify(dataToSend)
        });

        console.log('body ', JSON.stringify(dataToSend));


        let data;
        if (responseType === 'json') {
            data = await res.json();
            customCallback(data);
        }
        else if (responseType == 'text') {
            data = await res.text();
            customCallback(data);
        } else {
            throw new Error('Invalid response type');
        }

        customCallback(data);
    }
    catch (error) {
        console.log(error);
    }


}


const getFetch = async (customUrl, responseType, customCallback, method) => {

    method = (method == undefined) ?  'GET' : method;

    try {
        const res = await fetch(completedUrl + customUrl, {
            method: method,
            headers: {
                'Content-Type': 'application/json'
            }
        });
        let data;
        if (responseType === "json") {
            data = await res.json();
        } else if (responseType == "text") {
            data = await res.text();
        } else {
            throw new Error("Invalid response type");
        }

        customCallback(data);

    }
    catch (error) {
        console.log(error);
    }
}


const generateTable = (data) => {
    if (data.isEditable == undefined) {
        data.isEditable = false;
    }

    if (data.isDeletable == undefined) {
        data.isDeletable = false;
    }

    if(data.showPhotoField == undefined){
        data.showPhotoField = false;
    }

    let content = '';


    content += `<table id="dataTable" class="table table-striped table-bordered">`;
    content += `<thead class="table-dark">`;
    content += `<tr>`;
    data.headers.forEach(currentHeader => {
        content += `<th>${currentHeader}</th>`;
    });

    if (data.showPhotoField) {
        content += `<th>Imagen</th>`;
    }

    if (data.isEditable || data.isDeletable) {
        content += `<th>Acciones</th>`;
    }



    content += `</tr>`;
    content += `</thead>`;

    content += `<tbody id="tableBody">`;

    data.data.forEach(currentData => {

        content += `<tr>`;
        data.keys.forEach(currentKey => {
            content += `<td>${currentData[currentKey]}</td>`;
        });

        if (data.showPhotoField) {
            content += `<td><img src="${currentData.photo}" class="img-fluid"
            style="height: 50px;"
            alt="photo"></td>`;
        }


        if (data.isDeletable || data.isEditable) {
            content += `<td class="d-flex align-items-center justify-content-between ">`
            if (data.isEditable)
                content += `<button class="btn btn-primary" onclick="${data.editFunction}('${encodeURIComponent(JSON.stringify(currentData))}')">
                    <i class="fa-solid fa-pen-to-square"></i>
                </button>`;

            if (data.isDeletable) content += `<button class="btn btn-danger" onclick="${data.deleteFunction}('${encodeURIComponent(JSON.stringify(currentData))}')">
            <i class="fa-solid fa-trash"></i>
            </button>`;

            content += `</td>`;
        }


    });



    content += `</tbody>`;

    content += `</table>`;

    content += `</div>`;

    return content;
}


const renderTable = (data) => {
    const table = generateTable(data);
    document.getElementById('tableContainer').innerHTML = table;
    $(document).ready(function () {
        $('#dataTable').DataTable({
            "paging": true,
            "lengthMenu": [5, 10, 25, 50],
            "searching": true,
            "ordering": true,
            "info": true,
            "language": {
                "lengthMenu": "Mostrar _MENU_ registros por página",
                "zeroRecords": "No se encontraron resultados",
                "info": "Mostrando página _PAGE_ de _PAGES_",
                "infoEmpty": "No hay registros disponibles",
                "infoFiltered": "(filtrado de _MAX_ registros totales)",
                "search": "Buscar:",
                "paginate": {
                    "first": "Primero",
                    "last": "Último",
                    "next": "Siguiente",
                    "previous": "Anterior"
                }
            },
        });
    });
}


const downloadCSVFileFromTable = (tableId, filename) => {

    let csvContent = "data:text/csv;charset=utf-8,";
    /*Set header for csv file */

    console.log('headddd', document.querySelectorAll(`#${tableId}`));

    document.querySelectorAll(`#${tableId} thead`).forEach(header => {
        console.log('header', header);

        let dataRow = [];
        header.querySelectorAll('th').forEach(cell => {
            dataRow.push(cell.innerText);
        });
        csvContent += dataRow.join(',') + '\n';
    });



    document.querySelectorAll(`#${tableId} tbody tr`).forEach(row => {
        console.log(row);
        let dataRow = [];
        row.querySelectorAll('td').forEach(cell => {
            console.log(cell);
            dataRow.push(cell.innerText);
        });
        csvContent += dataRow.join(',') + '\n';

    });

    let encodedUri = encodeURI(csvContent);
    let link = document.createElement("a");
    link.setAttribute("href", encodedUri);
    link.setAttribute("download", `${filename}.csv`);
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}


const clearForm = (formId) => {
    document.getElementById(formId).reset();
};


//utils

const validateEmail = (email) => {
    const emailRegex = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$/;
    return emailRegex.test(email);
}

const validatePhone = (phone) => {
    const phoneRegex = /^[0-9]{10}$/;
    return phoneRegex.test(phone);
}

const validateName = (name) => {
    const nameRegex = /^[a-zA-Z]{2,}$/;
    return nameRegex.test(name);
}

const validateNumber = (number) => {
    //include point
    const numberRegex = /^[0-9]+(\.[0-9]+)?$/;
    return numberRegex.test(number);
}


const validatePrice = (price) => {
    const priceRegex = /^[0-9]+(\.[0-9]+)?$/;
    return priceRegex.test(price);
}

const validateYear = (year) => {
    const yearRegex = /^[0-9]{4}$/;
    return yearRegex.test(year);
}

const validateUrl = (url) => {
    const urlRegex = /^(http|https):\/\/[^ "]+$/;
    return urlRegex.test(url);
}