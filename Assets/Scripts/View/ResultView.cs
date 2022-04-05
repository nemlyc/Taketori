using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultView : MonoBehaviour
{
    [SerializeField]
    TMP_Text normalNum, shineNum, kaguyaNum, scoreNum;

    [SerializeField]
    GameObject itemRoot, collectionPrefab;

    [SerializeField]
    ResultViewPresenter presenter;

    [SerializeField]
    Button closeButton, retryButton;
    [SerializeField]
    CollectionItem[] itemList;
    Dictionary<string, CollectionItem> itemDictionary;
    [SerializeField]
    AudioPlayer audioPlayer;

    public void UpdateNums(ScoreEntity entity)
    {
        normalNum.text = AppendPresetText(entity.NormalNum, PresetText.Hon);
        shineNum.text = AppendPresetText(entity.ShineNum, PresetText.Hon);
        kaguyaNum.text = AppendPresetText(entity.KaguyaNum, PresetText.Hon);

        scoreNum.text = entity.Score.ToString();
    }

    public void PlacementGotItem(List<ItemEntity> items)
    {
        foreach (var itemEntity in items)
        {
            var go = Instantiate(collectionPrefab, itemRoot.transform);
            go.GetComponent<CollectionResultComponent>().SetImage(itemDictionary[itemEntity.ID].sprite);
        }
    }

    private void Start()
    {
        closeButton.onClick.AddListener(() =>
        {
            audioPlayer.PlayOneShot(AudioInfo.UIClick);

            presenter.ToHomeScene();
        });
        retryButton.onClick.AddListener(() =>
        {
            audioPlayer.PlayOneShot(AudioInfo.UIClick);

            presenter.Retry();
        });

        itemDictionary = new Dictionary<string, CollectionItem>();
        foreach (var item in itemList)
        {
            itemDictionary.Add(item.GetID(), item);
        }
    }

    string AppendPresetText(int num, string presetText)
    {
        return $"{num}{presetText}";
    }
}
