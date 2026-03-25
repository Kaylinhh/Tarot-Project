using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using vinkn;

public class GameSceneManager : MonoBehaviour
{
    // ===== CONFIGURATION =====
    [SerializeField] private string firstScene;
    [SerializeField] private List<string> restrictedScenes;
    [SerializeField] private StoryReader storyReader;
    [SerializeField] private UnityEvent onSceneChange;

    // ===== STATE =====
    private Scene currentScene;
    private string previousScene = "";
    private FadeTransition fade;

    void Start()
    {
        fade = FadeTransition.Instance;
        SceneManager.LoadScene(firstScene, LoadSceneMode.Additive);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Ignore GlobalScene
        if (scene.name == "GlobalScene" || scene.name == gameObject.scene.name)
            return;

        // for additive scenes
        if (mode == LoadSceneMode.Additive)
        {
            currentScene = scene;
            onSceneChange?.Invoke();
            StartCoroutine(_StartStoryAsync());
        }
    }

    public void ChangeScene(string sceneName)
    {
        UnityEvent callback = new UnityEvent();
        callback.AddListener(() => TryChangeScene(sceneName));
        fade?.FadeIn(callback);
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
        // save before unloading
        string sceneToUnload = currentScene.name;

        AsyncOperation rem = SceneManager.UnloadSceneAsync(currentScene);
        yield return rem;

        // Set previous scene
        previousScene = sceneToUnload;

        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    IEnumerator _StartStoryAsync()
    {
        // wait for the scene to be fully loaded and initialized
        yield return null;
        yield return null;

        string current = currentScene.name;

        // first load 
        if (string.IsNullOrEmpty(previousScene))
        {
            storyReader.Next();
        }
        // barview from main or main from barview: resume
        else if ((current == "BarView" && previousScene == "Main") ||
                 (current == "Main" && previousScene == "BarView"))
        {
            storyReader.Resume();
        }
        else if ((current == "Main" && previousScene == "CardReading") ||
                (current == "CardReading" && previousScene == "Main"))
        {
            storyReader.Resume();
        }
        // Fallback
        else
        {
            if (storyReader.isPaused)
                storyReader.Resume();
            else
                storyReader.Next();
        }

        fade?.FadeOut();
    }

    public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ChangeSceneFromBar(string drinkServed)
    {
        if (storyReader?.story != null)
        {
            storyReader.story.variablesState["drinkServed"] = drinkServed;
        }
        else
        {
            Debug.LogError("StoryReader not found or story not initialized!");
        }

        ChangeScene("Main");
    }
}