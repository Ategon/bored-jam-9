using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Assignables")]
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private GameObject[] colorTiles;
    [SerializeField] private Sprite[] controlsSprites;
    [SerializeField] private Collision collisionScript;

    private GameObject characterSelect;
    private GameObject character2Select;
    private float changeCooldown = 0;
    private bool escapeBuffer = false;
    private bool jumpBuffer = true;
    private bool attackBuffer = true;

    private float changeCooldown2 = 0;
    private bool escapeBuffer2 = false;
    private bool jumpBuffer2 = true;
    private bool attackBuffer2 = true;
    private GameObject playerObject;
    private GameObject playerObject2;
    [SerializeField] private GameObject playerPrefab;
    private bool notFirst = false;

    private bool inGame = false;


    private float healAmount;
    private float movementAmount;
    private float dashingAmount;

    [SerializeField] private float maxMovementSpeed;
    [SerializeField] private float movementSpeed;


    [Header("Variables")]
    [SerializeField] private float defaultX;
    [SerializeField] private float defaultX2;
    [SerializeField] private bool splitKeyboard = false;
    [SerializeField] private bool escape;
    [SerializeField] private bool p1Jump;
    [SerializeField] private bool p2Jump;
    [SerializeField] private Vector2 p1Move;
    [SerializeField] private Vector2 p2Move;
    [SerializeField] private bool p1Attack;
    [SerializeField] private bool p2Attack;
    [SerializeField] private bool p1Fireball;
    [SerializeField] private bool p2Fireball;
    [SerializeField] private bool p1Dash;
    [SerializeField] private bool p2Dash;

    [SerializeField] private int p1ColorSelectIndex;
    [SerializeField] private int p2ColorSelectIndex;
    [SerializeField] private List<PaletteColor> paletteColors;
    [SerializeField] private List<PaletteColor> paletteColors2;
    private int splitIndex;

    private int selectingState = 0;
    private int selectingState2 = 0;

    private PlayerManager playerManager;

    private float health;

    [SerializeField] private float jumpSpeed;
    [SerializeField] private float jumpCoyoteTimer;
    [SerializeField] private float preJumpTimer;
    private float jumpTimer;
    private bool waitJump = true;

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }   
    
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }


    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (!splitKeyboard)
        {
            if (ctx.ReadValue<float>() == 1f)
            {
                p1Jump = true;
            }
            else
            {
                p1Jump = false;
            }
        }
    }

    public void OnP1Jump(InputAction.CallbackContext ctx)
    {
        if (splitKeyboard)
        {
            if (ctx.ReadValue<float>() == 1f)
            {
                p1Jump = true;
            }
            else
            {
                p1Jump = false;
            }
        }
    }

    public void OnP2Jump(InputAction.CallbackContext ctx)
    {
        if (splitKeyboard)
        {
            if (ctx.ReadValue<float>() == 1f)
            {
                p2Jump = true;
            }
            else
            {
                p2Jump = false;
            }
        }
    }

    public void OnDash(InputAction.CallbackContext ctx)
    {
        if (!splitKeyboard)
        {
            if (ctx.ReadValue<float>() == 1f)
            {
                p1Dash = true;
            }
            else
            {
                p1Dash = false;
            }
        }
    }

    public void OnP1Dash(InputAction.CallbackContext ctx)
    {
        if (splitKeyboard)
        {
            if (ctx.ReadValue<float>() == 1f)
            {
                p1Dash = true;
            }
            else
            {
                p1Dash = false;
            }
        }
    }

    public void OnP2Dash(InputAction.CallbackContext ctx)
    {
        if (splitKeyboard)
        {
            if (ctx.ReadValue<float>() == 1f)
            {
                p2Dash = true;
            }
            else
            {
                p2Dash = false;
            }
        }
    }

    public void OnAttack(InputAction.CallbackContext ctx)
    {
        if (!splitKeyboard)
        {
            if (ctx.ReadValue<float>() == 1f)
            {
                p1Attack = true;
            }
            else
            {
                p1Attack = false;
            }
        }
    }

    public void OnP1Attack(InputAction.CallbackContext ctx)
    {
        if (splitKeyboard)
        {
            if (ctx.ReadValue<float>() == 1f)
            {
                p1Attack = true;
            }
            else
            {
                p1Attack = false;
            }
        }
    }

    public void OnP2Attack(InputAction.CallbackContext ctx)
    {
        if (splitKeyboard)
        {
            if (ctx.ReadValue<float>() == 1f)
            {
                p2Attack = true;
            }
            else
            {
                p2Attack = false;
            }
        }
    }

    public void OnFireball(InputAction.CallbackContext ctx)
    {
        if (!splitKeyboard)
        {
            if (ctx.ReadValue<float>() == 1f)
            {
                p1Fireball = true;
            }
            else
            {
                p1Fireball = false;
            }
        }
    }

    public void OnP1Fireball(InputAction.CallbackContext ctx)
    {
        if (splitKeyboard)
        {
            if (ctx.ReadValue<float>() == 1f)
            {
                p1Fireball = true;
            }
            else
            {
                p1Fireball = false;
            }
        }
    }

    public void OnP2Fireball(InputAction.CallbackContext ctx)
    {
        if (splitKeyboard)
        {
            if (ctx.ReadValue<float>() == 1f)
            {
                p2Fireball = true;
            }
            else
            {
                p2Fireball = false;
            }
        }
    }

    public void OnEscape(InputAction.CallbackContext ctx)
    {
        if (!splitKeyboard)
        {
            if (ctx.ReadValue<float>() == 1f)
            {
                escape = true;
            }
            else
            {
                escape = false;
            }
        }
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        if (!splitKeyboard)
        {
            p1Move = ctx.ReadValue<Vector2>();
        }
    }

    public void OnP1Move(InputAction.CallbackContext ctx)
    {
        if (splitKeyboard)
        {
            p1Move = ctx.ReadValue<Vector2>();
        }
    }

    public void OnP2Move(InputAction.CallbackContext ctx)
    {
        if (splitKeyboard)
        {
            p2Move = ctx.ReadValue<Vector2>();
        }
    }


    void Start()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
        playerManager = GameObject.Find("Player Manager").GetComponent<PlayerManager>();
        

        if (playerManager.totalPlayers < 4)
        {
            playerManager.activePlayers++;
            playerManager.totalPlayers++;
            DontDestroyOnLoad(this.gameObject);

            if (playerManager.overrideIndex != -1)
            {
                switch(playerManager.overrideIndex + 1)
                {
                    case 1:
                        characterSelect = GameObject.Find("P1CharacterSelect");
                        break;
                    case 2:
                        characterSelect = GameObject.Find("P2CharacterSelect");
                        break;
                    case 3:
                        characterSelect = GameObject.Find("P3CharacterSelect");
                        break;
                    case 4:
                        characterSelect = GameObject.Find("P4CharacterSelect");
                        break;
                }

                playerManager.overrideIndex = -1;
            }
            else
            {

                switch (playerManager.totalPlayers)
                {
                    case 1:
                        characterSelect = GameObject.Find("P1CharacterSelect");
                        break;
                    case 2:
                        characterSelect = GameObject.Find("P2CharacterSelect");
                        break;
                    case 3:
                        characterSelect = GameObject.Find("P3CharacterSelect");
                        break;
                    case 4:
                        characterSelect = GameObject.Find("P4CharacterSelect");
                        break;
                }
            }
            defaultX = characterSelect.transform.Find("Character").Find("Color Bars").Find("Red").transform.position.x;

            GenerateCharacterSelect(characterSelect, paletteColors);
        } else
        {
            Destroy(this.gameObject);
        }

    }

    void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (!escapeBuffer)
            {
                if (escape)
                {
                    SceneManager.LoadScene(0);
                }
            }
        } else if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Destroy(this.gameObject);
        }

        

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (changeCooldown <= 0)
            {
                if (selectingState != 1)
                {
                    if (p1Move.x > 0)
                    {
                        if (p1ColorSelectIndex == 3 || p1ColorSelectIndex == 7)
                        {
                            p1ColorSelectIndex -= 3;
                        }
                        else
                        {
                            p1ColorSelectIndex += 1;
                        }

                        changeCooldown = 0.15f;
                        GenerateSprite(p1ColorSelectIndex, characterSelect, paletteColors);


                    }
                    else if (p1Move.x < 0)
                    {
                        if (p1ColorSelectIndex == 0 || p1ColorSelectIndex == 4)
                        {
                            p1ColorSelectIndex += 3;
                        }
                        else
                        {
                            p1ColorSelectIndex -= 1;
                        }

                        changeCooldown = 0.15f;
                        GenerateSprite(p1ColorSelectIndex, characterSelect, paletteColors);


                    }

                    if (p1Move.y > 0 || p1Move.y < 0)
                    {
                        if (p1ColorSelectIndex >= 4)
                        {
                            p1ColorSelectIndex -= 4;
                        }
                        else
                        {
                            p1ColorSelectIndex += 4;
                        }

                        changeCooldown = 0.15f;
                        GenerateSprite(p1ColorSelectIndex, characterSelect, paletteColors);


                    }
                }
            } else
            {
                changeCooldown -= Time.deltaTime;
            }

            if (changeCooldown2 <= 0)
            {
                if (selectingState2 != 1)
                {
                    if (p2Move.x > 0)
                    {
                        if (p2ColorSelectIndex == 3 || p2ColorSelectIndex == 7)
                        {
                            p2ColorSelectIndex -= 3;
                        }
                        else
                        {
                            p2ColorSelectIndex += 1;
                        }

                        changeCooldown2 = 0.15f;
                        GenerateSprite(p2ColorSelectIndex, character2Select, paletteColors2);


                    }
                    else if (p2Move.x < 0)
                    {
                        if (p2ColorSelectIndex == 0 || p2ColorSelectIndex == 4)
                        {
                            p2ColorSelectIndex += 3;
                        }
                        else
                        {
                            p2ColorSelectIndex -= 1;
                        }

                        changeCooldown2 = 0.15f;
                        GenerateSprite(p2ColorSelectIndex, character2Select, paletteColors2);


                    }

                    if (p2Move.y > 0 || p2Move.y < 0)
                    {
                        if (p2ColorSelectIndex >= 4)
                        {
                            p2ColorSelectIndex -= 4;
                        }
                        else
                        {
                            p2ColorSelectIndex += 4;
                        }

                        changeCooldown2 = 0.15f;
                        GenerateSprite(p2ColorSelectIndex, character2Select, paletteColors2);


                    }
                }
            }
            else
            {
                changeCooldown2 -= Time.deltaTime;
            }

            if (p1Jump)
            {
                if (!jumpBuffer)
                {
                    jumpBuffer = true;

                    switch (selectingState)
                    {
                        case 0:
                            if (p1ColorSelectIndex == 7)
                            {
                                p1ColorSelectIndex = 0;
                                GenerateCharacterSelect(characterSelect, paletteColors);
                            }
                            else
                            {
                                characterSelect.transform.Find("Colors").gameObject.SetActive(false);
                                characterSelect.transform.Find("Player Tag").gameObject.SetActive(true);

                                float H, S, V;
                                Color.RGBToHSV((paletteColors[p1ColorSelectIndex].clothesL + paletteColors[p1ColorSelectIndex].bodyL + paletteColors[p1ColorSelectIndex].hairL) / 3, out H, out S, out V);
                                if (V < 0.5f) V += 0.4f;

                                characterSelect.transform.Find("Player Tag").gameObject.GetComponent<SpriteRenderer>().color = Color.HSVToRGB(H, S, V);

                                selectingState = 1;
                                playerManager.activePlayers--;
                            }
                            break;
                        case 1:
                            if(playerManager.activePlayers == 0)
                            {
                                SceneManager.LoadScene(2);
                            }
                            break;
                        default:
                            break;
                    }
                }
            } else
            {
                jumpBuffer = false;
            }

            if (p2Jump)
            {
                if (!jumpBuffer2)
                {
                    jumpBuffer2 = true;

                    switch (selectingState2)
                    {
                        case 0:
                            if (p2ColorSelectIndex == 7)
                            {
                                p2ColorSelectIndex = 0;
                                GenerateCharacterSelect(character2Select, paletteColors2);
                            }
                            else
                            {
                                character2Select.transform.Find("Colors").gameObject.SetActive(false);
                                character2Select.transform.Find("Player Tag").gameObject.SetActive(true);

                                float H, S, V;
                                Color.RGBToHSV((paletteColors2[p2ColorSelectIndex].clothesL + paletteColors2[p2ColorSelectIndex].bodyL + paletteColors2[p2ColorSelectIndex].hairL) / 3, out H, out S, out V);
                                V = 0.8f;

                                character2Select.transform.Find("Player Tag").gameObject.GetComponent<SpriteRenderer>().color = Color.HSVToRGB(H, S, V);

                                selectingState2 = 1;
                                playerManager.activePlayers--;
                            }
                            break;
                        case 1:
                            if (playerManager.activePlayers == 0)
                            {
                                SceneManager.LoadScene(2);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                jumpBuffer2 = false;
            }

            if (p1Attack)
            {
                if (!attackBuffer)
                {
                    attackBuffer = true;

                    switch (selectingState)
                    {
                        case 1:
                            characterSelect.transform.Find("Colors").gameObject.SetActive(true);
                            characterSelect.transform.Find("Player Tag").gameObject.SetActive(false);

                            selectingState = 0;
                            playerManager.activePlayers++;
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                attackBuffer = false;
            }

            if (p2Attack)
            {
                if (!attackBuffer2)
                {
                    attackBuffer2 = true;

                    switch (selectingState2)
                    {
                        case 1:
                            character2Select.transform.Find("Colors").gameObject.SetActive(true);
                            character2Select.transform.Find("Player Tag").gameObject.SetActive(false);

                            selectingState2 = 0;
                            playerManager.activePlayers++;
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                attackBuffer2 = false;
            }

            if (p1Dash)
            {
                if (!splitKeyboard)
                {
                    if (playerManager.totalPlayers < 4)
                    {
                        if (playerInput.currentControlScheme == "Keyboard&Mouse")
                        {
                            splitKeyboard = true;
                            p1Dash = false;

                            playerManager.activePlayers++;
                            playerManager.totalPlayers++;
                            splitIndex = playerManager.totalPlayers - 1;
                            characterSelect.transform.Find("Controls").gameObject.GetComponent<SpriteRenderer>().sprite = controlsSprites[1];

                            if (playerManager.overrideIndex != -1)
                            {
                                switch (playerManager.overrideIndex + 1)
                                {
                                    case 1:
                                        character2Select = GameObject.Find("P1CharacterSelect");
                                        break;
                                    case 2:
                                        character2Select = GameObject.Find("P2CharacterSelect");
                                        break;
                                    case 3:
                                        character2Select = GameObject.Find("P3CharacterSelect");
                                        break;
                                    case 4:
                                        character2Select = GameObject.Find("P4CharacterSelect");
                                        break;
                                }

                                playerManager.overrideIndex = -1;
                            } else
                            {
                                switch (playerManager.totalPlayers)
                                {
                                    case 1:
                                        character2Select = GameObject.Find("P1CharacterSelect");
                                        break;
                                    case 2:
                                        character2Select = GameObject.Find("P2CharacterSelect");
                                        break;
                                    case 3:
                                        character2Select = GameObject.Find("P3CharacterSelect");
                                        break;
                                    case 4:
                                        character2Select = GameObject.Find("P4CharacterSelect");
                                        break;
                                }
                            }

                            if(defaultX2 == 0)
                            {
                                defaultX2 = character2Select.transform.Find("Character").Find("Color Bars").Find("Red").transform.position.x;
                            }
                         

                            GenerateCharacterSelect(character2Select, paletteColors2);
                        }
                    }
                }
            }

            if (p1Fireball || p2Fireball)
            {
                if (splitKeyboard)
                {
                    splitKeyboard = false;
                    p2Fireball = false;
                    p1Fireball = false;

                    if(selectingState2 != 1)
                    {
                        playerManager.activePlayers--;
                    }
                    selectingState2 = 0;
                    playerManager.totalPlayers--;

                    character2Select.transform.Find("Press Text").gameObject.SetActive(true);
                    character2Select.transform.Find("Character").gameObject.SetActive(false);
                    character2Select.transform.Find("Colors").gameObject.SetActive(false);

                    character2Select.transform.Find("Controls").gameObject.SetActive(false);
                    character2Select.transform.Find("Player Tag").gameObject.SetActive(false);

                    characterSelect.transform.Find("Controls").gameObject.GetComponent<SpriteRenderer>().sprite = controlsSprites[0];

                    playerManager.overrideIndex = splitIndex;
                }
            }
        } else if (inGame)
        {
            float shiftingAmount = 0.005f;
            RunManager runManager = GameObject.Find("Run Manager").GetComponent<RunManager>();

            if (runManager.stationNum != 1)
            {
                float red = paletteColors[p1ColorSelectIndex].hairL.r;
                float green = paletteColors[p1ColorSelectIndex].hairL.g;
                float blue = paletteColors[p1ColorSelectIndex].hairL.b;

                if (runManager.currentStation.targetRed == 1f)
                {
                    red += shiftingAmount * Time.deltaTime;
                }
                else
                {
                    red -= shiftingAmount * Time.deltaTime;
                }

                if (runManager.currentStation.targetBlue == 1f)
                {
                    blue += shiftingAmount * Time.deltaTime;
                }
                else
                {
                    blue -= shiftingAmount * Time.deltaTime;
                }

                if (runManager.currentStation.targetGreen == 1f)
                {
                    green += shiftingAmount * Time.deltaTime;
                }
                else
                {
                    green -= shiftingAmount * Time.deltaTime;
                }

                red = Mathf.Clamp(red, 0, 1);
                green = Mathf.Clamp(green, 0, 1);
                blue = Mathf.Clamp(blue, 0, 1);

                float H, S, V;
                Color.RGBToHSV(new Color(red, green, blue, 1.0F), out H, out S, out V);


                int direction;
                if (H >= 0.16666666666f && H <= 0.72222222222) direction = 1;
                else direction = -1;

                paletteColors[p1ColorSelectIndex].hairM = Color.HSVToRGB(H + (0.05f * direction), S - 0.05f, V - 0.1f);
                paletteColors[p1ColorSelectIndex].hairD = Color.HSVToRGB(H + (0.1f * direction), S - 0.1f, V - 0.2f);
                paletteColors[p1ColorSelectIndex].hairE = Color.HSVToRGB(H + (0.15f * direction), S - 0.15f, V - 0.3f);

                paletteColors[p1ColorSelectIndex].hairL = new Color(red, green, blue);

                red = paletteColors[p1ColorSelectIndex].bodyL.r;
                green = paletteColors[p1ColorSelectIndex].bodyL.g;
                blue = paletteColors[p1ColorSelectIndex].bodyL.b;

                if (runManager.currentStation.targetRed == 1f)
                {
                    red += shiftingAmount * Time.deltaTime;
                }
                else
                {
                    red -= shiftingAmount * Time.deltaTime;
                }

                if (runManager.currentStation.targetBlue == 1f)
                {
                    blue += shiftingAmount * Time.deltaTime;
                }
                else
                {
                    blue -= shiftingAmount * Time.deltaTime;
                }

                if (runManager.currentStation.targetGreen == 1f)
                {
                    green += shiftingAmount * Time.deltaTime;
                }
                else
                {
                    green -= shiftingAmount * Time.deltaTime;
                }

                red = Mathf.Clamp(red, 0, 1);
                green = Mathf.Clamp(green, 0, 1);
                blue = Mathf.Clamp(blue, 0, 1);

                Color.RGBToHSV(new Color(red, green, blue, 1.0F), out H, out S, out V);

                if (H >= 0.16666666666f && H <= 0.72222222222) direction = 1;
                else direction = -1;

                paletteColors[p1ColorSelectIndex].bodyM = Color.HSVToRGB(H + (0.05f * direction), S - 0.05f, V - 0.1f);
                paletteColors[p1ColorSelectIndex].bodyD = Color.HSVToRGB(H + (0.1f * direction), S - 0.1f, V - 0.2f);

                paletteColors[p1ColorSelectIndex].bodyL = new Color(red, green, blue);



                red = paletteColors[p1ColorSelectIndex].clothesL.r;
                green = paletteColors[p1ColorSelectIndex].clothesL.g;
                blue = paletteColors[p1ColorSelectIndex].clothesL.b;

                if (runManager.currentStation.targetRed == 1f)
                {
                    red += shiftingAmount * Time.deltaTime;
                }
                else
                {
                    red -= shiftingAmount * Time.deltaTime;
                }

                if (runManager.currentStation.targetBlue == 1f)
                {
                    blue += shiftingAmount * Time.deltaTime;
                }
                else
                {
                    blue -= shiftingAmount * Time.deltaTime;
                }

                if (runManager.currentStation.targetGreen == 1f)
                {
                    green += shiftingAmount * Time.deltaTime;
                }
                else
                {
                    green -= shiftingAmount * Time.deltaTime;
                }

                red = Mathf.Clamp(red, 0, 1);
                green = Mathf.Clamp(green, 0, 1);
                blue = Mathf.Clamp(blue, 0, 1);

                Color.RGBToHSV(new Color(red, green, blue, 1.0F), out H, out S, out V);

                if (H >= 0.16666666666f && H <= 0.72222222222) direction = 1;
                else direction = -1;

                paletteColors[p1ColorSelectIndex].clothesM = Color.HSVToRGB(H + (0.05f * direction), S - 0.05f, V - 0.1f);
                paletteColors[p1ColorSelectIndex].clothesD = Color.HSVToRGB(H + (0.1f * direction), S - 0.1f, V - 0.2f);
                paletteColors[p1ColorSelectIndex].clothesDD = Color.HSVToRGB(H + (0.15f * direction), S - 0.15f, V - 0.3f);
                paletteColors[p1ColorSelectIndex].clothesDDD = Color.HSVToRGB(H + (0.2f * direction), S - 0.2f, V - 0.4f);

                paletteColors[p1ColorSelectIndex].clothesL = new Color(red, green, blue);

                GenerateRunningSprite(playerObject, paletteColors[p1ColorSelectIndex]);





                if (splitKeyboard)
                {
                    red = paletteColors2[p2ColorSelectIndex].hairL.r;
                    green = paletteColors2[p2ColorSelectIndex].hairL.g;
                    blue = paletteColors2[p2ColorSelectIndex].hairL.b;

                    if (runManager.currentStation.targetRed == 1f)
                    {
                        red += shiftingAmount * Time.deltaTime;
                    }
                    else
                    {
                        red -= shiftingAmount * Time.deltaTime;
                    }

                    if (runManager.currentStation.targetBlue == 1f)
                    {
                        blue += shiftingAmount * Time.deltaTime;
                    }
                    else
                    {
                        blue -= shiftingAmount * Time.deltaTime;
                    }

                    if (runManager.currentStation.targetGreen == 1f)
                    {
                        green += shiftingAmount * Time.deltaTime;
                    }
                    else
                    {
                        green -= shiftingAmount * Time.deltaTime;
                    }

                    red = Mathf.Clamp(red, 0, 1);
                    green = Mathf.Clamp(green, 0, 1);
                    blue = Mathf.Clamp(blue, 0, 1);

                    Color.RGBToHSV(new Color(red, green, blue, 1.0F), out H, out S, out V);

                    if (H >= 0.16666666666f && H <= 0.72222222222) direction = 1;
                    else direction = -1;

                    paletteColors2[p2ColorSelectIndex].hairM = Color.HSVToRGB(H + (0.05f * direction), S - 0.05f, V - 0.1f);
                    paletteColors2[p2ColorSelectIndex].hairD = Color.HSVToRGB(H + (0.1f * direction), S - 0.1f, V - 0.2f);
                    paletteColors2[p2ColorSelectIndex].hairE = Color.HSVToRGB(H + (0.15f * direction), S - 0.15f, V - 0.3f);

                    paletteColors2[p2ColorSelectIndex].hairL = new Color(red, green, blue);

                    red = paletteColors2[p2ColorSelectIndex].bodyL.r;
                    green = paletteColors2[p2ColorSelectIndex].bodyL.g;
                    blue = paletteColors2[p2ColorSelectIndex].bodyL.b;

                    if (runManager.currentStation.targetRed == 1f)
                    {
                        red += shiftingAmount * Time.deltaTime;
                    }
                    else
                    {
                        red -= shiftingAmount * Time.deltaTime;
                    }

                    if (runManager.currentStation.targetBlue == 1f)
                    {
                        blue += shiftingAmount * Time.deltaTime;
                    }
                    else
                    {
                        blue -= shiftingAmount * Time.deltaTime;
                    }

                    if (runManager.currentStation.targetGreen == 1f)
                    {
                        green += shiftingAmount * Time.deltaTime;
                    }
                    else
                    {
                        green -= shiftingAmount * Time.deltaTime;
                    }

                    red = Mathf.Clamp(red, 0, 1);
                    green = Mathf.Clamp(green, 0, 1);
                    blue = Mathf.Clamp(blue, 0, 1);

                    Color.RGBToHSV(new Color(red, green, blue, 1.0F), out H, out S, out V);

                    if (H >= 0.16666666666f && H <= 0.72222222222) direction = 1;
                    else direction = -1;

                    paletteColors2[p2ColorSelectIndex].bodyM = Color.HSVToRGB(H + (0.05f * direction), S - 0.05f, V - 0.1f);
                    paletteColors2[p2ColorSelectIndex].bodyD = Color.HSVToRGB(H + (0.1f * direction), S - 0.1f, V - 0.2f);

                    paletteColors2[p2ColorSelectIndex].bodyL = new Color(red, green, blue);



                    red = paletteColors2[p2ColorSelectIndex].clothesL.r;
                    green = paletteColors2[p2ColorSelectIndex].clothesL.g;
                    blue = paletteColors2[p2ColorSelectIndex].clothesL.b;

                    if (runManager.currentStation.targetRed == 1f)
                    {
                        red += shiftingAmount * Time.deltaTime;
                    }
                    else
                    {
                        red -= shiftingAmount * Time.deltaTime;
                    }

                    if (runManager.currentStation.targetBlue == 1f)
                    {
                        blue += shiftingAmount * Time.deltaTime;
                    }
                    else
                    {
                        blue -= shiftingAmount * Time.deltaTime;
                    }

                    if (runManager.currentStation.targetGreen == 1f)
                    {
                        green += shiftingAmount * Time.deltaTime;
                    }
                    else
                    {
                        green -= shiftingAmount * Time.deltaTime;
                    }

                    red = Mathf.Clamp(red, 0, 1);
                    green = Mathf.Clamp(green, 0, 1);
                    blue = Mathf.Clamp(blue, 0, 1);

                    Color.RGBToHSV(new Color(red, green, blue, 1.0F), out H, out S, out V);

                    if (H >= 0.16666666666f && H <= 0.72222222222) direction = 1;
                    else direction = -1;

                    paletteColors2[p2ColorSelectIndex].clothesM = Color.HSVToRGB(H + (0.05f * direction), S - 0.05f, V - 0.1f);
                    paletteColors2[p2ColorSelectIndex].clothesD = Color.HSVToRGB(H + (0.1f * direction), S - 0.1f, V - 0.2f);
                    paletteColors2[p2ColorSelectIndex].clothesDD = Color.HSVToRGB(H + (0.15f * direction), S - 0.15f, V - 0.3f);
                    paletteColors2[p2ColorSelectIndex].clothesDDD = Color.HSVToRGB(H + (0.2f * direction), S - 0.2f, V - 0.4f);

                    paletteColors2[p2ColorSelectIndex].clothesL = new Color(red, green, blue);

                    GenerateRunningSprite(playerObject2, paletteColors2[p2ColorSelectIndex]);
                }
            }

            //playerObject.GetComponent<Rigidbody2D>().MovePosition(playerObject.transform.position + Vector3.right * p1Move.x * Time.deltaTime * 1 * 1);

            if (splitKeyboard)
            {
                playerObject2.GetComponent<Rigidbody2D>().MovePosition(playerObject2.transform.position + Vector3.right * p2Move.x * Time.deltaTime * 1 * 1);
            }

            //Moving
            var direction = new Vector3(inputHandler.MovementInput, 0f);
            playerObject.GetComponent<Rigidbody2D>().AddForce(direction * movementSpeed);

            if (playerObject.GetComponent<Rigidbody2D>().velocity.x > maxMovementSpeed)
            {
                playerObject.GetComponent<Rigidbody2D>().velocity = new Vector2(maxMovementSpeed, playerObject.GetComponent<Rigidbody2D>().velocity.y);
            }
            if (playerObject.GetComponent<Rigidbody2D>().velocity.x < -maxMovementSpeed)
            {
                playerObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-maxMovementSpeed, playerObject.GetComponent<Rigidbody2D>().velocity.y);
            }

            //JUMPING [>A
            jumpCoyoteTimer -= Time.deltaTime;
            preJumpTimer -= Time.deltaTime;

            if (jumpTimer > 0)
                if (waitJump == true)
                    jumpTimer = 0;
                else
                    jumpTimer -= Time.deltaTime;
            else if (jumpTimer < 0)
            {
                waitJump = true;
                jumpTimer = 0;
            }

            if (!playerObject.GetComponent<Collision>().onGround)
            {
                waitJump = true;
            }

            if (playerObject.GetComponent<Collision>().onGround)
            {
                jumpCoyoteTimer = 0.1f;
            }

            if (p1Jump)
            {
                preJumpTimer = 0.1f;
            }

            if (preJumpTimer > 0 && jumpCoyoteTimer > 0 && waitJump)
            {
                jumpCoyoteTimer = 0;
                preJumpTimer = 0;
                waitJump = false;
                jumpTimer = 0.30f;
                playerObject.GetComponent<Rigidbody2D>().velocity = new Vector2(playerObject.GetComponent<Rigidbody2D>().velocity.x, 0);
                playerObject.GetComponent<Rigidbody2D>().velocity += Vector2.up * jumpSpeed;

                
            }

            float fallMultiplier = 2.5f;
            float lowJumpMultiplier = 2f;

            if (playerObject.GetComponent<Rigidbody2D>().velocity.y< 0)
            {
                playerObject.GetComponent<Rigidbody2D>().velocity += Vector2.up* Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
            else if (playerObject.GetComponent<Rigidbody2D>().velocity.y > 0 && !p1Jump)
            {
                playerObject.GetComponent<Rigidbody2D>().velocity += Vector2.up* Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }

             Debug.Log(playerObject.GetComponent<Rigidbody2D>().velocity);


        }
    }

    public void GenerateCharacterSelect(GameObject characterSelect, List<PaletteColor> paletteColors)
    { 
        characterSelect.transform.Find("Press Text").gameObject.SetActive(false);
        characterSelect.transform.Find("Character").gameObject.SetActive(true);
        characterSelect.transform.Find("Colors").gameObject.SetActive(true);
        paletteColors.Clear();

        for (int i = 0; i < 7; i++)
        {
            PaletteColor newColor = new PaletteColor();

            float[] colorValues = new float[3];

            // HAIR
            colorValues[0] = Random.Range(0.7f, 1f);
            colorValues[1] = Random.Range(0, 1f);
            colorValues[2] = Random.Range(0, 1f);

            newColor.hairL = Color.HSVToRGB(colorValues[0], colorValues[1], colorValues[2]);

            int direction;
            if (colorValues[0] >= 0.16666666666f && colorValues[0] <= 0.72222222222) direction = 1;
            else direction = -1;

            newColor.hairM = Color.HSVToRGB(colorValues[0] + (0.05f * direction), colorValues[1] - 0.05f, colorValues[2] - 0.1f);
            newColor.hairD = Color.HSVToRGB(colorValues[0] + (0.1f * direction), colorValues[1] - 0.1f, colorValues[2] - 0.2f);
            newColor.hairE = Color.HSVToRGB(colorValues[0] + (0.15f * direction), colorValues[1] - 0.15f, colorValues[2] - 0.3f);

            // BODY
            colorValues[0] = Random.Range(0, 1f);
            colorValues[1] = Random.Range(0.25f, 1f);
            colorValues[2] = Random.Range(0.5f, 1f);

            newColor.bodyL = Color.HSVToRGB(colorValues[0], colorValues[1], colorValues[2]);

            if (colorValues[0] >= 0.16666666666f && colorValues[0] <= 0.72222222222f) direction = 1;
            else direction = -1;

            newColor.bodyM = Color.HSVToRGB(colorValues[0] + (0.05f * direction), colorValues[1] - 0.05f, colorValues[2] - 0.1f);
            newColor.bodyD = Color.HSVToRGB(colorValues[0] + (0.1f * direction), colorValues[1] - 0.1f, colorValues[2] - 0.2f);

            // Clothes
            colorValues[0] = Random.Range(0, 1f);
            colorValues[1] = Random.Range(0, 1f);
            colorValues[2] = Random.Range(0, 1f);

            newColor.clothesL = Color.HSVToRGB(colorValues[0], colorValues[1], colorValues[2]);

            if (colorValues[0] >= 0.16666666666f && colorValues[0] <= 0.72222222222f) direction = 1;
            else direction = -1;

            newColor.clothesM = Color.HSVToRGB(colorValues[0] + (0.05f * direction), colorValues[1] - 0.05f, colorValues[2] - 0.1f);
            newColor.clothesD = Color.HSVToRGB(colorValues[0] + (0.1f * direction), colorValues[1] - 0.1f, colorValues[2] - 0.2f);
            newColor.clothesDD = Color.HSVToRGB(colorValues[0] + (0.15f * direction), colorValues[1] - 0.15f, colorValues[2] - 0.3f);
            newColor.clothesDDD = Color.HSVToRGB(colorValues[0] + (0.2f * direction), colorValues[1] - 0.2f, colorValues[2] - 0.4f);

            // Misc
            colorValues[0] = Random.Range(0, 1f);
            colorValues[1] = Random.Range(0, 1f);
            colorValues[2] = Random.Range(0, 1f);

            newColor.miscL = Color.HSVToRGB(colorValues[0], colorValues[1], colorValues[2]);

            if (colorValues[0] >= 0.16666666666f && colorValues[0] <= 0.72222222222f) direction = 1;
            else direction = -1;

            newColor.miscD = Color.HSVToRGB(colorValues[0] + (0.05f * direction), colorValues[1] - 0.05f, colorValues[2] - 0.1f);

            paletteColors.Add(newColor);
        }

        PaletteColor customColor = new PaletteColor();
        customColor.hairL = Color.black;
        customColor.hairM = Color.black;
        customColor.hairD = Color.black;
        customColor.hairE = Color.black;
        customColor.bodyL = Color.black;
        customColor.bodyM = Color.black;
        customColor.bodyD = Color.black;
        customColor.clothesL = Color.black;
        customColor.clothesM = Color.black;
        customColor.clothesD = Color.black;
        customColor.clothesDD = Color.black;
        customColor.miscL = Color.black;
        customColor.miscD = Color.black;

        paletteColors.Add(customColor);

        characterSelect.transform.Find("Colors").Find("Square (6)").gameObject.GetComponent<SpriteRenderer>().color = (paletteColors[0].clothesL + paletteColors[0].bodyL + paletteColors[0].hairL) / 3;
        characterSelect.transform.Find("Colors").Find("Square (5)").gameObject.GetComponent<SpriteRenderer>().color = (paletteColors[1].clothesL + paletteColors[1].bodyL + paletteColors[1].hairL) / 3;
        characterSelect.transform.Find("Colors").Find("Square (4)").gameObject.GetComponent<SpriteRenderer>().color = (paletteColors[2].clothesL + paletteColors[2].bodyL + paletteColors[2].hairL) / 3;
        characterSelect.transform.Find("Colors").Find("Square (7)").gameObject.GetComponent<SpriteRenderer>().color = (paletteColors[3].clothesL + paletteColors[3].bodyL + paletteColors[3].hairL) / 3;

        characterSelect.transform.Find("Colors").Find("Square (2)").gameObject.GetComponent<SpriteRenderer>().color = (paletteColors[4].clothesL + paletteColors[4].bodyL + paletteColors[4].hairL) / 3;
        characterSelect.transform.Find("Colors").Find("Square (1)").gameObject.GetComponent<SpriteRenderer>().color = (paletteColors[5].clothesL + paletteColors[5].bodyL + paletteColors[5].hairL) / 3;
        characterSelect.transform.Find("Colors").Find("Square").gameObject.GetComponent<SpriteRenderer>().color = (paletteColors[6].clothesL + paletteColors[6].bodyL + paletteColors[6].hairL) / 3;
        characterSelect.transform.Find("Colors").Find("Square (3)").gameObject.GetComponent<SpriteRenderer>().color = Color.black;

        GenerateSprite(0, characterSelect, paletteColors);
    }

    public void GenerateSprite(int index, GameObject characterSelect, List<PaletteColor> paletteColors)
    {
        characterSelect.transform.Find("Controls").gameObject.SetActive(true);

        if (characterSelect == character2Select)
        {
            if(p2ColorSelectIndex == 7)
            {
                characterSelect.transform.Find("Refresh").gameObject.SetActive(true);
            }
            else
            {
                characterSelect.transform.Find("Refresh").gameObject.SetActive(false);
            }

            characterSelect.transform.Find("Controls").gameObject.GetComponent<SpriteRenderer>().sprite = controlsSprites[2];
        } 
        else
        {
            if (p1ColorSelectIndex == 7)
            {
                characterSelect.transform.Find("Refresh").gameObject.SetActive(true);
            }
            else
            {
                characterSelect.transform.Find("Refresh").gameObject.SetActive(false);
            }

            if (playerInput.currentControlScheme == "Keyboard&Mouse")
            {
                if (splitKeyboard)
                {
                    characterSelect.transform.Find("Controls").gameObject.GetComponent<SpriteRenderer>().sprite = controlsSprites[1];
                }
                else
                {
                    characterSelect.transform.Find("Controls").gameObject.GetComponent<SpriteRenderer>().sprite = controlsSprites[0];
                }
            } 
            else
            {
                characterSelect.transform.Find("Controls").gameObject.GetComponent<SpriteRenderer>().sprite = controlsSprites[3];
            }
        }

        Color tileColor = (paletteColors[index].clothesL + paletteColors[index].bodyL + paletteColors[index].hairL) / 3;
        Transform red = characterSelect.transform.Find("Character").Find("Color Bars").Find("Red");
        Transform blue = characterSelect.transform.Find("Character").Find("Color Bars").Find("Blue");
        Transform green = characterSelect.transform.Find("Character").Find("Color Bars").Find("Green");

        if(characterSelect == character2Select)
        {
            red.position = new Vector3(defaultX2 + tileColor.r / 6.5f - 0.05f, red.transform.position.y, red.transform.position.z);
            red.localScale = new Vector3(tileColor.r * 30, red.transform.localScale.y, red.transform.localScale.z);

            blue.position = new Vector3(defaultX2 + tileColor.b / 6.5f - 0.05f, blue.transform.position.y, blue.transform.position.z);
            blue.localScale = new Vector3(tileColor.b * 30, blue.transform.localScale.y, blue.transform.localScale.z);

            green.position = new Vector3(defaultX2 + tileColor.g / 6.5f - 0.05f, green.transform.position.y, green.transform.position.z);
            green.localScale = new Vector3(tileColor.g * 30, green.transform.localScale.y, green.transform.localScale.z);
        }
        else
        {
            red.position = new Vector3(defaultX + tileColor.r / 6.5f - 0.05f, red.transform.position.y, red.transform.position.z);
            red.localScale = new Vector3(tileColor.r * 30, red.transform.localScale.y, red.transform.localScale.z);

            blue.position = new Vector3(defaultX + tileColor.b / 6.5f - 0.05f, blue.transform.position.y, blue.transform.position.z);
            blue.localScale = new Vector3(tileColor.b * 30, blue.transform.localScale.y, blue.transform.localScale.z);

            green.position = new Vector3(defaultX + tileColor.g / 6.5f - 0.05f, green.transform.position.y, green.transform.position.z);
            green.localScale = new Vector3(tileColor.g * 30, green.transform.localScale.y, green.transform.localScale.z);
        }
        


        characterSelect.transform.Find("Character").Find("Hair L").gameObject.GetComponent<SpriteRenderer>().color = paletteColors[index].hairL;
        characterSelect.transform.Find("Character").Find("Hair M").gameObject.GetComponent<SpriteRenderer>().color = paletteColors[index].hairM;
        characterSelect.transform.Find("Character").Find("Hair D").gameObject.GetComponent<SpriteRenderer>().color = paletteColors[index].hairD;
        characterSelect.transform.Find("Character").Find("Hair Eyes").gameObject.GetComponent<SpriteRenderer>().color = paletteColors[index].hairE;

        characterSelect.transform.Find("Character").Find("Body").gameObject.GetComponent<SpriteRenderer>().color = paletteColors[index].bodyL;
        characterSelect.transform.Find("Character").Find("Body (1)").gameObject.GetComponent<SpriteRenderer>().color = paletteColors[index].bodyM;
        characterSelect.transform.Find("Character").Find("Body (2)").gameObject.GetComponent<SpriteRenderer>().color = paletteColors[index].bodyD;

        characterSelect.transform.Find("Character").Find("Clothes").gameObject.GetComponent<SpriteRenderer>().color = paletteColors[index].clothesL;
        characterSelect.transform.Find("Character").Find("Clothes (1)").gameObject.GetComponent<SpriteRenderer>().color = paletteColors[index].clothesM;
        characterSelect.transform.Find("Character").Find("Clothes (2)").gameObject.GetComponent<SpriteRenderer>().color = paletteColors[index].clothesD;
        characterSelect.transform.Find("Character").Find("Clothes (3)").gameObject.GetComponent<SpriteRenderer>().color = paletteColors[index].clothesDD;

        characterSelect.transform.Find("Character").Find("Misc").gameObject.GetComponent<SpriteRenderer>().color = paletteColors[index].miscL;
        characterSelect.transform.Find("Character").Find("Misc (1)").gameObject.GetComponent<SpriteRenderer>().color = paletteColors[index].miscD;

        int yLevel = 0;
        if(index >= 4)
        {
            yLevel = 1;
        }

        int xLevel = index;
        if (index >= 4)
        {
            xLevel -= 4;
        }

        characterSelect.transform.Find("Colors").Find("Selection").localPosition = new Vector3(-0.12f + 0.09f * xLevel, 0.33f - 0.09f * yLevel, transform.localPosition.y);
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if(scene.buildIndex == 2)
        {   
            playerObject = Instantiate(playerPrefab, new Vector3(Random.Range(-1.1f, 1.1f), -0.25f, 0), Quaternion.identity);
            GenerateRunningSprite(playerObject, paletteColors[p1ColorSelectIndex]);

            if (splitKeyboard)
            {
                playerObject2 = Instantiate(playerPrefab, new Vector3(Random.Range(-1.1f, 1.1f), -0.25f, 0), Quaternion.identity);
                GenerateRunningSprite(playerObject2, paletteColors2[p2ColorSelectIndex]);
            }

            inGame = true;
        }
    }

    void GenerateRunningSprite(GameObject player, PaletteColor palette)
    {
        player.transform.Find("Hair M").gameObject.GetComponent<SpriteRenderer>().color = palette.hairM;
        player.transform.Find("Hair D").gameObject.GetComponent<SpriteRenderer>().color = palette.hairD;
        player.transform.Find("Hair Eyes").gameObject.GetComponent<SpriteRenderer>().color = palette.hairE;

        player.transform.Find("Body").gameObject.GetComponent<SpriteRenderer>().color = palette.bodyL;
        player.transform.Find("Body (1)").gameObject.GetComponent<SpriteRenderer>().color = palette.bodyM;
        player.transform.Find("Body (2)").gameObject.GetComponent<SpriteRenderer>().color = palette.bodyD;

        player.transform.Find("Clothes").gameObject.GetComponent<SpriteRenderer>().color = palette.clothesL;
        player.transform.Find("Clothes (1)").gameObject.GetComponent<SpriteRenderer>().color = palette.clothesM;
        player.transform.Find("Clothes (2)").gameObject.GetComponent<SpriteRenderer>().color = palette.clothesD;
        player.transform.Find("Clothes (3)").gameObject.GetComponent<SpriteRenderer>().color = palette.clothesDD;
        player.transform.Find("Clothes (4)").gameObject.GetComponent<SpriteRenderer>().color = palette.clothesDDD;
    }

    [System.Serializable]
    public class PaletteColor
    {
        public Color hairL;
        public Color hairM;
        public Color hairD;
        public Color hairE;
        public Color bodyL;
        public Color bodyM;
        public Color bodyD;
        public Color clothesL;
        public Color clothesM;
        public Color clothesD;
        public Color clothesDD;
        public Color clothesDDD;
        public Color miscL;
        public Color miscD;
    }
}
