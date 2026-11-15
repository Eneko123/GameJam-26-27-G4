using UnityEngine;

// Los scripts que necesiten reiniciarse cada vez que el minijuego se activa
// implementan esta interfaz (en vez de usar Start, que solo corre la primera vez).
public interface IMinijuego
{
    void Iniciar();
}

// Se pone en la raíz del minijuego (la parte "mundo": sprites, colliders, scripts).
// Se encarga de activar/desactivar el minijuego, dejarlo como estaba en la escena
// y guardar el resultado.
public class MinijuegoResultado : MonoBehaviour
{
    [Tooltip("Paneles de UI (hijos del Canvas principal) que se activan junto al minijuego")]
    [SerializeField] private GameObject[] panelesUI;

    public bool Terminado { get; private set; }
    public bool Victoria { get; private set; }

    private struct Estado
    {
        public Transform t;
        public Vector3 pos;
        public Quaternion rot;
        public Vector3 escala;
        public bool activo;
    }

    private Estado[] snapshot;

    public void Reportar(bool victoria)
    {
        if (Terminado) return;   // solo cuenta el primer resultado
        Terminado = true;
        Victoria = victoria;
    }

    public void Activar()
    {
        // La foto se toma la primera vez, antes de activar: así guarda el estado original de la escena
        if (snapshot == null) TomarSnapshot();
        Restaurar();

        Terminado = false;
        Victoria = false;

        gameObject.SetActive(true);
        foreach (GameObject p in panelesUI)
            if (p != null) p.SetActive(true);

        foreach (IMinijuego m in GetComponentsInChildren<IMinijuego>())
            m.Iniciar();
    }

    public void Desactivar()
    {
        gameObject.SetActive(false);
        foreach (GameObject p in panelesUI)
            if (p != null) p.SetActive(false);
    }

    private void TomarSnapshot()
    {
        Transform[] hijos = GetComponentsInChildren<Transform>(true);
        snapshot = new Estado[hijos.Length];
        for (int i = 0; i < hijos.Length; i++)
        {
            snapshot[i] = new Estado
            {
                t = hijos[i],
                pos = hijos[i].localPosition,
                rot = hijos[i].localRotation,
                escala = hijos[i].localScale,
                activo = hijos[i].gameObject.activeSelf
            };
        }
    }

    private void Restaurar()
    {
        foreach (Estado e in snapshot)
        {
            if (e.t == null || e.t == transform) continue;
            e.t.localPosition = e.pos;
            e.t.localRotation = e.rot;
            e.t.localScale = e.escala;
            e.t.gameObject.SetActive(e.activo);
        }
        Physics2D.SyncTransforms();
    }
}