using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Timer : MonoBehaviour
{
    /*
     * タイマースクリプト
     */

    /// <summary>
    /// 現在の時間をstringで返す。
    /// </summary>
    public string CurrentTime { get; private set; }

    /// <summary>
    /// 現在の時間のfloatを返す。
    /// </summary>
    public float RawTime { get; private set; }

    ReactiveProperty<bool> isTimeUp = new ReactiveProperty<bool>();
    public IReadOnlyReactiveProperty<bool> IsTimeUp { get { return isTimeUp; } }

    float localTime;
    bool isCountDown = false;
    bool isRunning = false;

    /// <summary>
    /// 内部タイマーを0にする。
    /// </summary>
    public void ResetTimer()
    {
        localTime = 0;
    }

    /// <summary>
    /// 動作を停止する。
    /// </summary>
    public void StopTimer()
    {
        isRunning = false;
    }

    /// <summary>
    /// 動作を停止し、最終的な時間を返す。
    /// </summary>
    /// <returns>時間</returns>
    public string StopAndReturnTimer()
    {
        isRunning = false;
        return CurrentTime;
    }

    /// <summary>
    /// カウントアップタイマーを開始する。
    /// </summary>
    /// <param name="isRefresh">0から開始するかどうか。</param>
    public void StartTimer(bool isRefresh = false)
    {
        isRunning = true;
        if (isRefresh)
        {
            ResetTimer();
        }
    }

    /// <summary>
    /// カウントダウンタイマーを開始する
    /// </summary>
    /// <param name="startTime">開始する時間</param>
    public void StartTimer(int startTime)
    {
        localTime = startTime;
        isCountDown = true;

        isRunning = true;

        isTimeUp.Value = false;
    }

    /// <summary>
    /// タイマーの動作状況を返す
    /// </summary>
    /// <returns>タイマーの動作状況</returns>
    public bool GetIsRunning()
    {
        return isRunning;
    }

    /// <summary>
    /// カウントアップとカウントダウンを切り替える。
    /// </summary>
    public void SwitchTimer()
    {
        isCountDown = !isCountDown;
    }

    public void SetTimerType(bool isCountDown)
    {
        this.isCountDown = isCountDown;
    }

    /// <summary>
    /// 時間のフォーマットを整える。
    /// </summary>
    /// <returns></returns>
    private string TimerFormat()
    {
        float ms = localTime;
        float m = Mathf.Floor(localTime / 60);
        float rawSecond = ms - (m * 60);
        float secondsSize = Mathf.Floor(rawSecond);

        string s = rawSecond.ToString("f2");
        if (secondsSize < 10)
        {
            s = $"0{rawSecond:f2}";
        }

        return $"{m}:{s}";
    }

    void CountUp()
    {
        localTime += Time.deltaTime;
    }
    void CountDown()
    {
        localTime -= Time.deltaTime;
        if (localTime <= 0)
        {
            isTimeUp.Value = true;
        }
    }

    private void Update()
    {
        if (isRunning)
        {
            if (isCountDown)
            {
                CountDown();
            }
            else
            {
                CountUp();
            }
        }
        CurrentTime = TimerFormat();
        RawTime = localTime;
    }

}
