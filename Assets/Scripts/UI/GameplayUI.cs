using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

enum State {Playing, InMenu, Lose}

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
        manager.OnCrashed += GameOver;

        manager = gameManager.instance;

        actualState = State.InMenu;
        Time.timeScale = 0f;
        informationText.text = "Quien ha hakeado mi Ñesla?";
        playButton.GetComponentInChildren<TextMeshProUGUI>().text = "Jugar";

        playButton.onClick.AddListener(PCButtons);
        exitButton.onClick.AddListener(Exit);
    }

    private void OnDestroy()
    {
        if (manager != null) manager.OnCrashed -= GameOver;
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
        if (actualState == State.Lose) return;

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

    private void GameOver()
    {
        /*
        Time.timeScale = 0;
        informationText.text = "Ostia terrible...";
        manager.gamePaused = true;
        panel.SetActive(true);
        actualState = State.Lose;
        playButton.GetComponentInChildren<TextMeshProUGUI>().text = "Volver a jugar";
        playButton.onClick.RemoveAllListeners();
        playButton.onClick.AddListener(RestartGame);
        */
    }

    private void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
