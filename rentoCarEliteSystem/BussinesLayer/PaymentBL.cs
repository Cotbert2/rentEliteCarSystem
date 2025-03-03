

using EntityLayer;
using BussinesLayer.utils;
namespace BussinesLayer
{
    public class PaymentBL
    {
        public EntityLayer.systemEntities.ResponseEL createPayment(PaymentEL payment)
        {
            DataLayer.PaymentDL paymentDL = new DataLayer.PaymentDL();
            InsuranceBL insuranceBL = new InsuranceBL();
            //TODO: compute payment amount
            InsuranceEL insurance = insuranceBL.getInsuranceByBookingId(payment.booking.bookingID);

            TimeSpan difference = payment.booking.endDate - payment.booking.startDate;


            int rentDays = difference.Days + 1;



            payment.amount = rentDays * (payment.booking.vehicle.price + insurance.amount)  * 1.15f;
            payment.paymentDate = System.DateTime.Now;

            string htmlContent = $@"<!DOCTYPE html>
            <html lang=""en"">
            <head>
                <meta charset=""UTF-8"">
                <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                <title>Document</title>
                <style>
                    body {{
                        font-family: Arial, sans-serif;
                        margin: 0;
                        padding: 0;
                    }}
                    .container {{
                        width: 80%;
                        margin: 0 auto;
                        padding: 20px;
                    }}
                    .header {{
                        background-color: #f8f9fa;
                        padding: 10px;
                        text-align: center;
                    }}
                    .content {{
                        padding: 20px;
                    }}
                    .footer {{
                        background-color: #f8f9fa;
                        padding: 10px;
                        text-align: center;
                    }}
                </style>
            </head>

            <body>
                <div class=""container"">
                    <div class=""header"">
                        <h1>Rent a Car Elite</h1>
                    </div>
                    <div class=""content"">
                        <h2>Factura de Pago</h2>
                        <p>Estimado/a {payment.booking.customer.firstName} {payment.booking.customer.lastName},</p>
                        <p>Se ha generado una factura de pago por el monto de ${payment.amount} correspondiente a su reserva.</p>
                        <p>Detalles de la reserva:</p>
                        <ul>
                            <li><strong>Fecha de inicio:</strong> {payment.booking.startDate}</li>
                            <li><strong>Fecha de fin:</strong> {payment.booking.endDate}</li>
                            <li><strong>Vehículo:</strong> {payment.booking.vehicle.brand} {payment.booking.vehicle.model}</li>
                            <li><strong>Detalle:</strong> </li>
                        </ul>

                        <table border=""1"" style=""width: 100%"">
                            <thead>
                                <tr>
                                    <th>Concepto</th>
                                    <th>Monto</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>Alquiler de vehículo por ${rentDays} dias</td>
                                    <td>${rentDays * payment.booking.vehicle.price}</td>
                                </tr>
                                <tr>
                                    <td>Seguro</td>
                                    <td>${rentDays * insurance.amount}</td>
                                </tr>
                                <tr>
                                    <td>Impuestos</td>
                                    <td>${rentDays * payment.booking.vehicle.price * 0.15}</td>
                                </tr>
                                <tr>
                                    <td><strong>Total</strong></td>
                                    <td><strong>${payment.amount}</strong></td>
                                </tr>
                            </tbody>

                        <p>Gracias por confiar en Rent a Car Elite.</p>
                    </div>
                    <div class=""footer"">
                        <p>© 2022 Rent a Car Elite</p>
                    </div>
                </div>
            </body>
            </html>";
            var emailManager = new Mailer();
            emailManager.SendEmailAsync(payment.booking.customer.email, "Factura de Consumo", htmlContent);

            return paymentDL.createPayment(payment);
        }
    }
}
