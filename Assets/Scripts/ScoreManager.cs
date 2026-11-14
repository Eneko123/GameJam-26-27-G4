using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int score;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] public float tiempoActual;
    [SerializeField] public bool cronometroActivado;
    public MovimientoTesla Jugador;

    private void Start()
    {
        score = 0;
        tiempoActual = 0f;
        cronometroActivado = true;
     
    }
     
    private void Update()
    {
        if (cronometroActivado)
        {
            tiempoActual += Time.deltaTime;
            scoreText.text = "Puntuación: " + score.ToString();

        }
        else
        {
            scoreText.text = "La puntuación lograda es  : " + score.ToString();
        }
    }
}
