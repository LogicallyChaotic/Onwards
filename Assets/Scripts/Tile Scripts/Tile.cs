using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    #region variables and fields

    [Header("base tile information")]
    public TileInfo info;
    public bool CanStepOn = true;
    public bool InRange;

    #endregion
    #region unity Methods
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("playerhit"))
        {
            InRange = true;

            if (CanStepOn)
            {
                TileStatus(true);
            }
        }
    }
    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("playerhit"))
        {
            InRange = false;

            if (CanStepOn)
            {
                TileStatus(false);
            }
        }
    }
    #endregion
    #region other functions

    /// <summary>
    /// changes the appearance of the tile depending on if it is near the player to be selected;
    /// </summary>
    /// <param name="canStepOn"> if the tile can be stepped on</param>
    public virtual void TileStatus(bool canStepOn)
    {
        SpriteRenderer thisSprite = this.gameObject.GetComponent<SpriteRenderer>();

        thisSprite.sprite = canStepOn ? info.highlighted : info.unused;
        thisSprite.color = canStepOn ? info.HighlightColor : Color.white;
    }

    #endregion
}
