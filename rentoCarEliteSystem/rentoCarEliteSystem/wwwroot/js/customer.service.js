/*****Endpoints Definition*****/
const getAllCustomerEndpoint = '/Customer/getAllCustomer';
const createCustomerEndpoint = '/Customer/createCustomer';
const deleteCustomerEndpoint = '/Customer/deleteCustomer';


/*****Services*****/

const getAllCustomers = async (callback) => {
    getFetch(getAllCustomerEndpoint, 'json', (data) => {
        console.log('data froms service', data);
        callback(data);
    });
};


const createCustomer = async (data, callback) => {
    bodyFetch(createCustomerEndpoint, data, 'json', (responses) => {
        console.log('data froms service', data);
        callback(responses);
    }, 'POST'); 
}


const deleteCustomerService = async (id, callback) => {
    getFetch(`${deleteCustomerEndpoint}?id=${id}`, 'json', (response) => {
        console.log('data froms service', response);
        callback(response);
    }, 'DELETE');
}
