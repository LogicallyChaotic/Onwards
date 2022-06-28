using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skeleton : EnemyTile
{
    #region Variables and Fields
    [Header("relevant game areas and anims")]
    public Animator anim;
    public GameObject hit;
    public GameObject warning;
    public GameObject shield;


    [Header("Audio")]
    public AudioSource hitDeath;


    [Header("loot dropped")]
    public GameObject dropObj;

    private int attackdir;
    private bool isDefending;
    private bool Dead;
    #endregion

    #region unity Methods

    //make sure the skeleton is attacking in the right direction
    public override void Start()
    {
        base.Start();

        TurnManager.instance.TurnHappen += CycleIndexChanged;
        TurnManager.instance.UseItem += EnemyHit;
        TurnManager.instance.EquipItem += hasPlayerSword;
        TurnManager.instance.HurtPlayer += hasPlayerSword;

        CanStepOn = false;

        switch (info.attackDir)
        {
            case TileInfo.attackDirection.LEFT:
                attackdir = 1;
                break;
            case TileInfo.attackDirection.RIGHT:
                attackdir = 2;
                break;
            case TileInfo.attackDirection.UP:
                attackdir = 3;
                break;
            case TileInfo.attackDirection.DOWN:
                attackdir = 4;
                break;
            default:
                break;
        }
        anim.SetInteger("attackplace", attackdir);
    }

    #endregion

    #region other functions & ienumerators

    //everytime a turn is started, the cycleindex gets changed, and this function gets called to carry out the behaviours that
    //animate and allow the skeleton to attack
    public void CycleIndexChanged()
    {
        if (cycleIndex == info.attackMoveNum && !Dead)
        {
            warning.SetActive(false);
            shield.SetActive(false);
            anim.SetTrigger("attack");
            StartCoroutine(c_attack());
            isDefending = false;
        }
        if (cycleIndex == info.defendMoveNum && !Dead)
        {
            shield.SetActive(true);
            hit.SetActive(false);
            warning.SetActive(false);
            anim.SetTrigger("defend");
            isDefending = true;
        }
        if (cycleIndex == info.prepareNum && !Dead)
        {
            shield.SetActive(false);
            hit.SetActive(false);
            warning.SetActive(true);
            anim.SetTrigger("prepare");
            isDefending = false;

        }
    }
    /// <summary>
    /// if the player is not dead and has the sword while qualifying all the necessary parameters to be in range
    /// the tile can be stepped on 
    /// </summary>
   
    public void hasPlayerSword()
    {
        if (!Dead && PlayerController.instance.SwordHeld && !isDefending && InRange)
        {
            CanStepOn = true;
        }
        else if (!Dead)
        {
            CanStepOn = false;
        }

        TileStatus(CanStepOn);

    }
    //once the enemy has been hit and dies, set all the children of the tile unactive
    //if a object is dropped, set it's tile to active
    public void EnemyHit()
    {
        if (CanStepOn)
        {
            hit.SetActive(false);
            shield.SetActive(false);
            warning.SetActive(false);

            if (transform.childCount > 0)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).gameObject.SetActive(false);
                }
            }
            
            Dead = true;
            CanStepOn = true;

            if (dropObj != null)
            {
                if(dropObj.transform.parent != null)
                {
                    dropObj.transform.parent = null;
                }

                dropObj.SetActive(true);
                this.gameObject.SetActive(false);
            }
            enabled = false;
        }
    } 
    
    public IEnumerator c_attack()
    {
        hit.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        hit.SetActive(false);
    }
    #endregion
}