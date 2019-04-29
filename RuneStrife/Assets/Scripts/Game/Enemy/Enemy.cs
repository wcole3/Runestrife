/*
 * Copyright (c) 2018 Razeware LLC
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * Notwithstanding the foregoing, you may not use, copy, modify, merge, publish, 
 * distribute, sublicense, create a derivative work, and/or sell copies of the 
 * Software in any work that is designed, intended, or marketed for pedagogical or 
 * instructional purposes related to programming, coding, application development, 
 * or information technology.  Permission for such use, copying, modification,
 * merger, publication, distribution, sublicensing, creation of derivative works, 
 * or sale is expressly withheld.
 *    
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxHealth = 100f;
    public float health = 100f;
    public float moveSpeed = 3f;
    public int goldDrop = 10;
    public float timeToStayFrozen = 2f;
    public bool frozen;

    public int pathIndex = 0;

    private int wayPointIndex = 0;
    private float timeFrozen;

    //on start register enemey
    private void Start()
    {
        EnemyManager.Instance.RegisterEnemy(this);
    }

    void OneGotToLastWayPoint()
    {
        GameManager.Instance.OnEnemyEscape();
        Die();
    }

    public void TakeDamage(float amountOfDamage)
    {
        health -= amountOfDamage;

        if (health <= 0)
        {
            DropGold();
            Die();
        }
    }

    //give gold on kill
    private void DropGold()
    {
        GameManager.Instance.gold += goldDrop;
    }

    void Die()
    {
        if (gameObject != null)
        {
            //unregister
            EnemyManager.Instance.UnregisterEnemy(this);
            //shrink the enemy on death
            gameObject.AddComponent<AutoScaler>().scalerSpeed = -2f;
            enabled = false;
            Destroy(gameObject, 0.3f);
        }
    }
    //update the enemy movement
    private void UpdateMovement()
    {
        //move to the next waypoint
        Vector3 target = WaypointManager.Instance.Paths[pathIndex].WayPoints[wayPointIndex].position;
        transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        transform.localRotation = UtilityMethods.SmoothLook(transform, target);
        //check if the enemy has reached the target
        if(Vector3.Distance(transform.position, target) < 0.1f)
        {
            wayPointIndex++;//set next target
        }
    }

    private void Update()
    {
        //figure out where the enemy is in the path
        if(wayPointIndex < WaypointManager.Instance.Paths[pathIndex].WayPoints.Count)
        {
            UpdateMovement();
        }
        else
        {
            //the enemy reached the end of the road
            OneGotToLastWayPoint();
        }
        //check if enemy is frozen
        if (frozen)
        {
            timeFrozen += Time.deltaTime;
            if(timeFrozen >= timeToStayFrozen)
            {
                Thaw();
            }
        }
    }
    //freeze enemy if hit by ice tower
    public void Freeze()
    {
        if (!frozen)
        {
            frozen = true;
            moveSpeed /= 2;//lower speed
        }
    }
    //thaw the enemy after set period of time
    private void Thaw()
    {
        timeFrozen = 0f;
        frozen = false;
        moveSpeed *= 2;//reset speed
    }
}
