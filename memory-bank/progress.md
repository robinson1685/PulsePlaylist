# PulsePlaylist - Project Progress

**Last Updated:** March 27, 2025
**Based On:** Project setup, planning, and initial phase discussions.

## Current Status

- **Active Phase:** Phase 1: Core Domain Model Setup.
- **Current Tasks:** Implementing domain entities (`User`, `Track`, `AudioFeatures`), value objects (`WorkoutIntensity`, `PlaylistSettings`), domain events, domain exceptions, and associated unit tests (`PulsePlaylist.Domain.Tests`) using a Test-Driven Development (TDD) approach.
- **Methodology:** Writing tests first in xUnit/FluentAssertions, then implementing the domain logic to make the tests pass, following Clean Architecture and DDD principles.
- **Immediate Next Step:** Complete Phase 1 implementation (including domain services) and prepare to start Phase 2 (Data Access Layer).

## What Works (Completed / Established)

1.  **Project Foundation:**
    - Solution structure created with distinct projects for Domain, Application, Infrastructure, API, Shared (UI), Web, Mobile, AppHost (Aspire), ServiceDefaults (Aspire) under `src/`. `PulsePlaylist.AdaptationEngine` (Python ML) also under `src/`. Test projects under `tests/`.
    - Basic `.gitignore` configured and operational, correctly ignoring `bin`/`obj`.
2.  **Technology Stack Defined:**
    - Core stack selected: .NET 9, C# 12, PostgreSQL, EF Core 9, ASP.NET Core Identity, Minimal APIs, MAUI Blazor Hybrid, Blazor Web App, MudBlazor, .NET Aspire, Python/FastAPI/PyTorch, **Microsoft.OpenApi with Scalar UI**, AutoMapper, FluentValidation, SignalR, TestContainers, Serilog, OpenTelemetry, Azure Blob Storage, Docker, Traefik, GitHub Actions, Grafana.
3.  **Architecture & Planning:**
    - Clean Architecture and DDD principles adopted as the architectural foundation.
    - Detailed, phased iterative build plan created, prioritizing the mobile app MVP core loop.
    - Foundational documents generated: `projectbrief.md` (v1.1), `productContext.md` (v1.1), `techContext.md`, `systempatterns.md`, and this `progress.md`.
    - Expert assessment reviewed, confirming feasibility and highlighting key risks (esp. YouTube Music).
4.  **Initial Tooling & Prompts:**
    - Detailed, context-rich prompts generated for guiding the implementation of Phase 1 (Domain), Phase 2 (Data Access), and Phase 3 (API).

## What's Left to Build (Remaining Tasks by Phase - MVP Focus)

- **Phase 1: Core Domain Model (In Progress)**
  - Complete implementation of all entities (`User`, `Track`, `AudioFeatures`, `Playlist`, `WorkoutSession`), value objects (`WorkoutIntensity`, `PlaylistSettings`), domain events, domain exceptions, and domain services (`PlaylistCurationService`, `WorkoutAnalysisService`, `SpotifyMusicMetadataService`).
  - Ensure full unit test coverage for the Domain layer.
- **Phase 2: Data Access Layer**
  - Verify `ApplicationDbContext` extending `IdentityDbContext<ApplicationUser>` for PostgreSQL.
  - Configure EF Core entities and relationships (using Fluent API) for domain models.
  - Implement Repository pattern interfaces (`IUserRepository`, `ITrackRepository`, etc.) in `Infrastructure`.
  - Set up EF Core Migrations for both domain and Identity schemas within `PulsePlaylist.Infrastructure`.
  - Configure PostgreSQL connection string (using User Secrets locally) and resource definition in `.NET Aspire` (`PulsePlaylist.AppHost`).
  - Set up integration tests using `TestContainers.PostgreSql` in `Infrastructure.Tests`.
  - Implement database seeding (e.g., using Bogus).
