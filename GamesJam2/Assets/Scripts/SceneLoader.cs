using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private AsyncOperation asyncOperation;
    [SerializeField] private string sceneToLoad = "";

    private void Start()
    {
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

    private void Update()
    {
        if (Input.anyKey)
        {
            asyncOperation.allowSceneActivation = true;
        }
    }
}
