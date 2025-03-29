# PulsePlaylist - Active Context

**Last Updated:** March 30, 2025

## Current Work Focus

- **Phase 2 Implementation:** Starting the Data Access Layer implementation with PostgreSQL and EF Core 9.
- **Infrastructure Focus:** Setting up database connections, migrations, and repository pattern implementations.
- **Test Strategy:** Expanding test coverage to include integration tests with TestContainers.

## Recent Changes

- **Phase 1 Completed Successfully:**
  - All domain entities implemented and fully tested (92 passing tests)
  - Clean Architecture principles validated through implementation
  - Business rules enforced via comprehensive test coverage
  - Domain events and value objects properly implemented
  - JSON serialization/deserialization working for complex types
  - Floating-point precision handling verified

- **Project Structure Validated:**
  - Domain layer isolation maintained
  - Entity relationships defined
  - Value objects properly encapsulated
  - Test patterns established

## Next Steps

1.  **Begin Phase 2 Implementation:**
    - Set up ApplicationDbContext with proper configuration
    - Design repository interfaces in Application layer
    - Implement repositories in Infrastructure layer
    - Configure PostgreSQL connection and migrations
    - Set up TestContainers for integration testing

2.  **Database Configuration:**
    - Configure provider-specific migrations in Migrators project
    - Set up Aspire configuration for PostgreSQL
    - Implement database seeding strategy
    - Define proper indexing strategy

3.  **Identity Integration:**
    - Verify ASP.NET Core Identity schema compatibility
    - Ensure proper transaction handling across Identity and domain operations
    - Set up proper repository patterns for user-related operations

## Active Decisions and Considerations

- **Database Strategy:** Using SQLite for development/testing but ensuring PostgreSQL compatibility through provider-specific migrations
- **Repository Pattern:** How to structure repositories to maintain clean architecture while providing efficient querying capabilities
- **Integration Testing:** Planning TestContainers setup for reliable database testing
- **Identity Integration:** How to properly manage user-related operations while maintaining domain isolation
- **Migration Strategy:** How to handle schema evolution and maintain data integrity
- **Transaction Management:** Ensuring proper handling of operations that span multiple aggregates
- **YouTube Music Integration Strategy:** Decision pending based on assessment risks. Likely deferring full integration for MVP, focusing solely on Spotify initially. Need to confirm limited scope (e.g., playlist import only?) or full deferral.
- **Initial ML Model Approach:** Confirming the MVP will use simple rule-based logic within the backend service first, deferring the actual PyTorch model implementation until Phase 4. Architect the backend service to easily swap implementations later.
- **Specific Health Metrics for MVP:** Confirming that **real-time Heart Rate** is the primary metric for MVP intensity calculation. Pace/Cadence integration to be considered post-MVP or if easily achievable alongside HR.
- **Background Processing (Mobile):** Need to research and plan the specific implementation strategy for reliable background execution of health data collection, API communication (SignalR), and music playback using MAUI Blazor Hybrid.
- **Transaction Management:** How to ensure atomicity for operations that might involve updates to both ASP.NET Identity tables and domain entity tables within the same user request (e.g., during registration linking).
- **Initial Onboarding UX:** Design the flow for requesting permissions (Health, Music, Notifications) smoothly during the first app launch.
- **Secrets Management:** Plan for secure handling of API keys (Spotify, Google) and database connection strings, likely using .NET user secrets locally and Azure Key Vault in production, integrated via Aspire configuration.
