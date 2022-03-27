﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class InGameViewPresenter : MonoBehaviour
{
    /*
     * Todo : Age
     * 
     * 購読する
     *  - Timer
     *  - Age
     *  - Score
     * 
     * UIを書き換える
     *  - Timer Value
     *  - Age Value
     *  - Score Value
     */

    [SerializeField]
    ScoreManager score;
    [SerializeField]
    Timer timer;
    [SerializeField]
    PlayerController player;
    [SerializeField]
    InGameView gameView;
    [SerializeField]
    BambooGenerator bambooGenerator;
    [SerializeField]
    float readyTime = 4, ingameTime = 300;

    [SerializeField]
    ResultViewPresenter resultViewPresenter;

    [SerializeField]
    CollectionItem[] testItem;

    private void Start()
    {
        GameInitialize();
        
        #region ゲームが始まるまでをカウントダウンするObservable
        Timer localTimer = gameObject.AddComponent<Timer>();
        localTimer.StartTimer(readyTime);
        localTimer.currentTime.Subscribe(time =>
        {
            if (time != null)
            {
                gameView.UpdateReadyTimeValue(time.Substring(3, 1));
            }
        }).AddTo(this);
        localTimer.IsTimeUp.Subscribe(timeUp =>
        {
            if (timeUp)
            {
                gameView.ToggleReadyTime(false);
                timer.StartTimer(ingameTime);
                Destroy(localTimer);
            }
            else
            {
                gameView.ToggleReadyTime(true);
            }
        }).AddTo(this);
        #endregion

        timer.currentTime.Subscribe(time =>
        {
            gameView.UpdateTimerValue(time);
        }).AddTo(this);

        timer.IsTimeUp.Subscribe(timeUp =>
        {
            if (timeUp)
            {
                FinishedGame();
            }
            else
            {
                InProgressGame();
            }
        }).AddTo(this);

        score.currentScore.Subscribe(score =>
        {
            gameView.UpdateScoreValue(score);

        }).AddTo(this);

        player.currentAge.Subscribe(age =>
        {
            gameView.UpdateAgeValue(Mathf.FloorToInt(age));
        }).AddTo(this);
    }

    void GameInitialize()
    {
        /*
         * - 竹の切り替え
         * - 月の配置
         */
        // bambooGenerator.PlacementBamboo();

    }

    void InProgressGame()
    {
        CursorManager.OffCursor();
        Time.timeScale = 1;
    }

    void FinishedGame()
    {
        Debug.Log("Game Stop");
        CursorManager.OnCursor();
        Time.timeScale = 0;

        SaveHighScore(score.currentEntity);
        SaveItemProgress(score.currentEntity.itemEntities);

        resultViewPresenter.ShowResultView(true);
    }

    #region LocalMethod

    void SaveHighScore(ScoreEntity thisScore)
    {
        var isExist = PlayerDataManager.LoadScoreData(out var scoreEntity);
        if (!isExist || thisScore.Score > scoreEntity.Score)
        {
            PlayerDataManager.WriteScoreData(thisScore);
        }
    }
    void SaveItemProgress(List<ItemEntity> items)
    {
        PlayerDataManager.WriteProgressData(items);
    }
    #endregion

    void TestScore()
    {
        score.AddBamboo(BambooInfo.BambooType.Normal);
        score.AddBamboo(BambooInfo.BambooType.Normal);
        score.AddBamboo(BambooInfo.BambooType.Shine);
        score.AddBamboo(BambooInfo.BambooType.Normal);
        score.AddBamboo(BambooInfo.BambooType.Kaguya);
        foreach (var i in testItem)
        {
            var ent = new ItemEntity();
            ent.ID = i.GetID();
            ent.IsGot = true;
            score.AddItem(ent);
        }
    }
}
