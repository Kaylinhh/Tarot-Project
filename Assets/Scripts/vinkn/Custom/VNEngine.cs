using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace vinkn
{
    public class VNEngine : MonoBehaviour
    {
        // ===== SINGLETON =====
        public static VNEngine Instance { get; private set; }

        // ===== EVENTS =====
        public static event System.Action OnSceneChanging;
        public static event System.Action OnDialogueMode;
        public static event System.Action OnMinigameMode;

        // ===== SERIALIZED FIELDS =====
        [SerializeField] List<Character> characters;
        [SerializeField] List<SOCharacter> charactersDefinitions;
        [SerializeField] List<EDisplayable> backgrounds;
        [SerializeField] List<DisplayAnchor> anchors;
        [SerializeField] List<CharacterData> allCharactersData;
        [SerializeField] List<RecipeData> allRecipesData;

        // ===== PRIVATE FIELDS =====
        GameSceneManager gameSceneManager;
        protected EDisplayable currentBg { get; set; }

        void Awake()
        {
            // ===== SINGLETON SETUP =====
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                return; 
            }

            // ===== INITIALIZATION =====
            try
            {
                currentBg = null;
                gameSceneManager = FindAnyObjectByType<GameSceneManager>();

                allCharactersData = new List<CharacterData>();
                allRecipesData = new List<RecipeData>();
            }
            catch (System.Exception e)
            {
                Debug.LogError($"[VNE] EXCEPTION IN AWAKE: {e.Message}");
            }
        }

        // ===== UNITY LIFECYCLE =====
        void Start()
        {
            // getting data
            if (DataManager.Instance != null)
            {
                allCharactersData = DataManager.Instance.GetCharacters();
                allRecipesData = DataManager.Instance.GetRecipes();
                Debug.Log($"[VNE] Loaded {allCharactersData.Count} characters, {allRecipesData.Count} recipes from DataManager");
            }
            else
            {
                Debug.LogError("[VNE] DataManager.Instance still NULL in Start!");
            }
        }

        void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }

        //SCENE MANAGEMENT =====
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "Main")
                RegisterSceneObjects();
        }

        private void RegisterSceneObjects()
        {
            characters.Clear();
            backgrounds.Clear();
            anchors.Clear();

            Character[] foundCharacters = FindObjectsOfType<Character>();

            foreach (var c in foundCharacters)
            {
                AddCharacter(c);
            }

            EDisplayable[] foundBackgrounds = FindObjectsOfType<EDisplayable>();

            foreach (var bg in foundBackgrounds)
            {
                AddBackground(bg);
            }

            DisplayAnchor[] foundAnchors = FindObjectsOfType<DisplayAnchor>();

            foreach (var a in foundAnchors)
            {
                Add(a);
            }

            Debug.Log($"[VNE] AFTER registration: {characters.Count} characters, {backgrounds.Count} backgrounds, {anchors.Count} anchors");
        }

        public void ChangeScene(string sceneName)
        {
            if (sceneName == "BarView")
                OnMinigameMode?.Invoke();
            else
                OnDialogueMode?.Invoke();

            OnSceneChanging?.Invoke();
            gameSceneManager.ChangeScene(sceneName);
        }


        // ===== ADD METHODS =====
        public void Add(DisplayAnchor a)
        {
            if (a && !anchors.Contains(a))
                anchors.Add(a);
        }

        public void AddCharacter(Character c)
        {
            if (c != null && !characters.Contains(c))
            {
                if (c.details != null)
                    charactersDefinitions.Add(c.details); //also adding SOCharacter
                else
                    Debug.LogError("Details not found for '" + c.name + "'");

                characters.Add(c);
            }
        }

        public void AddBackground(EDisplayable sb)
        {
            if (sb != null && !backgrounds.Contains(sb))
                backgrounds.Add(sb);
        }

        public void Add(EDisplayable item, InkObjectType type)
        {
            switch(type)
            {
                case InkObjectType.BACKGROUND:
                    if (!backgrounds.Contains(item))
                        backgrounds.Add(item);
                    break;
                case InkObjectType.CHARACTER:
                    if (!characters.Contains((Character)item))
                        characters.Add((Character)item);
                    break;
            }
        }

        // ===== FIND METHODS =====
        public DisplayAnchor FindAnchor(string objName)
        {
            objName = objName.ToLower();
            DisplayAnchor a = anchors.Find(x => (x?.tagID.ToLower().Equals(objName) ?? false));
            if (a == null)
                throw new UnassignedReferenceException("'" + objName + "' anchor not found");

            return a;
        }

        public EDisplayable FindBackground(string bg)
        {
            bg = bg.ToLower();

            EDisplayable bgObj = backgrounds.Find(x => (x?.tagID.ToLower().Equals(bg) ?? false));

            if (bgObj == null)
                throw new UnassignedReferenceException("'" + bg + "' background not found");

            return bgObj;
        }

        // ===== GETTERS =====
        public static SOCharacter GetCharacterDefinition(string id)
        {
            if (Instance == null)
            {
                Debug.LogError("[VNEngine] Instance is null! Cannot get character definition.");
                return null;
            }

            return Instance.FindCharacterDefinition(id);
        }

        public SOCharacter FindCharacterDefinition(string id)
        {
            id = id.ToLower();
            return charactersDefinitions.Find(x => x.tag.ToLower().Equals(id));
        }

        public Character FindCharacter(string id)
        {
            id = id.ToLower();
            Character c = characters.Find(x => (x?.tagID.ToLower().Equals(id) ?? false));

            if (c == null)
                throw new UnassignedReferenceException("'" + id + "' character not found");

            return c;
        }

        // ===== BG CONTROL =====
        public virtual void FadeToBackground(string name, float duration)
        {
            try
            {
                if (currentBg != null)
                {
                    currentBg.Fade(0, duration);
                }

                currentBg = FindBackground(name);
                currentBg.Fade(1, duration);
            }
            catch (System.Exception e)
            {
                Debug.LogWarning($"[VNE] Could not change background to '{name}': {e.Message}");
            }
        }

        // ===== CHARACTER CONTROL =====
        public virtual void FlipXChar(string item)
        {
            Character c = FindCharacter(item);
            c?.FlipX();
        }

        public virtual void FlipYChar(string item)
        {
            Character c = FindCharacter(item);
            c?.FlipY();
        }

        public virtual void DisplayChar(string item, bool display)
        {
            Character c = FindCharacter(item);

            if (display)
                c.Show();
            else
                c.Hide();
        }

        public virtual void DisplayChar(string item, bool display, float duration)
        {
            Character c = FindCharacter(item);

            if (display)
                c?.Fade(1, duration);
            else
                c?.Fade(0, duration);
        }

        public void SetEmotion(string character, string name)
        {
            Character c = FindCharacter(character);
            c?.ChangeContent(name);
        }

        public void MoveTo(string character, string name, float duration)
        {
            Character c = FindCharacter(character);
            DisplayAnchor a = FindAnchor(name);

            if (duration > 0)
                c.Move(a.transform.position, duration);
            else
                c.transform.position = a.transform.position;
        }

        // ===== GAME PROGRESSION =====
        public void MeetCharacter(string characterName)
        {
            // look for characterData
            var characterData = allCharactersData.FirstOrDefault(c => c.characterName == characterName);

            if (characterData != null)
            {
                characterData.hasMetToday = true;
                DataManager.Instance.NotifyCharacterDiscovered(); //trigger event for the grimoire
            }
            else
            {
                Debug.LogWarning($"Aucun CharacterData trouvé pour le nom : {characterName}");
            }

            if (characterData.isDiscovered == false)
            {
                characterData.isDiscovered = true;
            }
        }

        public void DiscoverRecipe(string recipeName)
        {
            var recipeData = allRecipesData.FirstOrDefault(c => c.recipeName == recipeName);

            if (recipeData != null)
            {
                recipeData.isDiscovered = true;

                if (DataManager.Instance != null)
                {
                    DataManager.Instance.NotifyRecipeDiscovered();
                }
            }
            else
            {
                Debug.LogWarning($"No RecipeData found for {recipeName}.");
            }
        }

        public void GainAffinity(string characterName, int quantity)
        {
            var characterData = allCharactersData.FirstOrDefault(c => c.characterName == characterName);

            if (characterData != null)
            {
                characterData.friendshipLevel += quantity;
                characterData.affinityGainedToday += quantity;
            }
            else
            {
                Debug.LogWarning($"Character {characterName} not found!");
            }
        }

        public void EndDay()
        {
            List<CharacterData> charactersOfTheDay = new List<CharacterData>();
            string names = string.Join(", ", charactersOfTheDay.ConvertAll(c => c.characterName));

            foreach (var c in allCharactersData)
            {
                if (c.hasMetToday)
                    charactersOfTheDay.Add(c);
            }

            if (DataManager.Instance != null)
            {
                DataManager.Instance.charactersOfTheDay = new List<CharacterData>(charactersOfTheDay);
                Debug.Log("Stored " + charactersOfTheDay.Count + " characters in DataManager");
            }
            else
            {
                Debug.LogError("No DataManager found!");
            }

            // reset for the day after
            foreach (var c in allCharactersData)
                c.hasMetToday = false;
        }
    }
}