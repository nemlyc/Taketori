using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BambooGenerator : MonoBehaviour
{
    /*
     * すでに配置した竹を入れ替える
     *  - ある程度遠くの、候補となる竹にタグ付けして取得。ランダムに選出して、初期化を行う。
     *   - Shinable
     *   - （使用しない）Kaguyable
     */
    [SerializeField]
    readonly int ShinableNum = 5, KaguyaNum = 1;

    public void PlacementBamboo()
    {
        var shinableBamboos = GameObject.FindGameObjectsWithTag(BambooInfo.BambooTag.Shinable.ToString());

        AssignAnyBamboo(shinableBamboos);
    }

    void AssignShinyBamboo(GameObject[] shinables)
    {
        if (shinables.Length == 0)
        {
            return;
        }

        var randomPicks = PickUp(shinables.Length, ShinableNum);
        InitializeBamboos(shinables, randomPicks.ToList(), BambooInfo.BambooType.Shine);
    }

    void AssignKaguyaBamboo(GameObject[] kaguyable)
    {
        if (kaguyable.Length == 0)
        {
            return;
        }

        var randomPicks = PickUp(kaguyable.Length, KaguyaNum);
        InitializeBamboos(kaguyable, randomPicks.ToList(), BambooInfo.BambooType.Kaguya);
    }

    void AssignAnyBamboo(GameObject[] shinables)
    {
        if (shinables.Length == 0)
        {
            return;
        }

        var shinePicks = PickUp(shinables.Length, ShinableNum).ToList();
        InitializeBamboos(shinables, shinePicks, BambooInfo.BambooType.Shine);


        var kaguyaPicks = PickUp(shinables.Length, KaguyaNum).ToList();
        while (IsMultiple(kaguyaPicks, shinePicks))
        {
            kaguyaPicks = PickUp(shinables.Length, KaguyaNum).ToList();
        }

        InitializeBamboos(shinables, kaguyaPicks, BambooInfo.BambooType.Kaguya);
    }

    void InitializeBamboos(GameObject[] bamboos, List<int> selected, BambooInfo.BambooType type)
    {
        foreach (var index in selected)
        {
            bamboos[index].GetComponent<BambooModelChanger>().Initialize(type);
        }
    }

    IEnumerable<int> PickUp(int length, int takeNum)
    {
        return Enumerable.Range(0, length).OrderBy(n => System.Guid.NewGuid()).Take(takeNum);
    }

    bool IsMultiple(List<int> kaguyaPicks, List<int> shinePicks)
    {
        foreach (var item in kaguyaPicks)
        {
            if (shinePicks.Contains(item))
            {
                return true;
            }
        }
        return false;
    }
}
