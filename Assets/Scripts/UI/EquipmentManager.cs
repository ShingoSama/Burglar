using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager instance;
    public PlayerStats playerStats;
    void Awake()
    {
        instance = this;
        //DontDestroyOnLoad(gameObject);
    }
    public Equipment[] currentEquipment;
    InventorySystem inventory;
    public StatsPanel statsPanel;

    // Start is called before the first frame update
    void Start()
    {

        inventory = InventorySystem.instance;
        statsPanel.SetStats(playerStats.MaxHealth,
                            playerStats.Damage,
                            playerStats.CriticalChance,
                            playerStats.CriticalDamage,
                            playerStats.Defense,
                            playerStats.MovementSpeed,
                            playerStats.AttackSpeed,
                            playerStats.Luck);
        statsPanel.UpdateStatsValues(playerStats);
        int numSlots = System.Enum.GetNames(typeof(EquipmentType)).Length;
        currentEquipment = new Equipment[numSlots];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public delegate void OnEquipChanged();
    public OnEquipChanged onEquipChangedCallback;
    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipmentSlot;
        Equipment oldItem = null;
        if(currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);
            oldItem.UnEquip(playerStats);
            statsPanel.UpdateStatsValues(playerStats);
        }
        currentEquipment[slotIndex] = newItem;
        newItem.Equip(playerStats);
        statsPanel.UpdateStatsValues(playerStats);
        if (onEquipChangedCallback != null)
        {
            onEquipChangedCallback.Invoke();
        }
    }
    public void UnEquip(int slotIndex)
    {
        if(currentEquipment[slotIndex] != null)
        {
            Equipment oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);
            currentEquipment[slotIndex] = null;
            oldItem.UnEquip(playerStats);
            statsPanel.UpdateStatsValues(playerStats);
        }
        if (onEquipChangedCallback != null)
        {
            onEquipChangedCallback.Invoke();
        }
    }
    public Equipment GetItem(int slotIndex)
    {
        return currentEquipment[slotIndex];
    }
}
