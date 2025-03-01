/*****Endpoints Definition*****/
const getBookingsByVehicleIdEnpoint = '/Booking/getBookingsByVechileId';
const createBookingEndpoint = '/Booking/createBooking';





/*****Services*****/

const getBookingsByVehicleId  = async (id, callback) => {
    getFetch(`${getBookingsByVehicleIdEnpoint}?vehicleId=${id}`, 'json', (data) => {
        console.log('data froms service', data);
        callback(data);
    });
};


const createBookingService = async (data, callback) => {
    bodyFetch(createBookingEndpoint, data, 'json', (responses) => {
        console.log('data froms service', data);
        callback(responses);
    }, 'POST');
}