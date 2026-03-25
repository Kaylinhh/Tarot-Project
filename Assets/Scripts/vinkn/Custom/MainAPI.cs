using UnityEngine;

namespace vinkn
{
    [RequireComponent(typeof(StoryReader))]
    public class MainAPI : MonoBehaviour
    {
        StoryReader reader;

        void Start()
        {
            reader = GetComponent<StoryReader>();
            SetupGlobalMethods();
        }

        void SetupGlobalMethods()
        {
            // Background control
            reader.story.BindExternalFunction("changeBg", (string name) => VNEngine.Instance?.FadeToBackground(name, 0));
            reader.story.BindExternalFunction("fadeBg", (string name, float duration) => VNEngine.Instance?.FadeToBackground(name, duration));

            // Character visibility
            reader.story.BindExternalFunction("show", (string name) => VNEngine.Instance?.DisplayChar(name, true));
            reader.story.BindExternalFunction("hide", (string name) => VNEngine.Instance?.DisplayChar(name, false));
            reader.story.BindExternalFunction("fadeIn", (string name, float duration) => VNEngine.Instance?.DisplayChar(name, true, duration));
            reader.story.BindExternalFunction("fadeOut", (string name, float duration) => VNEngine.Instance?.DisplayChar(name, false, duration));

            // Character manipulation
            reader.story.BindExternalFunction("flipX", (string name) => VNEngine.Instance?.FlipXChar(name));
            reader.story.BindExternalFunction("flipY", (string name) => VNEngine.Instance?.FlipYChar(name));
            reader.story.BindExternalFunction("face", (string character, string name) => VNEngine.Instance?.SetEmotion(character, name));
            reader.story.BindExternalFunction("moveTo", (string character, string name, float duration) => VNEngine.Instance?.MoveTo(character, name, duration));
            reader.story.BindExternalFunction("placeTo", (string character, string name) => VNEngine.Instance?.MoveTo(character, name, 0));
            
            // Scene management 
            reader.story.BindExternalFunction("changeScene", (string sceneName) => VNEngine.Instance?.ChangeScene(sceneName));
            
            // Game progression
            reader.story.BindExternalFunction("meetCharacter", (string characterName) => VNEngine.Instance?.MeetCharacter(characterName));
            reader.story.BindExternalFunction("discoverRecipe", (string recipeName) => VNEngine.Instance?.DiscoverRecipe(recipeName));
            reader.story.BindExternalFunction("gainAffinity", (string characterName, int quantity) => VNEngine.Instance?.GainAffinity(characterName, quantity));
            reader.story.BindExternalFunction("endDay", () => VNEngine.Instance?.EndDay());
        }
    }
}
