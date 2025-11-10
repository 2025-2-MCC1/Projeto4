using UnityEngine;

public class PasswordManager : MonoBehaviour
{
    public static PasswordManager Instance { get; private set; }

    // senha atual (inicial vazia ou default)
    private string currentPassword = "";

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetPassword(string pwd)
    {
        currentPassword = pwd ?? "";
        Debug.Log("[PasswordManager] senha definida: " + (currentPassword == "" ? "<vazia>" : "<set>"));
    }

    public string GetPassword()
    {
        return currentPassword;
    }

    public bool CheckPassword(string attempt)
    {
        return attempt == currentPassword;
    }
}

