using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneTracker.PreviousSceneName = SceneManager.GetActiveScene().name;
            Debug.Log("Player won. Loading WinScreen");
            SceneManager.LoadScene("WinScreen");
            GameState.Instance.StartAI = false;
        }

    }
}

