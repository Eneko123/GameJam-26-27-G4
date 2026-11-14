using TMPro;
using UnityEngine;

public class VelocidadUI : MonoBehaviour
{
    public TextMeshProUGUI velocidadText;
    public gameManager gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //velocidadText.text = gameManager.velocidad + " km/h";
        velocidadText.text = gameManager.velocidad+"";

    }
}
