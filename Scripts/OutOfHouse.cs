using UnityEngine;

public class OutOfHouse : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has left the house. Starting AI");
            GameState.Instance.StartAI = true;
        }
    }
}
