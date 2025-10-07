using UnityEngine;

public class RotacaoDaSala : MonoBehaviour
{
    [Header("Configurações de Rotação")]
    [SerializeField] float dragRotationSpeed = 100f;     // Velocidade do arrasto
    [SerializeField] float smoothRotationSpeed = 300f;   // Velocidade da rotação suave

    [Header("Referências das Paredes")]
    public GameObject Parede1;
    public GameObject Parede2;
    public GameObject Parede3;
    public GameObject Parede4;

    private bool dragging = false;
    private bool isRotating = false;
    private Vector3 lastMousePosition;

    private Quaternion targetRotation;
    private float currentSnapAngle;
    private float nextSnapAngle;

    void Start()
    {
        currentSnapAngle = 90f;
        targetRotation = Quaternion.Euler(0, currentSnapAngle, 0);
        transform.rotation = targetRotation;
        nextSnapAngle = currentSnapAngle;
    }

    void Update()
    {
        // === CLIQUE E ARRASTO DO MOUSE ===
        if (Input.GetMouseButtonDown(0) && !isRotating)
        {
            dragging = true;
            lastMousePosition = Input.mousePosition;

            float rounded = Mathf.Round(transform.eulerAngles.y / 90f) * 90f;
            currentSnapAngle = rounded;
            nextSnapAngle = currentSnapAngle;
        }

        if (Input.GetMouseButtonUp(0) && dragging && !isRotating)
        {
            dragging = false;
            targetRotation = Quaternion.Euler(0, nextSnapAngle, 0);
            isRotating = true;
        }

        // === ANIMAÇÃO SUAVE ATÉ O ÂNGULO FINAL ===
        if (isRotating)
        {
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                smoothRotationSpeed * Time.deltaTime
            );

            if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
            {
                transform.rotation = targetRotation;
                isRotating = false;
            }

            AtualizarParedes();
            return;
        }

        // === LÓGICA DE ARRASTO ===
        if (dragging)
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;
            float rotationAmount = -delta.x * dragRotationSpeed * Time.deltaTime;

            float currentY = transform.eulerAngles.y;

            // Limita a rotação entre currentSnapAngle ±90°
            float minAngle = (currentSnapAngle - 90f + 360f) % 360f;
            float maxAngle = (currentSnapAngle + 90f) % 360f;

            float proposedY = (currentY + rotationAmount + 360f) % 360f;
            bool allowRotation;

            if (minAngle < maxAngle)
                allowRotation = proposedY >= minAngle && proposedY <= maxAngle;
            else
                allowRotation = proposedY >= minAngle || proposedY <= maxAngle;

            if (allowRotation)
            {
                transform.Rotate(0f, rotationAmount, 0f);
                lastMousePosition = Input.mousePosition;

                float angleDiff = Mathf.DeltaAngle(currentSnapAngle, transform.eulerAngles.y);
                if (Mathf.Abs(angleDiff) >= 45f)
                {
                    nextSnapAngle = (angleDiff > 0)
                        ? (currentSnapAngle + 90f) % 360f
                        : (currentSnapAngle - 90f + 360f) % 360f;
                }
            }

            AtualizarParedes();
        }

        // === ROTACÃO POR TECLA ===
        if (Input.GetKeyDown(KeyCode.RightArrow) && !isRotating)
        {
            currentSnapAngle = Mathf.Round(transform.eulerAngles.y / 90f) * 90f;
            nextSnapAngle = (currentSnapAngle - 90f + 360f) % 360f;
            targetRotation = Quaternion.Euler(0, nextSnapAngle, 0);
            isRotating = true;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && !isRotating)
        {
            currentSnapAngle = Mathf.Round(transform.eulerAngles.y / 90f) * 90f;
            nextSnapAngle = (currentSnapAngle + 90f) % 360f;
            targetRotation = Quaternion.Euler(0, nextSnapAngle, 0);
            isRotating = true;
        }

        AtualizarParedes();
    }

    void AtualizarParedes()
    {
        float angle = Mathf.Round(transform.eulerAngles.y) % 360;

        switch ((int)angle)
        {
            case 0:
                Parede1.SetActive(true);
                Parede2.SetActive(false);
                Parede3.SetActive(false);
                Parede4.SetActive(true);
                break;

            case 90:
                Parede1.SetActive(true);
                Parede2.SetActive(false);
                Parede3.SetActive(true);
                Parede4.SetActive(false);
                break;

            case 180:
                Parede1.SetActive(false);
                Parede2.SetActive(true);
                Parede3.SetActive(true);
                Parede4.SetActive(false);
                break;

            case 270:
                Parede1.SetActive(false);
                Parede2.SetActive(true);
                Parede3.SetActive(false);
                Parede4.SetActive(true);
                break;
        }
    }
}

