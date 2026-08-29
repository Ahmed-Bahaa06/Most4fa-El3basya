# Implementation Plan: AI Combat Systems (Projectiles & Melee)

This plan outlines the addition of projectile combat for the Security Guard's `ShootingStateSO` and a new `SecurityChaseStateSO` for melee combat, strictly adhering to the project's performance (zero allocation) and architectural rules.

## Proposed Changes

### Core Weapons System

#### [NEW] `Assets/Scripts/Combat/Projectile.cs`
- Handles straight-line movement via `Update()`.
- Uses `OnTriggerEnter2D` to detect collisions.
- If it hits an `IDamagable`, it calls `TakeDamage()`.
- Returns itself to an `ObjectPool` upon collision or after a lifetime expires.

#### [NEW] `Assets/Scripts/Combat/ProjectileWeapon.cs`
- Attached to the NPC GameObject.
- Initializes and manages the `UnityEngine.Pool.ObjectPool<Projectile>`.
- Exposes a `Fire(Vector2 direction)` method.

### AI State Machine Updates

#### [MODIFY] `Assets/Scripts/Core/FSM/NPC/States/ShootingStateSO.cs`
- When `ActionTimer` hits `fireRate`, it calculates the direction to the player.
- Retrieves the `ProjectileWeapon` component from the `NPCStateMachine` context and calls `Fire()`.

#### [NEW] `Assets/Scripts/Core/FSM/NPC/States/SecurityChaseStateSO.cs`
- New SO State inheriting from `NPCStateSO`.
- **Movement**: Uses `AIPath` to continuously path towards the player (`Target.position`).
- **Melee Attack**: Checks distance. If `distance <= meleeAttackRange` and `ActionTimer >= meleeAttackRate`, it finds the `IDamagable` on the Target and applies damage.
