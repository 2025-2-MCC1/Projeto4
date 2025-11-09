using UnityEngine;
using UnityEngine.UI;

public class FoboExpressionsUI : MonoBehaviour
{
    [Header("Referências")]
    public SanityManager sanityManager;
    private Image image;

    [Header("Sprites / Estados")]
    public Sprite normalSprite;
    public Sprite tensoSprite;
    public Sprite superTensoSprite;
    public Sprite mortoSprite;

    private bool entrouEmMorto = false; // <- registra se o susto ocorreu
    private FoboState estadoAtual = FoboState.Normal;

    void Start()
    {
        image = GetComponent<Image>();
        if (image == null)
        {
            Debug.LogError("[FoboExpressionsUI] Nenhum Image encontrado!");
            return;
        }

        if (sanityManager == null)
            Debug.LogWarning("[FoboExpressionsUI] SanityManager não atribuído!");

        SetState(FoboState.Normal);
    }

    void Update()
    {
        if (sanityManager == null || sanityManager.sanitySlider == null)
            return;

        float currentSanity = sanityManager.sanitySlider.value;

        // --- Valor exatamente 10: chance de susto ---
        if (currentSanity == 10f)
        {
            if (!entrouEmMorto)
            {
                if (Random.value <= 0.05f)
                {
                    entrouEmMorto = true;
                    SetState(FoboState.Morto);
                    estadoAtual = FoboState.Morto;
                    return;
                }
            }

            // 95% ou já tinha passado sem susto
            SetState(FoboState.SuperTenso);
            estadoAtual = FoboState.SuperTenso;
            return;
        }

        // --- Abaixo de 10: mantém o estado anterior ---
        if (currentSanity < 10f)
        {
            if (entrouEmMorto)
            {
                // Já entrou em morto antes → mantém morto
                SetState(FoboState.Morto);
                estadoAtual = FoboState.Morto;
                return;
            }
            else
            {
                // Nunca teve susto → mantém SuperTenso
                SetState(FoboState.SuperTenso);
                estadoAtual = FoboState.SuperTenso;
                return;
            }
        }

        // --- Lógica normal para valores acima de 10 ---
        FoboState novoEstado;

        if (currentSanity > 6600f)
            novoEstado = FoboState.Normal;
        else if (currentSanity > 3300f)
            novoEstado = FoboState.Tenso;
        else
            novoEstado = FoboState.SuperTenso;

        if (novoEstado != estadoAtual)
        {
            estadoAtual = novoEstado;
            SetState(novoEstado);
        }
    }

    enum FoboState { Normal, Tenso, SuperTenso, Morto }

    void SetState(FoboState state)
    {
        if (image == null)
            return;

        switch (state)
        {
            case FoboState.Normal: image.sprite = normalSprite; break;
            case FoboState.Tenso: image.sprite = tensoSprite; break;
            case FoboState.SuperTenso: image.sprite = superTensoSprite; break;
            case FoboState.Morto: image.sprite = mortoSprite; break;
        }
    }
}
