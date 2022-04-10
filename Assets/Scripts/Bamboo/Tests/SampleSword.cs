using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleSword : MonoBehaviour
{
    int currentScore = 0;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<GenericBamboo>(out var bamboo))
        {
            currentScore = bamboo.CalcScore(currentScore);
            bamboo.AttackAction();

            Debug.Log(currentScore);
        }
    }
}
