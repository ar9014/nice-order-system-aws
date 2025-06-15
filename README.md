# Nice.OrderSystem

A distributed microservices-based Order Management system built using ASP.NET Core (.NET 9), Docker Compose, Redis, Kafka, MediatR, and Polly. Deployed and tested on AWS EC2 Free Tier.

---

## 🧱 Architecture Overview

- **OrderService** – Handles order creation, retrieval, and publishes events to Kafka.
- **NotificationService** – Receives events via HTTP and handles external notifications.
- **Redis** – Caches order data for quick retrieval.
- **Kafka** – Asynchronous messaging between services.
- **Docker Compose** – Manages service orchestration.

---

## 🚀 Features

- RESTful APIs for order operations
- Redis caching with StackExchange.Redis
- Kafka producer integration
- Polly retry logic for outbound HTTP
- Inter-service HTTP communication
- Full Dockerized setup (local or cloud)

---

## 🧪 How to Run Locally

### 🔧 Prerequisites

- [.NET 9 SDK (preview)](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Docker](https://www.docker.com/products/docker-desktop)
- Git

### ▶️ Local Development (without Docker)

1. Clone the repo:
   ```bash
   git clone https://github.com/ar9014/nice-order-system-aws.git
   cd nice-order-system
   ```

2. Run Redis and Kafka using Docker:
   ```bash
   docker-compose up redis kafka zookeeper
   ```

3. Run services:
   ```bash
   dotnet run --project src/OrderService/OrderService.csproj
   dotnet run --project src/NotificationService/NotificationService.csproj
   ```

---

## 🐳 Docker Compose Deployment

### ▶️ Run All Services (Locally or on AWS EC2)

```bash
docker-compose down -v    # Clean old containers
docker-compose up --build -d
```

### 📦 Exposed Ports

| Service             | Port     |
|---------------------|----------|
| OrderService        | 5000     |
| NotificationService | 7000     |
| Redis               | 6379     |
| Kafka               | 9092     |

---

## ☁️ AWS EC2 Deployment (Free Tier)

### ✅ Steps Summary

1. Launch Ubuntu EC2 (`t2.micro`)
2. SSH into the instance
3. Install Docker + Docker Compose
4. Clone the repo:
   ```bash
   git clone https://github.com/ar9014/nice-order-system-aws.git Nice.OrderSystem.AWS
   cd Nice.OrderSystem.AWS
   ```

5. Build and start services:
   ```bash
   docker-compose up --build -d
   ```

6. Ensure port `5000` is allowed in EC2 security group

---

## 🔍 API Reference

### 📬 Create Order

```
POST /orders
```

**Body:**
```json
{
  "customerId": "8a97f2f9-b4c4-497c-a7d4-f77e8d261f6a",
  "productItems": [
    { "productId": "A", "quantity": 1 },
    { "productId": "B", "quantity": 2 }
  ]
}
```

---

### 📥 Get Order

```
GET /orders/{orderId}
```

Returns order details (from cache if available).

---

## 🛠️ Technologies Used

- ASP.NET Core (.NET 9 Preview)
- MediatR
- Redis (StackExchange)
- Kafka (Confluent + rdkafka)
- Docker & Docker Compose
- Polly for resilience
- AWS EC2 Free Tier (Ubuntu)

---

## 👨‍💻 Author

**Akshay Raut**  
Senior Software Engineer – Backend & Cloud Solutions  
[GitHub Profile](https://github.com/ar9014)

---

## 📌 License

MIT License – do whatever you want 😄
