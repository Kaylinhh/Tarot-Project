using System.Collections.Generic;
using TMPro;
using UnityEngine;
using vinkn;

public class DialogueLogUI : MonoBehaviour
{
    // ===== UI ELEMENTS ======
    [SerializeField] private GameObject logPanel;
    [SerializeField] private Transform contentParent;
    [SerializeField] private GameObject entryPrefab;

    public void ToggleLog()
    {
        bool isActive = !logPanel.activeSelf;
        logPanel.SetActive(isActive);

        if (isActive)
        {
            RefreshLog();
        }
    }

    private Dictionary<string, Color> colorCache = new Dictionary<string, Color>();

    void RefreshLog()
    {
        colorCache.Clear(); 

        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        var history = DialogueHistory.Instance.GetHistory();

        foreach (var entry in history)
        {
            GameObject entryObj = Instantiate(entryPrefab, contentParent);
            TextMeshProUGUI text = entryObj.GetComponentInChildren<TextMeshProUGUI>();
            if (entry.content.Contains(":"))
            {
                string[] parts = entry.content.Split(new[] { ':' }, 2);
                string speaker = parts[0].Trim();
                string dialogue = parts[1].Trim();

                // Use the cache
                Color color = GetCachedColor(speaker);
                text.text = $"<color=#{ColorUtility.ToHtmlStringRGB(color)}><b>{speaker}:</b></color> {dialogue}";
            }
            else
            {
                text.text = entry.content;
            }
        }
    }

    Color GetCachedColor(string speaker)
    {
        //if never looked up before, get and store in cache
        if (!colorCache.ContainsKey(speaker))
        {
            colorCache[speaker] = GetColorForSpeaker(speaker);
        }

        return colorCache[speaker];
    }

    public static Color GetColorForSpeaker(string speakerName)
    {
        VNEngine engine = FindFirstObjectByType<VNEngine>();

        if (engine != null)
        {
            SOCharacter character = engine.FindCharacterDefinition(speakerName);

            if (character != null)
            {
                return character.characterColor;
            }
        }

        // default color
        return Color.white;
    }

    void Update()
    {
        // Hotkey to open
        if (Input.GetKeyDown(KeyCode.L))
        {
            ToggleLog();
        }
    }
}