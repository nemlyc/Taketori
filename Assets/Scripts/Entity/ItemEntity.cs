using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ItemEntity
{
    public string ID { get; set; }
    public bool IsGot { get; set; }

    public override string ToString()
    {
        return $"[{ID}] : IsGot [{IsGot}]";
    }
}
