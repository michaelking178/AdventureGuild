using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeItemRow : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI descriptionText;

    [SerializeField]
    private TextMeshProUGUI rewardText;

    [SerializeField]
    private TextMeshProUGUI progressText;

    [SerializeField]
    private Image checkmark;
    
    [SerializeField]
    private Image tintPanel;

    private Challenge challenge;
    private MenuManager menuManager;
    private Menu_Challenges menu_Challenges;

    private void Start()
    {
        menuManager = FindObjectOfType<MenuManager>();
        menu_Challenges = FindObjectOfType<Menu_Challenges>();
    }

    private void FixedUpdate()
    {
        if (menuManager.CurrentMenu == menu_Challenges && challenge != null)
        {
            descriptionText.text = challenge.Objective;
            Debug.Log(challenge.name);
            Debug.Log(challenge.Reward.Gold);
            rewardText.text = $"Reward:\n{challenge.Reward.Gold} Gold, {challenge.Reward.Wood} Wood, {challenge.Reward.Iron} Iron, {challenge.Reward.Renown} Renown";
            if (challenge.IsCompleted)
                progressText.text = $"Complete!";
            else
                progressText.text = $"Progress: {challenge.Progress}/{challenge.ObjectiveQuantity}";

            if (challenge.IsCompleted)
            {
                checkmark.gameObject.SetActive(true);
                tintPanel.gameObject.SetActive(true);
            }
            else
            {
                checkmark.gameObject.SetActive(false);
                tintPanel.gameObject.SetActive(false);
            }
        }
    }

    public void SetChallenge(Challenge _challenge)
    {
        challenge = _challenge;
    }
}
