using UnityEngine;

public class BreakWallsItem : PickableItem
{
    public float radius = .6f;

    public override void Use()
    {
        Debug.Log("BreakWalls: Use() called");

        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            Component[] comps = playerObj.GetComponents<Component>();


            PlayerController player = playerObj.GetComponentInChildren<PlayerController>();

            if (player != null)
            {
                player.ApplyBreakWalls(transform.position, radius);
            }
            else
                Debug.Log("not good");

        }
        else
        {
            Debug.LogWarning("BreakWallsItem: Player object not found");
        }
    }
}