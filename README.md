# Documentation

This application is a **Web API** built with **.NET 8** for managing contacts, companies, and countries, supporting CRUD operations with PostgreSQL database integration. It follows **Onion Architecture** and implements several advanced features like pagination, logging, error handling, and authentication.

## **Technologies Used**

- **C#** - Programming language
- **.NET 8** - Framework
- **PostgreSQL** - Database
- **Entity Framework Core** - ORM for data access
- **Swagger** - API documentation and UI
- **Fluent Validation** - Input validation
- **JWT Authentication** - User authentication and authorization
- **Unit Tests** - NUnit and Moq for testing
  
## Architecture

This application follows **Onion Architecture** and **Vertical Slices** principles to organize the codebase for scalability, maintainability, and separation of concerns.

## **Features**

- **CRUD Operations** for Contacts, Companies, and Countries
- **Filter Contacts** by `countryId`, `companyId`, or both
- **Pagination** for efficient data retrieval
- **Swagger UI** for easy API exploration
- **Unit Tests** with Moq and NUnit for testing
- **Logging and Error Handling**
- **Authentication & Authorization** with JWT Tokens
- **Country Statistics** to show the number of contacts per company in each country

## **API Documentation**

### **Contacts**

- `GET /api/Contacts/GetAll` - Retrieve all contacts.
- `GET /api/Contacts/Get/{id}` - Retrieve a specific contact by ID.
- `POST /api/Contacts/Create` - Create a new contact.
- `PUT /api/Contacts/Update/{id}` - Update an existing contact by ID.
- `DELETE /api/Contacts/Delete/{id}` - Delete a contact by ID.

### **Companies**

- `GET /api/Company/GetAll` - Retrieve all companies.
- `GET /api/Company/Get/{id}` - Retrieve a specific company by ID.
- `GET /api/Company/company-statistics/{countryId}` - Retrieve companies in a given country and the number of contacts linked to each.
- `POST /api/Company/Create` - Create a new company.
- `PUT /api/Company/Update/{id}` - Update an existing company by ID.
- `DELETE /api/Company/Delete/{id}` - Delete a company by ID.

### **Countries**

- `GET /api/Country/GetAll` - Retrieve all countries.
- `GET /api/Country/Get/{id}` - Retrieve a specific country by ID.
- `POST /api/Country/Create` - Create a new country.
- `DELETE /api/Country/Delete/{id}` - Retrieve company statistics for a country.
- `PUT /api/Country/Update/{id}` - Update an existing country by ID.

### **Filter Contacts**

- `GET /api/Contacts/Filter` - Filter contacts by `countryId`, `companyId`, or both, provided via query parameters.

### **Authentication**

- `POST /api/user/login` - Login to get a JWT token for secure access.
- `POST /api/user/Register` -Register a new user by providing necessary details such as username, email, and password.

## **Deployment Instructions**

The application is hosted on Azure and is ready to be accessed via the following URL:

- **Access the API via**: [Swagger Link](https://aspekt-web-app-erb0fsecgpdvh7bg.canadacentral-01.azurewebsites.net/swagger/index.html)

The database is hosted on [**Tembo**](https://tembo.io/) and uses a **managed PostgreSQL** database. A managed database means that the database infrastructure, maintenance, and updates are handled by the service provider.

## **Error Handling**

The application uses custom error handling for common issues like not found, bad requests, etc. If an error occurs, the API will return a detailed error message with the appropriate HTTP status code.

## **Authentication and Authorization**

Actions that modify the database (such as creating, updating, or deleting resources) require authentication. 
Use the JWT token obtained from the `/api/user/login` endpoint in the `Authorization` header as a Bearer token for protected routes.

## **Bonus Features**

- Lambda expressions for querying and filtering.
- Design patterns: Repository Pattern.
- Pagination using `Skip` and `Take`.
- MediatR for handling requests and responses in a clean, decoupled way.
- Pipeline behavior to apply cross-cutting concerns like logging, validation, and authorization in a standardized manner.
- CQRS (Command Query Responsibility Segregation) to separate read and write operations for better scalability and maintainability.
- Fluent validation for input models.
- JWT Authentication for secured routes.
