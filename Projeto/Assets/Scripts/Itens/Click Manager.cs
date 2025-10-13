using UnityEngine;

public class ClickManager : MonoBehaviour
{
    [SerializeField] private Camera mainCamera; // arraste a Main Camera aqui no inspetor
    [SerializeField] private float clickRange = 100f; // distância máxima do clique

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // clique esquerdo do mouse
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, clickRange))
            {
                // verifica se o objeto clicado tem um Interactable
                Interactable interactable = hit.collider.GetComponent<Interactable>();

                if (interactable != null)
                {
                    interactable.Interact(); // executa o comportamento (no caso, coletar e destruir)
                    
                }
            }
        }
    }
}