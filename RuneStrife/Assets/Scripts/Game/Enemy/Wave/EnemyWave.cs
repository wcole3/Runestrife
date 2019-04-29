using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//class describing the waves that spawn
[Serializable]
public class EnemyWave {
    //path to take
    public int pathIndex;
    //when to start spawning
    public float startSpawnTime;
    //time between spawns
    public float timeBetweenSpawns = 1f;
    //enemies in the wace
    public List<GameObject> listOfEnemies = new List<GameObject>();
	
}
