using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Jugador : MonoBehaviour {

    public KeyCode MoverArriba;
    public KeyCode MoverAbajo;
    public KeyCode MoverIzquierda;
    public KeyCode MoverDerecha;
    public KeyCode TeclaBomba;



    float x1;
    float y1;
    float z1;
  
    public GameObject contrincante;
    public GameObject popUp;
    public GameObject Resultado;
    public GameObject Bomba;

    public GameObject cubo1;
    public GameObject cubo2;
    public GameObject cubo3;

    Vector3 cubito1;
    Vector3 cubito2;
    Vector3 cubito3;

    public Vector3[] cubitos = new Vector3[3];

   

    bool flag1;
    Vector3 pos1;
    Vector3 pos2;
    Vector3 futuro1;
    Vector3 futuro2;
    public float moveSpeed;
    int upperlimitX = 5;
    int lowerlimitX = -5;
    int upperlimitZ = 5;
    int lowerlimitZ = -5;
    Vector3[] prohibidos = new Vector3[21];
   
 

    int scorePropio;
   int scoreContrincante;

	void Start () {

    

        cubito1 = new Vector3(-3, 1, 3);
        cubito2 = new Vector3(1, 1, 3);
        cubito3 = new Vector3(3, 1, -2);


        cubitos[0] = cubito1;
        cubitos[1] = cubito2;
        cubitos[2] = cubito3;

        /*
       scorePropio = 2;
        scoreContrincante = 2;
        */

        popUp.SetActive(false);


        //posiciones random de jugadores
        x1 = Random.Range(-4, 4);
        y1 = 1;
        z1 = -4;
        pos1 = new Vector3(x1, y1, z1);
        transform.position = pos1;
        flag1 = true;



        //array de paredes 
        prohibidos[0] = new Vector3(-1, 1, -1);
        prohibidos[1] = new Vector3(-1, 1, 0);
        prohibidos[2] = new Vector3(-1, 1, 1);
        prohibidos[3] = new Vector3(-1, 1, 2);
        prohibidos[4] = new Vector3(-1, 1, 3);
        prohibidos[5] = new Vector3(-1, 1, 4);

        prohibidos[6] = new Vector3(1, 1, 0);
        prohibidos[7] = new Vector3(1, 1, -1);
        prohibidos[8] = new Vector3(1, 1, -2);
        prohibidos[9] = new Vector3(1, 1, -3);

        prohibidos[10] = new Vector3(3, 1, 0);
        prohibidos[11] = new Vector3(3, 1, 1);
        prohibidos[12] = new Vector3(3, 1, 2);
        prohibidos[13] = new Vector3(3, 1, 3);

        prohibidos[14] = new Vector3(-4, 1, -3);
        prohibidos[15] = new Vector3(-3, 1, -3);
        prohibidos[16] = new Vector3(-2, 1, -3);
        prohibidos[17] = new Vector3(-1, 1, -3);

        prohibidos[18] = new Vector3(-3, 1, 0);
        prohibidos[19] = new Vector3(-3, 1, -1);
        prohibidos[20] = new Vector3(-3, 1, -2);
	}
	
	void Update () {
        //  Update1();
        //  Update2();
        flag1 = true;
        if (Input.GetKeyUp(MoverArriba))
        {
            if (transform.position.z + 1 < upperlimitZ)
            {
                
                futuro1 = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
                for (int i = 0; i <= 20; i++)
                {
                    if (futuro1 == prohibidos[i])
                    {
                        flag1 = false;
                    }

                }

                for (int j = 0; j <= 2; j++)
                {
                    if (futuro1 == cubitos[j])
                    {
                        flag1 = false;
                    }

                }



                if (flag1 == true)
                {
                    transform.position += new Vector3(0, 0, 1);

                }
            }
        }
        if (Input.GetKeyUp(MoverAbajo))

        {
            if (transform.position.z - 1 > lowerlimitZ)
            {
                
                futuro1 = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
                for (int i = 0; i <= 20; i++)
                {
                    if (futuro1 == prohibidos[i])
                    {
                        flag1 = false;
                    }

                }
                for (int j = 0; j <= 2; j++)
                {
                    if (futuro1 == cubitos[j])
                    {
                        flag1 = false;
                    }

                }



                if (flag1 == true)
                {
                    transform.position += new Vector3(0, 0, -1);

                }
            }
        }

        if (Input.GetKeyUp(MoverIzquierda))

        {
            if (transform.position.x - 1 > lowerlimitX)
            {
                

                futuro1 = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
                for (int i = 0; i <= 20; i++)
                {
                    if (futuro1 == prohibidos[i])
                    {
                        flag1 = false;
                    }

                }
                for (int j = 0; j <= 2; j++)
                {
                    if (futuro1 == cubitos[j])
                    {
                        flag1 = false;
                    }

                }





                if (flag1 == true)
                {
                    transform.position += new Vector3(-1, 0, 0);

                }
            }
        }


        if (Input.GetKeyUp(MoverDerecha))
        {


            if (transform.position.x + 1 < upperlimitX)
            {

                futuro1 = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
                for (int i = 0; i <= 20; i++)
                {
                    if (futuro1 == prohibidos[i])
                    {
                        flag1 = false;
                    }

                }
                for (int j = 0; j <= 2; j++)
                {
                    if (futuro1 == cubitos[j])
                    {
                        flag1 = false;
                    }

                }




                if (flag1 == true)
                {
                    transform.position += new Vector3(1, 0, 0);

                }
            }
        }

        if (Input.GetKeyUp(TeclaBomba))
        {

            {
                GameObject Temporary_Bomba;
                Temporary_Bomba = Instantiate(Bomba, transform.position, transform.rotation) as GameObject;

                if ((Mathf.Abs(cubito1.x - Temporary_Bomba.transform.position.x) < 3) && (Mathf.Abs(cubito1.z - Temporary_Bomba.transform.position.z) < 3))
                {
                    Invoke("Explosion1", 3.0f);
                }

                if ((Mathf.Abs(cubito2.x - Temporary_Bomba.transform.position.x) < 3) && (Mathf.Abs(cubito2.z - Temporary_Bomba.transform.position.z) < 3))
                {
                    Invoke("Explosion2", 3.0f);
                }

                if ((Mathf.Abs(cubito3.x - Temporary_Bomba.transform.position.x) < 3) && (Mathf.Abs(cubito3.z - Temporary_Bomba.transform.position.z) < 3))
                {
                    Invoke("Explosion3", 3.0f);
                }


                float a = Temporary_Bomba.transform.position.x;
                float b = Temporary_Bomba.transform.position.z;
                Destroy(Temporary_Bomba, 3.0f);
                StartCoroutine(Explosion4(a, b, 3));
                StartCoroutine(Explosion5(a, b, 3));

            }






        }


            }


  // explosiones cubos
    void Explosion1()
    {
        cubo1.SetActive(false);
        cubito1 = new Vector3(100, 100, 100);
        cubitos[0] = cubito1;
       

    }

    void Explosion2()
    {
        cubo2.SetActive(false);
        cubito2 = new Vector3(100, 100, 100);
        cubitos[1] = cubito2;
       
    }

    void Explosion3()
    {
        cubo3.SetActive(false);
        cubito3= new Vector3(100, 100, 100);
        cubitos[2] = cubito3;
    }

    //explosion jugador 1
    IEnumerator Explosion4(float a, float b, float delay)
    {

        yield return new WaitForSeconds(delay);
     
        if ((Mathf.Abs(gameObject.transform.position.x- a) < 3) && (Mathf.Abs(gameObject.transform.position.z - b) < 3))
        {
            gameObject.SetActive(false);

          
        

       // ScoreBehaviour.RefreshScore();
        //    Result.AddScore1();

        x1 = Random.Range(-4, 4);
            pos1 = new Vector3(x1, y1, z1);
            transform.position = pos1;
            flag1 = true;

            /*
            if (scorePropio == 0)
        {
            popUp.SetActive(true);

        }
       */
       
            gameObject.SetActive(true);
        
        }

        }

    IEnumerator Explosion5(float a, float b, float delay)
    {

        yield return new WaitForSeconds(delay);

        if ((Mathf.Abs(contrincante.transform.position.x - a) < 3) && (Mathf.Abs(contrincante.transform.position.z - b) < 3))
        {
            contrincante.SetActive(false);



         //   ScoreBehaviour2.RefreshScore();
        //    Result.AddScore1();

            x1 = Random.Range(-4, 4);
            pos1 = new Vector3(x1, y1, z1);
            contrincante.transform.position = pos1;
            flag1 = true;

           
            /*
             * if (scoreContrincante == 0)
            {
                popUp.SetActive(true);

            }
*/

            contrincante.SetActive(true);

        }

    }
     




}
  

 

    



    
           
