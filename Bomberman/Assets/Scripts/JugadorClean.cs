using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorClean : MonoBehaviour {

    public KeyCode MoverArriba;
    public KeyCode MoverAbajo;
    public KeyCode MoverIzquierda;
    public KeyCode MoverDerecha;
    public KeyCode TeclaBomba;

    public GameObject contrincante;
    public GameObject popUp;
    public GameObject Resultado;
    public GameObject Bomba;
    public GameObject cubo1;
    public GameObject cubo2;
    public GameObject cubo3;

    public List<Vector3> cubitos = new List<Vector3>();
    public List<GameObject> explosiveItems;

    Vector3[] prohibidos = new Vector3[21];
    Vector3[] gridBoundaries;

    float bombExplosionDelay;

    int bombExplosionDistance;
    int upperlimitX;
    int lowerlimitX;
    int upperlimitZ;
    int lowerlimitZ;

    
    // Use this for initialization
    void Start () {
        upperlimitX = 5;
        lowerlimitX = -5;
        upperlimitZ = 5;
        lowerlimitZ = -5;
        bombExplosionDistance = 3;
        bombExplosionDelay = 3.0f;

        gridBoundaries = new Vector3[(upperlimitX - lowerlimitX) * 2 + (upperlimitZ - lowerlimitZ) * 2];
        for (int i = 0; i < gridBoundaries.Length / 4; i++)
        {
            gridBoundaries[i * 4] = new Vector3(lowerlimitX + i, 1.0f, lowerlimitZ);
            gridBoundaries[i * 4 + 1] = new Vector3(lowerlimitX + i, 1.0f, upperlimitZ);
            gridBoundaries[i * 4 + 2] = new Vector3(lowerlimitX, 1.0f, lowerlimitZ + i);
            gridBoundaries[i * 4 + 3] = new Vector3(upperlimitX, 1.0f, lowerlimitZ + i);
        }
        cubitos.Add(cubo1.transform.position);
        cubitos.Add(cubo2.transform.position);
        cubitos.Add(cubo3.transform.position);

        explosiveItems.Add(cubo1);
        explosiveItems.Add(cubo2);
        explosiveItems.Add(cubo3);

        RelocatePlayer();

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

    // Update is called once per frame
    void Update () {
        Vector3 directionOfMovement = Vector3.zero;

        if (Input.GetKeyUp(MoverArriba))
            directionOfMovement = Vector3.forward;
        else if (Input.GetKeyUp(MoverAbajo))
            directionOfMovement = Vector3.back;
        else if (Input.GetKeyUp(MoverDerecha))
            directionOfMovement = Vector3.right;
        else if (Input.GetKeyUp(MoverIzquierda))
            directionOfMovement = Vector3.left;

        Vector3 newPosition = transform.position + directionOfMovement;

        if (IsTileFree(prohibidos, newPosition) && IsTileFree(cubitos.ToArray(), newPosition) && IsTileFree(gridBoundaries, newPosition))
            transform.position += directionOfMovement;

        if (Input.GetKeyUp(TeclaBomba))
            StartCoroutine("PlaceBomb");
    }

    IEnumerator PlaceBomb()
    {
        GameObject Temporary_Bomba;
        Temporary_Bomba = Instantiate(Bomba, transform.position, transform.rotation) as GameObject;
        yield return new WaitForSeconds(bombExplosionDelay);
        for (int i = 0; i < explosiveItems.Count; i++)
        {
            if (Mathf.Abs(explosiveItems[i].transform.position.x - Temporary_Bomba.transform.position.x) < bombExplosionDistance
                && Mathf.Abs(explosiveItems[i].transform.position.z - Temporary_Bomba.transform.position.z) < bombExplosionDistance)
            {
                contrincante.GetComponent<Jugador>().cubitos.Remove(explosiveItems[i].transform.position);
                contrincante.GetComponent<Jugador>().explosiveItems.Remove(explosiveItems[i]);

                Destroy(explosiveItems[i]);
                cubitos.Remove(explosiveItems[i].transform.position);
                explosiveItems.Remove(explosiveItems[i]);
            }
        }
        if ((Mathf.Abs(gameObject.transform.position.x - Temporary_Bomba.transform.position.x) < bombExplosionDistance)
            && Mathf.Abs(gameObject.transform.position.z - Temporary_Bomba.transform.position.z) < bombExplosionDistance)
            KillPlayer();
        Destroy(Temporary_Bomba);
        if ((Mathf.Abs(contrincante.transform.position.x - Temporary_Bomba.transform.position.x) < bombExplosionDistance)
            && Mathf.Abs(contrincante.transform.position.z - Temporary_Bomba.transform.position.z) < bombExplosionDistance)
            contrincante.GetComponent<Jugador>().KillPlayer();
    }

    bool IsTileFree(Vector3[] tilesOcuppied, Vector3 newPosition)
    {
        foreach (Vector3 tileOccupied in tilesOcuppied)
            if (tileOccupied == newPosition)
                return false;
        return true;
    }

    public void KillPlayer()
    {
        RelocatePlayer();
    }

    void RelocatePlayer()
    {
        transform.position = new Vector3(Random.Range(-4, 4), 1, -4);
    }
}
