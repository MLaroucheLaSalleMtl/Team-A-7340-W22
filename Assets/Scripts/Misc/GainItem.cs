using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GainItem : MonoBehaviour
{
    public TextMeshProUGUI text;
    private GameObject textPrefab;

    public void NewItem(string itemGained)
    {
        textPrefab = Instantiate(text.gameObject, transform);
        textPrefab.GetComponent<TextMeshProUGUI>().text = "You gained " + itemGained;
        StartCoroutine(NewItemText());
    }

    private IEnumerator NewItemText()
    {
        yield return new WaitForSeconds(3f);
        Destroy(this.textPrefab.gameObject);
    }
}
