using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{
    private int health;
    private int exp;
    private int level;

    public void AddExp(int _exp)
    {
        exp += _exp;
    }

    public void SetAvatar(Sprite _avatar)
    {
        //person.avatar = _avatar;
    }

    public void UpdateHealth(int change)
    {
        health += change;
    }
}
