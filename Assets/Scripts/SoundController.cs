using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioSource fuente;

    public AudioClip Arrancar;
    public AudioClip Puerta;
    public AudioClip Acelerar;
    public AudioClip Glitch;
    public AudioClip Accidente;

    public AudioClip Conduccion;


    private void Start()
    {
        fuente = GetComponent<AudioSource>();
    }
    public void ConduccionSound()
    {
        AudioSource Conduccion = fuente;
        Conduccion.loop = true;
        Conduccion.Play();
    }
    public void Arranque()
    {
        fuente.PlayOneShot(Arrancar);
    }
    public void PuertaAbierta()
    {
        fuente.PlayOneShot(Puerta);
    }
    public void AcelerarMotor()
    {
        fuente.PlayOneShot(Acelerar);
    }
    public void GlitchSound()
    {
        Debug.Log("Se está ejecutando el sonido Glitch");
        fuente.PlayOneShot(Glitch);
    }
    public void AccidenteSound()
    {
        fuente.clip = Accidente;
        fuente.Play();
    }
}
