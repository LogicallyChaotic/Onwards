using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs_Tile : Tile
{
    [SerializeField] private bool locked;
    [SerializeField] Inventory_Manager inventory;
    private bool canMoveTo;
    [SerializeField] string nextLevel;
    public void Start()
    {
        locked = info.NeedKeyToOpen;
        CanStepOn = locked ? false : true;
    }
    //check constantly if a key is present with the player, which allows the player to move onto it

    public void Update()
    {
        if (info.NeedKeyToOpen)
        {
            HasKey();
        }
        if (canMoveTo)
        {
            if (!locked)
            {
                TileStatus(true);
            }
            else
            {
                TileStatus(false);
            }
        }
    }
   
    /// <summary>
    /// if a key is needed for this stairs, the player would have to have had the key and then been able to click on the tile
    /// update the inventory and then start the next level
    /// </summary>
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (collision.gameObject.CompareTag("Player"))
        {
            if (info.NeedKeyToOpen)
            {
                this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                inventory.InventoryUpdate(null, false, Inventory_Manager.pickup.KEY);
            }

            StartCoroutine(LevelManager.instance.ChangeLevel(1));
        }
    } 
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("playerhit"))
        {
            canMoveTo = true;
        }
    }

    public override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);

        if (collision.gameObject.CompareTag("playerhit"))
        {
            canMoveTo = false;
        }
    }
    //if player has the keys make sure the door is unlocked and can be stepped on
    private void HasKey()
    {
        if(PlayerController.instance.KeyHeld)
        {
            locked = false;
            CanStepOn = true;
        }
        if (!PlayerController.instance.KeyHeld)
        {
            locked = true;
            CanStepOn = false;
        }
    }
}
