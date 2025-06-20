using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera playerCamera;
    public Camera birdsEyeCamera;

    void Start()
    {
        playerCamera.gameObject.SetActive(true);
        birdsEyeCamera.gameObject.SetActive(false);
    }
}