- **Phase 3: First API Endpoints**
  - Configure Minimal API endpoints in `PulsePlaylist.API` using endpoint registration pattern.
  - Integrate ASP.NET Core Identity (using PostgreSQL store) for User Registration & Login endpoints (secure token handling TBD - likely cookie/reference token based).
  - Implement User Profile (Get/Update) and Track search/details/favorites endpoints.
  - Set up AutoMapper profiles and configuration.
  - Configure **Microsoft.OpenApi with Scalar UI** for documentation.
  - Implement validation using FluentValidation via MediatR pipeline behavior.
  - Set up global exception handling middleware and CORS.
  - Write API integration tests (using `WebApplicationFactory` and TestContainers).
- **Phase 4: ML Service Foundation**
  - Set up Python/FastAPI project (`PulsePlaylist.AdaptationEngine`).
  - Implement basic `/predict` endpoint using **simple rules** (no real ML model yet).
  - Containerize the ML service (Dockerfile).
  - Configure ML service (as container or executable) in `.NET Aspire`.
  - Implement client (`IMLServiceClient`) in `Infrastructure` layer to call the ML service.
- **Phase 5: Mobile App Core Experience (MVP Focus)**
  - Set up `PulsePlaylist.Mobile` (MAUI Blazor Hybrid) with MudBlazor.
  - Implement **Authentication UI flow connecting to API/Identity** (Method TBD: WebAuthenticator for OAuth flow, secure token storage - **needs confirmation if JWT or other token needed alongside Identity cookies/session**).
  - Implement _basic_ **HealthKit (iOS)** and/or **Health Connect (Android)** integration (via platform-specific code) to read **real-time Heart Rate**.
  - Implement logic to calculate basic intensity and send to backend API.
  - Create basic Workout screen UI (display HR, current track, controls).
  - Integrate **Spotify Mobile SDK** for playback control (requires Spotify Premium).
  - Implement **SignalR client** to receive next track updates from API.
  - Ensure reliable **background operation** for HR tracking, SignalR, and Spotify SDK playback.
- **Phase 6: Refine Core Loop & Basic Management**
  - Improve intensity calculation and backend **rule-based adaptation logic**.
  - Implement basic `PulsePlaylist.Web` UI (Login/Register via Identity, read-only Workout History).
  - Implement workout history saving/retrieval (API & DB).
- **Phase 7+: Enhance Personalization, Full Music/ML, Real-time Adaptation, DevOps, QA, Polish (Post-MVP)**
  - Implement full Spotify library integration & feature enrichment.
  - Develop and integrate the actual PyTorch ML model into `AdaptationEngine`.
  - Implement user feedback loop (Skips, Likes) for ML retraining.
  - Refine real-time SignalR updates for smoother playlist changes.
  - Set up CI/CD pipelines (**GitHub Actions** deploying **Docker** containers, potentially managed via **Docker Compose**).
  - Configure **Traefik** for ingress in deployed environment.
  - Set up **Terraform** for provisioning Azure resources (Database, Cache, Container Host, Blob Storage, Key Vault).
  - Comprehensive E2E testing, performance tuning, security hardening.
  - Documentation refinement.
  - (Post-MVP) Watch App development, YouTube Music (limited scope), Social Features, etc.

## Known Issues / Blockers / Risks

- **YouTube Music Integration:** High uncertainty. **Deferred** from MVP.
- **Health API Complexity:** Requires careful platform-specific implementation (iOS/Android differences).
- **Mobile Background Processing:** Critical technical challenge for reliability and battery life.
- **ML Model Data:** MVP uses rules; real ML needs data strategy (Phase 7+).
- **API Rate Limits:** Spotify API calls need careful management/caching.
- **Spotify SDK Limitations & Premium Requirement:** Core dependency risk.
- **Mobile Authentication with Identity:** Need to finalize strategy for MAUI app (WebAuthenticator + backend token exchange, or alternative secure method).
- **Solo Developer Bandwidth:** Ambitious scope requires strict adherence to phased plan and MVP focus.
