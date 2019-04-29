using UnityEngine;

public class StoneTower : Tower {
    //stone tower script
    public GameObject stonePrefab;


    protected override void AttackEnemy()
    {
        base.AttackEnemy();
        //get a projectile
        GameObject stone = (GameObject)Instantiate(stonePrefab, launcherTransform.position, Quaternion.identity);
        //setup stone
        stone.GetComponent<Stone>().enemyTarget = enemyTarget;
        stone.GetComponent<Stone>().damage = attackPower;
    }
}
