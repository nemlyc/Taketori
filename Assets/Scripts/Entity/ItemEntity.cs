using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ItemEntity
{
    public string ID { get; private set; }
    public bool IsGot { get; set; }

    public void SetID(string id)
    {
        this.ID = id;
    }
}