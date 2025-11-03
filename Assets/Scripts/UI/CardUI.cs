using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    [SerializeField] private Image cardImage;
    [SerializeField] private TMP_Text characterNameText;
    [SerializeField] private TMP_Text summaryText;

    public void Setup(CharacterData character)
    {
        cardImage.sprite = character.arcanaSprite;
        characterNameText.text = character.characterName;
        summaryText.text = character.GetDailySummary();
    }

}
