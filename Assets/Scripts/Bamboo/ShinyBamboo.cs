using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShinyBamboo : GenericBamboo
{
    public override int CalcScore(int current)
    {
        return current + BambooInfo.ShinyScore;
    }

    public override BambooInfo.BambooType GetBambooType()
    {
        return BambooInfo.BambooType.Shine;
    }
}
