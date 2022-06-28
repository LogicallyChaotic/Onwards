using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Tile : Tile
{
    #region variables and fields
    public bool PickUpGot;
    [SerializeField] Inventory_Manager.pickup pickupType;
    [SerializeField] Inventory_Manager inventory = null;
    [SerializeField] AudioSource pickupsound;

    #endregion
    #region unityMethods

    public void Start()
    {
        TurnManager.instance.HurtPlayer += ItemRemove;
    }
    //if the pickup hasn't already been taken, take the pickup, play the sound and update the inventory
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (!PickUpGot)
        {
            if (collision.CompareTag("Player"))
            {
                this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                pickupsound.Play();

                if (pickupType != Inventory_Manager.pickup.GEM)
                {
                    ItemtoGet();
                }
                else
                {
                    Instantiate(info.sparkleGem, null);
                }

                PickUpGot = true;
            }
        }
    }
    #endregion
    #region other functions
    //update the inventory either removing or adding items, 
    public void ItemtoGet() => inventory.InventoryUpdate(info.pickUpobject, true, pickupType);
    public void ItemRemove() { inventory.InventoryUpdate(info.pickUpobject, false,pickupType); PickUpGot = false; this.gameObject.transform.GetChild(0).gameObject.SetActive(true); }

    #endregion
}
