# Sound Manager Architecture

This plan details the implementation of a centralized, persistent `SoundManager` using a classic Singleton pattern, as requested by the user. It will handle categorization, random selection, and playback of audio clips using a simple object pool to avoid per-frame allocations.

## Proposed Changes

---

### Audio System

#### [NEW] [SoundType.cs](file:///d:/Unity%20Projects/Most4fa-El3basya/Assets/Scripts/Audio/SoundType.cs)
An Enum defining the different categories of sounds requested: `PlayerAction`, `PlayerKnockdown`, `GuardMeleeSpot`, `GuardRangedSpot`, `DoctorSpot`, `FemaleDoctorSpot`, `UI`, `VFX`.

#### [NEW] [SoundGroup.cs](file:///d:/Unity%20Projects/Most4fa-El3basya/Assets/Scripts/Audio/SoundGroup.cs)
A serializable struct to hold an array of `AudioClip`s, along with `Volume` and `PitchRange` parameters for random variation.

#### [NEW] [SoundManager.cs](file:///d:/Unity%20Projects/Most4fa-El3basya/Assets/Scripts/Audio/SoundManager.cs)
- Implemented as a Singleton (`public static SoundManager Instance`).
- Marked with `DontDestroyOnLoad` to persist across all scenes.
- Exposes `[SerializeField]` SoundGroups for each type of sound requested.
- Implements a simple pre-warmed `AudioSource` Object Pool (`List<AudioSource>`).
- Exposes a public method `public void PlaySound(SoundType type, Vector3 position)` which NPCs and UI will call directly.
