using System.Collections.Generic;
using System;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }

    // ===== SCRIPTABLE OBJECTS (ASSETS) =====
    [Header("Scriptable Objects")]
    [SerializeField] private List<CharacterData> soCharacters;
    [SerializeField] private List<RecipeData> soRecipes;

    // ===== RUNTIME COPIES =====
    private List<CharacterData> charactersRuntime;
    private List<RecipeData> recipesRuntime;

    // ===== DAILY TRACKING =====
    public List<CharacterData> charactersOfTheDay = new List<CharacterData>();
  
    // ===== EVENTS =====
    public event Action OnCharacterDiscovered;
    public event Action OnRecipeDiscovered;

    // ===== SINGLETON SETUP =====
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeData();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // create runtime copies of the ScriptableObjects to track changes during the game without modifying the original assets
    private void InitializeData()
    {
        charactersRuntime = new List<CharacterData>();
        recipesRuntime = new List<RecipeData>();

        foreach (var c in soCharacters)
            charactersRuntime.Add(Instantiate(c));

        foreach (var r in soRecipes)
            recipesRuntime.Add(Instantiate(r));
    }

    // ===== PUBLIC API =====
    public List<CharacterData> GetCharacters() => charactersRuntime;
    public List<RecipeData> GetRecipes() => recipesRuntime;

    public void NotifyCharacterDiscovered()
    {
        OnCharacterDiscovered?.Invoke();
    }

    public void NotifyRecipeDiscovered()
    {
        OnRecipeDiscovered?.Invoke();
    }
}
