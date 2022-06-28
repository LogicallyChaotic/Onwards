using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AttackTrigger : MonoBehaviour
{
    [SerializeField]AudioSource hit;
    [SerializeField] EnemyTile enemyScript;
    [SerializeField] Transform hitThowbackPlace;

    // if the attack trigger it touching the player, trigger the player hurt event
    //and throw the player back one space
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            hit.Play();
            TurnManager.instance.PlayerHurtEvent();
            PlayerController.instance.MovePlayer(hitThowbackPlace.gameObject);
        }
    }
}
