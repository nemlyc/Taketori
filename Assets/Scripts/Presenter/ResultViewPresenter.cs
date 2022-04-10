using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultViewPresenter : MonoBehaviour
{
    [SerializeField]
    ScoreManager score;

    [SerializeField]
    ResultView resultView;

    [SerializeField]
    GameObject resultCanvas;

    public void ShowResultView(bool state)
    {
        resultCanvas.SetActive(state);

        if (state)
        {
            var entity = score.currentEntity;
            resultView.UpdateNums(entity);

            resultView.PlacementGotItem(entity.itemEntities);
        }
    }

    public void ToHomeScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(ViewInfo.HomeView);
    }

    public void Retry()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(ViewInfo.InGameView);
    }
}
