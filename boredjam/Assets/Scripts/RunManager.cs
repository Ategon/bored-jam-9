using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using TMPro;

public class RunManager : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private float highscore;
    [SerializeField] private float distance;
    private bool alive = true;
    public int stationNum = 1;
    private int lastStation = 0;
    private int secondlastStation = 0;
    public Station currentStation;
    [SerializeField] private Light2D globalLight;
    [SerializeField] private Light2D globalLight2;
    private float textTimer;

    [Header("Set Attributes")]
    [SerializeField] private Station[] stations;
    [SerializeField] private TextMeshProUGUI distanceText;
    [SerializeField] private TextMeshProUGUI stationName;
    [SerializeField] private TextMeshProUGUI stationNumber;

    [System.Serializable]
    public class Station
    {
        public string name;
        public Color color;
        public float targetRed;
        public float targetBlue;
        public float targetGreen;
    }

    void Start()
    {
        currentStation = stations[0];
        StartStation();
    }    

    void FixedUpdate()
    {
        if (alive)
        {
            distance += Time.deltaTime;

            if (textTimer > 0) textTimer -= Time.deltaTime;
            else if(textTimer < 0)
            {
                StartCoroutine(Fade(stationName, true));
                StartCoroutine(Fade(stationNumber, true));
                textTimer = 0;
            }

            distanceText.text = "" + distance.ToString("F1") + "m";

            if (distance > 25 * stationNum)
            {
                int newStation = lastStation;

                while(newStation == lastStation || newStation == secondlastStation)
                {
                    newStation = Random.Range(1, 6);
                }


                currentStation = stations[newStation];
                StartCoroutine(FadeBackground());
                stationNum++;
                secondlastStation = lastStation;
                lastStation = newStation;


                StartStation();

            }
        }
    }

    void StartStation()
    {
        textTimer = 3;

        stationName.text = currentStation.name;
        stationNumber.text = "Station " + stationNum;
        StartCoroutine(Fade(stationName, false));
        StartCoroutine(Fade(stationNumber, false));
    }

    IEnumerator Fade(TextMeshProUGUI text, bool fadeAway)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                text.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
        // fade from transparent to opaque
        else
        {
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                text.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
    }

    IEnumerator FadeBackground()
    {
        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            globalLight.color = Color.Lerp(stations[secondlastStation].color, currentStation.color, i);
            globalLight2.color = Color.Lerp(stations[secondlastStation].color, currentStation.color, i);
            yield return null;
        }
    }

}
