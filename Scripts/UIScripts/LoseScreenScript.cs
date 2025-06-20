using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseScreenScript : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = true;
    }
    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

    public void ReplayLevel()
    {
        if (!string.IsNullOrEmpty(SceneTracker.PreviousSceneName))
        {
            SceneManager.LoadScene(SceneTracker.PreviousSceneName);
        }
        else
        {
            Debug.LogWarning("Previous scene name not set.");
        }
    }
}
