using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    [SerializeField] private Transform player;
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] LayerMask whatIsPlayer;

    //Enemy's stats
    [SerializeField] private float health;
    [SerializeField] private float damage;
    [SerializeField] private float expValue;
    [SerializeField] private float goldValue;

    public bool isDead;
    public bool playerIsDead;

    //Patrolling
    private Vector3 walkPoint;
    private bool walkPointSet;
    public float walkPointRange; //how far does the monster walk
    private bool isIdling;
    public bool isBeingAttacked;

    //Attacking
    [SerializeField] private GameObject hitbox;
    public bool attacking;
    //public Player playermanager;

    //States
    public float sightRange, attackRange, distanceFromPlayer; 
    //how far does the player need to be for the monster to see and/or attack him
    public bool playerInSightRange, playerInAttackRange;

    //Animation
    private Animator anim;


    
    public float Damage { get => damage; set => damage = value; }
    public float Health { get => health; set => health = value; }

    void Awake()
    {
        player = GameObject.Find("PlayerCharacter").transform;
        
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        hitbox.SetActive(false);
        
    }

    private void Start()
    {

        //playermanager = Player.instance;
    }

    public virtual void Update()
    {
        if(!isDead)
        {
            //check attack range, sight range, distance from player
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
            distanceFromPlayer = Vector3.Distance(transform.position, player.position);

            if (!isIdling)
                if (!playerInSightRange || playerIsDead) Patrolling();

            if (playerInSightRange && !playerInAttackRange && !playerIsDead
                || isBeingAttacked && !playerInAttackRange && !playerIsDead)
            {
                ChasePlayer();
            }
            if(!attacking && !playerIsDead)
                if (playerInSightRange && playerInAttackRange) AttackPlayer();
        }
    }

    private void Patrolling()
    {
        agent.speed = 2f;
        anim.SetBool("Chasing", false);
        anim.SetBool("Attacking", false);

        if (!walkPointSet) SearchWalkPoint(); //if there's no walk point, create one

        if (walkPointSet) agent.SetDestination(walkPoint);

        Vector3 distancetoWalkPoint = transform.position - walkPoint;
        if (distancetoWalkPoint.magnitude < 1f)
        {
            isIdling = true;
            anim.SetBool("Moving", false);
            StartCoroutine(Idling(Random.Range(0, 4)));
            walkPointSet = false;
        }
    }

    private IEnumerator Idling(float time)
    {
        yield return new WaitForSeconds(time);
        isIdling = false;
    }

    private void SearchWalkPoint()
    {
        //calculate a random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
            anim.SetBool("Moving", true);
        }
    }

    public void ChasePlayer()
    {
        if (Vector3.Distance(agent.transform.position, player.transform.position) > 15f)
            agent.speed = 15f;
        else
            agent.speed = 3.5f;
        anim.SetBool("Moving", true);
        anim.SetBool("Chasing", true);
        agent.SetDestination(player.position);
    }

    

    private void AttackPlayer()
    {
        if (!anim.GetBool("Attacking"))
            anim.SetFloat("AttackType", Random.Range(0, 2));


        agent.SetDestination(transform.position);
        transform.LookAt(player);
        anim.SetTrigger("Attacking");

        if(!attacking)
            StartCoroutine(ResetAttack(2f));
    }

    

    private IEnumerator SetDestination()
    {
        yield return new WaitForSeconds(2f);
        agent.SetDestination(player.position);
    }
    private IEnumerator ResetAttack(float time)
    {
        attacking = true;
        hitbox.SetActive(true);
        anim.SetBool("Moving", false);
        yield return new WaitForSeconds(time);
        hitbox.SetActive(false);
        attacking = false;
    }

    public virtual void TakeDamage(float damage)
    {
        this.health -= damage;
        this.isBeingAttacked = true;
        //add sound effect

        if (!isDead)
        {
            float exp = damage * 5 / 100;
            //playermanager.AddExp(exp);
            GameObject.Find("Player").GetComponentInChildren<Player>().AddExp(exp);
            if (health <= 0) MonsterDeath();
        }
    }

    public virtual void MonsterDeath()
    {
        isDead = true;
        //agent.SetDestination(transform.position);

        //playermanager.AddExp(expValue);
        GameObject.Find("Player").GetComponentInChildren<Player>().AddExp(expValue);
        GameObject.Find("Player").GetComponentInChildren<Player>().AddGold(goldValue);
        //playermanager.AddGold(goldValue);

        anim.SetFloat("DeadAnim", Random.Range(0, 3));
        anim.SetTrigger("Dead");
        StartCoroutine(Dying(5));
    }

    public virtual void Stop() { }

    private IEnumerator Dying(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
