using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {
    //Manager enemy waves
    public static WaveManager Instance;
    //the waves in the game
    public List<EnemyWave> enemyWaves = new List<EnemyWave>();

    //time in game
    private float timeElapsed = 0f;
    //active wave
    private EnemyWave activeWave;
    //when last spawn happened
    private float spawnCounter;
    //waves that have been activated
    private List<EnemyWave> activatedWaves = new List<EnemyWave>();

    //this is a singleton
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);//there can only be one
        }
    }

    //update
    private void Update()
    {
        timeElapsed += Time.deltaTime;
        SearchForWave();//find active wave
        UpdateActiveWave();//do spawning 
    }
    //Find active wave
    private void SearchForWave()
    {
        foreach(EnemyWave wave in enemyWaves)
        {
            if(!activatedWaves.Contains(wave) && wave.startSpawnTime <= timeElapsed)
            {
                //if wave hasnt been activated and the start time has passed
                activeWave = wave;
                activatedWaves.Add(wave);
                spawnCounter = 0f;
                GameManager.Instance.waveNumber++;
                UIManager.Instance.ShowCenterWindow("Wave " + GameManager.Instance.waveNumber);
                break;
            }
        }
    }

    //do spawning for active wave
    private void UpdateActiveWave()
    {
        if(activeWave != null)
        {
            spawnCounter += Time.deltaTime;//update spawn timer
            if(spawnCounter >= activeWave.timeBetweenSpawns)
            {
                spawnCounter = 0f;
                //spawn next enemy
                if(activeWave.listOfEnemies.Count != 0)
                {
                    //always take next enemy from top of list
                    GameObject enemy = (GameObject)Instantiate(activeWave.listOfEnemies[0],
                        WaypointManager.Instance.FindSpawnPoint(activeWave.pathIndex), Quaternion.identity);
                    enemy.GetComponent<Enemy>().pathIndex = activeWave.pathIndex;
                    activeWave.listOfEnemies.RemoveAt(0);//take away spawned enemy
                }
                else
                {
                    //the list has been spawned, move to next one
                    activeWave = null;
                    if(activatedWaves.Count == enemyWaves.Count)
                    {
                        GameManager.Instance.enemySpawningOver = true;
                    }
                }
            }
        }
    }

    //stop spawning enemies
    public void StopSpawning()
    {
        timeElapsed = 0;
        spawnCounter = 0f;
        activeWave = null;
        activatedWaves.Clear();
        enabled = false;
    }
}
