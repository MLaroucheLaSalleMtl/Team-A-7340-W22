using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Male1DialogueSorter : SelectDialogue
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
                npc.select++;
                break;
            case 1:
                break;
        }
    }

}
