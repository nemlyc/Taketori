﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    /*
     * 確率でまだ入手していないアイテムを排出する。
     */
    public Dictionary<string, ItemEntity> itemMap = new Dictionary<string, ItemEntity>();

    List<ItemEntity> unownedList = new List<ItemEntity>();

    readonly float probability = 5f; // => %
    readonly float Min = 0f;
    readonly float Max = 10001f;
    readonly float Percent = 100;

    public void CreateItemMap()
    {
        PlayerDataManager.LoadProgressData(out var data);
        foreach (var item in data)
        {
            itemMap.Add(item.ID, item);
        }
        CreateUnownedList();
    }

    public ItemEntity GetItem(int index)
    {
        return unownedList[index];
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
            return Random.Range((int)Min, unownedList.Count);
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
    }

}