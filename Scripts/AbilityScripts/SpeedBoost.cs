using UnityEngine;

public class SpeedBoostItem : PickableItem
{
    public float boostAmount = 10f;
    public float duration = 5f;

    public override void Use()
    {
        Debug.Log("SpeedBoostItem: Use() called");

        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {

            Debug.Log("Player object: " + playerObj.name);
            Component[] comps = playerObj.GetComponents<Component>();
            foreach (var comp in comps)
            {
                Debug.Log("Component: " + comp.GetType().Name);
            }


            Debug.Log("SpeedBoostItem: Found player object");
            PlayerController player = playerObj.GetComponentInChildren<PlayerController>();

            if (player != null)
            {
                player.ApplySpeedBoost(boostAmount, duration);
            }
            else
                Debug.Log("not good");
        }
        else
        {
            Debug.LogWarning("SpeedBoostItem: Player object not found");
        }
    }

}
