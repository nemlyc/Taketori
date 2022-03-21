using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeViewPresenter : MonoBehaviour
{
    [SerializeField]
    HomeView homeView;

    private void Awake()
    {
        LoadProgressData();
        LoadScoreData();
    }

    void LoadProgressData()
    {
        if (PlayerDataManager.LoadProgressData(out var items))
        {
            foreach (var item in items)
            {
                // WIP: Fill item status
            }
        }
    }

    void LoadScoreData()
    {
        if (PlayerDataManager.LoadScoreData(out var score))
        {
            homeView.UpdateScore(CalcScore(score));
            homeView.UpdateBambooNums(score);
            homeView.UpdateDate(score.Date.ToString());
        }
    }

    int CalcScore(ScoreEntity score)
    {
        var result = ((score.NormalNum * BambooInfo.NormalScore) +
            (score.ShineNum * BambooInfo.ShinyScore)) * score.KaguyaNum * BambooInfo.KaguyaScoreMagnification;

        return result;
    }
}
