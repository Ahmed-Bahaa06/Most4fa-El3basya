# Agent Context Handover

## Project State Summary
We are building a 2D Top-Down Survival Horror game (similar to The Escapists).
We have laid the architectural foundations for the core systems, prioritizing modularity, ScriptableObject event channels, and strict adherence to the project rules (zero per-frame allocations, encapsulation).

## What Has Been Completed
1. **Event Channel Architecture**: 
   - Generic `EventChannelSO<T>` and `EventListener<T>` using HashSets to avoid GC.
   - Created `Void` and `Float` variants.
   - Added custom Inspector Editor scripts to invoke events directly from the Unity Editor via Reflection.
2. **GameManager FSM**:
   - Replaced the enum-based GameManager with a ScriptableObject FSM (`GameManagerStateMachine`).
3. **NPC AI (A* Pathfinding)**:
   - Built a Stateless Flyweight SO State Machine for the AI (`NPCStateMachine`).
   - Integrated with Aron Granberg's A* Pathfinding Project (`AIPath` components).
   - Created shared `IdleStateSO` and `PatrolStateSO`.
   - Created specific `RunningStateSO` (Doctor fleeing), `ShootingStateSO` (Guard attacking), and `SecurityChaseStateSO` (Guard melee chase).
   - Created Object Pool-based combat system (`Projectile.cs`, `ProjectileWeapon.cs`) and integrated it into the `ShootingStateSO`.
   - Added a universal `KnockedOutStateSO` that stops the agent, turns them black, and disables colliders. The base `NPCStateMachine` now exposes a `KnockOut()` method.
   - Ensured all calculations use `Vector2` to prevent 2D Z-axis bugs.
4. **UI Setup**:
   - Created `HUDManager`, `TimerUI`, and `HealthBarUI`.
   - Connected them to the SO Event Channels. 
   - Optimized `TimerSystem` to only broadcast when the visual second changes to prevent string allocation GC spikes.
5. **Player Combat (Syringe Dash)**:
   - Separated movement logic by creating `PlayerDash.cs`.
   - Input mapped to Space bar.
   - Implemented 4-directional tracking passing `DirX` and `DirY` to an Animator Blend Tree.
   - Used zero per-frame allocation `Physics2D.OverlapBoxAll` for detecting hits on NPCs to knock them out and reward adrenaline. Misses result in a recovery state.
6. **Scene Management Architecture**:
   - Implemented ScriptableObject-based Scene Management (`GameSceneSO`) to eliminate brittle `buildIndex` usage and merge conflicts.
   - Enhanced `GenericEventChannelSO<T>` with a pure C# event (`OnEventRaised`) to allow manager scripts to subscribe natively.
   - Created `LoadEventChannelSO` to trigger scene loads.
   - Created `SceneLoader` and `Bootstrapper` managers to handle persistent background loading and the initial flow from Bootstrapper -> MainMenu -> Gameplay.
   - Updated `MainMenuUI` to decouple it from `SceneManager` and instead trigger the `LoadEventChannelSO`.

## What Needs To Be Done Next
1. **AI Polish**:
   - Implement actual waypoint logic for the `PatrolStateSO`.
   - Implement line-of-sight (Raycasting) instead of simple distance checks for spotting the player.
2. **Level Mechanics**:
   - Interactables, stealth mechanics (Hiding in lockers for the HidingStateSO).
   - Trap interactions (Electric wires on doorways, Overcharge mechanics).
