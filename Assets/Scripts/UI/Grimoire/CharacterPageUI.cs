using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class CharacterPageUI : MonoBehaviour, IPageFiller<CharacterData>
{
    public TextMeshProUGUI nameText;
    public Image portraitImage;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI traitsText;
    public TextMeshProUGUI friendshipText;

    public void FillPage(CharacterData character)
    {
        nameText.text = character.characterName;
        portraitImage.sprite = character.portrait;
        descriptionText.text = character.description;

        // Exemple simple : afficher les traits séparés par des virgules
        traitsText.text = "Traits: " + string.Join(", ", character.traits);

        // Exemple de stat : niveau d’amitié
        friendshipText.text = "Friendship: " + character.friendshipLevel;
    }
}
