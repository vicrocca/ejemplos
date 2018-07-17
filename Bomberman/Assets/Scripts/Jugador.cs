using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Jugador : MonoBehaviour {
    
    /*@Santor 
     * Se programa en inglessssssssssssssssssss chicasss :P
     */

    /*@Santor 
     * Vamos a ordenar las variables, primero las publicas, despues las privadas.
     */
    public KeyCode MoverArriba;
    public KeyCode MoverAbajo;
    public KeyCode MoverIzquierda;
    public KeyCode MoverDerecha;
    public KeyCode TeclaBomba;

    public GameObject contrincante;
   /*@reski 
    * 
    *fijense que hay varias cosas que no estan usando saquenlo si no lo necesitan mas
    */ public GameObject popUp;
    public GameObject Resultado;
    public GameObject Bomba;
    public GameObject cubo1;
    public GameObject cubo2;
    public GameObject cubo3;

    /*@Santor
     * Esto lo vamos a cambiar por una Lista, asi podemos libremente sacar cubitos cuando explotan. Con Array tambien se puede hacer,
     * es mas performante pero mas complicado, asi que como son pocos cubitos usamos Lista.
     */
    //public Vector3[] cubitos = new Vector3[3];
    public List<Vector3> cubitos = new List<Vector3>();

    /*@Santor 
     * Creamos una lista que contenga todo lo explotable
     */
    public List<GameObject> explosiveItems;

    /*@Santor
     * Nos estamos moviendo por cuadrados cuando el jugador aprieta una tecla.
     * A priori, no limitamos la velocidad, salvo que no queramos que apriete una x cantidad de veces por segundo.
     */
    // public float moveSpeed;

    /*@Santor
     * Esto no lo necesitamos mas
     */
    /*   float x1;
       float y1;
       float z1;*/

    /*@Santor
     * Esto no lo vamos a necesitar
     */
    /*  Vector3 cubito1;
      Vector3 cubito2;
      Vector3 cubito3;*/

    /*@Santor
    * Esto no lo vamos a necesitar
    */
  /*  Vector3 pos1;
    Vector3 pos2;
    Vector3 futuro1;
    Vector3 futuro2;*/
    Vector3[] prohibidos = new Vector3[21];

    /*@Santor
    * Esto no lo vamos a necesitar
    */
//    bool flag1;

    int upperlimitX = 5;
    int lowerlimitX = -5;
    int upperlimitZ = 5;
    int lowerlimitZ = -5;
    /*@Santor 
     * Vamos a crear un array para los limites, asi seguimos con la logica de arrays de lugares ocupados
     */
    Vector3[] gridBoundaries;

    /*@Santor
     * Despues vemos el score
     */
  //  int scorePropio;
   // int scoreContrincante;

    /*@Santor 
     * Variables para evitar problemas a futuro de numeros magicos, explicados mas abajo.
     */
    int bombExplosionDistance;
    float bombExplosionDelay;
    
    void Start () {
        bombExplosionDistance = 3;
        bombExplosionDelay = 3.0f;

       
        /*@Santor 
         * El perimetro del cuadrado
         */
        gridBoundaries = new Vector3[(upperlimitX - lowerlimitX) * 2 + (upperlimitZ - lowerlimitZ) * 2];
        for(int i = 0; i < gridBoundaries.Length / 4; i++)
        {
            /*@Santor 
             * Usamos Vector3 para ser coherentes con los demas Arrays. Aca se podria optimizar usando Vector2.
             * Uso i * 4 para llenar el array porque por cada i que avanzo en el for lleno 4 casilleros.
             */
            //Para Z = -5, lleno todos los x.
            gridBoundaries[i * 4] = new Vector3(lowerlimitX + i, 1.0f, lowerlimitZ);
            //Para Z = 5, lleno todos los x
            gridBoundaries[i * 4 + 1] = new Vector3(lowerlimitX + i, 1.0f, upperlimitZ);
            //Para X = -5, lleno todos los z
            gridBoundaries[i * 4 + 2] = new Vector3(lowerlimitX, 1.0f, lowerlimitZ + i);
            //Para X = 5, lleno todos los z
            gridBoundaries[i * 4 + 3] = new Vector3(upperlimitX, 1.0f, lowerlimitZ + i);
        }

        /*@Santor
         * Ya tenemos los GameObjects cubos que cada uno tiene su posicion, y se los estamos pasando por parametro en el Editor.
         * No necesitamos asignar estas posiciones, dado que cada una corresponde al cubo.transform.position
         */
        cubitos.Add(cubo1.transform.position);
        cubitos.Add(cubo2.transform.position);
        cubitos.Add(cubo3.transform.position);
       
        explosiveItems.Add(cubo1);
        explosiveItems.Add(cubo2);
        explosiveItems.Add(cubo3);

        /*
       scorePropio = 2;
        scoreContrincante = 2;
        */

        /*@Santor
         * Despues Vemos el Score, por ahora comentamos esto porque no lo usamos
         */
        //popUp.SetActive(false);

        /*@Santor
         * Creamos funcion para ubicar jugadores, esta explicado mas abajo
         */
        //posiciones random de jugadores
        RelocatePlayer();


        /*@Santor 
         * Aca podemos ver que se reptien patrones con las paredes. Por ejemplo, son cubos. Entonces yo si supiera la posicion de la pared,
         * el tamaño que tiene (tamaño en x,y,z) puedo rapidamente llenar este array con un for. Pero esto lo vamos a hacer directamente de nuevo
         * cuando cambiemos el metodo en que esta hecho el bomberman, se los explico mejor en el mail.
         */
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
    //    flag1 = true;

        /*@Santor 
         * Esto esta muy bien resuelto, abajo se los simplifique un poco para que vean otras formas de hacerlo. 
         * Un parametro para ver si se puede simplificar el codigo es identificar si el codigo que tengo se repite todo 
         * y cambia solo una cosa, o si es demasiado parecido y esta muchas veces repetido.
        */
        /* if (Input.GetKeyUp(MoverArriba))
         {
             //@Santor Una vez que elegi que tecla aprieto, el codigo de abajo es casi lo mismo
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
         }*/

        /*@Santor 
         * Inicializamos esta variable en cero, total la vamos a asignar mas abajo.
         */
        Vector3 directionOfMovement = Vector3.zero;

        /*
         * @Santor vamos a usar if y else if, asi solo entra a 1 opcion. Total si aprieta arriba y a la derecha al mismo tiempo en el mismo frame,
         * (muy poco probable) no me interesa porque no vamos a darle movimiento en diagonal. Vector3.forward o la posicion que sea, 
         * lo que me da es un vector (0, 0, 1), o (1, 0, 0) etc, dependiendo de la direccion.
         */
        if (Input.GetKeyUp(MoverArriba))
            directionOfMovement = Vector3.forward;
        else if (Input.GetKeyUp(MoverAbajo))
            directionOfMovement = Vector3.back;
        else if (Input.GetKeyUp(MoverDerecha))
            directionOfMovement = Vector3.right;
        else if (Input.GetKeyUp(MoverIzquierda))
            directionOfMovement = Vector3.left;

        /*
         * @Santor si voy a usar transform.position + directionOfMovement mas de una vez, me conviene guardarlo en una variable por 2 motivos:
         * 1) asi no computo la suma o formula cada vez que quiero saber a donde me tengo que mover
         * 2) si llego a necesitar cambiar algo de transform.position + directionOfMovement, no tengo que ir a buscar en todos lados donde lo puse,
         * simplemente lo cambio en la variable y se cambia en todos lados
        */
        Vector3 newPosition = transform.position + directionOfMovement;

        /*@Santor 
         * Vamos a crear un metodo que me diga si mi nueva posicion es factible o no, dado que estamos usando el codigo de los "for"
         * para el array prohibidos y el array cubitos repetidas veces. Aca, Para los metodos aplica igual que la variable. 
         * Si estoy repitiendo codigo, conviene meterlo en una funcion, de esa manera si algun dia lo tengo que cambiar lo cambio solo una vez. 
         * De paso, el codigo es mas facil de leer.
         * */
        if (IsTileFree(prohibidos, newPosition) && IsTileFree(cubitos.ToArray(), newPosition) && IsTileFree(gridBoundaries, newPosition))
            transform.position += directionOfMovement;
        

        /*@Santor 
         * Vamos a intentar simplificar esta parte del codigo tambien, teniendo en cuenta que una vez puesta la bomba, todo lo
         * "explotable" que este a distancia 3 tiene que desaparecer
         */
     /*   if (Input.GetKeyUp(TeclaBomba))
        {

            {
                GameObject Temporary_Bomba;
                Temporary_Bomba = Instantiate(Bomba, transform.position, transform.rotation) as GameObject;

                /*@Santor 
                 * Cuidado con los "numeros magicos", por ejemplo este 3 para la distancia y el 3.0f para el delay de la bomba. Numero magico
                 * se le llama porque alguien de afuera a priori no entiende que significa ese numero, entonces tiene que ponerse a interpretar
                 * el codigo. A parte de eso, son muy peligrosos porque si vos de repente queres que la distancia de explosion sea de 4, tenes
                 * que encontrar todas las veces que pusiste el 3 y puede que alguna se te pase. Si se te pasa, encontrar ese error en el futuro
                 * puede llevar demasiado tiempo para lo que es. Vamos a crear variables para estos casos, publicas o privadas dependiendo de si 
                 * queremos que el usuario en el Editor de Unity los pueda modificar o no, y si los necesitamos en otro script como informacion.
                 * En vez de estos 3 y 3.0f vamos a crear float bombExplosionDistance y bombExplosionDelay.
                 */
       /*         if ((Mathf.Abs(cubito1.x - Temporary_Bomba.transform.position.x) < 3) && (Mathf.Abs(cubito1.z - Temporary_Bomba.transform.position.z) < 3))
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
        }*/


        if (Input.GetKeyUp(TeclaBomba))
            StartCoroutine("PlaceBomb");
    }

    IEnumerator PlaceBomb()
    {
        GameObject Temporary_Bomba;
        Temporary_Bomba = Instantiate(Bomba, transform.position, transform.rotation) as GameObject;
        yield return new WaitForSeconds(bombExplosionDelay);
        for(int i = 0; i < explosiveItems.Count; i++)
        {
            if (Mathf.Abs(explosiveItems[i].transform.position.x - Temporary_Bomba.transform.position.x) < bombExplosionDistance
                && Mathf.Abs(explosiveItems[i].transform.position.z - Temporary_Bomba.transform.position.z) < bombExplosionDistance)
            {
                /*@Santor
                 * Removemos tanto en mi jugador como en el jugador contrincante el cuadrado. Asi si yo lo elimino, el otro no sigue
                 * chocando con un cubito invisible. Esto no es lo ideal porque el Script Jugador tiene demasiada responsabilidad.
                 * Esto se los explico mejor en el mail.
                 */
                contrincante.GetComponent<Jugador>().cubitos.Remove(explosiveItems[i].transform.position);
                contrincante.GetComponent<Jugador>().explosiveItems.Remove(explosiveItems[i]);

                Destroy(explosiveItems[i]);
                cubitos.Remove(explosiveItems[i].transform.position);
                explosiveItems.Remove(explosiveItems[i]);
            }
        }
        /*@Santor
         * Nos fijamos si nos suicidamos o matamos al oponoente.
         * Creamos una funcion para reubicar el personaje
         */
        if ((Mathf.Abs(gameObject.transform.position.x - Temporary_Bomba.transform.position.x) < bombExplosionDistance)
            && Mathf.Abs(gameObject.transform.position.z - Temporary_Bomba.transform.position.z) < bombExplosionDistance)
            KillPlayer();
        Destroy(Temporary_Bomba);
        if ((Mathf.Abs(contrincante.transform.position.x - Temporary_Bomba.transform.position.x) < bombExplosionDistance)
            && Mathf.Abs(contrincante.transform.position.z - Temporary_Bomba.transform.position.z) < bombExplosionDistance)
            contrincante.GetComponent<Jugador>().KillPlayer();
    }
    //@Santor Metodo que checkea si un Vector esta en el Array
    bool IsTileFree(Vector3[] tilesOcuppied, Vector3 newPosition)
    {
        foreach (Vector3 tileOccupied in tilesOcuppied)
        {
            if (tileOccupied == newPosition)
            {
                //@Santor Poniendo el return false aca, lo que hago es cortar el metodo cuando encuentre que esta ocupado, para no seguir buscando.
                return false;
            }
        }
        //@Santor si llegue hasta aca es porque esta libre esa posicion
        return true;   
    }

    public void KillPlayer()
    {
        //TODO: Aca metemos lo que queramos hacer de puntajes.
        /*@Santor
         * Vamos a crear un metodo para reubicar el personaje porque lo estamos usando cuando se crea y cuando lo matamos, entonces
         * armamos una funcion para no dejar codigo repetido
         */
        RelocatePlayer();
    }

    void RelocatePlayer()
    {
        /*@Santor
         * Aca podemos hacerlo todo en la posicion.
         */
        /*  x1 = Random.Range(-4, 4);
          y1 = 1;
          z1 = -4;
          pos1 = new Vector3(x1, y1, z1);
          transform.position = pos1;*/
        transform.position = new Vector3(Random.Range(-4, 4), 1, -4);
    }
/*
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
/*     
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
/*
            contrincante.SetActive(true);

        }

    }*/





}
  

 

    



    
           
