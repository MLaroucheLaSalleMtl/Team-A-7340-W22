using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Teleport : MonoBehaviour
{
    private AsyncOperation async;
    [SerializeField] private int sceneIndex;

    private IEnumerator OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerPrefs.SetInt("NextScene", sceneIndex);

            GameObject.Find("GameManager").GetComponent<GameManager>().BlackScreenFade(0, 1, 2, false);
            yield return new WaitForSeconds(1.9f);
            async = SceneManager.LoadSceneAsync(1);//Go to Loading Screen

            async.allowSceneActivation = true;
        }
    }
}
