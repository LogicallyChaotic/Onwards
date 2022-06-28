using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int Level;
    public int livesLeft;

    //data that needs to be saved
    public SaveData(int lives, int level)
    {
        livesLeft = lives;
        Level = level;
    }
}