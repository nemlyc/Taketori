using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;

public class CollectionViewPresenter : MonoBehaviour
{
    [SerializeField]
    CollectionView collectionView;

    [SerializeField]
    HomeViewPresenter homeViewPresenter;

    public List<ItemEntity> GetItems()
    {
        return homeViewPresenter.items;
    }
}
