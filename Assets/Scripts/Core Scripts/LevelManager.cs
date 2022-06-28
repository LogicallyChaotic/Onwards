using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Animator circleScreen;
    [SerializeField] GameObject sparkle;
    [SerializeField] List<GameObject> canvases;
    [SerializeField] PlayerController Player;
    [SerializeField] health health;

    private static LevelManager _instance;
    private void Awake()
    {
        if (instance == null)
        {
            _instance = this;
        }
    }
    public static LevelManager instance
    {
        get { return _instance; }
    }

    void Start()
    {
        StartCoroutine(StartLevel());
    }
    /// <summary>
    /// set up the scene to start the next level ie. setting canvases unactive and showing the player sparkle of sucessfully finishing the level
    /// </summary>
    /// <param name="waittime"> time to wait between the player particle sparkle and starting the next scene</param>
    /// <returns></returns>
    public IEnumerator ChangeLevel(int waittime)
    {
        sparkle.SetActive(true);
        yield return new WaitForSeconds(waittime);
        foreach (GameObject canvas in canvases)
        {
            canvas.SetActive(false);
        }
        
        StartCoroutine(nxtLevel());
    }
    /// <summary>
    /// start the next scene and save the amount of lives the player currently has
    /// </summary>
    /// <returns></returns>
    public IEnumerator nxtLevel()
    {
        sparkle.SetActive(false);
        circleScreen.SetTrigger("open");
        yield return new WaitForSeconds(0.6f);
        
        if (SceneManager.GetActiveScene().buildIndex > 0)
        {
            SaveSystem.SaveInfo(health.lives, SceneManager.GetActiveScene().buildIndex);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    // load up the new scene and start by loading how many lives the player has, and setting each cavnas active again
    public IEnumerator StartLevel()
    {
        yield return new WaitForSeconds(0.6f);

        if (SceneManager.GetActiveScene().buildIndex > 1)
        {
            SaveData data = SaveSystem.LoadData();
            health.lives = data.livesLeft;
            health.UpdateHealth();
        }

        foreach (GameObject canvas in canvases)
        {
            canvas.SetActive(true);
        }
    }
}