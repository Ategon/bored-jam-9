using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Soundtrack : MonoBehaviour
{
    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void Start()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
        DontDestroyOnLoad(this.gameObject);
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 2)
        {
            Destroy(this.gameObject);
        }
    }
}
