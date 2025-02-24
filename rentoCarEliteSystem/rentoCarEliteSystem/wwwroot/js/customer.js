document.getElementById('downloadCsv').addEventListener('click', () => {
    downloadCSVFileFromTable('dataTable', 'customers');
});

let tableData;

getAllCustomers((data) => {
    tableData = data;
    renderTable({
        headers: ['id', 'first name', 'last name', 'phone', 'email'],
        keys: ['id', 'firstName', 'lastName', 'phone', 'email'],
        data,
        isEditable: true,
        isDeletable: true,
        editFunction: 'editCustomer',
        deleteFunction: 'deleteCustomer',
    });
});

const editCustomer = (customer) => {

    alert('edit customer');
}


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
            renderTable({
                headers: ['id', 'first name', 'last name', 'phone', 'email'],
                keys: ['id', 'firstName', 'lastName', 'phone', 'email'],
                data,
                isEditable: true,
                isDeletable: true,
                editFunction: 'editCustomer',
                deleteFunction: 'deleteCustomer',
            });
        });

    });
});




document.getElementById('searchInput').addEventListener('keyup', (event) => {
    const searchValue = event.target.value.trim().toLowerCase();
    if (!tableData || !Array.isArray(tableData)) return;

    const filteredData = tableData.filter((currentData) =>
        Object.values(currentData).some(value =>
            value.toString().toLowerCase().includes(searchValue))
    );
    renderTable({
        headers: ['id', 'first name', 'last name', 'phone', 'email'],
        keys: ['id', 'firstName', 'lastName', 'phone', 'email'],
        data,
        isEditable: true,
        isDeletable: true,
        editFunction: 'editCustomer',
        deleteFunction: 'deleteCustomer',
    });
});
