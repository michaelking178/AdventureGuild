using UnityEngine;

public class BoostManager : MonoBehaviour
{
    public bool IsQuestExpBoosted = false;
    public bool IsQuestGoldBoosted = false;
    public bool IsQuestWoodBoosted = false;
    public bool IsQuestIronBoosted = false;

    public Boost GetBoost(string _name)
    {
        foreach(GameObject child in Helpers.GetChildren(gameObject))
        {
            if (child.GetComponent<Boost>().Name == _name)
            {
                return child.GetComponent<Boost>();
            }
        }
        Debug.LogWarning("Boost Manager could not find Boost: " + _name);
        return null;
    }
}
