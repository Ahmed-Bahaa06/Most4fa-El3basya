# Syringe Dash Implementation Plan

Based on the Game Design Document and the Project Rules, here is the updated plan to implement the Syringe Dash mechanic.

## User Review Required
- **Input**: I will modify `Gameplay.inputactions` to rebind **Dash** to the `Space` key (replacing `Jump`, which is currently bound to Space).
- **Animations**: I propose using an `Animator` with 2D Blend Trees (passing `DirX` and `DirY` parameters) to handle the 4-directional animations cleanly, avoiding complex structs or hardcoded state names in the script.

## Proposed Changes

### Input System
- **Modify** `Gameplay.inputactions`: Reassign the Space key from the "Jump" action to the "Dash" action.

---

### Player Core & Animations
- **Modify** `PlayerMovement.cs`:
  - Fix the current 3D logic bug in `HandleDash` (`new Vector3(x, 0, y)`) which violates the 2D Z-axis rule.
  - Track `_facingDirection` (snapped to 4 main directions: Up, Down, Left, Right).
  - Add an `Animator` reference. Update the animator parameters (`DirX`, `DirY`) whenever `_facingDirection` changes so the Blend Tree always knows which way to face.
  - Update `HandleDash` to start a `DashRoutine` Coroutine:
    1. Lock player movement (`isDashing = true`) and trigger the Dash animation (`animator.SetTrigger("Dash")`).
    2. Apply high velocity in the `_facingDirection` for a short `dashDuration`.
    3. During the dash, perform a `Physics2D.OverlapBox` check to detect if we hit any NPC.
    4. **Hit**: If an NPC (Doctor or Guard) is hit, invoke `KnockOut()` on them and call `adrenalinSystem.IncreaseCurrentAdrenaline(...)`. Dash ends successfully.
    5. **Miss**: If the dash ends without hitting an NPC, the player enters a stunned recovery state (`isRecovering = true`) for a set `recoveryTime`, triggering a "Stunned" animation or effect.

---

### NPC State Machine
- **New File** `KnockedOutStateSO.cs`:
  - Create a new scriptable object state inheriting from `NPCStateSO`.
  - In `OnEnter`, this state will stop the AI agent's pathfinding (`Agent.isStopped = true`), play a knockout animation, and disable their colliders so they can no longer interact or be dashed into again.
- **Modify** `NPCStateMachine.cs`:
  - Add a public `KnockOut()` method to the base `NPCStateMachine` class instead of just the Doctor. This ensures **both Doctors and Security Guards** can be killed.
  - When called, this method will force a state transition to `KnockedOutStateSO`.

## Verification Plan

### Manual Verification
1. Play the game and walk around to ensure 4-directional facing is tracked and visually updated via the Animator.
2. Press Space to dash. 
3. **Miss Scenario**: Verify that missing causes the player to be immobilized for a short recovery period.
4. **Hit Scenario**: Verify that dashing into a Doctor or Security Guard knocks them out (they stop moving) and increases the Adrenaline bar, without causing the player to be stunned.
