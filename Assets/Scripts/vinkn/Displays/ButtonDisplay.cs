using Ink.Runtime;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonDisplay : MonoBehaviour
{
    [SerializeField] TMP_Text content;
    Choice buttonChoice;
    Button btn;

    void Awake()
    {
        btn = GetComponent<Button>();
    }

    public void SetChoice(Choice choice, Action<Choice> callback)
    {
        content.text = choice.text;
        buttonChoice = choice;

        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() => callback.Invoke(buttonChoice));
    }
}
