using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameViewUISimulation : MonoBehaviour
{
    [SerializeField]
    Timer timer;
    [SerializeField]
    ScoreManager score;
    [SerializeField, Tooltip("秒数で指定")]
    int TimeLimit = 10;

    private void Start()
    {
        timer.StartTimer(TimeLimit);
    }
}
