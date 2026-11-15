using UnityEngine;

public class Creditos : MonoBehaviour
{
    public float velocidad = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += new Vector3(0, +velocidad, 0);
    }
}
