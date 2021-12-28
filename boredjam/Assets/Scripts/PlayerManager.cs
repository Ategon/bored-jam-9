using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int activePlayers = 0;
    public int totalPlayers = 0;
    public int overrideIndex = -1;

    [SerializeField] private GameObject pressStart;

    void FixedUpdate()
    {
        if (activePlayers == 0 && totalPlayers > 0)
        {
            pressStart.SetActive(true);
        }
        else
        {
            pressStart.SetActive(false);
        }
    }
}
