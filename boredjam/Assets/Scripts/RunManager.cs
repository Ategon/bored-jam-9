using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunManager : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private float highscore;
    [SerializeField] private float distance;

    [Header("Set Attributes")]
    [SerializeField] private Station[] stations;

    [System.Serializable]
    public class Station
    {
        public string name;
        public Color color;
        public int fireballType; //0 = mini, 1 = normal, 2 = big
        public int slashType; //0 = quick short, 1 = long, 2 = heavy
        public int dashType; //0 = normal dash, 1 = invulnerable dash, 2 - quick dash, 3 - dash that goes far
        public int jumpType; //0 - normal jump, 1 = double jump
    }
}
