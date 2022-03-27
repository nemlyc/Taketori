﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UniRx;
using UnityEngine.SceneManagement;
using System;
using System.Linq;

public class HomeView : MonoBehaviour
{
    [SerializeField]
    TMP_Text scoreValue, normalNum, shineNum, kaguyaNum;
    [SerializeField]
    TMP_Text rateValue;
    [SerializeField]
    TMP_Text dateValue;

    [SerializeField]
    Button startButton, collectionButton;

    [SerializeField]
    CanvasFader fader;

    [SerializeField]
    CollectionView collectionView;

    [SerializeField]
    GameObject gotItemRoot;

    [SerializeField]
    GameObject collectionItemPrefab;

    [SerializeField]
    HomeViewPresenter presenter;

    [SerializeField]
    CollectionItemViewComponent[] itemList;

    readonly float fadeTime = 0.5f;

    public void UpdateScore(int score)
    {
        scoreValue.text = score.ToString();
    }

    public void UpdateBambooNums(ScoreEntity scoreEntity)
    {
        normalNum.text = AppendPresetText(scoreEntity.NormalNum, PresetText.Hon);
        shineNum.text = AppendPresetText(scoreEntity.ShineNum, PresetText.Hon);
        kaguyaNum.text = AppendPresetText(scoreEntity.KaguyaNum, PresetText.Hon);
    }

    public void UpdateRateValue(int rate)
    {
        rateValue.text = AppendPresetText(rate, PresetText.Percent);
    }

    public void UpdateDate(string date)
    {
        dateValue.text = $"（{date}）";
    }

    public void PlacementGotItem(List<ItemEntity> items)
    {
        var map = presenter.GetItemMap();

        foreach (var itemEntity in items)
        {
            var go = Instantiate(collectionItemPrefab, gotItemRoot.transform);
            go.GetComponent<CollectionResultComponent>().SetImage(map[itemEntity.ID].sprite);
        }
    }

    public void UpdateStatus(ItemEntity entity)
    {
        var list = itemList.ToList();

        var item = list.Find(x => x.ID.Equals(entity.ID));
        item.UpdateStatus(entity.ID, entity.IsGot);        
    }

    private void Start()
    {
        startButton.OnClickAsObservable().Subscribe(_ =>
        {
            fader.DoFadeOut(fadeTime);
            var a = SceneManager.LoadSceneAsync(ViewInfo.InGameView);
            a.allowSceneActivation = false;

            Observable.Timer(TimeSpan.FromSeconds(fadeTime))
                .Subscribe(_ => ExecuteSceneChange(a)).AddTo(this);
        }).AddTo(this);

        collectionButton.OnClickAsObservable().Subscribe(_ =>
        {
            collectionView.ShowCollectionCanvas(true);
        }).AddTo(this);
    }

    void ExecuteSceneChange(AsyncOperation a)
    {
        a.allowSceneActivation = true;
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
        dateValue = GameObject.Find("Date Value").GetComponent<TMP_Text>();
        startButton = GameObject.Find("Start Button").GetComponent<Button>();
    }
}
