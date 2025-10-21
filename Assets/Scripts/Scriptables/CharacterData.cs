using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Grimoire/Character")]
public class CharacterData : ScriptableObject
{
    public string characterName;
    public Sprite portrait;
    public string arcanaName;
    public Sprite arcanaSprite;
    [TextArea] public string description;
    public int friendshipLevel;
    public string[] traits;
    [HideInInspector] public bool hasMetToday = false;

    public string GetDailySummary()
    {  return "This is the daily summary (WIP)."; }

}
