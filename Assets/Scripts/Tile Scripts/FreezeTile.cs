using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeTile : Tile
{
    public GameObject FreezeScreen;

    //most of this is held in the player controller script as that is where the next turn is called
    //so in here only the freeze screen is controlled (the visuals)


    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (collision.gameObject.CompareTag("Player"))
        {
            FreezeScreen.SetActive(true);
        }
    }

    public override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);

        if (collision.gameObject.CompareTag("Player"))
        {
            FreezeScreen.SetActive(false);
        }
    }
}
