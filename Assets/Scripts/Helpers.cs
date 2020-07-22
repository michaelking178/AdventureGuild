using System.Collections.Generic;
using UnityEngine;

public static class Helpers
{
    /// <summary>
    /// Returns a list of child game objects.
    /// </summary>
    /// <param name="gameObject"></param>
    /// <returns></returns>
    public static List<GameObject> GetChildren(this GameObject gameObject)
    {
        List<GameObject> children = new List<GameObject>();
        foreach (Transform tran in gameObject.transform)
        {
            children.Add(tran.gameObject);
        }
        return children;
    }

    public static string QuestRewardStr(Quest quest)
    {
        string reward = "";
        if (quest.Reward.Gold != 0)
        {
            reward += quest.Reward.Gold.ToString() + " Gold";
        }
        if (quest.Reward.Wood != 0)
        {
            reward += ", " + quest.Reward.Wood.ToString() + " Wood";
        }
        if (quest.Reward.Iron != 0)
        {
            reward += ", " + quest.Reward.Iron.ToString() + " Iron";
        }
        return reward;
    }
}
