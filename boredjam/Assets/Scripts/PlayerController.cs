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

    [Header("Variables")]
    [SerializeField] private bool p1Escape;
    [SerializeField] private bool p2Escape;
    [SerializeField] private bool p1Jump;
    [SerializeField] private bool p2Jump;
    [SerializeField] private float p1Move;
    [SerializeField] private float p2Move;
    [SerializeField] private bool p1Attack;
    [SerializeField] private bool p2Attack;
    [SerializeField] private bool p1Fireball;
    [SerializeField] private bool p2Fireball;
    [SerializeField] private bool p1Dash;
    [SerializeField] private bool p2Dash;

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
        characterSelect.transform.Find("Press Text").gameObject.SetActive(false);
        characterSelect.transform.Find("Character").gameObject.SetActive(true);
        characterSelect.transform.Find("Colors").gameObject.SetActive(true);
    }
    

}
