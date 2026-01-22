![alt text](https://github.com/comanda-platform/comanda/blob/main/media/comanda-logo.svg?raw=true)
# Comanda
> Simple orders. Clear flow. From kitchen to customer.

**Comanda** is an open-source restaurant management suite designed to streamline operations across kitchen, delivery, and administrative workflows. Built with .NET 10, SignalR, and a CLEAN architecture, it manages orders, inventory, staff, and real-time notifications.

---

**Comanda** is Spanish for *order slip* — the simple but crucial piece of paper that connects the kitchen, the delivery runner, and the person taking orders or payment.

The project is named after this concept: a clear, reliable flow that keeps everyone aligned from the moment an order is placed until it reaches the customer.

---

## Features

- **Multi-App Architecture**  
  Separate apps for kitchen staff, delivery personnel, and admin users, all sharing a unified backend.

- **Order Management**  
  Create, track, and update customer orders efficiently.

- **Inventory & Supplier Management**  
  Track ingredients, supplies, and purchases.

- **Employee Management**  
  Manage staff, roles, and access, including API key-based authentication.

- **Real-Time Notifications**  
  SignalR-powered notifications to all connected clients, ensuring kitchen and delivery teams stay updated.

- **Flexible Client Model**  
  Support for groups of customers, single orders, and multi-person orders.

- **Secure Authentication**  
  JWT and API key authentication with minimal API endpoints.

- **Open-Source & Extensible**  
  Modular design following CLEAN architecture principles, making it easy to extend with new features or client apps.

---

## Architecture

- **Domain**: Core business entities, domain services, and state logic.  
- **Application**: Use cases and service layer for business workflows.  
- **Infrastructure**: Database repositories, adapters, mappers, and notification queues.  
- **API**: Minimal API endpoints, authentication, and SignalR hubs.  
- **Clients**: Separate apps for kitchen, delivery, and admin interfaces.  

---

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- SQL Server or compatible database

### Setup

1. Clone the repository:  
   ```bash
   git clone https://github.com/comanda-platform/comanda.git
   cd comanda

## Author

**Pål Rune Sørensen Tuv**  
Senior Software Engineer / Systems Architect

- GitHub: https://github.com/paaltuv  
- LinkedIn: https://www.linkedin.com/in/p%C3%A5l-rune-s%C3%B8rensen-tuv-702412392/
