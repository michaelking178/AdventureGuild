using UnityEngine;
using UnityEngine.Advertisements;

public class AdvertisementInitializer : MonoBehaviour
{
#if UNITY_IOS
    private readonly string gameId = "4049018";
#elif UNITY_ANDROID
    private readonly string gameId = "4049019";
#endif

    bool testMode = false;

    void Start()
    {
        Advertisement.Initialize(gameId, testMode);
    }

    public bool IsReady()
    {
        return Advertisement.IsReady();
    }
}