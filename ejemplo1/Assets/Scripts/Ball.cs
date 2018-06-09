using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public float speed;
    public Rigidbody rb;
  

    public GameObject Bullet_Emitter;
    public GameObject Bullet;
    public GameObject Esferita;
    public float Bullet_Forward_Force;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
     
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);

       

      //  joint.connectedBody = Esfera2.rigidbody;


       if (Input.GetKeyDown("space"))
        {

            // Bullet instantiation
            GameObject Temporary_Bullet_Handler;
            Temporary_Bullet_Handler = Instantiate(Bullet, Bullet_Emitter.transform.position, Bullet_Emitter.transform.rotation) as GameObject;
            // what, from where y rotation

            //por si el pivot se seteo mal de esta manera lo corregimos
            Temporary_Bullet_Handler.transform.Rotate(Vector3.left * 90);

            Rigidbody Temporary_RigidBody;
            Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();

            //tell bullet to be pushed
            Temporary_RigidBody.AddForce(transform.forward * Bullet_Forward_Force);

            Destroy(Temporary_Bullet_Handler, 10.0f);



        }



      
	}

    void OnCollisionEnter(Collision other)
    {

        if (other.gameObject == Esferita)
        {
        Rigidbody Temporary_Esferita;
        Temporary_Esferita = Esferita.GetComponent<Rigidbody>();
            gameObject.AddComponent<FixedJoint>().connectedBody = Temporary_Esferita;
        }
    }

   
   
   
  

}
