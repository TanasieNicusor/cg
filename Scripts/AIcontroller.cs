using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using System.Collections;
using System.Linq;


public class AIcontroller : MonoBehaviour
{

    private GameObject destination;
    private NavMeshAgent agent;
    private int startAI = 0;
    void Start()
    {
        destination = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if ((GameState.Instance.StopAIStun == true) || (GameState.Instance.StopAIBirdsEye == true))
            agent.isStopped = true;
        else
            agent.isStopped = false;

        if (GameState.Instance.StartAI && startAI == 0)
        {
            StartCoroutine(Wait3Sec());
        }
        if (startAI == 1)
            agent.SetDestination(destination.transform.position);
    }

        private IEnumerator Wait3Sec()
    {
        yield return new WaitForSeconds(3f);  
        startAI = 1; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            SceneTracker.PreviousSceneName = SceneManager.GetActiveScene().name;
            Debug.Log("Loading LoseScreen");
            SceneManager.LoadScene("LoseScreen");
            GameState.Instance.StartAI = false;
        }
    }

    private void MoveRandomly(float radius = 10f)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }

    private IEnumerator WanderRoutine()
    {
        while (true)
        {
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                MoveRandomly(10f);
            }
            yield return new WaitForSeconds(3f);
        }
    }
}
