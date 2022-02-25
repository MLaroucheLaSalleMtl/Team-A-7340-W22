using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballProjectile : MonoBehaviour
{
    private Rigidbody rigid;
    private Player player;
    [SerializeField] private float speed;
    [SerializeField] private Transform fireballHit;
    [SerializeField] private float damage;
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
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponentInChildren<Player>().TakeDamage(damage);
            GameObject.Find("Player").GetComponentInChildren<Player>().isBurned = true;
        }
        
        Instantiate(fireballHit, transform.position, Quaternion.identity);
        GameObject.Find("GameManager").GetComponent<GameManager>().PlaySoundEffect(
                        GameObject.Find("FireballSpawn").GetComponent<AudioSource>(),
                        GameObject.Find("GameManager").GetComponent<GameManager>().FireballHit, 0.5f, 1f);
        if (!other.CompareTag("Hitbox"))
           Destroy(gameObject);
    }

    private void Update()
    {
        //If it doesn't collide with anything, destroy after 10 seconds
        if (!wait)
        {
            StartCoroutine(DestroyAfterNoHit());
            wait = true;
        }
    }

    private IEnumerator DestroyAfterNoHit()
    {
        yield return new WaitForSeconds(5);
        Instantiate(fireballHit, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
