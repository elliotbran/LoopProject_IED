using UnityEngine;
using System.Collections;

public class KickedDoor : MonoBehaviour
{
    // AudioSource 1: Para el sonido del golpe repetido
    public AudioSource golpeAudioSource;
    // AudioSource 2: Para el sonido de llantos (¡Asignar en el Inspector!)
    public AudioSource llantoAudioSource;

    private Animator animator;

    [Header("Configuración de Animación y Golpes")]
    // ⭐ El nombre del parámetro BOOL en el Animator (ej: "IsAnimating")
    public string animBoolName = "IsAnimating";
    // Duración total de la secuencia de golpes
    public float golpesDuration = 5.0f;
    // Tiempo entre cada golpe (0.6667s = 40 frames a 60 FPS)
    public float timePerHit = 0.6667f;

    private bool isAnimating = false; // Controla la lógica de sonido en Update()
    private float timer = 0f;
    private bool hasBeenTriggered = false; // Controla la ejecución única

    void Start()
    {
        golpeAudioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

        if (golpeAudioSource == null || llantoAudioSource == null || animator == null)
        {
            Debug.LogError("PuertaGolpes: ¡Asegúrate de tener 2 AudioSources (el segundo asignado) y el Animator!");
            enabled = false;
        }
    }

    void Update()
    {
        // Controla la reproducción de sonidos mientras la animación de golpes está activa
        if (isAnimating)
        {
            timer += Time.deltaTime;

            if (timer >= timePerHit)
            {
                golpeAudioSource.Play();
                timer = 0f; // Reinicia el temporizador
            }
        }
    }

    // Método PÚBLICO: Llamado por el Trigger Detector
    public void ActivarGolpes()
    {
        // Solo se ejecuta la primera vez que se llama
        if (!hasBeenTriggered)
        {
            hasBeenTriggered = true;

            StartCoroutine(SecuenciaGolpesYLlanto());
        }
    }

    IEnumerator SecuenciaGolpesYLlanto()
    {
        // --- PARTE 1: GOLPES REPETIDOS Y ANIMACIÓN (5 Segundos) ---

        // 1. Iniciar el ciclo de Sonido y Animación
        isAnimating = true;
        timer = 0f;

        // ⭐ Activamos el BOOL del Animator para iniciar la animación
        animator.SetBool(animBoolName, true);

        // 2. Esperar la duración total de los golpes (5 segundos)
        yield return new WaitForSeconds(golpesDuration);

        // 3. Detener el ciclo de golpes
        isAnimating = false;

        // ⭐ Desactivamos el BOOL del Animator para detener la animación
        animator.SetBool(animBoolName, false);

        // --- PARTE 2: LLANTOS (Audio completo) ---

        // 4. Reproducir el audio de llantos
        llantoAudioSource.Play();

        // 5. Esperar a que el audio de llantos termine (usando su longitud)
        yield return new WaitForSeconds(llantoAudioSource.clip.length);
    }
}