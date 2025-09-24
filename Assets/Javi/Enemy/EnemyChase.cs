using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Lógica de persecución del enemigo.
/// Sigue al jugador usando el NavMeshAgent.
/// </summary>
public class EnemyChase : MonoBehaviour
{
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Actualizar destino hacia el jugador
    public void DoChase(Transform player)
    {
        if (player != null)
            agent.SetDestination(player.position);
    }
}