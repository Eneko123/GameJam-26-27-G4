using UnityEngine;

// Se pone en la raíz del minijuego (el mismo objeto que MinijuegoResultado).
// Cada vez que el minijuego se activa, deja encendida solo una de sus variantes.
public class VarianteAleatoria : MonoBehaviour, IMinijuego
{
    [SerializeField] private GameObject[] variantes;
    [SerializeField] private bool evitarRepetir = true;

    private int ultima = -1;

    public void Iniciar()
    {
        if (variantes == null || variantes.Length == 0) return;

        int elegida;
        do { elegida = Random.Range(0, variantes.Length); }
        while (evitarRepetir && elegida == ultima && variantes.Length > 1);
        ultima = elegida;

        for (int i = 0; i < variantes.Length; i++)
        {
            if (variantes[i] != null)
                variantes[i].SetActive(i == elegida);
        }

        // MinijuegoResultado ya recorrió los IMinijuego antes de que esta variante se encendiera,
        // así que se los reiniciamos aquí (Iniciar es seguro de llamar dos veces).
        foreach (IMinijuego m in variantes[elegida].GetComponentsInChildren<IMinijuego>())
            m.Iniciar();
    }
}