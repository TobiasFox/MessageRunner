using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private AsyncOperation asyncOperation;
    [SerializeField] private string sceneToLoad = "";
    private AudioSource startGameAudioSource;
    private AudioSource backgroundMusic;
    [SerializeField] private float startSoundLength;
    private GameObject INSTANCE;
    private bool isTitleScene = true;

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

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.Equals("CarinaScene"))
        {
            FindObjectOfType<GameManager>()?.StartGame();
        }
    }

    private void Start()
    {
        startGameAudioSource = GetComponent<AudioSource>();
        backgroundMusic = GameObject.FindGameObjectWithTag("BackgroundMusic")?.GetComponent<AudioSource>();

        StartAsyncSceneLoading(sceneToLoad);
    }

    public void StartAsyncSceneLoading(string sceneName)
    {
        StartCoroutine(LoadScene(sceneName));
    }

    private IEnumerator LoadScene(string sceneName)
    {
        asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation = false;

        yield return null;
    }

    public void ShowLoadedScene()
    {
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
        if (!isTitleScene)
        {
            return;
        }

        if (Input.anyKey)
        {
            StartCoroutine(StartGame());
            isTitleScene = false;
        }
    }

    private void OnApplicationQuit()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
