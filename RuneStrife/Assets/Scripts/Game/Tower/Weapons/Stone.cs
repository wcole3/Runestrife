﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : FollowingProjectile {
    public float damage;
    protected override void OnEnemyHit()
    {
        enemyTarget.TakeDamage(damage);
        Destroy(gameObject);
    }

}
