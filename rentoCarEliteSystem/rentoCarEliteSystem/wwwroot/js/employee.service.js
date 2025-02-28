/*****Endpoints Definition*****/
const loginEndpoint = '/Employee/login'
const getAllEmployeesEndpoint = '/Employee/getAllEmployees';
const createEmployeeeEndpoint = '/Employee/createEmployee';
const deleteEmployeeEndpoint = '/Employee/deleteEmployee';


/*****Services*****/

const login = async (data, callback) => {

    bodyFetch(loginEndpoint, data, 'json', (responses) => {
        console.log('data froms service', data);
        callback(responses);
    }, 'POST');
    
}

const getAllEmployees= async (callback) => {
    getFetch(getAllEmployeesEndpoint, 'json', (data) => {
        console.log('data froms service', data);
        callback(data);
    });
};





const createEmployeeService = async (data, callback) => {
    bodyFetch(createEmployeeeEndpoint, data, 'json', (responses) => {
        console.log('data froms service', data);
        callback(responses);
    }, 'POST');
}


const deleteEmployeeService = async (id, callback) => {
    console.log('id to delete', id);
    getFetch(`${deleteEmployeeEndpoint}?id=${id}`, 'json', (response) => {
        console.log('data froms service', response);
        callback(response);
    }, 'DELETE');
}
