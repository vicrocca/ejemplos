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



    public GameObject Bomb;

	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
        
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

 

        if (World.IsTileFree(newPosition))
        {
            World.ChangeToCero(transform.position);
            transform.position += directionOfMovement;
            World.ChangeToOne(transform.position);

        }

        if (Input.GetKeyUp(BombKey) && World.IsTileFree(transform.position))
        {
             StartCoroutine("PlaceBomb");
        World.ChangeToOne(transform.position);
        }
	}

    void PlaceBomb()
    {
        GameObject Temporary_Bomb;
        Temporary_Bomb = Instantiate(Bomb, transform.position, transform.rotation) as GameObject;
    }

       
  

   

}
