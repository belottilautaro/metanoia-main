using UnityEngine;

/// <summary>
/// Lógica de drenaje de cordura del enemigo.
/// Si el jugador está dentro de cierto rango, pierde cordura gradualmente.
/// </summary>
public class EnemySanityDrain : MonoBehaviour
{
    [Header("Sanity Drain Settings")]
    public float drainRange;      // Distancia a la que empieza a drenar
    public float drainAmount;     // Cordura drenada por segundo

    private Transform player;
    private PlayerSanity playerSanity; // Script en el jugador que maneja la cordura

    void Start()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
            playerSanity = playerObj.GetComponent<PlayerSanity>();
        }
    }

    public void DrainIfVisible()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance > drainRange) return;

        // Lanzar rayo desde el enemigo hacia el jugador
        Vector3 direction = (player.position - transform.position).normalized;
        Vector3 rayOrigin = transform.position + Vector3.up * 1.5f;
        Ray ray = new Ray(rayOrigin, direction);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, drainRange))
        {
            // Si el rayo golpea al jugador directamente
            if (hit.collider.CompareTag("Player"))
            {
                // Llamo a RaduceSanity
                playerSanity.ReduceSanity(drainAmount * Time.deltaTime);
            }
        }
    }

    /*private Transform playerTransform;
    private PlayerSanity playerSanity; // Script en el jugador que maneja la cordura

    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
            playerSanity = player.GetComponent<PlayerSanity>();
        }
    }

    void Update()
    {
        if (playerTransform == null || playerSanity == null) return;

        float distance = Vector3.Distance(transform.position, playerTransform.position);

        // Si el jugador está cerca -> drenar cordura
        if (distance <= drainRange)
        {   
            playerSanity.ReduceSanity(drainAmount * Time.deltaTime);
        }
    }*/
}