using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMapper : MonoBehaviour
{
    [SerializeField]
    AudioClip HomeBGM, InGameBGM, ResultBGM;
    [SerializeField]
    AudioClip NormalAttack, SpecialAttack;
    [SerializeField]
    AudioClip HitNormal, HitKaguya, GetItem;
    [SerializeField]
    AudioClip UIClick;

    public Dictionary<string, AudioClip> audioMap = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        audioMap.Add(AudioInfo.HomeBGM, HomeBGM);
        audioMap.Add(AudioInfo.InGameBGM, InGameBGM);
        audioMap.Add(AudioInfo.ResultBGM, ResultBGM);

        audioMap.Add(AudioInfo.NormalAttack, NormalAttack);
        audioMap.Add(AudioInfo.SpecialAttack, SpecialAttack);

        audioMap.Add(AudioInfo.HitNormal, HitNormal);
        audioMap.Add(AudioInfo.HitKaguya, HitKaguya);

        audioMap.Add(AudioInfo.GetItem, GetItem);

        audioMap.Add(AudioInfo.UIClick, UIClick);
    }
}
