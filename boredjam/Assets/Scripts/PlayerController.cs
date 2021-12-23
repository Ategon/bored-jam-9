using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Assignables")]
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private GameObject[] colorTiles;

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

    [SerializeField] private int p1ColorSelectIndex;
    [SerializeField] private int p2ColorSelectIndex;
    [SerializeField] private List<PaletteColor> paletteColors;

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

        characterSelect.transform.Find("Press Text").gameObject.SetActive(false);
        characterSelect.transform.Find("Character").gameObject.SetActive(true);
        characterSelect.transform.Find("Colors").gameObject.SetActive(true);

        for (int i = 0; i < 8; i++)
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

        characterSelect.transform.Find("Character").Find("Hair L").gameObject.GetComponent<SpriteRenderer>().color = paletteColors[0].hairL;
        characterSelect.transform.Find("Character").Find("Hair M").gameObject.GetComponent<SpriteRenderer>().color = paletteColors[0].hairM;
        characterSelect.transform.Find("Character").Find("Hair D").gameObject.GetComponent<SpriteRenderer>().color = paletteColors[0].hairD;
        characterSelect.transform.Find("Character").Find("Hair Eyes").gameObject.GetComponent<SpriteRenderer>().color = paletteColors[0].hairE;

        characterSelect.transform.Find("Character").Find("Body").gameObject.GetComponent<SpriteRenderer>().color = paletteColors[0].bodyL;
        characterSelect.transform.Find("Character").Find("Body (1)").gameObject.GetComponent<SpriteRenderer>().color = paletteColors[0].bodyM;
        characterSelect.transform.Find("Character").Find("Body (2)").gameObject.GetComponent<SpriteRenderer>().color = paletteColors[0].bodyD;

        characterSelect.transform.Find("Character").Find("Clothes").gameObject.GetComponent<SpriteRenderer>().color = paletteColors[0].clothesL;
        characterSelect.transform.Find("Character").Find("Clothes (1)").gameObject.GetComponent<SpriteRenderer>().color = paletteColors[0].clothesM;
        characterSelect.transform.Find("Character").Find("Clothes (2)").gameObject.GetComponent<SpriteRenderer>().color = paletteColors[0].clothesD;
        characterSelect.transform.Find("Character").Find("Clothes (3)").gameObject.GetComponent<SpriteRenderer>().color = paletteColors[0].clothesDD;

        characterSelect.transform.Find("Character").Find("Misc").gameObject.GetComponent<SpriteRenderer>().color = paletteColors[0].miscL;
        characterSelect.transform.Find("Character").Find("Misc (1)").gameObject.GetComponent<SpriteRenderer>().color = paletteColors[0].miscD;

        characterSelect.transform.Find("Colors").Find("Square (6)").gameObject.GetComponent<SpriteRenderer>().color = paletteColors[0].clothesL;
        characterSelect.transform.Find("Colors").Find("Square (5)").gameObject.GetComponent<SpriteRenderer>().color = paletteColors[1].clothesL;
        characterSelect.transform.Find("Colors").Find("Square (4)").gameObject.GetComponent<SpriteRenderer>().color = paletteColors[2].clothesL;
        characterSelect.transform.Find("Colors").Find("Square (7)").gameObject.GetComponent<SpriteRenderer>().color = paletteColors[3].clothesL;

        characterSelect.transform.Find("Colors").Find("Square (2)").gameObject.GetComponent<SpriteRenderer>().color = paletteColors[4].clothesL;
        characterSelect.transform.Find("Colors").Find("Square (1)").gameObject.GetComponent<SpriteRenderer>().color = paletteColors[5].clothesL;
        characterSelect.transform.Find("Colors").Find("Square").gameObject.GetComponent<SpriteRenderer>().color = paletteColors[6].clothesL;
        characterSelect.transform.Find("Colors").Find("Square (3)").gameObject.GetComponent<SpriteRenderer>().color = paletteColors[7].clothesL;
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
