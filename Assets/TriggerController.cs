using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerController : MonoBehaviour
{
    public bool _activeForNPC;
    // Start is called before the first frame update
    void Start()
    {
        _activeForNPC = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag is "NPC")
        {
            other.GetComponent<WanderAI>().enabled = true;
            if (!other.GetComponent<NPCController>().HaveBottle)
            {
                other.GetComponent<NPCController>().HaveBottle = true;
            }
        }
    }
}
