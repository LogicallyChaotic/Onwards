using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventorySlot : MonoBehaviour
{
    public Sprite emptySlot; 
    public bool inUse;

    [SerializeField] Color togOnCol;
    [SerializeField] Color TogOffCol;

    public Inventory_Manager.pickup pickUpType;
    [SerializeField] Inventory_Manager inventoryMan;

    private bool keySoundPlayed;
    private bool swordSoundPlayed;

    public void Start()
    {
        TurnManager.instance.UseItem += ObjectUsed;
        GetComponent<Toggle>().isOn = false;
    }

    /// <summary>
    /// if the toggle is on, change the colour slightly and play the sound of the object being picked up, tell the player the object is presently being held
    /// if the toggle is off, change the colour back and play the sound of the object being dropped, and let the player know the object is not held but in inventory again
    /// </summary>
    public void Update()
    {
        if (inUse)
        {
            if (GetComponent<Toggle>().isOn)
            {
                this.GetComponent<Image>().color = togOnCol;

                if (pickUpType == Inventory_Manager.pickup.KEY)
                {
                    if (!keySoundPlayed)
                    {
                        inventoryMan.keys.Play();
                        keySoundPlayed = true;

                    }
                    PlayerController.instance.KeyHeld = true;
                    
                }
                if (pickUpType == Inventory_Manager.pickup.SWORD)
                {

                    if (!swordSoundPlayed)
                    {
                        inventoryMan.sword.Play();
                        swordSoundPlayed= true;

                    }
                    PlayerController.instance.SwordHeld = true;
                    TurnManager.instance.EquipItemEvent();
                }
            }
            else
            {
                this.GetComponent<Image>().color = TogOffCol;

                if (pickUpType == Inventory_Manager.pickup.KEY)
                {
                    if (keySoundPlayed)
                    {
                        inventoryMan.keysAway.Play();
                        keySoundPlayed = false;
                    }
                    
                    PlayerController.instance.KeyHeld = false;
                    
                }
                if (pickUpType == Inventory_Manager.pickup.SWORD)
                {
                    if (swordSoundPlayed)
                    {
                        inventoryMan.swordAway.Play(); 
                        swordSoundPlayed = false;
                    }
                    PlayerController.instance.SwordHeld = false;
                    TurnManager.instance.EquipItemEvent();
                    
                }
            }
        }
    }

    public void ObjectUsed()
    {
        switch (pickUpType)
        {
            case Inventory_Manager.pickup.KEY:
                PlayerController.instance.KeyHeld = false;
                break;
            case Inventory_Manager.pickup.SWORD:
                PlayerController.instance.SwordHeld = false;
                break;
            default:
                break;
        }
    }
}
