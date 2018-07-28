using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    //atributes: delay and range
    //functions: kill
   

  float bombExplosionDelay;
     int bombExplosionDistance;
    float screenDelay;
     int screenA;
    int screenB;

   
	// Use this for initialization
	 void Start () {

        screenA = 500;
        screenB = 400;
        screenDelay = 1.0f;
        bombExplosionDistance = 3;
        bombExplosionDelay = 3.0f;
        StartCoroutine("Explode");

		
	}
	
	

	void Update () {

    }

    //OLD IENUMERATOR
    /*
    IEnumerator   Explode()
    {

        yield return new WaitForSeconds(bombExplosionDelay);
       
      
      
        for (int i = 0; i < World.Instance.explosiveItems.Count; i++)
        {
            if (Mathf.Abs(World.Instance.explosiveItems[i].transform.position.x - transform.position.x) < bombExplosionDistance
                && Mathf.Abs(World.Instance.explosiveItems[i].transform.position.z - transform.position.z) < bombExplosionDistance)
            {

               
                World.Instance.ChangeToCero(World.Instance.explosiveItems[i].transform.position);
                // World.Instance.explosiveItems.Remove(World.Instance.explosiveItems[i]);
                //  Destroy(World.Instance.explosiveItems[i]);
               
                World.Instance.explosiveItems[i].SetActive(false);
            }
           
           
          

        }


        if ((Mathf.Abs(World.Instance.player1.transform.position.x - transform.position.x) < bombExplosionDistance)
            && Mathf.Abs(World.Instance.player1.transform.position.z - transform.position.z) < bombExplosionDistance)
        {

            Screen.TakeScreenshot_Static(500, 400);
            yield return new WaitForSeconds(1.0f);
            World.Instance.KillPlayer(World.Instance.player1);


          
        }

        if ((Mathf.Abs(World.Instance.player2.transform.position.x - transform.position.x) < bombExplosionDistance)
            && Mathf.Abs(World.Instance.player2.transform.position.z - transform.position.z) < bombExplosionDistance)
        {

            Screen.TakeScreenshot_Static(500, 400);
            yield return new WaitForSeconds(1.0f);
           World.Instance.KillPlayer(World.Instance.player2);
        }

       
        World.Instance.ChangeToCero(transform.position);
      
        Destroy(gameObject);
       


    }
*/

    //NEW IENUMERATOR
    IEnumerator Explode()
    {

        yield return new WaitForSeconds(bombExplosionDelay);
        bool a = CubeNear();
        bool b = Player1Near();
        bool c = Player2Near();


        //SIMPLIFICAR EL DESTROYCUBE
        if (a && !b && !c)
        { 
            DestroyCube(Cube());
        }

        else 

        { 
            Screen.TakeScreenshot_Static(screenA, screenB);
            yield return new WaitForSeconds(screenDelay);

            if (a)
            { 
                DestroyCube(Cube());

            }

            if (b)
                {
                    World.Instance.KillPlayer(World.Instance.player1);

                }
             if (c)
                {
                    World.Instance.KillPlayer(World.Instance.player2);

                }
           

            }


            
     

        DestroyBomb();
    }
        /*
        if (a && b  && c )
        {

            Screen.TakeScreenshot_Static(screenA, screenB);
           yield return new WaitForSeconds(screenDelay);

           
            DestroyCube(Cube());
            World.Instance.KillPlayer(World.Instance.player1);
           World.Instance.KillPlayer(World.Instance.player2);

        }

       
        else if (a && b) 
        {
       
            Screen.TakeScreenshot_Static(screenA, screenB);
            yield return new WaitForSeconds(screenDelay);

          
            DestroyCube(Cube());
            World.Instance.KillPlayer(World.Instance.player1);

       
        }

    
        else if (a && c)
        {
            
            Screen.TakeScreenshot_Static(screenA, screenB);
            yield return new WaitForSeconds(screenDelay);

           
            DestroyCube(Cube());
            World.Instance.KillPlayer(World.Instance.player2);
          
           
        }

       

        else if (b && c)
       
        {

            Screen.TakeScreenshot_Static(screenA, screenB);
            yield return new WaitForSeconds(screenDelay);

        
            World.Instance.KillPlayer(World.Instance.player1);
            World.Instance.KillPlayer(World.Instance.player2);

           
        }

      
        else if (a)
        {
           
            DestroyCube(Cube());

        }

        else if (b)
     
        {

            Screen.TakeScreenshot_Static(screenA, screenB);
            yield return new WaitForSeconds(screenDelay);

          
            World.Instance.KillPlayer(World.Instance.player1);
           
        }

        else if (c)
      
        {

            Screen.TakeScreenshot_Static(screenA, screenB);
           yield return new WaitForSeconds(screenDelay);

           
            World.Instance.KillPlayer(World.Instance.player2);
      
        }

        DestroyBomb();
         }
    */

   

    //is Player1 Near?
    private bool Player1Near()
    {
       
        return ((Mathf.Abs(World.Instance.player1.transform.position.x - transform.position.x) < bombExplosionDistance)
             && Mathf.Abs(World.Instance.player1.transform.position.z - transform.position.z) < bombExplosionDistance);
    }


    //is Player2 Near?
    private bool Player2Near()
    {
      
        return ((Mathf.Abs(World.Instance.player2.transform.position.x - transform.position.x) < bombExplosionDistance)
             && Mathf.Abs(World.Instance.player2.transform.position.z - transform.position.z) < bombExplosionDistance);
    }


    //is there a Cube Near?
    private bool CubeNear()
    {
        /*
         * @Santor
         * Se podría simplificar así
         */

        for (int i = 0; i < World.Instance.explosiveItems.Count; i++)
        {
            if (Mathf.Abs(World.Instance.explosiveItems[i].transform.position.x - transform.position.x) < bombExplosionDistance
                && Mathf.Abs(World.Instance.explosiveItems[i].transform.position.z - transform.position.z) < bombExplosionDistance)
                return true;
        }
        return false;

        /*
int a = 0;
for (int i = 0; i<World.Instance.explosiveItems.Count; i++)
{
    if (Mathf.Abs(World.Instance.explosiveItems[i].transform.position.x - transform.position.x) < bombExplosionDistance
        && Mathf.Abs(World.Instance.explosiveItems[i].transform.position.z - transform.position.z) < bombExplosionDistance)
    {

        a = a + 1;

}

}

if (a>0)
{

    return true;
}

else
{

    return false;
}
*/
    }

    //returns which Cube is near
    private int Cube()
    {
    for (int i = 0; i<World.Instance.explosiveItems.Count; i++)
        {
            if (Mathf.Abs(World.Instance.explosiveItems[i].transform.position.x - transform.position.x) < bombExplosionDistance
                && Mathf.Abs(World.Instance.explosiveItems[i].transform.position.z - transform.position.z) < bombExplosionDistance)
            {

                return i;
}
  
        }
        return 0;

    }


    private void DestroyCube(int i)
    {
        World.Instance.ChangeToCero(World.Instance.explosiveItems[i].transform.position);
        World.Instance.explosiveItems[i].SetActive(false);
        World.Instance.explosiveItems.Remove(World.Instance.explosiveItems[i]);
       
    }

    private void DestroyBomb()
    {
        World.Instance.ChangeToCero(transform.position);
        Destroy(gameObject);
    }

}
