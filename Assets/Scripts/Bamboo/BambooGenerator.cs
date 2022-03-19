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
     *   - Kaguyable
     */
    [SerializeField]
    readonly int ShinableNum = 5;

    private void Start()
    {
        var shinableBamboos = GameObject.FindGameObjectsWithTag(BambooInfo.BambooTag.Shinable.ToString());
        var kaguyables = GameObject.FindGameObjectsWithTag(BambooInfo.BambooTag.Kaguyable.ToString());

        AssignShinyBamboo(shinableBamboos);
        AssignKaguyaBamboo(kaguyables);
    }

    void AssignShinyBamboo(GameObject[] shinables)
    {
        if (shinables.Length == 0)
        {
            return;
        }

        var randomPicks = Enumerable.Range(0, shinables.Length).OrderBy(n => System.Guid.NewGuid()).Take(ShinableNum);
        foreach (var index in randomPicks)
        {
            shinables[index].GetComponent<BambooModelChanger>().Initialize(BambooInfo.BambooType.Shine);
        }
    }

    void AssignKaguyaBamboo(GameObject[] kaguyable)
    {
        if (kaguyable.Length == 0)
        {
            return;
        }

        var randomIndex = Random.Range(0, kaguyable.Length);
        kaguyable[randomIndex].GetComponent<BambooModelChanger>().Initialize(BambooInfo.BambooType.Kaguya);
    }
}
