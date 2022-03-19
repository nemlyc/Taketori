using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BambooModelChanger : MonoBehaviour
{
    [SerializeField]
    GameObject NormalBamboo, ShinyBamboo, KaguyaBamboo;

    readonly Vector3 BambooPosition = new Vector3(0, 10, 0);

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
        Instantiate(prefab, BambooPosition, Quaternion.identity, transform);
    }

    private void Reset()
    {
        NormalBamboo = transform.GetChild(0).gameObject;
    }
}
