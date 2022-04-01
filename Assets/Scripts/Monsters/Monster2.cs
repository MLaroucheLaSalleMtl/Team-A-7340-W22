using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster2 : EnemyAI
{
    private Monster2Spawn m2Spawn;
    // Start is called before the first frame update
    void Start()
    {
        m2Spawn = GameObject.Find("M2Spawn").GetComponent<Monster2Spawn>();
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
        m2Spawn.enemyCount--;
        m2Spawn.hasReachedMax = false;
    }
}
