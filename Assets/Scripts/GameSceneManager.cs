using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using vinkn;

public class GameSceneManager : MonoBehaviour
{
    [SerializeField] bool startStory;
    [SerializeField] string firstScene;
    [SerializeField] List<string> restrictedScenes;
    [SerializeField]  StoryReader storyReader;
    [SerializeField] UnityEvent onSceneChange;
    Scene currentScene;
    FadeTransition fade;
    string nextScene;

    // Start is called before the first frame update
    void Start()
    {
        fade = FindAnyObjectByType<FadeTransition>();
        SceneManager.LoadScene(firstScene, LoadSceneMode.Additive);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (mode == LoadSceneMode.Additive) {
            currentScene = scene;
            onSceneChange?.Invoke();
            StartCoroutine(_StartStoryAsync());
        } else if (startStory) {
        }
    }

    public void ChangeScene(string sceneName) 
    {
        UnityEvent callback = new UnityEvent();
        callback.AddListener(() => TryChangeScene(sceneName));
        fade.FadeIn(callback);
    }

    private void TryChangeScene(string sceneName)
    {
        if (currentScene != null && !restrictedScenes.Contains(sceneName))
        {
            StartCoroutine(_ChangeSceneAsync(sceneName));
        }
        else
        {
            GoToScene(sceneName);
        }
    }

    IEnumerator _ChangeSceneAsync(string sceneName)
    {
        AsyncOperation rem = SceneManager.UnloadSceneAsync(currentScene); 
        yield return rem;
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        //TODO: régler les events du fade in
    }

    IEnumerator _StartStoryAsync()
    {
        yield return null;
        storyReader.Next();
        fade.FadeOut();
    }

    public void GoToScene(string sceneName) 
    {
        SceneManager.LoadScene(sceneName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
