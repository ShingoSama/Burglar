using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{
    public EquipmentType slot;
    Equipment item;
    public Button removeButton;
    public Image icon;
    public void AddEquip(Equipment newitem)
    {
        item = newitem;
        icon.sprite = item.Icon;
        icon.enabled = true;
        removeButton.interactable = true;
    }
    public void ClearSlotEquip()
    {
        if (item != null)
        {
            EquipmentManager.instance.UnEquip((int)item.equipmentSlot);
        }
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }
    public void OnRemoveButton()
    {
        EquipmentManager.instance.UnEquip((int)item.equipmentSlot);
    }
    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
        }
    }
}
