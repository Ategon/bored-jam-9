using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistingData : MonoBehaviour
{
    private int masterVolume = 5;
    private int soundtracksVolume = 5;
    private int sfxVolume = 5;
    private int gameVolume = 5;
    public static PersistingData singleton = null;
   
    public static event System.Action OnGameVolume;
    public static event System.Action OnUIVolume;
    public static event System.Action OnSoundtrackVolume;

    void Start()
    {
        if(singleton != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            singleton = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public int MasterVolume { 
        get 
        {
            return masterVolume; 
        } 
        set
        {
            masterVolume = value;
            OnGameVolume?.Invoke();
            OnUIVolume?.Invoke();
            OnSoundtrackVolume?.Invoke();
        } 
    }

    public int SoundtracksVolume
    {
        get
        {
            return soundtracksVolume;
        }
        set
        {
            soundtracksVolume = value;
            OnSoundtrackVolume?.Invoke();
        }
    }

    public int GameVolume
    {
        get
        {
            return gameVolume;
        }
        set
        {
            gameVolume = value;
            OnGameVolume?.Invoke();
        }
    }

    public int SfxVolume
    {
        get
        {
            return sfxVolume;
        }
        set
        {
            sfxVolume = value;
            OnUIVolume?.Invoke();
        }
    }
}
