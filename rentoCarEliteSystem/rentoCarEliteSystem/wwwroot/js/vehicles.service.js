/*****Endpoints Definition*****/
const getAllVehiclesEndpoint = '/Vehicle/getAllVehicles';
const createVehicleEndpoint = '/Vehicle/createVehicle';
const deleteVehicleEndpoint = '/Vehicle/deleteVehicle';


/*****Services*****/

const getAllVehicles = async (callback) => {
    getFetch(getAllVehiclesEndpoint, 'json', (data) => {
        console.log('data froms service', data);
        callback(data);
    });
};


const createVehicle = async (data, callback) => {
    bodyFetch(createVehicleEndpoint, data, 'json', (responses) => {
        console.log('data froms service', data);
        callback(responses);
    }, 'POST');
}


const deleteVehicleService = async (id, callback) => {
    getFetch(`${deleteVehicleEndpoint}?vehicleId=${id}`, 'json', (response) => {
        console.log('data froms service', response);
        callback(response);
    }, 'DELETE');
}
