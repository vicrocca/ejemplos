using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {


    //atributes: 2 players, grid XY, N cubes, N walls
    //functions: start game, kill player and relocate, end game


    public GameObject player1;
    public GameObject player2;

    public GameObject cube1;
    public GameObject cube2;
    public GameObject cube3;

    public GameObject bomb;

 
    public List<GameObject> explosiveItems; //cubes

    public static int[,] Board; 
   

    int upperlimitX;
    int lowerlimitX;
    int upperlimitZ;
    int lowerlimitZ;

    static int half;
  

	// Use this for initialization
	void Start () {



        upperlimitX = 5;
        lowerlimitX = -5;
        upperlimitZ = upperlimitX;
        lowerlimitZ = lowerlimitX;
        half=(upperlimitX-lowerlimitX)/ 2;


        //create Board 
        Board = new int[(upperlimitX - lowerlimitX), (upperlimitZ - lowerlimitZ)];
        for (int i = 0; i < (upperlimitX - lowerlimitX);i++)
        {
            for (int j = 0; j < (upperlimitZ - lowerlimitZ); j++)
            {
                Board[i,j] = 0;

            }

        }


        //external walls
        for (int i = 0; i < (upperlimitX - lowerlimitX) ;i++)
        {
            Board[lowerlimitX + half + i, lowerlimitZ+ half] = 1;
            Board[lowerlimitX + half + i, upperlimitZ+ half-1] = 1;
            Board[lowerlimitX + half , lowerlimitZ+i+ half] = 1;
            Board[upperlimitX+ half-1, lowerlimitZ+i+ half] = 1;
         
        }

      

        //internal walls

            //wall 1
            for (int j = 4; j < 10; j++)
            {
            Board[1, j] = 1;
            }

            //wall 2
            for (int j = 2; j < 6; j++)
            {
              
            Board[6, j] = 1;
            }

            //wall 3
            for (int j = 5; j < 9; j++)
            {
               
            Board[8, j] = 1;
            }

            //wall 4
            for (int j = 2; j < 6; j++)
            {
          
            Board[2, j] = 1;
            }

            //wall 5
            for (int j = 1; j < 5; j++)
            {
           
            Board[j, 2] = 1;
            }


        //cubes

       
        Board[(int)cube1.transform.position.x+half, (int)cube1.transform.position.z+half] = 1;
        Board[(int)cube2.transform.position.x+half, (int)cube2.transform.position.z+half] = 1;
        Board[(int)cube3.transform.position.x+half, (int)cube3.transform.position.z+half] = 1;
       

        explosiveItems.Add(cube1);
        explosiveItems.Add(cube2);
        explosiveItems.Add(cube3);

        //players

        RelocatePlayer(player1);
        RelocatePlayer(player2);

        Board[(int)player1.transform.position.x+half, (int)player1.transform.position.z+half] = 1;
        Board[(int)player2.transform.position.x+half, (int)player2.transform.position.z+half] = 1;

       

	}
	
	// Update is called once per frame
	void Update () {
        
       
		
	}


 


    public static  bool IsTileFree(Vector3 newPosition)
   {
        if (Board[(int)newPosition.x+half, (int)newPosition.z+half] == 1)
        {

            return true;

        }
        else
            return false;
   }
   


    public static void ChangeToOne(Vector3 Position)
    {
        Board[(int)Position.x+half, (int)Position.z+half]=1;
    }


    public static void ChangeToCero(Vector3 Position)
    {
        Board[(int)Position.x+half, (int)Position.z+half] = 0;
    }

    public static void KillPlayer(GameObject player)
    {
        Board[(int)player.transform.position.x + half, (int)player.transform.position.z + half] = 0;
        RelocatePlayer(player);

    }

   
    static void RelocatePlayer(GameObject player)
    {
        player.transform.position = new Vector3(Random.Range(-4, 4), 1, -4);
        Board[(int)player.transform.position.x + half, (int)player.transform.position.z + half] = 1;
    }



   
}
