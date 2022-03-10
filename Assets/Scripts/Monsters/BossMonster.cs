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
    private Animator anim;
    private GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.instance;
        maxHealth = base.Health;
        anim = GetComponent<Animator>();
        bossAudio = GetComponent<AudioSource>();
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

        bossHealthBar.fillAmount = base.Health / maxHealth;
        
    }

    private IEnumerator UseAbility()
    {
        //Fireball
        yield return new WaitForSeconds(3f);
        anim.SetTrigger("Shooting");
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
                        GameObject.Find("GameManager").GetComponent<GameManager>().Fireball, 0.5f, 0.9f);

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

    public override void Stop()
    {
        base.agent.SetDestination(transform.position);
        transform.LookAt(transform.position);
    }
}
