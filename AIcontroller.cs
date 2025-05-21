using UnityEngine;
using UnityEngine.AI;

public class AIcontroller : MonoBehaviour
{
    private GameObject destination;
    private NavMeshAgent agent;

    void Start()
    {
        destination = GameObject.FindGameObjectWithTag("Player");

        agent = GetComponent<NavMeshAgent>();

    }

    private void Update()
    {
        agent.SetDestination(destination.transform.position);
    }
}
