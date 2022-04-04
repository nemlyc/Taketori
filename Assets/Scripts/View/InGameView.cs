using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InGameView : MonoBehaviour
{
    [SerializeField]
    TMP_Text ageValue, scoreValue, timerValue, readyTimeValue, logText;
    [SerializeField]
    CanvasFader fader;
    [SerializeField]
    GameObject statusCanvas, resultCanvas, playerCanvas, logCanvas;

    [SerializeField]
    ParticleSystem clearParticle;

    readonly float fadeTime = 0.5f;
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

    public void UpdateReadyTimeValue(string time)
    {
        readyTimeValue.text = time;
    }

    public void UpdateLogText(string text)
    {
        logText.text += text;
    }

    public void PlayParticle()
    {
        clearParticle.Play();
    }

    public void ToggleReadyTime(bool isVisible)
    {
        readyTimeValue.gameObject.SetActive(isVisible);
    }

    public void SetView(bool isVisible)
    {
        statusCanvas.SetActive(isVisible);
        SetLogWindow(isVisible);
        SetPlayerWindow(isVisible);
    }

    public void SetLogWindow(bool isVisible)
    {
        logCanvas.SetActive(isVisible);
    }

    public void SetPlayerWindow(bool isVisible)
    {
        playerCanvas.SetActive(isVisible);
    }

    private void Start()
    {
        fader.DoFadeIn(fadeTime);

        SetLogWindow(false);
    }

    private void Reset()
    {
        ageValue = GameObject.Find("Age Value").GetComponent<TMP_Text>();
        scoreValue = GameObject.Find("Score Value").GetComponent<TMP_Text>();
        timerValue = GameObject.Find("Timer Value").GetComponent<TMP_Text>();
        readyTimeValue = GameObject.Find("Ready Time Value").GetComponent<TMP_Text>();
    }
}
