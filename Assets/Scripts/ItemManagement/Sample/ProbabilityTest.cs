using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProbabilityTest : MonoBehaviour
{
    [SerializeField]
    ItemManager item;

    private void Awake()
    {
        int i = 0;
        float atari = 0;
        float max = 1000000;

        while (i < max)
        {
            if (item.PickUp() != -1)
            {
                atari++;
            }
            i++;
        }

        Debug.Log($"あたりの数は [{atari}]。確率は {atari / max * 100} %");
    }
}
