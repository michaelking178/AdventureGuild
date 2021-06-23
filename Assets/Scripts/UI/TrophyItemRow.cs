using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TrophyItemRow : MonoBehaviour
{
    [SerializeField]
    private Image trophyImage;

    [SerializeField]
    private TextMeshProUGUI trophyNameTxt;

    [SerializeField]
    private TextMeshProUGUI descriptionTxt;

    [SerializeField]
    private TextMeshProUGUI unlockDate;

    [SerializeField]
    private TextMeshProUGUI unlockDateTxt;

    public Trophy Trophy { get; private set; }
    private Color lockedColor = new Color(0, 0, 0, 0.5f);

    public void SetTrophy(Trophy _trophy)
    {
        Trophy = _trophy;
    }

    public void UpdateTrophyItem()
    {
        if (Trophy != null)
        {
            trophyNameTxt.text = Trophy.Name;
            descriptionTxt.text = Trophy.Description;

            if (Trophy.IsUnlocked)
            {
                unlockDate.gameObject.SetActive(true);
                unlockDateTxt.text = Trophy.UnlockDate.ToString("G");
                trophyImage.color = Color.white;
                trophyNameTxt.color = new Color(0.33f, 1, 0.2f);
            }
            else
            {
                unlockDate.gameObject.SetActive(false);
                unlockDateTxt.text = "";
                trophyImage.color = lockedColor;
                trophyNameTxt.color = new Color(0.75f, 0.75f, 0.75f);
            }
        }
    }
}
