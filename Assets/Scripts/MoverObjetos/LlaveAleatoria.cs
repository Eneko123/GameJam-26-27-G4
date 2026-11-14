
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
            llave.position = puntoMontonA.position;
        }
        else
        {
            llave.position = puntoMontonB.position;
        }

        Debug.Log("Llave colocada aleatoriamente");
    }
}
