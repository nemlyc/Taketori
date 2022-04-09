using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioMapper))]
[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour
{
    /*
     * ID<場面の名前>を指定して音を再生する。
     * BGMと効果音は分ける。
     */

    [SerializeField]
    AudioMapper mapper;
    [SerializeField]
    AudioSource bgmSource, seSource;
    [SerializeField]
    float MasterVolume = 1f;
    [SerializeField]
    float BGMVolume = 0.03f;

    public void PlayBGMFromMap(string name)
    {
        if (TryGetClip(name, out var clip))
        {
            bgmSource.clip = clip;
            bgmSource.Play();
        }
    }
    public void StopBGM()
    {
        bgmSource.Stop();
    }

    public void PlayOneShot(string name)
    {
        if (TryGetClip(name, out var data))
        {
            seSource.PlayOneShot(data);
        }
    }

    public void PlayBambooSE(BambooInfo.BambooType type)
    {
        switch (type)
        {
            case BambooInfo.BambooType.Normal:
            case BambooInfo.BambooType.Shine:
                PlayOneShot(AudioInfo.HitNormal);
                break;
            case BambooInfo.BambooType.Kaguya:
                PlayOneShot(AudioInfo.HitKaguya);
                break;
        }
    }

    bool TryGetClip(string name, out AudioClip clip)
    {
        if (mapper.audioMap.TryGetValue(name, out clip))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Awake()
    {
        bgmSource.volume = BGMVolume;
        seSource.volume = MasterVolume;
        bgmSource.loop = true;
    }

    private void Reset()
    {
        mapper = GetComponent<AudioMapper>();
        bgmSource = GetComponent<AudioSource>();
        seSource = GetComponent<AudioSource>();
    }
}
