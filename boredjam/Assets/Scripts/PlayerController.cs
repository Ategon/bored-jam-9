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

    private GameObject characterSelect;
    private float changeCooldown = 0;
    private bool escapeBuffer = false;
    private bool jumpBuffer = true;
    private bool attackBuffer = true;

    private bool splitKeyboard = false;
    private bool escape;
    
    private Vector2 p1Move;
    private bool p1Jump;
    private bool p1Attack;
    private bool p1Fireball;
    private bool p1Dash;
    private Vector2 p2Move;
    private bool p2Jump;
    private bool p2Attack;
    private bool p2Fireball;
    private bool p2Dash;

    private int p1ColorSelectIndex;
    private int p2ColorSelectIndex;
    private List<PaletteColor> paletteColors;

    private int selectingState = 0;

    private PlayerManager playerManager;


    public void OnJump(InputAction.CallbackContext ctx)
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

    public void OnDash(InputAction.CallbackContext ctx)
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

    public void OnAttack(InputAction.CallbackContext ctx)
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

    public void OnFireball(InputAction.CallbackContext ctx)
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

    public void OnEscape(InputAction.CallbackContext ctx)
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

    public void OnMove(InputAction.CallbackContext ctx)
    {
        p1Move = ctx.ReadValue<Vector2>();
    }



    void Start()
    {

        playerManager = GameObject.Find("Player Manager").GetComponent<PlayerManager>();
        playerManager.activePlayers++;
        DontDestroyOnLoad(this.gameObject);

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

        characterSelect.transform.Find("Press Text").gameObject.SetActive(false);
        characterSelect.transform.Find("Character").gameObject.SetActive(true);
        characterSelect.transform.Find("Colors").gameObject.SetActive(true);

        for (int i = 0; i < 7; i++)
        {
            PaletteColor newColor = new PaletteColor();

            float[] colorValues = new float[3];

            // HAIR
            colorValues[0] = Random.Range(0, 1f);
            colorValues[1] = Random.Range(0, 1f);
            colorValues[2] = Random.Range(0, 1f);

            newColor.hairL = Color.HSVToRGB(colorValues[0], colorValues[1], colorValues[2]);

            int direction;
            if (colorValues[0] >= (360/360*60) && colorValues[0] <= (360 / 360 * 240)) direction = 1;
            else direction = -1;

            newColor.hairM = Color.HSVToRGB(colorValues[0] + (0.05f * direction), colorValues[1]-0.05f, colorValues[2]-0.1f);
            newColor.hairD = Color.HSVToRGB(colorValues[0] + (0.1f * direction), colorValues[1]-0.1f, colorValues[2]-0.2f);
            newColor.hairE = Color.HSVToRGB(colorValues[0] + (0.15f * direction), colorValues[1] - 0.15f, colorValues[2] - 0.3f);

            // BODY
            colorValues[0] = Random.Range(0, 1f);
            colorValues[1] = Random.Range(0.25f, 1f);
            colorValues[2] = Random.Range(0.5f, 1f);

            newColor.bodyL = Color.HSVToRGB(colorValues[0], colorValues[1], colorValues[2]);

            if (colorValues[0] >= (360 / 360 * 60) && colorValues[0] <= (360 / 360 * 240)) direction = 1;
            else direction = -1;

            newColor.bodyM = Color.HSVToRGB(colorValues[0] + (0.05f * direction), colorValues[1] - 0.05f, colorValues[2] - 0.1f);
            newColor.bodyD = Color.HSVToRGB(colorValues[0] + (0.1f * direction), colorValues[1] - 0.1f, colorValues[2] - 0.2f);

            // Clothes
            colorValues[0] = Random.Range(0, 1f);
            colorValues[1] = Random.Range(0, 1f);
            colorValues[2] = Random.Range(0, 1f);

            newColor.clothesL = Color.HSVToRGB(colorValues[0], colorValues[1], colorValues[2]);

            if (colorValues[0] >= (360 / 360 * 60) && colorValues[0] <= (360 / 360 * 240)) direction = 1;
            else direction = -1;

            newColor.clothesM = Color.HSVToRGB(colorValues[0] + (0.05f * direction), colorValues[1] - 0.05f, colorValues[2] - 0.1f);
            newColor.clothesD = Color.HSVToRGB(colorValues[0] + (0.1f * direction), colorValues[1] - 0.1f, colorValues[2] - 0.2f);
            newColor.clothesDD = Color.HSVToRGB(colorValues[0] + (0.15f * direction), colorValues[1] - 0.15f, colorValues[2] - 0.3f);

            // Misc
            colorValues[0] = Random.Range(0, 1f);
            colorValues[1] = Random.Range(0, 1f);
            colorValues[2] = Random.Range(0, 1f);

            newColor.miscL = Color.HSVToRGB(colorValues[0], colorValues[1], colorValues[2]);

            if (colorValues[0] >= (360 / 360 * 60) && colorValues[0] <= (360 / 360 * 240)) direction = 1;
            else direction = -1;

            newColor.miscD = Color.HSVToRGB(colorValues[0] + (0.05f * direction), colorValues[1] - 0.05f, colorValues[2] - 0.1f);

            paletteColors.Add(newColor);
        }

        PaletteColor customColor = new PaletteColor();
        customColor.hairL = Color.black;
        customColor.hairM = Color.black;
        customColor.hairD = Color.black;
        customColor.hairE = Color.black;

        paletteColors.Add(customColor);


        GenerateSprite(0);

        characterSelect.transform.Find("Colors").Find("Square (6)").gameObject.GetComponent<SpriteRenderer>().color = paletteColors[0].clothesL;
        characterSelect.transform.Find("Colors").Find("Square (5)").gameObject.GetComponent<SpriteRenderer>().color = paletteColors[1].clothesL;
        characterSelect.transform.Find("Colors").Find("Square (4)").gameObject.GetComponent<SpriteRenderer>().color = paletteColors[2].clothesL;
        characterSelect.transform.Find("Colors").Find("Square (7)").gameObject.GetComponent<SpriteRenderer>().color = paletteColors[3].clothesL;

        characterSelect.transform.Find("Colors").Find("Square (2)").gameObject.GetComponent<SpriteRenderer>().color = paletteColors[4].clothesL;
        characterSelect.transform.Find("Colors").Find("Square (1)").gameObject.GetComponent<SpriteRenderer>().color = paletteColors[5].clothesL;
        characterSelect.transform.Find("Colors").Find("Square").gameObject.GetComponent<SpriteRenderer>().color = paletteColors[6].clothesL;
        characterSelect.transform.Find("Colors").Find("Square (3)").gameObject.GetComponent<SpriteRenderer>().color = Color.black;
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
                    GenerateSprite(p1ColorSelectIndex);


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
                    GenerateSprite(p1ColorSelectIndex);


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
                    GenerateSprite(p1ColorSelectIndex);


                }
            }
        } else
        {
            changeCooldown -= Time.deltaTime;
        }

        if (p1Jump)
        {
            if (!jumpBuffer)
            {
                jumpBuffer = true;

                switch (selectingState)
                {
                    case 0:
                        characterSelect.transform.Find("Colors").gameObject.SetActive(false);
                        characterSelect.transform.Find("Player Tag").gameObject.SetActive(true);

                        float H, S, V;
                        Color.RGBToHSV(paletteColors[p1ColorSelectIndex].clothesL, out H, out S, out V);
                        if (V < 0.5f) V += 0.4f;

                        characterSelect.transform.Find("Player Tag").gameObject.GetComponent<SpriteRenderer>().color = Color.HSVToRGB(H, S, V);

                        selectingState = 1;
                        playerManager.activePlayers--;
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

        if (p1Fireball)
        {
            if (!splitKeyboard)
            {
                splitKeyboard = true;

                playerManager.activePlayers++;
                //add setting p2 char select
            }
        }
    }

    public void GenerateSprite(int index)
    {
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
        if(p1ColorSelectIndex >= 4)
        {
            yLevel = 1;
        }

        int xLevel = p1ColorSelectIndex;
        if (p1ColorSelectIndex >= 4)
        {
            xLevel -= 4;
        }

        characterSelect.transform.Find("Colors").Find("Selection").localPosition = new Vector3(-0.12f + 0.09f * xLevel, 0.33f - 0.09f * yLevel, transform.localPosition.y);
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
        public Color miscL;
        public Color miscD;
    }
}
