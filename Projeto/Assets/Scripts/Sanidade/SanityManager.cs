using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class SanityManager : MonoBehaviour
{
    [Header("Slider de Sanidade")]
    public Slider sanitySlider;
    public int fullSanity = 100;
    public int difficulty = 1;

    [Header("URP Global Volume")]
    public Volume volume;

    // Efeitos URP
    private Vignette vignette;
    private ChromaticAberration chromatic;
    private FilmGrain grain;
    private LensDistortion distortion;

    [Header("Valores Máximos dos Efeitos")]
    public float maxVignette = 1f;
    public float maxChromatic = 1f;
    public float maxGrain = 1f;
    public float maxDistortion = -0.5f;

    private float percent;
    private bool isLosingSanity = false;
    private Coroutine sanityRoutine;

    [Header("Lampada")]
    public LampManager lampManager;

    public UnityEvent onInsane;

    void Start()
    {
        volume.profile.TryGet(out vignette);
        volume.profile.TryGet(out chromatic);
        volume.profile.TryGet(out grain);
        volume.profile.TryGet(out distortion);

        if (sanitySlider == null)
            sanitySlider = GetComponent<Slider>();

        sanitySlider.maxValue = fullSanity;
        sanitySlider.value = fullSanity;

        ResetEffects();
    }

    void ResetEffects()
    {
        if (vignette != null) vignette.intensity.value = 0f;
        if (chromatic != null) chromatic.intensity.value = 0f;
        if (grain != null) grain.intensity.value = 0f;
        if (distortion != null) distortion.intensity.value = 0f;
    }

    public void StartLosingSanity()
    {
        if (!isLosingSanity)
            sanityRoutine = StartCoroutine(LoseSanity());
    }

    public void StopLosingSanity()
    {
        if (isLosingSanity && sanityRoutine != null)
        {
            StopCoroutine(sanityRoutine);
            sanityRoutine = null;
            isLosingSanity = false;
        }
    }

    IEnumerator LoseSanity()
    {
        isLosingSanity = true;

        while (sanitySlider.value > 0)
        {
            sanitySlider.value -= 2f * difficulty * Time.deltaTime * 10f;
            sanitySlider.value = Mathf.Clamp(sanitySlider.value, 0, sanitySlider.maxValue);

            percent = 1f - (sanitySlider.value / sanitySlider.maxValue);

            ApplyEffects(percent);

            yield return null;
        }

        onInsane.Invoke();
        isLosingSanity = false;
        sanityRoutine = null;
    }

    void ApplyEffects(float p)
    {
        if (vignette != null)
            vignette.intensity.value = Mathf.Lerp(0f, maxVignette, p);

        if (chromatic != null)
            chromatic.intensity.value = Mathf.Lerp(0f, maxChromatic, p);

        if (grain != null)
            grain.intensity.value = Mathf.Lerp(0f, maxGrain, p);

        if (distortion != null)
            distortion.intensity.value = Mathf.Lerp(0f, maxDistortion, p);
    }

    public void AffectSanity(float value)
    {
        sanitySlider.value = Mathf.Clamp(sanitySlider.value + value, 0, sanitySlider.maxValue);
    }
}
