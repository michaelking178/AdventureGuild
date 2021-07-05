using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestPopup : PopupMenu
{
    #region Data

    [SerializeField]
    private TextMeshProUGUI contractor;

    [SerializeField]
    private TextMeshProUGUI reward;

    [SerializeField]
    private TextMeshProUGUI briefing;

    [SerializeField]
    private TextMeshProUGUI level;

    [SerializeField]
    private TextMeshProUGUI time;

    #endregion

    public void Populate(QuestPopupData popupData)
    {
        title.text = popupData.Title;
        contractor.text = popupData.Contractor;
        reward.text = popupData.Reward;
        briefing.text = popupData.Briefing;
        level.text = $"Level {popupData.Level}";
        time.text = popupData.Time;
        foreach (TextSizer textSizer in GetComponentsInChildren<TextSizer>())
        {
            textSizer.Refresh();
        }
        if (GetComponentInChildren<Scrollbar>() != null)
            GetComponentInChildren<Scrollbar>().value = 1;
        base.Populate();
    }
}
