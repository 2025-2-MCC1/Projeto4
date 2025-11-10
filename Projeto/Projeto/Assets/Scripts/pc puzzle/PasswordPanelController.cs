using UnityEngine;
using UnityEngine.UI;

public class PasswordPanelController : MonoBehaviour
{
    [Header("UI")]
    public GameObject panelRoot; // painel completo
    public Text usernameText;    // campo que mostra o usuário (pré-preenchido, readonly)
    public InputField passwordInput;
    public Button submitButton;
    public Text feedbackText;    // "senha errada", "acesso liberado"
    public Button closeButton;

    [Header("Config")]
    public string defaultUsername = "kaike"; // preencha com o que quiser
    public bool clearOnOpen = true;
    public int maxAttempts = 5; // 0 = ilimitado

    private int attempts = 0;

    void Start()
    {
        if (panelRoot != null)
            panelRoot.SetActive(false);

        if (submitButton != null)
            submitButton.onClick.AddListener(OnSubmit);

        if (closeButton != null)
            closeButton.onClick.AddListener(ClosePanel);
    }

    public void OpenPanel()
    {
        attempts = 0;
        if (panelRoot != null)
            panelRoot.SetActive(true);

        if (usernameText != null)
            usernameText.text = defaultUsername;

        if (clearOnOpen && passwordInput != null)
            passwordInput.text = "";

        if (feedbackText != null)
            feedbackText.text = "";
    }

    public void ClosePanel()
    {
        if (panelRoot != null)
            panelRoot.SetActive(false);
    }

    void OnSubmit()
    {
        string attempt = passwordInput != null ? passwordInput.text : "";

        if (PasswordManager.Instance == null)
        {
            Debug.LogWarning("PasswordManager não encontrado!");
            if (feedbackText != null) feedbackText.text = "Erro interno.";
            return;
        }

        attempts++;
        if (PasswordManager.Instance.CheckPassword(attempt))
        {
            // correta
            if (feedbackText != null) feedbackText.text = "Acesso liberado.";
            Debug.Log("Senha correta! Acesso concedido.");
            // Aqui você pode chamar eventos: abrir arquivos, cutscene, liberar item, etc.
            OnAccessGranted();
        }
        else
        {
            // incorreta
            if (feedbackText != null) feedbackText.text = "Senha incorreta.";
            Debug.Log("Senha incorreta. tentativa " + attempts);
            if (maxAttempts > 0 && attempts >= maxAttempts)
            {
                // bloqueia ou fecha
                if (feedbackText != null) feedbackText.text = "Painel bloqueado.";
                // você pode desabilitar submit:
                if (submitButton != null) submitButton.interactable = false;
            }
        }
    }

    void OnAccessGranted()
    {
        // exemplo: fecha o painel após 1s e dispara algo
        // Mas para simplicidade, apenas fecha agora:
        ClosePanel();

        // Exemplo: disparar evento global ou chamar método de outro script:
        // FindObjectOfType<GameManager>()?.OnComputerAccessGranted();
    }
}
