using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class button : Tile
{
    [SerializeField] GameObject countDown;
    [SerializeField] int timeLimit;
    [SerializeField] int timer;
    [SerializeField] bool start;
    [SerializeField] List<Sprite> numbers;

    [SerializeField] AudioSource buttonPress;
    [SerializeField] GameObject firstSpike;
    [SerializeField] GameObject SecondSpike;
    [SerializeField] GameObject firstTile, secondTile;

    public void Start()
    {
        TurnManager.instance.TurnHappen += DecreaseTimer;
    }

    /// <summary>
    /// when a turn happens, the timer is decreased by one, and the numbers shown are also decreased
    /// when timer is zero switch the tiles back to the normal set up, the dangerous one now not dangerous the non dangerous one now dangerous
    /// </summary>
    public void DecreaseTimer()
    {
        if (start)
        {
            timer--;

            countDown.gameObject.GetComponent<SpriteRenderer>().sprite = numbers[timer];

            if (timer == 0)
            {
                start = false;

                SecondSpike.SetActive(false);
                secondTile.SetActive(true);
                firstSpike.SetActive(true);
                firstTile.SetActive(false);
                countDown.SetActive(false);
            }
        }
    }
    //when the player steps on the button, play the audio and signify that the timer can start counting down
    // the tiles then switch, the dangerous one now not dangerous the non dangerous one now dangerous
    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);

        if(other.gameObject.CompareTag("Player"))
        {
            buttonPress.Play();
            timer = timeLimit;
            start = true;

            countDown.gameObject.GetComponent<SpriteRenderer>().sprite = numbers[timer];
            countDown.SetActive(true);

            SecondSpike.SetActive(true);
            secondTile.SetActive(false);
            firstSpike.SetActive(false);
            firstTile.SetActive(true);
        }
    }
}
