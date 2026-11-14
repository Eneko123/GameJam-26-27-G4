using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

enum State {Playing, InMenu}

public class GameplayUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI informationText;
    [SerializeField] private Button playButton;
    [SerializeField] private Button exitButton;

    private State actualState;
    private gameManager manager;

    void Start()
    {
        manager = gameManager.instance;

        actualState = State.InMenu;
        Time.timeScale = 0f;
        informationText.text = "Quien ha hakeado mi Ñesla?";
        playButton.GetComponentInChildren<TextMeshProUGUI>().text = "Jugar";

        playButton.onClick.AddListener(PCButtons);
        exitButton.onClick.AddListener(Exit);
    }

    void Update()
    {
        
    }

    private void PCButtons()
    {
        Time.timeScale = 1.0f;
        manager.gamePaused = false;
        panel.SetActive(false);
        playButton.GetComponentInChildren<TextMeshProUGUI>().text = "Continuar";
        actualState = State.Playing;
    }

    private void OnPause(InputValue value)
    {
        Time.timeScale = 0.0f;
        informationText.text = "Juego pausado";
        manager.gamePaused = true;
        panel.SetActive(true);
        actualState = State.InMenu;
    }

    private void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
