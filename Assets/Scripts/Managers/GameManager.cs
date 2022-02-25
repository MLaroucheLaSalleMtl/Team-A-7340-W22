using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    //Death system
    private AsyncOperation async;
    private float respawnTime = -1;
    private bool timerDone = false;
    [SerializeField] private Image blackScreen;
    [SerializeField] private GameObject message;
    [SerializeField] private TextMeshProUGUI respawn_Text;

    //Sound effects
    [SerializeField] private AudioSource gameAudio;
    [SerializeField] private AudioClip gameOver;
    [SerializeField] private AudioClip fireballhit;
    [SerializeField] private AudioClip fireball;

    //Pause menu
    private bool gamePaused;
    [SerializeField] GameObject pause_Menu;
    public bool canPause = true;


    public AudioSource GameAudio { get => gameAudio; set => gameAudio = value; }
    public AudioClip FireballHit { get => fireballhit; set => fireballhit = value; }
    public AudioClip Fireball { get => fireball; set => fireball = value; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gameAudio = GetComponent<AudioSource>();
        pause_Menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //For GameOver
        if(timerDone)
        {
            StartCoroutine(Respawn());
            timerDone = false;
        }

        //Pause Menu
        if (Input.GetKeyDown(KeyCode.Escape) && canPause)
        {
            if (gamePaused)
                Resume();
            else
                PauseGame();
        }
    }

    public void PlaySoundEffect(AudioSource source, AudioClip clip, float volume, float pitch)
    {
        source.volume = volume;
        source.pitch = pitch;
        source.PlayOneShot(clip);
    }

    public void GameOver()
    {
        canPause = false;
        this.BlackScreenFade(0f, 1f, 2, true);
        PlaySoundEffect(gameAudio, gameOver, 0.5f, 1f);
        //"You died" message appears
        message.SetActive(true);
        LeanTween.cancel(message);
        message.transform.localScale = Vector3.one;
        LeanTween.scale(message, Vector3.one * 2, 3).setEaseOutExpo();

        //Respawn Text appears
        respawnTime = 5f;
        respawn_Text.enabled = true;
        respawn_Text.canvasRenderer.SetAlpha(0f);
        respawn_Text.CrossFadeAlpha(1, 1, false);
        StartCoroutine(RespawnText());
    }

    private void PauseGame()
    {
        this.BlackScreenFade(0f, 0.7f, 1, false);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        gamePaused = true;

        pause_Menu.SetActive(true);
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        gamePaused = false;
        this.BlackScreenFade(0.7f, 0f, 1, false);
        pause_Menu.SetActive(false);
    }

    public void ReturnToMainMenu()
    {
        PlayerPrefs.SetInt("NextScene", 0);
        this.BlackScreenFade(0.7f, 1f, 2, true);
        Time.timeScale = 1f;
        gamePaused = false;
        async = SceneManager.LoadSceneAsync(1);
        async.allowSceneActivation = true;
    }

    public void BlackScreenFade(float setAlpha, float alpha, int duration, bool value)
    {
        blackScreen.enabled = true;
        blackScreen.canvasRenderer.SetAlpha(setAlpha);
        blackScreen.CrossFadeAlpha(alpha, duration, true);
        StartCoroutine(Faded(duration, value));
    }

    private IEnumerator Faded(float time, bool value)
    {
        yield return new WaitForSeconds(time);
        blackScreen.enabled = value;
    }

    private IEnumerator RespawnText()
    {
        for(float i = respawnTime; i >= 0; i--)
        {
            yield return new WaitForSeconds(1f);
            respawn_Text.text = "Respawning in " + respawnTime + " seconds...";
            respawnTime--;
        }

        timerDone = true;

    }

    private IEnumerator Respawn()
    {
        message.SetActive(false);
        respawn_Text.enabled = false;
        PlayerPrefs.SetInt("NextScene", 3); //Index of the Church scene

        yield return new WaitForSeconds(2f);
        async = SceneManager.LoadSceneAsync(1);//Go to Loading Screen

        async.allowSceneActivation = true;
    }
}
