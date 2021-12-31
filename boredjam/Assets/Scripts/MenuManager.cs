using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("Assignables")]

    [SerializeField] private Light2D backgroundLight;
    [SerializeField] private LightManager lightManager;
    [SerializeField] private Color normalColor;
    [SerializeField] private Color optionsColor;
    [SerializeField] private Color creditsColor;
    [SerializeField] private Color podiumColor;
    [SerializeField] private Slider[] sliders;
    private PersistingData persistingData;

    public void Start()
    {
        Time.timeScale = 1;
        persistingData = GameObject.Find("Persisting Data").GetComponent<PersistingData>();
        sliders[0].value = persistingData.MasterVolume;
        sliders[1].value = persistingData.SoundtracksVolume;
        sliders[2].value = persistingData.SfxVolume;
        sliders[3].value = persistingData.GameVolume;
    }

    public void SetNormalColor()
    {
        backgroundLight.color = normalColor;
    }

    public void SetOptionsColor()
    {
        backgroundLight.color = optionsColor;
    }

    public void SetCreditsColor()
    {
        backgroundLight.color = creditsColor;
    }

    public void SetPodiumColor()
    {
        backgroundLight.color = podiumColor;
    }

    public void quit()
    {
#if UNITY_EDITOR
         UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void play()
    {
        SceneManager.LoadScene(1);
    }

    public void Ategon()
    {
        Application.OpenURL("https://twitter.com/Etegondev");
    }

    public void BatterFingers()
    {
        Application.OpenURL("https://twitter.com/Batterfingers");
    }

    public void ToggleFullscreen()
    {
        if (Screen.fullScreenMode == FullScreenMode.Windowed)
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
    }

    public void UpdateMasterVolume(float amount){
        persistingData.MasterVolume = (int) amount;
    }

    public void UpdateSoundtracksVolume(float amount)
    {
        persistingData.SoundtracksVolume = (int)amount;
    }

    public void UpdateSfxVolume(float amount)
    {
        persistingData.SfxVolume = (int)amount;
    }

    public void UpdateGameVolume(float amount)
    {
        persistingData.GameVolume = (int)amount;
    }
}
