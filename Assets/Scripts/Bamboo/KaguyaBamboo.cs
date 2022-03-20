using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KaguyaBamboo : GenericBamboo
{
    public override void AttackAction()
    {
        Debug.Log("Found Kaguya !");
    }

    public override int CalcScore(int current)
    {
        return current * BambooInfo.KaguyaScoreMagnification;
    }

    public override BambooInfo.BambooType GetBambooType()
    {
        return BambooInfo.BambooType.Kaguya;
    }
}
