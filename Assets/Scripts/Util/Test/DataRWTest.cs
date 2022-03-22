using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataRWTest : MonoBehaviour
{
    [SerializeField]
    CollectionItem item1;

    private void Start()
    {
        ItemEntity i1 = new ItemEntity();
        i1.ID = item1.GetID();
        i1.IsGot = true;
        Debug.Log(i1.ID);
        List<ItemEntity> items = new List<ItemEntity>();
        items.Add(i1);

        ScoreEntity score = new ScoreEntity()
        {
            Date = System.DateTime.Now,
            NormalNum = 11,
            ShineNum = 5,
            KaguyaNum = 1,
            itemEntities = items
        };

        PlayerDataManager.WriteProgressData(items);
        PlayerDataManager.WriteScoreData(score);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (PlayerDataManager.LoadProgressData(out var a))
            {
                Debug.Log(a[0].ToString());
            }
            if (PlayerDataManager.LoadScoreData(out var b))
            {
                Debug.Log(b.ToString());
            }
        }
    }
}
