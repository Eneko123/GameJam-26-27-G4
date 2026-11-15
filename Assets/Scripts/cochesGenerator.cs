using UnityEngine;

public class cochesGenerator : MonoBehaviour
{
    public GameObject coche;
    public GameObject[] coches = new GameObject[3];
    public gameManager gameManager;
    private Vector3[] carriles = new Vector3[4];
    public float tiempoGeneracion = 3f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        carriles[0] = new Vector3(-1.8f-4.8f, 10, 0);
        carriles[1] = new Vector3(-1.8f-2.4f, 10, 0);
        carriles[2] = new Vector3(-1.8f, 10, 0);
        carriles[3] = new Vector3(0.6f, 10, 0);


        InvokeRepeating(nameof(GenerarCoche), 0f, tiempoGeneracion);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GenerarCoche();
        }
    }
    void GenerarCoche()
    {
        int carril = Random.Range(0, 4);
        int cocheIndex = Random.Range(0, coches.Length);

        Vector3 posicion = carriles[carril];
        posicion.z = -1f;

        GameObject cocheNuevo = Instantiate(coches[cocheIndex], posicion, Quaternion.identity);
        cocheNuevo.GetComponent<cocheEnemy>().velocidad = gameManager.velocidad;
    }
}
