using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Progress;

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


        private DailySummaryUI dailySummaryUI;

        GameSceneManager gameSceneManager;

        StoryReader reader;

        protected EDisplayable currentBg { get; set; }

        // Start is called before the first frame update
        void Awake()
        {
            currentBg = null;
            gameSceneManager = FindAnyObjectByType<GameSceneManager>();
            dailySummaryUI = FindAnyObjectByType<DailySummaryUI>();
            allCharactersData = DataManager.Instance.GetCharacters();
            allRecipesData = DataManager.Instance.GetRecipes();
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
            if (currentBg != null)
            {
                currentBg.Fade(0, duration);
            }

            currentBg = FindBackground(name);
            currentBg.Fade(1, duration);
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
            var characterData = allCharactersData.FirstOrDefault(c => c.characterName == characterName);
            if (characterData != null)
            {
                characterData.friendshipLevel += 1;

                Debug.Log($"L'affinité de {characterName} a augmenté de 1.");
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
