using System.Collections.Generic;
using UnityEngine;

public class DayDataManager : MonoBehaviour
{
    public static DayDataManager Instance { get; private set; }

    public List<CharacterData> charactersOfTheDay = new List<CharacterData>();


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
