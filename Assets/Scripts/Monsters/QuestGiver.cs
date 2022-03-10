using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestGiver : MonoBehaviour
{
    [SerializeField] public Quest quest;
    [SerializeField] public Player player;
    [SerializeField] public GameManager manager;
    [SerializeField] public GameObject pause_Menu;
    [SerializeField] public GameObject questWindow;
    [SerializeField] public TextMeshProUGUI questTitle;
    [SerializeField] public TextMeshProUGUI questDesc;
    [SerializeField] public TextMeshProUGUI questReward;

    //TODO
    //add quest to the player script

    //make a list of quests

    //if(quest.isActice)
    //{
    //     quest.objective.EnemyKilled();
    //        if(quest.objective.IsCompleted)
    //        {
    //            player.gold += Quest.goldReward;
    //            quest.Complete();
    //        }
    //
    //
    //}
    public void OpenQuestWindow()
    {
        //manager.BlackScreenFade(0f, 0.7f, 1, false);

        pause_Menu.SetActive(false);
        questWindow.SetActive(true);
        //questTitle = quest.title;
        //questDesc = quest.description;
        //questReward = quest.Reward();
    }

    public void CloseQuestWindow()
    {
        questWindow.SetActive(false);
        pause_Menu.SetActive(true);
    }

    public void AcceptQuest()
    {
        questWindow.SetActive(false);
        quest.isActive = true;
        
    }

    void Update()
    {
        //Pause Menu
        if (Input.GetKeyDown(KeyCode.Escape) && manager.canPause)
        {
            questWindow.SetActive(false);
        }
    }
}
