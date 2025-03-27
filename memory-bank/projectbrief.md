# PulsePlaylist - Project Brief

**Document Version:** 1.1
**Last Updated:** March 27, 2025
**Project Owner:** [Your Name / Solo Developer]
**Status:** Active Development (Phase Based)

## 1. Project Overview

PulsePlaylist is an AI-driven adaptive workout music service. Its core function is to **dynamically adjust a user's music playlist in real-time based on their measured workout intensity**. It achieves this by integrating with music streaming services (initially Spotify) and health tracking platforms (HealthKit for iOS, Health Connect for Android).

## 2. Vision Statement

To transform the workout experience by providing a seamless, personalized soundtrack that automatically adapts to the user's physical effort, enhancing motivation, performance, and enjoyment without manual intervention.

## 3. Core Problem Solved

Eliminates the disruption and sub-optimal experience of static workout playlists that don't match fluctuating workout intensity. Provides motivation through music precisely when needed during different phases of exercise (warm-up, peak effort, cool-down). Bridges the gap between fitness tracking data and music selection.

## 4. Target Audience

- **Primary:** Fitness enthusiasts who regularly use music during workouts (running, cycling, HIIT, gym) and track their performance using wearables or phones (Apple Watch, Garmin, Fitbit, etc.).
- **Secondary:** Casual exercisers looking for enhanced motivation; users of Spotify Premium.

## 5. Core Features (MVP Scope - Prioritized)

1.  **User Authentication:** Secure login via **Spotify OAuth**. Link Spotify account to an internal user profile managed by **ASP.NET Core Identity**.
2.  **Health Data Integration:** Connect to **HealthKit (iOS)** and **Health Connect (Android)** to retrieve **real-time heart rate** data during an active workout session.
3.  **Basic Workout Session Management (Mobile App):** Start, Pause, Stop workout sessions (initially focus on generic "Cardio", "Running", "Cycling").
4.  **Real-time Intensity Calculation:** Process incoming heart rate data to determine a current workout intensity level (e.g., score 0.0-1.0 or Low/Med/High zones).
5.  **Music Service Integration (Spotify First):** Use Spotify Mobile SDKs for iOS/Android within the MAUI app, and potentially Web Playback SDK for the Blazor Web App to:
    - Fetch user's liked songs and selected playlists.
    - Retrieve track **audio features** (BPM, Energy, Valence, etc.) via Spotify Web API.
    - Reliably **stream selected tracks** within the mobile app (requires Spotify Premium).
