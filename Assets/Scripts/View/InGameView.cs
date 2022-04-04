using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InGameView : MonoBehaviour
{
    [SerializeField]
    TMP_Text ageValue, scoreValue, timerValue, readyTimeValue;
    [SerializeField]
    CanvasFader fader;
    [SerializeField]
    GameObject statusCanvas;
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
    }

    private void Start()
    {
        fader.DoFadeIn(fadeTime);
    }

    private void Reset()
    {
        ageValue = GameObject.Find("Age Value").GetComponent<TMP_Text>();
        scoreValue = GameObject.Find("Score Value").GetComponent<TMP_Text>();
        timerValue = GameObject.Find("Timer Value").GetComponent<TMP_Text>();
        readyTimeValue = GameObject.Find("Ready Time Value").GetComponent<TMP_Text>();
    }
}
