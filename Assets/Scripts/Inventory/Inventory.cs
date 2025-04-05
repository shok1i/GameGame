using UnityEngine;

public class Inventory : MonoBehaviour
{
    public InventorySlot[] items;
    public int inventorySize;

    // Call once before Start()
    private void Awake()
    {
        items = new InventorySlot[inventorySize];
    }

    public bool AddItem(ItemBase item)
    {
        for (int i = 0; i < items.Length; i++)
            if (items[i].item == item)
            {
                if (items[i].itemCount < item.maxStackSize)
                {
                    items[i].itemCount++;
                    Debug.Log($"Item added to stack at {i} slot");
                    return true;
                }
            }
            else if (items[i].item == null)
            {
                items[i].item = item;
                items[i].itemCount = 1;
                Debug.Log($"Item added at {i} slot");
                return true;
            }

        Debug.Log("Item cannot be added");
        return false;
    }

    public bool RemoveItem(int slot)
    {
        if (slot >= 0 && slot < items.Length)
        {
            items[slot].itemCount -= 1;
            if (items[slot].itemCount <= 0)
            {
                items[slot].item = null;
                items[slot].itemCount = 0;
            }

            Debug.Log($"Item was removed at {slot} slot");
            return true;
        }

        Debug.Log("Item cannot be removed");
        return false;
    }

    public int ContainItem(ItemBase searchItem)
    {
        for (int i = 0; i < items.Length; i++)
            if (items[i].item == searchItem)
            {
                Debug.Log($"Item found at {i} slot");
                return i;
            }

        Debug.Log("Item not found");
        return -1;
    }

    public void ClearInventory()
    {
        for (int i = 0; i < items.Length; i++)
        {
            items[i].item = null;
            items[i].itemCount = 0;
        }

        Debug.Log($"Inventory was cleared");
    }
}