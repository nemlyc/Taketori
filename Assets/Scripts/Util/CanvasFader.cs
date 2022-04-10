using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;

[RequireComponent(typeof(Timer))]
public class CanvasFader : MonoBehaviour
{
    [SerializeField]
    Image fadePanel;

    Color blackProperty = Color.black;
    int alphaValue = 0;

    Timer localTimer;
    float currentDuration = 0;
    bool isFadeIn;

    /// <summary>
    /// 徐々に明るくする。
    /// </summary>
    /// <param name="duration"></param>
    public void DoFadeIn(float duration)
    {
        isFadeIn = true;
        fadePanel.color = blackProperty;
        currentDuration = duration;

        localTimer.StartTimer(duration);
    }

    /// <summary>
    /// 徐々に暗くする。
    /// </summary>
    /// <param name="duration"></param>
    public void DoFadeOut(float duration)
    {
        isFadeIn = false;
        fadePanel.color = new Color(blackProperty.r, blackProperty.g, blackProperty.b, 0);
        currentDuration = duration;

        localTimer.StartTimer(duration);
    }

    private void Update()
    {
        if (localTimer.GetIsRunning())
        {
            if (isFadeIn)
            {
                fadePanel.color -= new Color(0, 0, 0, Mathf.Clamp01(Time.deltaTime / currentDuration));
            }
            else
            {
                fadePanel.color += new Color(0, 0, 0, Mathf.Clamp01(Time.deltaTime / currentDuration));
            }
        }
    }

    private void Awake()
    {
        fadePanel.color = new Color(blackProperty.r, blackProperty.g, blackProperty.b, alphaValue);
        localTimer = GetComponent<Timer>();
    }

    private void Reset()
    {
        fadePanel = GetComponentInChildren<Image>();
    }
}
