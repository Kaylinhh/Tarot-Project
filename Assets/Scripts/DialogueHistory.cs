using System.Collections.Generic;
using UnityEngine;

public class DialogueHistory : MonoBehaviour
{
    public static DialogueHistory Instance { get; private set; }

    private List<DialogueEntry> history = new List<DialogueEntry>();

    // ===== SINGLETON SETUP =====
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddEntry(string content, List<string> tags)
    {
        history.Add(new DialogueEntry
        {
            content = content,
            tags = tags,
            timestamp = System.DateTime.Now
        });
    }

    public List<DialogueEntry> GetHistory()
    {
        return new List<DialogueEntry>(history);
    }

    public void Clear()
    {
        history.Clear();
    }
}

[System.Serializable]
public class DialogueEntry
{
    public string content;
    public List<string> tags;
    public System.DateTime timestamp;
}