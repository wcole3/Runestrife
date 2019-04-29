using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTower : Tower {
    //fire tower script
    //setup the particles
    public GameObject fireParticlesPrefab;

    protected override void AttackEnemy()
    {
        base.AttackEnemy();
        //spawn the fire particles
        GameObject particles = (GameObject)Instantiate(fireParticlesPrefab, transform.position + 
            new Vector3(0, 0.5f), fireParticlesPrefab.transform.rotation);
        particles.transform.localScale *= aggroRadius / 10f;
        //damage all enemies in range
        foreach(Enemy enemy in EnemyManager.Instance.GetEnemiesInRange(transform.position, aggroRadius))
        {
            enemy.TakeDamage(attackPower);
        }
    }
}
