# NPC State Machine Architecture

## Overview
The NPC AI uses a **Stateless Flyweight ScriptableObject State Machine**.
This allows hundreds of enemies to share the exact same `StateSO` assets in memory without conflicts. All instance-specific data (like timers and current targets) is stored on the `NPCStateMachine` context.

## Class Diagram
```mermaid
classDiagram
    class NPCStateMachine {
        +AIPath Agent
        +Transform Target
        +NPCStateSO CurrentState
        +float StateTimer
        +float ActionTimer
        +ChangeState(NPCStateSO)
    }
    
    class DoctorStateMachine {
        -NPCStateSO _initialState
    }
    
    class SecurityStateMachine {
        -NPCStateSO _initialState
    }
    
    class NPCStateSO {
        <<ScriptableObject>>
        +OnEnter(NPCStateMachine)
        +OnUpdate(NPCStateMachine)
        +OnExit(NPCStateMachine)
    }
    
    class IdleStateSO
    class PatrolStateSO
    class RunningStateSO
    class ShootingStateSO
    class HidingStateSO
    
    NPCStateMachine --|> MonoBehaviour
    DoctorStateMachine --|> NPCStateMachine
    SecurityStateMachine --|> NPCStateMachine
    
    NPCStateSO --|> ScriptableObject
    IdleStateSO --|> NPCStateSO
    PatrolStateSO --|> NPCStateSO
    RunningStateSO --|> NPCStateSO
    ShootingStateSO --|> NPCStateSO
    HidingStateSO --|> NPCStateSO
    
    NPCStateMachine --> NPCStateSO : CurrentState
```

## State Transitions (Node-Based)
Transitions are configured in the Unity Inspector by dragging SO assets into public fields (e.g., `stateWhenFinished`).

### Doctor
```mermaid
stateDiagram-v2
    Doctor_Idle --> Doctor_Patrol : Timer Ends
    Doctor_Patrol --> Doctor_Idle : Timer Ends
    
    Doctor_Idle --> Doctor_Running : Player Spotted
    Doctor_Patrol --> Doctor_Running : Player Spotted
    
    Doctor_Running --> Doctor_Idle : Player Lost
```

### Security Guard
```mermaid
stateDiagram-v2
    Guard_Idle --> Guard_Patrol : Timer Ends
    Guard_Patrol --> Guard_Idle : Timer Ends
    
    Guard_Idle --> Guard_Shooting : Player Spotted
    Guard_Patrol --> Guard_Shooting : Player Spotted
    
    Guard_Shooting --> Guard_Patrol : Player Lost
```
