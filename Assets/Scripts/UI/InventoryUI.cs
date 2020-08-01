using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject inventoryUI;
    InventorySystem inventory;
    InventorySlot[] slots;
    public MainGameMenu mainGameMenu;
    private bool isInMenuUI;
    public Button firstItemButton;
    //private void Awake()
    //{
    //    DontDestroyOnLoad(gameObject);
    //}
    // Start is called before the first frame update
    void Start()
    {
        inventory = InventorySystem.instance;
        inventory.onItemChangedCallback += UpdateUI;
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            isInMenuUI = mainGameMenu.GetMenuUIActive();
            if (!isInMenuUI)
            {
                inventoryUI.SetActive(!inventoryUI.activeSelf);
                if (firstItemButton != null)
                {
                    firstItemButton.Select();
                }
            }
        }
    }
    public bool GetInventoryUIActive()
    {
        return inventoryUI.active;
    }
    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
