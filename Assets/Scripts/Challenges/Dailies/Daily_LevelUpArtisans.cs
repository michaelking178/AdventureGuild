using UnityEngine;

public class Daily_LevelUpArtisans : DailyChallenge
{
    public override void Init()
    {
        base.Init();
        ObjectiveQuantity = Random.Range(3, 6);
        Objective = $"Level up Artisans {ObjectiveQuantity} times";
        GuildMember.OnArtisanLevelUp += AddProgress;
    }

    public override void EndChallenge()
    {
        GuildMember.OnArtisanLevelUp -= AddProgress;
    }
}
