using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossMonster : EnemyAI
{
    [SerializeField] private Transform fireball;
    [SerializeField] private Transform muzzle;
    [SerializeField] private float cooldownTime;
    [SerializeField] private Image bossHealthBar;
    [SerializeField] private GameObject bossUI;
    [SerializeField] private GameObject bossDefeat;
    [SerializeField] private AudioClip bossDead;
    private AudioSource bossAudio;

    private float maxHealth;
    private bool canUse = true;
    private bool usingAbility = false;
    private Animator animator;
    private GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.instance;
        maxHealth = base.Health;
        animator = GetComponent<Animator>();
        bossAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (playerInSightRange && !playerInAttackRange && !playerIsDead && !attacking
                || isBeingAttacked && !playerInAttackRange && !playerIsDead && !attacking)
        {
            if(!usingAbility)
            {
                ChasePlayer();
                StartCoroutine(UseAbility());
            }
        }
        if(base.isDead)
        {
            Stop(transform.position);
        }

        bossHealthBar.fillAmount = base.Health / maxHealth;
        
    }

    private IEnumerator UseAbility()
    {
        if (canUse && !base.isDead)
        {
            usingAbility = true;
            yield return new WaitForSeconds(1.2f);
            Stop(GameObject.Find("PlayerCharacter").transform.position);
            animator.SetTrigger("Shooting");
            yield return new WaitForSeconds(0.8f);
            ShootFireball();
            yield return new WaitForSeconds(0.2f);
            usingAbility = false;
            StartCoroutine(Cooldown());
        }
    }

    private void ShootFireball()
    {
        Vector3 pos = GameObject.Find("FireballSpawn").transform.position;
        Vector3 end = GameObject.Find("PlayerCharacter").transform.position;
        Vector3 dir = (end - pos).normalized + new Vector3(0, 0.1f, 0);
        Instantiate(fireball, pos, Quaternion.LookRotation(dir, Vector3.up));
        Instantiate(muzzle, pos, Quaternion.LookRotation(dir, Vector3.up));

        //Play sound
        GameObject.Find("GameManager").GetComponent<GameManager>().PlaySoundEffect(
                    GameObject.Find("FireballSpawn").GetComponent<AudioSource>(),
                    GameObject.Find("GameManager").GetComponent<GameManager>().Fireball, 0.5f, 0.9f);
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
        manager.quest1Complete = true;
        bossUI.SetActive(false);
        GameObject.Find("BossWall").GetComponent<BoxCollider>().enabled = false;
        bossDefeat.SetActive(true);
        LeanTween.cancel(bossDefeat);
        bossDefeat.transform.localScale = Vector3.one;
        LeanTween.scale(bossDefeat, Vector3.one * 2, 3).setEaseOutExpo();
        StartCoroutine(ChangeMusic());
        
        base.MonsterDeath();
    }

    private IEnumerator ChangeMusic()
    {
        GameObject.Find("Music").GetComponent<AudioSource>().volume = 0f;
        GameObject.Find("Music").GetComponent<AudioSource>().Stop();
        GameObject.Find("GameManager").GetComponent<GameManager>().PlaySoundEffect(bossAudio,bossDead, 0.3f, 0.9f);
        yield return new WaitForSeconds(5f);
        bossDefeat.SetActive(false);
        GameObject.Find("Music").GetComponent<AudioSource>().volume = 0.5f;
        GameObject.Find("Music").GetComponent<AudioSource>().PlayOneShot(GameObject.Find("GameManager").GetComponent<GameManager>().mainMusic);
        
    }

    public override void Stop(Vector3 value)
    {
        base.agent.SetDestination(transform.position);
        transform.LookAt(value);
    }
}
