using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    //atributes: delay and range
    //functions: kill

    float bombExplosionDelay;
    int bombExplosionDistance;


	// Use this for initialization
	void Start () {

        bombExplosionDistance = 3;
        bombExplosionDelay = 3.0f;
		
	}
	
	/**
     * @reski : se va a llamar una vez por loop, osea la funcion explode se va a llamar 
     * muchas veces por segundo conviene ponerla en Start asi solo se llama cuando creamos 
     * la Bomba con el Instantiate de Player.
     * */
	void Update () {
        
        Explode();

       
      
    }
		
	

   
    IEnumerator Explode()
    {

        yield return new WaitForSeconds(bombExplosionDelay);

        for (int i = 0; i < GetComponent<World>().explosiveItems.Count; i++)
        {
            if (Mathf.Abs(GetComponent<World>().explosiveItems[i].transform.position.x - transform.position.x) < bombExplosionDistance
                && Mathf.Abs(GetComponent<World>().explosiveItems[i].transform.position.z - transform.position.z) < bombExplosionDistance)
            {


                World.ChangeToCero(GetComponent<World>().explosiveItems[i].transform.position);
                GetComponent<World>().explosiveItems.Remove(GetComponent<World>().explosiveItems[i]);

                Destroy(GetComponent<World>().explosiveItems[i]);

            }
        }

        if ((Mathf.Abs(GetComponent<World>().player1.transform.position.x - transform.position.x) < bombExplosionDistance)
            && Mathf.Abs(GetComponent<World>().player1.transform.position.z - transform.position.z) < bombExplosionDistance)
            World.KillPlayer(GetComponent<World>().player1);

        if ((Mathf.Abs(GetComponent<World>().player2.transform.position.x - transform.position.x) < bombExplosionDistance)
            && Mathf.Abs(GetComponent<World>().player2.transform.position.z - transform.position.z) < bombExplosionDistance)
            World.KillPlayer(GetComponent<World>().player2);

        Destroy(gameObject);
        World.ChangeToCero(transform.position);

    }


}
