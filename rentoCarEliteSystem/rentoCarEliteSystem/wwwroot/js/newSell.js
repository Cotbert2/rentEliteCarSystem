let vehiclesCatalog;
let customersCatalog;
let calendar;

const secures = [
    {
        type : "Ninguno",
        description : "No cubre daños, tu responsabilidad es total",
        amount : 0,
    },
    {
        type : "Premium",
        description : "Cubre daños a terceros, a tu vehículo y a ti",
        amount : 35,
    },
    {
        type : "Completo",
        description : "Cubre daños a terceros y a tu vehículo",
        amount : 25,
    },
    {
        type : "Basico",
        description : "Cubre accidentes menores",
        amount : 15,
    },
    {
        type : "Terceros",
        description : "Cubre daños a terceros",
        amount : 10,
    }
];

let secureOption;
let currentVehicle;


let selectCustomer

let select
document.addEventListener("DOMContentLoaded", () => {
    window.stepper = new Stepper(document.querySelector("#stepper"));
    stepper.to(1); // Ir al primer paso al cargar la página

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


    select.on("change", (e) => {
        booking.vehicle = JSON.parse(e);
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

        const vehicle = JSON.parse(e);


        document.getElementById('brand').value = vehicle.brand;
        document.getElementById('model').value = vehicle.model;
        document.getElementById('price').value = vehicle.price;
        document.getElementById('year').value = vehicle.vehicleYear;
        document.getElementById('image').src = vehicle.photo;


        console.log('event',e);
    })

    selectCustomer.on("change", (e) => {
        booking.customer = JSON.parse(e);
        console.log('event', e);

        document.getElementById('name').value = booking.customer.firstName;
        document.getElementById('lastName').value = booking.customer.lastName;
        document.getElementById('phone').value = booking.customer.phone;
        document.getElementById('email').value = booking.customer.email;

    });

    let bookingID;
    let events;

    const renderCalendar = (datesInavailable) => {

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
        setTimeout(() => {
            if (calendar) {
                calendar.updateSize();
            }
        }, 300);
    };


    let booking = {
        vehicle: {},
        customer: {},
        startDate: '',
        endDate: '',
        bookingStatus : "ACTIVE",
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
            createBookingService(booking, (data) => {
                console.log('data from createBooking', data);
                if (data.code > 1) {
                    bookingID = data.code;
                    alert('Reserva creada exitosamente');

                } else {
                    alert('Ocurrio un error al crear la reserva');
                }
            });
                    stepper.next();


            console.log('should pass');


            document.getElementById('totalAmount').value = computeTotalAmount();
        }
    });




    document.getElementById('paymentMethod').addEventListener('change', (e) => {
        booking.paymentMethod = e.target.value;
    });

    document.getElementById('nextStepPayment').addEventListener('click', () => {
        createPayment({
            paymentMethod: document.getElementById('paymentMethod').value,
            booking: {
                bookingID
            }

        }, (data) => {
            console.log('data bring', data);
            if (data.code >= 1) {
                alert('Pago creado con exito');
            } else {
                alert('Ocurrio un error al crear el pago');
            }

        })
    });



    const computeTotalAmount = () => {
        const days = getReservedDates().length;
        const pricePerDay = booking.vehicle.price;
        const totalAmount = (days * pricePerDay) + fullSelectedTypeSecure.amount;
        return totalAmount;

    }

    const secureTypeSelect = document.getElementById("secureType");
    const descriptionField = document.getElementById("description");
    const amountField = document.getElementById("amount");

    let fullSelectedTypeSecure;

    secures.forEach(secure => {
        let option = document.createElement("option");
        option.value = secure.type;
        option.textContent = secure.type;
        secureTypeSelect.appendChild(option);
    });

    secureTypeSelect.addEventListener("change",  () => {
        const selectedType = secureTypeSelect.value;
        const selectedSecure = secures.find(s => s.type === selectedType);
        fullSelectedTypeSecure = selectedSecure;
        if (selectedSecure) {
            descriptionField.value = selectedSecure.description;
            amountField.value = `$${selectedSecure.amount}`;
        }
    });

    document.getElementById('customerNext').addEventListener('click', () => {
        if (!booking.customer.id) {
            alert('Debes seleccionar un cliente.');
            return;
        }
        stepper.next();
    });

    document.getElementById('nextInsuranceButton').addEventListener('click', () => {
        secureOption = secureTypeSelect.value;
        console.log('secure option', secureOption)
        createInsurancePayment({
            booking:  {
            bookingID
            },
            insuranceType: fullSelectedTypeSecure.type,
            amount: fullSelectedTypeSecure.amount
        }, (data) => {
            console.log('data', data);
            if (data.code >= 1) {
                alert('Pago de seguro creado con exito');
            } else {
                alert('Ocurrio un error al crear el pago de seguro');
            }
        })
        stepper.next();
    });

    document.getElementById('vechileNextSelection').addEventListener('click', () => {
        if (!booking.vehicle.vehicleId) {
            alert('Debes seleccionar un vehículo.');
            return;
        }
            stepper.next();
    });

});


document.querySelector("#stepper").addEventListener("shown.bs-stepper", (event) => {
    console.log("Step " + event.detail.indexStep + " shown");
    if (event.detail.indexStep === 2 && calendar) {
        setTimeout(() => {
            calendar.updateSize();
        }, 200);
    }
});


