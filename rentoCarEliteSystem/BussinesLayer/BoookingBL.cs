using BussinesLayer.utils;
using DataLayer;
using EntityLayer;

namespace BussinesLayer
{
    public class BoookingBL
    {

        public  EntityLayer.systemEntities.ResponseEL createBooking(BookingEL booking)
        {
            var emailManager = new Mailer();


            string htmlContent = $@"
            <!DOCTYPE html>
            <html>
            <head>
                <meta charset=""UTF-8"">
                <meta name=""viewport"" content=""width=device-width, initial-scale=1"">
                <title>Confirmación de Reserva - Rent a Car</title>
                <link href=""https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css"" rel=""stylesheet"">
                <style>
                    body {{
                        background-color: #f8f9fa;
                        font-family: Arial, sans-serif;
                    }}
                    .email-container {{
                        max-width: 600px;
                        margin: 20px auto;
                        background: white;
                        padding: 20px;
                        border-radius: 10px;
                        box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
                    }}
                    .footer {{
                        text-align: center;
                        padding: 15px;
                        background-color: #343a40;
                        color: white;
                        border-radius: 0 0 10px 10px;
                    }}
                    .footer a {{
                        color: #ffc107;
                        text-decoration: none;
                    }}
                </style>
            </head>
            <body>
                <div class=""email-container"">
                    <h2 class=""text-center text-primary"">¡Reserva Confirmada!</h2>
                    <p>Estimado/a <strong>{booking.customer.firstName} {booking.customer.lastName}</strong>,</p>
                    <p>Su reserva ha sido confirmada con éxito. A continuación, los detalles de su alquiler:</p>
                    <table class=""table table-bordered"">
                        <thead class=""table-dark"">
                            <tr>
                                <th>Detalle</th>
                                <th>Información</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>Vehículo</td>
                                <td>{booking.vehicle.brand} {booking.vehicle.model} </td>
                            </tr>
                            <tr>
                                <td>Fecha de Recogida</td>
                                <td>{booking.startDate}</td>
                            </tr>
                            <tr>
                                <td>Fecha de Devolución</td>
                                <td>{booking.endDate}</td>
                            </tr>
                        </tbody>
                    </table>
                    <p>Gracias por elegir <strong>Rent a Car</strong>. ¡Le deseamos un excelente viaje!</p>
                    <div class=""footer"">
                        <p>&copy; 2025 Rent a Car. Todos los derechos reservados.</p>
                        <p><a href=""#"">Visite nuestro sitio web</a> | <a href=""#"">Contacto</a></p>
                    </div>
                </div>
            </body>
            </html>";

            emailManager.SendEmailAsync(booking.customer.email, "Alquiler Reservado en Rent a Car Elite", htmlContent);
            DataLayer.BookingDL bookingDL = new DataLayer.BookingDL();
            return bookingDL.createBooking(booking);
        }

        public List<BookingEL> getBookingsByCustomerId(int customerId)
        {
            DataLayer.BookingDL bookingDL = new DataLayer.BookingDL();
            return bookingDL.getBookingsByCustomerId(customerId);
        }


        public EntityLayer.systemEntities.ResponseEL deleteBooking(int bookingId)
        {
            DataLayer.BookingDL bookingDL = new DataLayer.BookingDL();
            EntityLayer.systemEntities.ResponseEL response = bookingDL.deleteBooking(bookingId);
            
            if (response.code >= 1)
            {
                CustomerDL customerDL = new CustomerDL();
                CustomerEL currentCustomer  =customerDL.getCustomerByBookingId(bookingId);
                //mail for customer notification
                var emailManager = new Mailer();
                string htmlContent = $@"
                    <!DOCTYPE html>
                    <html>
                    <head>
                        <meta charset=""UTF-8"">
                        <meta name=""viewport"" content=""width=device-width, initial-scale=1"">
                        <title>Cancelación de Reserva - Rent a Car</title>
                        <link href=""https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css"" rel=""stylesheet"">
                        <style>
                            body {{
                                background-color: #f8f9fa;
                                font-family: Arial, sans-serif;
                            }}
                            .email-container {{
                                max-width: 600px;
                                margin: 20px auto;
                                background: white;
                                padding: 20px;
                                border-radius: 10px;
                                box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
                            }}
                            .footer {{
                                text-align: center;
                                padding: 15px;
                                background-color: #343a40;
                                color: white;
                                border-radius: 0 0 10px 10px;
                            }}
                            .footer a {{
                                color: #ffc107;
                                text-decoration: none;
                            }}
                        </style>
                    </head>
                    <body>
                        <div class=""email-container"">
                            <h2 class=""text-center text-danger"">¡Reserva Cancelada!</h2>
                            <p>Estimado/a <strong>{currentCustomer.firstName} {currentCustomer.lastName}</strong>,</p>
                            <p>Su reserva ha sido cancelada. Agradecemos su preferencia y esperamos poder servirle en el futuro.</p>
                            <p>Si piensas que esto es un error, por favor contáctanos.</p>
                            <div class=""footer"">
                                <p>&copy; 2025 Rent a Car. Todos los derechos reservados.</p>
                                <p><a href=""#"">Visite nuestro sitio web</a> | <a href=""#"">Contacto</a></p>
                            </div>
                        </div>
                    </body>
                    </html>";

                emailManager.SendEmailAsync(currentCustomer.email, "Reserva Cancelada en Rent a Car Elite", htmlContent);
            }

            return response;
        }
        public List<BookingEL> getBookingsByVechileId(int vehicleId)
        {


            DataLayer.BookingDL bookingDL = new DataLayer.BookingDL();
            return bookingDL.getBookingsByVehicleId(vehicleId);
        }


        public List<EntityLayer.systemEntities.DashBoardEL> getDashBoardData()
        {
            DataLayer.BookingDL bookingDL = new DataLayer.BookingDL();
            return bookingDL.getDashBoardData();
        }

    }


}
