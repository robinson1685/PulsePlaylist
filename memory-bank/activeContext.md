# PulsePlaylist - Active Context

**Last Updated:** March 14, 2025

## Current Work Focus

- **Phase 1 Implementation:** Starting the actual coding for **Phase 1: Core Domain Model Setup**.
- **Test-Driven Development (TDD):** Implementing domain entities (`Track`, `AudioFeatures`), value objects (`WorkoutIntensity`, `PlaylistSettings`), and associated unit tests (`PulsePlaylist.Domain.Tests`) using xUnit and FluentAssertions, following the detailed prompt generated previously.
- **Establishing Core Business Logic:** Ensuring the initial domain models correctly capture the fundamental concepts of the applicationUser , tracks, features, and workout settings.

## Recent Changes

- **Project Structure Finalized:** All .NET projects (`.API`, `.Application`, `.Domain`, `.Infrastructure`, `.WebApp`, `.Mobile`, `.AppHost`, `.ServiceDefaults`, `Migrators`) are now organized under the `src/` directory. The Python ML project (`PulsePlaylist.AdaptationEngine`) also in /src.
- **Technology Stack Confirmed:** Solidified choices include .NET 9, C# 12, PostgreSQL with EF Core 9, ASP.NET Core Identity, Minimal APIs, MAUI Blazor Hybrid (for `.Mobile`), Blazor Web App (for `.Web`), MudBlazor, .NET Aspire, Python/FastAPI/PyTorch (for ML), Microsoft.OpenApi, AutoMapper, FluentValidation, TestContainers (for testing), Serilog, OpenTelemetry.
- **Build Plan Defined:** Established a detailed, phased iterative build plan prioritizing the mobile app MVP core loop (Mobile -> Backend -> Simple Adaptation -> Music Playback).
- **Foundational Documents Created:** `projectbrief.md` generated to define scope and goals. An expert assessment analysis was reviewed, confirming feasibility but highlighting risks (esp. YouTube Music).
- **Detailed Phase Prompts Generated:** Created specific, context-rich prompts for guiding the implementation of Phase 1 (Domain), Phase 2 (Data Access with Postgres/Aspire/Identity), and Phase 3 (API with Identity/Microsoft.OpenApi/AutoMapper).
- **`.gitignore` Corrected:** Resolved issues with `bin`/`obj` folders not being ignored.

## Next Steps

1.  **Complete Phase 1 Implementation:** Finish writing tests and implementing the core domain entities and value objects as defined in the prompt. Ensure all tests pass.
2.  **Begin Phase 2 Implementation (Data Access):** Start TDD for Phase 2, focusing on:
    - Migrate my migrations from Sqlite to Postgres
    - Verifying and setting up `ApplicationDbContext` is configured for PostgreSQL.
    - Implementing repository interfaces for Phase 1 entities.
    - Creating EF Core migrations for both domain and Identity schemas within the `PulsePlaylist.Infrastructure` project.
    - Configuring PostgreSQL resource and connection strings within the `.NET Aspire` (`PulsePlaylist.AppHost`) setup.
    - Setting up TestContainers for PostgreSQL integration testing (`PulsePlaylist.Infrastructure.Tests`).
3.  **Prepare for Phase 3 (API):** Review the generated prompt for API implementation, ensuring understanding of Identity integration, AutoMapper setup, and Microsoft.OpenApi configuration.

## Active Decisions and Considerations

- **YouTube Music Integration Strategy:** Decision pending based on assessment risks. Likely deferring full integration for MVP, focusing solely on Spotify initially. Need to confirm limited scope (e.g., playlist import only?) or full deferral.
- **Initial ML Model Approach:** Confirming the MVP will use simple rule-based logic within the backend service first, deferring the actual PyTorch model implementation until Phase 4. Architect the backend service to easily swap implementations later.
- **Specific Health Metrics for MVP:** Confirming that **real-time Heart Rate** is the primary metric for MVP intensity calculation. Pace/Cadence integration to be considered post-MVP or if easily achievable alongside HR.
- **Background Processing (Mobile):** Need to research and plan the specific implementation strategy for reliable background execution of health data collection, API communication (SignalR), and music playback using MAUI Blazor Hybrid.
- **Transaction Management:** How to ensure atomicity for operations that might involve updates to both ASP.NET Identity tables and domain entity tables within the same user request (e.g., during registration linking).
- **Initial Onboarding UX:** Design the flow for requesting permissions (Health, Music, Notifications) smoothly during the first app launch.
- **Secrets Management:** Plan for secure handling of API keys (Spotify, Google) and database connection strings, likely using .NET user secrets locally and Azure Key Vault in production, integrated via Aspire configuration.
