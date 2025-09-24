using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Lógica de patrullaje del enemigo.
/// Genera puntos aleatorios cerca del spawn y los visita.
/// </summary>
public class EnemyPatrol : MonoBehaviour
{
    private NavMeshAgent agent;
    private Vector3 spawnPosition;
    [SerializeField] private float patrolRadius = 10f; // Radio para puntos de patrulla

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        spawnPosition = transform.position;
        SetNewPatrolTarget();
    }

    // Moverse hacia el punto de patrullaje actual
    public void DoPatrol()
    {
        // Si ya llegó al destino -> buscar uno nuevo
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            SetNewPatrolTarget();
    }

    // Elegir un nuevo punto aleatorio en el NavMesh
    public void SetNewPatrolTarget()
    {
        Vector3 randomDir = Random.insideUnitSphere * patrolRadius; // radio de patrulla
        randomDir += spawnPosition;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDir, out hit, 10f, NavMesh.AllAreas))
            agent.SetDestination(hit.position);
    }
}