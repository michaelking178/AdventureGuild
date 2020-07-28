using System;
using UnityEditor;
using UnityEngine;

[Serializable]
public class GuildMemberData
{
    public Person person;
    public Vocation vocation;
    public string avatarPath;
    public int health;
    public int experience;
    public int level;
    public bool isAvailable;

    public GuildMemberData(Person _person, Vocation _vocation, Sprite _avatar, int _health, int _experience, int _level, bool _isAvailable)
    {
        person = _person;
        vocation = _vocation;
        avatarPath = AssetDatabase.GetAssetPath(_avatar);
        health = _health;
        experience = _experience;
        level = _level;
        isAvailable = _isAvailable;
    }
}
