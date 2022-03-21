using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugeCollision : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6)
        {
            Debug.Log(other.gameObject.name);
        }
    }
}
