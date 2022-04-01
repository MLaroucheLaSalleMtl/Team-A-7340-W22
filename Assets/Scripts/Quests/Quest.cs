using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    [SerializeField] private string title;
    [TextArea(5, 15)]
    [SerializeField] private string description;

    [SerializeField] private CollectObjective[] collectObjectives;
    [SerializeField] private KillObjective[] killObjectives;


    public string Title { get => title; set => title = value; }
    public QuestScript MyQuestScript { get; set; }
    public string Description { get => description; set => description = value; }
    public CollectObjective[] CollectObjectives { get => collectObjectives; }

    public KillObjective[] KillObjectives { get => killObjectives; }


    public bool IsComplete
    {
        get { 
            foreach (Objective objective in killObjectives)
            {
                if (!objective.IsComplete)
                {
                    return false;
                }
            }
            return true;
        }
    }
}

[System.Serializable]
public abstract class Objective
{
    
    [SerializeField] private int amount;
    private int currentAmount;
    [SerializeField] private string type;

    private GameObject Item;

    public int Amount { get => amount; }
    public int CurrentAmount { get => currentAmount; set => currentAmount = value; }
    public string Type { get => type; }


    public bool IsComplete
    {
        get { return currentAmount >= amount; }
    }
}

[System.Serializable]
public class CollectObjective : Objective
{
    /*public void UpdateCount(Item item)
    {
        if(Type.ToLower() == item.Title.ToLower())
        {
            CurrentAmount = Inventory.Instance.GetItemCount(item.Title);
            QuestLog.Instance.UpdateSelected();
            QuestLog.Instance.CheckCompletion();
        }
    }*/
}

[System.Serializable]
public class KillObjective : Objective
{
    public void UpdateKillCount(EnemyAI enemy)
    {

        if(Type == enemy.Type && CurrentAmount < Amount)
        {
            CurrentAmount++;
            QuestLog.Instance.UpdateSelected();
            QuestLog.Instance.CheckCompletion();
        }
    }
}
