using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    private GameSceneManager gameSceneManager;

    // Start is called before the first frame update
    void Start()
    {
        gameSceneManager = FindFirstObjectByType<GameSceneManager>();    
    }

    // Update is called once per frame
    public void ChangeSceneFromBar()
    {
        if (gameSceneManager != null)
        {
            gameSceneManager.ChangeScene("Main");
            Debug.Log("if");
        } else {
            SceneManager.LoadScene("Main");
        }

    }


}
