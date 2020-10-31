using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Levelling
{
    /// <summary>
    /// LevelValues returns the experience required for the given character level (index)
    /// </summary>
    public static int[] GuildMemberLevel = { 0, 250, 750, 1500, 2500, 4000, 6000, 8500, 11000, 15000, 20000, 27500, 35000, 45000, 60000, 75000, 100000 };

    public static int[] SkillLevel = { 0, 500, 1250, 2000 };
}
