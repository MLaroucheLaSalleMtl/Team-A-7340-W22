using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster3 : EnemyAI
{
    private Monster3Spawn m3Spawn;
    // Start is called before the first frame update
    void Start()
    {
        m3Spawn = GameObject.Find("M3Spawn").GetComponent<Monster3Spawn>();
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
        m3Spawn.enemyCount--;
        m3Spawn.hasReachedMax = false;
    }
}
