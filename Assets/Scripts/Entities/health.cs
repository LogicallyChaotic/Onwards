using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class health : MonoBehaviour
{
    public int lives;
    [Header("UI heart images")]
    [SerializeField] Image heart1;
    [SerializeField] Image heart2;
    [Header("sprites for the hearts below")]
    [SerializeField] Sprite fullHeart;
    [SerializeField] Sprite halfheart;
    [SerializeField] Sprite emptyheart;

    [SerializeField] GameObject gameoverScreen;

    public void Start()
    {
        TurnManager.instance.HurtPlayer += Damage;
    }

    //seperated out so could be used in an event as seen above, when the player has been hurt
    public void Damage()
    {
        ChangeHealth(true);
    }
    /// <summary>
    /// allows for health to be added or removed
    /// </summary>
    /// <param name="isGettingDamaged"> if the player is getting damaged or not</param>

    public void ChangeHealth(bool isGettingDamaged)
    {
        if(isGettingDamaged)
        {
            lives--;
            UpdateHealth();
        }
        else
        {
            lives++;
            UpdateHealth();
        }
    }

    // just in case I wanted to KO the player
    public void KO()
    {
        lives = 0;
        UpdateHealth();
    }

    //a switch statment which changes the appearance of the sprites through checking how many lives are avalaible to the player.
    public void UpdateHealth()
    {
        switch (lives)
        {
            case 4:
                heart1.sprite = fullHeart;
                heart2.sprite = fullHeart;
                break;
            case 3:
                heart1.sprite = fullHeart;
                heart2.sprite = halfheart;
                break;
            case 2:
                heart1.sprite = fullHeart;
                heart2.sprite = emptyheart;
                break;
            case 1:
                heart1.sprite = halfheart;
                heart2.sprite = emptyheart;
                break;
            case 0:
                heart1.sprite = emptyheart;
                heart2.sprite = emptyheart;
                gameoverScreen.SetActive(true);
                PlayerController.instance.Gameover = true;
                break;
        }
    }
}
