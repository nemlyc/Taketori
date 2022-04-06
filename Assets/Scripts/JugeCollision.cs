using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugeCollision : MonoBehaviour
{
    [SerializeField]
    PlayerController player;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6)
        {
            if (other.gameObject.TryGetComponent<GenericBamboo>(out var genericBamboo))
            {
                other.enabled = false;
                player.AttackBambooLogic(genericBamboo);
                //Debug.Log(other.gameObject.name);
            };
        }
    }
}
