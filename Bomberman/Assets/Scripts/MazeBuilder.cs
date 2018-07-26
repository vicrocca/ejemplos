using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeBuilder : MonoBehaviour
{
   
    public GameObject wallBlock;
   
    // Use this for initialization
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
            BuildMaze();
    }

    private void BuildMaze()
    { 
        for (int i = 0; i <10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                
                if (World.Instance.Board[i, j] != 0)
                    //GetComponent<World>()
                    Instantiate(wallBlock, transform.position + new Vector3(i,1,j), transform.rotation);
            }
        }
    }
}
