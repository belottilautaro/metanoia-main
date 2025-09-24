using UnityEngine;

/// <summary>
/// Maneja la cordura del jugador.
/// Otros scripts (como enemigos) pueden llamar a ReduceSanity.
/// </summary>
public class PlayerSanity : MonoBehaviour
{
    [Header("Sanity Settings")]
    public float maxSanity = 100f;
    public float currentSanity;

    void Start()
    {
        currentSanity = maxSanity;
    }

    // Llamado por enemigos u otros efectos
    public void ReduceSanity(float amount)
    {
        currentSanity -= amount;
        currentSanity = Mathf.Clamp(currentSanity, 0f, maxSanity);

        // Opcional: acá podrías actualizar UI, activar efectos, etc.
        Debug.Log("Sanity: " + currentSanity);
    }
}