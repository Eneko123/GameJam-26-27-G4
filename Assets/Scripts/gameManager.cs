using UnityEngine;
using System;

public class gameManager : MonoBehaviour
{
    [Header("Sonidos")]
    public AudioSource audioSource;
    public AudioClip sonidoCrash;
    public SoundController soundController;
    public static gameManager instance;

    public event Action OnCrashed;
    public event Action OnVictoria;     // velocidad llegó a 0

    public float velocidad;
    public float velocidadInicial = 120f;
    public float velocidadMaxima = 200f;

    public bool gamePaused = true;
    public bool crshed = false;
    public bool ganado = false;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    void Start()
    {
        velocidad = velocidadInicial;
    }

    // delta negativo = frenar (minijuego ganado), positivo = acelerar (minijuego perdido)
    public void CambiarVelocidad(float delta)
    {
        if (crshed || ganado)
        { 
        return;
        }


        velocidad = Mathf.Clamp(velocidad + delta, 0f, velocidadMaxima);

        if (velocidad <= 0f)
            ganado = true;   // el aviso (OnVictoria) lo da ConfirmarVictoria cuando acaba el glitch
    }

    // Lo llama minijuegoManager cuando ya terminó el último minijuego y su glitch
    public void ConfirmarVictoria()
    {
        if (crshed || !ganado) return;
        OnVictoria?.Invoke();
    }

    public void Crash()
    {
        if (crshed) return;
        Debug.Log("funciona crash");
        crshed = true;
        soundController.AccidenteSound();
        OnCrashed?.Invoke();
    }
}