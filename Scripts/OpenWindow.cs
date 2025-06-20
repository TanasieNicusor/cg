using UnityEngine;
using System.Collections;

public class OpenWindow : MonoBehaviour
{
    [SerializeField] private GameObject window;
    private bool isOpen = false;

    public void Open()
    {
        if (!isOpen)
        {
            StartCoroutine(OpenWindowCoroutine());  
        }
    }


    private IEnumerator OpenWindowCoroutine()
    {
        float i = 0f;
        while (i < 0.9f)
        {
            Vector3 newPosition = window.transform.position;
            newPosition.z += 0.05f;
            window.transform.position = newPosition;
            i += 0.05f;
            yield return new UnityEngine.WaitForSeconds(0.01f);

        }
        isOpen = true;
        Debug.Log("Window opened");
    }
}

