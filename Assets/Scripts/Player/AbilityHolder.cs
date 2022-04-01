using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.SceneManagement;

public class AbilityHolder : MonoBehaviour
{
    public Ability[] ability;
    public GameObject dashEffect;
    public GameObject laser;
    public GameObject shield;
    public bool dash;

    public AudioClip[] abilitySounds;

    private void Start()
    {
        for(int i = 0; i < ability.Length; i++)
        {
            ability[i].CanUse = true;
            ability[i].cooldownTimeUI = ability[i].cooldownTime;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex != 2)
        {
            //Ability 1
            if (GameObject.Find("Player").GetComponentInChildren<StarterAssetsInputs>().ability1)
            {
                //Makes sure that you can only use this while aiming and also not while using the laser beam
                if(GameObject.Find("Player").GetComponentInChildren<StarterAssetsInputs>().aim && !ability[2].PerformingAbility)
                   ActivateAbility(0);

                GameObject.Find("Player").GetComponentInChildren<StarterAssetsInputs>().ability1 = false;
            }

            //Ability2
            if(GameObject.Find("Player").GetComponentInChildren<StarterAssetsInputs>().ability2)
            {
                ActivateAbility(1);
                
                GameObject.Find("Player").GetComponentInChildren<StarterAssetsInputs>().ability2 = false;
            }

            //Ability3
            if (GameObject.Find("Player").GetComponentInChildren<StarterAssetsInputs>().ability3)
            {
                if (GameObject.Find("Player").GetComponentInChildren<StarterAssetsInputs>().aim)
                    ActivateAbility(2);

                GameObject.Find("Player").GetComponentInChildren<StarterAssetsInputs>().ability3 = false;
            }
            //Ability4
            if (GameObject.Find("Player").GetComponentInChildren<StarterAssetsInputs>().ability4)
            {
                ActivateAbility(3);

                GameObject.Find("Player").GetComponentInChildren<StarterAssetsInputs>().ability4 = false;
            }
        }
        
    }

    private void ActivateAbility(int index)
    { 
        if (ability[index].CanUse)
        {
            //Activate
            ability[index].TriggerAbility();
            StartCoroutine(ability[index].WaitForActiveTime());
            StartCoroutine(ability[index].StartCooldown());

            //Only for dash ability
            if (dash)
                StartCoroutine(DashEffect());
        }
    }

    private IEnumerator DashEffect()
    {
        dashEffect.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        dashEffect.SetActive(false);
        dash = false;
    }

    
}
