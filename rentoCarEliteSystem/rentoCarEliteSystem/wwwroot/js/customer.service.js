/*****Endpoints Definition*****/
const getAllCustomerEndpoint = '/Customer/getAllCustomer';
const createCustomerEndpoint = '/Customer/createCustomer';


const getAllCustomers = async (callback) => {
    getFetch(getAllCustomerEndpoint, 'json', (data) => {
        console.log('data froms service', data);
        callback(data);
    });
};saveCustomerButton


const createCustomer = async (data, callback) => {
    bodyFetch(createCustomerEndpoint, data, 'json', (responses) => {
        console.log('data froms service', data);
        callback(responses);
    }, 'POST'); 
}
