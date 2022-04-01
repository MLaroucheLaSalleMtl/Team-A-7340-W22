using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestLog : MonoBehaviour
{
 
    [SerializeField] private GameObject questPrefab;
    [SerializeField] private Transform questList;
    [SerializeField] private TextMeshProUGUI questDescription;

    private List<QuestScript> questScripts = new List<QuestScript>();

    private Quest selected;
    private static QuestLog instance;

    public static QuestLog Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<QuestLog>();
            }
            return instance;
        }
        
    }

    public void AcceptQuest(Quest quest)
    {

        GameObject gameObject = Instantiate(questPrefab, questList); //instanciate the quest
        QuestScript questScript = gameObject.GetComponent<QuestScript>();
        questScript.MyQuest = quest; //make sure the quest script has a reference to the quest
        quest.MyQuestScript = questScript; //make sure the quest has a reference to the quest script
        questScripts.Add(questScript);
        gameObject.GetComponent<TextMeshProUGUI>().text = quest.Title;

        foreach (CollectObjective objective in quest.CollectObjectives)
        {

        }

        foreach (KillObjective objective in quest.KillObjectives)
        {
            GameManager.instance.killConfirmedEvent += new KillConfirmed(objective.UpdateKillCount);
        }
    }
    

    public void ShowDesc(Quest quest)
    {
        if(quest != null)
        {
            if (selected != null && selected != quest) selected.MyQuestScript.DeSelect();

            string objective = string.Empty;

            foreach (Objective obj in quest.KillObjectives)
            {
                objective += obj.Type + ": " + obj.CurrentAmount + "/" + obj.Amount + "\n";
            }

            selected = quest;
            questDescription.text = string.Format("{0}\n{1}\n\n</size>\nObjective\n\n</size>{2}", quest.Title, quest.Description, objective);
        }

        
    }

    public void UpdateSelected()
    {
        ShowDesc(selected);
    }

    public void CheckCompletion()
    {
        foreach(QuestScript questScript in questScripts)
        {
            questScript.IsComplete();
        }
    }
    
}
