using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class World : MonoBehaviour {


    //atributes: 2 players, grid XY, N cubes, N walls
    //functions: start game, kill player and relocate, end game

    public static World Instance;


    public  Player player1;
    public Player player2;

    public  GameObject cube1;
    public  GameObject cube2;
    public  GameObject cube3;


    public  Bomb bomb;



    public  List<GameObject> explosiveItems; //cubes



    public int[,] Board; 
   


   public int upperLimitX;
   public int lowerLimitX;
    public int upperLimitZ;
   public int lowerLimitZ;



	// Use this for initialization

	private void Awake()
	{
        if (!Instance)
            Instance = this;
	}


	public void Start () {

      
        upperLimitX = 10;
        lowerLimitX = 0;
        upperLimitZ = upperLimitX;
        lowerLimitZ = lowerLimitX;
       


        //create Board 
        Board = new int[(upperLimitX - lowerLimitX)+1, (upperLimitZ - lowerLimitZ)+1];
        for (int i = 1; i < (upperLimitX - lowerLimitX);i++)
        {
            for (int j = 1; j < (upperLimitZ - lowerLimitZ)+1; j++)
            {
                Board[i,j] = 0;
            }

        }


        //external walls
        for (int i = 0; i < (upperLimitX - lowerLimitX) +1  ;i++)
        {
            Board[lowerLimitX  + i, lowerLimitZ] = 1;
            Board[lowerLimitX  + i, upperLimitZ] = 1;
            Board[lowerLimitX  , lowerLimitZ+i] = 1;
            Board[upperLimitX, lowerLimitZ+i] = 1;
         
        }

      

        //internal walls

            //wall 1
            for (int j = 4; j < 9; j++)
            {
            Board[4, j] = 1;
            }

            //wall 2
            for (int j = 3; j < 7; j++)
            {
              
            Board[j, 4] = 1;
            }

            //wall 3
            for (int j = 1; j < 3; j++)
            {
               
            Board[2,j] = 1;
            }

            //wall 4
            for (int j = 1; j < 5; j++)
            {
          
            Board[8, j] = 1;
            }

            //wall 5
            for (int j = 7; j < 10; j++)
            {
           
            Board[7, j] = 1;
            }


        //cubes

       
        Board[(int)cube1.transform.position.x, (int)cube1.transform.position.z] = 1;
        Board[(int)cube2.transform.position.x, (int)cube2.transform.position.z] = 1;
        Board[(int)cube3.transform.position.x, (int)cube3.transform.position.z] = 1;
       

        explosiveItems.Add(cube1);
        explosiveItems.Add(cube2);
        explosiveItems.Add(cube3);

        //players

        RelocatePlayer(player1);
        RelocatePlayer(player2);

        Board[(int)player1.transform.position.x, (int)player1.transform.position.z] = 1;
        Board[(int)player2.transform.position.x, (int)player2.transform.position.z] = 1;

       

	}
	
	// Update is called once per frame
	void Update () {
        
       
		
	}


 


    public  bool IsTileFree(Vector3 newPosition)
   {
      
        if (Board[(int)newPosition.x , (int)newPosition.z ] == 0)
        {
            return true;

        }
        else
            return false;
   }
   


    public  void ChangeToOne(Vector3 Position)
    {
        Board[(int)Position.x, (int)Position.z]=1;
    }


    public  void ChangeToCero(Vector3 Position)
    {
        Board[(int)Position.x, (int)Position.z] = 0;
    }

    public  void ChangeToTwo(Vector3 Position)
    {
        Board[(int)Position.x, (int)Position.z] = 2;
    }

    public  void KillPlayer(Player player)
    {

      

        Board[(int)player.transform.position.x , (int)player.transform.position.z ] = 0;
        RelocatePlayer(player);
        player.RefreshScore();


    }

   
     public void RelocatePlayer(Player player)
    {
        player.transform.position = new Vector3(Random.Range(3, 6), 1, 2);
        while (Board[(int)player.transform.position.x, (int)player.transform.position.z] ==1)
        {
            player.transform.position = new Vector3(Random.Range(3, 6), 1, 2);
        }

        Board[(int)player.transform.position.x , (int)player.transform.position.z ] = 1;
    }
   
}
