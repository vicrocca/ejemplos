using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour {

    public KeyCode releaseKey;
    FixedJoint playerFixedJoint;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision other)
    {

        StartCoroutine("Release");

        Player player = other.gameObject.GetComponent<Player>();
        if (player)
        {
            Rigidbody temporaryPlayer;
            temporaryPlayer = player.GetComponent<Rigidbody>();
            playerFixedJoint = gameObject.AddComponent<FixedJoint>();
            playerFixedJoint.connectedBody = temporaryPlayer;


        }
    }

    IEnumerator Release(){

        while (true)
        {
        if (Input.GetKeyUp(releaseKey))
        {
                Destroy(playerFixedJoint);
                StopCoroutine("Release");
                //return;
        }
            yield return null;
        }

       
    }

    /*
   public void Release(){
       
                 Destroy(GetComponent<FixedJoint>().connectedBody);


    }

    if (Input.GetKeyUp(Release))
        {
            Magnet.Release();
            //Release();
        }
        */



}
