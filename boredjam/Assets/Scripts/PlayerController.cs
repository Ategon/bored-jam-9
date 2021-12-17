using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Assignables")]
    [SerializeField] private PlayerInput playerInput;

    private GameObject characterSelect;
    private float changeCooldown = 0;

    void Start()
    {
        switch (playerInput.playerIndex)
        {
            case 0:
                characterSelect = GameObject.Find("P1CharacterSelect");
                break;
            case 1:
                characterSelect = GameObject.Find("P2CharacterSelect");
                break;
            case 2:
                characterSelect = GameObject.Find("P3CharacterSelect");
                break;
            case 3:
                characterSelect = GameObject.Find("P4CharacterSelect");
                break;
        }

        //TODO update visual of character select
    }
    

}
