using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UniRx;

public class HomeView : MonoBehaviour
{
    [SerializeField]
    TMP_Text scoreValue, normalNum, shineNum, kaguyaNum;
    [SerializeField]
    TMP_Text rateValue;

    [SerializeField]
    Button startButton;

    public void UpdateScore(int score)
    {
        scoreValue.text = score.ToString();
    }

    public void UpdateNormalNum(int num)
    {
        normalNum.text = AppendPresetText(num, PresetText.Hon);
    }

    public void UpdateShineNum(int num)
    {
        shineNum.text = AppendPresetText(num, PresetText.Hon);
    }

    public void UpdateKaguyaNum(int num)
    {
        kaguyaNum.text = AppendPresetText(num, PresetText.Hon);
    }

    public void UpdateRateValue(int rate)
    {
        rateValue.text = AppendPresetText(rate, PresetText.Percent);
    }

    private void Start()
    {
        startButton.OnClickAsObservable().Subscribe(_ =>
        {
            // to InGameScene
        }).AddTo(this);
    }

    string AppendPresetText(int num, string presetText)
    {
        return $"{num}{presetText}";
    }

    private void Reset()
    {
        scoreValue = GameObject.Find("Score Value").GetComponent<TMP_Text>();
        normalNum = GameObject.Find("Normal Num").GetComponent<TMP_Text>();
        shineNum = GameObject.Find("Shine Num").GetComponent<TMP_Text>();
        kaguyaNum = GameObject.Find("Kaguya Num").GetComponent<TMP_Text>();
        rateValue = GameObject.Find("Rate Value").GetComponent<TMP_Text>();
        startButton = GameObject.Find("Start Button").GetComponent<Button>();
    }
}
