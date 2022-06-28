using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Inventory_Manager : MonoBehaviour
{
    public List<InventorySlot> inventorySlots;
    public AudioSource keys, keysAway;
    public AudioSource sword, swordAway;

    public enum pickup
    {
        KEY,
        SWORD,
        GEM
    }
    public void Start()
    {
        TurnManager.instance.UseItem += UsedItem;
    }

    /// <summary>
    /// if an iteam is added, loop through the inventory slots to see if any are not in use
    /// and send it information about what the sprite/the item is and set it to inuse
    /// otherwise, set it to not in use and change the sprite image to empty;
    /// </summary>
    /// <param name="sprite"> the sprite of the pickup</param>
    /// <param name="toAdd"> whether the object is added or removed</param>
    /// <param name="PickUpType"> the type of pickup</param>
    public void InventoryUpdate(Sprite sprite, bool toAdd, pickup PickUpType)
    {
        if (toAdd)
        {
            foreach (InventorySlot slot in inventorySlots)
            {
                if (!slot.inUse)
                {
                    slot.GetComponent<Image>().sprite = sprite;
                    slot.inUse = true;
                    slot.pickUpType = PickUpType;

                    break;
                }
            }
        }
        else
        {
            foreach (InventorySlot slot in inventorySlots)
            {
                if (slot.inUse)
                {
                    slot.GetComponent<Image>().sprite = slot.emptySlot;
                    slot.inUse = false;
                    break;
                }
            }
        }
    }

    /// <summary>
    /// this gets called when an item is used, by the turn manager
    /// repeats some of the code from above- would change this if I had more time
    /// overall if the item in that particular slot has been used, which it does through checking the pickuptype
    /// and ensuring the player is removing the item, clear the sprite and set everything back to default;
    /// </summary>

    public void UsedItem()
    {
        if (PlayerController.instance.SwordRemoved)
        {
            foreach (InventorySlot slot in inventorySlots)
            {
                if (slot.inUse && slot.pickUpType == pickup.SWORD)
                {
                    slot.GetComponent<Image>().sprite = slot.emptySlot;
                    slot.inUse = false;
                    PlayerController.instance.SwordHeld = false;
                    PlayerController.instance.SwordRemoved = false;
                    break;
                }

            }
        }

        if (PlayerController.instance.KeyRemoved)
        {
            foreach (InventorySlot slot in inventorySlots)
            {
                if (PlayerController.instance.KeyHeld)
                {
                    if (slot.inUse && slot.pickUpType == pickup.KEY)
                    {
                        slot.GetComponent<Image>().sprite = slot.emptySlot;
                        slot.inUse = false;
                        PlayerController.instance.KeyHeld = false;
                        PlayerController.instance.KeyRemoved = false;
                        break;
                    }
                }
            }
        }
    }
}