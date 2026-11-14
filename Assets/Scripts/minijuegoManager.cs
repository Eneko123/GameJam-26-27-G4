using UnityEngine;

public class minijuegoManager : MonoBehaviour
{
    public GameObject[] minijuegos = new GameObject[3];
    public Transform minijuegoPos;
    public float contador = 10f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        minijuegoPos.position = new Vector3(13.5f, 3, 0);
        InvokeRepeating(nameof(CrearMinijuegos), 10f, contador);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CrearMinijuegos()
    {
        int randomMinijuego = Random.Range(0, 3);
        switch (randomMinijuego)
        {
            case 0:
                // Cargar el primer minijuego
                Debug.Log("Cargando minijuego 1");
                Instantiate(minijuegos[0], minijuegoPos.position, Quaternion.identity);
                break;
            case 1:
                // Cargar el segundo minijuego
                Debug.Log("Cargando minijuego 2");
                Instantiate(minijuegos[1], minijuegoPos.position, Quaternion.identity);
                break;
            case 2:
                // Cargar el tercer minijuego
                Debug.Log("Cargando minijuego 3");
                Instantiate(minijuegos[2], minijuegoPos.position, Quaternion.identity);
                break;
        }
    }
}
