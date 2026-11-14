

using UnityEngine;

public class LlaveAleatoria : MonoBehaviour
{
    [SerializeField] private Transform puntoMontonA;
    [SerializeField] private Transform puntoMontonB;
    [SerializeField] private Transform llave;

    void Start()
    {
        int numeroAleatorio = Random.Range(0, 2);

        if (numeroAleatorio == 0)
        {
            llave.position = new Vector3(puntoMontonA.position.x,puntoMontonA.position.y,0f);
        }
        else
        {
            llave.position = new Vector3(puntoMontonB.position.x,puntoMontonB.position.y,0f);
        }

        Debug.Log("Llave colocada aleatoriamente");
    }
}
