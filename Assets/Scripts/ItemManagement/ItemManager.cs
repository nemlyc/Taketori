using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    /*
     * 確率でまだ入手していないアイテムを排出する。
     */
    public Dictionary<string, ItemEntity> itemMap = new Dictionary<string, ItemEntity>();

    List<ItemEntity> unownedList = new List<ItemEntity>();

    readonly float probability = 2.5f; // => %
    readonly float Min = 0f;
    readonly float Max = 10001f;
    readonly float Percent = 100;

    public void CreateItemMap()
    {
        PlayerDataManager.LoadProgressData(out var data);
        if (data == null)
        {
            Debug.LogError("Homeシーンを一度実行してください（アイテム出現なし）。");
            return;
        }
        foreach (var item in data)
        {
            itemMap.Add(item.ID, item);
        }
        CreateUnownedList();
    }

    public ItemEntity GetAndRemoveItem(int index)
    {
        var entity = unownedList[index];
        unownedList.Remove(entity);
        return entity;
    }

    public int PickUp()
    {
        if (itemMap.Count == 0)
        {
            CreateItemMap();
        }

        Random.InitState(System.DateTime.Now.Millisecond);
        var value = Random.Range(Min, Max);
        if (unownedList.Count > 0 && value <= probability * Percent)
        {
            var pickupedIndex = Random.Range((int)Min, unownedList.Count);
            return pickupedIndex;
        }
        else
        {
            return -1;
        }
    }

    void CreateUnownedList()
    {
        foreach (var id in itemMap.Keys)
        {
            var entity = itemMap[id];
            if (!entity.IsGot)
            {
                unownedList.Add(itemMap[id]);
            }
        }
        Debug.Log($"Unowned list created : {unownedList.Count}");
    }

}
