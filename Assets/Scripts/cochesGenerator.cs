using UnityEngine;

public class cochesGenerator : MonoBehaviour
{
    public GameObject coche;
    public gameManager gameManager;
    private Vector3[] carriles = new Vector3[4];
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        carriles[0] = new Vector3(-6, 15, 0);
        carriles[1] = new Vector3(-2, 15, 0);
        carriles[2] = new Vector3(2, 15, 0);
        carriles[3] = new Vector3(6, 15, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GenerarCoche(gameManager.velocidad);
        }
    }
    void GenerarCoche(float velocidadCoche)
    { 
        int carril = Random.Range(0, 4);
        GameObject cocheNuevo = Instantiate(coche, carriles[carril], Quaternion.identity);
        cocheNuevo.GetComponent<cocheEnemy>().velocidad = velocidadCoche;
    }
}
