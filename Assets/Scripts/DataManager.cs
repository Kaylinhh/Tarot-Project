using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }

    [Header("Scriptable Objects")]
    [SerializeField] private List<CharacterData> soCharacters;
    [SerializeField] private List<RecipeData> soRecipes;

    //Instantiated SO
    private List<CharacterData> charactersRuntime;
    private List<RecipeData> recipesRuntime;

    public List<CharacterData> charactersOfTheDay = new List<CharacterData>();
    public bool characterIsDiscovered;
    public bool recipeIsDiscovered;
    public event Action OnCharacterDiscovered;
    public event Action OnRecipeDiscovered;

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

    private void InitializeData()
    {
        charactersRuntime = new List<CharacterData>();
        recipesRuntime = new List<RecipeData>();

        // On crée une copie de chaque ScriptableObject
        foreach (var c in soCharacters)
            charactersRuntime.Add(Instantiate(c));

        foreach (var r in soRecipes)
            recipesRuntime.Add(Instantiate(r));
    }

    // Méthodes d'accès
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
