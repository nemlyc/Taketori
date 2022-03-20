using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/CorrectionItem")]
public class CorrectionItem : ScriptableObject
{
    [SerializeField]
    private string ID;
    [SerializeField]
    private Sprite sprite;

    public string GetID()
    {
        return ID;
    }

    public Sprite GetSprite()
    {
        return sprite;
    }

    public CorrectionItem()
    {
        ID = System.Guid.NewGuid().ToString();
    }
}
