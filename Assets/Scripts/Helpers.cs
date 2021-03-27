using System.Collections.Generic;
using UnityEngine;

public static class Helpers
{
    private static List<int> ids = new List<int>();

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
        BoostManager boostManager = GameObject.FindObjectOfType<BoostManager>();
        string reward = "";

        if (quest.Reward.Exp != 0)
        {
            if (boostManager.IsQuestExpBoosted || quest.ExpBoosted)
                reward += $"{quest.Reward.Exp} Experience (+{quest.Reward.BoostExp} bonus)\n";
            else
                reward += quest.Reward.Exp.ToString() + " Experience\n";
        }
        if (quest.Reward.Gold != 0)
        {
            if (boostManager.IsQuestGoldBoosted || quest.GoldBoosted)
                reward += $"{quest.Reward.Gold} Gold (+{quest.Reward.BoostGold} bonus)\n";
            else
                reward += quest.Reward.Gold.ToString() + " Gold\n";
        }
        if (quest.Reward.Wood != 0)
        {
            if (boostManager.IsQuestWoodBoosted || quest.WoodBoosted)
                reward += $"{quest.Reward.Wood} Wood (+{quest.Reward.BoostWood} bonus)\n";
            else
                reward += quest.Reward.Wood.ToString() + " Wood\n";
        }
        if (quest.Reward.Iron != 0)
        {
            if (boostManager.IsQuestIronBoosted || quest.IronBoosted)
                reward += $"{quest.Reward.Iron} Iron (+{quest.Reward.BoostIron} bonus)\n";
            else
                reward += quest.Reward.Iron.ToString() + " Iron\n";
        }
        if (quest.Reward.Renown != 0)
            reward += quest.Reward.Renown.ToString() + " Renown\n";
        if (quest.Reward.SkillExp != 0)
            reward += quest.Reward.SkillExp.ToString() + " " + quest.QuestSkill.ToString() + " Experience\n";
        return reward;
    }

    public static string IncidentRewardStr(Incident incident)
    {
        string reward = "";
        {
            if (incident.reward != null)
            {
                if (incident.reward.Experience != 0)
                {
                    reward += incident.reward.Experience.ToString() + " Experience\n";
                }
                if (incident.reward.Gold != 0)
                {
                    reward += incident.reward.Gold.ToString() + " Gold\n";
                }
                if (incident.reward.Wood != 0)
                {
                    reward += incident.reward.Wood.ToString() + " Wood\n";
                }
                if (incident.reward.Iron != 0)
                {
                    reward += incident.reward.Iron.ToString() + " Iron\n";
                }
                if (incident.reward.Hitpoints != 0)
                {
                    reward += incident.reward.Hitpoints.ToString() + " Hitpoints\n";
                }
            }
        }
        return reward;
    }

    public static string FormatTimer(int timeRemaining)
    {
        int hours = 0;
        int minutes = 0;

        if (timeRemaining > 3599)
        {
            hours = Mathf.FloorToInt(timeRemaining / 3600);
            timeRemaining %= 3600;
        }
        if (timeRemaining > 59)
        {
            minutes = Mathf.FloorToInt(timeRemaining / 60);
            timeRemaining %= 60;
        }

        string hoursStr;
        string minStr;
        string secStr;
        if (hours < 10)
        {
            hoursStr = $"0{hours}";
        }
        else
        {
            hoursStr = hours.ToString();
        }
        if (minutes < 10)
        {
            minStr = $"0{minutes}";
        }
        else
        {
            minStr = minutes.ToString();
        }
        if (timeRemaining < 10)
        {
            secStr = $"0{timeRemaining}";
        }
        else
        {
            secStr = timeRemaining.ToString();
        }

        return $"{hoursStr}:{minStr}:{secStr}";
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

    // Todo: Keep an eye on GenerateId(). Make sure it works.
    public static int GenerateId()
    {
        bool isNew = true;
        int a = Random.Range(3334, 33333);
        int b = Random.Range(3334, 33333);
        int c = Random.Range(3334, 33333);
        int newId = a + b + c;

        if (ids.Count > 0)
        {
            foreach (int id in ids)
            {
                if (id == newId)
                {
                    isNew = false;
                }
            }
        }
        else isNew = true;

        if (isNew)
        {
            ids.Add(newId);
            return newId;
        }
        else
        {
            Debug.Log("Helper's generated ID is not original. Trying again...");
            return GenerateId();
        }
    }
}
