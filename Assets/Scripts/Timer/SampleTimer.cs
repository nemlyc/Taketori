using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UniRx;

[RequireComponent(typeof(Timer))]
public class SampleTimer : MonoBehaviour
{
    [SerializeField]
    Button countUpStart, countDownStart, StopButton, ResetButton;
    [SerializeField]
    int CountDownTime = 60;
    [SerializeField]
    TMP_Text timerText, rawTime, result;

    Timer timer;

    private void Awake()
    {
        timer = GetComponent<Timer>();
        countUpStart.onClick.AddListener(() =>
        {
            timer.SetTimerType(false);
            timer.StartTimer();
        });
        countDownStart.onClick.AddListener(() =>
        {
            timer.SetTimerType(true);
            timer.StartTimer(CountDownTime);
        });

        StopButton.onClick.AddListener(() =>
        {
            result.text = timer.StopAndReturnTimer();
        });
        ResetButton.onClick.AddListener(() =>
        {
            timer.ResetTimer();
        });

        timer.IsTimeUp.Subscribe(timeUp =>
        {
            if (timeUp)
            {
                timer.StopTimer();
                result.text = "Time Up";
            }
            
        }).AddTo(this);
    }

    private void Update()
    {
        timerText.text = timer.currentTime.Value;
        rawTime.text = timer.RawTime.ToString();
    }
}
