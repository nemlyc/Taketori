using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InGameView : MonoBehaviour
{
    [SerializeField]
    TMP_Text ageValue, scoreValue, timerValue;

    //GameViewPresenter presenter;

    public void UpdateAgeValue(int age)
    {
        ageValue.text = age.ToString();
    }

    public void UpdateScoreValue(int score)
    {
        scoreValue.text = score.ToString();
    }

    public void UpdateTimerValue(string time)
    {
        timerValue.text = time;
    }
}
