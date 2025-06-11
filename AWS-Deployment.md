# ☁️ AWS Deployment Plan – NICE Order Microservices

This document outlines how to deploy the OrderService and NotificationService to AWS using containerized architecture.

---

## 🧱 1. Architecture Overview

```
[Client] --> [API Gateway] --> [ECS / Fargate Service (OrderService)]
                             |
                             --> [ECS / Fargate Service (NotificationService)]
                             |
                             --> [ElastiCache for Redis]
                             |
                             --> [Amazon MSK (Kafka)]
```

---

## 🚀 2. Deployment Strategy

### 🐳 Containers
- Containerize both `OrderService` and `NotificationService` using Docker
- Push to **Amazon ECR** (Elastic Container Registry)

### ☸️ Container Orchestration
- Deploy both services on **AWS ECS** (Fargate) or **EKS**
- Fargate is preferred for small teams (no server maintenance)

---

## 🔧 3. Services to Provision

| AWS Service       | Purpose                         |
|-------------------|---------------------------------|
| **ECS Fargate**   | Run containerized .NET services |
| **ECR**           | Store Docker images             |
| **API Gateway**   | Expose OrderService REST API    |
| **ElastiCache**   | Managed Redis for caching       |
| **MSK (Kafka)**   | Managed Kafka cluster           |
| **CloudWatch**    | Logs and metrics                |
| **IAM**           | Role-based access control       |

---

## 🔐 4. IAM & Security

- Create ECS Task Role for OrderService with permissions:
  - Publish to Kafka (MSK)
  - Access ElastiCache endpoint
  - Send logs to CloudWatch
- Use **parameter store or secrets manager** for environment secrets

---

## 📦 5. Docker Image Build Example

```bash
# In OrderService folder
docker build -t orderservice .
docker tag orderservice:latest <your-aws-account-id>.dkr.ecr.<region>.amazonaws.com/orderservice:latest
docker push <your-ecr-uri>
```

Repeat for NotificationService.

---

## 🧪 6. Post-deployment Checklist

- ✅ Swagger accessible via API Gateway
- ✅ Kafka topic (`orders.created`) receiving events
- ✅ Redis working (cache hits confirmed)
- ✅ CloudWatch logs confirming Polly retries, Kafka publish, etc.

---

## 📌 7. Monitoring & Alerts

- Use CloudWatch Logs for each container
- Set up alarms for:
  - Failed HTTP calls (NotificationService)
  - Kafka publish failures
  - Redis unavailability

---

## 🧠 8. CI/CD (Optional Bonus)

- Use **GitHub Actions** or **AWS CodePipeline**
- On push to `main`, trigger:
  - Docker build
  - Image push to ECR
  - ECS task deployment update

---

## ✍️ Notes

- For demo/testing: Use t3.micro EC2 or dev tier ElastiCache/MSK
- Security: Use HTTPS, VPC subnets, and scoped IAM roles
- Resilience: ECS health checks + Polly + Kafka

---

## 📞 Questions?

If required, describe fallback strategies, rollbacks, or autoscaling options.
