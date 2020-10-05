using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class NPCController : Singleton<NPCController>
{
    private bool haveBottle = false;

    public bool HaveBottle
    {
        get => haveBottle;
        set
        {
            haveBottle = value;
            if(value)
                StartCoroutine("DropBottle", Random.Range(3, 10));
        }
    }

    private void Start()
    {
        EventSystem.Instance.StopDelivery += MoveToTruck;
    }
    
    public void MoveToTruck(Vector3 point)
    {
        GetComponent<NavMeshAgent>().isStopped = true;
        GetComponent<WanderAI>().enabled = false;
        GetComponent<NavMeshAgent>().SetDestination(point);
        GetComponent<NavMeshAgent>().isStopped = false;
    }

    private void Update()
    {
    }

    

    IEnumerator DropBottle(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameManager.Instance.SpawnEmptyBottle(transform.position);
        haveBottle = false;
        yield return null;
    }
    
}