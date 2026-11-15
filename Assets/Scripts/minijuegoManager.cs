using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class minijuegoManager : MonoBehaviour
{
    [Header("Referencias")]
    public CanvasPantalla canvasPantalla;
    public GlitchController glitch;
    public Image barraCooldown;                     // Image con Image Type = Filled
    public GameObject[] ocultarEnMinijuego;         // p. ej. el texto de velocidad
    public MinijuegoResultado[] minijuegos;         // objetos de la escena (raíz de cada minijuego)

    [Header("Tiempos")]
    public float intervalo = 4f;                    // espera entre un minijuego y el siguiente
    public float tiempoLimite = 10f;                // tiempo que tiene el jugador (lo que vacía la barra)
    public float glitchVictoria = 0.2f;
    public float glitchDerrota = 2f;

    [Header("Velocidad")]
    public float bajadaVelocidad = 20f;             // al ganar
    public float subidaVelocidad = 15f;             // al perder

    private gameManager gm;
    private MinijuegoResultado actual;
    private int ultimo = -1;
    private Vector3 escalaBarra = Vector3.one;
    private bool detenido;

    void Start()
    {
        gm = gameManager.instance;
        gm.OnCrashed += Detener;

        // Todos empiezan apagados
        foreach (MinijuegoResultado m in minijuegos)
            if (m != null) m.Desactivar();

        if (barraCooldown != null)
        {
            escalaBarra = barraCooldown.transform.localScale;
            barraCooldown.fillAmount = 0f;
        }
        StartCoroutine(Bucle());
    }

    private void OnDestroy()
    {
        if (gm != null) gm.OnCrashed -= Detener;
    }

    // WaitForSeconds respeta Time.timeScale, así que el menú de pausa lo congela todo.
    private IEnumerator Bucle()
    {
        while (!detenido && !gm.ganado)
        {
            yield return new WaitForSeconds(intervalo);
            yield return Jugar();
        }
    }

    private IEnumerator Jugar()
    {
        actual = minijuegos[ElegirIndice()];

        canvasPantalla.Establecer(true);
        MostrarOcultables(false);

        // La barra siempre arranca entera y con su escala original
        if (barraCooldown != null)
        {
            barraCooldown.transform.localScale = escalaBarra;
            barraCooldown.fillAmount = 1f;
        }

        actual.Activar();

        // Barra común: se vacía hasta que el minijuego termina o se acaba el tiempo
        float restante = tiempoLimite;
        while (!actual.Terminado && restante > 0f)
        {
            restante -= Time.deltaTime;
            if (barraCooldown != null) barraCooldown.fillAmount = Mathf.Clamp01(restante / tiempoLimite);
            yield return null;
        }

        bool victoria = actual.Terminado && actual.Victoria;   // sin resultado a tiempo = derrota

        gm.CambiarVelocidad(victoria ? -bajadaVelocidad : subidaVelocidad);

        float duracion = victoria ? glitchVictoria : glitchDerrota;
        if (glitch != null) glitch.PlayGlitch(duracion);
        yield return new WaitForSeconds(duracion);

        actual.Desactivar();
        actual = null;
        canvasPantalla.Establecer(false);
        MostrarOcultables(true);
        if (barraCooldown != null) barraCooldown.fillAmount = 0f;

        if (gm.ganado) gm.ConfirmarVictoria();
    }

    // Aleatorio sin repetir el minijuego anterior
    private int ElegirIndice()
    {
        int i;
        do { i = Random.Range(0, minijuegos.Length); }
        while (i == ultimo && minijuegos.Length > 1);
        ultimo = i;
        return i;
    }

    private void Detener()
    {
        detenido = true;
        StopAllCoroutines();
        if (actual != null) actual.Desactivar();
        canvasPantalla.Establecer(false);
        MostrarOcultables(true);
    }

    private void MostrarOcultables(bool visible)
    {
        foreach (GameObject o in ocultarEnMinijuego)
            if (o != null) o.SetActive(visible);
    }
}