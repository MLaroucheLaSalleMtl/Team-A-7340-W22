using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    [SerializeField] public string title;
    [SerializeField] public string description;
    [SerializeField] public int expReward;
    [SerializeField] public int GoldReward;

    [SerializeField] public bool isActive;

    [SerializeField] public QuestObjective goal;

    public bool Reward()
    {
        //"EXP = " + expReward + " || GOLD = " + GoldReward + ;
        return true;
    }

    public void Complete()
    {
        isActive = false;
        //the quest was completed

    }
}