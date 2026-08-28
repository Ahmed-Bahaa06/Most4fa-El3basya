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
   - Created specific `RunningStateSO` (Doctor fleeing) and `ShootingStateSO` (Guard attacking).
   - Ensured all calculations use `Vector2` to prevent 2D Z-axis bugs.
4. **UI Setup**:
   - Created `HUDManager`, `TimerUI`, and `HealthBarUI`.
   - Connected them to the SO Event Channels. 
   - Optimized `TimerSystem` to only broadcast when the visual second changes to prevent string allocation GC spikes.

## What Needs To Be Done Next
1. **Player Combat (Syringe Dash)**: 
   - Implement the dash mechanic using the event channels and the `AdrenalineSystem`.
2. **AI Polish**:
   - Implement actual waypoint logic for the `PatrolStateSO`.
   - Implement line-of-sight (Raycasting) instead of simple distance checks for spotting the player.
3. **Level Mechanics**:
   - Interactables, stealth mechanics (Hiding in lockers for the HidingStateSO).
