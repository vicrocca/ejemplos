using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBehaviour2 : MonoBehaviour {

    private Text thisText;
    private static int score;

    // Use this for initialization
    void Start()
    {
        thisText = GetComponent<Text>();

        score = 2;
    }

    // Update is called once per frame
    void Update()
    {

        thisText.text = "Red: " + score;
    }

    public static void RefreshScore()
    {
        score -= 1;
    }

}
