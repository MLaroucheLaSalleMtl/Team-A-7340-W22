using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using TMPro;

public abstract class Interactible : MonoBehaviour
{
    private bool PlayerInRange = false;
    [SerializeField] private TextMeshProUGUI InteractableText;

    //public StarterAssetsInputs Inputs;

    private void Start()
    {
       //Inputs = StarterAssetsInputs.instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            InteractableText.enabled = true;
            
            PlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            InteractableText.enabled = false;
            PlayerInRange = false;
        }
    }

    private void HideUI()
    {

    }

    public virtual void Activate()
    {
        InteractableText.enabled = false;
        
    }

    public virtual void Deactivate()
    {

    }

    void FixedUpdate()
    {
        if(GameObject.Find("Player").GetComponentInChildren<StarterAssetsInputs>().interact && PlayerInRange)
        {
            Activate();
            GameObject.Find("Player").GetComponentInChildren<StarterAssetsInputs>().interact = false;
        }
        else if(GameObject.Find("Player").GetComponentInChildren<StarterAssetsInputs>().interact) //To make sure that the interact bool isn't true whenever the player is not in the range
        {
            GameObject.Find("Player").GetComponentInChildren<StarterAssetsInputs>().interact = false;
        }
        
        if(PlayerInRange == false)
        {
            Deactivate();
        }
        
    }





}
