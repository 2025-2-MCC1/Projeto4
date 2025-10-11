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
        currentSnapAngle = 0f;
        targetRotation = Quaternion.Euler(0, currentSnapAngle, 0);
        transform.rotation = targetRotation;
        nextSnapAngle = currentSnapAngle;

        AtualizarParedes();
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
        float angulo = transform.eulerAngles.y;

        // Normaliza o ângulo entre 0 – 360
        angulo = (angulo + 360f) % 360f;

        // INTERVALOS 

        // 0° = entre 315° e 45°
        if (angulo >= 315f || angulo < 45f)
        {
            Parede1.SetActive(true);
            Parede2.SetActive(false);
            Parede3.SetActive(false);
            Parede4.SetActive(true);
        }

        // 90° = entre 45° e 135°
        else if (angulo >= 45f && angulo < 135f)
        {
            Parede1.SetActive(true);
            Parede2.SetActive(false);
            Parede3.SetActive(true);
            Parede4.SetActive(false);
        }

        // 180° = entre 135° e 225°
        else if (angulo >= 135f && angulo < 225f)
        {
            Parede1.SetActive(false);
            Parede2.SetActive(true);
            Parede3.SetActive(true);
            Parede4.SetActive(false);
        }

        // 270° = entre 225° e 315°
        else if (angulo >= 225f && angulo < 315f)
        {
            Parede1.SetActive(false);
            Parede2.SetActive(true);
            Parede3.SetActive(false);
            Parede4.SetActive(true);
        }
    }
}

