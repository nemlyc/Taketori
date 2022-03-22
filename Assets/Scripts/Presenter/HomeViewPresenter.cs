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
            homeView.UpdateScore(score.Score);
            homeView.UpdateBambooNums(score);
            homeView.UpdateDate(score.Date.ToString());
        }
    }
}
