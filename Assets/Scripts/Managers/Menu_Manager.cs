using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class Menu_Manager : AbstractTeleport
{
    public static Menu_Manager instance = null;
    //For title
    [SerializeField] private GameObject title;
    [SerializeField] private float tweenTime;

    //For menu
    [SerializeField] private GameObject[] buttons;

    //For Options
    [SerializeField] private GameObject options;
    public AudioMixer audioMixer;
    public float musicVolume;
    public float sfxVolume;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }

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

        //Options
        musicVolume = PlayerPrefs.GetFloat("MusicParam", 1f);
        sfxVolume = PlayerPrefs.GetFloat("SFXParam", 1f);

        //Set the Audio
        audioMixer.SetFloat("MusicParam", Mathf.Log10(musicVolume) * 30);
        audioMixer.SetFloat("SFXParam", Mathf.Log10(sfxVolume) * 30);
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

    public void ShowOptions()
    {
        options.SetActive(true);
        SetMenu(false);
    }

    public void Return()
    {
        options.SetActive(false);
        PlayerPrefs.Save();
        SetMenu(true);
    }

    private void GetSettings()
    {

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().DeleteData();
        }
    }

}
