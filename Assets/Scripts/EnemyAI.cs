using UnityEngine.AI;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Transform target;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = 5;

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            target = player.transform;
        }
    }

    void Update()
    {
        if(target != null)
        {
            agent.SetDestination(target.position);
        }
        
    }
}
