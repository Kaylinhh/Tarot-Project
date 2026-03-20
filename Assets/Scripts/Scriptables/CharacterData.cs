using UnityEngine;

[CreateAssetMenu(menuName = "Grimoire/Character")]
public class CharacterData : ScriptableObject
{
    [Header("Character Info")]
    public string characterName;
    public Sprite portrait;
    public Sprite portraitLocked;
    public string arcanaName;
    public Sprite arcanaSprite;

    [TextArea]
    public string description;

    [Header("Progression")]
    public int friendshipLevel = 0;
    public bool isDiscovered = false;

    [HideInInspector]
    public bool hasMetToday = false;

    [System.NonSerialized]
    public int affinityGainedToday = 0; 

    [Header("Daily Summaries")]
    public AffinitySummaries summaries; 

    public string GetDailySummary()
    {
        if (affinityGainedToday <= 0)
            return summaries.lowAffinitySummary;
        else if (affinityGainedToday <= 2)
            return summaries.mediumAffinitySummary;
        else
            return summaries.highAffinitySummary;
    }
}

// Classe pour stocker les 3 textes de summary
[System.Serializable]
public class AffinitySummaries
{
    [Header("<= 0 points - Neutral/Failed")]
    [TextArea(3, 5)]
    public string lowAffinitySummary;

    [Header("1-2 points - Good Progress")]
    [TextArea(3, 5)]
    public string mediumAffinitySummary;

    [Header("3+ points - Excellent Connection")]
    [TextArea(3, 5)]
    public string highAffinitySummary;
}