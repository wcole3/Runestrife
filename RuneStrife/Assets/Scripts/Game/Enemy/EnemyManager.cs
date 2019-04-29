using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
    //manager class for enemies
    public static EnemyManager Instance;//singleton
    //the enemies in game
    public List<Enemy> Enemies = new List<Enemy>();

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    //register enemy as it spawns
    public void RegisterEnemy(Enemy enemy)
    {
        Enemies.Add(enemy);
        //add health bar
        UIManager.Instance.CreateHealthBarForEnemy(enemy);
    }
    //unregister
    public void UnregisterEnemy(Enemy enemy)
    {
        Enemies.Remove(enemy);
    }

    //method to find enemies in range of a certain point
    public List<Enemy> GetEnemiesInRange(Vector3 position, float range)
    {
        return Enemies.Where(enemy => Vector3.Distance(enemy.transform.position, 
            position) <= range).ToList();//find all enemies in range
    }
    //destory all enemies
    public void DestroyAllEnemies()
    {
        foreach(Enemy enemy in Enemies)
        {
            Destroy(enemy.gameObject);
        }
        Enemies.Clear();
    }
}
