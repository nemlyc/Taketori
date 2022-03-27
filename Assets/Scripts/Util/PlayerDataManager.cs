using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerDataManager
{
    readonly static string ProgressDataPath = "PlayerProgress.json";
    readonly static string ScoreDataPath = "PlayerScore.json";
    readonly static string MissingData = "-1";

    public static bool LoadProgressData(out List<ItemEntity> loadedData)
    {
        var data = JsonManager.ReadJsonData(ProgressDataPath);
        if (!data.Equals(MissingData))
        {
            loadedData = JsonManager.ExpandJsonData<List<ItemEntity>>(data);
            return true;
        }
        else
        {
            loadedData = null;
            return false;
        }
    }

    public static bool LoadScoreData(out ScoreEntity loadedData)
    {
        var data = JsonManager.ReadJsonData(ScoreDataPath);
        if (!data.Equals(MissingData))
        {
            loadedData = JsonManager.ExpandJsonData<ScoreEntity>(data);

            return true;
        }
        else
        {
            loadedData = null;
            return false;
        }
    }

    public static void WriteProgressData(List<ItemEntity> newProgress)
    {
        LoadProgressData(out var currentProgress);

        var progressDictionary = new Dictionary<string, bool>();
        foreach (var item in currentProgress)
        {
            progressDictionary.Add(item.ID, item.IsGot);
        }

        foreach (var entity in newProgress)
        {
            if (progressDictionary.TryGetValue(entity.ID, out var isGot) && !isGot)
            {
                progressDictionary[entity.ID] = true;
            }
        }

        var newList = new List<ItemEntity>();
        foreach (var id in progressDictionary.Keys)
        {
            var entity = new ItemEntity()
            {
                ID = id,
                IsGot = progressDictionary[id]
            };
            newList.Add(entity);
        }

        var json = JsonManager.GenerateJsonObject<List<ItemEntity>>(newList);
        JsonManager.WriteJsonData(ProgressDataPath, json);
    }

    public static void WriteScoreData(ScoreEntity entity)
    {
        var json = JsonManager.GenerateJsonObject<ScoreEntity>(entity);
        JsonManager.WriteJsonData(ScoreDataPath, json);
    }
}
