using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Assignables")]

    [SerializeField] private Light2D backgroundLight;
    [SerializeField] private LightManager lightManager;
    [SerializeField] private Color normalColor;
    [SerializeField] private Color optionsColor;
    [SerializeField] private Color creditsColor;
    [SerializeField] private Color podiumColor;

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
}
