using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TrainingManager : MonoBehaviour
{
    [SerializeField]
    protected Image instructionsImage;

    [SerializeField]
    protected Image resultsImage;

    public bool GamePaused { get; protected set; } = false;

    protected GuildMember guildMember;
    protected int score = 0;
    protected int exp = 0;

    public void SetGuildMember(GuildMember _guildMember)
    {
        guildMember = _guildMember;
    }

    public void OpenInstructions()
    {
        StartCoroutine(OpenInstructionsCRTN());
    }

    private IEnumerator OpenInstructionsCRTN()
    {
        yield return new WaitForSeconds(0.5f);
        instructionsImage.GetComponent<Animator>().SetTrigger("Open");
    }
    public void AddPoints(int points)
    {
        score += points;
    }

    public virtual void ApplyResults()
    {
        Debug.LogWarning($"ApplyResults() has not been implemented in {name}");
    }
}
