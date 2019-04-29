using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FollowingProjectile : MonoBehaviour {
    //projectile to follow enemies around
    //target
    public Enemy enemyTarget;
    //speed
    public float moveSpeed = 15f;

    private void Update()
    {
        if(enemyTarget == null)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.LookAt(enemyTarget.transform);
            GetComponent<Rigidbody>().velocity = transform.forward * moveSpeed;
        }
    }
    //when projectile hits enemy do damage
    public void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Enemy>() == enemyTarget)
        {
            OnEnemyHit();
        }
    }
    //logic for when an enemy is hit to be described by child
    protected abstract void OnEnemyHit();

}
