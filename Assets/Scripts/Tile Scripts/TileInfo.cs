using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tile_Info", menuName = "GameDataInfo/TileInfo", order = 1)]
[System.Serializable]
public class TileInfo : ScriptableObject
{
    public Sprite unused;
    public Sprite highlighted;
    public Color HighlightColor;
    public enum tileType
    {
        STAIRS,
        NORMAL,
        ENEMY,
        PICKUP,
        MOVING,
        FREEZE
    }
    public tileType typeOfTile;

    //pickup tile info
    public bool isKey;
    public bool isSword;   
    public bool isGem;
    [HideInInspector] public Sprite pickUpobject;
    public GameObject sparkleGem;
 

    //stair tile info
    public bool NeedKeyToOpen;

    //enemy tile info
    public enum attackDirection
    {
        LEFT,
        RIGHT,
        UP,
        DOWN
    }
    public attackDirection attackDir;
    public int movesInCycle;
    public int attackMoveNum;
    public int defendMoveNum;
    public int prepareNum;
}