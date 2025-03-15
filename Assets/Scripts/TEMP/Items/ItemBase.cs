using UnityEngine;


[CreateAssetMenu(fileName = "newItemBase", menuName = "Items/Item")]
public class ItemBase : ScriptableObject
{
    public string itemName;
    public int maxStackSize;
    
    public Sprite itemIcon;

    public virtual void Use()
    {
        Debug.Log("Using " + itemName);
    }
}