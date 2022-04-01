using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantDialogueSorter : SelectDialogue
{
    private TalkToNPC npc;

    private void Start()
    {
        npc = gameObject.GetComponent<TalkToNPC>();
        base.dialogueManager = DialogueManager.GetInstance();
    }

    public override void GetNextDialogue()
    {
        switch (npc.select)
        {
            case 0:
                dialogueManager.questIndex = -1;
                npc.select++;
                break;
            case 1:
                dialogueManager.questIndex = -1;
                break;
        }
    }
}
