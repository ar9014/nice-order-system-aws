# 🛒 NICE Order & Notification Microservices

A technical solution to NICE Systems' .NET assignment, showcasing a modular microservices-based backend using modern .NET practices.

---

## 📦 Project Structure

```
Nice.OrderSystem/
├── src/
│   ├── OrderService/             # Main microservice (API, Kafka, Redis, MediatR)
│   ├── NotificationService/      # Mock HTTP service to receive notifications
│   └── OrderService.Tests/       # Unit tests using xUnit and Moq
├── docker-compose.yml            # Redis + Kafka + Zookeeper services
├── Nice.OrderSystem.sln
└── README.md
```

---

## ✅ Features Implemented

| Feature                          | Status |
|----------------------------------|--------|
| REST API (`POST /orders`)        | ✅     |
| REST API (`GET /orders/{id}`)    | ✅     |
| MediatR (CQRS pattern)           | ✅     |
| HTTP integration with retry (Polly) | ✅  |
| Kafka event publishing (`orders.created`) | ✅ |
| Redis caching for GET endpoint   | ✅     |
| Unit tests for business logic    | ✅     |
| Swagger for both APIs            | ✅     |

---

## 🧱 Tech Stack

- **.NET 8**
- **MediatR** – For clean command-handling
- **Polly** – Retry policy for HTTP calls
- **Confluent.Kafka** – Kafka producer
- **StackExchange.Redis** – Redis caching
- **xUnit & Moq** – Unit testing
- **Docker** – Kafka, Redis, Zookeeper setup

---

## 🚀 How to Run Locally

### 1. 🐳 Start Redis + Kafka via Docker

```bash
docker-compose up -d
```

> This launches Kafka, Zookeeper, and Redis on the default ports.

### 2. ▶️ Run NotificationService (port 7000)

```bash
dotnet run --project src/NotificationService
```

### 3. ▶️ Run OrderService (port 5000)

```bash
dotnet run --project src/OrderService
```

### 4. 🧪 Test in Swagger:

- [http://localhost:5000/swagger](http://localhost:5000/swagger) – OrderService
- [http://localhost:7000/swagger](http://localhost:7000/swagger) – NotificationService

---

## 💡 Architecture Decisions

### ✅ Why MediatR?
- Enforces separation of concerns
- Allows clean testing of handlers
- Aligns with CQRS pattern

### ✅ Why Redis?
- Fast, in-memory caching for `GET /orders/{id}`
- Reduces load on storage/backend services

### ✅ Why Kafka?
- Enables scalable, async event-driven communication
- Easily integrates with downstream consumers in production

### ✅ Why Polly?
- Adds resilience in HTTP integration (NotificationService)
- Handles transient faults with exponential backoff

---

## 🔐 Assumptions

- Order persistence is simulated (no database layer).
- NotificationService is mocked (local-only).
- Kafka is only producing to `orders.created`; no consumers implemented.
- Redis is used solely for order read-caching.

---

## 🧪 Testing

```bash
dotnet test
```

- Unit-tested: `CreateOrderHandler` (business logic)
- Dependencies (`INotificationClient`, `IKafkaProducer`) are mocked using Moq
- Assertions use FluentAssertions

---

## 📋 NFR (Non-Functional Requirements)

| Concern        | Approach |
|----------------|----------|
| **Resilience** | Polly retries for NotificationService |
| **Scalability**| Kafka for async communication |
| **Performance**| Redis cache for GET endpoint |
| **Security**   | Assumes internal/private API; security can be layered in with API gateway or auth middleware |
| **Reliability**| Logs to console; can be extended to Serilog + CloudWatch in production |

---

## 🛠️ Troubleshooting & Monitoring

| Concern         | Solution |
|------------------|----------|
| Kafka issues     | Check if port `9092` is open, and Kafka container is healthy |
| Redis not caching| Ensure container is running at `localhost:6379`, and key is not expired |
| HTTP errors      | Retry logic already built-in with Polly (3 tries, exponential backoff) |
| View logs        | Console output is rich with tracing info for Kafka, Redis, and MediatR |

---

## ☁️ (Bonus) AWS Deployment Plan (Optional)

If required, can be included in a separate section or Markdown file (`AWS-Deployment.md`).

---

## ✍️ Author

Developed by **[Your Name / GitHub ID]**  
For NICE Systems – Backend .NET Technical Task
