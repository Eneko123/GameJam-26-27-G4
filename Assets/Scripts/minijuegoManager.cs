using UnityEngine;

public class minijuegoManager : MonoBehaviour
{
    public CanvasPantalla canvasPantalla;
    public GameObject[] minijuegos = new GameObject[3];
    public Transform minijuegoPos;
    public float contador = 10f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating(nameof(CrearMinijuegos), 10f, contador);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            CrearMinijuegos();
        }
        else if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            Debug.Log("Cargando minijuego 0");
            Instantiate(minijuegos[0], minijuegoPos.position, Quaternion.identity);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            Debug.Log("Cargando minijuego 1");
            Instantiate(minijuegos[1], new Vector3(8.2f,0,0), Quaternion.identity);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            Debug.Log("Cargando minijuego 2");
            Instantiate(minijuegos[2], minijuegoPos.position, Quaternion.identity);
        }
    }
    public void CrearMinijuegos()
    {
            canvasPantalla.CambiarPantalla();
        int randomMinijuego = Random.Range(0, 3);
        switch (randomMinijuego)
        {
            case 0:
                // Cargar el primer minijuego
                Debug.Log("Cargando minijuego 0");
                Instantiate(minijuegos[0], minijuegoPos.position, Quaternion.identity);
                break;
            case 1:
                // Cargar el segundo minijuego
                Debug.Log("Cargando minijuego 1");
                Instantiate(minijuegos[1], new Vector3(8.2f, 0, 0), Quaternion.identity);

                break;
            case 2:
                // Cargar el tercer minijuego
                Debug.Log("Cargando minijuego 2");
                Instantiate(minijuegos[2], minijuegoPos.position, Quaternion.identity);
                break;
        }
    }
}
