using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MainMenuUIHandler : MonoBehaviour
{
    [SerializeField] FadeTransition fade;
    [SerializeField] UnityEvent onStart;

    public void Start()
    {
        fade.FadeOut();
    }

    public void StartNew()
    {
        SceneManager.LoadScene("GlobalScene");
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void LaunchGame()
    {
        fade.FadeIn(onStart);
    }

    public void OnArcadeModeClick()
    {
        GameModeManager.Instance.StartArcadeMode();
        SceneManager.LoadScene("BarView");
    }
}
