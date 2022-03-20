using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBamboo : GenericBamboo
{
    public override int CalcScore(int current)
    {
        return current + BambooInfo.NormalScore;
    }

    public override BambooInfo.BambooType GetBambooType()
    {
        return BambooInfo.BambooType.Normal;
    }
}
