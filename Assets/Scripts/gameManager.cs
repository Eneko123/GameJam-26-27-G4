using UnityEngine;

public class gameManager : MonoBehaviour
{
    public static gameManager instance;

    public float velocidad;

    public bool gamePaused = true;

    private void Awake()
    {
        if (instance == null) instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        velocidad = 120f;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
