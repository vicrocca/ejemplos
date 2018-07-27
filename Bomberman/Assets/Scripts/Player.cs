using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour {

    //atributes: 3 lives
    //functions: move, place a bomb, die


    public KeyCode MoveUp;
    public KeyCode MoveDown;
    public KeyCode MoveLeft;
    public KeyCode MoveRight;
    public KeyCode BombKey;


   
    public int score;

    // To allow only "bomb" script GameObjects to be attached to the player 
    public Bomb bomb;
  
   

	// Use this for initialization
	void Start () {
        score = 2;
   

	}
	
	// Update is called once per frame
	public void Update () {
        
        Vector3 directionOfMovement = Vector3.zero;

        if (Input.GetKeyUp(MoveUp))
            directionOfMovement = Vector3.forward;
        else if (Input.GetKeyUp(MoveDown))
            directionOfMovement = Vector3.back;
        else if (Input.GetKeyUp(MoveRight))
            directionOfMovement = Vector3.right;
        else if (Input.GetKeyUp(MoveLeft))
            directionOfMovement = Vector3.left;

        Vector3 newPosition = transform.position + directionOfMovement;

       
        // directionOfMovement!= Vector3.zero is added so that when the key is not pressed the World isnt asked 
        if (directionOfMovement != Vector3.zero && World.Instance.IsTileFree(newPosition))
        {
            
            if(World.Instance.Board[(int)transform.position.x, (int)transform.position.z] != 2)
            {
                World.Instance.ChangeToCero(transform.position);
            }
            transform.position += directionOfMovement;
            World.Instance.ChangeToOne(transform.position);

        }
       
        // valor de 2 para bomba
        if (Input.GetKeyUp(BombKey)&& World.Instance.Board[(int)transform.position.x, (int)transform.position.z]!=2)
        {
            PlaceBomb(); 
            World.Instance.ChangeToTwo(transform.position);
        }
	}

    public void PlaceBomb()
    {
        GameObject Temporary_Bomb;
        Temporary_Bomb = Instantiate(bomb.gameObject, transform.position, transform.rotation) as GameObject;
    }

    public void RefreshScore()
    {
        score -= 1;
        if (score==0)
        {
            UIBehaviour.Instance.EndGame();
                        
        }
    }
}
