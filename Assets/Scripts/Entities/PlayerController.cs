using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region fields & variables

    [Header("movement variables")]
    [SerializeField] float speed = 5;
    [SerializeField] Transform movePoint;
    [SerializeField] LayerMask ground;

    [Header("audio variables")]
    [SerializeField] List<AudioSource> step;
    [SerializeField] AudioSource freeze;

    [Header("PickUpobjects held")]
    public bool KeyHeld;
    public bool SwordHeld;
    public bool SwordRemoved;
    public bool KeyRemoved;

    [HideInInspector] public bool Gameover= false;
    //instance and private variables
    private static PlayerController _instance;
    public static PlayerController instance
    {
        get { return _instance; }
    }
    private Vector2 mousePos;
    private int Sound = 0;
    #endregion

    #region unity functions
    //create an instance 
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    /// <summary>
    /// unparent the movepoint(parented for neatness at the moment)
    /// add 'emptyhanded' to the turn manager so that it is called everytime
    /// a player gets hurt leading to them dropping all their items.
    /// </summary>
    void Start()
    {
        movePoint.parent = null;
        TurnManager.instance.HurtPlayer += EmptyHanded;
    }
    /// <summary>
    /// movement for the player, uses raycast to check if the place hit has a collider, then checks for
    /// the information in the tile to assess before making a move.
    /// calls on the instance of turn manager and starts the next turn if not a freeze tile
    /// </summary>
    void Update()
    {
        float movementAmout = speed * Time.deltaTime;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0) && !Gameover)
        {
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit.collider.GetComponent<Tile>() != null)
            {
                var tile = hit.collider.gameObject;

                if (tile.GetComponent<Tile>().InRange && tile.GetComponent<Tile>().CanStepOn)
                {
                    if (tile.GetComponent<Tile>().info.typeOfTile != TileInfo.tileType.FREEZE)
                    {
                        TurnManager.instance.TurnCount++;
                        TurnManager.instance.TurnStartEvent();
                    }
                    else
                    {
                        freeze.Play();
                    }
                    StartCoroutine(DoOtherAction(tile));
                    
                }
            }
        }
    }
    public void EmptyHanded()
    {
        SwordHeld = false;
        KeyHeld = false;
    }

    //move the player to the movepoint position and play the walking step audio
    public void MovePlayer(GameObject transformObj)
    {
        movePoint.transform.position = transformObj.transform.position;
        this.transform.localPosition = movePoint.position;
        StepAudio();
    }

    /// <summary>
    /// check to see if tile is an enemytile(opened it out so more types of tiles can be added)
    /// then trigger useitemevent which only does something when the player is holding an item
    /// indicated by swordHeld and keyHeld
    /// </summary>
    /// <param name="tile"> the raycasted object that was hit</param>
    /// <returns></returns>
    public IEnumerator DoOtherAction(GameObject tile)
    {
        var typeoftile = tile.GetComponent<Tile>().info.typeOfTile;
        if (typeoftile == TileInfo.tileType.ENEMY)
        {
            SwordRemoved = true;
            TurnManager.instance.UseItemEvent();
            yield return new WaitForSeconds(0.2f);
        }
        MovePlayer(tile);
    }

    #endregion

    #region other functions

    /// <summary>
    /// alternate the footstep audio by switching between the two sounds.
    /// </summary>
    public void StepAudio()
    {
        step[Sound].Play();
        Sound++;

        if (Sound == 2)
        {
            Sound = 0;
        }
    }
    #endregion
}
