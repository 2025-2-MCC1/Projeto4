using UnityEngine;
using UnityEngine.UI;

public class FoboExpressionsUI : MonoBehaviour
{
    [Header("Referências")]
    public SanityManager sanityManager;
    private Image image; // Troca SpriteRenderer por Image

    [Header("Sprites / Estados")]
    public Sprite normalSprite;
    public Sprite tensoSprite;
    public Sprite superTensoSprite;
    public Sprite mortoSprite;

    private bool isDead = false;
    private FoboState estadoAtual = FoboState.Normal;

    void Start()
    {
        image = GetComponent<Image>();
        if (image == null)
        {
            Debug.LogError("[FoboExpressionsUI] Nenhum Image encontrado no GameObject!");
            return;
        }

        if (sanityManager == null)
            Debug.LogWarning("[FoboExpressionsUI] SanityManager não foi atribuído no Inspector!");

        SetState(FoboState.Normal);
    }

    void Update()
    {
        if (sanityManager == null || sanityManager.sanitySlider == null)
            return;

        if (isDead)
            return;

        float currentSanity = sanityManager.sanitySlider.value;

        FoboState novoEstado;

        if (currentSanity > 6600f)
            novoEstado = FoboState.Normal;
        else if (currentSanity > 3300f)
            novoEstado = FoboState.Tenso;
        else if (currentSanity > 10f)
            novoEstado = FoboState.SuperTenso;
        else
            novoEstado = FoboState.Morto;

        if (novoEstado != estadoAtual)
        {
            estadoAtual = novoEstado;

            if (novoEstado == FoboState.Morto)
                Morrer();
            else
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

    void Morrer()
    {
        if (isDead) return;
        isDead = true;
        SetState(FoboState.Morto);
    }
}
