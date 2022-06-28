using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTile : Tile
{
    #region variables and fields 
    public List<currentTile> movepoints;
    #endregion

    #region unityMethods
    public void Start()
    {
        TurnManager.instance.TurnHappen += Move;
    }

    #endregion
    //at the start of everyturn move the movetile to the relevant place and indicate this by setting the movepoint to inUse
    //there can only be 2 movepoints if this tile moves across a gap as it is turn based

    #region other functions
    public void Move()
    {
        if (this.gameObject.activeInHierarchy)
        {
            if (movepoints[0].inUse)
            {
                movepoints[1].inUse = true;
                this.gameObject.transform.position = movepoints[1].gameObject.transform.position;
                movepoints[0].inUse = false;
            }
            else if (movepoints[1].inUse)
            {
                movepoints[0].inUse = true;
                this.gameObject.transform.position = movepoints[0].gameObject.transform.position;
                movepoints[1].inUse = false;
            }
        }
    }
}
#endregion