using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster2 : EnemyAI
{
    // Start is called before the first frame update
    void Start()
    {
        
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
}
