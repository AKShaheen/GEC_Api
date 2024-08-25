
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
  - `Password`: Must meet specific password rules, must not be empty.
    - Has at least one lower character
    - Has at least one Upper character
    - Has at least one Number character
    - Has at least one Special character
    - Minimum length of 10 characters
- **Authentication**: Not required.
- **Response**:
  - `200 OK`: The registered user's details as `UserViewModel`.
  - `400 Bad Request`: Validation errors.

### `POST /api/Account/Login`
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
    "Name": "string (required, max length: 50)",
    "Description": "string (optional, max length: 150)",
    "Price": "decimal (required, must be between 0.01 and 99999999.99)",
    "Stock": "int (required)",
    "Status": "string (required)"
  }
  ```
- **Constraints**:
  - Name: Maximum length of 50 characters, must not be empty.
  - Description: Maximum length of 150 characters, optional.
  - Price: Must be a decimal value between 0.01 and 99999999.99, must not be empty.
  - Stock: Must not be empty.
  - Status: Must not be empty.
- **Authentication**: **Required**. **Admin role needed**.
- **Response**:
  - `200 OK`: Product added successfully.
  - `400 Bad Request`: Validation errors.

### `PUT /api/Product/Update`
- **Description**: Updates an existing product. **Only accessible to users with the "Admin" role**.
- **Request Body**:
  ```json
  {
    "Name": "string (optional, max length: 50)",
    "Description": "string (optional, max length: 150)",
    "Price": "decimal (optional)",
    "Stock": "int (optional)",
    "Status": "string (optional)"
  }
  ```
- **Constraints**:
  -  Name: Maximum length of 50 characters, optional.
  -  Description: Maximum length of 150 characters, optional.
  -  Price: Optional.
  -  Stock: Optional.
  -  Status: Optional.
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
