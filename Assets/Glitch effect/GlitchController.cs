using System.Collections;
using UnityEngine;

public class GlitchController : MonoBehaviour
{
    [SerializeField] private Material glitchMaterial;

    [Header("Glitch automático (opcional)")]
    [SerializeField] private bool autoGlitch = false;
    [SerializeField] private Vector2 intervalRange = new Vector2(2f, 5f);   // segundos entre glitches
    [SerializeField] private float autoDuration = 0.3f;

    private static readonly int IntensityId = Shader.PropertyToID("_Intensity");

    void Start()
    {
        SetIntensity(0f);

        if (autoGlitch)
        {
            StartCoroutine(AutoRoutine());
        }
    }

    void OnDisable()
    {
        SetIntensity(0f);
    }

    // Llámalo desde cualquier sitio: glitchController.PlayGlitch(0.4f);
    public void PlayGlitch(float duration = 0.3f, float intensity = 1f)
    {
        StopAllCoroutines();
        StartCoroutine(GlitchRoutine(duration, intensity));
    }

    // Para mantener el glitch encendido o apagado a mano (0 = nada, 1 = máximo)
    public void SetIntensity(float value)
    {
        glitchMaterial.SetFloat(IntensityId, value);
    }

    private IEnumerator GlitchRoutine(float duration, float intensity)
    {
        SetIntensity(intensity);
        yield return new WaitForSecondsRealtime(duration);
        SetIntensity(0f);
    }

    private IEnumerator AutoRoutine()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(Random.Range(intervalRange.x, intervalRange.y));
            yield return GlitchRoutine(autoDuration, Random.Range(0.5f, 1f));
        }
    }
}