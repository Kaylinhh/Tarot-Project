using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using vinkn;

public class DialogueLogUI : MonoBehaviour
{
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

        Debug.Log("[DialogueLogUI] RefreshLog called");
        Debug.Log($"[DialogueLogUI] contentParent is null? {contentParent == null}");
        Debug.Log($"[DialogueLogUI] DialogueHistory.Instance is null? {DialogueHistory.Instance == null}");

        colorCache.Clear(); // Reset le cache

        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        var history = DialogueHistory.Instance.GetHistory();
        Debug.Log($"[DialogueLogUI] History count: {history.Count}");


        foreach (var entry in history)
        {
            GameObject entryObj = Instantiate(entryPrefab, contentParent);
            TextMeshProUGUI text = entryObj.GetComponentInChildren<TextMeshProUGUI>();
            if (entry.content.Contains(":"))
            {
                string[] parts = entry.content.Split(new[] { ':' }, 2);
                string speaker = parts[0].Trim();
                string dialogue = parts[1].Trim();

                // UTILISE LE CACHE
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
        // Si jamais cherché avant, cherche et stocke
        if (!colorCache.ContainsKey(speaker))
        {
            colorCache[speaker] = GetColorForSpeaker(speaker);
        }

        // Retourne depuis le cache (super rapide)
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

        // Couleur par défaut si pas trouvé
        Debug.LogWarning($"[DialogueLog] No character found for '{speakerName}', using white");
        return Color.white;
    }

    void Update()
    {
        // Hotkey pour ouvrir (L = Log)
        if (Input.GetKeyDown(KeyCode.L))
        {
            ToggleLog();
        }
    }
}