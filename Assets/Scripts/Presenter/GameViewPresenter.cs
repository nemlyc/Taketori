using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class GameViewPresenter : MonoBehaviour
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
    InGameView gameView;

    private void Start()
    {
        timer.currentTime.Subscribe(time =>
        {
            gameView.UpdateTimerValue(time);
        }).AddTo(this);

        timer.IsTimeUp.Subscribe(timeUp =>
        {
            Debug.Log("タイムアップ！ wip");
        }).AddTo(this);

        score.currentScore.Subscribe(score =>
        {
            gameView.UpdateScoreValue(score);
        }).AddTo(this);
    }
}
