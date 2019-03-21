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
        if (Input.anyKey)
        {
            StartCoroutine(StartGame());
        }
    }
}
