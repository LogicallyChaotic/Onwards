using System;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    private static TurnManager _instance;
    public int TurnCount;
    private void Awake()
    {
        if (instance == null)
        {
            _instance = this;
        }
    }

    public static TurnManager instance
    {
        get { return _instance; }
    }

    //the turn manager holds all unity events and unity actions in a static instance so that they can be accessed by all
    //scripts, this allows for different scripts to subscribe to Actions and then get called in various places
    //essentially, if the actions have be subscribed to, play them when the functions are called;



    public event Action TurnHappen;
    public void TurnStartEvent()
    {
        if (TurnHappen != null)
        {
            TurnHappen();
        }
    }
    public event Action EquipItem;
    public void EquipItemEvent()
    {
        if (EquipItem != null)
        {
            EquipItem();
        }
    }

    public event Action UseItem;
    public void UseItemEvent()
    {
        if (UseItem != null)
        {
            UseItem();
        }
    }

    public event Action HurtPlayer;
    public void PlayerHurtEvent()
    {
        if (HurtPlayer != null)
        {
            HurtPlayer();
        }
    }

}
