using UnityEngine;

namespace vinkn
{
    public class Character : EDisplayable
    {
        [Header("Character")]
        [SerializeField] SOCharacter definition;

        public SOCharacter details => definition;

        void Awake()
        {
            if (definition != null)
            {
                itemID = definition.tag.ToLower();
                gameObject.name = $"Character_{itemID}";
            }
            else
            {
                Debug.LogWarning($"[Character] No SOCharacter definition on {gameObject.name}!");
                gameObject.name = "Character_Null";
            }
        }
    }
}