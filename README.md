
# Talabat E-Commerce API

The Talabat E-Commerce API, built with .NET 8 and Angular, is designed to power a modern e-commerce platform, offering comprehensive functionalities such as efficient product management with CRUD operations, secure user authentication, seamless order processing, intuitive basket management, Stripe payment integration for secure transactions, and management of delivery methods.


## Features

- **Product Management:** CRUD operations for products.
- **Category Management:** Organize products by categories.
- **Shopping Cart:** Add, update, and remove items in the shopping cart.
- **Order Management:** Place orders, track order status, and view order history.
- **Payment Integration:** Stripe integration for secure and efficient payments.
- **JWT Authentication:** Secure API access using JSON Web Tokens.
- **Specification Pattern:** Efficient querying and filtering of data.
- **Unit of Work Pattern:** Streamlines transaction management and improves efficiency.
- **Clean Architecture:** Separation of concerns, scalability, and maintainability.

## Technologies Used

- **.NET 8**: Core framework for building the API.
- **C#**: Primary language used for development.
- **Entity Framework Core**: For database management and CRUD operations.
- **SQL Server**: Database used for persistent storage.
- **Stripe**: For payment processing integration.
- **Unit of Work & Specification Patterns**: For code scalability and efficiency.
- **JWT Authentication**: For user authentication and secure API access.
- **Swagger**: API documentation and testing.

## Getting Started

### Prerequisites

- [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Stripe Account](https://stripe.com/) (For payment integration)

### Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/Ahmed-Abdrabo/Talabat.API.git
   ```

2. **Navigate to the project directory:**
   ```bash
   cd Talabat.API
   ```
   
3. Set up the database:

   - Update the \`appsettings.json\` file with your SQL Server connection string.
   - Apply migrations to create the database schema:

     ```bash
     dotnet ef database update
     ```

4. Set up Stripe:

   - Add your **Stripe** API keys in the \`appsettings.json\` file under \`StripeSettings\`.

5. Run the application:

   ```bash
   dotnet run
   ```

6. Access the API documentation at:

   ```
   https://localhost:{port}/swagger
   ```

## API Endpoints

### Account
- **POST** `/api/Account/login` - Authenticate a user and return a JWT token.
- **POST** `/api/Account/register` - Register a new user.
- **GET** `/api/Account` - Get the details of the authenticated user.
- **GET** `/api/Account/address` - Retrieve the authenticated user's address.
- **PUT** `/api/Account/address` - Update the authenticated user's address.
- **GET** `/api/Account/emailExists` - Check if an email already exists.

![image](https://github.com/user-attachments/assets/de4aaab7-a36e-4580-b19d-9bc98d4d7fef)


### Basket
- **GET** `/api/Basket` - Retrieve the current user's shopping basket.
- **POST** `/api/Basket` - Add an item to the shopping basket.
- **DELETE** `/api/Basket` - Remove an item from the shopping basket.

![image](https://github.com/user-attachments/assets/c3778af0-f071-4876-a480-613e255a0471)


### Orders
- **POST** `/api/Orders` - Place a new order.
- **GET** `/api/Orders` - Retrieve a list of orders for the authenticated user.
- **GET** `/api/Orders/{id}` - Retrieve order details by order ID.
- **GET** `/api/Orders/deliverymethods` - Retrieve available delivery methods.

![image](https://github.com/user-attachments/assets/b59ffc3d-6b86-4167-bffc-c65ef2b10cf0)


### Payments
- **POST** `/api/Payments/{basketId}` - Process a payment for the specified basket.
- **POST** `/api/Payments/webhook` - Handle webhook events for payments.

![image](https://github.com/user-attachments/assets/931d431b-6413-427c-b61c-cda9ffbd64dc)


### Products
- **GET** `/api/Products` - Retrieve all products.
- **GET** `/api/Products/{id}` - Retrieve a specific product by ID.
- **GET** `/api/Products/categories` - Retrieve a list of product categories.
- **GET** `/api/Products/brands` - Retrieve a list of product brands.

![image](https://github.com/user-attachments/assets/fa80f495-7bc9-4916-aeaa-61bceea32ad0)


### JWT Authentication
To access secure endpoints, you must include a valid JWT token in the request header:
```http
Authorization: Bearer <token>
```


## Contact

- **Author:** Ahmed Abdrabo
- **Email:** ahmedabdraboo221b@gmail.com
