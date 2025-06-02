using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    void Update()
    {
        PickItemToInventory();
    }

    private void PickItemToInventory()
    {
        GameObject itemPick = PlayerManager.instance.playerClosestItem.currentHighlight;

        if (itemPick)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //if (PlayerManager.instance.playerInventory.AddItem(itemPick.GetComponent<itemManager>().instance))
                //    Destroy(itemPick);
            }
        }
    }
}