# 📱 Phone Store E-Commerce API

A scalable and secure backend system for a Phone Store e-commerce platform built with **ASP.NET Core Web API**.  
The project follows **Clean Architecture principles** and implements best practices such as **Separation of Concerns, Repository Pattern, Unit of Work, and Service Layer architecture**.

---

## 🚀 Features

### 👤 Authentication & Users
- User registration and login using **ASP.NET Core Identity**
- Secure authentication using **JWT tokens**
- Role-based authorization support

### 📦 Core E-Commerce Features
- 📂 Categories management
- 📱 Products browsing with detailed specifications (CPU, RAM, Battery, Storage)
- 🛒 Shopping Cart system
- 📦 Order creation and management
- 💳 Payment processing (Cash, Visa, Mastercard)

---

## 🏗️ Architecture

The project is built using **Clean Architecture** and follows this flow:

Controller → Service → Unit of Work → Repository → EF Core → SQL Server

---

### Layers:
- **API Layer** (Controllers)
- **Service Layer** (Business logic)
- **Domain Layer** (Entities)
- **Infrastructure Layer** (Data access, Identity, EF Core)

---

## 🧰 Design Patterns & Practices

- Repository Pattern
- Unit of Work Pattern
- Service Layer Pattern
- Dependency Injection

---

## 🛠️ Technologies Used

- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- ASP.NET Core Identity
- JWT Authentication
- Fluent API (Entity configurations)
- Fluent Validation

---

## 🗄️ Database Overview

The system includes the following main tables:

### 👤 Identity Tables
- AspNetUsers
- AspNetRoles
- AspNetUserRoles
- AspNetUserClaims
- AspNetUserLogins
- AspNetUserTokens

### 🛍️ Business Tables
- Categories
- Products
- Carts
- CartItems
- Orders
- OrderItems
- Payments

### 🔗 Relationships
- A **User** has one **Cart** and many **Orders**
- A **Cart** contains multiple **CartItems**
- A **Product** belongs to a **Category**
- An **Order** contains multiple **OrderItems**
- A **Payment** is linked to an **Order**

---

## 🔐 Authentication Flow

1. User registers or logs in
2. System returns a **JWT token**
3. Token is used to access protected endpoints
4. User can:
   - Browse products and categories
   - Add items to cart
   - Place orders
   - Make payments (Cash / Visa / Mastercard)

---

## 🛒 Business Flow

1. User registers / logs in  
2. Browses categories and products  
3. Adds products to cart  
4. Creates an order from cart  
5. Chooses payment method  
6. Payment is processed and order is completed  

---

## 📌 Notes

- Built with scalability and maintainability in mind
- Follows clean separation between layers
- Uses Fluent Validation for request validation
- Uses Fluent API for database configuration

---

## 📷 Future Improvements

- Admin dashboard for managing products & orders
- Product image uploads
- Email notifications
- Payment gateway integration (Stripe / PayPal)

---

## 👨‍💻 Author

**Yousef Ahmed Fawzy**

- Backend Developer
- ASP.NET Core Enthusiast
