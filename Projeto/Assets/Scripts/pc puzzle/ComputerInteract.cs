using UnityEngine;

public class ComputerInteract : MonoBehaviour
{
    public GameObject passwordPanelPrefab; // opcional: prefab do painel OU
    public GameObject passwordPanelInstance; // arraste o painel já presente na cena (recomendado)
    public bool usePrefab = false;

    void Start()
    {
        if (!usePrefab && passwordPanelInstance != null)
            passwordPanelInstance.SetActive(false);
    }

    void OnMouseDown()
    {
        // quando o jogador clica no computador
        if (usePrefab && passwordPanelPrefab != null)
        {
            Instantiate(passwordPanelPrefab);
        }
        else if (passwordPanelInstance != null)
        {
            passwordPanelInstance.SetActive(true);
        }
    }
}
