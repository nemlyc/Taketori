using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeViewPresenter : MonoBehaviour
{
    [SerializeField]
    HomeView homeView;
    public List<ItemEntity> items;
    [SerializeField] CollectionItem[] ItemTemplate;
    public ScoreEntity highScore;

    Dictionary<string, CollectionItem> itemInfoMap = new Dictionary<string, CollectionItem>();

    [SerializeField]
    AudioPlayer audioPlayer;

    public Dictionary<string, CollectionItem> GetItemMap()
    {
        return itemInfoMap;
    }

    private void Awake()
    {
        CreateItemMap();
        //TestItemProgress();

        LoadProgressData();
        LoadScoreData();
    }

    private void Start()
    {
        audioPlayer.PlayBGMFromMap(AudioInfo.HomeBGM);
    }

    void LoadProgressData()
    {
        if (PlayerDataManager.LoadProgressData(out var items))
        {
            float size = items.Count;
            float got = 0;
            foreach (var item in items)
            {                
                homeView.UpdateStatus(item);
                if (item.IsGot)
                {
                    got += 1;
                }
            }
            this.items = items;

            int value = Mathf.FloorToInt(got / size * 100);
            homeView.UpdateRateValue(value);
        }
    }

    void LoadScoreData()
    {
        if (PlayerDataManager.LoadScoreData(out var score))
        {
            homeView.UpdateScore(score.Score);
            homeView.UpdateBambooNums(score);
            homeView.UpdateDate(score.Date.ToString());
            highScore = score;

            var items = score.itemEntities;
            homeView.PlacementGotItem(items);
        }
    }

    void CreateItemMap()
    {
        foreach (var item in ItemTemplate)
        {
            itemInfoMap.Add(item.GetID(), item);
        }
    }

    void TestItemProgress()
    {
        var list = new List<ItemEntity>();
        foreach (var item in ItemTemplate)
        {
            var ent = new ItemEntity();
            ent.ID = item.GetID();
            ent.IsGot = Random.Range(0, 2) == 0;
            list.Add(ent);
        }
        PlayerDataManager.WriteProgressData(list);
    }
}
