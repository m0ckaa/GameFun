using UnityEngine.AI;
using UnityEngine;

public class PlayerControllerAI : MonoBehaviour
{
    public Camera mainCamera;
    private NavMeshAgent agent;

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
            agent.speed = 6;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider != null)
                {
                    agent.SetDestination(hit.point);
                }
            }
        }
    }
}
