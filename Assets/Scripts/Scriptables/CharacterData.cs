using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Grimoire/Character")]
public class CharacterData : ScriptableObject
{
    public string characterName;
    public Sprite portrait;
    [TextArea] public string description;
    public int friendshipLevel;
    public string[] traits;
}
