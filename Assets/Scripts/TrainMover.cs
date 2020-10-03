using System.Collections;
using System.Collections.Generic;
using PathCreation;
using UnityEngine;

public class TrainMover : MonoBehaviour
{
    public PathCreator pathCreator;
    public EndOfPathInstruction end;
    public float speed;
    float dstTravelled;

    private Vector3[] points;
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
}
