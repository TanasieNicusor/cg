using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreenScript : MonoBehaviour
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

    public void LoadNextLevel()
    {
        if (string.IsNullOrEmpty(SceneTracker.PreviousSceneName))
        {
            Debug.LogWarning("Previous scene name not found.");
            return;
        }

        string scenePath = "Assets/" + SceneTracker.PreviousSceneName + ".unity";
        int currentIndex = SceneUtility.GetBuildIndexByScenePath(scenePath);

        if (currentIndex == -1)
        {
            Debug.LogError("Scene not found in build settings: " + SceneTracker.PreviousSceneName);
            return;
        }

        int nextIndex = currentIndex + 1;

        if (nextIndex < 3)
        {
            SceneManager.LoadScene(nextIndex);
        }
        else
        {
            Debug.Log("No more levels! Maybe return to main menu?");
        }
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
