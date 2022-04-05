using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System.Linq;

public class CollectionView : MonoBehaviour
{
    private ReactiveProperty<bool> isActive = new ReactiveProperty<bool>();
    public IReadOnlyReactiveProperty<bool> IsActive { get { return IsActive; } }

    [SerializeField]
    CollectionItemViewComponent[] itemList;
    [SerializeField]
    Button backButton;
    [SerializeField]
    GameObject collectionCanvas;

    [SerializeField]
    CollectionViewPresenter collectionViewPresenter;

    [SerializeField]
    AudioPlayer audioPlayer;

    private void Start()
    {
        backButton.OnClickAsObservable().Subscribe(_ =>
        {
            audioPlayer.PlayOneShot(AudioInfo.UIClick);

            ShowCollectionCanvas(false);
        }).AddTo(this);
    }

    public void ShowCollectionCanvas(bool status)
    {
        collectionCanvas.SetActive(status);
        isActive.Value = status;

        if (status)
        {
            var itemData = collectionViewPresenter.GetItems();

            // CollectionItemViewComponentとIDの一致するItemEntityの状態を反映する。
            var list = itemList.ToList();
            foreach (var entity in itemData)
            {
                var item = list.Find(x => x.ID.Equals(entity.ID));
                item.UpdateStatus(entity.ID, entity.IsGot);
            }

        }
    }
}
