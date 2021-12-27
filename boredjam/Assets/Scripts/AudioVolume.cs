using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioVolume : MonoBehaviour
{
    public enum AudioType
    {
        Game,
        SFX,
        Soundtracks
    }

    [SerializeField] private AudioType audioType;
    private PersistingData persistingData;
    private AudioSource audioSource;

    void Start()
    {
        persistingData = GameObject.Find("PersistingData").GetComponent<PersistingData>();
        audioSource = GetComponent<AudioSource>();

        switch (audioType)
        {
            case AudioType.Soundtracks:
                audioSource.volume = (float)Math.Pow(((float)persistingData.SoundtracksVolume / 10) * ((float)persistingData.MasterVolume / 10), 0.5f);
                PersistingData.OnSoundtrackVolume += VolumeChange;
                break;
            case AudioType.SFX:
                audioSource.volume = (float)Math.Pow(((float)persistingData.SfxVolume / 10) * ((float)persistingData.SfxVolume / 10), 0.5f);
                PersistingData.OnUIVolume += VolumeChange;
                break;
            case AudioType.Game:
                audioSource.volume = (float)Math.Pow(((float)persistingData.GameVolume / 10) * ((float)persistingData.GameVolume / 10), 0.5f);
                PersistingData.OnGameVolume += VolumeChange;
                break;
            default:
                break;
        }
        
    }

    void VolumeChange()
    {
        switch (audioType)
        {
            case AudioType.Soundtracks:
                audioSource.volume = (float)Math.Pow(((float)persistingData.SoundtracksVolume / 10) * ((float)persistingData.MasterVolume / 10), 0.5f);
                break;
            case AudioType.SFX:
                audioSource.volume = (float)Math.Pow(((float)persistingData.SfxVolume / 10) * ((float)persistingData.SfxVolume / 10), 0.5f);
                break;
            case AudioType.Game:
                audioSource.volume = (float)Math.Pow(((float)persistingData.GameVolume / 10) * ((float)persistingData.GameVolume / 10), 0.5f);
                break;
            default:
                audioSource.volume = (float)persistingData.MasterVolume;
                break;
        }
    }

    private void OnDestroy()
    {
        PersistingData.OnUIVolume -= VolumeChange;
        PersistingData.OnSoundtrackVolume -= VolumeChange;
        PersistingData.OnGameVolume -= VolumeChange;
    }
    private void OnDisable()
    {
        PersistingData.OnUIVolume -= VolumeChange;
        PersistingData.OnSoundtrackVolume -= VolumeChange;
        PersistingData.OnGameVolume -= VolumeChange;
    }
}
