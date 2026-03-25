using TMPro;
using UnityEngine;

public class CharacterDisplay : MonoBehaviour
{
    TMP_Text characterName; 
    void Awake()
    {
        characterName = GetComponent<TMP_Text>();
    }

    public void SetCharacter(SOCharacter ch)
    {
        characterName.text = ch.characterName;
        characterName.color = ch.characterColor;
    }
}
