using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CollectionItemViewComponent : MonoBehaviour
{

    [SerializeField]
    CollectionItem item;
    [SerializeField]
    TMP_Text itemName;
    [SerializeField]
    GameObject notHeldPanel;
    [SerializeField]
    Image itemImage;

    public string ID { get; private set; }
    private bool isGot;

    public void UpdateStatus(string id, bool status)
    {
        if (!id.Equals(ID))
        {
            return;
        }
        isGot = status;
        notHeldPanel.SetActive(!status);
    }

    public void UpdateName()
    {
        itemName.text = item.itemName;
    }

    private void OnEnable()
    {
        ID = item.GetID();
        gameObject.name = ID;
        UpdateName();
    }
}
