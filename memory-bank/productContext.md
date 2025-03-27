# PulsePlaylist - Product Context

**Document Version:** 1.1
**Last Updated:** March 27, 2025
**Status:** Active Development (Phase Based)

## 1. Why This Project Exists (The Vision)

PulsePlaylist exists to **transform the workout experience by making music truly adaptive and responsive to the user's body**. The vision is to create a seamless connection between a person's physical effort during exercise and the soundtrack accompanying it, eliminating manual music management and enhancing motivation, performance, and enjoyment. We believe music is a powerful workout tool, but static playlists fail to match the dynamic nature of exercise. PulsePlaylist aims to be the "AI DJ" that instinctively knows the right beat for every moment of a workout.

## 2. Problems It Solves

- **Static Workout Music:** Pre-made playlists or albums often don't match the varying intensity levels within a single workout (e.g., warm-up, high-intensity intervals, cool-down).
- **Workout Disruption:** Users manually skipping tracks, searching for different songs, or adjusting volume during exercise breaks focus and flow.
- **Suboptimal Motivation/Performance:** Music that doesn't match the energy level can either fail to motivate during tough segments or be jarring during recovery periods, potentially hindering performance and enjoyment.
- **Lack of Personalization:** Generic workout playlists don't cater to individual music tastes _and_ real-time physiological responses.
- **Data Silos:** Fitness tracking data (heart rate, pace) and music listening data/preferences exist separately, offering no integrated, responsive experience.

## 3. How It Should Work (Core Functionality - MVP Focus)

1.  **Connect Services:** User authenticates via **Spotify OAuth** and grants permission to access their mobile device's health platform (**HealthKit** on iOS, **Health Connect** on Android) during a guided onboarding flow.
2.  **Select Music Source:** Before starting a workout, the user selects a source for the music (**MVP: A specific Spotify playlist or their 'Liked Songs' library**). _Initial state/new users default to 'Liked Songs' if available, otherwise prompt selection._
3.  **Start Workout:** User initiates a workout session (MVP: Run, Cycle, General Cardio) in the PulsePlaylist mobile app.
4.  **Real-time Monitoring:** App continuously reads **Heart Rate** data from the connected health platform reliably in the background.
5.  **Intensity Calculation:** App or backend processes HR data (e.g., % of Max HR) to determine the current workout intensity level (e.g., 0.0-1.0 or Low/Med/High).
6.  **Rule-Based Feature Prediction (MVP):** Current intensity level is sent to the backend, which uses **pre-defined rules** to determine the _ideal_ audio features (target BPM, Energy) for the _next_ song. _(ML prediction planned post-MVP)_.
7.  **Track Selection:** Backend logic selects a suitable track from the user's chosen Spotify source that best matches the target audio features, potentially prioritizing liked tracks and avoiding immediate repeats.
8.  **Dynamic Playlist Update:** Backend sends the Spotify Track ID of the selected next track to the mobile app via **SignalR**.
9.  **Seamless Playback:** Mobile app uses the **Spotify Mobile SDK** to queue and play the next track, aiming for a smooth transition before the current song ends. Requires **Spotify Premium**.
10. **Continuous Loop:** Steps 4-9 repeat throughout the workout session.
11. **Workout Completion:** User ends the session. A basic summary (duration, avg/max HR) is saved.
12. **Feedback (Basic MVP):** Allow user to **skip** tracks during workout (implicit negative feedback). _(Explicit like/dislike planned post-MVP)_.

## 4. User Experience Goals (MVP Focus)

- **Seamless & Automatic:** Core adaptation should work reliably in the background with minimal required user interaction during the workout.
- **Responsive & Timely:** Music changes should feel connected to significant shifts in heart rate, ideally transitioning at the next natural song break.
- **Motivating:** Music selections should generally support the perceived workout intensity (higher energy for higher HR).
- **Personalized (MVP Level):** Music primarily drawn from user's chosen Spotify source (playlist/liked songs). _(Deeper personalization via learning planned post-MVP)_.
- **Reliable:** App functions consistently during workouts (**MVP behavior on network loss: playback may pause or stop adaptation; requires reconnect**). Minimize crashes and battery drain.
- **Intuitive Control:** Simple, easily accessible controls for Start/Stop workout and Skip Track.
- **Transparency & Trust:** Clear permission requests for Health data and Spotify access. Secure handling of data.
- **Discoverable Value:** User perceives the benefit of music changing with their effort compared to a static playlist. Visual feedback (current HR, maybe intensity zone) helps.
- **Manage Cold Start:** Acknowledge that the very first sessions might feel less personalized until basic preferences or history are established (though adaptation based on HR works immediately).

## 5. Error Handling Considerations (User Facing - MVP)

- **Spotify Connection Failure:** Inform user connection lost, attempt reconnect. Playback likely stops or pauses adaptation.
- **Health Data Interruption:** Inform user, attempt reconnect. Adaptation pauses or relies on last known good data temporarily.
- **Network Loss:** Inform user. SignalR connection drops, adaptation stops. Playback might continue with current track or pause depending on Spotify SDK behavior. App attempts reconnect when network returns.
