using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;
using vinkn;

public class StoryParser : MonoBehaviour
{
    // ===== EVENTS =====
    [Header("Events")]
    [SerializeField] UnityEvent<string> OnContentChange;
    [SerializeField] UnityEvent<SOCharacter> OnCharacterChange;

    string lastCharacter;
    string content;

    private void Start()
    {
        OnCharacterChange?.Invoke(null);
    }

    public void Parse(string line, List<string> tags)
    {
        content = line;

        if (IsACharacter())
        {
            string id = CharacterID();
            if (!string.IsNullOrEmpty(id) && id != lastCharacter)
            {
                lastCharacter = id;
                ChangeCharacter(id);
            }

            if (string.IsNullOrEmpty(id))
            {
                content = content.Substring(1);
            }
            else
            {
                content = CharacterContent();
            }
        }
        else if (lastCharacter != null)
        {
            OnCharacterChange?.Invoke(null);
            lastCharacter = null;
        }

        OnContentChange?.Invoke(content);
    }

    public virtual bool IsACharacter()
    {
        if (content.StartsWith(':'))
            return true;
        else
        {
            Regex rx = new Regex("^[^: ]+:");
            return rx.IsMatch(content);
        }
    }

    public virtual string CharacterID()
    {
        return content.Substring(0, content.IndexOf(':'));
    }

    public virtual string CharacterContent()
    {
        return content.Substring(lastCharacter.Length + 1);
    }

    protected virtual void ChangeCharacter(string id)
    {
        SOCharacter charac = VNEngine.GetCharacterDefinition(id);

        if (charac != null)
        {
            OnCharacterChange?.Invoke(charac);
        }
        else
        {
            OnCharacterChange?.Invoke(null);
        }
    }
}
