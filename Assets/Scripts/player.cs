using UnityEngine;

public class player : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("funciona");
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("funciona tambien");
            gameManager.instance.Crash();
            Destroy(collision.gameObject);

        }
    }
}
