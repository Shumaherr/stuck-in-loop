using System.Collections;
using System.Collections.Generic;
using PathCreation;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] public Transform player;
    [SerializeField] public Transform truck;
    [SerializeField] public Transform emptyBottle;

    [SerializeField] public List<Transform> NPCs;
    [SerializeField] public int numNPCs;
    [SerializeField] public Transform wanderingZone;
    TextMeshProUGUI _bottlesCounter;

    public int countBottles;
    private int pickedBottles;
    public Transform truckInstance;
    private List<Transform> _spawnedNPCs;
    
    // Start is called before the first frame
    // update
    void Start()
    {
        _bottlesCounter = GameObject.Find("BottleCounter").GetComponent<TextMeshProUGUI>();
        InvokeRepeating("SpawnTruck", 0, 60);
        EventSystem.Instance.GiveBottle += GiveBottles;
        _spawnedNPCs = new List<Transform>();
        SpawnCroud();
    }

    private void GiveBottles()
    {
        pickedBottles = 0;
        _bottlesCounter.text = pickedBottles + " bottles picked up";
    }

    public void AddBottle()
    {
        pickedBottles++;
        _bottlesCounter.text = pickedBottles + " bottles picked up";
    }

    private void DriveBack()
    {
        truckInstance.GetComponent<TruckController>().end = EndOfPathInstruction.Reverse;
    }

    private void SpawnTruck()
    {
        if (truckInstance != null && !truckInstance.GetComponent<TruckController>().DriveAway)
            truckInstance.GetComponent<TruckController>().DriveAway = true;
       truckInstance = Instantiate(this.truck, new Vector3(5,0,29),Quaternion.identity);
        EventSystem.Instance.OnStartDelivery(truckInstance);
        GetComponent<Timer>().time = 60;
    }

    private void SpawnCroud()
    {
        for (int i = 1; i < numNPCs; i++)
        {
            
            _spawnedNPCs.Add(Instantiate(NPCs[Random.Range(0, NPCs.Capacity)], GetRandomLocation(),Quaternion.identity));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Vector3 GetRandomLocation()
    {
        NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();
 
        // Pick the first indice of a random triangle in the nav mesh
        int t = Random.Range(0, navMeshData.indices.Length-3);
         
        // Select a random point on it
        Vector3 point = Vector3.Lerp(navMeshData.vertices[navMeshData.indices[t]], navMeshData.vertices[navMeshData.indices[t+1]], Random.value);
        Vector3.Lerp(point, navMeshData.vertices[navMeshData.indices[t+2]], Random.value);
 
        return point;
    }

    public Transform SpawnEmptyBottle(Vector3 pos)
    {
        Vector3 tempPos = pos;
        tempPos.y = 1.18f;
        return Instantiate(emptyBottle, tempPos, Quaternion.Euler(0,45,0));
    }
    
}
