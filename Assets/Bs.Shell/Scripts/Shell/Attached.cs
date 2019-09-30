using UnityEngine;

public class Attached<T> : Component
        where T : Component
{
    T _component;
    T component
    {
        get
        {
            if (_component == null)
                _component = GetComponent<T>();
            return component;
        }
    }

    public T Value { get { return component; } }
}