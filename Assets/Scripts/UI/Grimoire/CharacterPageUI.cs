using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterPageUI : MonoBehaviour, IPageFiller<CharacterData>
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Image portraitImage;
    [SerializeField] private Image portraitLocked;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI traitsText;
    [SerializeField] private TextMeshProUGUI friendshipText;

    [Header("Locked Placeholder")]
    [SerializeField] private string lockedName = "???";
    [SerializeField] private string lockedDescription = "You haven't met this person yet.";
    [SerializeField] private string lockedTraits = "???";
    [SerializeField] private string lockedFriendship = "???";

    private void Start()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    }

    public void FillPage(CharacterData character)
    {

        Debug.Log("character page got instantiated");

        if (character.isDiscovered == true)
        {
            nameText.text = character.characterName;
            portraitImage.sprite = character.portrait;
            descriptionText.text = character.description;
            friendshipText.text = "Friendship: " + character.friendshipLevel;
        }
        else
        {
            nameText.text = lockedName;
            portraitImage.sprite = character.portraitLocked;
            descriptionText.text = lockedDescription;
            traitsText.text = "Traits: " + lockedTraits;
            friendshipText.text = "Friendship: " + lockedFriendship;
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    }
}
