using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BambooModelChanger : MonoBehaviour
{
    /*
     * GeneratorからInitializeを呼び出し、モデルを切り替える。
     */

    [SerializeField]
    GameObject NormalBamboo, ShinyBamboo, KaguyaBamboo;

    public void Initialize(BambooInfo.BambooType type)
    {
        switch (type)
        {
            case BambooInfo.BambooType.Normal:
                break;

            case BambooInfo.BambooType.Shine:
                ChangeModelFromNormal(ShinyBamboo);
                break;

            case BambooInfo.BambooType.Kaguya:
                ChangeModelFromNormal(KaguyaBamboo);
                break;
        }
    }

    void ChangeModelFromNormal(GameObject prefab)
    {
        NormalBamboo.SetActive(false);
        Instantiate(prefab, transform.position, Quaternion.identity, transform);
    }

    private void Reset()
    {
        NormalBamboo = transform.GetChild(0).gameObject;
    }
}
