using UnityEngine;
using System.Collections.Generic;

public class GeneradorCarretera : MonoBehaviour
{
    public GameObject tramoPrefab;
    public Transform coche;
    public float longitudTramo = 10f;
    public int tramosIniciales = 5;

    private float siguienteY;

    void Start()
    {
        for (int i = 0; i < tramosIniciales; i++)
        {
            CrearTramo();
        }
    }

    void Update()
    {
        if (coche.position.y + 30f > siguienteY)
        {
            CrearTramo();
        }
    }

    void CrearTramo()
    {
        Instantiate(
            tramoPrefab,
            new Vector3(0, siguienteY, 0),
            Quaternion.identity
        );

        siguienteY += longitudTramo;
    }
}