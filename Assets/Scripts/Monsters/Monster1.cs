using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster1 : EnemyAI
{
    private Monster1Spawn m1Spawn;
    // Start is called before the first frame update
    void Start()
    {
        m1Spawn = GameObject.Find("M1Spawn").GetComponent<Monster1Spawn>();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (playerInSightRange && !playerInAttackRange && !playerIsDead && !attacking
                || isBeingAttacked && !playerInAttackRange && !playerIsDead && !attacking)
        {
            ChasePlayer();
        }

        if (base.isDead)
        {
            Stop(transform.position);
        }
    }

    public override void Stop(Vector3 value)
    {
        base.agent.SetDestination(value);
        transform.LookAt(value);
    }

    public override void MonsterDeath()
    {
        base.MonsterDeath();
        m1Spawn.enemyCount--;
        m1Spawn.hasReachedMax = false;
    }
}

    
