using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Menu_Manager : AbstractTeleport
{
    //For title
    [SerializeField] private GameObject title;
    [SerializeField] private float tweenTime;

    //For menu
    [SerializeField] private GameObject[] buttons;

    
    // Start is called before the first frame update
    void Awake()
    {
        
        //title.SetActive(false);
        SetMenu(false);

        //Show title
        //StartCoroutine(ShowTitle());
        Tween();
        //Show menu
        StartCoroutine(ShowMenu());

    }

    private void Start()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().canPause = false;
        //Fade out
        GameObject.Find("GameManager").GetComponent<GameManager>().BlackScreenFade(1f, 0f, 3, false);
    }

    private void SetMenu(bool value)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(value);
        }
    }   

    
    private IEnumerator ShowTitle()
    {
        yield return new WaitForSeconds(4);
        title.SetActive(true);
        Tween();
    }
    
    
    private IEnumerator ShowMenu()
    {
        yield return new WaitForSeconds(2);
        SetMenu(true);
    }
    
    public void Tween()
    {
        LeanTween.cancel(title);
        title.transform.localScale = Vector3.one;
        LeanTween.scale(title, Vector3.one * 2, tweenTime).setEaseOutExpo();
    }

    public void StartGame()
    {
        StartCoroutine(Teleporting());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().DeleteData();
        }
    }
}
