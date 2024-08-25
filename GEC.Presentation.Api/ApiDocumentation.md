
# API Documentation

## Authentication
- The API uses **JWT (JSON Web Token)** for authentication. 
- Include the token in the `Authorization` header in the format: **`Bearer {token}`**.
- The endpoints in the `ProductController` marked with `[Authorize]` require authentication.
- Some actions are restricted to users with the **"Admin"** role.

## Password Security
- **Passwords** are stored securely using **hashing and salting** techniques. This ensures that even if the database is compromised, the passwords remain protected.

## AccountController
Handles user registration and login.

### `POST /api/Account/Register`
- **Description**: Registers a new user.
- **Request Body**:
  ```json
  {
    "Email": "string (required)",
    "Password": "string (required)",
    "ConfirmPassword": "string (required)",
    "FirstName": "string (required)",
    "LastName": "string (required)"
  }
  ```
- **Authentication**: Not required.
- **Response**:
  - `200 OK`: The registered user's details as `UserViewModel`.
  - `400 Bad Request`: Validation errors.

### `POST /api/Account/Login`
- **Description**: Authenticates a user and returns a **JWT token**.
- **Request Body**:
  ```json
  {
    "Email": "string (required)",
    "Password": "string (required)"
  }
  ```
- **Authentication**: Not required.
- **Response**:
  - `200 OK`: The authenticated user's details as `UserViewModel` along with a **JWT token**.
  - `400 Bad Request`: Validation errors or wrong username/password.

## ProductController
Handles product-related operations.

### `GET /api/Product`
- **Description**: Retrieves all available products.
- **Authentication**: **Required**.
- **Response**:
  - `200 OK`: A list of products as `List<ProductsViewModel>`.
  - `404 Not Found`: No products available.

### `GET /api/Product/{name}`
- **Description**: Retrieves a product by its name.
- **Parameters**:
  - `name` (string, required): The name of the product.
- **Authentication**: **Required**.
- **Response**:
  - `200 OK`: The product details as `ProductsViewModel`.
  - `404 Not Found`: Product not found.

### `POST /api/Product/Add`
- **Description**: Adds a new product. **Only accessible to users with the "Admin" role**.
- **Request Body**:
  ```json
  {
    "Name": "string (required)",
    "Description": "string (required)",
    "Price": "decimal (required)",
    "StockQuantity": "int (required)"
  }
  ```
- **Authentication**: **Required**. **Admin role needed**.
- **Response**:
  - `200 OK`: Product added successfully.
  - `400 Bad Request`: Validation errors.

### `PUT /api/Product/Update`
- **Description**: Updates an existing product. **Only accessible to users with the "Admin" role**.
- **Request Body**:
  ```json
  {
    "Name": "string (required)",
    "NewName": "string (optional)",
    "Description": "string (optional)",
    "Price": "decimal (optional)",
    "StockQuantity": "int (optional)"
  }
  ```
- **Authentication**: **Required**. **Admin role needed**.
- **Response**:
  - `200 OK`: The updated product details as `AdminProductVM`.
  - `404 Not Found`: The target product is not found.

### `DELETE /api/Product/{name}`
- **Description**: Deletes a product by its name. **Only accessible to users with the "Admin" role**.
- **Parameters**:
  - `name` (string, required): The name of the product to delete.
- **Authentication**: **Required**. **Admin role needed**.
- **Response**:
  - `200 OK`: Product deleted successfully.
  - `404 Not Found`: The target product is not found.
