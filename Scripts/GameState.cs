using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState Instance;

    public bool IsPaused = false;
    public bool StopAIStun = false;
    public bool StopAIBirdsEye = false;
    public bool StartAI = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject); 
    }
}
