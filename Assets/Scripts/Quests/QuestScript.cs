using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestScript : MonoBehaviour
{
    public Quest MyQuest { get; set; }

    private bool isComplete = false;

    public void SelectDesc()
    {
        GetComponent<TextMeshProUGUI>().color = new Color(0.82f, 0.73f, 0.73f);
        QuestLog.Instance.ShowDesc(MyQuest);
    }

    public void DeSelect()
    {
        GetComponent<TextMeshProUGUI>().color = Color.white;
    }

    public void IsComplete()
    {
        if(MyQuest.IsComplete && !isComplete)
        {
            isComplete = true;
            GetComponent<TextMeshProUGUI>().text += " [!]";
        }
        else if(!MyQuest.IsComplete)
        {
            isComplete = false;
            GetComponent<TextMeshPro>().text = MyQuest.Title;
        }
    }

    
}
