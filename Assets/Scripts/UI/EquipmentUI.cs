using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentUI : MonoBehaviour
{
    public Transform equipmentParent;
    EquipmentManager equipmentManager;
    EquipmentSlot[] equipmentSlot;
    // Start is called before the first frame update
    //private void Awake()
    //{
    //    DontDestroyOnLoad(gameObject);
    //}
    void Start()
    {
        equipmentManager = EquipmentManager.instance;
        equipmentManager.onEquipChangedCallback += UpdateUI;
        equipmentSlot = equipmentParent.GetComponentsInChildren<EquipmentSlot>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void UpdateUI()
    {
        for (int i = 0; i < equipmentSlot.Length; i++)
        {
            Equipment newEquip = equipmentManager.GetItem((int)equipmentSlot[i].slot);
            if (newEquip != null)
            {
                equipmentSlot[i].AddEquip(newEquip);
            }
        }
    }
}
