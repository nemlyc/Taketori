using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[Serializable]
public class ScoreEntity
{
    public DateTime Date { get; set; }
    public int Score { get; set; }
    public int NormalNum { get; set; }
    public int ShineNum { get; set; }
    public int KaguyaNum { get; set; }
    public List<ItemEntity> itemEntities = new List<ItemEntity>();

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("[");
        foreach (var item in itemEntities)
        {
            sb.Append($"{item.ID}, ");
        }
        sb.Append("]");
        Date = DateTime.Now;
        string res = $"Date : {{{Date}}}\n" +
            $"NormalNum : {NormalNum}\nShine : {ShineNum}\nKaguya : {KaguyaNum}\n" +
            $"Items : {sb}";
        return res;
    }
}
