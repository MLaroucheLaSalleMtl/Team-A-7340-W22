using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderProjectile : MonoBehaviour
{
    private Rigidbody rigid;
    [SerializeField] private float speed;
    [SerializeField] private Transform explosionHit;
    private Player player;
    private bool wait = false;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        rigid.velocity = transform.forward * speed;
        player = Player.instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Hitbox"))
        {
            if (other.CompareTag("Enemy"))
                other.gameObject.GetComponentInParent<EnemyAI>().TakeDamage(player.AbilityDamage);

            Instantiate(explosionHit, transform.position, Quaternion.identity);
            GameObject.Find("GameManager").GetComponent<GameManager>().PlaySoundEffect(
                            GameObject.Find("GameManager").GetComponent<GameManager>().GameAudio,
                            GameObject.Find("Player").GetComponentInChildren<AbilityHolder>().abilitySounds[1], 0.2f, 1f);


            Destroy(gameObject);
        }
            
    }

    private void Update()
    {
        //If it doesn't collide with anything, destroy after 10 seconds
        if(!wait)
        {
            StartCoroutine(DestroyAfterNoHit());
            wait = true;
        }
    }

    private IEnumerator DestroyAfterNoHit()
    {
        yield return new WaitForSeconds(10);
        Instantiate(explosionHit, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
