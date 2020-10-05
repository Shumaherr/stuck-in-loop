using System;
using System.Collections;
using System.Collections.Generic;
using PathCreation;
using UnityEngine;

public class TruckController : Singleton<TruckController>
{
    public PathCreator pathCreator;
    public EndOfPathInstruction end;
    public float speed;
    float dstTravelled;

    private Vector3[] points;

    public delegate void OnTruckStop(Vector3 point);

    public event OnTruckStop onTruckStop;
    // Start is called before the first frame update
    void Start()
    {
        end = EndOfPathInstruction.Stop;
        transform.rotation = Quaternion.Euler(0,0,0 );
    }
    void Update()
    {
        dstTravelled += speed * Time.deltaTime;
        transform.position  = pathCreator.path.GetPointAtDistance(dstTravelled, end);
        transform.rotation = pathCreator.path.GetRotationAtDistance(dstTravelled, end);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name is "StopTrig")
        {
            EventSystem.Instance.OnStopDelivery(new Vector3(46,0,24 ));
            other.GetComponent<TriggerController>()._activeForNPC = true;
        }
    }
    
    
}
