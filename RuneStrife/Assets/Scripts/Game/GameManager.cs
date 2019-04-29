using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    //manager class for the game
    public static GameManager Instance;//singleton
    public int gold;//player gold
    public int waveNumber;//current wave
    public int escapedEnemies;//the ones that got away
    public int maxAllowedEnemies = 5;//lose con
    public bool enemySpawningOver;//see if all enemies have spawned; public bc it is set elsewhere
    //clips for game win and loss
    public AudioClip gameWinClip;
    public AudioClip gameloseClip;

    private bool gameOver = false;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if(!gameOver && enemySpawningOver)
        {
            //check for win con
            if(EnemyManager.Instance.Enemies.Count == 0)
            {
                //do game win stuff
                OnGameWin();
            }
        }
        //quit to title screen on esp
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitToTitleScreen();
        }
    }

    private void OnGameWin()
    {
        UIManager.Instance.ShowWinScreen();
        AudioSource.PlayClipAtPoint(gameWinClip, Camera.main.transform.position);
        gameOver = true;
    }

    public void QuitToTitleScreen()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    //count the escaped enemies
    public void OnEnemyEscape()
    {
        escapedEnemies++;
        UIManager.Instance.ShowDamageScreen();
        if(escapedEnemies == maxAllowedEnemies)
        {
            //player has lost
            OnGameLose();
        }
    }
    //do game loss
    private void OnGameLose()
    {
        UIManager.Instance.ShowLoseScreen();
        gameOver = true;
        AudioSource.PlayClipAtPoint(gameloseClip, Camera.main.transform.position);
        //clear the remaining enemies
        EnemyManager.Instance.DestroyAllEnemies();
        WaveManager.Instance.StopSpawning();
    }
    //check if player want to try again
    public void RetryLevel()
    {
        SceneManager.LoadScene("Game");
    }
}
