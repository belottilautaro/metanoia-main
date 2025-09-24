using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Controla la máquina de estados del enemigo.
/// Decide si el enemigo patrulla o persigue al jugador.
/// </summary>
public class EnemyAI : MonoBehaviour , IStunnable
{
    // Estados posibles del enemigo
    public enum EnemyState { PATROLLING, CHASING, STUNNED }
    public EnemyState currentState;

    [Header("Detection")]
    public float detectionRange = 10f;// Distancia para empezar a perseguir
    public float loseAggroRange = 15f;// Distancia para dejar de perseguir
    

    private Transform playerTransform;// Referencia al jugador
    private NavMeshAgent agent;// Componente de navegación
    private EnemyPatrol patrol;// Script de patrullaje
    private EnemyChase chase;// Script de persecución
    private EnemySanityDrain sanity;// Script de drenaje de cordura
    private float stunTimer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        patrol = GetComponent<EnemyPatrol>();
        chase = GetComponent<EnemyChase>();
        sanity = GetComponent<EnemySanityDrain>();

        // Buscar al jugador por tag
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null) playerTransform = player.transform;

        ChangeState(EnemyState.PATROLLING);
    }

    void Update()
    {
        if (playerTransform == null) return;

        float distance = Vector3.Distance(transform.position, playerTransform.position);

        switch (currentState)
        {
            case EnemyState.PATROLLING:
                patrol.DoPatrol(); // Patrullaje automático

                // Si el jugador está dentro del rango -> cambiar a persecución
                if (distance <= detectionRange)
                    ChangeState(EnemyState.CHASING);
                break;

            case EnemyState.CHASING:
                chase.DoChase(playerTransform); // Perseguir al jugador
                sanity.DrainIfVisible();
                // Si el jugador se aleja demasiado -> volver a patrullaje
                if (distance > loseAggroRange)
                    ChangeState(EnemyState.PATROLLING);
                break;

            case EnemyState.STUNNED:
                stunTimer -= Time.deltaTime;
                if (stunTimer <= 0) ChangeState(EnemyState.PATROLLING);
                break;
        }
    }

    // Cambiar de estado
    void ChangeState(EnemyState newState)
    {
        currentState = newState;
        if (newState != EnemyState.STUNNED)
            agent.isStopped = false; // reanuda movimiento cuando termina el stun
        if (newState == EnemyState.PATROLLING)
            patrol.SetNewPatrolTarget(); // Buscar un nuevo punto de patrulla
    }

    public void Stun(float duration)
    {
        stunTimer = duration;
        agent.isStopped = true; // detiene al enemigo
        ChangeState(EnemyState.STUNNED);
    }


    void OnDrawGizmosSelected()
    {
        if (transform == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, loseAggroRange);

        if (sanity != null) 
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, sanity.drainRange);
        }
    }
}