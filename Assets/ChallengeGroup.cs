using UnityEngine;
using UnityEngine.UI;

public class ChallengeGroup : MonoBehaviour
{
    public enum ChallengeType { Daily, Weekly }
    public ChallengeType challengeType;

    public GameObject ContentPanel;

    private bool isExpanded = true;
    private Animator anim;
    private Button button;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        button = GetComponentInChildren<Button>();
    }

    private void FixedUpdate()
    {
        if (ContentPanel.GetChildren().Count == 0)
        {
            Collapse();
            button.interactable = false;
        }
        else
        {
            button.interactable = true;
        }
    }

    public void ToggleDropdown()
    {
        if (!isExpanded)
            Expand();
        else
            Collapse();
    }

    public void Expand()
    {
        if (!isExpanded)
        {
            isExpanded = true;
            anim.SetTrigger("Expand");
            ContentPanel.SetActive(true);
            Canvas.ForceUpdateCanvases();
            LayoutRebuilder.MarkLayoutForRebuild(GetComponent<RectTransform>());
        }
    }

    public void Collapse()
    {
        if (isExpanded)
        {
            isExpanded = false;
            anim.SetTrigger("Collapse");
            ContentPanel.SetActive(false);
        }
    }
}
