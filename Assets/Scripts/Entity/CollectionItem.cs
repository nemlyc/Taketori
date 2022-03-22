using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/CollectionItem")]
public class CollectionItem : ScriptableObject
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

    public CollectionItem()
    {
        ID = System.Guid.NewGuid().ToString();
    }
}
