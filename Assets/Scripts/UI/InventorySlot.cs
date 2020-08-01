using UnityEngine;
using UnityEngine.UI;
public class InventorySlot : MonoBehaviour
{
    Items item;
    public Button removeButton;
    public Image icon;
    public void AddItem(Items newitem)
    {
        item = newitem;
        icon.sprite = item.Icon;
        icon.enabled = true;
        removeButton.interactable = true;
    }
    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }
    public void OnRemoveButton()
    {
        InventorySystem.instance.Remove(item);
    }
    public void UseItem()
    {
        if(item != null)
        {
            item.Use();
        }
    }
}
