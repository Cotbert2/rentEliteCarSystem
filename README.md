# rentCarSystem
A rent car system built in ASP.NET Core MVC


<!-- Index -->

## Index

- [How to run the project](#how-to-run-the-project)
- [Description](#description)
- [Technologies](#technologies)
- [Features](#features)
- [Deployment URL](#deployment)
- [License](#license)


<!-- How to run the project -->

## How to run the project

1. Clone the repository

```bash
git clone https://github.com/Cotbert2/rentEliteCarSystem.git
```

2. Open the project in Visual Studio

3. Run the project

4. Database setup: you can restore the database using the `rentEliteCar.bak` file located in the `database` folder at root's project.

## Description

This project is a rent car system built in ASP.NET Core MVC. It aims to provide a system where employees can rent cars and manage their rentals.

### Features

- **User auth**: Employees can login to the system, the core of authentication is about managing sessions into local storage; therefore routes are not protected by default, but the system checks if the user is authenticated before rendering the page.
As a role integration, the system has tree roles: Root, Admin and Employee.


| Role | Description |
| --- | --- |
| Root | The root user has access to all the system features, root is the only user that cannot be deleted. |
| Admin | The admin user has access to all the system features, but cannot delete root users. |
| Employee | Employee users are not able to manage theirselves, they can only manage their rentals. |


<!--Rentals-->

#### Rentals
The system use a rental manipulation using calendars, employees set the date range to rent a car, the system checks if the car is available in the selected range, if the car is available the system creates a rental, otherwise the system shows a message to the user.

#### Insurance Handling
Employees are able to set a insurance to customer's bookings.


#### Payments
The system has a payment integration, the system calculates the total amount of the rental and the user can pay the rental using a credit card.

#### Notifications
The system has a notification system, the system sends a notification to the customer when the rental is created, when the payment is confirmed and when the rental is aborted by email.





## Technologies

- **ASP.NET Core MVC**: The project is built in ASP.NET Core MVC, a web framework for building web applications using C# and .NET.
- **MS SQL Server**: The project uses MS SQL Server as the database.



## Deployment
You Can access the project [here](http://www.rentelitecar.somee.com/)






