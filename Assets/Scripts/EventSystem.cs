using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : Singleton<EventSystem>
{
    public event Action<Transform> StrartDelivery;

    public virtual void OnStartDelivery(Transform obj)
    {
        StrartDelivery?.Invoke(obj);
    }
    
    public event Action<Vector3> StopDelivery;

    public virtual void OnStopDelivery(Vector3 obj)
    {
        StopDelivery?.Invoke(obj);
    }

    public event Action<Vector3> DropBottle;

    public virtual void OnDropBottle(Vector3 obj)
    {
        DropBottle?.Invoke(obj);
    }
    
    public event Action<GameObject> PickupBottle;

    public virtual void OnPickupBottle(GameObject obj)
    {
        PickupBottle?.Invoke(obj);
    }
    
    public event Action GiveBottle;

    public virtual void OnGiveBottle()
    {
        GiveBottle?.Invoke();
    }
}
