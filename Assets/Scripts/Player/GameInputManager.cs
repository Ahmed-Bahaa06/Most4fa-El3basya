using UnityEngine;
using System;
[DefaultExecutionOrder(-1)]

public class GameInputManager : MonoBehaviour
{
    public static GameInputManager Instance { get; private set; }

    Gameplay inputActions;
    public event Action OnInteract;
    public event Action OnDrop;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        inputActions = new Gameplay();
       
    }

    void OnEnable()
    {
        inputActions.Enable();
        inputActions.Player.Interact.performed += Interact_performed;
        //inputActions.Player.Drop.performed += Drop_performed;
    }

    void OnDisable()
    {
        inputActions.Player.Interact.performed -= Interact_performed;
        //inputActions.Player.Drop.performed -= Drop_performed;
        inputActions.Disable();
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnInteract?.Invoke();
        }
    }

    private void Drop_performed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnDrop?.Invoke();
        }
    }

    public Vector2 GetMovement()
    {
        return inputActions.Player.Move.ReadValue<Vector2>().normalized;
    }

}
