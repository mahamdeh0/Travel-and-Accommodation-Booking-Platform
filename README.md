# Travel and Accommodation Booking Platform

![License](https://img.shields.io/badge/License-MIT-blue.svg)
![Build](https://img.shields.io/badge/build-passing-brightgreen.svg)
![Version](https://img.shields.io/badge/version-1.0.0-orange.svg)

Welcome to the **Travel and Accommodation Booking Platform**, a state-of-the-art solution designed to streamline and enhance the management of travel bookings, accommodations, and related services. Our platform offers a robust API capable of handling a diverse range of operations, including user authentication, booking management, city and hotel administration, discounts, reviews, and more.

---

## 📝 Table of Contents

- [🌟 Key Features](#-key-features)
- [🛠 Technology Stack Overview](#-technology-stack-overview)
- [🗂 API Versioning](#-api-versioning)
- [🚀 Setup Guide](#-setup-guide)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
  - [Configuration](#configuration)
  - [Running the Application](#running-the-application)
- [🔗 Endpoints](#-endpoints)
  - [Authentication](#authentication)
  - [Bookings](#bookings)
  - [Cities](#cities)
  - [Discounts](#discounts)
  - [Hotels](#hotels)
  - [Owners](#owners)
  - [Reviews](#reviews)
  - [Room Classes](#room-classes)
  - [Rooms](#rooms)
  - [User Dashboard](#user-dashboard)
- [🏢 System Architecture](#-system-architecture)
- [🗂 Database Schema Design](#-database-schema-design)
- [Contributing](#contributing)
- [📄 License](#-license)

---

## 🌟 Key Features

- **User Authentication**: Secure login and registration for guests and administrators.
- **Booking Management**: Create, view, and delete bookings; retrieve invoices in PDF format.
- **City Management**: Add, update, delete cities; manage city thumbnails; view trending cities.
- **Hotel Management**: Perform CRUD operations on hotels; manage hotel galleries and thumbnails; search and filter hotels; view featured deals.
- **Discount Management**: Apply and manage discounts on room classes.
- **Owner Management**: Efficiently manage hotel owners.
- **Review Management**: Create, view, update, and delete reviews for hotels.
- **Room Class & Room Management**: Manage various room classes and individual rooms; handle room availability.
- **User Dashboard**: Personalized view of recently visited hotels for each user.

---

## 🛠 Technology Stack Overview

| **Layer**                | **Technologies**                                      |
|--------------------------|-------------------------------------------------------|
| **Backend**              | ASP.NET Core 8.0                                       |
| **ORM**                  | Entity Framework Core 9.0                              |
| **Authentication**       | JWT Bearer Tokens                                      |
| **API Documentation**    | Swagger/OpenAPI                                       |
| **Logging**              | Serilog                                                |
| **Dependency Injection** | Built-in ASP.NET Core DI                              |
| **Design Patterns**      | CQRS with MediatR, Repository Pattern, Unit of Work    |
| **Others**               | AutoMapper, FluentValidation, MailKit                   |

---

## 🗂 API Versioning

The API employs **URL Segment Versioning** to manage multiple versions seamlessly. All endpoints are prefixed with the version number to ensure backward compatibility and organized development.

- **Current Version**: `v1.0`

**Example**: `https://api.example.com/v1.0/hotels`

---

## 🚀 Setup Guide

### Prerequisites

- **.NET 8.0 SDK**: [Download Here](https://dotnet.microsoft.com/download/dotnet/8.0)
- **SQL Server**: Ensure access to a SQL Server instance.
- **Node.js** (optional): Required if working on the frontend or utilizing tools like Swagger UI.

### Installation

1. **Clone the Repository**

   ```bash
   git clone https://github.com/yourusername/TravelAndAccommodationBookingPlatform.git
   cd TravelAndAccommodationBookingPlatform
   ```

2. **Restore Dependencies**

   ```bash
   dotnet restore
   ```

3. **Apply Migrations and Seed Data**

   Ensure your `appsettings.json` is configured with the correct connection string.

   ```bash
   dotnet ef database update --project Infrastructure/TravelAndAccommodationBookingPlatform.Infrastructure.csproj
   ```

### Configuration

1. **Update `appsettings.json`**

   Configure your database connection, JWT settings, and other necessary configurations.

   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=YOUR_SERVER;Database=TravelBookingDB;Trusted_Connection=True;"
     },
     "JwtSettings": {
       "Secret": "YourSuperSecretKey",
       "Issuer": "YourIssuer",
       "Audience": "YourAudience",
       "ExpirationMinutes": 60
     },
     "Logging": {
       "LogLevel": {
         "Default": "Information",
         "Microsoft.AspNetCore": "Warning"
       }
     },
     "AllowedHosts": "*"
   }
   ```

2. **Environment Variables**

   For sensitive data like JWT secrets, consider using environment variables or a secrets manager to enhance security.

### Running the Application

Navigate to the `WebAPI` project directory and execute the application:

```bash
cd WebAPI
dotnet run
```

The API will be accessible at `https://localhost:5001` or `http://localhost:5000` by default.

Access the Swagger UI for interactive API documentation at `https://localhost:5001/swagger` or `http://localhost:5000/swagger`.

---

## 🔗 Endpoints

### Authentication

Handles user authentication operations such as login and registration.

| **Endpoint**                          | **Method** | **Description**                           | **Authentication** |
|---------------------------------------|------------|-------------------------------------------|--------------------|
| `/api/authentication/login`           | POST       | Authenticate user and return JWT token     | No                 |
| `/api/authentication/register-guest`  | POST       | Register a new guest user                 | No                 |

### Bookings

Manages booking operations for users.

| **Endpoint**                       | **Method** | **Description**                          | **Authentication** |
|------------------------------------|------------|------------------------------------------|--------------------|
| `/api/user/bookings`               | GET        | Retrieve a paginated list of user bookings| Yes                |
| `/api/user/bookings/{id}`          | GET        | Retrieve booking details by ID            | Yes                |
| `/api/user/bookings/{id}/invoice`  | GET        | Retrieve invoice for a booking as PDF     | Yes                |
| `/api/user/bookings`               | POST       | Create a new booking                      | Yes                |
| `/api/user/bookings/{id}`          | DELETE     | Delete an existing booking                | Yes                |

### Cities

Manages city-related operations.

| **Endpoint**                      | **Method** | **Description**                                   | **Authentication** |
|-----------------------------------|------------|---------------------------------------------------|--------------------|
| `/api/cities/trending`            | GET        | Retrieve a list of trending cities                | No                 |
| `/api/cities`                      | GET        | Retrieve a paginated list of cities for management | Yes                |
| `/api/cities`                      | POST       | Create a new city                                  | Yes                |
| `/api/cities/{id}/thumbnail`       | PUT        | Add or update a city's thumbnail image             | Yes                |
| `/api/cities/{id}`                 | PUT        | Update an existing city                            | Yes                |
| `/api/cities/{id}`                 | DELETE     | Delete an existing city                            | Yes                |

### Discounts

Manages discounts applied to room classes.

| **Endpoint**                                          | **Method** | **Description**                                 | **Authentication** |
|-------------------------------------------------------|------------|-------------------------------------------------|--------------------|
| `/api/room-classes/{roomClassId}/discounts/{id}`      | GET        | Retrieve a specific discount by ID              | Yes                |
| `/api/room-classes/{roomClassId}/discounts`           | GET        | Retrieve a paginated list of discounts          | Yes                |
| `/api/room-classes/{roomClassId}/discounts`           | POST       | Create a new discount for a room class          | Yes                |
| `/api/room-classes/{roomClassId}/discounts/{id}`      | DELETE     | Delete a specific discount by ID                | Yes                |

### Hotels

Manages hotel-related operations.

| **Endpoint**                      | **Method** | **Description**                                   | **Authentication** |
|-----------------------------------|------------|---------------------------------------------------|--------------------|
| `/api/hotels`                      | GET        | Retrieve a paginated list of hotels for management | Yes                |
| `/api/hotels/{id}`                 | GET        | Retrieve hotel details for guests                  | No                 |
| `/api/hotels/featured-deals`       | GET        | Retrieve a list of featured deals for hotels        | No                 |
| `/api/hotels/search`               | GET        | Search and filter hotels based on criteria          | No                 |
| `/api/hotels/{id}/room-classes`    | GET        | Retrieve room classes for guests by hotel ID        | No                 |
| `/api/hotels`                      | POST       | Create a new hotel                                 | Yes                |
| `/api/hotels/{id}/gallery`         | POST       | Add an image to the hotel's gallery                | Yes                |
| `/api/hotels/{id}/thumbnail`       | PUT        | Set a thumbnail image for a hotel                   | Yes                |
| `/api/hotels/{id}`                 | PUT        | Update an existing hotel                           | Yes                |
| `/api/hotels/{id}`                 | DELETE     | Delete a hotel by ID                               | Yes                |

### Owners

Manages hotel owners.

| **Endpoint**          | **Method** | **Description**                      | **Authentication** |
|-----------------------|------------|--------------------------------------|--------------------|
| `/api/owners/{id}`     | GET        | Retrieve owner details by ID         | Yes                |
| `/api/owners`          | GET        | Retrieve a paginated list of owners  | Yes                |
| `/api/owners`          | POST       | Create a new owner                   | Yes                |
| `/api/owners/{id}`     | PUT        | Update an existing owner             | Yes                |

### Reviews

Manages reviews for hotels.

| **Endpoint**                              | **Method** | **Description**                          | **Authentication** |
|-------------------------------------------|------------|------------------------------------------|--------------------|
| `/api/hotels/{hotelId}/reviews/{id}`       | GET        | Retrieve a specific review by ID         | No                 |
| `/api/hotels/{hotelId}/reviews`            | GET        | Retrieve a paginated list of reviews     | No                 |
| `/api/hotels/{hotelId}/reviews`            | POST       | Create a new review for a hotel          | Yes                |
| `/api/hotels/{hotelId}/reviews/{id}`       | PUT        | Update an existing review                | Yes                |
| `/api/hotels/{hotelId}/reviews/{id}`       | DELETE     | Delete a review for a hotel               | Yes                |

### Room Classes

Manages room classes within hotels.

| **Endpoint**                        | **Method** | **Description**                                 | **Authentication** |
|-------------------------------------|------------|-------------------------------------------------|--------------------|
| `/api/room-classes`                  | GET        | Retrieve a paginated list of room classes for management | Yes                |
| `/api/room-classes`                  | POST       | Create a new room class                          | Yes                |
| `/api/room-classes/{id}/gallery`     | POST       | Add an image to the room class's gallery         | Yes                |
| `/api/room-classes/{id}`             | PUT        | Update an existing room class                    | Yes                |
| `/api/room-classes/{id}`             | DELETE     | Delete a room class by ID                        | Yes                |

### Rooms

Manages individual rooms within room classes.

| **Endpoint**                                            | **Method** | **Description**                                 | **Authentication** |
|---------------------------------------------------------|------------|-------------------------------------------------|--------------------|
| `/api/room-classes/{roomClassId}/rooms/availableRooms`  | GET        | Retrieve available rooms for guests              | No                 |
| `/api/room-classes/{roomClassId}/rooms`                 | GET        | Retrieve a paginated list of rooms for management | Yes                |
| `/api/room-classes/{roomClassId}/rooms`                 | POST       | Create a new room within a room class            | Yes                |
| `/api/room-classes/{roomClassId}/rooms/{id}`            | PUT        | Update an existing room                          | Yes                |
| `/api/room-classes/{roomClassId}/rooms/{id}`            | DELETE     | Delete a room by ID                               | Yes                |

### User Dashboard

Provides user-specific data and functionalities.

| **Endpoint**                                      | **Method** | **Description**                                  | **Authentication** |
|---------------------------------------------------|------------|--------------------------------------------------|--------------------|
| `/api/user/dashboard/recently-visited-hotels`      | GET        | Retrieve a list of recently visited hotels by user | Yes                |

---

## 🗂 Database Schema Design

Based on the provided C# entity definitions, the following database schema has been designed to accurately represent the data structures and relationships within the **Travel and Accommodation Booking Platform**.

### Users

| **Column**     | **Type**   | **Description**                      |
|----------------|------------|--------------------------------------|
| `Id`           | GUID (PK)  | Unique identifier for the user       |
| `FirstName`    | string     | User's first name                    |
| `LastName`     | string     | User's last name                     |
| `Password`     | string     | User's password (hashed)             |
| `Email`        | string     | User's email address                 |
| `DateOfBirth`  | DateTime   | User's date of birth                 |
| `PhoneNumber`  | string     | User's contact number                |

### Roles

| **Column**     | **Type**   | **Description**                      |
|----------------|------------|--------------------------------------|
| `Id`           | GUID (PK)  | Unique identifier for the role       |
| `Name`         | string     | Name of the role (e.g., Admin, Guest)|
| `Description`  | string     | Description of the role              |

### UserRoles

*Junction table for many-to-many relationship between Users and Roles.*

| **Column**     | **Type**   | **Description**                      |
|----------------|------------|--------------------------------------|
| `UserId`       | GUID (FK)  | Foreign key referencing Users(Id)    |
| `RoleId`       | GUID (FK)  | Foreign key referencing Roles(Id)    |

### Cities

| **Column**     | **Type**   | **Description**                      |
|----------------|------------|--------------------------------------|
| `Id`           | GUID (PK)  | Unique identifier for the city        |
| `Name`         | string     | Name of the city                      |
| `Country`      | string     | Country where the city is located     |
| `PostOffice`   | string     | Post office information               |
| `Region`       | string     | Region of the city                     |
| `ThumbnailId`  | GUID (FK)  | Foreign key referencing Images(Id)     |
| `CreatedAt`    | DateTime   | Timestamp when the city was created    |
| `UpdatedAt`    | DateTime   | Timestamp when the city was last updated|

### Owners

| **Column**     | **Type**   | **Description**                      |
|----------------|------------|--------------------------------------|
| `Id`           | GUID (PK)  | Unique identifier for the owner      |
| `FirstName`    | string     | Owner's first name                   |
| `LastName`     | string     | Owner's last name                    |
| `Email`        | string     | Owner's email address                |
| `PhoneNumber`  | string     | Owner's contact number               |

### Hotels

| **Column**           | **Type**   | **Description**                              |
|----------------------|------------|----------------------------------------------|
| `Id`                 | GUID (PK)  | Unique identifier for the hotel              |
| `Name`               | string     | Name of the hotel                             |
| `Address`            | string     | Address of the hotel                          |
| `Description`        | string     | Full description of the hotel                 |
| `BriefDescription`   | string     | Brief description of the hotel                |
| `Website`            | string     | Hotel's website URL                           |
| `Geolocation`        | string     | Geolocation data (e.g., coordinates)          |
| `HotelStatus`        | int        | Status of the hotel (enum represented as int) |
| `PhoneNumber`        | string     | Hotel's contact number                        |
| `CityId`             | GUID (FK)  | Foreign key referencing Cities(Id)            |
| `OwnerId`            | GUID (FK)  | Foreign key referencing Owners(Id)            |
| `ReviewsRating`      | double     | Average rating from reviews                    |
| `StarRating`         | int        | Star rating of the hotel                        |
| `CreatedAt`          | DateTime   | Timestamp when the hotel was created            |
| `UpdatedAt`          | DateTime   | Timestamp when the hotel was last updated       |
| `ThumbnailId`        | GUID (FK)  | Foreign key referencing Images(Id)              |

### Images

| **Column**     | **Type**   | **Description**                      |
|----------------|------------|--------------------------------------|
| `Id`           | GUID (PK)  | Unique identifier for the image      |
| `EntityId`     | GUID       | Identifier of the associated entity  |
| `Type`         | int        | Type of image (enum represented as int)|
| `Path`         | string     | Path or URL to the image file        |

### Bookings

| **Column**           | **Type**   | **Description**                              |
|----------------------|------------|----------------------------------------------|
| `Id`                 | GUID (PK)  | Unique identifier for the booking            |
| `GuestId`            | GUID (FK)  | Foreign key referencing Users(Id)            |
| `HotelId`            | GUID (FK)  | Foreign key referencing Hotels(Id)           |
| `TotalPrice`         | decimal    | Total price of the booking                   |
| `CheckInDate`        | DateOnly   | Date of check-in                             |
| `CheckOutDate`       | DateOnly   | Date of check-out                            |
| `BookingDate`        | DateOnly   | Date when the booking was made               |
| `GuestRemarks`       | string     | Optional remarks from the guest              |
| `PaymentType`        | int        | Type of payment (enum represented as int)    |
| `CreatedAt`          | DateTime   | Timestamp when the booking was created       |
| `UpdatedAt`          | DateTime   | Timestamp when the booking was last updated  |

### InvoiceDetails

| **Column**                  | **Type**   | **Description**                              |
|-----------------------------|------------|----------------------------------------------|
| `Id`                        | GUID (PK)  | Unique identifier for the invoice detail     |
| `RoomId`                    | GUID (FK)  | Foreign key referencing Rooms(Id)            |
| `BookingId`                 | GUID (FK)  | Foreign key referencing Bookings(Id)         |
| `RoomClassName`             | string     | Name of the room class                       |
| `RoomNumber`                | string     | Number of the room                           |
| `DiscountAppliedAtBooking`  | decimal?   | Discount applied at the time of booking      |
| `PriceAtReservation`        | decimal    | Price at the time of reservation             |
| `AmountAfterDiscount`       | decimal?   | Amount after applying discount               |
| `TaxAmount`                 | decimal?   | Tax amount applied                           |
| `AdditionalCharges`         | decimal?   | Any additional charges                       |
| `TotalAmount`               | decimal    | Total amount for the invoice detail          |

### RoomClasses

| **Column**           | **Type**   | **Description**                              |
|----------------------|------------|----------------------------------------------|
| `Id`                 | GUID (PK)  | Unique identifier for the room class          |
| `HotelId`            | GUID (FK)  | Foreign key referencing Hotels(Id)            |
| `Name`               | string     | Name of the room class                        |
| `Description`        | string     | Description of the room class                 |
| `NightlyRate`        | decimal    | Nightly rate for the room class               |
| `TypeOfRoom`         | int        | Type of room (enum represented as int)        |
| `MaxChildrenCapacity`| int        | Maximum number of children allowed            |
| `MaxAdultsCapacity`  | int        | Maximum number of adults allowed              |
| `CreatedAt`          | DateTime   | Timestamp when the room class was created     |
| `UpdatedAt`          | DateTime   | Timestamp when the room class was last updated|

### Rooms

| **Column**           | **Type**   | **Description**                              |
|----------------------|------------|----------------------------------------------|
| `Id`                 | GUID (PK)  | Unique identifier for the room               |
| `RoomClassId`        | GUID (FK)  | Foreign key referencing RoomClasses(Id)      |
| `Number`             | string     | Room number                                  |
| `CreatedAt`          | DateTime   | Timestamp when the room was created          |
| `UpdatedAt`          | DateTime   | Timestamp when the room was last updated     |

### Reviews

| **Column**     | **Type**   | **Description**                      |
|----------------|------------|--------------------------------------|
| `Id`           | GUID (PK)  | Unique identifier for the review      |
| `GuestId`      | GUID (FK)  | Foreign key referencing Users(Id)      |
| `HotelId`      | GUID (FK)  | Foreign key referencing Hotels(Id)     |
| `Content`      | string     | Content of the review                  |
| `Rating`       | int        | Rating given by the guest              |
| `CreatedAt`    | DateTime   | Timestamp when the review was created  |
| `UpdatedAt`    | DateTime   | Timestamp when the review was last updated |

### Additional Tables

Other tables include `Cities`, `Discounts`, `Owners`, `RoomClasses`, etc., each with appropriate columns and relationships as detailed above.

---

## 🏢 System Architecture

The **Travel and Accommodation Booking Platform** is architected following the **Clean Architecture** paradigm, ensuring a clear separation of concerns, scalability, and maintainability.

### Architecture Layers

1. **Presentation Layer (WebAPI)**
   - **Responsibilities**:
     - Handle HTTP requests and responses.
     - Implement controllers and API endpoints.
     - Utilize Swagger for interactive API documentation.
   - **Technologies**:
     - ASP.NET Core 8.0
     - Swagger/OpenAPI

2. **Application Layer**
   - **Responsibilities**:
     - Contains business logic using the **CQRS** pattern with **MediatR**.
     - Defines commands, queries, and handlers.
     - Implements DTOs and validation using **FluentValidation**.
   - **Technologies**:
     - MediatR
     - FluentValidation
     - AutoMapper

3. **Domain Layer (Core)**
   - **Responsibilities**:
     - Defines domain entities, value objects, enums, and interfaces.
     - Contains business rules and domain logic.
   - **Technologies**:
     - Custom domain models

4. **Infrastructure Layer**
   - **Responsibilities**:
     - Implements data access using **Entity Framework Core**.
     - Manages external services like email sending with **MailKit**.
     - Handles authentication and authorization.
     - Configures dependency injection and logging with **Serilog**.
   - **Technologies**:
     - Entity Framework Core
     - Serilog
     - MailKit

### Architecture Diagram

```
+------------------+
|   Presentation    |
|      (WebAPI)     |
+--------+---------+
         |
         v
+--------+---------+
|    Application    |
| (CQRS with MediatR)|
+--------+---------+
         |
         v
+--------+---------+
|      Domain       |
|      (Core)       |
+--------+---------+
         |
         v
+--------+---------+
|  Infrastructure   |
| (EF Core, Services)|
+-------------------+
```

### Workflow Steps

1. **Client** sends an HTTP request to the **WebAPI**.
2. **WebAPI Controller** receives the request and delegates it to the **Application Layer** using **MediatR**.
3. **Application Layer** processes the command/query, interacting with the **Domain Layer** as needed.
4. **Domain Layer** enforces business rules and interacts with the **Infrastructure Layer** for data access.
5. **Infrastructure Layer** communicates with the database or external services.
6. The response flows back through the layers to the **Client**.

---

### Relationships and Constraints

- **Users & Roles**: Many-to-Many via `UserRoles` table.
- **Users & Bookings**: One-to-Many (A user can have multiple bookings).
- **Users & Reviews**: One-to-Many (A user can write multiple reviews).
- **Hotels & Owners**: Many-to-One (A hotel has one owner; an owner can own multiple hotels).
- **Hotels & Cities**: Many-to-One (A hotel belongs to one city; a city can have multiple hotels).
- **Hotels & RoomClasses**: One-to-Many (A hotel can have multiple room classes).
- **RoomClasses & Rooms**: One-to-Many (A room class can have multiple rooms).
- **RoomClasses & Discounts**: One-to-Many (A room class can have multiple discounts).
- **Bookings & Rooms**: Many-to-Many via `InvoiceDetails` table.
- **Hotels & Reviews**: One-to-Many (A hotel can have multiple reviews).
- **Images**: Polymorphic association via `EntityId` and `Type` to various entities (e.g., Hotels, Cities).

### Notes

- **Enum Representations**: Enums like `PaymentType`, `ImageType`, `HotelStatus`, and `RoomType` are stored as integers in the database.
- **Auditable Entities**: Entities implementing `IAuditableEntity` include `CreatedAt` and `UpdatedAt` fields to track creation and modification times.
---

## Contributing

Contributions are highly appreciated! To contribute to this project, please follow the steps below:

1. **Fork the Repository**

   Click the "Fork" button at the top right corner of the repository page.

2. **Create a Feature Branch**

   ```bash
   git checkout -b feature/YourFeature
   ```

3. **Commit Your Changes**

   ```bash
   git commit -m "Add your message"
   ```

4. **Push to the Branch**

   ```bash
   git push origin feature/YourFeature
   ```

5. **Open a Pull Request**

   Navigate to the repository on GitHub and click "Compare & pull request". Provide a clear description of your changes.

---

## 📄 License

This project is licensed under the [MIT License](LICENSE).
