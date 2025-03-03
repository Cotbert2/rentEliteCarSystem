/*****Endpoints Definition*****/
const getAllBookingsEndpoint = '/Booking/getDashBoardData';
const deleteBookingEndpoint = '/Booking/deleteBooking';


/*****Services*****/

const paymentMethods = {
    debit_card: 'Tarjeta de debito',
    credit_card: 'Tarjeta de credito',
    cash: 'Efectivo'
}

const getAllBookings = async (callback) => {
    getFetch(getAllBookingsEndpoint, 'json', (data) => {
        console.log('data froms service', data);
        //flatten the data
        // headers: ['Fecha inicio', 'Fecha devolucion', 'Cliente', 'Correo', 'vehiculo', 'Tipo de seguro', 'Tipo de pago', 'Total'],

        const flattenData = data.map((item) => {
            return {
                id : item.booking.bookingID,
                startDate: item.booking.startDate,
                endDate: item.booking.endDate,
                customer: item.booking.customer.firstName + ' ' + item.booking.customer.lastName,
                email: item.booking.customer.email,
                vehicle: item.booking.vehicle.brand + ' ' + item.booking.vehicle.model,
                insurance:  item.insurance.insuranceType,
                payment: paymentMethods[item.payment.paymentMethod],
                total: item.payment.amount

            }
        });
        callback(flattenData);
    });
};




const deleteBookingService = async (id, callback) => {
    getFetch(`${deleteBookingEndpoint}?bookingId=${id}`, 'json', (response) => {
        console.log('data froms service', response);
        callback(response);
    }, 'DELETE');
}
