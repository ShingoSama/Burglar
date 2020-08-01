using UnityEngine;

public class ItemPickUp : Interactable
{
    public Items item;
    public SpriteRenderer itemSR;
    private void Awake()
    {
        itemSR.sprite = item.Icon;
    }
    public override void Interact()
    {
        base.Interact();
        PickUp();
    }
    void PickUp()
    {
        Debug.Log("Pickupitem");
        bool pickup = InventorySystem.instance.Add(item);
        if (pickup)
        {
            Destroy(gameObject);
        }
    }
}
