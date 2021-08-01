using TMPro;
using UnityEngine;

public class ChallengeItemRow : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI descriptionText;

    [SerializeField]
    private TextMeshProUGUI progressText;

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
            progressText.text = $"Progress: {challenge.Progress}/{challenge.ObjectiveQuantity}";
        }
    }

    public void SetChallenge(Challenge _challenge)
    {
        challenge = _challenge;
    }
}
