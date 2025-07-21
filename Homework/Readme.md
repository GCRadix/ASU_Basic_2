# 🏠 Homework - Week 2: Expanding Proximity Audio
In this exercise, you’ll build upon the proximity sound logic from our live coding session. The goal is to apply the same principles to different gameplay contexts, improve structure, and introduce new behavior types.

---

## ✅ Part 1 - Adapt `ProximitySFX` to a New Use Case

Choose one of the following:

- **Turret Warning Beacon**
  Use the proximity retriggering logic to create a repeating sound cue when the player enters a defensive turret’s radius.

- **Environmental Object**
  Place a sound-emitting object (e.g., beeping console, magic rune, sensor node) that signals when the player is near.

**What to do:**

- Use the `ProximitySFX` script or a cleaned-up version of it.
- Add a public `GameObject target` field to support proximity to non-player objects.
- Tweak retrigger rate and distance values to fit your use case.

---

## 🔁 Part 2 - Extend It: Area Entry & Exit Sounds

Expand your logic to add **entry and exit audio cues**:

- When the player **enters** the radius, play a short one-shot sound (e.g., alert ping, activation).
- When the player **exits**, play a different sound (e.g., fade-out cue, deactivation).

**You’ll need:**

- A state-tracking variable (e.g., `bool playerInsideRange`)
- `AudioSource.PlayOneShot()` for the one-shot sounds
- Optional: a small cooldown or hysteresis to avoid spamming entry/exit if the player moves across the boundary repeatedly


---

## ⚙️ Optional Bonus – Smooth Parameterization

Instead of retriggering sounds directly, use:

- `AudioSource.volume`, or
- a filter (e.g., lowpass cutoff)

…to smoothly modulate sound intensity based on distance.

---

## 📦 Submission

- Submit your Script and a short screen recording showing your system in action.
- Your script should be cleanly structured, commented, and named.
