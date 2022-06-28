using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyTile : Tile
{
    [HideInInspector] public int cycleIndex;
    
    public virtual void Start()
    {
        TurnManager.instance.TurnHappen += IncreaseCycleIndex;
    }
    /// <summary>
    /// keeps track of the point in the cycle bigger enemies are in
    /// currently only the skeleton uses this, but if pushed further, other bigger enemies can also use this
    /// </summary>
    public void IncreaseCycleIndex()
    {
        cycleIndex++;

        if (cycleIndex > info.movesInCycle)
        {
            cycleIndex = 1;
        }
    }

}

