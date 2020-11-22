using System.Collections;
using UnityEngine;

public class WorkshopUpgrade : Upgrade
{
    // Start is called before the first frame update
    private new void Start()
    {
        base.Start();
        StartCoroutine(DelayedCheckForUpgrade());
    }

    public override void Apply()
    {
        base.Apply();
        populationManager.EnableArtisans();
    }

    public void CheckForUpgrade()
    {
        IsPurchased = FindObjectOfType<PopulationManager>().ArtisansEnabled;
    }

    private IEnumerator DelayedCheckForUpgrade()
    {
        yield return new WaitForSeconds(1);
        CheckForUpgrade();
        yield return null;
    }
}
