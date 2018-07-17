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


    /*
     *@reski: deberia ser bomb con minuscula por sr variable, porque sino se confunde con la Clase Bomb 
     *conviene cambiar de GameObject a Bomb asi solo se pueden poner GameObjects con el Script Bomb attacheado.
     * */
    //public GameObject Bomb;
    public Bomb Bomb;

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

        /*
         * @reski
         * agregro una condicion al if asi no pregunta al world cuando no apretamos tecla
         * */

        //if (World.IsTileFree(newPosition))
        if (directionOfMovement!= Vector3.zero && World.IsTileFree(newPosition))
        {
            World.ChangeToCero(transform.position);
            transform.position += directionOfMovement;
            World.ChangeToOne(transform.position);

        }
        /*
         * @reski: IsTile free daba siempre falso al poner bomba xq esta el jugador.
         * O lo sacan o hacen que el jugador tenga otro numero e.g.  2 
         * */
        if (Input.GetKeyUp(BombKey) /*&& World.IsTileFree(transform.position)*/)
        {
             StartCoroutine("PlaceBomb");
            /*
        * @reski:van a tener problemas con esto porque el jugador se va a ir del tile que dejaron la bomba,
        * y lo va a poner denuevo en 0. Asique es como que no hubiese nada. eso depende como quieran ustedes 
        * la jugabilidad. Si quieren que la bomba bloquee movimiento o no. 
        * */
            World.ChangeToOne(transform.position);
        }
	}

    void PlaceBomb()
    {
        GameObject Temporary_Bomb;
        Temporary_Bomb = Instantiate(Bomb.gameObject, transform.position, transform.rotation) as GameObject;
    }

       
  

   

}
