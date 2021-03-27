using UnityEngine;
using UnityEngine.Advertisements;

public class AdvertisementInitializer : MonoBehaviour
{
    string gameId = "4049019";
    bool testMode = true;

    void Start()
    {
        Advertisement.Initialize(gameId, testMode);
    }
}
