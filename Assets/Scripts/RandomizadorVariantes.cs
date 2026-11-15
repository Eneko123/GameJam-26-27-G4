using UnityEngine;

public class RandomizadorVariantes : MonoBehaviour
{
    public int variante;
    public GameObject[] variantes;

    void Start()
    {
        variante = Random.Range(0, variantes.Length);

        Instantiate(variantes[variante]);
    }
}