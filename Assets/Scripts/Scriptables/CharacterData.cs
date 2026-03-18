using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Grimoire/Character")]
public class CharacterData : ScriptableObject
{
    public string characterName;
    public Sprite portrait;
    public Sprite portraitLocked;
    public string arcanaName;
    public Sprite arcanaSprite;
    [TextArea] public string description;
    public int friendshipLevel = 0;
    [HideInInspector] public bool hasMetToday = false;
    public bool isDiscovered = false;

    public string GetDailySummary()
    {  return "This is the daily summary (WIP)."; }
    
}
