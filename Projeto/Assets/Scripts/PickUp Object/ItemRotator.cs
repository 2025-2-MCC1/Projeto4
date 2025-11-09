using UnityEngine;

public class ItemRotator : MonoBehaviour
{
    [Header("Objeto que será rotacionado")]
    public Transform rotationRoot;

    [Header("Configurações")]
    public float rotationSpeed = 50f;

    [HideInInspector] public bool canRotate = false;

    private bool dragging = false;
    private Vector3 lastMousePosition;

    void Update()
    {
        // Se não pode rotacionar no momento → sai
        if (!canRotate || rotationRoot == null)
            return;

        // Início do drag
        if (Input.GetMouseButtonDown(0))
        {
            dragging = true;
            lastMousePosition = Input.mousePosition;
        }

        // Final do drag
        if (Input.GetMouseButtonUp(0))
        {
            dragging = false;
        }

        // Rotação durante arraste
        if (dragging)
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;

            float rotX = delta.y * rotationSpeed * Time.unscaledDeltaTime;
            float rotY = -delta.x * rotationSpeed * Time.unscaledDeltaTime;

            //Rotação estável em espaço global, sem inverter nunca
            rotationRoot.Rotate(Vector3.up, rotY, Space.World);
            rotationRoot.Rotate(Vector3.right, rotX, Space.World);

            lastMousePosition = Input.mousePosition;
        }
    }

    // MÉTODOS USADOS PELO PickUp

    //Versão nova (com parâmetro explícito)
    public void EnableRotation(bool state)
    {
        canRotate = state;

        if (state)
            dragging = false;
    }

    //Versão antiga (compatível com seu código atual)
    public void EnableRotation()
    {
        EnableRotation(true);
    }
}
