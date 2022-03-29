using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericBamboo : MonoBehaviour, IBamboo
{
    public virtual void AttackAction()
    {
        /*
         * -> Split Bamboo
         * -> Animation
         * -> Destroy
         */
        //Debug.Log($"Hit [{gameObject.name.Substring(12)}]");
        Debug.Log($"Hit [{gameObject.name}]");
    }

    public abstract int CalcScore(int current);

    public abstract BambooInfo.BambooType GetBambooType();
}
