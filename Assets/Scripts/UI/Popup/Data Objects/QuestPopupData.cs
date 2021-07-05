public class QuestPopupData : PopupData
{
    public string Contractor { get; set; } = string.Empty;
    public string Briefing { get; set; } = string.Empty;
    public string Level { get; set; } = string.Empty;
    public string Time { get; set; } = string.Empty;
    public string Reward { get; set; } = string.Empty;

    public QuestPopupData(Quest quest)
    {
        Title = quest.questName;
        Contractor = quest.contractor;
        Briefing = quest.description;
        Level = quest.level.ToString();
        Time = Helpers.FormatTimer(quest.time);
        Reward = Helpers.QuestRewardStr(quest);
        ConfirmText = "Begin";
    }
}
