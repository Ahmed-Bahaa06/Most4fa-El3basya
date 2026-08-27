using UnityEngine;
using UnityEngine.Events;

public abstract class GenericEventListener<T> : MonoBehaviour
{
    [Header("Event Configuration")]
    [SerializeField] private GenericEventChannelSO<T> _eventToListen;
    
    [Header("Actions")]
    [SerializeField] private UnityEvent<T> _onEvent;

    private void OnEnable()
    {
        if (_eventToListen != null)
        {
            _eventToListen.Register(this);
        }
    }
    
    private void OnDisable()
    {
        if (_eventToListen != null)
        {
            _eventToListen.Deregister(this);
        }
    }

    public void Listen(T value)
    {
        _onEvent?.Invoke(value);
    }
}
