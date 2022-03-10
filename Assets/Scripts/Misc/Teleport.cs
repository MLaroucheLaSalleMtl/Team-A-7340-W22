using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Teleport : AbstractTeleport
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().SaveData();
            StartCoroutine(Teleporting());
        }
    }

    
}
