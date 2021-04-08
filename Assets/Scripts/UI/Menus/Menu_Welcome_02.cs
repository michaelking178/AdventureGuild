using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_Welcome_02 : Menu
{
    public override void Open()
    {
        FindObjectOfType<HeroMaker>().CreateHero();
        base.Open();
    }
}
