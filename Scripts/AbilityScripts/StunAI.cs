using UnityEngine;

public class StunAIItem : PickableItem
{
    public float duration = 5f;

    public override void Use()
    {
        Debug.Log("StunAI: Use() called");

        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            Component[] comps = playerObj.GetComponents<Component>();


            PlayerController player = playerObj.GetComponentInChildren<PlayerController>();

            if (player != null)
            {
                player.ApplyStunAI(duration);
            }
            else
                Debug.Log("not good");
        }
        else
        {
            Debug.LogWarning("StunAIItem: Player object not found");
        }
    }
}
