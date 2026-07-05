# KareMa – Service Booking Platform 🚀

A full-featured service marketplace inspired by modern on-demand platforms. KareMa connects **Customers**, **Experts**, and **Admins** inside a clean, scalable ASP.NET Core MVC architecture.

This isn’t a simple CRUD app — KareMa handles real-world business logic such as job requests, expert suggestions, profile images, city management, and a secure payment flow after job completion.

## 🎯 Project Overview
KareMa is a platform where Customers request services, Experts submit suggestions and complete jobs, and Admins manage the entire system.

### 👥 Core System Roles
- **Admin** – full management over the platform  
- **Expert** – responds to service requests and performs the work  
- **Customer** – submits requests and approves completed jobs  

---

## 🛠️ Tech Stack

### Backend
- ASP.NET Core MVC  
- C#  
- Entity Framework Core  
- Repository + Service + AppService Architecture  
- SQL Server  

### Frontend
- Razor Views  
- Bootstrap  
- jQuery (minimal use)  

### Other Tools
- AutoMapper  
- FluentValidation  
- Authentication & Authorization

---

## 📁 Project Structure


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


---

## ⭐ Core Features

### 👤 Role-Based System
- **Customer:** Creates service requests  
- **Expert:** Sends suggestions & completes the job  
- **Admin:** Manages users, experts, cities, orders, and system data  
Each role includes its own profile picture.
---
### 🛠️ Service Request Workflow
1. Customer creates a service request  
2. Experts view the request and submit suggestions  
3. Customer selects and approves one suggestion  
4. Expert completes the job  
5. Customer confirms job completion  
6. Payment is automatically processed — **Customer → Expert**

👉 Financial logic is implemented inside **Repository + Configuration layer**, *not the Service layer*, ensuring clean architecture.

---

## 💳 Financial & Transaction System
- Balance tracking for Customers & Experts  
- Automatic transfer after job confirmation  
- Transaction table for full history  
- No payment logic inside controllers (clean separation)  

---

## 🏙️ City & Location Management
- Cities table  
- Customers & Experts linked to cities  
- Used for filtering & UI logic across the system  

---

## 🖼️ Image Management
- Separate **Image** entity  
- Used for profile pictures + service photos  
- Files stored in **wwwroot**, metadata stored in database  

---

## 🚀 Development Log – Phase 1

### ✔ Completed
- Project architecture  
- Entities & relationships  
- Repository & Service layers  
- Suggestion system  
- Order workflow  
- Expert payment system  
- City + Image modules  
- Razor UI + Responsive design  
- Full multi-role support
-  Improve UI/UX  
- Add authentication  

### 🔧 Next Steps
- Integrate SMS & online payments  
- Deploy to hosting
- Notifications  
- Dedicated API for mobile app
---
## 🎯 Project Goals
Designed to demonstrate real-world backend skills with a focus on:
- Clean, scalable architecture  
- Realistic business workflows  
- Proper separation of concerns  
- Production-ready code structure  

