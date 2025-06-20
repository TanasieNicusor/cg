using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
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



}
