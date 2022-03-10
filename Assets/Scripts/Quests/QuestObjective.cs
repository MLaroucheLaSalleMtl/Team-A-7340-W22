using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestObjective
{
    public ObjectiveType objectiveType;
    public MonsterType monsterType;

    [SerializeField] public int requiredAmount;
    [SerializeField] public int currentAmount;

    public bool IsCompleted()
    {
        return currentAmount >= requiredAmount;
    }

    public void MonsterKilled()
    {
        if(objectiveType == ObjectiveType.Kill)
        {
            if(monsterType == MonsterType.Monster1)
            {
                currentAmount++;
            }
            else if(monsterType == MonsterType.Monster2)
            {
                currentAmount++;
            }
            else if(monsterType == MonsterType.Monster3)
            {
                currentAmount++;
            }
            else if(monsterType == MonsterType.BossMonster)
            {
                currentAmount++;
            }
           
        }
            
    }

    public void ItemPickUp()
    {
        if(objectiveType == ObjectiveType.PickUp)
        {
            currentAmount++;
        }
    }

}

public enum ObjectiveType
{ 
    Kill,
    PickUp
}

public enum MonsterType
{
    Monster1,
    Monster2,
    Monster3,
    BossMonster
}

