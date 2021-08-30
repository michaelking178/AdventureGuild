using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class GPGSAuthentication : MonoBehaviour
{
    public static PlayGamesPlatform platform;

    private void Awake()
    {
        if (platform == null)
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.DebugLogEnabled = true;
            platform = PlayGamesPlatform.Activate();
        }
    }

    void Start()
    {
        Social.Active.localUser.Authenticate(success =>{});
    }

    public void AddScoreToLeaderboard(string boardId, int score)
    {
        if (Social.Active.localUser.authenticated)
        {
            Social.Active.ReportScore(score, boardId, success => { });
        }
    }

    public void UnlockAchievement(string achievementId, double progress)
    {
        if (Social.Active.localUser.authenticated)
        {
            Social.Active.ReportProgress(achievementId, progress, success => { });
        }
    }

    public void IncrementAchievement(string achievementId, int progress)
    {
        if (Social.Active.localUser.authenticated)
        {
            platform.IncrementAchievement(achievementId, progress, success => { });
        }
    }

    public void ShowLeaderboard()
    {
        if (Social.Active.localUser.authenticated)
        {
            Social.Active.ShowLeaderboardUI();
        }
    }

    public void ShowAchievements()
    {
        if (Social.Active.localUser.authenticated)
        {
            Social.Active.ShowAchievementsUI();
        }
    }
}
