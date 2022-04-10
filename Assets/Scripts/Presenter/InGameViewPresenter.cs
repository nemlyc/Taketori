using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.Events;

public class InGameViewPresenter : MonoBehaviour
{
    /*
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
    MoonController moon;
    [SerializeField]
    ItemManager item;
    [SerializeField]
    float readyTime = 4, ingameTime = 300;

    [SerializeField]
    ResultViewPresenter resultViewPresenter;

    [SerializeField]
    AudioPlayer audioPlayer;

    public UnityEvent GamePreClearEvent;
    public UnityEvent GameClearEvent;
    public bool isInGame { get; private set; } = false;

    public bool PickUpItem(out ItemEntity entity)
    {
        var index = item.PickUp();
        if (index != -1)
        {
            entity = item.GetAndRemoveItem(index);
            gameView.SetLogWindow(true);
            gameView.UpdateLogText(PresetText.Rare);

            return true;
        }
        else
        {
            entity = new ItemEntity();

            return false;
        }
    }

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
                isInGame = true;
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
        Time.timeScale = 0;

        bambooGenerator.PlacementBamboo();
        moon.InitializeMoon();
        item.CreateItemMap();
        score.Init();

        GamePreClearEvent.AddListener(PreClearGame);
        GameClearEvent.AddListener(FinishedGame);
    }

    void InProgressGame()
    {
        CursorManager.OffCursor();
        Time.timeScale = 1;

        audioPlayer.PlayBGMFromMap(AudioInfo.InGameBGM);
    }

    void PreClearGame()
    {
        audioPlayer.StopBGM();

        gameView.SetView(false);
        timer.StopTimer();
        gameView.PlayParticle();
    }

    void FinishedGame()
    {
        Debug.Log("Game Stop");
        CursorManager.OnCursor();
        Time.timeScale = 0;
        isInGame = false;
        audioPlayer.PlayBGMFromMap(AudioInfo.ResultBGM);

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
}
