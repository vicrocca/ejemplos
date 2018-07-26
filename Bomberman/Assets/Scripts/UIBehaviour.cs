using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBehaviour : MonoBehaviour {

    public static UIBehaviour Instance;

    public Text thisText1;
    public Text thisText2;

    public Text endText1;
    public Text endText2;

    public Player player1;
    public Player player2;

   public int scorePlayer1;
     public int scorePlayer2;


    public Canvas ending;

    public Image imageOne;
    public Image imageTwo;
    public Image imageThree;

    public Text finalText1;
    public Text finalText2;
    public Text finalText3;

    private void Awake()
    {
        if (!Instance)
            Instance = this;
    }



	// Use this for initialization
	void Start () {

        ending.enabled = false;

        imageOne.enabled = false;
        imageTwo.enabled = false;
        imageThree.enabled = false;

     

        scorePlayer1 = player1.score;
        scorePlayer2 = player2.score;

        thisText1.text = "Player1: " + scorePlayer1;
        thisText2.text=  "Player2: " + scorePlayer2;
    

	}
	
	// Update is called once per frame
	void Update () {
        scorePlayer1 = player1.score;
        scorePlayer2 = player2.score;
        thisText1.text = "Player1:   " + scorePlayer1;
        thisText2.text = "Player2: " + scorePlayer2;
	}


    public void EndGame()
    {

        ending.enabled = true;

        scorePlayer1 = player1.score;
        scorePlayer2 = player2.score;

        endText1.text = "Player1: " + scorePlayer1;
        endText2.text= "Player2: " + scorePlayer2;

        finalText1.text = Screen.Instance.scoreText[0];
        finalText2.text = Screen.Instance.scoreText[1];

     
        imageOne.sprite = Image(Screen.Instance.imageNames[0]);
        imageOne.enabled = true;

        imageTwo.sprite = Image(Screen.Instance.imageNames[1]);
        imageTwo.enabled = true;

        if (Screen.Instance.scoreText.Count == 3)
        {
            finalText3.text = Screen.Instance.scoreText[2];
            imageThree.sprite = Image(Screen.Instance.imageNames[2]);
            imageThree.enabled = true;
        }



    }

    public Sprite Image(string path)
    {
            byte[] www = System.IO.File.ReadAllBytes("/Users/PauliDagnino/Desktop/GitHub/ejemplos/Bomberman/Assets/" + path);
        Texture2D tex = new Texture2D(400, 500);
        tex.LoadImage(www);
        Sprite tempSprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
        return tempSprite;
    }


}
