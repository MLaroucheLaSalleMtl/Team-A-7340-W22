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
        m2Spawn.enemyCount--;
        m2Spawn.hasReachedMax = false;
    }
}
