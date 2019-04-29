using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//the different types of towers
public enum TowerType
{
    Stone, Fire, Ice
}

public class Tower : MonoBehaviour {
    //parent class of all tower objects
    //power
    public float attackPower = 3f;
    //fire rate
    public float timeBetweenAttacks = 1f;
    //aggro radius
    public float aggroRadius = 15f;
    //current level
    public int towerLevel = 1;
    //the type of tower
    public TowerType type;
    //shooting clip
    public AudioClip shootClip;
    //transform of launcher; this will aim at the current enemy
    public Transform launcherTransform;
    //current target
    public Enemy enemyTarget = null;
    //time since last attack
    private float attackCounter;

    public virtual void Update()
    {
        //constantly attack enemies in range
        attackCounter -= Time.deltaTime;
        if (enemyTarget == null)
        {
            //no current enemy, reset aiming point
            if (launcherTransform)
            {
                AimAtTarget(launcherTransform.transform.position - new Vector3(0, 0, 1));
            }
            //find new target
            Enemy testEnemy = GetNearestEnemyInRange();
            //make sure target is still in range
            if (testEnemy && (Vector3.Distance(transform.position, testEnemy.transform.position) <= aggroRadius))
            {
                enemyTarget = testEnemy;
            }
        }
        else
        {
            //we have a target
            if (launcherTransform)
            {
                AimAtTarget(enemyTarget.transform.position);
            }
            if(attackCounter <= 0f)
            {
                AttackEnemy();
                attackCounter = timeBetweenAttacks;//set attack cooldown
            }
            //check if target leaves range
            if(Vector3.Distance(transform.position, enemyTarget.transform.position) > aggroRadius)
            {
                enemyTarget = null;
            }
        }

    }

    //method to aim at the target
    public void AimAtTarget(Vector3 target)
    {
        launcherTransform.rotation = UtilityMethods.SmoothLook(launcherTransform, target);
    }

    //attack the enemy
    protected virtual void AttackEnemy()
    {
        GetComponent<AudioSource>().PlayOneShot(shootClip);
        //child classes will build on this
    }

    //find targets in aggro radius
    public List<Enemy> GetEnemiesInRange()
    {
        List<Enemy> enemiesInRange = new List<Enemy>();
        foreach(Enemy enemy in EnemyManager.Instance.Enemies)
        {
            if(Vector3.Distance(transform.position, enemy.transform.position) <= aggroRadius)
            {
                enemiesInRange.Add(enemy);
            }
        }

        return enemiesInRange;
    }
    //find the nearest enemy
    public Enemy GetNearestEnemyInRange()
    {
        Enemy nearestEnemy = null;
        //find all enemies in range
        float smallestDistance = float.PositiveInfinity;
        foreach(Enemy enemy in GetEnemiesInRange())
        {
            //check distance
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy <= smallestDistance)
            {
                nearestEnemy = enemy;
                smallestDistance = distanceToEnemy;
            }
        }
        return nearestEnemy;
    }

    //increase the tower specs on levelup
    public void LevelUp()
    {
        towerLevel++;
        //increase performance
        attackPower *= 2;
        timeBetweenAttacks *= 0.7f;
        aggroRadius *= 1.2f;
    }

    //show tower info
    public void ShowTowerInfo()
    {
        UIManager.Instance.ShowTowerInfoWindow(this);
    }
}
