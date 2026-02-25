using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using vinkn;

public class GameSceneManager : MonoBehaviour
{
    [SerializeField] bool startStory;
    [SerializeField] string firstScene;
    [SerializeField] List<string> restrictedScenes;
    [SerializeField] StoryReader storyReader;
    [SerializeField] UnityEvent onSceneChange;
    Scene currentScene;
    FadeTransition fade;
    string nextScene;
    private string previousScene = "";
    private Scene? previousSceneObject = null;

    // Start is called before the first frame update
    void Start()
    {
        fade = FindAnyObjectByType<FadeTransition>();
        SceneManager.LoadScene(firstScene, LoadSceneMode.Additive);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"OnSceneLoaded - Scene: {scene.name}, Mode: {mode}");

        // Ignore GlobalScene load
        if (scene.name == "GlobalScene" || scene.name == gameObject.scene.name)
        {
            Debug.Log("GlobalScene loaded (ignored)");
            return;
        }

        // Pour les scènes additives
        if (mode == LoadSceneMode.Additive)
        {
            currentScene = scene;

            Debug.Log($"Updated - Current: {currentScene.name}, Previous: '{previousScene}'");

            onSceneChange?.Invoke();
            StartCoroutine(_StartStoryAsync());
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
        // Save name before unloading
        string sceneToUnload = currentScene.name;
        Debug.Log($"Unloading {sceneToUnload}, loading {sceneName}");

        AsyncOperation rem = SceneManager.UnloadSceneAsync(currentScene);
        yield return rem;

        // set previous scene now
        previousScene = sceneToUnload;
        Debug.Log($"Set previousScene to '{previousScene}'");

        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    IEnumerator _StartStoryAsync()
    {
        yield return null;

        string current = currentScene.name;
        Debug.Log($"_StartStoryAsync - Current: {current}, Previous: '{previousScene}', isPaused: {storyReader.isPaused}");

        // Premier load depuis GlobalScene (previousScene vide)
        if (string.IsNullOrEmpty(previousScene))
        {
            Debug.Log("First content scene to Start story");
            storyReader.Next();
        }
        // BarView depuis Main
        else if (current == "BarView" && previousScene == "Main")
        {
            Debug.Log("BarView from Main to Resume");
            storyReader.Resume();
        }
        // Main depuis BarView
        else if (current == "Main" && previousScene == "BarView")
        {
            Debug.Log("Main from BarView to Resume");
            storyReader.Resume();
        }
        // Autre
        else
        {
            Debug.LogWarning($"Unhandled transition: {previousScene} to {current}");
            // Fallback safe
            if (storyReader.isPaused)
            {
                storyReader.Resume();
            }
            else
            {
                storyReader.Next();
            }
        }

        fade.FadeOut();
    }

    public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ChangeSceneFromBar(string drinkServed)
    {
        if (storyReader != null && storyReader.story != null)
        {
            storyReader.story.variablesState["drinkServed"] = drinkServed;
            Debug.Log($"Sent to Ink: drinkServed = {drinkServed}");
        }
        else
        {
            Debug.LogError("StoryReader not found or story not initialized!");
        }

        // Change de scène normalement
        ChangeScene("Main");
    }
}
