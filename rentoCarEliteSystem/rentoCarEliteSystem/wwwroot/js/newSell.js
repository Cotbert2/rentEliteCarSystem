let vehiclesCatalog;
let customersCatalog;



let currentVehicle;


let selectCustomer

let select
document.addEventListener("DOMContentLoaded", () => {
    window.stepper = new Stepper(document.querySelector("#stepper"));
    stepper.to(3); // Ir al primer paso al cargar la página

    select = new TomSelect("#select-items", {
        create: false, // No permite agregar nuevos elementos
        sortField: { field: "text", direction: "asc" }
    });

    // Simulación de datos desde la base de datos
    let items = [];
    getAllVehicles((data) => {
        vehiclesCatalog = data;
        items = data.map((item) => {
            return { value: JSON.stringify( item), text: item.brand + ' ' + item.model + ' ' + item.vehicleYear };
        });
        select.addOptions(items);
    });


    selectCustomer = new TomSelect("#select-customer", {
        create: false,
        sortField: { field: "text", direction: "asc" }
    });

    let itemsCustomer = [];

    getAllCustomers((data) => {
        customersCatalog = data;
        itemsCustomer = data.map((item) => {
            return { value: JSON.stringify(item), text: item.firstName + ' ' + item.lastName };
        });
        selectCustomer.addOptions(itemsCustomer);
    });

    selectCustomer.addOptions(itemsCustomer);
    select.addOptions(itemsCustomer);

    select.on("change", (e) => {
        booking.vehicle.VehicleId = JSON.parse(e).vehicleId;
        let datesInavailable = [];
        getBookingsByVehicleId(JSON.parse(e).vehicleId, (data) => {
            console.log('booking form this car', data);
            datesInavailable = data.map(booking => {
                return {
                    startDate: booking.startDate,
                    endDate: booking.endDate
                }
            });

            renderCalendar(datesInavailable)



            console.log(datesInavailable);
        });


        console.log('event',e);
    })

    selectCustomer.on("change", (e) => {
        booking.customer.id = JSON.parse(e).id;
        console.log('event', e);
    });































    let calendar;
    let events;

    const renderCalendar = (datesInavailable) => {
        console.log('asdasdlaksjd');

        let calendarEl = document.getElementById('calendar');
        events = datesInavailable.map(item => ({
            title: 'Reservado',
            start: item.startDate,
            end: item.endDate,
            color: 'red',
            display: 'block'
        }));

        console.log('events', events);

        calendar = new FullCalendar.Calendar(calendarEl, {
            initialView: 'dayGridMonth',
            locale: 'es',
            headerToolbar: {
                left: 'prev,next today',
                center: 'title',
                right: 'dayGridMonth,timeGridWeek,timeGridDay'
            },
            events: events,
            dateClick: function (info) {
                let dateSelected = info.dateStr;
                let isUnavailable = events.some(event =>
                    new Date(event.start).toISOString().split('T')[0] === dateSelected
                );
                let isAlreadyReserved = calendar.getEvents().some(event =>
                    event.start.toISOString().split('T')[0] === dateSelected && event.title === 'Reservar'
                );

                let reservedDates = calendar.getEvents()
                    .filter(event => event.title === 'Reservar' || event.title === 'Reservado')
                    .map(event => event.start.toISOString().split('T')[0]);

                let prevDate = new Date(dateSelected);
                prevDate.setDate(prevDate.getDate() - 1);
                let nextDate = new Date(dateSelected);
                nextDate.setDate(nextDate.getDate() + 1);

                let isAdjacent = reservedDates.includes(prevDate.toISOString().split('T')[0]) || reservedDates.includes(nextDate.toISOString().split('T')[0]);

                if (!isUnavailable && !isAlreadyReserved && isAdjacent) {
                    calendar.addEvent({
                        title: 'Reservar',
                        start: dateSelected,
                        color: 'green',
                        display: 'block'
                    });
                } else if (isUnavailable) {
                    alert('Esta fecha ya está reservada.');
                } else if (isAlreadyReserved) {
                    alert('Esta fecha ya está en tu lista de reservas.');
                } else {
                    alert('Solo puedes reservar días adyacentes a una reserva existente.');
                }
            }
        });

        calendar.render();
    };


    let booking = {
        vehicle: {},
        customer: {},
        startDate: '',
        endDate: '',
        bookingStatus : "Pending"
    };

    const getReservedDates = () => {
        return calendar.getEvents()
            .filter(event =>  event.title === 'Reservar')
            .map(event => event.start.toISOString().split('T')[0]); // Extrae solo la fecha en formato YYYY-MM-DD
    };


    document.getElementById('calendarNext').addEventListener('click', () => {
        let reservedDates = getReservedDates();

        //get the oldest date
        const startDate = reservedDates.sort()[0];
        const endDate = reservedDates.sort()[reservedDates.length - 1];

        console.log('startDate', startDate);
        console.log('endDate', endDate);

        booking.startDate = startDate;
        booking.endDate = endDate;

        console.log('booking', booking);




        console.log('reservedDates', reservedDates);
        if (reservedDates.length === 0) {
            alert('Debes seleccionar al menos una fecha.');
        } else {
            //stepper.next();
            createBookingService(booking, (data) => {
                console.log('data from createBooking', data);
                if (data.code > 1) {
                    alert('Reserva creada exitosamente');
                    stepper.next();

                } else {
                    alert('Ocurrio un error al crear la reserva');
                }
            });
            console.log('should pass')
        }
    });




    document.getElementById('paymentMethod').addEventListener('change', (e) => {
        booking.paymentMethod = e.target.value;
    });

});

