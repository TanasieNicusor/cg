using UnityEngine;

public class BirdsEyeItem : PickableItem
{
    public float duration = 5f;

    public override void Use()
    {
        Debug.Log("BirdsEyeItem: Use() called");

        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            Debug.Log("Player object: " + playerObj.name);
            Component[] comps = playerObj.GetComponents<Component>();


            Debug.Log("BirdsEyeItem: Found player object");
            PlayerController player = playerObj.GetComponentInChildren<PlayerController>();

            if (player != null)
            {
                player.ApplyBirdsEye(duration);
            }
            else
                Debug.Log("not good");
        }
        else
        {
            Debug.LogWarning("BirdsEyeItem: Player object not found");
        }
    }
}
