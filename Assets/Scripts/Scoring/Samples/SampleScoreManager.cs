using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ScoreManager))]
public class SampleScoreManager : MonoBehaviour
{
    [SerializeField]
    CorrectionItem[] items;

    ScoreManager scoreManager;

    private void Awake()
    {
        scoreManager = GetComponent<ScoreManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<GenericBamboo>(out var bamboo))
        {
            var score = scoreManager.currentScore;
            scoreManager.currentScore = bamboo.CalcScore(score);

            bamboo.AttackAction();
            var type = bamboo.GetBambooType();
            scoreManager.AddBamboo(type);

            Debug.Log(scoreManager.currentScore);
        }
    }

    private void OnApplicationQuit()
    {
        var entity = scoreManager.FetchResult();
        Debug.Log(entity);
    }

    void ObtainTestItem()
    {
        foreach (var item in items)
        {
            ItemEntity entity = new ItemEntity()
            {
                IsGot = true
            };
            entity.SetID(item.GetID());

            scoreManager.AddItem(entity);
        }

        
    }
}
