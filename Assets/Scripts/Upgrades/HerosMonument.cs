public class HerosMonument : TierUpgrade
{
    public override void Apply()
    {
        base.Apply();
        UnityEngine.Debug.LogWarning("Hero's Monument does not have an upgrade effect!");
        // Do something upgradey. Enable Legend Levelling?
    }
}
