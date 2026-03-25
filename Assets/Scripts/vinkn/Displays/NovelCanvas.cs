using Ink.Runtime;
using System.Collections.Generic;
using UnityEngine;
using vinkn;

public class NovelCanvas : MonoBehaviour
{
    // ===== UI REFERENCES =====    
    [SerializeField] CharacterDisplay character;
    [SerializeField] GameObject charNameTextBox;
    [SerializeField] GameObject storyDisplay;
    [SerializeField] ChoiceDisplay choices;

    // ===== LIFECYCLE =====
    void OnEnable()
    {
        VNEngine.OnSceneChanging += HideUI;
    }

    void OnDisable()
    {
        VNEngine.OnSceneChanging -= HideUI;
    }

    // ===== EVENT HANDLERS =====
    void HideUI()
    {
        DisplayUI(false);
    }

    public void OnChoicesDisplay(List<Choice> selection)
    {
        DisplayStory(false);
        choices.DisplayChoices(selection);
    }

    public void OnChangeCharacter(SOCharacter ch)
    {
        if (ch != null)
        {
            character.SetCharacter(ch);
            character.gameObject.SetActive(true);
            charNameTextBox.SetActive(true);
        }
        else
        {
            character.gameObject.SetActive(false);
            charNameTextBox.SetActive(false);
        }
    }

    // ===== DISPLAY FUNCTIONS =====
    public void DisplayStory(bool active)
    {
        if (storyDisplay.activeSelf != active)
        {
            storyDisplay.SetActive(active);
            choices.gameObject.SetActive(!active);
        }
    }

    public void DisplayUI(bool active)
    {
        if (active)
        {
            storyDisplay.SetActive(true);
            choices.gameObject.SetActive(false);
        }
        else
        {
            storyDisplay.SetActive(false);
            choices.gameObject.SetActive(false);
        }
    }

    public void OnMinigameStart()
    {
        DisplayUI(false);
    }

    public void OnMinigameEnd()
    {
        DisplayStory(true);
    }
}
