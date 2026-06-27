# Opportunity Management API

## Project Overview

This project is a RESTful API built using ASP.NET Core 8 for managing opportunities such as scholarships, internships, training programs, competitions, and fellowships.

The project was developed using a layered architecture to ensure maintainability, scalability, and separation of concerns.

---

## Technologies Used

* ASP.NET Core 8 Web API
* Entity Framework Core
* SQL Server
* JWT Authentication
* Swagger / OpenAPI
* Repository Pattern
* Service Layer
* Dependency Injection

---

## Project Structure

OpportunityAPI

* Controllers
* DTOs
* Middleware

OpportunitiesBL

* Interfaces
* Services

OpportunityEF

* Data
* Models
* Repositories

---

## Features

* Create Opportunity
* Update Opportunity
* Delete Opportunity
* Get All Opportunities
* Get Opportunity By Id
* Search by Keyword
* Filter by Country
* Filter by Type
* Filter by Funding Status
* Pagination
* JWT Authentication
* Authorization
* Global Exception Handling

---

## Database Setup

Run the following commands:

Add-Migration InitialCreate -Project OpportunityEF -StartupProject OpportunityAPI

Update-Database -Project OpportunityEF -StartupProject OpportunityAPI

---

## Authentication

### Register

POST /api/auth/register

### Login

POST /api/auth/login

Returns a JWT Token.

---

## Protected Endpoints

POST /api/opportunities

PUT /api/opportunities/{id}

DELETE /api/opportunities/{id}

---

## Public Endpoints

GET /api/opportunities

GET /api/opportunities/{id}

---

## Pagination Example

GET /api/opportunities?pageNumber=1&pageSize=5

---

## Search Example

GET /api/opportunities?keyword=scholarship

---

## Filter Examples

GET /api/opportunities?country=Turkey

GET /api/opportunities?type=Scholarship

GET /api/opportunities?isFullyFunded=true

---

## Author

Ammar
ASP.NET Core Backend Developer
