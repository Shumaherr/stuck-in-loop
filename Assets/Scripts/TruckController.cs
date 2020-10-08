using System;
using System.Collections;
using System.Collections.Generic;
using PathCreation;
using UnityEngine;

public class TruckController : MonoBehaviour
{
    public PathCreator pathCreator;
    public EndOfPathInstruction end;
    public float speed;
    float dstTravelled;
    public int bottles;

    public int Bottles
    {
        get => bottles;
        set
        {
            bottles = value;
            if (bottles <= 0)
            {
                _driveAway = true;
                GetComponent<PathCreator>().enabled = false;
            }
        }
    }

    private Vector3[] points;
    private bool _driveAway;

    public bool DriveAway
    {
        get => _driveAway;
        set => _driveAway = value;
    }

    public delegate void OnTruckStop(Vector3 point);

    public event OnTruckStop onTruckStop;

    // Start is called before the first frame update
    void Start()
    {
        bottles = 20;
        end = EndOfPathInstruction.Stop;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    void Update()
    {
        if (_driveAway)
        {
            Vector3 newPos = transform.position;
            newPos.z += 2.0f * Time.deltaTime;
            if(newPos.z >= 65)
                Destroy(gameObject);
            transform.position = newPos;
            return;
        }

        dstTravelled += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(dstTravelled, end);
        transform.rotation = pathCreator.path.GetRotationAtDistance(dstTravelled, end);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name is "StopTrig")
        {
            EventSystem.Instance.OnStopDelivery(new Vector3(46, 0, 24));
            other.GetComponent<TriggerController>()._activeForNPC = true;
        }
    }
}