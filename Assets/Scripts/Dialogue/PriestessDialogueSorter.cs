using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriestessDialogueSorter : SelectDialogue
{
    private TalkToNPC npc;

    private void Start()
    {
        npc = gameObject.GetComponent<TalkToNPC>();
        base.dialogueManager = DialogueManager.GetInstance();
    }

    /* public override void GetQuest()
    {
        switch (npc.select)
        {
            case 0:
                dialogueManager.questIndex = 0;
                break;
            case 1:
                dialogueManager.questIndex = 0;
                break;
            case 2:
                dialogueManager.questIndex = -1;
                break;
            case 3:
                dialogueManager.questIndex = -1;
                break;
        }
    } */

    public override void GetNextDialogue()
    {
        switch (npc.select)
        {
            case 0:
                if (dialogueManager.questAccepted == true)
                {
                    npc.select = 2;
                    dialogueManager.questAccepted = false;
                }
                else
                    npc.select = 1;
                break;

            case 1:
                if (dialogueManager.questAccepted == true)
                {
                    npc.select = 2;
                    dialogueManager.questAccepted = false;
                }
                break;

            case 2:
                //if quest is finished, go 3
                break;

            case 3:
                break;
        }
    }
}
