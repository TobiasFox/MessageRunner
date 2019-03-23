using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private AsyncOperation asyncOperation;
    [SerializeField] private int sceneToLoad;
    private AudioSource startGameAudioSource;
    private AudioSource backgroundMusic;
    [SerializeField] private float startSoundLength;
    private GameObject INSTANCE;
    private bool isTitleScene = true;
    private bool isGameOverScene = false;
    private int currentLoadedSceneIndex;

    private void Awake()
    {
        if (INSTANCE == null)
        {
            INSTANCE = gameObject;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        startGameAudioSource = GetComponent<AudioSource>();
        backgroundMusic = GameObject.FindGameObjectWithTag("BackgroundMusic")?.GetComponent<AudioSource>();

        StartAsyncSceneLoading(sceneToLoad);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 2)
        {
            FindObjectOfType<GameManager>()?.StartGame();
        }
        else if (scene.buildIndex == 3)
        {
            StartCoroutine("StartCountdown", 1f);
        }
    }

    public void StartAsyncSceneLoading(int sceneIndex)
    {
        currentLoadedSceneIndex = sceneIndex;
        StartCoroutine(LoadScene(sceneIndex));
    }

    private IEnumerator LoadScene(int sceneIndex)
    {
        asyncOperation = SceneManager.LoadSceneAsync(sceneIndex);
        asyncOperation.allowSceneActivation = false;

        yield return null;
    }

    public void ShowLoadedScene()
    {
        if (currentLoadedSceneIndex == 2)
        {
            StartCoroutine(StartGame());
            return;
        }
        asyncOperation.allowSceneActivation = true;
    }

    private IEnumerator StartGame()
    {
        backgroundMusic.volume = 0.5f;
        startGameAudioSource.Play();
        yield return new WaitForSeconds(startSoundLength);
        backgroundMusic.volume = 1f;
        asyncOperation.allowSceneActivation = true;
    }

    private void Update()
    {
        if (!isTitleScene && !isGameOverScene)
        {
            return;
        }

        if (isTitleScene && Input.anyKey)
        {
            ShowLoadedScene();
            isTitleScene = false;
        }
        else if (isGameOverScene && Input.anyKeyDown)
        {
            isGameOverScene = false;
            SceneManager.LoadScene(1);
            //asyncOperation.allowSceneActivation = true;
        }
    }

    private void OnApplicationQuit()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private IEnumerator StartCountdown(float wait)
    {
        yield return new WaitForSeconds(wait);
        isGameOverScene = true;
        //StartAsyncSceneLoading("ChoosePlayerScene");
    }
}
