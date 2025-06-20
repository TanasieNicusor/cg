using UnityEngine;

public class PickableItem : MonoBehaviour
{
    public string itemName = "Default Item";

    public virtual void Use()
    {
        Debug.Log("Used item: " + itemName);
    }
}
