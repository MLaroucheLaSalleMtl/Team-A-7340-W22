using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class AbilityHolder : MonoBehaviour
{
    public Ability[] ability;
    public float cooldownTime;
    private float activeTime;
    private int abilityIndex;
    public bool[] canUse;
    public bool performingAbility = false;
    public AudioClip[] abilitySounds;

    private void Start()
    {
        for(int i = 0; i < canUse.Length; i++)
        {
            canUse[i] = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(GameObject.Find("Player").GetComponentInChildren<StarterAssetsInputs>().aim)
        {
            if (GameObject.Find("Player").GetComponentInChildren<StarterAssetsInputs>().bomb)
            {
                abilityIndex = 0;
                if (canUse[abilityIndex])
                {
                    //Activate
                    ability[abilityIndex].TriggerAbility();
                    cooldownTime = ability[abilityIndex].cooldownTime;
                    activeTime = ability[abilityIndex].activeTime;
                    StartCoroutine(WaitActiveTime());
                    StartCoroutine(Cooldown());
                }
                
                GameObject.Find("Player").GetComponentInChildren<StarterAssetsInputs>().bomb = false;
            }
            //else if for next ability (ability[1])
        }
        
    }
    private IEnumerator Cooldown()
    {
        canUse[abilityIndex] = false;
        for (float i = cooldownTime; i >= 0; i--)
        {
            yield return new WaitForSeconds(1f);
            cooldownTime--;
        }
        canUse[abilityIndex] = true;
    }

    private IEnumerator WaitActiveTime()
    {
        performingAbility = true;
        yield return new WaitForSeconds(activeTime);
        performingAbility = false;
    }
}
