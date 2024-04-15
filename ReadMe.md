# GamesGlobal Test Project

## Introduction

This document provides an overview of the GamesGlobal project structure. It explains the purpose of each top-level directory and the role of the projects within those directories. Below you'll find a brief description of each component and its responsibilities within the overall architecture.

## Project Structure

**`/src`** - The source directory contains all the application code, organized into libraries and web components.

### Libraries

The `/src/Library` directory contains the core libraries for the GamesGlobal application.

- **`GamesGlobal.Application`**: Contains the application's business logic, including command and query handlers, implemented using the MediatR library for CQRS support.

- **`GamesGlobal.Common`**: Houses common components shared across the application, such as wrappers, utilities, constants, and interfaces for common functionality.

- **`GamesGlobal.Core`**: Defines the core domain entities and aggregates that represent the business model of the application. It also includes repositories and other domain services.

- **`GamesGlobal.Enum`**: Contains all enumerations used throughout the application, promoting consistency and a single source of truth for such constants.

- **`GamesGlobal.Infrastructure`**: Implements the persistence layer, which includes Entity Framework Core configurations, migrations, data seeding, and concrete repository implementations.

### Web

The `/src/Web` directory contains projects that deal with the web interface and API infrastructure.

- **`GamesGlobal.Web.Api`**: The ASP.NET Core project which serves as the entry point for the web API. It defines controllers, middlewares, and filters that handle HTTP requests and responses.

- **`GamesGlobal.Web.Infrastructure`**: Provides the infrastructure necessary for the web layer, including data transfer object (DTO) definitions and other web-related concerns, such as filters or attribute definitions.

## Tests

**`/test`** - Contains all the test projects, ensuring the application behaves as expected.

- **`GamesGlobal.IntegrationTest`**: Contains integration tests that verify the application's components work harmoniously and as intended with external systems.

- **`GamesGlobal.Test.Shared`**: Includes shared testing utilities and fixtures that can be utilized by both integration and unit tests for common setup tasks and object creation.

- **`GamesGlobal.UnitTest`**: Hosts unit tests for the application, focusing on testing individual components in isolation.


## Authentication

The GamesGlobal application leverages the Azure Active Directory B2C (Azure AD B2C) identity service for authentication. This integration ensures a robust, secure, and scalable user identity management system.

Additionally, user-specific context information, such as the user's unique identifier, is made available through the `IUserContext` interface, which can be resolved and used within the application where access to the current user's information is necessary:

### Validating Scopes and Roles

The `[Authorize]` attribute is used throughout the API controllers to enforce that an endpoint requires the caller to be authenticated. The `[RequiredScope]` attribute further tightens security by specifying that certain operations also require the authenticated user to have specific scopes or roles as part of their token claims:

```csharp
[HttpPost]
[RequiredScope("images.write")]

```

In the example above, only authenticated users with an access token containing the `"images.write"` scope would be allowed to call the endpoint. In Azure AD B2C, these scopes are defined in the application registration and assigned during the user flow or custom policies.

## CQRS Architecture

The GamesGlobal project implements the Command Query Responsibility Segregation (CQRS) pattern. This design pattern separates read operations (Queries) from write operations (Commands). The overall structure adheres to the principle of segregating the responsibility of handling command inputs from the responsibility of handling query outputs.

### Commands and Queries

Commands and queries are organized into feature folders, each representing a distinct context or functionality within the application. This organization facilitates code maintenance by grouping related operations together:

- **Commands** encapsulate the data and intention to alter the application state.
- **Queries** encapsulate the parameters and intention to retrieve data without side effects.

### Handlers

Each command or query has a corresponding handler that contains the business logic needed to execute the operation:

- **Command Handlers** execute write operations, processing the command and modifying the state of the application accordingly.
- **Query Handlers** execute read operations by accessing and returning the requested data.

### Validators

Input validation is integral to the CQRS model. Validators ensure that the commands and queries are valid before they reach the handlers:

- **Command Validators** verify that the command data meets defined criteria, such as required fields being populated and values falling within acceptable ranges.
- **Query Validators** (less common) might be utilized to verify that query parameters are valid.

### Mappings

