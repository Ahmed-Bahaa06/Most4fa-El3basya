using System.Collections.Generic;
using UnityEngine;

public abstract class GenericEventChannelSO<T> : ScriptableObject
{
    [Header("Testing/Debugging")]
    [SerializeField] private T _testingValue;

    private readonly HashSet<GenericEventListener<T>> _listeners = new HashSet<GenericEventListener<T>>();

    public void Register(GenericEventListener<T> listener)
    {
        _listeners.Add(listener); // HashSet automatically handles duplicates
    }

    public void Deregister(GenericEventListener<T> listener)
    {
        _listeners.Remove(listener);
    }

    public void Invoke(T value)
    {
        foreach(GenericEventListener<T> listener in _listeners)
        {
            listener.Listen(value);
        }
    }
}
