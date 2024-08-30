# 📦 Generic Ecommerce API Documentation

---

## 📑 Table of Contents

| **🗂 Section**                      | **🔗 Subsections**                                                                                     |
|-------------------------------------|--------------------------------------------------------------------------------------------------------|
| **[🔐 Authentication](#-authentication)**         |                                                                                                        |
| **[🔒 Password Security](#-password-security)**   |                                                                                                        |
| **[👤 AccountController](#-accountcontroller)**   | [View Endpoints](#accountcontroller-endpoints)                                                         |
| **[🛒 ProductController](#-productcontroller)**   | [View Endpoints](#productcontroller-endpoints)                                                         |
| **[📦 OrderController](#-ordercontroller)**       | [View Endpoints](#ordercontroller-endpoints)                                                           |
| **[🌱 Data Seeding](#-data-seeding)**             | [View Details](#data-seeding-details)                                                                   |

---
## 📝 Controllers Summary

### **👤 AccountController Endpoints**

| **HTTP Method** | **Endpoint**        | **Description**                          | **Authentication**    | **Response Status Codes**     |
|-----------------|----------------------|------------------------------------------|------------------------|-------------------------------|
| `POST`          | /Account/Register    | Registers a new user.                    | Not required           | `200 OK`, `400 Bad Request`   |
| `POST`          | /Account/Login       | Authenticates a user and returns a JWT token. | Not required           | `200 OK`, `400 Bad Request`   |

---

### **🛒 ProductController Endpoints**

| **HTTP Method** | **Endpoint**                | **Description**                                  | **Authentication**           | **Response Status Codes**             |
|-----------------|------------------------------|--------------------------------------------------|-------------------------------|---------------------------------------|
| `GET`           | /Product/GetAllProducts      | Retrieves all available products.                | Required (Authentication Mode) | `200 OK`, `401 Unauthorized`, `404 Not Found` |
| `GET`           | /Product/GetProductsById/{id} | Retrieves a product by its ID.                   | Required (Authentication Mode) | `200 OK`, `401 Unauthorized`, `404 Not Found` |
| `POST`          | /Product/AddProduct          | Adds a new product. **Admin role required**     | Required (Admin role needed) | `200 OK`, `401 Unauthorized`, `403 Forbidden`, `400 Bad Request` |
| `PUT`           | /Product/UpdateProduct       | Updates an existing product. **Admin role required** | Required (Admin role needed) | `200 OK`, `401 Unauthorized`, `403 Forbidden`, `404 Not Found` |
| `DELETE`        | /Product/DeleteProduct/{id}  | Deletes a product by its ID. **Admin role required** | Required (Admin role needed) | `200 OK`, `401 Unauthorized`, `403 Forbidden`, `404 Not Found` |

---

### **📦 OrderController Endpoints**

| **HTTP Method** | **Endpoint**       | **Description**                                | **Authentication**    | **Response Status Codes**       |
|-----------------|---------------------|------------------------------------------------|------------------------|---------------------------------|
| `GET`           | /Order/GetAllOrders | Retrieves all available orders.                | Required               | `200 OK`, `401 Unauthorized`, `404 Not Found` |
| `POST`          | /Order/AddOrder     | Adds a new order.                             | Required               | `200 OK`, `401 Unauthorized`, `404 Not Found`, `400 Bad Request` |
| `DELETE`        | /Order/DeleteOrder/{id} | Deletes an order by its ID.     

---

## 🔐 Authentication

### 🚀 Enabling Authentication Mode
To enable authentication mode, configure preprocessor symbols in the `Directory.Build.props` file.

### 🎯 Purpose
Defining preprocessor symbols in a `Directory.Build.props` file allows you to compile sections of your code conditionally across the entire solution. This is useful for enabling or disabling features based on the configuration.

### 🛠️ Configuration Steps

1. **Open the Solution's `Directory.Build.props` File**
2. **Define Preprocessor Symbols**
   - Add the `<DefineConstants>` tag inside the `<PropertyGroup>` element to define your preprocessor symbols.

Here's the basic structure to add a symbol named `AuthMode`:

```xml
<Project>
    <PropertyGroup>
        <!-- You Can Choose One Of the Below Options -->
        <DefineConstants>AuthMode</DefineConstants> <!-- Defining The Tag to Enable Authentication Mode -->
        <DefineConstants></DefineConstants> <!-- No Adding a Tag to Disable Authentication Mode -->
    </PropertyGroup>
</Project>
```
### 🔑 Authentication Overview
- The API uses **JWT (JSON Web Token)** for authentication. 
- Include the token in the `Authorization` header in the format: **` Bearer {token}`**.
- The endpoints in the `ProductController` marked with `[Authorize]` require authentication.
- Some actions are restricted to users with the **"Admin"** role.

## 🔒 Password Security
- **Passwords** are stored securely using **hashing and salting** techniques. This ensures that the passwords remain protected even if the database is compromised.

## 📋 AccountController
Handles user registration and login.

### `POST /Account/Register`
- **Description**: Registers a new user.
- **Request Body**:
  ```json
  {
    "Name": "string (required, max length: 50)",
    "Address": "string (required)",
    "Phone": "string (required, max length: 14)",
    "Email": "string (required, valid email format)",
    "Password": "string (required, must meet password rules)"
  }
  ```
- **Constraints**: 
  - `Name`: Maximum length of 50 characters, must not be empty.
  - `Address`: Must not be empty.
  - `Phone`: Maximum length of 14 characters, must not be empty.
  - `Email`: Must be a valid email format, must not be empty.
  - `Password`: Must meet specific password rules, and must not be empty.
    - Has at least one lower character
    - Has at least one Upper character
    - Has at least one Number character
    - Has at least one Special character
    - Minimum length of 10 characters
- **Authentication**: Not required.
- **Response**:
  - `200 OK`: The registered user's details as `UserViewModel`
      ```json
    {
      "UserId": "Guid",
      "Name": "string",
      "Address": "string",
      "Phone": "string",
      "Email": "string"
    }
    ```
  - `400 Bad Request`: Validation errors.

### `POST /Account/Login`
- **Description**: Authenticates a user and returns a **JWT token**.
- **Request Body**:
  ```json
  {
    "Email": "string (required, valid email format)",
    "Password": "string (required)"
  }
  ```
- **Constraints**:
  - `Email`: Must be a valid email format, must not be empty.
  - `Password`: Must not be empty.
- **Authentication**: Not required.
- **Response**:
  - `200 OK`: The registered user's details as `UserViewModel`
      ```json
    {
      "UserId": "Guid",
      "Name": "string",
      "Address": "string",
      "Phone": "string",
      "Email": "string"
    }
  - `200 OK`: The registered user's details as `UserViewModel` **Authentication Mode Enabled**
      ```json
    {
      "UserId": "Guid",
      "Name": "string",
      "Address": "string",
      "Phone": "string",
      "Email": "string"
      "Token": "string"
    }
  - `400 Bad Request`: Validation errors or wrong username/password.

## 🛒 ProductController
Handles product-related operations. **Note**: Authentication constraints only applied when **Authentication Mode** is enabled.

### `GET /Product/GetAllProducts`
- **Description**: Retrieves all available products.
- **Authentication**: Required.
- **Response**:
  - `200 OK`: A list of products as `List<ProductsViewModel>`
    ```json
    [
      {
        "productId": "Guid",
        "name": "string",
        "description": "string",
        "price": "decimal",
        "stock": "int",
        "status": "boolean"
      },
    ]
    ```
  - `401 Unauthorized`: Unauthorized *Authentication Mode*.
  - `404 Not Found`: No products available.

### `GET /Product/GetProductsById/{id}`
- **Description**: Retrieves a product by its ID.
- **Parameters**:
  - `id` (Guid, required): The ID of the product.
- **Authentication**: **Required**.
- **Response**:
  - `200 OK`: The product details as `ProductsViewModel`
      ```json
    {
      "productId": "Guid",
      "name": "string",
      "description": "string",
      "price": "decimal",
      "stock": "int",
      "status": "boolean"
    }
  - `401 Unauthorized`: Unauthorized `Authentication Mode`.
  - `404 Not Found`: Product not found.

### `POST /Product/AddProduct`
- **Description**: Add a new product. **Only accessible to users with the "Admin" role In `Authentication Mode`**.
- **Request Body**:
  ```json
  {
    "Name": "string (required, max length: 50)",
    "Description": "string (optional, max length: 150)",
    "Price": "decimal (required, must be between 0.01 and 99999999.99)",
    "Stock": "int (required)",
    "Status": "boolean (required)"
  }
  ```
- **Constraints**:
  - Name: Maximum length of 50 characters, must not be empty.
  - Description: Maximum length of 150 characters, optional.
  - Price: Must be a decimal value between 0.01 and 99999999.99, and must not be empty.
  - Stock: Must not be empty.
  - Status: Must not be empty.
- **Authentication**: Required. `Admin role needed`.
- **Response**:
  - `200 OK`: Product added successfully.
  - `401 Unauthorized`: Unauthorized `Authentication Mode`.
  - `403 Forbidden`: Forbidden `Authentication Mode`.
  - `400 Bad Request`: Validation errors.

### `PUT /Product/UpdateProduct`
- **Description**: Updates an existing product. **Only accessible to users with the "Admin" role `Authentication Mode`**.
- **Request Body**:
  ```json
  {
    "Name": "string (required, max length: 50)",
    "Description": "string (optional, max length: 150)",
    "Price": "decimal (required, must be between 0.01 and 99999999.99)",
    "Stock": "int (required)",
    "Status": "boolean (required)"
  }
  ```
- **Constraints**:
  -  Name: Maximum length of 50 characters, required.
  -  Description: Maximum length of 150 characters, optional.
  -  Price: Must be a decimal value between 0.01 and 99999999.99, and must not be empty.
  -  Stock: required.
  -  Status: required.
- **Authentication**: Required. `Admin role needed`.
- **Response**:
  - `200 OK`: A list of products as `ProductsViewModel`
    ```json
    {
      "productId": "Guid",
      "name": "string",
      "description": "string",
      "price": "decimal",
      "stock": "int",
      "status": "boolean"
    }
    ```
  - `401 Unauthorized`: Unauthorized `Authentication Mode`.
  - `403 Forbidden`: Forbidden `Authentication Mode`.
  - `404 Not Found`: No products available.

### `DELETE /Product/DeleteProduct/{id}`
- **Description**: Deletes a product by its id. **Only accessible to users with the "Admin" role `Authentication Mode`**.
- **Parameters**:
  - `id` (Guid, required): The ID of the product to delete.
- **Authentication**: Required. `Admin role needed`.
- **Response**:
  - `200 OK`: Product deleted successfully.
  - `401 Unauthorized`: Unauthorized `Authentication Mode`.
  - `403 Forbidden`: Forbidden `Authentication Mode`.
  - `404 Not Found`: The target product is not found.

## 📦 OrderController
Handles Order-related operations.

### `GET /Order/GetAllOrders`
- **Description**: Retrieves all available products.
- **Authentication**: Required.
- **Response**:
  - `200 OK`: A list of Orders as `List<OrdersViewModel> including List<OrderItemsViewModel>`
    ```json
    [
      {
        "orderId": "Guid",
        "amount": "decimal",
        "tax": "decimal",
        "totalAmount": "decimal",
        "orderDate": "DateTime",
        "userId": "Guid",
        "orderItems": [
          {
            "productId": "Guid",
            "quantity": "int",
            "cost": "decimal"
          }
        ]
      }
    ]
  - `401 Unauthorized`: Unauthorized *Authentication Mode*.
  - `404 Not Found`: No products available.

### `POST /Order/AddOrder`
- **Description**: Add a new product.
- **Request Body**:
  ```json
    {
      "userId": "Guid (required)",
      "orderItems": [
        {
          "productId": "Guid (required)",
          "quantity": "int (required)"
        }
      ]
    }
  ```
- **Constraints**:
  - userId: must not be empty.
  - productId: must not be empty.
  - quantity: must not be empty.
- **Authentication**: Required.
- **Response**:
  - `200 OK`: Product Added successfully.
  - `401 Unauthorized`: Unauthorized `Authentication Mode`.
  - `404 Not Found`: Returned if the user or product is not found.
  - `400 Bad Request`: Any errors.

  ### `DELETE /DeleteOrder/{id}`
- **Description**: Deletes an Order by its ID.
- **Parameters**:
  - `id` (Guid, required): The ID of the Order to delete.
- **Authentication**: Required.
- **Response**:
  - `200 OK`: Product deleted successfully.
  - `401 Unauthorized`: Unauthorized `Authentication Mode`.
  - `404 Not Found`: The target product is not found.

## 🌱 Data Seeding
Handles user registration and login.

### `Seeding an Admin User`
Inside the Code There is A Seeding Service Configured With Some Admin Data
**Login Credentials**:
  ```json
    {
      "email": "admin@ldc.com",
      "password": "AdminAdmin1#"
    }
  ```