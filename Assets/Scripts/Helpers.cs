using System.Collections.Generic;
using System.IO;
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


    /// <summary>
    /// Produces an easy-to-use string representing the provided quest's rewards (experience not included)
    /// </summary>
    /// <param name="quest"></param>
    /// <returns></returns>
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
        if (quest.Reward.Renown != 0)
        {
            reward += ", " + quest.Reward.Renown.ToString() + " Renown";
        }
        return reward;
    }

    /// <summary>
    /// Compares an object against a list of objects of the same type. Returns a bool based on whether the object occurs uniquely in that list
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="toCompare"></param>
    /// <param name="list"></param>
    /// <returns></returns>
    public static bool IsUniqueMember<T>(T toCompare, List<T> list)
    {
        foreach (T item in list)
        {
            if (toCompare.Equals(item))
            {
                return false;
            }
        }
        return true;
    }

    // Todo: Improve the ID generator. This is not robust.
    public static int GenerateId()
    {
        int a = Random.Range(3334, 33333);
        int b = Random.Range(3334, 33333);
        int c = Random.Range(3334, 33333);
        return a + b + c;
    }
}
