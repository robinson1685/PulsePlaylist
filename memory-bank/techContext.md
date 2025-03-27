# PulsePlaylist - Technical Context

**Last Updated:** March 27, 2025
**Based On:** Detailed Architecture & Stack Description Provided

This document outlines the key technologies, development setup, constraints, and dependencies for the PulsePlaylist application.

## 1. Technologies Used

### Core Backend & Architecture

- **Runtime/Framework:** .NET 9 with C# 12, ASP.NET Core (using Minimal APIs pattern)
- **Architecture:** Clean Architecture, Domain-Driven Design (DDD)
- **Application Logic:** Mediator pattern (`MediatR`) for CQRS
- **Authentication:** ASP.NET Core Identity
- **Real-time:** SignalR

### Database & Data Access

- **Database:** PostgreSQL
- **ORM:** Entity Framework Core 9 with `Npgsql.EntityFrameworkCore.PostgreSQL` provider
- **Migrations:** EF Core Migrations (managed within Infrastructure project)

### Frontend

- **Mobile/Desktop:** .NET MAUI Blazor Hybrid (iOS, Android planned)
- **Web:** Blazor Web App (Auto interactivity mode - Server & WebAssembly)
- **UI Library:** MudBlazor
- **Web Capabilities:** Progressive Web App (PWA) enabled
- **Offline Storage:** IndexedDB (for client-side caching/offline support)

### API & Documentation

- **API Style:** RESTful via Minimal APIs
- **Specification:** OpenAPI generated via built-in ASP.NET Core support (using `Microsoft.OpenApi` models)
- **API UI:** **Scalar** (via `Scalar.AspNetCore`) for interactive documentation
- **Client Generation:** Microsoft Kiota

### Machine Learning

- **Language/Framework:** Python (3.12+) with FastAPI
- **Library:** PyTorch

### Caching

- **Framework:** FusionCache
- **Backends:** In-Memory and Distributed (Redis via Aspire for dev, Azure Cache for Redis for prod)

### File Storage

- **Service:** **Azure Blob Storage**

### DevOps & Orchestration

- **Development Orchestration:** .NET Aspire
- **Containerization:** Docker
- **Deployment:** GitHub Actions deploying containers, potentially managed via **Docker Compose**
- **Ingress/Reverse Proxy:** **Traefik** (for deployed environments)

### Cross-Cutting Concerns

- **Validation:** FluentValidation
- **Mapping:** AutoMapper
- **Logging:** Serilog (structured logging, sinks for Console, File, OpenTelemetry)
- **Observability:** **OpenTelemetry** (Tracing & Metrics), visualized with **Grafana** (likely with Loki/Tempo/Prometheus backend)

## 2. Development Setup

- **Prerequisites:**
  - Visual Studio 2022 (Latest Preview for .NET 9 Recommended) or compatible IDE
  - .NET 9 SDK
  - Python 3.12+ (with pip)
  - PostgreSQL instance (Local installation or run via Docker/Aspire)
  - Docker Desktop
  - (MAUI): Platform SDKs (Android SDK, Xcode on macOS for iOS)
- **Build Process & Philosophy:**
  - Solution structured according to Clean Architecture.
  - Domain-Driven Design principles guide modeling.
  - Database schema managed via EF Core Migrations (source controlled).
  - Iterative, phased implementation approach.
- **Testing Strategy:**
  - Test-Driven Development (TDD) workflow preferred.
  - **Unit Tests:** xUnit for test framework, FluentAssertions for assertions, Moq for mocking dependencies.
  - **Integration Tests:** xUnit, **TestContainers (`Testcontainers.PostgreSql`)** for isolated database testing.
  - Dedicated test projects per architectural layer (`*.Tests`).

## 3. Technical Constraints

- **Mobile Background Processing:** Critical requirement for MAUI app to collect health data reliably when minimized or screen is off.
- **Real-time Performance:** Low latency needed for processing biometric data, ML inference (if applicable in real-time loop), and pushing music changes via SignalR.
- **Offline Capabilities:** Mobile/Web clients need to handle temporary network loss gracefully, potentially caching upcoming music cues or workout state using IndexedDB/local storage.
- **Battery Efficiency:** Mobile app implementation must minimize power consumption during active workouts.
- **Cross-Platform UI Consistency:** Maintain a similar look, feel, and core functionality across iOS, Android, and Web using MAUI Blazor Hybrid, Blazor Web App, and MudBlazor.
- **Health API Limitations:** Adherence to rules, permissions, and data access patterns of Health Connect (Android) and HealthKit (iOS). Potential differences in data granularity or update frequency.
- **Music Service API Quotas/Limits:** Careful management of API calls to Spotify (and potential YouTube Music) to avoid rate limiting, especially during real-time adaptation. Dependence on API stability and terms of service. **Spotify Premium requirement** for full playback control.

## 4. Dependencies

### Key NuGet Packages

- `Microsoft.AspNetCore.*` (Core, Identity, Minimal APIs, **OpenApi**)
- `Microsoft.AspNetCore.OpenApi.Scalar` _(If using this specific package for Scalar UI)_
- `Microsoft.EntityFrameworkCore.*` (Core, Npgsql, Design, Tools)
- `Microsoft.AspNetCore.Identity.EntityFrameworkCore`
- `MudBlazor`
- `MediatR` / `MediatR.Extensions.Microsoft.DependencyInjection`
- `FluentValidation.AspNetCore`
- `AutoMapper` / `AutoMapper.Extensions.Microsoft.DependencyInjection`
- `FusionCache` / `FusionCache.Backplane.StackExchangeRedis` _(Optional Redis backplane)_
- `Microsoft.Extensions.Caching.StackExchangeRedis`
- `Microsoft.Kiota.Abstractions` / `Microsoft.Kiota.Http.HttpClientLibrary`
- `Testcontainers.PostgreSql`
- `Serilog.AspNetCore` / Sinks (Console, File, **Serilog.Sinks.OpenTelemetry**)
- `OpenTelemetry.Extensions.Hosting` / `OpenTelemetry.Instrumentation.*`
- `Npgsql` (underlying ADO.NET provider)
- `Microsoft.AspNetCore.SignalR.Client` (for frontends)
- `Azure.Storage.Blobs` _(For Azure Blob Storage client)_
- `Aspire.*` _(Relevant Aspire hosting/resource packages like `Aspire.Hosting`, `Aspire.Npgsql.EntityFrameworkCore`, `Aspire.StackExchange.Redis`)_

### External Services / APIs

- **Spotify Web API:** Music streaming control, track metadata, audio features, user library access.
- **YouTube Music API:** (Planned/Limited Scope)
- **Apple HealthKit API:** (iOS) Real-time health metrics.
- **Google Health Connect API:** (Android) Real-time health metrics.
- **OAuth Providers:** Google (for YouTube/Identity), Spotify (for Spotify/Identity).
- **Azure Blob Storage:** Object storage service.
- **Azure Cache for Redis:** (Production target for distributed cache).
- **Azure Key Vault:** (Production target for secrets management).
- **PostgreSQL Database:** (Hosted instance in production, e.g., Azure Database for PostgreSQL).

_(Local development leverages Docker containers for PostgreSQL/Redis managed via .NET Aspire)_
_(Deployment utilizes Docker, Traefik, and GitHub Actions)_
