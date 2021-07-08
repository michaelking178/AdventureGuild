using UnityEngine.UI;

public class TrainingXPBoost : Boost
{
    public override void Apply()
    {
        SetBoostBool(true);
        GetComponent<Button>().interactable = false;
    }

    public void EndBoost()
    {
        SetBoostBool(false);
        GetComponent<Button>().interactable = true;
    }

    protected override void SetBoostBool(bool value)
    {
        boostManager.IsTrainingExpBoosted = value;
    }

    protected override bool GetBoostBool()
    {
        return boostManager.IsTrainingExpBoosted;
    }
}
