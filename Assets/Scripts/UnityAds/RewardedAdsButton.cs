using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

[RequireComponent(typeof(Button))]
public class RewardedAdsButton : MonoBehaviour, IUnityAdsListener
{
    Button myButton;
    public string myPlacementId = "rewardedVideo";

    void Start()
    {
        myButton = GetComponent<Button>();

        // Map the ShowRewardedVideo function to the button’s click listener:
        //if (myButton) myButton.onClick.AddListener(ShowRewardedVideo);
    }

    private void FixedUpdate()
    {
        OnUnityAdsReady(myPlacementId);
    }

    // Implement a function for showing a rewarded video ad:
    public void ShowRewardedVideo()
    {
        // Initialize the Ads listener and service:
        Advertisement.AddListener(this);
        Advertisement.Show(myPlacementId);
    }

    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsReady(string placementId)
    {
        // If the ready Placement is rewarded, activate the button: 
        if (Advertisement.IsReady() && placementId == myPlacementId)
        {
            myButton.interactable = true;
        }
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
            // Reward the user for watching the ad to completion.
            GetComponent<BoostObject>().Boost.Apply();
            SaveSystem.SaveGame();
        }
        else if (showResult == ShowResult.Skipped)
        {
            // Do not reward the user for skipping the ad.
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.LogWarning("The ad did not finish due to an error.");
        }
        Advertisement.RemoveListener(this);
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.LogError(message);
        Advertisement.RemoveListener(this);
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // Optional actions to take when the end-users triggers an ad.
    }
}