using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CirclesMG : MonoBehaviour, IMinijuego
{
    [Header("Referencias")]
    [SerializeField] private TextMeshProUGUI objetiveText;
    [SerializeField] private GameObject squareTimer;   // La barra negra
    [SerializeField] private GameObject blueCircle;    // Prefab de bola azul
    [SerializeField] private GameObject redCircle;     // Prefab de bola roja
    [SerializeField] private RectTransform playArea;   // Zona donde pueden salir las bolas

    [Header("Reglas")]
    [SerializeField] private int targetBlueBalls = 20;
    [SerializeField] private float gameDuration = 10f;

    [Header("Aparición de bolas")]
    [SerializeField] private float spawnInterval = 0.25f;
    [Range(0f, 1f)]
    [SerializeField] private float redChance = 0.3f;
    [SerializeField] private float circleLifetime = 1f;

    [Header("Eventos")]
    public UnityEvent<bool> onGameFinished;            // true = victoria, false = derrota

    private class ActiveCircle
    {
        public GameObject go;
        public bool isRed;
        public float radius;
        public float expiresAt;
    }

    private readonly List<ActiveCircle> activeCircles = new List<ActiveCircle>();
    private Camera cam;
    private Vector3 timerStartScale;
    private int collectedBlueBalls = 0;
    private bool win = false;
    private bool playing = false;
    private float elapsed;
    private float spawnTimer;

    void Awake()
    {
        cam = Camera.main;
        timerStartScale = squareTimer.transform.localScale;
    }

    // Lo llama MinijuegoResultado.Activar() cada vez que se muestra el minijuego
    public void Iniciar()
    {
        StartGame();
    }

    void OnDisable()
    {
        // Las bolas se instancian fuera de este objeto, hay que limpiarlas al apagarlo
        ClearCircles();
        playing = false;
    }

    public void StartGame()
    {
        ClearCircles();
        collectedBlueBalls = 0;
        win = false;
        elapsed = 0f;
        spawnTimer = spawnInterval;
        playing = true;

        squareTimer.transform.localScale = timerStartScale;
        UpdateText();
    }

    void Update()
    {
        // No hacer nada si el juego terminó o si el menú de pausa está abierto
        if (!playing || Time.timeScale == 0f) return;

        elapsed += Time.deltaTime;

        // Barra de tiempo: solo se reduce la escala en X
        Vector3 scale = timerStartScale;
        scale.x *= Mathf.Clamp01(1f - elapsed / gameDuration);
        squareTimer.transform.localScale = scale;

        CheckClick();
        if (!playing) return;

        RemoveExpiredCircles();

        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0f)
        {
            spawnTimer += spawnInterval;
            SpawnCircle();
        }

        if (elapsed >= gameDuration)
        {
            EndGame(false);
        }
    }

    private void SpawnCircle()
    {
        bool isRed = Random.value < redChance;
        GameObject template = isRed ? redCircle : blueCircle;

        GameObject go = Instantiate(template);
        go.SetActive(true);

        float radius = go.GetComponent<Renderer>().bounds.extents.x;

        // Posición aleatoria dentro de Play Area
        Rect area = GetPlayAreaInWorld();
        float x = Random.Range(area.xMin + radius, area.xMax - radius);
        float y = Random.Range(area.yMin + radius, area.yMax - radius);
        go.transform.position = new Vector3(x, y, -5);

        activeCircles.Add(new ActiveCircle
        {
            go = go,
            isRed = isRed,
            radius = radius,
            expiresAt = elapsed + circleLifetime
        });
    }

    private Rect GetPlayAreaInWorld()
    {
        Canvas canvas = playArea.GetComponentInParent<Canvas>().rootCanvas;
        Camera uiCam = null;
        if (canvas.renderMode != RenderMode.ScreenSpaceOverlay)
        {
            uiCam = canvas.worldCamera != null ? canvas.worldCamera : cam;
        }

        Vector3[] corners = new Vector3[4];
        playArea.GetWorldCorners(corners);   // 0 = abajo-izquierda, 2 = arriba-derecha

        Vector3 min = cam.ScreenToWorldPoint(RectTransformUtility.WorldToScreenPoint(uiCam, corners[0]));
        Vector3 max = cam.ScreenToWorldPoint(RectTransformUtility.WorldToScreenPoint(uiCam, corners[2]));

        return Rect.MinMaxRect(min.x, min.y, max.x, max.y);
    }

    private void CheckClick()
    {
        Pointer pointer = Pointer.current;
        if (pointer == null || !pointer.press.wasPressedThisFrame) return;

        Vector2 clickPos = cam.ScreenToWorldPoint(pointer.position.ReadValue());

        for (int i = activeCircles.Count - 1; i >= 0; i--)
        {
            ActiveCircle c = activeCircles[i];
            if (Vector2.Distance(clickPos, c.go.transform.position) > c.radius) continue;

            activeCircles.RemoveAt(i);
            Destroy(c.go);

            if (c.isRed)
            {
                collectedBlueBalls = Mathf.Max(0, collectedBlueBalls - 1);
            }
            else
            {
                collectedBlueBalls++;
            }

            UpdateText();

            if (collectedBlueBalls >= targetBlueBalls)
            {
                EndGame(true);
            }
            return;
        }
    }

    private void RemoveExpiredCircles()
    {
        for (int i = activeCircles.Count - 1; i >= 0; i--)
        {
            if (elapsed >= activeCircles[i].expiresAt)
            {
                Destroy(activeCircles[i].go);
                activeCircles.RemoveAt(i);
            }
        }
    }

    private void EndGame(bool victory)
    {
        playing = false;
        win = victory;
        ClearCircles();

        objetiveText.text = win ? "¡Ganaste!" : "¡Perdiste!";
        onGameFinished?.Invoke(win);
        GetComponentInParent<MinijuegoResultado>()?.Reportar(win);
    }

    private void UpdateText()
    {
        objetiveText.text = $"{collectedBlueBalls:00}/{targetBlueBalls}";
    }

    private void ClearCircles()
    {
        foreach (ActiveCircle c in activeCircles)
        {
            Destroy(c.go);
        }
        activeCircles.Clear();
    }
}