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
        if (base.isDead)
        {
            Stop();
        }
    }

    public override void Stop()
    {
        base.agent.SetDestination(transform.position);
        transform.LookAt(transform.position);
    }

    public override void MonsterDeath()
    {
        base.MonsterDeath();
        m1Spawn.enemyCount--;
        m1Spawn.hasReachedMax = false;
    }
}

    
