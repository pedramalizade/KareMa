# KareMa – On-Demand Service Platform 🚀

KareMa is a clean and scalable service-booking platform built with ASP.NET Core MVC. It connects **Customers**, **Experts**, and **Admins** through a real workflow system including job requests, expert suggestions, order approval, and automatic payment transfers.

## 🎯 Overview
KareMa is not a simple CRUD project. It includes real business logic such as:
- Service request management  
- Expert suggestion system  
- Order approval flow  
- Financial transactions  
- City management  
- Profile image management  
- Multi-role system (Admin, Expert, Customer)

## 🛠 Tech Stack
**Backend:** ASP.NET Core MVC, C#, EF Core, SQL Server  
**Architecture:** Repository + Service + AppService  
**Frontend:** Razor Views, Bootstrap  
**Tools:** AutoMapper, FluentValidation  

## 📂 Project Structure

KareMa/

├── Core/<br/>
│       ├── Entities/     
│       ├── Interfaces/     
│       └── DTOs/         
├── Infrastructure/<br/>
│       ├── Data/                  
│       ├── Repositories/         
│       └── Migrations/<br/>
├── Services/<br/>
│       ├── Business/              
│       └── AppServices/           
├── Web/<br/>
│       ├── Controllers/           
│       ├── Views/                
│       ├── ViewModels/           
│       └── wwwroot/                        

## ⭐ Main Features
### 👥 Role System
- **Customer:** Creates service requests  
- **Expert:** Sends suggestions & completes jobs  
- **Admin:** Manages users, experts, cities, and orders  

### 🛠 Service Request Flow
1. Customer creates a request  
2. Experts submit suggestions  
3. Customer selects one suggestion  
4. Expert completes the job  
5. Customer confirms completion  
6. System transfers money from Customer → Expert automatically  

💡 *Financial logic is implemented in the Repository layer, not in Controllers.*

### 💳 Financial System
- User balance tracking  
- Automatic transfer after job completion  
- Transaction history table  
- No business logic inside controllers

### 🏙 City Management
- City entity  
- Linked to Customers & Experts  
- Used for filtering and UI logic

### 🖼 Image Management
- Dedicated Image entity  
- Used for profile images and service photos  

## 🚀 Development Status
### Completed
- Architecture setup  
- Entities + relationships  
- Repository & Service layers  
- Suggestion & order system  
- Expert payment logic  
- City + images module  
- Responsive Razor UI  
- Multi-role support  

### Next Steps
- Authentication & Authorization  
- SMS verification  
- Online payment gateway  
- Rating system  
- API for mobile app  
