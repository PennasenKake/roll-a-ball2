using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    // Viittaus pelaajan sijaintiin
    public Transform player;

    // NavMeshAgent-komponentti reitinhakuun
    private NavMeshAgent navMeshAgent;

    // Alustetaan NavMeshAgent-komponentti
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Päivitetään vihollisen liikkeet
    void Update()
    {
        // Jos pelaaja on olemassa, asetetaan reitti pelaajaa kohti
        if (player != null)
        {
            navMeshAgent.SetDestination(player.position);
        }
    }
}
