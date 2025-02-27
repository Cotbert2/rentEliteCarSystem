/*****Endpoints Definition*****/
const loginEndpoint = '/Employee/login'
const getAllVehiclesEndpoint = '/Vehicle/getAllVehicles';
const createVehicleEndpoint = '/Vehicle/createVehicle';
const deleteVehicleEndpoint = '/Vehicle/deleteVehicle';


/*****Services*****/

const login = async (data, callback) => {

    bodyFetch(loginEndpoint, data, 'json', (responses) => {
        console.log('data froms service', data);
        callback(responses);
    }, 'POST');
    
}