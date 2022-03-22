using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BambooGenerationSampleSetup : MonoBehaviour
{
    [SerializeField]
    BambooGenerator gen;

    private void Start()
    {
        gen.PlacementBamboo();
    }
}
