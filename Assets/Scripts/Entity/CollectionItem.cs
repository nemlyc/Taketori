using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/CollectionItem")]
public class CollectionItem : ScriptableObject
{
    [SerializeField]
    private string ID;
    [SerializeField]
    public string itemName;
    [SerializeField]
    public Sprite sprite;

    public string GetID()
    {
        return ID;
    }

    public CollectionItem()
    {
        ID = System.Guid.NewGuid().ToString();
    }
}
