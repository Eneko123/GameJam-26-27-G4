using UnityEngine;

public class Player_move : MonoBehaviour, IMinijuego
{
    public float moveSpeed = 5f;

    private Vector3 targetPos;
    private bool moving = false;

    // Se llama cada vez que el minijuego se activa (después de restaurar la posición inicial)
    public void Iniciar()
    {
        moving = false;
        targetPos = transform.position;
    }

    // Por si el minijuego se apaga a mitad de movimiento
    private void OnDisable()
    {
        moving = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Mathf.Abs(Camera.main.transform.position.z);
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

            targetPos = new Vector3(worldPos.x, worldPos.y, transform.position.z);
            moving = true;
        }

        if (moving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPos) < 0.05f)
            {
                moving = false;
            }
        }
    }
}