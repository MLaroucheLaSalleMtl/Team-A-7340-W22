using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public abstract class SelectDialogue : MonoBehaviour
{
    protected DialogueManager dialogueManager;
    public virtual void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (dialogueManager.dialogueEnded)
            {
                GetNextDialogue();
                dialogueManager.dialogueEnded = false;
            }
        }
    }
    public virtual void GetNextDialogue()
    {

    }

}