Automapper profiles are employed to define the mappings between different object representations such as Domain Models and Data Transfer Objects (DTOs). This standardizes the transformation process across the application's layers:

- **Mappings** are defined in a centralized manner, which simplifies the maintainability of object transformations and provides a clear overview of how different data structures correlate.

The usage of CQRS within the GamesGlobal project provides clear and robust separation of concerns, allowing for more targeted optimization, easier fault isolation, and potentially resulting in improved scalability and maintainability of the application.

## Entities and Data Context

Entities in the GamesGlobal project are the heart of the domain. These are C# classes that represent the data the application will handle. Each entity corresponds to a table in the database and becomes a crucial part of the Entity Framework Core context.

### Entity Framework Core

Entity Framework Core is an object-relational mapper (ORM) that enables .NET developers to work with a database using .NET objects. It eliminates the need for most of the data-access code developers usually need to write.

### Data Models

`GamesGlobal.Infrastructure/DataAccess/Entities/`: Contains the entity definitions used by the ORM for data operations. Entities inherit from `BaseEntity`, which includes common properties like `Id`, `CreatedAt`, `UpdatedAt`, etc.

### Configurations

`GamesGlobal.Infrastructure/DataAccess/Configurations/`: Homes the EF Core configurations for each entity, detailing how they map to the underlying database constructs.

For instance:
- `ImageConfiguration.cs`: Defines the shape of the images table and its relationships.
- `ProductConfiguration.cs`: Details the schema for the products table, including fields and constraints.

### Interfaces and Abstract Classes

Interfaces like `IProductSpecificationAttributeConfigurationDataModel` and abstract classes like `BaseProductSpecificationAttributeConfigurationDataModel` are used to implement polymorphic configurations for the Entity Framework context.

### Seed Data

`GamesGlobal.Infrastructure/DataAccess/Seed/`: Contains classes that seed the database with initial data for entities.

### DbContext

`GamesGlobal.Infrastructure/DataAccess/GamesGlobalDbContext.cs`: The DbContext is the bridge between the application code and the database. It's where DbSet<T> properties are set up for each entity, configurations are applied, and the connection to the database is established.

In the GamesGlobal project, the specification attribute system is structured to allow products to have additional customizable features that can be specified when a customer adds them to their shopping cart. Here's an overview of how this system is implemented:

## Product Specification Attributes

Specification attributes are designed to define customizable characteristics of products. These can include things such as design options for a t-shirt, where a customer might choose to add a custom image or text.

- **ProductSpecificationAttributeDataModel**: Represents the schema for different attributes that can be associated with a product. Attributes such as "Custom Design" for a t-shirt can be defined through this entity.

### Attribute Configurations

The attribute configurations provide the details on how each attribute can be customized.

- **IProductSpecificationAttributeConfigurationDataModel**: This interface enables various configurations for different types of attributes. For example, it could define how an image can be uploaded and linked to a certain product attribute.
- **ImageProductSpecificationAttributeConfigurationDataModel**: Inherits from the base configuration and specializes in configurations that pertain to image-related attributes, like image specifications for a print-on-demand t-shirt service.

### Shopping Cart Item Attributes

When a product with customizable attributes is added to the shopping cart, these attributes can be tailored as per customer preference.

- **ShoppingCartItemAttributeDataModel**: Connects the `ProductSpecificationAttributeDataModel` with the `ShoppingCartItemDataModel`. It represents the customers' specifications - for instance, the specific image a customer wants to be printed on their t-shirt.

### Attribute Value Implementations

For each customizable attribute, a value must be provided when it is associated with a shopping cart item.

- **IShoppingCartItemAttributeValueDataModel**: Interface that defines the contract for any value associated with a shopping cart item’s attribute. For instance, it could define the contract to store the image file chosen by the customer.
- **ImageShoppingCartItemAttributeValueDataModel**: A concrete implementation of the interface storing the image selection with its associated data. It allows capturing the data necessary to print a customer's image on the product.

### Summary

This system ensures that products like t-shirts with custom images can be created as catalog items with "customizable image" attributes. When customers add these products to their shopping cart, they have the option to customize them - for example, by uploading an image that they want to be printed on the t-shirt. The shopping cart items will then contain the attribute configurations and values that provide all the required details to process the order with the specified customizations.
