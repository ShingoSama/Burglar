using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem instance;
    public int space = 20;
    public List<Items> items = new List<Items>();
    void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one");
            return;
        }
        instance = this;
        //DontDestroyOnLoad(gameObject);
    }
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;
    public bool Add(Items item)
    {
        if (!item.IsDefaultItem)
        {
            if (items.Count >= space)
            {
                Debug.Log("No more space");
                return false;
            }
            items.Add(item);
            if (onItemChangedCallback != null)
            {
                onItemChangedCallback.Invoke();
            }
        }
        return true;
    }
    public void Remove(Items item)
    {
        items.Remove(item);
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }
}
