using System;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]

public class GameInputManager : MonoBehaviour
{
    public static GameInputManager Instance { get; private set; }

    Gameplay inputActions;

    public event Action<Vector2> OnMove;

    public event Action OnInteract;
    public event Action OnDrop;
    public event Action OnDash;
    public event Action OnPause;

    private void Awake()
    {
        if (Instance == null) Instance = this;

        inputActions = new Gameplay();
    
    }

    void OnEnable()
    {
        inputActions.Enable();

        inputActions.Player.Move.performed += Move_performed;
        inputActions.Player.Interact.performed += Interact_performed;
        inputActions.Player.Dash.performed += Dash_performed;
        inputActions.Player.Pause.performed += Pause_performed;

        //inputActions.Player.Drop.performed += Drop_performed;
    }

    private void Move_performed(InputAction.CallbackContext obj)
    {
        if (obj.performed) OnMove?.Invoke(obj.ReadValue<Vector2>().normalized);
    }

    private void Interact_performed(InputAction.CallbackContext context)
    {
        if (context.performed) OnInteract?.Invoke();
    }

    private void Drop_performed(InputAction.CallbackContext context)
    {
        if (context.performed) OnDrop?.Invoke();
    }

    private void Dash_performed(InputAction.CallbackContext obj)
    {
        if (obj.performed) OnDash?.Invoke();
    }

    private void Pause_performed(InputAction.CallbackContext obj)
    {
        if (obj.performed) OnPause?.Invoke();
    }

    void OnDisable()
    {
        inputActions.Player.Move.performed -= Move_performed;
        inputActions.Player.Interact.performed -= Interact_performed;
        inputActions.Player.Dash.performed -= Dash_performed;
        inputActions.Player.Pause.performed -= Pause_performed;

        //inputActions.Player.Drop.performed -= Drop_performed;

        inputActions.Disable();
    }
}
