using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupContentQuests : MonoBehaviour
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

    public void Init(Quest quest)
    {
        contractor.text = quest.contractor;
        reward.text = Helpers.QuestRewardStr(quest);
        briefing.text = quest.description;
        level.text = $"Level {quest.level}";
        time.text = Helpers.FormatTimer(quest.time);
    }
}