6.  **Core Adaptation Logic (Backend/ML Service):**
    - Receive current intensity level from mobile app.
    - Determine target audio features (initially rule- logic residing within the Backend API's Application Layer, architected for future ML service integration, e.g., "Need higher BPM/Energy").
    - Select the _next_ suitable track from the user's specified Spotify source (liked songs/playlist).
    - (ML model integration planned post-MVP).
7.  **Real-time Playlist Update:** Backend pushes the next track ID to the mobile app (via SignalR) for seamless playback transition.
8.  **Mobile App Interface (MAUI Blazor Hybrid):**
    - Display current workout metrics (HR, time).
    - Show currently playing track information.
    - Provide basic playback controls (Play/Pause, Skip).
    - Implement platform-specific wrappers for HealthKit/Connect access
    - Handle authentication and permissions flow.
    - Ensure reliable **background operation** for tracking and playback.
9.  **Basic Backend & Database (PostgreSQL):** Secure API endpoints for auth, workout data, track selection requests. Store user profiles (linked to Identity), securely stored Spotify tokens, basic workout summaries.
10. **Web App Interface (Blazor Web App):** Basic user profile view, workout history list (read-only initially), account settings management (e.g., music/health service connections, preferences).

## 6. Technical Architecture & Stack

- **Architecture:** Clean Architecture, Domain-Driven Design (DDD).
- **Backend:** .NET 9, ASP.NET Core Minimal APIs.
- **Authentication:** ASP.NET Core Identity integrated with PostgreSQL.
- **Database:** PostgreSQL with Entity Framework Core 9 (using `Npgsql.EntityFrameworkCore.PostgreSQL`).
- **Frontend (Mobile/Desktop):** .NET MAUI Blazor Hybrid (`PulsePlaylist.Mobile`).
- **Frontend (Web):** Blazor Web App (`PulsePlaylist.Web`) - Auto interactivity mode.
- **UI Components:** MudBlazor.
- **Shared Code:** Razor Class Library (`PulsePlaylist.Shared`).
- **ML Service:** Python 3.12+, FastAPI, PyTorch (`PulsePlaylist.AdaptationEngine` - simple rules for MVP).
- **Orchestration/DevOps:** .NET Aspire (`PulsePlaylist.AppHost`, `PulsePlaylist.ServiceDefaults`), Docker, Terraform, GitHub Actions.
- **Real-time Communication:** SignalR.
- **API Documentation:** OpenAPI specification generated via built-in ASP.NET Core capabilities
- **Mapping:** AutoMapper.
- **Validation:** FluentValidation.
- **Cloud Platform:** Azure (PostgreSQL, Cache for Redis, Key Vault). Docker + Docker Compose
- **Logging/Monitoring:** Serilog, OpenTelemetry, Grafana.
- **Testing:** xUnit, FluentAssertions, Moq, Testcontainers (PostgreSQL).

## 7. Scope Definition (MVP)

- **In Scope:** All features listed under Section 5. Focus on **Spotify Premium** users initially. Core adaptation based primarily on **Heart Rate**. Support for basic workout types (Run, Cycle, General Cardio). iOS and Android mobile apps + basic Web portal.
- **Out of Scope (Post-MVP / Future):**
  - **Full YouTube Music Integration** (due to API limitations).
  - Smart Watch Companion Apps (watchOS, Wear OS).
  - Advanced ML model training and feedback loops.
  - Social features (sharing, leaderboards).
  - Offline mode / downloaded music.
  - Advanced workout analytics and planning.
  - Integration with other music services (Apple Music, etc.).
  - Integration with fitness hardware (treadmills, bikes).
  - Support for non-Premium Spotify users (likely limited functionality).
  - Using metrics beyond heart rate (e.g., HRV, cadence sync, power).
  - Detailed Playlist Management (beyond selecting source for workout).

## 8. Success Metrics (MVP Goals)

- **Core Functionality:** Successfully adapt music in >80% of workout sessions based on HR changes.
- **User Engagement:** 500+ weekly active users completing at least one adaptive session within 3 months post-launch.
- **Retention:** 30% 30-day retention rate for registered users.
- **Stability:** <1% crash rate during active workout sessions; 99.5% API uptime.
- **User Satisfaction:** Average App Store/Play Store rating of 4.0+.

## 9. Key Constraints & Risks

- **API Dependency:** High reliance on Spotify API (rate limits, policy changes, Premium requirement) and HealthKit/Connect APIs.
- **YouTube Music Integration:** Significant technical/legal risk. De-prioritized for MVP.
- **Real-time Performance:** Latency in the data->intensity->ML->backend->music control loop must be low.
- **Background Execution Reliability:** Mobile OS background limitations can affect tracking/playback.
- **Battery Consumption:** Continuous sensor reading and processing must be optimized.
- **ML Accuracy (Future):** Effectiveness depends on model quality and training data.
- **Data Privacy:** Strict compliance required for health and music data.

## 10. Stakeholders (Solo Developer Roles)

- **Product Owner / Manager**
- **Lead Architect / Developer**
- **Backend Developer**
- **Mobile Developer**
- **Web Developer**
- **(Future) ML Engineer**
- **DevOps / Infrastructure**
- **QA Tester**

This document serves as the **single source of truth** for the PulsePlaylist MVP's scope, goals, and technical direction. Refer back to this brief to ensure development stays aligned with the core objectives.
