using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DeathTile : Tile
{
    //very similar to attackTrigger, however it now inherits from Tile
    //if made contact with the tile, throw the player back and do the hurt event;
    [SerializeField] AudioSource hit;
    [SerializeField] Transform hitThowbackPlace;
    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);

        if (other.gameObject.CompareTag("Player"))
        {
            hit.Play();
            TurnManager.instance.PlayerHurtEvent();
            PlayerController.instance.MovePlayer(hitThowbackPlace.gameObject);
        }
    }
}
