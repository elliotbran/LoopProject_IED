using System.Collections;
using UnityEngine;

public class DollJumpscare : MonoBehaviour
{
    // Duración fija del sonido de jumpscare (3.0s)
    private const float JUMPSCARE_DURATION = 3.0f;

    [Header("Configuración de Activación")]
    public float maxDetectionAngle = 45f;
    public float maxDetectionDistance = 5f;
    public LayerMask detectionLayer;
    public GameObject barrierCollider;

    [Header("Sonidos")]
    public AudioClip jumpscareSound;
    public AudioClip laughSound;

    [Header("Animación")]
    public string animationTriggerName = "Jumpscare";
    public float animationDuration = 0.5f; // Duración de la animación

    private AudioSource dollAudioSource;
    private Animator dollAnimator;
    private Transform playerCameraTransform;
    private Collider dollCollider;
    private bool hasBeenActivated = false;

    void Start()
    {
        // Obtener componentes
        dollAudioSource = GetComponent<AudioSource>();
        dollAnimator = GetComponent<Animator>();
        dollCollider = GetComponent<Collider>();

        // Buscar la cámara del jugador
        Camera mainCam = Camera.main;
        if (mainCam != null)
        {
            playerCameraTransform = mainCam.transform;
        }

        // Verificación de componentes críticos
        if (playerCameraTransform == null)
        {
            Debug.LogError("Cámara principal no encontrada.");
            enabled = false;
            return;
        }
        if (dollAudioSource == null || dollAnimator == null || dollCollider == null)
        {
            Debug.LogError("Faltan AudioSource, Animator, o Collider en la Muñeca.");
            enabled = false;
            return;
        }
    }

    void Update()
    {
        if (hasBeenActivated)
        {
            return;
        }

        // Comprobar si el jugador está mirando a la muñeca
        if (IsPlayerLookingAtDoll())
        {
            StartJumpscareSequence();
        }
    }

    bool IsPlayerLookingAtDoll()
    {
        // 1. Comprobación de Distancia
        float distanceToPlayer = Vector3.Distance(playerCameraTransform.position, transform.position);
        if (distanceToPlayer > maxDetectionDistance) return false;

        // 2. Comprobación de Ángulo
        Vector3 directionToDoll = (transform.position - playerCameraTransform.position).normalized;
        float angle = Vector3.Angle(playerCameraTransform.forward, directionToDoll);
        if (angle > maxDetectionAngle) return false;

        // 3. Comprobación de Raycast
        RaycastHit hit;
        if (Physics.Raycast(playerCameraTransform.position, directionToDoll, out hit, maxDetectionDistance, detectionLayer))
        {
            if (hit.collider == dollCollider)
            {
                return true;
            }
        }

        return false;
    }

    void StartJumpscareSequence()
    {
        hasBeenActivated = true;
        StartCoroutine(JumpscareSequence());
    }

    IEnumerator JumpscareSequence()
    {
        float totalDelay = 0f;

        // 1. Primer Sonido: Jumpscare (3.0s fijos)
        if (jumpscareSound != null)
        {
            dollAudioSource.PlayOneShot(jumpscareSound);
            // El tiempo total de espera para que todo termine será AL MENOS la duración del jumpscare
            totalDelay = JUMPSCARE_DURATION;
        }

        // Esperar 0.5 segundos del Jumpscare Sound
        yield return new WaitForSeconds(0.5f);

        // 2. Risa y Animación (Comienzan a los 0.5s del jumpscare)

        // Iniciar Risa
        if (laughSound != null)
        {
            dollAudioSource.PlayOneShot(laughSound);
            // Calcular cuánto tiempo más debe durar la risa desde el inicio del jumpscare
            totalDelay = Mathf.Max(totalDelay, 0.5f + laughSound.length);
        }

        // Iniciar Animación
        if (dollAnimator != null && !string.IsNullOrEmpty(animationTriggerName))
        {
            dollAnimator.SetTrigger(animationTriggerName);
            // Calcular cuánto tiempo más debe durar la animación desde el inicio del jumpscare
            totalDelay = Mathf.Max(totalDelay, 0.5f + animationDuration);
        }

        // El tiempo total que ha pasado desde que comenzó el jumpscare sound es 0.5s.
        // Ahora esperamos el tiempo restante para que el evento más largo termine.
        float remainingTime = totalDelay - 0.5f;

        if (remainingTime > 0f)
        {
            // Esperamos el tiempo restante para que terminen los 3.0s del jumpscare sound O 
            // la risa/animación (lo que dure más, medido desde el inicio de la secuencia).
            yield return new WaitForSeconds(remainingTime);
        }

        // 3. Desaparecer la Muñeca
        gameObject.SetActive(false);

        // 4. Desactivar el Box Collider de Barrera
        if (barrierCollider != null)
        {
            barrierCollider.SetActive(false);
        }
    }
}