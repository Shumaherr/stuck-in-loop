using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : Singleton<EventSystem>
{
    public event Action<Transform> StrartDelivery;

    public virtual void OnStrartDelivery(Transform obj)
    {
        StrartDelivery?.Invoke(obj);
    }
    
    public event Action<Vector3> StopDelivery;

    public virtual void OnStopDelivery(Vector3 obj)
    {
        StopDelivery?.Invoke(obj);
    }

    public event Action<Vector3> DropBottle;

    protected virtual void OnDropBottle(Vector3 obj)
    {
        DropBottle?.Invoke(obj);
    }
}
