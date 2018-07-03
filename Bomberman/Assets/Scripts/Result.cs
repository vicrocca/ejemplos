using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{

    private Text thisText;

     public static int score1;
    public static int score2;
   
 

    // Use this for initialization
    void Start()
    {
        thisText = GetComponent<Text>();
     
        score1 = 2;
        score2 = 2;
       

    }
     
    // Update is called once per frame
    void Update()
    {

        thisText.text = "            END\n" + "Violet: " + score1 + "    Red: " + score2;
      
    }

   
    public static void AddScore1()
    {
        score1 = score1 - 1;
    }

    public static void AddScore2()
    {
        score2 = score2- 1;
    }
}


