using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Items : ScriptableObject
{
    new public string Name = "New Item";
    public Sprite Icon = null;
    public string Description = "New Description";
    public bool IsDefaultItem = false;
    public virtual void Use()
    {
        Debug.Log("pasa algo");
    }
    public void RemoveFromInventory()
    {
        InventorySystem.instance.Remove(this);
    }
}
