Dianelsa Chiru & Monica Monterrosa

CS 458

Dr. Yan

Spring 2026

## Property Management App


The Property Management App is a full-stack web application created to help landlords and property managers organize their rental property information in one central place. The main idea of the project is to make daily property management tasks easier, faster, and more organized. Instead of keeping tenant information, rent payments, invoices, maintenance requests, and communication records in different places, this application brings everything together in one system.

The project was built using ASP.NET Core Web API for the backend and Blazor WebAssembly for the frontend. The database was created using SQL Server LocalDB, and Entity Framework Core was used to connect the application to the database. The system also includes JWT authentication, which allows an administrator to log in and access protected parts of the app. This helps make the system more secure and shows how login features can be used in a real web application.

The application includes several important modules. The Properties Module allows users to view, add, edit, and delete property information. The Tenants Module helps users manage tenant records, search for tenants, view tenant details, and access each
tenant’s rent ledger. The Rent Collection Module helps track rent schedules, rent status, late fees, rent reminders, and paid or unpaid rent records. The Rent Records Module allows users to review charges, payments, balances, print statements, and export ledger information to a CSV file.

The app also includes a Maintenance Module, where users can create maintenance projects, assign them to properties, track vendors, manage bids, start work, and close completed projects. The Invoicing Module allows users to create invoices, send invoices, mark invoices as paid, and track outstanding balances. The Communications Module stores records of SMS, email, and call logs, which helps keep communication history organized. The Eviction Prep Modulehelps users start eviction cases, track workflow steps, advance case status, and close cases. This section also includes a legal disclaimer because the project is only for academic and informational purposes.

Overall, this project shows how different technologies can work together to create a realistic business application. It demonstrates our ability to build a backend API, design a frontend interface, connect to a database, manage user authentication, and organize data into useful modules. Even though this project was created for class, it represents a real-world style system that could be improved in the future. More features could be added later, such as stronger security, more user roles, better reporting tools, online payment options, and improved notifications. This project helped us practice important programming skills while creating an application that solves a practical problem.
