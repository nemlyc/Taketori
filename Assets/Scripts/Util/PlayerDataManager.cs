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

    public static void WriteProgressData(List<ItemEntity> items)
    {
        var json = JsonManager.GenerateJsonObject<List<ItemEntity>>(items);
        JsonManager.WriteJsonData(ProgressDataPath, json);
    }

    public static void WriteScoreData(ScoreEntity entity)
    {
        var json = JsonManager.GenerateJsonObject<ScoreEntity>(entity);
        JsonManager.WriteJsonData(ScoreDataPath, json);
    }
}
