using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public ItemObject[] item;
    public string goldDrop;
    public float gold = 0;
    public int randomItem;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            randomItem = Random.Range(0, 13);

            switch (randomItem)
            {
                case 0: case 1: case 2: case 3: case 4:
                    GameObject.Find("Player").GetComponentInChildren<Player>().AddGold(gold);
                    GameObject.Find("Player").GetComponentInChildren<GainItem>().NewItem(goldDrop);
                    break;

                case 5: case 6: case 7: case 8:
                    GameObject.Find("Player").GetComponentInChildren<Player>().AddGold(gold);
                    GameObject.Find("Player").GetComponentInChildren<GainItem>().NewItem(goldDrop);
                    GameObject.Find("Player").GetComponentInChildren<GainItem>().NewItem(item[0].ToString());
                    break;

                case 9: case 10:
                    GameObject.Find("Player").GetComponentInChildren<Player>().AddGold(gold);
                    GameObject.Find("Player").GetComponentInChildren<GainItem>().NewItem(goldDrop);
                    GameObject.Find("Player").GetComponentInChildren<GainItem>().NewItem(item[1].ToString());
                    break;

                case 11: case 12:
                    GameObject.Find("Player").GetComponentInChildren<Player>().AddGold(gold);
                    GameObject.Find("Player").GetComponentInChildren<GainItem>().NewItem(goldDrop);
                    GameObject.Find("Player").GetComponentInChildren<GainItem>().NewItem(item[2].ToString());
                    break;

                case 13:
                    GameObject.Find("Player").GetComponentInChildren<Player>().AddGold(gold);
                    GameObject.Find("Player").GetComponentInChildren<GainItem>().NewItem(goldDrop);
                    GameObject.Find("Player").GetComponentInChildren<GainItem>().NewItem(item[3].ToString());
                    break;

                case 14:
                    GameObject.Find("Player").GetComponentInChildren<Player>().AddGold(gold);
                    GameObject.Find("Player").GetComponentInChildren<GainItem>().NewItem(goldDrop);
                    GameObject.Find("Player").GetComponentInChildren<GainItem>().NewItem(item[4].ToString());
                    break;

                default:
                    GameObject.Find("Player").GetComponentInChildren<Player>().AddGold(gold);
                    break;
            }

            Destroy(gameObject);
        }
    }
}
