using UnityEngine;
using UnityEngine.UI;

public class GuildmemberGroup : MonoBehaviour
{
    public enum GuildmemberType { Adventurer, Artisan, Peasant}
    public GuildmemberType guildmemberType;

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
        {
            Expand();
        }
        else
        {
            Collapse();
        }
    }

    public void Expand()
    {
        if (!isExpanded)
        {
            isExpanded = true;
            anim.SetTrigger("Expand");
            ContentPanel.SetActive(true);
        }
    }

    public void Collapse()
    {
        if (isExpanded)
        {
            foreach (GameObject child in ContentPanel.GetChildren())
            {
                if (child.GetComponent<PersonUI>() != null)
                {
                    child.GetComponent<PersonUI>().HideExtensionPanel();
                    Canvas.ForceUpdateCanvases();
                    LayoutRebuilder.MarkLayoutForRebuild(GetComponent<RectTransform>());
                }
            }
            isExpanded = false;
            anim.SetTrigger("Collapse");
            ContentPanel.SetActive(false);
        }
    }
}
