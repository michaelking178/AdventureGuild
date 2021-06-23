using System;
using System.Collections;
using System.Collections.Generic;
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

    private Trophy trophy;
    private Color lockedColor = new Color(0, 0, 0, 0.5f);

    public void SetTrophy(Trophy _trophy)
    {
        trophy = _trophy;
    }

    public void UpdateTrophyItem()
    {
        if (trophy != null)
        {
            trophyNameTxt.text = trophy.Name;
            descriptionTxt.text = trophy.Description;

            if (trophy.IsUnlocked)
            {
                unlockDate.gameObject.SetActive(true);
                unlockDateTxt.text = trophy.UnlockDate.ToString();
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
