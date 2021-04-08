using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestPopup : PopupMenu
{
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

    public void Populate(Quest quest)
    {
        title.text = quest.questName;
        contractor.text = quest.contractor;
        reward.text = Helpers.QuestRewardStr(quest);
        briefing.text = quest.description;
        level.text = $"Level {quest.level}";
        time.text = Helpers.FormatTimer(quest.time);
        foreach (TextSizer textSizer in GetComponentsInChildren<TextSizer>())
        {
            textSizer.Refresh();
        }
        if (GetComponentInChildren<Scrollbar>() != null)
            GetComponentInChildren<Scrollbar>().value = 1;
        base.Populate();
    }
}
