using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.IO;
using TMPro;

public class LicenseView : MonoBehaviour
{
    [SerializeField]
    Button closeButton;
    [SerializeField]
    GameObject canvasRoot;
    [SerializeField]
    TMP_Text bodyText;

    [SerializeField]
    AudioPlayer audioPlayer;

    readonly string LicenseText = "License.txt";
    string cache;

    private void Start()
    {
        closeButton.onClick.AddListener(() =>
        {
            audioPlayer.PlayOneShot(AudioInfo.UIClick);

            ShowLicenseView(false);
        });
    }

    public async void ShowLicenseView(bool status)
    {
        canvasRoot.SetActive(status);

        if (!status)
        {
            return;
        }

        var path = JsonManager.GetStreamingDataPath(LicenseText);
        if (cache == null)
        {
            var text = await UniTask.RunOnThreadPool(() => File.ReadAllText(path));
            cache = text;
        }
        bodyText.text = cache;
    }
}
