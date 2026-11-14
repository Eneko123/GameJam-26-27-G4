using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovimientoTesla : MonoBehaviour
{
    public float rotacion = 7f; // Velocidad de rotación del objeto
    public Vector2 mov;
    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + mov * rotacion * Time.fixedDeltaTime);
    }
    public void OnMove(InputValue value)
    {
        mov = value.Get<Vector2>();
    }
}
