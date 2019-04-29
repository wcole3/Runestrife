using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTower : Tower {
    //ice tower script
    public GameObject icePrefab;

    //find nofrozen enemy
    private void FindNonFrozenTarget()
    {
        foreach (Enemy enemy in EnemyManager.Instance.GetEnemiesInRange(transform.position, aggroRadius))
        {
            if (!enemy.frozen)
            {
                enemyTarget = enemy;
                break;
            }
        }
    }
    protected override void AttackEnemy()
    {
        base.AttackEnemy();
        GameObject ice = (GameObject)Instantiate(icePrefab, launcherTransform.position, Quaternion.identity);
        ice.GetComponent<Ice>().enemyTarget = enemyTarget;
    }
    public override void Update()
    {
        base.Update();
        FindNonFrozenTarget();//make sure target isnt frozen
    }
}
