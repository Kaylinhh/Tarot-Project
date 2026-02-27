using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace vinkn
{
    public class VNEngine : MonoBehaviour
    {
        [SerializeField] List<Character> characters;
        [SerializeField] List<SOCharacter> charactersDefinitions;
        [SerializeField] List<EDisplayable> backgrounds;
        [SerializeField] List<DisplayAnchor> anchors;
        [SerializeField] List<CharacterData> allCharactersData;
        [SerializeField] List<RecipeData> allRecipesData;
        NovelCanvas novelCanvas;
        GameSceneManager gameSceneManager;
       // public GameObject storyReader;

        protected EDisplayable currentBg { get; set; }

        // Start is called before the first frame update
        void Awake()
        {
            Debug.Log("[VNE] ===== AWAKE START =====");

            try
            {
                currentBg = null;
                gameSceneManager = FindAnyObjectByType<GameSceneManager>();
                novelCanvas = FindAnyObjectByType<NovelCanvas>();

                // NE PAS get ici, juste initialiser vide
                allCharactersData = new List<CharacterData>();
                allRecipesData = new List<RecipeData>();

                Debug.Log("[VNE] ===== AWAKE COMPLETE =====");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"[VNE] EXCEPTION IN AWAKE: {e.Message}");
            }
        }

        void Start()
        {
            Debug.Log("[VNE] START CALLED");

            // GET ICI (Start s'exécute après tous les Awake)
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
            Debug.Log("[VNE] OnEnable - Subscribing to sceneLoaded");
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void OnDisable()
        {
            Debug.Log("[VNE] OnDisable - Unsubscribing from sceneLoaded");
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Debug.Log($"[VNE] OnSceneLoaded called - Scene: {scene.name}, Mode: {mode}");

            if (scene.name == "Main")
            {
                Debug.Log($"[VNE] Main detected! Current lists BEFORE clear: {characters.Count} chars, {backgrounds.Count} bgs");
                RegisterSceneObjects();
            }
            else
            {
                Debug.Log($"[VNE] Scene {scene.name} loaded but not Main, skipping registration");
            }
        }

        private void RegisterSceneObjects()
        {
            Debug.Log("[VNE] CLEARING old references...");
            characters.Clear();
            backgrounds.Clear();
            anchors.Clear();

            Debug.Log("[VNE] Finding objects in scene...");

            Character[] foundCharacters = FindObjectsOfType<Character>();
            Debug.Log($"[VNE] FindObjectsOfType<Character> found: {foundCharacters.Length}");

            foreach (var c in foundCharacters)
            {
                Debug.Log($"[VNE]   - Found character: {c.name} (tagID: {c.tagID}) in scene {c.gameObject.scene.name}");
                AddCharacter(c);
            }

            EDisplayable[] foundBackgrounds = FindObjectsOfType<EDisplayable>();
            Debug.Log($"[VNE] FindObjectsOfType<EDisplayable> found: {foundBackgrounds.Length}");

            foreach (var bg in foundBackgrounds)
            {
                Debug.Log($"[VNE]   - Found background: {bg.name} (tagID: {bg.tagID}) in scene {bg.gameObject.scene.name}");
                AddBackground(bg);
            }

            DisplayAnchor[] foundAnchors = FindObjectsOfType<DisplayAnchor>();
            Debug.Log($"[VNE] FindObjectsOfType<DisplayAnchor> found: {foundAnchors.Length}");

            foreach (var a in foundAnchors)
            {
                Debug.Log($"[VNE]   - Found anchor: {a.name} (tagID: {a.tagID}) in scene {a.gameObject.scene.name}");
                Add(a);
            }

            Debug.Log($"[VNE] AFTER registration: {characters.Count} characters, {backgrounds.Count} backgrounds, {anchors.Count} anchors");
        }

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
                    charactersDefinitions.Add(c.details);
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

        public virtual void FadeToBackground(string name, float duration)
        {
            try
            {
                Debug.Log($"[VNE] FadeToBackground called: {name}");

                if (currentBg != null)
                {
                    currentBg.Fade(0, duration);
                }

                currentBg = FindBackground(name);
                currentBg.Fade(1, duration);

                Debug.Log($"[VNE] Background changed to {name}");
            }
            catch (System.Exception e)
            {
                Debug.LogWarning($"[VNE] Could not change background to '{name}': {e.Message}");
                // Ne pas planter, juste skip
            }
        }

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
        public void ChangeScene(string sceneName)
        {
            // Cache l'UI AVANT de changer de scène
            if (novelCanvas != null)
            {
                novelCanvas.DisplayUI(false);
                Debug.Log("[VNE] UI hidden before scene change");
            }

            gameSceneManager.ChangeScene(sceneName);
        }
        public void MeetCharacter(string characterName)
        {
            // Cherche le CharacterData correspondant au nom
            var characterData = allCharactersData.FirstOrDefault(c => c.characterName == characterName);

            if (characterData != null)
            {
                characterData.hasMetToday = true;
                Debug.Log($"{characterName} a été rencontré(e) aujourd'hui.");
                DataManager.Instance.NotifyCharacterDiscovered();
            }
            else
            {
                Debug.LogWarning($"Aucun CharacterData trouvé pour le nom : {characterName}");
            }

            if (characterData.isDiscovered == false)
            {
                characterData.isDiscovered = true;

                if (DataManager.Instance != null)
                {
                    DataManager.Instance.characterIsDiscovered = true;
                    Debug.Log("Stored " + characterData.isDiscovered + " in DayDataManager");
                }
                else
                {
                    Debug.LogError("No DayDataManager found!");
                }

            }

        }

        public void DiscoverRecipe(string recipeName)
        {
            var recipeData = allRecipesData.FirstOrDefault(c => c.recipeName == recipeName);

            if (recipeData != null)
            {
                recipeData.isDiscovered = true;
                Debug.Log($"La recette suivante a été découverte : {recipeName}.");
                DataManager.Instance.NotifyRecipeDiscovered();
            }
            else
            {
                Debug.LogWarning($"Aucun RecipeData trouvé pour le nom : {recipeName}.");
            }

            if (recipeData.isDiscovered == false)
            {
                recipeData.isDiscovered = true;

                if (DataManager.Instance != null)
                {
                    DataManager.Instance.recipeIsDiscovered = true;
                    Debug.Log("Stored " + recipeData.isDiscovered + " in DayDataManager");
                }
                else
                {
                    Debug.LogError("No DayDataManager found!");
                }

            }

        }

        public void GainAffinity(string characterName, int quantity)
        {
            Debug.Log($"[VNE] GainAffinity called with: '{characterName}'");
            Debug.Log($"[VNE] Available characters: {string.Join(", ", allCharactersData.ConvertAll(c => $"'{c.characterName}'"))}");

            var characterData = allCharactersData.FirstOrDefault(c => c.characterName == characterName);

            if (characterData != null)
            {
                characterData.friendshipLevel += quantity;
                Debug.Log($"L'affinité de {characterName} a augmenté de {quantity}.");
            }
            else
            {
                Debug.LogWarning($"Aucun CharacterData trouvé pour le nom : {characterName}");
            }

        }

        public void EndDay()
        {
            foreach (var c in allCharactersData)
            {
                Debug.Log($"{c.characterName} - hasMetToday: {c.hasMetToday}");
            }

            // On récupère les persos rencontrés aujourd’hui
            List<CharacterData> charactersOfTheDay = new List<CharacterData>();

            string names = string.Join(", ", charactersOfTheDay.ConvertAll(c => c.characterName));
            Debug.Log("1 : " + names);

            foreach (var c in allCharactersData)
            {
                if (c.hasMetToday)
                    charactersOfTheDay.Add(c);
            }

            foreach (var c in charactersOfTheDay)
            {
                Debug.Log($"{c.characterName} - characters of the day: {c.characterName}");
            }

            if (DataManager.Instance != null)
            {
                DataManager.Instance.charactersOfTheDay = new List<CharacterData>(charactersOfTheDay);
                Debug.Log("Stored " + charactersOfTheDay.Count + " characters in DayDataManager");
            }
            else
            {
                Debug.LogError("No DayDataManager found!");
            }

                // On reset pour le lendemain
                foreach (var c in allCharactersData)
                c.hasMetToday = false;
        }
    }
}
