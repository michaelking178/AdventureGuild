using TMPro;
using UnityEngine;

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
        clickBlocker.SetActive(true);
        anim.SetTrigger("Open");
    }
}
