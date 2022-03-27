﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

public class ScoreManager : MonoBehaviour
{
    public ReactiveProperty<int> currentScore = new ReactiveProperty<int>();

    public ScoreEntity currentEntity { get; private set; }

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

        currentScore.Value = CalcScore();
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

    int CalcScore()
    {
        var result = ((currentEntity.NormalNum * BambooInfo.NormalScore) +
            (currentEntity.ShineNum * BambooInfo.ShinyScore)) * currentEntity.KaguyaNum * BambooInfo.KaguyaScoreMagnification;

        return result;
    }

    private void Start()
    {
        currentScore.Subscribe(score =>
        {
            currentEntity.Score = score;
        }).AddTo(this);
    }
}
