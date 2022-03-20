using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScoreManager : MonoBehaviour
{
    public int currentScore { get; set; }

    ScoreEntity currentEntity;

    public void AddBamboo(BambooInfo.BambooType type)
    {
        if (currentEntity == null)
        {
            currentEntity = new ScoreEntity();
        }

        switch (type)
        {
            case BambooInfo.BambooType.Normal:
                currentEntity.NormalNum += 1;
                break;

            case BambooInfo.BambooType.Shine:
                currentEntity.ShineNum += 1;
                break;

            case BambooInfo.BambooType.Kaguya:
                currentEntity.KaguyaNum += 1;
                break;
        }
    }
    public void AddItem(ItemEntity item)
    {
        if (currentEntity == null)
        {
            currentEntity = new ScoreEntity();
        }

        currentEntity.itemEntities.Add(item);
    }

    public ScoreEntity FetchResult(bool clearTempScore = true)
    {
        var result = currentEntity;
        if (clearTempScore)
        {
            currentEntity = null;
        }

        return result;
    }

    public ScoreEntity LoadLastHighScore(out string status)
    {
        status = "記録なし";
        return new ScoreEntity();
    }

    public bool WriteItems(out string status)
    {
        status = "未実装";
        return true;
    }

    public bool WriteHighScore(out string status)
    {
        status = "未実装";
        return true;
    }    
}
