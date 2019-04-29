using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : FollowingProjectile {
    //ice projectile
    protected override void OnEnemyHit()
    {
        enemyTarget.Freeze();
        Destroy(gameObject);
    }
}
