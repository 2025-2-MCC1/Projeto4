using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class SanityManager : MonoBehaviour
{
    public Slider sanitySlider;
    public PostProcessProfile profile;
    private Vignette vignette;
    public int fullSanity;
    public int difficulty;
    private float percent;
    private bool isLosingSanity = false;
    private Coroutine sanityRoutine;

    public LampManager lampManager;

    public UnityEvent onInsane;

    void Start()
    {
        profile.TryGetSettings(out vignette);
        if (sanitySlider == null)
            sanitySlider = GetComponent<Slider>();

        sanitySlider.maxValue = fullSanity;
        sanitySlider.value = fullSanity;
        vignette.intensity.value = 0f;
    }

    // Inicia ou retoma a perda de sanidade
    public void StartLosingSanity()
    {
        if (!isLosingSanity)
        {
            sanityRoutine = StartCoroutine(LoseSanity());
        }
    }

    // Para a perda de sanidade
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
            // Diminuição contínua, visível mesmo em piscadas curtas
            sanitySlider.value -= 2f * difficulty * Time.deltaTime * 10f;
            sanitySlider.value = Mathf.Clamp(sanitySlider.value, 0, sanitySlider.maxValue);

            float newValue = (sanitySlider.value - sanitySlider.maxValue) * -1;
            percent = newValue / sanitySlider.maxValue;
            vignette.intensity.value = percent;

            yield return null; // atualiza a cada frame
        }

        onInsane.Invoke();
        Debug.Log("O jogador enlouqueceu!");

        isLosingSanity = false;
        sanityRoutine = null;
    }

    public void AffectSanity(float value)
    {
        sanitySlider.value += value;
        sanitySlider.value = Mathf.Clamp(sanitySlider.value, 0, sanitySlider.maxValue);
    }
}
