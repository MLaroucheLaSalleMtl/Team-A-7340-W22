using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class Female1DialogueSorter : SelectDialogue
{
    private TalkToNPC npc;
    private GameManager manager;

    private void Start()
    {
        manager = GameManager.instance;
        npc = gameObject.GetComponent<TalkToNPC>();
        base.dialogueManager = DialogueManager.GetInstance();
    }

    public override void GetNextDialogue()
    {
        switch (npc.select)
        {
            case 0:
                npc.select++;
                manager.quest1Active = true;
                if (manager.quest1Complete)
                    npc.select = 2;
                break;
            case 1:
                if (manager.quest1Complete)
                    npc.select++;
                break;
            case 2:
                manager.CompleteFirstQuest();
                npc.select++;
                break;
            case 3:
                break;
        }
    }
}
