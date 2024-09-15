# Blog API

This project is a basic Blog API built using **ASP.NET Core**, showcasing CRUD operations, user authentication, and authorization features. It uses **JWT** for authentication and **Identity** for managing users. The project is structured around the **repository pattern** for better scalability and maintainability.

## Features

- **Authentication & Authorization**: Secure your API with JWT (JSON Web Token) authentication, integrated with ASP.NET Identity for user registration and login.
- **CRUD Operations**: Perform Create, Read, Update, and Delete operations on comments, posts, and tags.
- **Repository Pattern**: Clean architecture and separation of concerns by implementing the repository pattern.

## Technologies Used

- **ASP.NET Core 8.0**
- **MS SQL Server** for database management
- **Entity Framework Core** for database interactions
- **JWT Authentication**
- **ASP.NET Identity** for managing users
- **Repository Pattern** for better maintainability
- **Swagger** for API documentation

## Endpoints

### Account

- **POST** `/api/account/register` - Register a new user
- **POST** `/api/account/login` - Login and retrieve a JWT token

### Comment

- **GET** `/api/comment/all` - Get all comments
- **GET** `/api/comment/{id}` - Get comment by ID
- **POST** `/api/comment/create` - Create a new comment
- **PUT** `/api/comment/update/{id}` - Update an existing comment
- **DELETE** `/api/comment/delete/{id}` - Delete a comment by ID

### Post

- **GET** `/api/post/all` - Get all posts
- **GET** `/api/post/{id}` - Get post by ID
- **POST** `/api/post/create` - Create a new post
- **PUT** `/api/post/update/{id}` - Update an existing post
- **DELETE** `/api/post/delete/{id}` - Delete a post by ID

### Tag

- **GET** `/api/tag/all` - Get all tags
- **GET** `/api/tag/{id}` - Get tag by ID
- **POST** `/api/tag/create` - Create a new tag
- **PUT** `/api/tag/update/{id}` - Update an existing tag
- **DELETE** `/api/tag/delete/{id}` - Delete a tag by ID

## Installation & Setup

1. Clone the repository:
    ```bash
    git clone https://github.com/Ammar-Barakat/WEB-API-Demo-Project
    ```
   
2. Navigate into the project directory:
    ```bash
    cd WEB-API-Demo-Project
    ```

3. Install the dependencies:
    ```bash
    dotnet restore
    ```

4. Set up your database (replace `Data Source=.;Initial Catalog=BlogAPI;Integrated Security=True;Encrypt=False;Trust Server Certificate=False;` in `appsettings.json`):
    ```json
    {
      "ConnectionStrings": {
        "DefaultConnection": "Data Source=.;Initial Catalog=BlogAPI;Integrated Security=True;Encrypt=False;Trust Server Certificate=False;"
      }
    }
    ```

5. Run database migrations:
    ```bash
    dotnet ef database update
    ```

6. Run the application:
    ```bash
    dotnet run
    ```
