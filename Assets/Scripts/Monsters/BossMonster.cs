using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : EnemyAI
{
    [SerializeField] private Transform fireball;
    [SerializeField] private Transform muzzle;
    [SerializeField] private float cooldownTime;
    private bool canUse = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (playerInSightRange && !playerInAttackRange && !playerIsDead
                || isBeingAttacked && !playerInAttackRange && !playerIsDead)
        {
            ChasePlayer();
            StartCoroutine(UseAbility());
        }
        if(base.isDead)
        {
            Stop();
        }
        
    }

    private IEnumerator UseAbility()
    {
        //Fireball
        yield return new WaitForSeconds(3f);
        ShootFireball();
        
    }

    private void ShootFireball()
    {
        if (canUse && !base.isDead)
        {
            Vector3 pos = GameObject.Find("FireballSpawn").transform.position;
            Vector3 end = GameObject.Find("PlayerCharacter").transform.position;
            Vector3 dir = (end - pos).normalized + new Vector3(0, 0.1f, 0);
            Instantiate(fireball, pos, Quaternion.LookRotation(dir, Vector3.up));
            Instantiate(muzzle, pos, Quaternion.LookRotation(dir, Vector3.up));

            //Play sound
            GameObject.Find("GameManager").GetComponent<GameManager>().PlaySoundEffect(
                        GameObject.Find("FireballSpawn").GetComponent<AudioSource>(),
                        GameObject.Find("GameManager").GetComponent<GameManager>().Fireball, 0.5f, 1f);

            StartCoroutine(Cooldown());
        }
    }

    private IEnumerator Cooldown()
    {
        canUse = false;
        yield return new WaitForSeconds(cooldownTime);
        canUse = true;
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }

    public override void MonsterDeath()
    {
        base.MonsterDeath();
    }

    public override void Stop()
    {
        base.agent.SetDestination(transform.position);
        transform.LookAt(transform.position);
    }
}
