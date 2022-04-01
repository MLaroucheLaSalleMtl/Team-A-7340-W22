using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StarterAssets;

public class Player : MonoBehaviour
{
    public static Player instance = null;
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentAmmo;
    [SerializeField] private float maxCurrentAmmo;
    [SerializeField] private float maxAmmo;
    [SerializeField] private float trueMaxAmmo;
    [SerializeField] private float damage;
    [SerializeField] private float level;
    [SerializeField] private float addedHealthFromLevelUp;
    [SerializeField] private float gold;
    [SerializeField] private Image bloodScreen;
    [SerializeField] private GameObject burning;
    [SerializeField] private float abilityDamage;
    [SerializeField] private InventoryObject inventory;

    private float expGained; //Change this to enemy's EXP or Quest EXP
    private float expLost;
    private float goldGained;//Change this to enemy's Gold or Quest Gold
    private float goldLost = 5;
    private float currentExp = 0;
    private int requiredExp;
    private bool leveledUp;
    public bool isDead;
    public bool isBurned;
    public bool shieldOn;

    private Animator animator;
    [SerializeField] private AudioSource playerAudio;
    [SerializeField] private AudioClip gettingHurt;

    #region Encapsulation
    public float CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public float CurrentAmmo { get => currentAmmo; set => currentAmmo = value; }
    public float MaxAmmo { get => maxAmmo; set => maxAmmo = value; }
    public float MaxCurrentAmmo { get => maxCurrentAmmo; set => maxCurrentAmmo = value; }
    public float Damage { get => damage; set => damage = value; }
    public float Level { get => level; set => level = value; }
    public float CurrentExp { get => currentExp; set => currentExp = value; }
    public int RequiredExp { get => requiredExp; set => requiredExp = value; }
    public float ExpGained { get => expGained; set => expGained = value; }
    public float Gold { get => gold; set => gold = value; }
    public float GoldGained { get => goldGained; set => goldGained = value; }
    public float GoldLost { get => goldLost; set => goldLost = value; }
    public float ExpLost { get => expLost; set => expLost = value; }
    public float AbilityDamage { get => abilityDamage; set => abilityDamage = value; }
    public float TrueMaxAmmo { get => trueMaxAmmo; set => trueMaxAmmo = value; }
    public GameObject Burning { get => burning; set => burning = value; }

    #endregion

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(this);
        }
        requiredExp = (int)Mathf.Floor(25 * Mathf.Pow(level, 2));
        trueMaxAmmo = maxAmmo;
        isDead = false;
        animator = GetComponent<Animator>();
        playerAudio = GameObject.Find("Player").GetComponent<AudioSource>();
    }

    
    private void Update()
    {
        if (currentExp >= requiredExp)
        {
            this.currentExp -= this.requiredExp;
            level += 1;
            leveledUp = true;
            Heal(addedHealthFromLevelUp);
            requiredExp = (int) Mathf.Floor(25 * Mathf.Pow(this.Level, 2)); //Required EXP is updated

            GameObject.Find("GameManager").GetComponent<GameManager>().PlaySoundEffect(
                                             GameObject.Find("CanvasBasicUI").GetComponent<PlayerUI_Manager>().UiAudio,
                                             GameObject.Find("CanvasBasicUI").GetComponent<PlayerUI_Manager>().LevelUp, 0.5f, 1.2f);
        }
    }

    
    public void AddExp(float amount)
    {
        currentExp += amount;
        expGained = amount;
        GameObject.Find("CanvasBasicUI").GetComponent<PlayerUI_Manager>().gotExp = true;
    }
    

    public void AddGold(float amount)
    {
        gold += amount;
        goldGained = amount;
        GameObject.Find("CanvasBasicUI").GetComponent<PlayerUI_Manager>().gotGold = true;
    }

    public void RemoveGold(float amount)
    {
        if(gold > 0)
        {
            gold -= amount;
            goldLost = amount;
            GameObject.Find("CanvasBasicUI").GetComponent<PlayerUI_Manager>().lostGold = true;
        }
    }

    public void RemoveExp(float amount)
    {
        if(currentExp > 0)
        {
            currentExp -= amount;

        }
    }

    public void TakeDamage(float damage)
    {
        if (isDead || shieldOn)//Make sure you dont take damage while being dead
            return;
        this.currentHealth -= damage;

        GameObject.Find("GameManager").GetComponent<GameManager>().PlaySoundEffect(playerAudio, gettingHurt, 0.3f, 1f);
        BloodScreenFade(1, 0, 1, false);
        
        if(this.CheckIfDead())
        {
            isDead = true;
            animator.SetTrigger("Death");
            burning.SetActive(false);
            GameObject.Find("Player").GetComponentInChildren<ThirdPersonController>().death = true;
            GameObject.Find("Player").GetComponentInChildren<Canvas>().enabled = false;
            GameObject.Find("Player").GetComponentInChildren<ThirdPersonShooterController>().AimVirtualCamera.enabled = false;
            GameObject.Find("Player").GetComponentInChildren<EquipGun>().Unequip();
            GameObject.Find("Player").GetComponentInChildren<ThirdPersonShooterController>().enabled = false;
            GameObject.Find("GameManager").GetComponent<GameManager>().GameOver();
        }
    }

    public void BloodScreenFade(float setAlpha, float alpha, int duration, bool value)
    {
        bloodScreen.enabled = true;
        bloodScreen.canvasRenderer.SetAlpha(setAlpha);
        bloodScreen.CrossFadeAlpha(alpha, duration, true);
        StartCoroutine(Faded(duration, value));
    }

    private IEnumerator Faded(float time, bool value)
    {
        yield return new WaitForSeconds(time);
        bloodScreen.enabled = value;
    }

    public void Heal(float value)
    {
        if(leveledUp)
        {
            this.currentHealth += value;
            this.maxHealth += value;
        }
        else
        {
            if(value < (this.maxHealth - this.currentHealth))
            {
                this.currentHealth += value;
            }
            else
            {
                value = this.maxHealth;
                this.currentHealth = value;
            }
        }
        
    }

    public bool CheckIfDead()
    {
        return this.currentHealth <= 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<ItemDrop>();
        if(item)
        {
            switch (item.randomItem)
            {
                case 0: case 1: case 2: case 3: case 4:
                    break;
                case 5: case 6: case 7: case 8:
                    inventory.AddItem(new Item(item.item[0]), 1);
                    break;
                case 9: case 10:
                    inventory.AddItem(new Item(item.item[1]), 1);
                    break;
                case 11: case 12:
                    inventory.AddItem(new Item(item.item[2]), 1);
                    break;
                case 13:
                    inventory.AddItem(new Item(item.item[3]), 1);
                    break;
                case 14:
                    inventory.AddItem(new Item(item.item[4]), 1);
                    break;
                default:
                    break;
            }
        }
    }

    private void OnApplicationQuit()
    {
        //inventory.Container.Clear();
    }
}
